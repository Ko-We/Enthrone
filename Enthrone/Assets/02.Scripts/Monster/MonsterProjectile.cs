using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    MyObject myChar;
    GameManager instance;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    private GameObject MonsterProjectileManager;
    private GameObject TargetPos;
    private GameObject Player;

    public bool Icicle;
    public bool YetiKing;
    public bool Yeti;
    public bool IceSpirit;
    public bool GuideShot;
    public bool BounceShot;
    private bool CoroutineCheck = false;

    public Transform targetPos;
    public Vector3 distance;
    public Vector3 NewDir;
    public Vector3 AttackPoint;
    public Vector2 Boltdir;

    public GameObject HitEffect;
    public GameObject YetiKingWall;
    public GameObject Icicle1, Icicle2;
    
    public float BoltSpeed = 5f;
    public float Damage;

    public GameObject WallParent;

    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        MonsterProjectileManager = GameObject.Find("MonsterProjectileManager");
        if (IceSpirit)
        {
            Damage = instance.UnitDataMgr.GetTemplate(7).Damage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.Find("Player");
        for (int i = 0; i < myChar.HeroNum; i++)
        {
            if (Player.transform.GetChild(i).gameObject.activeSelf == true)
            {
                TargetPos = Player.transform.GetChild(i).gameObject;
            }
        }
        if (Icicle)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
        }
        if (!GuideShot)
        {
            transform.Translate(Vector2.up * BoltSpeed * myChar.SlowSpeed * Time.deltaTime);
        }
        if (GuideShot)
        {
            Vector3 dir = (TargetPos.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotTarget = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * BoltSpeed);
            _rigidbody.velocity = new Vector2(dir.x * BoltSpeed * myChar.SlowSpeed, dir.y * BoltSpeed * myChar.SlowSpeed);
            //StartCoroutine(GuideStart());
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Ground")
        {
            if (col.gameObject.tag == "Player")
            {
                if (myChar.Reflection)
                {
                    Quaternion originalRot = transform.rotation;

                    Vector3 originalRotVec3 = originalRot.eulerAngles;
                    Vector3 newRotation = originalRotVec3 + new Vector3(0, 0, 180);

                    transform.rotation = Quaternion.Euler(newRotation);
                }
                //col.transform.GetComponent<PlayerController>().PlayerHit(1);
                if (!myChar.Reflection)
                {
                    col.SendMessage("PlayerHit", Damage);
                    if (Yeti)
                    {
                        col.gameObject.GetComponent<PlayerController>().SlowUse(3f, 0.5f);
                        //if (myChar.Chapter < 2)
                        //{
                        //    col.gameObject.GetComponent<PlayerController>().SlowUse(2f, 0.5f);
                        //}
                        //else if (myChar.Chapter >= 2)
                        //{
                        //    col.gameObject.GetComponent<PlayerController>().SlowUse(2f, 0.25f);
                        //}
                    }
                    //col.gameObject.GetComponent<PlayerController>().StartCoroutine(SlowEffect(1f));

                    Destroy(gameObject);

                    if (YetiKing)
                    {
                        col.gameObject.GetComponent<PlayerController>().SlowUse(2f, 0.25f);

                        //if (myChar.Chapter < 2)
                        //{
                        //    col.gameObject.GetComponent<PlayerController>().SlowUse(2f, 0.5f);
                        //}
                        //else if (myChar.Chapter >= 2)
                        //{
                        //    col.gameObject.GetComponent<PlayerController>().SlowUse(2f, 0.25f);
                        //}
                    }
                }
                GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                Effect.transform.parent = MonsterProjectileManager.transform;
                if (Effect.GetComponent<ExplosionDamage>())
                {
                    Effect.GetComponent<ExplosionDamage>().Damage = Damage;
                }
                Destroy(Effect, 2f);
            }
            else if (col.gameObject.tag == "Ground")
            {
                if (!Icicle)
                {
                    GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                    Effect.transform.parent = MonsterProjectileManager.transform;
                    if (Effect.GetComponent<ExplosionDamage>())
                    {
                        Effect.GetComponent<ExplosionDamage>().Damage = Damage;
                    }
                    
                    Destroy(Effect, 2f);
                    Destroy(gameObject);
                }                
                else if (Icicle)
                {
                    GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                    Effect.transform.parent = MonsterProjectileManager.transform;
                    if (Effect.GetComponent<ExplosionDamage>())
                    {
                        Effect.GetComponent<ExplosionDamage>().Damage = Damage;
                    }
                    Icicle1.SetActive(false);
                    Icicle2.SetActive(true);
                    Destroy(gameObject, 0.1f);
                    Destroy(Effect, 2f);
                    BoltSpeed = 0;
                }
                if (YetiKing)
                {
                    if (col.gameObject.tag != "DestroyWall")
                    {
                        GameObject Wall = Instantiate(YetiKingWall, transform.position, Quaternion.identity);
                        Wall.GetComponent<MonsterYetiKingWall>().PosCheck = false;
                        //Wall.transform.parent = WallParent.transform;
                        SoundManager.Instance.PlaySfx(67);

                        //Wall.transform.localPosition = new Vector2(transform.position.x, -2.35f);
                    }
                    //if (myChar.Chapter == 2)
                    //{
                    //    if (col.gameObject.tag == "DestroyWall")
                    //    {
                    //        Debug.Log("King");
                    //    }
                    //    else if (col.gameObject.tag != "DestroyWall")
                    //    {
                    //        GameObject Wall = Instantiate(YetiKingWall, transform.position, Quaternion.identity);
                    //        Wall.transform.parent = WallParent.transform;
                    //        SoundManager.Instance.PlaySfx(67);

                    //        //Wall.transform.localPosition = new Vector2(transform.position.x, -2.35f);
                    //    }
                    //}                    
                }
            }
        }
        else if (col.gameObject.tag == "YetiKingWall")
        {
            if (!Icicle)
            {
                GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                Effect.transform.parent = MonsterProjectileManager.transform;
                Destroy(Effect, 2f);
                Destroy(gameObject);

                col.gameObject.GetComponent<MonsterYetiKingWall>().ThawWallRefrash();
            }
            else if (Icicle)
            {
                GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                Effect.transform.parent = MonsterProjectileManager.transform;
                if (Effect.GetComponent<ExplosionDamage>())
                {
                    Effect.GetComponent<ExplosionDamage>().Damage = Damage;
                }
                Icicle1.SetActive(false);
                Icicle2.SetActive(true);
                Destroy(Effect, 2f);
                Destroy(gameObject, 0.1f);
                BoltSpeed = 0;
            }         
        }
        else if (col.gameObject.tag == "DestroyWall")
        {
            GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
            Effect.transform.parent = MonsterProjectileManager.transform;
            if (Effect.GetComponent<ExplosionDamage>())
            {
                Effect.GetComponent<ExplosionDamage>().Damage = Damage;
            }

            Destroy(Effect, 2f);
            Destroy(gameObject);
        }
    }
    IEnumerator GuideStart()
    {
        Vector3 dir = (TargetPos.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotTarget = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * BoltSpeed);
        _rigidbody.velocity = new Vector2(dir.x * BoltSpeed * myChar.SlowSpeed, dir.y * BoltSpeed * myChar.SlowSpeed);

        yield return new WaitForSeconds(3f);
        bool Turncheck = false;
        if (!Turncheck)
        {
            Vector3 dir2 = TargetPos.transform.position - transform.position;
            float angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis((angle - 90), Vector3.forward);
            Turncheck = true;
        }

        transform.Translate(Vector2.up * BoltSpeed * myChar.SlowSpeed * Time.deltaTime);

    }
}
