using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDestroy : MonoBehaviour
{
    MyObject myChar;

    public enum TrapType
    {
        NotTrapMonster,
        InstallationTypeTrap,
        CircleProjectileTrap
    }
    public int MonsterIndex;
    public TrapType traptypes;
    public GameObject DeathEffect;
    public GameObject DeathEffect2;
    public GameObject RockBossDeathEffect;
    public GameObject TrapObject, SkillObject;
    public GameObject MonsterProjectile;
    public Transform DeathEffectPos;
    public float DestroySecond;
    public bool SpriteFalseCheck;
    public bool ObjectDestroyCheck;
    public bool DeathTrap;
    public bool TagUnchange;
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        MonsterProjectile = GameObject.Find("MonsterProjectileManager");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    GameObject Effect = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        //    if (myChar.Stage == myChar.BasicStage + 3)
        //    {
        //        Effect.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        //    }

        //}
    }

    private void MonsterDeathEffect()
    {
        GameObject FoundationEffect = Instantiate(DeathEffect2, transform.position, Quaternion.identity);
        Destroy(FoundationEffect, 2.5f);

        if (!DeathEffectPos)
        {
            if (ObjectDestroyCheck)
            {
                Destroy(gameObject, DestroySecond);
                gameObject.layer = LayerMask.NameToLayer("DeathMonster");
                gameObject.tag = "DeathMonster";
            }
            if (!DeathEffect)
            {
                return;
            }
            GameObject Effect = Instantiate(DeathEffect, transform.position, Quaternion.identity);
            if (myChar.Stage % 10 == 0)
            {
                Effect.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            Destroy(Effect, 2.5f);
            //GameObject Effect2 = Instantiate(DeathEffect2, transform.position, Quaternion.identity);
            //Destroy(Effect2, 2.5f);
            //GameObject Effect2 = Instantiate(DeathEffect2, DeathEffectPos.transform.position, Quaternion.identity);
            //Destroy(Effect2, 2.5f);
            //gameObject.layer = LayerMask.NameToLayer("DeathMonster");
            //gameObject.tag = "DeathMonster";
        }
        else if (DeathEffectPos)
        {
            if (!DeathEffect)
            {
                return;
            }
            GameObject Effect = Instantiate(DeathEffect, DeathEffectPos.transform.position, Quaternion.identity);
            Destroy(Effect, 2.5f);
            //gameObject.layer = LayerMask.NameToLayer("DeathMonster");
            //gameObject.tag = "DeathMonster";
        }
        

        if (!transform.parent || transform.parent.name == "Monster")
        {
            if (!DeathTrap)
            {
                //if (DestroySecond != 0)
                //{
                //    Destroy(gameObject, DestroySecond);
                //    gameObject.layer = LayerMask.NameToLayer("DeathMonster");
                //    gameObject.tag = "DeathMonster";
                //}
                Destroy(gameObject, 2.5f);

                gameObject.layer = LayerMask.NameToLayer("DeathMonster");
                gameObject.tag = "DeathMonster";
            }
            else if (DeathTrap)
            {
                if (traptypes == TrapType.InstallationTypeTrap)
                {
                    if (transform.GetComponent<MonsterScript>().MonsterIndex == 10) //캐터스(선인장) 죽었을때 폭발하기위해 분류함[Page체크떄문에]
                    {
                        gameObject.layer = LayerMask.NameToLayer("DeathMonster");
                        gameObject.tag = "DeathMonster";
                        GameObject Trap = Instantiate(TrapObject, transform.position, Quaternion.identity);
                        Trap.transform.parent = MonsterProjectile.transform;
                        Trap.transform.localScale = new Vector2(1, 1);
                        if (Trap.GetComponent<TrapDamage>())
                        {
                            Trap.GetComponent<TrapDamage>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(transform.GetComponent<MonsterScript>().MonsterIndex).Damage;
                        }
                        if (transform.GetComponent<MonsterScript>().MonsterIndex == 10)
                        {
                            Trap.GetComponent<ExplosionDamage>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(transform.GetComponent<MonsterScript>().MonsterIndex).Damage;
                        }
                        Destroy(Trap, DestroySecond);
                        //if (myChar.Chapter >= 1)
                        //{
                        //    gameObject.layer = LayerMask.NameToLayer("DeathMonster");
                        //    gameObject.tag = "DeathMonster";
                        //    GameObject Trap = Instantiate(TrapObject, transform.position, Quaternion.identity);
                        //    Trap.transform.parent = MonsterProjectile.transform;
                        //    Trap.transform.localScale = new Vector2(1, 1);
                        //    if (Trap.GetComponent<TrapDamage>())
                        //    {
                        //        Trap.GetComponent<TrapDamage>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(transform.GetComponent<MonsterScript>().MonsterIndex).Damage;
                        //    }
                        //    if (transform.GetComponent<MonsterScript>().MonsterIndex == 10)
                        //    {
                        //        Trap.GetComponent<ExplosionDamage>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(transform.GetComponent<MonsterScript>().MonsterIndex).Damage;
                        //    }
                        //    Destroy(Trap, DestroySecond);
                        //}

                    }
                    else
                    {
                        gameObject.layer = LayerMask.NameToLayer("DeathMonster");
                        gameObject.tag = "DeathMonster";
                        GameObject Trap = Instantiate(TrapObject, transform.position, Quaternion.identity);
                        Trap.transform.parent = MonsterProjectile.transform;
                        Trap.transform.localScale = new Vector2(1, 1);
                        if (Trap.GetComponent<TrapDamage>())
                        {
                            Trap.GetComponent<TrapDamage>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(transform.GetComponent<MonsterScript>().MonsterIndex).Damage;
                        }
                        if (transform.GetComponent<MonsterScript>().MonsterIndex == 10)
                        {
                            Trap.GetComponent<ExplosionDamage>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(transform.GetComponent<MonsterScript>().MonsterIndex).Damage;
                        }

                        Destroy(Trap, DestroySecond);
                    }                   
                }
                else if (traptypes == TrapType.CircleProjectileTrap)
                {
                    for (int i = 0; i < 360; i += 45)
                    {
                        GameObject temp = Instantiate(TrapObject, DeathEffectPos.transform.position, Quaternion.identity);
                        temp.GetComponent<MonsterProjectile>().BoltSpeed = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                        temp.GetComponent<MonsterProjectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                        temp.transform.parent = MonsterProjectile.transform;

                        Destroy(temp, 2f);

                        //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
                        temp.transform.rotation = Quaternion.Euler(0, 0, i);
                        if (i == 45)
                        {
                            i += 45;
                        }
                        if (i == 225)
                        {
                            i += 45;
                        }
                    }                   
                    Destroy(gameObject, DestroySecond);
                    //if (myChar.Chapter >= 2)
                    //{
                    //    for (int i = 0; i < 360; i += 45)
                    //    {
                    //        GameObject temp = Instantiate(TrapObject, DeathEffectPos.transform.position, Quaternion.identity);
                    //        temp.GetComponent<MonsterProjectile>().BoltSpeed = 3f;
                    //        temp.transform.parent = MonsterProjectile.transform;

                    //        Destroy(temp, 2f);

                    //        //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
                    //        temp.transform.rotation = Quaternion.Euler(0, 0, i);
                    //    }
                    //}
                }
            }
        }
        else if (transform.parent.name != "Monster" || transform.parent)
        {
            if (SpriteFalseCheck)
            {
                gameObject.GetComponent<SpriteRenderer>().gameObject.SetActive(false);
                Destroy(transform.parent.gameObject, DestroySecond);

            }
            else if (!SpriteFalseCheck)
            {
                gameObject.layer = LayerMask.NameToLayer("DeathMonster");
                gameObject.tag = "DeathMonster";
                if (ObjectDestroyCheck)
                {
                    Destroy(transform.parent.gameObject, DestroySecond);
                }
            }

            if (DeathTrap)
            {
                if (traptypes == TrapType.InstallationTypeTrap)     //날아다니는 몹들 죽었을때 설치형트랩
                {
                    //if (myChar.Chapter >= 2)
                    //{
                    //    gameObject.layer = LayerMask.NameToLayer("DeathMonster");
                    //    gameObject.tag = "DeathMonster";
                    //    GameObject Trap = Instantiate(TrapObject, transform.position, Quaternion.identity);
                    //    Trap.transform.parent = MonsterProjectile.transform;
                    //    Trap.GetComponent<TrapDamage>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(transform.GetComponent<FollowMonster>().MonsterIndex).Damage;
                    //    if (transform.GetComponent<FollowMonster>().MonsterIndex == 20)
                    //    {
                    //        Trap.transform.localScale = new Vector2(0.7f, 0.7f);
                    //        Destroy(Trap, GameManager.Instance.UnitDataMgr.GetTemplate(transform.GetComponent<FollowMonster>().MonsterIndex).P3);
                    //    }                        
                    //    else
                    //    {
                    //        Destroy(Trap, DestroySecond);
                    //    }
                    //}
                }
            }

        }
        
    }
    private void PlayerSkillTrap()
    {
        if (GameManager.Instance.PlayerSkill[10] >= 1)
        {
            for (int i = 0; i < 360; i += 45)
            {
                GameObject temp;
                if (traptypes == TrapType.CircleProjectileTrap)
                {
                    temp = Instantiate(SkillObject, DeathEffectPos.transform.position, Quaternion.identity);
                }
                else
                {
                    temp = Instantiate(SkillObject, transform.position, Quaternion.identity);
                }                
                temp.GetComponent<PlayerSkillProjectile>().BoltSpeed = 3.5f;

                Destroy(temp, 2f);

                //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
                temp.transform.rotation = Quaternion.Euler(0, 0, i);
            }
        }
        else if(GameManager.Instance.PlayerSkill[10] == 0)
        {
            return;
        }

    }
    private void EarthGuardianDeathEffect()
    {
        GameObject FoundationEffect = Instantiate(RockBossDeathEffect, transform.position, Quaternion.identity);
        Destroy(FoundationEffect, 2.5f);
    }
    private void MonsterLayerChange()
    {
        gameObject.layer = LayerMask.NameToLayer("DeathMonster");
        gameObject.tag = "DeathMonster";
    }

    private void DeathEffectSolo()
    {
        GameObject Effect = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        //if (myChar.Chapter < 1)
        //{
        //    GameObject Effect = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        //}        
    }
}
