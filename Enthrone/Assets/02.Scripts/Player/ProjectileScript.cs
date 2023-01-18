using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    MyObject myChar;
    GameManager instance;

    private Material _shader;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    private Collider2D _WallonlyCollider2D;
    private GameObject PlayerObjectManager;

    public Transform targetPos;
    public Vector3 distance;
    public Vector3 dir;
    public Vector3 NewDir;
    public Vector3 AttackPoint;

    public GameObject HitEffect;
    public GameObject SpikeProjectile;
    public Vector3 lastVelocity;

    ContactPoint2D[] contants = new ContactPoint2D[2];

    public bool MonsterHit = true;
    public bool WarriorBounce = false;
    public bool WizardBounce = false;

    bool die;

    public float WeaponSpeed = 5f;
    public float Damage;
    public int ElementalCnt;
    public float SlowTime;
    [SerializeField]
    private float BounceShootSpeed = 1f; //튕길때 미사일 속도
    private int bounceCnt;
    [SerializeField]
    private int WallbounceCnt;
    public int nextindex;
    public int closeindex;

    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _shader = GetComponentInChildren<Renderer>().material;
        _collider2D = GetComponent<Collider2D>();
        _WallonlyCollider2D = transform.GetChild(1).GetComponent<Collider2D>();
        bounceCnt = myChar.bounceCnt;
        WallbounceCnt = myChar.WallbounceCnt;
        PlayerObjectManager = GameObject.Find("PlayerObjectManager");

        die = false;
        
        switch (myChar.SelectHero)
        {
            case 0:
                SoundManager.Instance.PlaySfx(11);
                break;
            case 1:
                SoundManager.Instance.PlaySfx(14);
                break;
            case 2:
                SoundManager.Instance.PlaySfx(6);
                break;
            case 3:
                SoundManager.Instance.PlaySfx(15);
                break;
            case 4:
                SoundManager.Instance.PlaySfx(12);
                break;
        }
        _rigidbody.velocity = transform.up * WeaponSpeed;
    }

    private void Update()
    {
        lastVelocity = _rigidbody.velocity;
        if (lastVelocity == new Vector3(0,0,0))
        {
            Destroy(gameObject);
        }
        //Debug.Log(_rigidbody.velocity);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (myChar.Tutorial)
        {
            if (myChar.TutorialNum == 3)
            {
                if (myChar.SelectStoneNum == 0)
                {
                    Damage = 0.3f;
                }
            }
            //else if (myChar.TutorialNum == 5)
            //{
            //    if (!myChar.TutoActiveItemUse)
            //    {
            //        Damage = 0f;
            //    }
            //}
        }
        if (!myChar.Wizard)
        {
            //_rigidbody.velocity = transform.up * WeaponSpeed;
            //transform.Translate(Vector2.up * WeaponSpeed * Time.fixedDeltaTime);
        }

        //if (myChar.Warrior && WarriorBounce)
        //{
        //    transform.Translate(Vector2.up * WeaponSpeed * Time.deltaTime);
        //}
       

        if (myChar.Wizard)
        {
            if (targetPos == null)
            {
                transform.Translate(dir.normalized * WeaponSpeed * Time.fixedDeltaTime);
                return;
            }
            if (!WizardBounce)
            {
                dir = targetPos.position - transform.position;
                transform.Translate(dir.normalized * WeaponSpeed * Time.fixedDeltaTime);
                //_rigidbody.AddForce(dir.normalized * WeaponSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.up * WeaponSpeed * Time.fixedDeltaTime);
            }

        }

        if (myChar.Ninja)
        {
            transform.GetChild(0).Rotate(new Vector3(0, 0, 1620) * Time.fixedDeltaTime);
        }
    }
    Vector2 ResultDir(int index)
    {
        int closetIndex = -1;
        float closeDis = 2f;        //범위안에 적이있을시 튕김
        float currentDis = 0f;

        for (int i = 0; i < PlayerTargeting.Instance.MonsterList.Count; i++)
        {
            //if (i == index) continue;
            //// 미사일 날라가는 방향 코드
            //if (PlayerTargeting.Instance.MonsterList[i] != null)
            //{
            //    Vector3 dir = PlayerTargeting.Instance.MonsterList[i].transform.Find("HitPoint").position - transform.position;
            //    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //    transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            //    currentDis = Vector2.Distance(PlayerTargeting.Instance.MonsterList[i].transform.Find("HitPoint").position, transform.position);
            //    if (currentDis > 5f) continue;

            //    if (closeDis > currentDis)
            //    {
            //        closeDis = currentDis;
            //        closetIndex = i;
            //    }
            //}
            if (i == index) continue;
            // 미사일 날라가는 방향 코드

            if (PlayerTargeting.Instance.MonsterList[i])
            {

            }
            //일정 거리내에 있을때만 몬스터의 인덱스 저장
            currentDis = Vector2.Distance(PlayerTargeting.Instance.MonsterList[i].transform.Find("HitPoint").position, transform.position);
            Vector3 dir = PlayerTargeting.Instance.MonsterList[i].transform.Find("HitPoint").position - transform.position;
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
        return (PlayerTargeting.Instance.MonsterList[closetIndex].transform.Find("HitPoint").position - transform.position).normalized;
    }

    private void OnDrawGizmos()
    {
        //close Dis 거리표시
        //Gizmos.DrawWireSphere(transform.position,2f);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Monster"))
        {
            if (col.gameObject.CompareTag("Monster"))
            {
                //if (myChar.TutorialNum == 5)
                //{
                //    col.gameObject.transform.GetChild(0).GetComponent<FollowMonster>().AttackCnt--;
                //}
                if (myChar.SelectStoneNum == 0)
                {
                    SoundManager.Instance.PlaySfx(10);
                }
                else if (ElementalCnt == 1)
                {
                    switch (myChar.ElementStoneAll[0])
                    {
                        case 0:
                            Damage = Damage + (Damage * myChar.AntiqueitemDataMgr.GetTemplate(1000).Lv1 / 100);
                            break;
                        case 1:
                            Damage = Damage + (Damage * myChar.AntiqueitemDataMgr.GetTemplate(1000).Lv2 / 100);
                            break;
                        case 2:
                            Damage = Damage + (Damage * myChar.AntiqueitemDataMgr.GetTemplate(1000).Lv3 / 100);
                            break;
                        case 3:
                            Damage = Damage + (Damage * myChar.AntiqueitemDataMgr.GetTemplate(1000).Lv4 / 100);
                            break;
                        case 4:
                            Damage = Damage + (Damage * myChar.AntiqueitemDataMgr.GetTemplate(1000).Lv5 / 100);
                            break;
                        case 5:
                            Damage = Damage + (Damage * myChar.AntiqueitemDataMgr.GetTemplate(1000).Lv6 / 100);
                            break;
                        case 6:
                            Damage = Damage + (Damage * myChar.AntiqueitemDataMgr.GetTemplate(1000).Lv7 / 100);
                            break;
                        case 7:
                            Damage = Damage + (Damage * myChar.AntiqueitemDataMgr.GetTemplate(1000).Lv8 / 100);
                            break;
                        case 8:
                            Damage = Damage + (Damage * myChar.AntiqueitemDataMgr.GetTemplate(1000).Lv9 / 100);
                            break;
                        case 9:
                            Damage = Damage + (Damage * myChar.AntiqueitemDataMgr.GetTemplate(1000).Lv10 / 100);
                            break;
                        case 10:
                            Damage = Damage + (Damage * myChar.AntiqueitemDataMgr.GetTemplate(1000).Lv11 / 100);
                            break;
                    }
                }
                else if (ElementalCnt == 2)
                {
                    switch (myChar.ElementStoneAll[1])
                    {
                        case 0:
                            SlowTime = myChar.AntiqueitemDataMgr.GetTemplate(1001).Lv1;
                            break;
                        case 1:
                            SlowTime = myChar.AntiqueitemDataMgr.GetTemplate(1001).Lv2;
                            break;
                        case 2:
                            SlowTime = myChar.AntiqueitemDataMgr.GetTemplate(1001).Lv3;
                            break;
                        case 3:
                            SlowTime = myChar.AntiqueitemDataMgr.GetTemplate(1001).Lv4;
                            break;
                        case 4:
                            SlowTime = myChar.AntiqueitemDataMgr.GetTemplate(1001).Lv5;
                            break;
                        case 5:
                            SlowTime = myChar.AntiqueitemDataMgr.GetTemplate(1001).Lv6;
                            break;
                        case 6:
                            SlowTime = myChar.AntiqueitemDataMgr.GetTemplate(1001).Lv7;
                            break;
                        case 7:
                            SlowTime = myChar.AntiqueitemDataMgr.GetTemplate(1001).Lv8;
                            break;
                        case 8:
                            SlowTime = myChar.AntiqueitemDataMgr.GetTemplate(1001).Lv9;
                            break;
                        case 9:
                            SlowTime = myChar.AntiqueitemDataMgr.GetTemplate(1001).Lv10;
                            break;
                        case 10:
                            SlowTime = myChar.AntiqueitemDataMgr.GetTemplate(1001).Lv11;
                            break;
                    }
                    col.SendMessage("IndividualSlowCheck", SlowTime);
                }
                else if (ElementalCnt == 3)
                {

                    switch (myChar.ElementStoneAll[2])
                    {
                        case 0:
                            eightProjectile(col.transform.position, myChar.AntiqueitemDataMgr.GetTemplate(1002).Lv1, col.gameObject);
                            break;
                        case 1:
                            eightProjectile(col.transform.position, myChar.AntiqueitemDataMgr.GetTemplate(1002).Lv2, col.gameObject);
                            break;
                        case 2:
                            eightProjectile(col.transform.position, myChar.AntiqueitemDataMgr.GetTemplate(1002).Lv3, col.gameObject);
                            break;
                        case 3:
                            eightProjectile(col.transform.position, myChar.AntiqueitemDataMgr.GetTemplate(1002).Lv4, col.gameObject);
                            break;
                        case 4:
                            eightProjectile(col.transform.position, myChar.AntiqueitemDataMgr.GetTemplate(1002).Lv5, col.gameObject);
                            break;
                        case 5:
                            eightProjectile(col.transform.position, myChar.AntiqueitemDataMgr.GetTemplate(1002).Lv6, col.gameObject);
                            break;
                        case 6:
                            eightProjectile(col.transform.position, myChar.AntiqueitemDataMgr.GetTemplate(1002).Lv7, col.gameObject);
                            break;
                        case 7:
                            eightProjectile(col.transform.position, myChar.AntiqueitemDataMgr.GetTemplate(1002).Lv8, col.gameObject);
                            break;
                        case 8:
                            eightProjectile(col.transform.position, myChar.AntiqueitemDataMgr.GetTemplate(1002).Lv9, col.gameObject);
                            break;
                        case 9:
                            eightProjectile(col.transform.position, myChar.AntiqueitemDataMgr.GetTemplate(1002).Lv10, col.gameObject);
                            break;
                        case 10:
                            eightProjectile(col.transform.position, myChar.AntiqueitemDataMgr.GetTemplate(1002).Lv11, col.gameObject);
                            break;
                    }
                }
                else if (ElementalCnt == 4)
                {
                    SoundManager.Instance.PlaySfx(25);
                    if (col.gameObject.CompareTag("Monster"))
                    {
                        switch (myChar.ElementStoneAll[3])
                        {
                            case 0:
                                HealingSton((int)myChar.AntiqueitemDataMgr.GetTemplate(1003).Lv1);
                                break;
                            case 1:
                                HealingSton((int)myChar.AntiqueitemDataMgr.GetTemplate(1003).Lv2);
                                break;
                            case 2:
                                HealingSton((int)myChar.AntiqueitemDataMgr.GetTemplate(1003).Lv3);
                                break;
                            case 3:
                                HealingSton((int)myChar.AntiqueitemDataMgr.GetTemplate(1003).Lv4);
                                break;
                            case 4:
                                HealingSton((int)myChar.AntiqueitemDataMgr.GetTemplate(1003).Lv5);
                                break;
                            case 5:
                                HealingSton((int)myChar.AntiqueitemDataMgr.GetTemplate(1003).Lv6);
                                break;
                            case 6:
                                HealingSton((int)myChar.AntiqueitemDataMgr.GetTemplate(1003).Lv7);
                                break;
                            case 7:
                                HealingSton((int)myChar.AntiqueitemDataMgr.GetTemplate(1003).Lv8);
                                break;
                            case 8:
                                HealingSton((int)myChar.AntiqueitemDataMgr.GetTemplate(1003).Lv9);
                                break;
                            case 9:
                                HealingSton((int)myChar.AntiqueitemDataMgr.GetTemplate(1003).Lv10);
                                break;
                            case 10:
                                HealingSton((int)myChar.AntiqueitemDataMgr.GetTemplate(1003).Lv11);
                                break;
                        }
                    }
                }

                //리꼬셰
                if ((myChar.Ninja || instance.PlayerSkill[0] != 0) && PlayerTargeting.Instance.MonsterList.Count >= 2)
                {
                    if (myChar.Wizard)
                    {
                        WizardBounce = true;
                    }
                    int myIndex = PlayerTargeting.Instance.MonsterList.IndexOf(col.gameObject);

                    col.SendMessage("AttackHit", Damage);

                    if (bounceCnt > 0)
                    {
                        var speed = lastVelocity.magnitude;
                        NewDir = ResultDir(myIndex) * BounceShootSpeed;
                        float angle = Mathf.Atan2(NewDir.y, NewDir.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                        GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);

                        if (ElementalCnt == 2)
                        {
                            Effect.GetComponent<FrozenExplosion>().SlowTime = SlowTime;
                        }
                        Effect.transform.parent = PlayerObjectManager.transform;
                        Destroy(Effect, 2f);
                        bounceCnt--;
                        Damage *= 0.5f;
                        _rigidbody.velocity = NewDir * Mathf.Max(speed, 0.5f);

                        _rigidbody.velocity = transform.up * WeaponSpeed;
                       
                        MonsterHit = false;     //이것도 왜있는걸까?
                        nextindex = closeindex;
                        
                        //nextindex = myIndex;

                        //if문을 왜 나눈건지 모르겠음 있을이유가없는데 버그나면 확인하기!
                        //if (MonsterHit)
                        //{
                        //    GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                        //    if (ElementalCnt == 2)
                        //    {
                        //        Effect.GetComponent<FrozenExplosion>().SlowTime = SlowTime;
                        //    }
                        //    Effect.transform.parent = PlayerObjectManager.transform;
                        //    Destroy(Effect, 2f);
                        //    bounceCnt--;
                        //    Damage *= 0.7f;
                        //    _rigidbody.velocity = NewDir;
                        //    MonsterHit = false;
                        //    nextindex = closeindex;
                        //}
                        //else if (PlayerTargeting.Instance.MonsterList.IndexOf(col.gameObject) == nextindex)
                        //{
                        //    GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                        //    if (ElementalCnt == 2)
                        //    {
                        //        Effect.GetComponent<FrozenExplosion>().SlowTime = SlowTime;
                        //    }
                        //    Effect.transform.parent = PlayerObjectManager.transform;
                        //    Destroy(Effect, 2f);
                        //    bounceCnt--;
                        //    Damage *= 0.7f;
                        //    _rigidbody.velocity = NewDir;
                        //    MonsterHit = false;
                        //    nextindex = closeindex;
                        //}
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
                    col.SendMessage("AttackHit", Damage);
                    GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                    if (ElementalCnt == 2)
                    {
                        Effect.GetComponent<FrozenExplosion>().SlowTime = SlowTime;
                    }
                    Effect.transform.parent = PlayerObjectManager.transform;
                    _collider2D.enabled = false;
                    Destroy(Effect, 2f);
                    Destroy(gameObject);
                }

            }
            else if (col.gameObject.CompareTag("Ground"))
            {
                if (GameManager.Instance.PlayerSkill[17] == 0)
                {
                    if (myChar.SwordMan || myChar.Ninja)
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
                    else if (myChar.Warrior)
                    {
                        WeaponSpeed = 0;
                        _rigidbody.AddForce((-dir).normalized * 50);
                        StartCoroutine(FadeAway());
                        //_rigidbody.velocity = Vector2.zero;
                        //Destroy(gameObject, 0.5f);
                        //_rigidbody.gravityScale = 0f;
                    }
                    else if (myChar.Archer)
                    {
                        WeaponSpeed = 0;
                        _spriteRenderer.sortingOrder = 0;
                        StartCoroutine(Arrow());
                    }
                    else if (myChar.Wizard)
                    {
                        Destroy(gameObject);
                    }
                    else if (!myChar.SwordMan)
                    {
                        _rigidbody.velocity = Vector2.zero;
                        Destroy(gameObject, 0.5f);
                    }
                    GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                    if (ElementalCnt == 2)
                    {
                        Effect.GetComponent<FrozenExplosion>().SlowTime = SlowTime;
                    }
                    Effect.transform.parent = PlayerObjectManager.transform;
                    Destroy(Effect, 2f);
                    _collider2D.enabled = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            if (GameManager.Instance.PlayerSkill[17] != 0)
            {
                var speed = lastVelocity.magnitude;
                if (WallbounceCnt > 0)
                {
                    WallbounceCnt--;
                    var direction = Vector2.Reflect(lastVelocity.normalized, col.contacts[0].normal);
                    _rigidbody.velocity = direction * Mathf.Max(speed, 4f);
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    Damage *= 0.5f;

                    //NewDir = Vector2.Reflect(NewDir, col.contacts[0].normal);

                    //_rigidbody.velocity = NewDir * 20f;
                    return;
                }
                else
                {
                    if (myChar.SwordMan || myChar.Ninja)
                    {
                        _rigidbody.velocity = Vector2.zero;
                        _rigidbody.AddForce((-dir).normalized * 50f);
                        StartCoroutine(FadeAway());

                        //날라갈때 방향변경점
                        //float angle = Mathf.Atan2(reflection.y, reflection.x) * Mathf.Rad2Deg;
                        //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);                    
                    }
                    else if (myChar.Warrior)
                    {
                        _rigidbody.velocity = Vector2.zero;
                        _rigidbody.AddForce((-dir).normalized * 50f);
                        StartCoroutine(FadeAway());
                        //_rigidbody.velocity = Vector2.zero;
                        //Destroy(gameObject, 0.5f);
                        //_rigidbody.gravityScale = 0f;
                    }
                    else if (myChar.Archer)
                    {
                        _rigidbody.velocity = Vector2.zero;
                        _spriteRenderer.sortingOrder = 0;
                        StartCoroutine(Arrow());
                    }
                    else if (myChar.Wizard)
                    {
                        Destroy(gameObject);
                    }
                    else if (!myChar.SwordMan)
                    {
                        _rigidbody.velocity = Vector2.zero;
                        Destroy(gameObject, 0.5f);
                    }
                    GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                    if (ElementalCnt == 2)
                    {
                        Effect.GetComponent<FrozenExplosion>().SlowTime = SlowTime;
                    }
                    Effect.transform.parent = PlayerObjectManager.transform;
                    Destroy(Effect, 2f);
                    _collider2D.enabled = false;
                    _WallonlyCollider2D.enabled = false;
                }
            }
            //_rigidbody.velocity = Vector2.zero;
            //Destroy(gameObject);
            else
            {
                if (myChar.SwordMan || myChar.Ninja)
                {
                    _rigidbody.velocity = Vector2.zero;
                    _rigidbody.AddForce((-dir).normalized * 50f);
                    StartCoroutine(FadeAway());

                    //날라갈때 방향변경점
                    //float angle = Mathf.Atan2(reflection.y, reflection.x) * Mathf.Rad2Deg;
                    //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);                    
                }
                else if (myChar.Warrior)
                {
                    _rigidbody.velocity = Vector2.zero;
                    _rigidbody.AddForce((-dir).normalized * 50f);
                    StartCoroutine(FadeAway());
                    //_rigidbody.velocity = Vector2.zero;
                    //Destroy(gameObject, 0.5f);
                    //_rigidbody.gravityScale = 0f;
                }
                else if (myChar.Archer)
                {
                    _rigidbody.velocity = Vector2.zero;
                    _spriteRenderer.sortingOrder = 0;
                    StartCoroutine(Arrow());
                }
                else if (myChar.Wizard)
                {
                    Destroy(gameObject);
                }
                else if (!myChar.SwordMan)
                {
                    _rigidbody.velocity = Vector2.zero;
                    Destroy(gameObject, 0.5f);
                }
                GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                if (ElementalCnt == 2)
                {
                    Effect.GetComponent<FrozenExplosion>().SlowTime = SlowTime;
                }
                Effect.transform.parent = PlayerObjectManager.transform;
                Destroy(Effect, 2f);
                _collider2D.enabled = false;
                _WallonlyCollider2D.enabled = false;
            }

        }
        
    }

    private void IceProjectile()
    {
        
    }
    private void HealingSton(int Cnt)
    {
        int RandomCheck = Random.Range(0, 100);
        if (RandomCheck < Cnt)
        {
            myChar.currentHp += (int)Mathf.Ceil(myChar.maxHp * 0.01f);
            SoundManager.Instance.PlaySfx(43);
            GameManager.Instance.HealingEffect_On();
        }
    }
    private void eightProjectile(Vector3 Pos, float ScriptDamage, GameObject HitMonster)
    {
        for (int i = 0; i < 360; i += 45)
        {
            GameObject temp = Instantiate(SpikeProjectile, Pos, Quaternion.identity);
            temp.GetComponent<SpikeProjectile>().BoltSpeed = 3f;
            temp.GetComponent<SpikeProjectile>().Damage = Damage * (0.01f * ScriptDamage);
            temp.transform.parent = PlayerObjectManager.transform;
            temp.GetComponent<SpikeProjectile>().HitMonster = HitMonster;
            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }
    

    IEnumerator rotationWeapone()
    {
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator FadeAway()
    {
        float Cnt = 1;
        yield return new WaitForSeconds(0.1f);

        while (_spriteRenderer.color.a > 0)
        {
            //var color = _spriteRenderer.color;
            //color.a -= (2f * Time.deltaTime);
            Cnt -= (2f * Time.deltaTime);
            _shader.SetFloat("_Alpha", Cnt);
            transform.Rotate(new Vector3(0, 0, 1) * 1080 * Time.deltaTime);

            //_spriteRenderer.color = color;
            Destroy(gameObject, 0.5f);
            yield return null;
        }
       
    }

    IEnumerator Arrow()
    {
        yield return new WaitForSeconds(0.007f);

        _rigidbody.velocity = new Vector2(0, 0);

        Destroy(gameObject, 1f);
    }

}
