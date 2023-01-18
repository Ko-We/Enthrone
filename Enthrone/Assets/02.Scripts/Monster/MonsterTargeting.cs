using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTargeting : MonoBehaviour
{
    public int MonsterIndex;
    MyObject myChar;
    GameManager instance;

    public LineRenderer _lineRender;
    private GameObject MonsterProjectile;

    public GameObject MonsterBolt;
    public Transform AttackPoint;

    public Transform target;

    public int MultiShot_Cnt;
    public int Continous_Cnt;
    public int ContinuousCheck;
    public bool CrossShotCheck;
    public float BoltSpeed;

    private bool DumboCheck;
    public bool RadiationType;          //false 타입 +모양 방사형 , true 타입 x모양 방사형

    public GameObject PlayerList;

    [Header("ShotType")]
    public bool ForwardShot;
    public bool TargetShot;
    public bool startattack;
    public bool BossMonster;
    public bool LayerPoint;
    public bool MultiShot;              //방사형 멀티샷
    public bool ContinuousShot;         //연발 공격
    public bool Radiation;              //방사형 공격

    public int Radian;


    // Start is called before the first frame update
    void Start()
    {
        MonsterProjectile = GameObject.Find("MonsterProjectileManager");
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
        _lineRender = GetComponent<LineRenderer>();
        if (transform.gameObject.GetComponent<MonsterScript>())
        {
            MonsterIndex = transform.gameObject.GetComponent<MonsterScript>().MonsterIndex;
        }
        else if (transform.gameObject.GetComponent<BossPatternScript>())
        {
            MonsterIndex = transform.gameObject.GetComponent<BossPatternScript>().MonsterIndex;
        }

    }

    // Update is called once per frame
    void Update()
    {
        TargetAutoCheck();
        if (!BossMonster)
        {
            //PageCheck();
        }
        if (LayerPoint)
        {
            Vector3 distance = PlayerList.transform.position;

            Vector3 dir = distance - AttackPoint.position;
            _lineRender.SetPosition(0, AttackPoint.position);
            _lineRender.SetPosition(1, distance);
        }

        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    //for (int i = -90; i <= 90; i += Radian)
        //    //{
        //    //    GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
        //    //    projectile.GetComponent<MonsterProjectile>().BoltSpeed = 3.5f;
        //    //    projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
        //    //    projectile.transform.parent = MonsterProjectile.transform;

        //    //    //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
        //    //    projectile.transform.rotation = Quaternion.Euler(0, 0, i);
        //    //}
        //    if (!RadiationType)
        //    {
        //        for (int i = -90; i <= 90; i += 45)
        //        {
        //            GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
        //            projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed                   ;
        //            projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
        //            projectile.transform.parent = MonsterProjectile.transform;

        //            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
        //            projectile.transform.rotation = Quaternion.Euler(0, 0, i);
        //        }
        //        RadiationType = true;
        //    }
        //    else
        //    {
        //        for (float i = -67.5f; i <= 68f; i += 45)
        //        {
        //            GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
        //            projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
        //            projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
        //            projectile.transform.parent = MonsterProjectile.transform;

        //            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
        //            projectile.transform.rotation = Quaternion.Euler(0, 0, i);
        //        }
        //        RadiationType = false;
        //    }
        //}
    }

    public void TargetAutoCheck()
    {
        GameObject Player = GameObject.Find("Player");

        for (int i = 0; i < myChar.HeroNum; i++)
        {
            if (Player.transform.GetChild(i).gameObject.activeSelf == true)
            {
                if (Player.transform.GetChild(i).name != "SeasnalShield")
                {
                    target = Player.transform.GetChild(i);
                    PlayerList = target.gameObject;
                }
            }
        }
    }
    //YetiKing Icicle 전용 AngleAxis가 90도면 생성될때 가로로나와서 모서리에서 발사할경우 콜라이더에 닿아서 오브젝트가 파괴됨
    public void YetiKing_IcicleAttack()
    {
        GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
        //미사일 생성을 ProjectileManager자식으로 생성하기위함
        projectile.transform.parent = MonsterProjectile.transform;
        projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
        projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
        if (!LayerPoint && ForwardShot)
        {
            if (transform.localScale.x > 0)
            {
                projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(-180, Vector3.forward);
            }
            else if (transform.localScale.x < 0)
            {
                projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(180, Vector3.forward);
            }

        }
    }
    public void Attack()
    {
        GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
        //미사일 생성을 ProjectileManager자식으로 생성하기위함
        projectile.transform.parent = MonsterProjectile.transform;
        projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
        projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
        if (!LayerPoint && ForwardShot)
        {
            if (transform.localScale.x > 0)
            {
                projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            }
            else if (transform.localScale.x < 0)
            {
                projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(90, Vector3.forward);
            }

        }
        else if (TargetShot || LayerPoint)
        {
            if (PlayerList != null)
            {
                Vector3 distance = PlayerList.transform.position;
                Vector3 dir = distance - AttackPoint.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //projectile.transform.up = dir.normalized;
                projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90), Vector3.forward);

                //if (MonsterIndex == 8)
                //{
                //    if (myChar.Chapter >= 2)
                //    {
                //        projectile.transform.localScale = new Vector3(4, 4, 1);
                //    }
                //}
                //BossPattrn 스크립트를 보유중인 오브젝트의 마지막 미사일일때 3발쏘게됨
                if (gameObject.GetComponent<BossPatternScript>() && GetComponent<BossPatternScript>().attackCountingCheck <= 1)
                {
                    GameObject projectileR = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
                    projectileR.transform.parent = MonsterProjectile.transform;
                    projectileR.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + 45, Vector3.forward);
                    projectileR.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;

                    GameObject projectileL = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
                    projectileL.transform.parent = MonsterProjectile.transform;
                    projectileL.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - 45, Vector3.forward);
                    projectileL.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                }
                if (MultiShot)
                {
                    for (int i = 0; i < MultiShot_Cnt; i++)
                    {
                        GameObject projectileR = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
                        projectileR.transform.parent = MonsterProjectile.transform;
                        projectileR.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + 15 + (i * 15), Vector3.forward);
                        projectileR.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                        projectileR.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;

                        GameObject projectileL = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
                        projectileL.transform.parent = MonsterProjectile.transform;
                        projectileL.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - 15 - (i * 15), Vector3.forward);
                        projectileL.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                        projectileL.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;

                    }
                    if (MonsterIndex == 45)
                    {
                        SoundManager.Instance.PlaySfx(37);
                    }

                }
                if (ContinuousShot)
                {
                    for (int i = 0; i < Continous_Cnt; i++)
                    {
                        Invoke("TargetAttack", (i * 0.3f) + 0.3f);
                    }
                    if (MonsterIndex == 45)
                    {
                        SoundManager.Instance.PlaySfx(37);
                    }

                }
                //챔피언 라바 골렘 공격마다 멀티샷 늘려주고 초기화해주기위함
                if (MonsterIndex == 51)
                {
                    SoundManager.Instance.PlaySfx(54);
                    if (MultiShot_Cnt < 2)
                    {
                        MultiShot_Cnt++;
                    }
                    else
                    {
                        MultiShot_Cnt = 0;
                    }
                }
                //if (myChar.Chapter >= 2)        //유도미사일
                //{
                //    Vector3 dir = GameManager.Instance.SelectCharacter.transform.position - AttackPoint.position;
                //    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //    //projectile.transform.up = dir.normalized;
                //    projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90), Vector3.forward);
                //    projectile.GetComponent<MonsterProjectile>().GuideShot = true;
                //}
            }
        }
        else if (ContinuousShot)
        {
            projectile.transform.parent = MonsterProjectile.transform;
            projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
            Vector3 distance = PlayerList.transform.position;
            Vector3 dir = distance - AttackPoint.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90), Vector3.forward);
            projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;

            for (int i = 0; i < Continous_Cnt; i++)
            {
                Invoke("TargetAttack", (i * 0.3f) + 0.3f);
            }
        }
    }
    //일반공격과 바운스 몬스터 공격패턴이달라 추가해서 제작함
    public void BounceAttack()
    {
        GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
        //미사일 생성을 ProjectileManager자식으로 생성하기위함
        projectile.transform.parent = MonsterProjectile.transform;
        projectile.GetComponent<MonsterBounceProjectile2>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
        projectile.GetComponent<MonsterBounceProjectile2>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
        //projectile.GetComponent<MonsterBounceProjectile2>().BounceCheck = true;

        if (!LayerPoint && ForwardShot)
        {
            if (transform.localScale.x > 0)
            {
                projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            }
            else if (transform.localScale.x < 0)
            {
                projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(90, Vector3.forward);
            }

        }
        else if (TargetShot/* || LayerPoint*/)
        {
            if (PlayerList != null)
            {
                Vector3 distance = PlayerList.transform.position;
                if (MonsterIndex != 18)
                {
                    Vector3 dir = distance - AttackPoint.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    //projectile.transform.up = dir.normalized;
                    projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90), Vector3.forward);

                    //if (MonsterIndex == 8)
                    //{
                    //    if (myChar.Chapter >= 2)
                    //    {
                    //        projectile.transform.localScale = new Vector3(4, 4, 1);
                    //    }
                    //}
                    if (MultiShot)
                    {
                        for (int i = 0; i < MultiShot_Cnt; i++)
                        {
                            GameObject projectileR = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
                            projectileR.transform.parent = MonsterProjectile.transform;
                            projectileR.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + 15 + (i * 15), Vector3.forward);
                            projectileR.GetComponent<MonsterBounceProjectile2>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                            projectileR.GetComponent<MonsterBounceProjectile2>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                            //projectileR.GetComponent<MonsterBounceProjectile2>().BounceCheck = true;

                            GameObject projectileL = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
                            projectileL.transform.parent = MonsterProjectile.transform;
                            projectileL.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - 15 - (i * 15), Vector3.forward);
                            projectileL.GetComponent<MonsterBounceProjectile2>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                            projectileL.GetComponent<MonsterBounceProjectile2>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                            //projectileL.GetComponent<MonsterBounceProjectile2>().BounceCheck = true;
                        }
                    }
                }
            }
        }
    }

    public void TargetAttack()
    {
        GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
        //미사일 생성을 ProjectileManager자식으로 생성하기위함
        projectile.transform.parent = MonsterProjectile.transform;
        projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
        projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;

        if (PlayerList != null)
        {
            Vector3 distance = PlayerList.transform.position;
            Vector3 dir = distance - AttackPoint.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //projectile.transform.up = dir.normalized;
            projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90), Vector3.forward);
        }        
        ContinuousCheck++;

        if (MonsterIndex == 36)
        {
            SoundManager.Instance.PlaySfx(82);
            if (ContinuousCheck >= 2)
            {
                Invoke("Invisible", 1f);
            }
        }

        if (MonsterIndex == 40)
        {
            SoundManager.Instance.PlaySfx(84);
        }
        
        if (MonsterIndex == 45)
        {
            SoundManager.Instance.PlaySfx(37);
        }
    }
    //덤보문어 공격패턴
    public void DumboAttack()
    {
        GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
        //미사일 생성을 ProjectileManager자식으로 생성하기위함
        projectile.transform.parent = MonsterProjectile.transform;
        projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
        projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;

        if (!DumboCheck)
        {
            projectile.transform.parent = MonsterProjectile.transform;
            projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
            Vector3 distance = PlayerList.transform.position;
            Vector3 dir = distance - AttackPoint.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90), Vector3.forward);
            projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;

            for (int i = 0; i < Continous_Cnt; i++)
            {
                Invoke("TargetAttack", (i * 0.3f) + 0.3f);
            }
            DumboCheck = true;

            Invoke("AttackDealay", 2.5f);
        }
        else
        {
            Vector3 distance = PlayerList.transform.position;
            Vector3 dir = distance - AttackPoint.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //projectile.transform.up = dir.normalized;
            projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90), Vector3.forward);
            for (int i = 0; i < MultiShot_Cnt; i++)
            {
                GameObject projectileR = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
                projectileR.transform.parent = MonsterProjectile.transform;
                projectileR.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + 15 + (i * 15), Vector3.forward);
                projectileR.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                projectileR.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;

                GameObject projectileL = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
                projectileL.transform.parent = MonsterProjectile.transform;
                projectileL.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - 15 - (i * 15), Vector3.forward);
                projectileL.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                projectileL.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
            }
            DumboCheck = false;

            Invoke("Invisible", 1f);
        }
    }
    public void RadiationAttack()
    {
        if (!RadiationType)
        {
            for (int i = -90; i <= 90; i += 45)
            {
                GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
                if (projectile.GetComponent<MonsterProjectile>())
                {
                    projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                    projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                    projectile.transform.parent = MonsterProjectile.transform;
                }
                else if (projectile.GetComponent<MonsterBounceProjectile2>())
                {
                    projectile.GetComponent<MonsterBounceProjectile2>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                    projectile.GetComponent<MonsterBounceProjectile2>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                    projectile.transform.parent = MonsterProjectile.transform;
                }
                

                //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
                projectile.transform.rotation = Quaternion.Euler(0, 0, i);
            }
            RadiationType = true;
        }
        else
        {
            for (float i = -67.5f; i <= 68f; i += 45)
            {
                GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
                if (projectile.GetComponent<MonsterProjectile>())
                {
                    projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                    projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                    projectile.transform.parent = MonsterProjectile.transform;
                }
                else if (projectile.GetComponent<MonsterBounceProjectile2>())
                {
                    projectile.GetComponent<MonsterBounceProjectile2>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                    projectile.GetComponent<MonsterBounceProjectile2>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                    projectile.transform.parent = MonsterProjectile.transform;
                }

                //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
                projectile.transform.rotation = Quaternion.Euler(0, 0, i);
            }
            RadiationType = false;
        }
    }
    public void Natural_GreenEyesAttack()
    {
        int Pattern = Random.Range(0, 23);
        switch (Pattern)
        {
            case 0:
                NaturalBossAttackPattern(0, 225, 45);
                break;
            case 1:
                NaturalBossAttackPattern(15, 240, 45);
                break;
            case 2:
                NaturalBossAttackPattern(30, 255, 45);
                break;
            case 3:
                NaturalBossAttackPattern(45, 270, 45);
                break;
            case 4:
                NaturalBossAttackPattern(60, 285, 45);
                break;
            case 5:
                NaturalBossAttackPattern(75, 300, 45);
                break;
            case 6:
                NaturalBossAttackPattern(90, 315, 45);
                break;
            case 7:
                NaturalBossAttackPattern(135, 360, 45);
                break;
            case 8:
                NaturalBossAttackPattern(150, 375, 45);
                break;
            case 9:
                NaturalBossAttackPattern(165, 390, 45);
                break;
            case 10:
                NaturalBossAttackPattern(180, 405, 45);
                break;
            case 11:
                NaturalBossAttackPattern(195, 420, 45);
                break;
            case 12:
                NaturalBossAttackPattern(210, 435, 45);
                break;
            case 13:
                NaturalBossAttackPattern(225, 450, 45);
                break;
            case 14:
                NaturalBossAttackPattern(240, 465, 45);
                break;
            case 15:
                NaturalBossAttackPattern(255, 480, 45);
                break;
            case 16:
                NaturalBossAttackPattern(270, 495, 45);
                break;
            case 17:
                NaturalBossAttackPattern(285, 510, 45);
                break;
            case 18:
                NaturalBossAttackPattern(300, 525, 45);
                break;
            case 19:
                NaturalBossAttackPattern(315, 540, 45);
                break;
            case 20:
                NaturalBossAttackPattern(330, 555, 45);
                break;
            case 21:
                NaturalBossAttackPattern(345, 570, 45);
                break;
            case 22:
                NaturalBossAttackPattern(360, 585, 45);
                break;
            

            //case 0:
            //    NaturalBossAttackPattern(0, 225, 45);
            //    break;
            //case 1:
            //    NaturalBossAttackPattern(45, 270, 45);
            //    break;
            //case 2:
            //    NaturalBossAttackPattern(90, 315, 45);
            //    break;
            //case 3:
            //    NaturalBossAttackPattern(135, 360, 45);
            //    break;
            //case 4:
            //    NaturalBossAttackPattern(180, 405, 45);
            //    break;
            //case 5:
            //    NaturalBossAttackPattern(225, 450, 45);
            //    break;
            //case 6:
            //    NaturalBossAttackPattern(270, 495, 45);
            //    break;
            //case 7:
            //    NaturalBossAttackPattern(315, 540, 45);
            //    break;
            //case 8:
            //    NaturalBossAttackPattern(360, 585, 45);
            //    break;
        }
        //int Cnt = 3;
        //for (int i = 0; i < 45; i += 45)
        //{
        //    GameObject projectile = Instantiate(MonsterBolt, transform.position, Quaternion.identity);

        //    projectile.GetComponent<MonsterProjectile>().BoltSpeed = BoltSpeed;
        //    projectile.transform.parent = MonsterProjectile.transform;

        //    Destroy(projectile, 2f);

        //    //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
        //    projectile.transform.rotation = Quaternion.Euler(0, 0, i);
        //    //int num = Random.Range(0, 2);
        //    //if (Cnt != 0)
        //    //{
        //    //    if (num == 1)
        //    //    {
        //    //        GameObject projectile = Instantiate(MonsterBolt, transform.position, Quaternion.identity);

        //    //        projectile.GetComponent<MonsterProjectile>().BoltSpeed = BoltSpeed;
        //    //        projectile.transform.parent = MonsterProjectile.transform;

        //    //        Destroy(projectile, 2f);

        //    //        //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
        //    //        projectile.transform.rotation = Quaternion.Euler(0, 0, i);
        //    //    }
        //    //    else if (num == 0)
        //    //    {
        //    //        Cnt--;
        //    //    }
        //    //}
        //    //else if (Cnt == 0)
        //    //{
        //    //    GameObject projectile = Instantiate(MonsterBolt, transform.position, Quaternion.identity);

        //    //    projectile.GetComponent<MonsterProjectile>().BoltSpeed = BoltSpeed;
        //    //    projectile.transform.parent = MonsterProjectile.transform;

        //    //    Destroy(projectile, 2f);

        //    //    //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
        //    //    projectile.transform.rotation = Quaternion.Euler(0, 0, i);
        //    //}

        //}
    }
    
    public void YetiKingAttack()
    {
        GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
        //미사일 생성을 ProjectileManager자식으로 생성하기위함
        projectile.transform.parent = MonsterProjectile.transform;
        projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
        projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;

        if (MonsterIndex == 9)
        {
            projectile.GetComponent<MonsterProjectile>().WallParent = transform.parent.parent.parent.Find("BackGround").gameObject;
        }
        if (!LayerPoint && ForwardShot)
        {
            if (transform.localScale.x > 0)
            {
                projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            }
            else if (transform.localScale.x < 0)
            {
                projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(90, Vector3.forward);
            }

        }
        else if (TargetShot || LayerPoint)
        {
            if (PlayerList != null)
            {
                Vector3 distance = PlayerList.transform.position;

                Vector3 dir = distance - AttackPoint.position;

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //projectile.transform.up = dir.normalized;
                projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90), Vector3.forward);
            }
        }
    }
    //에이션트슬라임 점프착지 미사일발사부분(그외 반원공격)
    public void RadiationShot()
    {
        for (int i = -90; i <= 90; i += 45)
        {
            GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
            projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
            projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
            projectile.transform.parent = MonsterProjectile.transform;

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            projectile.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }
    public void CircleShot()
    {
        for (int i = 0; i < 360; i += 45)
        {
            GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
            if (projectile.GetComponent<MonsterProjectile>())
            {
                projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
            }
            else if (projectile.GetComponent<MonsterBounceProjectile2>())
            {
                projectile.GetComponent<MonsterBounceProjectile2>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                projectile.GetComponent<MonsterBounceProjectile2>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
            }
            
            
            projectile.transform.parent = MonsterProjectile.transform;

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            projectile.transform.rotation = Quaternion.Euler(0, 0, i);
        }
        if (MonsterIndex == 49)
        {
            Invoke("Invisible", instance.UnitDataMgr.GetTemplate(MonsterIndex).P2);
        }
    }

    public void halfCircle(int a, int b)
    {
        for (int i = a; i <= b; i += 45)
        {
            GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
            if (projectile.GetComponent<MonsterProjectile>())
            {
                projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
            }
            else if (projectile.GetComponent<MonsterBounceProjectile2>())
            {
                projectile.GetComponent<MonsterBounceProjectile2>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                projectile.GetComponent<MonsterBounceProjectile2>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
            }


            projectile.transform.parent = MonsterProjectile.transform;

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            projectile.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }

    public void halfCircleShot()
    {
        if (Mathf.Round(transform.eulerAngles.z) == 0)
        {
            halfCircle(-90, 90);
        }
        else if (Mathf.Round(transform.eulerAngles.z) == 90)
        {
            halfCircle(0, 180);
        }
        else if (Mathf.Round(transform.eulerAngles.z) == 180)
        {
            halfCircle(90, 270);
        }
        else if (Mathf.Round(transform.eulerAngles.z) == 270)
        {
            halfCircle(-180, 0);
        }
    }
    public void CrossShot()
    {
        if (!CrossShotCheck)
        {
            for (int i = 0; i < 360; i += 90)
            {
                GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
                if (projectile.GetComponent<MonsterProjectile>())
                {
                    projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                    projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                    projectile.transform.parent = MonsterProjectile.transform;
                }
                if (projectile.GetComponent<MonsterBounceProjectile2>())
                {
                    projectile.GetComponent<MonsterBounceProjectile2>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                    projectile.GetComponent<MonsterBounceProjectile2>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                    projectile.transform.parent = MonsterProjectile.transform;
                }
                

                //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
                projectile.transform.rotation = Quaternion.Euler(0, 0, i);
            }
            CrossShotCheck = true;
        }
        else
        {
            for (int i = 45; i < 405; i += 90)
            {
                GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
                if (projectile.GetComponent<MonsterProjectile>())
                {
                    projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                    projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                    projectile.transform.parent = MonsterProjectile.transform;
                }
                if (projectile.GetComponent<MonsterBounceProjectile2>())
                {
                    projectile.GetComponent<MonsterBounceProjectile2>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
                    projectile.GetComponent<MonsterBounceProjectile2>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                    projectile.transform.parent = MonsterProjectile.transform;
                }

                //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
                projectile.transform.rotation = Quaternion.Euler(0, 0, i);
            }
            CrossShotCheck = false;
        }
    }

    private void RockBossAttack()
    {
        GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
        projectile.transform.parent = MonsterProjectile.transform;
        projectile.GetComponent<MonsterBounceProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
        //Vector3 distance = PlayerList.transform.position;

        //Vector3 dir = distance - AttackPoint.position;

        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90), Vector3.forward);
    }

    private void NaturalBossAttackPattern(int startCnt, int LastCnt, int AddCnt)
    {
        int RandomNum = Random.Range(0, 2);
        int ShootCnt = 0;

        for (int i = startCnt; i <= LastCnt; i += AddCnt)
        {           
            GameObject projectile = Instantiate(MonsterBolt, transform.position, Quaternion.identity);
            projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
            projectile.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
            projectile.transform.parent = MonsterProjectile.transform;

            Destroy(projectile, 2f);

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            projectile.transform.rotation = Quaternion.Euler(0, 0, i);
            ShootCnt++;

            if (RandomNum == 0)
            {
                if (ShootCnt == 2)
                {
                    i += AddCnt;
                }
            }
            else if (RandomNum == 1)
            {
                if (ShootCnt == 3)
                {
                    i += AddCnt;
                }
            }
        }
    }
    private void Invisible()
    {
        transform.GetComponent<FollowMonster>()._anim.SetBool("Invisible", true);
    }
    private void PageCheck()
    {
        MonsterScript Script = transform.GetComponent<MonsterScript>();

        if (Script.attributeMonsterType == MonsterScript.AttributeType.Flame)
        {
            TargetShot = true;
            //if (myChar.Chapter < 2)
            //{
            //    ForwardShot = true;
            //    TargetShot = false;
            //}
            //else
            //{
            //    TargetShot = true;
            //    ForwardShot = false;
            //}
        }
    }
    public void FadeShot()
    {
        for (int i = 0; i < 360; i += 45)
        {
            GameObject temp = Instantiate(MonsterBolt, AttackPoint.transform.position, Quaternion.identity);
            temp.GetComponent<MonsterProjectile>().BoltSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).BoltSpeed;
            temp.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
            temp.transform.parent = MonsterProjectile.transform;

            Destroy(temp, 2f);

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }

    private void AttackDealay()
    {
        transform.GetComponent<FollowMonster>()._anim.SetBool("Attack", true);
    }


}
