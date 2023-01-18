using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossProjectile : MonoBehaviour
{
    public enum BossType
    {
        Tutorial,
        SwordMan,
        Warrior,
        Archer,
        Wizard,
        Ninja
    }
    MyObject myChar;
    GameManager instance;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    private GameObject PlayerObjectManager;

    public Transform targetPos;
    public Vector3 distance;
    public Vector3 dir;
    public Vector3 NewDir;
    public Vector3 AttackPoint;

    public BossType bossType;

    public GameObject HitEffect;

    ContactPoint2D[] contants = new ContactPoint2D[2];

    public bool MonsterHit = true;
    public bool WarriorBounce = false;
    public bool WizardBounce = false;

    bool die;

    public float WeaponSpeed = 5f;
    public float Damage;
    private float BounceShootSpeed = 1; //튕길때 미사일 속도
    private int bounceCnt;
    public int nextindex;
    public int closeindex;

    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        bounceCnt = myChar.bounceCnt;
        PlayerObjectManager = GameObject.Find("PlayerObjectManager");

        die = false;
        //if (myChar.SelectStoneNum == 1)
        //{
        //    if (myChar.ElementStone_Lv == 1)
        //    {
        //        Damage = Damage * 1.1f;
        //    }
        //    else if (myChar.ElementStone_Lv == 2)
        //    {
        //        Damage = Damage * 1.25f;
        //    }
        //    else if (myChar.ElementStone_Lv == 3)
        //    {
        //        Damage = Damage * 1.5f;
        //    }
        //}
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        translate로 변경중인 코드
        if (myChar.SwordMan || myChar.Archer)
        {
            transform.Translate(Vector2.up * WeaponSpeed * Time.deltaTime);
        }
        else if (myChar.Wizard)
        {
            if (targetPos == null)
            {
                transform.Translate(dir.normalized * WeaponSpeed * Time.deltaTime);
                return;
            }
            dir = targetPos.position - transform.position;
            transform.Translate(dir.normalized * WeaponSpeed * Time.deltaTime);
            //_rigidbody.AddForce(dir.normalized * WeaponSpeed * Time.deltaTime);
        }
        else if (myChar.Ninja || myChar.Warrior)
        {
            transform.Rotate(new Vector3(0, 0, 540) * Time.deltaTime);
        }
        */

        if (bossType != BossType.Wizard)
        {
            transform.Translate(Vector2.up * WeaponSpeed * Time.deltaTime);
        }

        if (bossType == BossType.Wizard)
        {
            if (targetPos == null)
            {
                transform.Translate(dir.normalized * WeaponSpeed * Time.deltaTime);
                return;
            }
            if (!WizardBounce)
            {
                dir = targetPos.position - transform.position;
                transform.Translate(dir.normalized * WeaponSpeed * Time.deltaTime);
                //_rigidbody.AddForce(dir.normalized * WeaponSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.up * WeaponSpeed * Time.deltaTime);
            }

        }

        if (bossType == BossType.Ninja || bossType == BossType.Tutorial)
        {
            transform.GetChild(0).Rotate(new Vector3(0, 0, 1620) * Time.deltaTime);
        }
    }
    Vector2 ResultDir(int index)
    {
        int closetIndex = -1;
        float closeDis = 2f;        //범위안에 적이있을시 튕김
        float currentDis = 0f;

        for (int i = 0; i < FinalBossTargeting.Instance.MonsterList.Count; i++)
        {
            if (i == index) continue;
            // 미사일 날라가는 방향 코드

            currentDis = Vector2.Distance(FinalBossTargeting.Instance.MonsterList[i].transform.Find("HitPoint").position, transform.position);
            Vector3 dir = FinalBossTargeting.Instance.MonsterList[i].transform.Find("HitPoint").position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            if (currentDis > 2f) continue;

            if (closeDis > currentDis)
            {
                closeDis = currentDis;
                closetIndex = i;
                closeindex = closetIndex;
                //Debug.Log("i : " + closetIndex);
            }
        }

        if (closetIndex == -1)
        {
            Destroy(gameObject);
            return Vector2.zero;
        }
        //Debug.Log("cindex : " + closeindex + " / " + "index : " + index);
        return (FinalBossTargeting.Instance.MonsterList[closetIndex].transform.Find("HitPoint").position - transform.position).normalized;
    }

    private void OnDrawGizmos()
    {
        //close Dis 거리표시
        //Gizmos.DrawWireSphere(transform.position,2f);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Ground")
        {
            if (col.gameObject.tag == "Player")
            {
                //리꼬셰
                if ((myChar.Ninja || instance.PlayerSkill[0] != 0) && FinalBossTargeting.Instance.MonsterList.Count >= 2)
                {
                    if (myChar.Warrior)
                    {
                        WarriorBounce = true;
                    }
                    if (myChar.Wizard)
                    {
                        WizardBounce = true;
                    }
                    int myIndex = FinalBossTargeting.Instance.MonsterList.IndexOf(col.gameObject);
                    col.SendMessage("AttackHit", Damage);
                    if (bounceCnt > 0)
                    {
                        NewDir = ResultDir(myIndex) * BounceShootSpeed;
                        float angle = Mathf.Atan2(NewDir.y, NewDir.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                        //nextindex = myIndex;
                        if (MonsterHit)
                        {
                            GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                            Effect.transform.parent = PlayerObjectManager.transform;
                            Destroy(Effect, 2f);
                            bounceCnt--;
                            Damage *= 0.7f;
                            _rigidbody.velocity = NewDir;
                            MonsterHit = false;
                            nextindex = closeindex;
                        }
                        else if (FinalBossTargeting.Instance.MonsterList.IndexOf(col.gameObject) == nextindex)
                        {
                            GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                            Effect.transform.parent = PlayerObjectManager.transform;
                            Destroy(Effect, 2f);
                            bounceCnt--;
                            Damage *= 0.7f;
                            _rigidbody.velocity = NewDir;
                            MonsterHit = false;
                            nextindex = closeindex;
                        }
                        //GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                        //Destroy(Effect, 2f);
                        //bounceCnt--;
                        //Damage *= 0.7f;
                        //NewDir = ResultDir(myIndex) * BounceShootSpeed;
                        //Debug.Log(ResultDir(myIndex));
                        //_rigidbody.velocity = NewDir;

                        if (bounceCnt == 0)
                        {
                            _rigidbody.velocity = Vector2.zero;
                            Destroy(gameObject);
                        }
                        return;
                    }
                }
                else
                {
                    col.SendMessage("PlayerHit", Damage);
                    GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                    Effect.transform.parent = PlayerObjectManager.transform;
                    _collider2D.enabled = false;
                    Destroy(Effect, 2f);
                    Destroy(gameObject);
                }

            }
            else if (col.gameObject.tag == "Ground")
            {
                if (bossType != BossType.Archer || bossType != BossType.Wizard)
                {
                    WeaponSpeed = 0;
                    _rigidbody.AddForce((-dir).normalized * 50);
                    StartCoroutine(FadeAway());

                    //col.GetContacts(contants);
                    //Vector2 nomals = contants[0].normal;
                    //Vector2 reflection = Vector2.Reflect(dir, nomals);
                    //_rigidbody.AddForce(reflection * 50);

                    //날라갈때 방향변경점
                    //float angle = Mathf.Atan2(reflection.y, reflection.x) * Mathf.Rad2Deg;
                    //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);                    
                }
                else if (bossType == BossType.Archer)
                {
                    WeaponSpeed = 0;
                    _spriteRenderer.sortingOrder = 0;
                    StartCoroutine(Arrow());
                }
                else if (bossType == BossType.Wizard)
                {
                    Destroy(gameObject);
                }
                else if (bossType != BossType.SwordMan)
                {
                    _rigidbody.velocity = Vector2.zero;
                    Destroy(gameObject, 0.5f);
                }
                GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                Effect.transform.parent = PlayerObjectManager.transform;
                Destroy(Effect, 2f);
                _collider2D.enabled = false;
            }

        }

    }



    IEnumerator rotationWeapone()
    {
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(0.1f);

        while (_spriteRenderer.color.a > 0)
        {
            var color = _spriteRenderer.color;
            color.a -= (2f * Time.deltaTime);
            transform.Rotate(new Vector3(0, 0, 1) * 1080 * Time.deltaTime);

            _spriteRenderer.color = color;
            yield return null;
        }
        Destroy(gameObject);
    }

    IEnumerator Arrow()
    {
        yield return new WaitForSeconds(0.007f);

        _rigidbody.velocity = new Vector2(0, 0);

        Destroy(gameObject, 1f);
    }

    IEnumerator DestroyOrb()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
