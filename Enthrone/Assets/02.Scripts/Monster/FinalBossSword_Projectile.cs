using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossSword_Projectile : MonoBehaviour
{
    public enum ThroneType
    {
        SwordMan,
        Warrior,
        Archer,
        Wizard,
        Ninja
    }
    public ThroneType throneType;

    MyObject myChar;

    public float Damage;
    public GameObject HitEffect;
    public bool Effect = true;              //나이트(소드맨) 벽에서 생성될때 잠시 생성모션 딜레이주기위함
    public bool ArcherShot = false;         //화살비 시전 모션에서 화살을 날리는데 그화살들은 히어로들 타겟안하게하기위함
    public float ArrowSpeed;
    public bool ArcherArrowLastCheck = false;       //마지막나가는 화살 체크용
    public GameObject FinalBoss;
    private Vector3 startPos;
    private Vector3 crushPos;
    private Rigidbody2D _rigidbody;
    public int SkillNo;
    public float VecX, VecY;
    public float speed;
    public int Bounce = 4;

    private GameObject MonsterProjectileManager;

    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        MonsterProjectileManager = GameObject.Find("MonsterProjectileManager");
        _rigidbody = GetComponent<Rigidbody2D>();
        Bounce = 4;

        if (throneType == ThroneType.Ninja)
        {
            Vector2 dir = new Vector2(VecX, VecY);
            dir = dir.normalized;
            _rigidbody.AddForce(dir * speed);
            startPos = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Effect)
        {
            if (throneType == ThroneType.SwordMan)
            {
                transform.Translate(Vector2.up * myChar.ThroneSkillDataMgr.GetTemplate(SkillNo).K_ProjectileSpeed * myChar.SlowSpeed * Time.deltaTime);
            }
            else if (throneType == ThroneType.Warrior)
            {
                transform.Translate(Vector2.up * myChar.ThroneSkillDataMgr.GetTemplate(SkillNo).W_ProjectileSpeed * myChar.SlowSpeed * Time.deltaTime);
            }
            else if (throneType == ThroneType.Archer)
            {
                if (ArcherShot)
                {
                    transform.Translate(Vector2.up * (myChar.ThroneSkillDataMgr.GetTemplate(SkillNo).A_ProjectileSpeed * ArrowSpeed) * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector2.up * myChar.ThroneSkillDataMgr.GetTemplate(SkillNo).A_ProjectileSpeed * myChar.SlowSpeed * Time.deltaTime);
                }
                
            }
            else if (throneType == ThroneType.Wizard)
            {
                transform.Translate(Vector2.up * myChar.ThroneSkillDataMgr.GetTemplate(SkillNo).Wi_ProjectileSpeed * myChar.SlowSpeed * Time.deltaTime);
            }
            else if (throneType == ThroneType.Ninja)
            {

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (throneType != ThroneType.Archer && throneType != ThroneType.Ninja)
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
                    if (!myChar.Reflection)
                    {
                        col.SendMessage("PlayerHit", Damage);
                        //col.gameObject.GetComponent<PlayerController>().StartCoroutine(SlowEffect(1f));
                        Destroy(gameObject);
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
        else if (throneType == ThroneType.Archer)
        {
            if (col.gameObject.tag == "Player" || col.gameObject.tag == "Ground")
            {
                if (!ArcherShot)
                {
                    if (col.gameObject.tag == "Player")
                    {

                        if (ArcherArrowLastCheck)
                        {
                            FinalBoss.GetComponent<FinalBossPattern>().ArcherTeleport();
                        }
                        if (myChar.Reflection)
                        {
                            Quaternion originalRot = transform.rotation;

                            Vector3 originalRotVec3 = originalRot.eulerAngles;
                            Vector3 newRotation = originalRotVec3 + new Vector3(0, 0, 180);

                            transform.rotation = Quaternion.Euler(newRotation);
                        }
                        if (!myChar.Reflection)
                        {
                            col.SendMessage("PlayerHit", Damage);
                            //col.gameObject.GetComponent<PlayerController>().StartCoroutine(SlowEffect(1f));
                            Destroy(gameObject);
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
                        GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                        Effect.transform.parent = MonsterProjectileManager.transform;
                        if (ArcherArrowLastCheck)
                        {
                            FinalBoss.GetComponent<FinalBossPattern>().ArcherTeleport();
                        }

                        if (Effect.GetComponent<ExplosionDamage>())
                        {
                            Effect.GetComponent<ExplosionDamage>().Damage = Damage;
                        }

                        Destroy(Effect, 2f);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    if (col.gameObject.tag == "Ground")
                    {
                        //GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                        //Effect.transform.parent = MonsterProjectileManager.transform;

                        //if (Effect.GetComponent<ExplosionDamage>())
                        //{
                        //    Effect.GetComponent<ExplosionDamage>().Damage = Damage;
                        //}
                        if (ArcherArrowLastCheck)
                        {
                            FinalBoss.GetComponent<FinalBossPattern>().ArcherArrowRain();

                            Destroy(gameObject, 2f);
                            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                        }
                        else
                        {
                            Destroy(gameObject);
                        }

                        //Destroy(Effect, 2f);
                        
                        
                    }
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
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (throneType == ThroneType.Ninja)
        {
            if (col.gameObject.tag == "Ground")
            {
                Bounce--;
                if (Bounce <= 0)
                {
                    GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                    Destroy(Effect, 2f);
                    Destroy(gameObject);
                }                
            }

        }
    }
    public void EffectOffCheck()
    {
        Effect = false;
    }
}
