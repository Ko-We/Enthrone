using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public static PlayerTargeting Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerTargeting>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerTargeting");
                    instance = instanceContainer.AddComponent<PlayerTargeting>();
                }
            }
            return instance;
        }
    }

    MyObject myChar;
    private static PlayerTargeting instance;
    
    private GameObject PlayerObjectManager;

    //protected CharacterStat characterState;

    public bool getATarget = true;
    public bool isTarget;
    public Collider2D[] isTarget2;
    //public bool isBoxTarget;
    public bool MonsterCheck;

    public List<float> distanceCheckList = new List<float>();
    [SerializeField]
    float currentDist = 0;      //현재 거리
    [SerializeField]
    float closetDist = 100f;    //가까운 거리
    [SerializeField]
    float TargetDist = 100f;   //타겟 거리
    [SerializeField]
    int closeDistIndex = 0;    //가장 가까운 인덱스
    public int TargetIndex = -1;      //타겟팅 할 인덱스
    int prevTargetIndex = 0;
    //public int BoxTargetIndex = -1;      //타겟팅 할 인덱스
    //int BoxprevTargetIndex = 0;
    public float OrbSpeed = 500f; //발사체 속도
    RaycastHit2D hit;
    public LayerMask _layerMask;
    public LayerMask AttackCheck_layerMask;
    //public LayerMask _boxLayerMask;

    public float attackCheckRadius;

    [SerializeField]
    GameObject nearestEnemy = null;
    Vector3 _preDistance;

    public float fireCountdown = 0f;
    public float fireRate = 2;
    public float ASPDActive = 1;

    public float atkSpd = 0f;

    public List<GameObject> MonsterList = new List<GameObject>();
    //public List<GameObject> BoxList = new List<GameObject>();

    public int currentOrb;
    public List<GameObject> PlayerBolt = new List<GameObject>();
    public List<GameObject> BoltHitEffect = new List<GameObject>();
    public List<GameObject> SeasnalHitEffect = new List<GameObject>();
    public Transform AttackPoint;
    //Monster를 담는 List 
    public float OrbDamage = 1f;

    private Vector3 CharacterProjcetileScale;
    private bool SummonMonsterCheck;
    private int SummonCnt;
    public float monsterCheckTime = 1.5f;




    private void Awake()
    {
        myChar = MyObject.MyChar;
        PlayerObjectManager = GameObject.Find("PlayerObjectManager");
        //characterState = GetComponent<CharacterStat>();
    }

    private void Start()
    {
        
    }
    void Update()
    {
        ProjcetileScale();
        currentOrb = myChar.EquipmentWeapon[myChar.SelectHero];
        CheckSurroundings();
        //if (myChar.SelectHero != 3)
        //{
        //    currentOrb = myChar.EquipmentWeapon[myChar.SelectHero];
        //}
        //else
        //{
        //    currentOrb = 0;
        //}


        if (GameObject.Find("Manager").transform.Find("RoomManager").gameObject.activeSelf == true)
        {
            if (!RoomManager.Instance.EquipmentRoom)
            {
                CheckSummonMonster();
            }
        }

        if (myChar.Range > 45)
        {
            attackCheckRadius = (4 + 30 + (15 / 5) + ((myChar.Range - 45) / 10)) * 0.25f;
        }
        else if (myChar.Range > 30)
        {
            attackCheckRadius = (4 + 30 + ((myChar.Range - 30) / 5)) * 0.25f;
        }
        else
        {
            attackCheckRadius = (4 + myChar.Range) * 0.25f;
        }
        if (myChar.InGameStart)
        {
            if (myChar.Range > 45)
            {
                attackCheckRadius = (4 + 30 + (15 / 5) + myChar.Range - 45 / 10) * 0.25f;
            }
            else if(myChar.Range > 30)
            {
                attackCheckRadius = (4 + 30 + ((myChar.Range - 30) / 5)) * 0.25f;
            }
            else
            {
                attackCheckRadius = (4 + myChar.Range) * 0.25f;
            }
            
            atkSpd = 20 / myChar.ASPD;
        }
        else if (!myChar.InGameStart)
        {
            //attackCheckRadius = 2.5f;
        }
        
        if (myChar.currentHp > 0)
        {
            if (MonsterCheck)
            {
                TargetCheck();
            }
            //if (isTarget)
            //{
            //    TargetCheck();
            //}

            MonsterList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        }        

        if (MonsterCheck)
        {
            if (GameManager.Instance.RoomManagerObj.activeSelf)
            {
                RoomManager.Instance.StageInfo_Sign.SetActive(false);
            }
            
            //RoomManager.Instance.NextDoorClose();
        }
        else if (!MonsterCheck)
        {
            if ((myChar.Stage % 10) != 9)
            {
                if (GameManager.Instance.RoomManagerObj.activeSelf)
                {
                    if (myChar.Chapter + 1 % 10 != 3 && myChar.Chapter + 1 % 10 != 6)
                    {
                        if (myChar.Stage < 50)
                        {
                            RoomManager.Instance.StageInfo_Sign.SetActive(true);
                        }
                        else
                        {
                            RoomManager.Instance.StageInfo_Sign.SetActive(false);
                        }
                    }
                    else
                    {
                        if (myChar.Stage < 30)
                        {
                            RoomManager.Instance.StageInfo_Sign.SetActive(true);
                        }
                        else
                        {
                            RoomManager.Instance.StageInfo_Sign.SetActive(false);
                        }
                    }
                        
                }
            }
            
            //RoomManager.Instance.NextDoorOpen();
            getATarget = false;
        }

        if (MonsterList.Count <= 0)
        {
            if (myChar.Chapter + 1 % 10 != 3 && myChar.Chapter + 1 % 10 != 6)
            {
                if (GameObject.Find("Manager").transform.Find("RoomManager").gameObject.activeSelf == true)
                {
                    if (!SummonMonsterCheck)
                    {
                        if (monsterCheckTime <= 0)
                        {
                            RoomManager.Instance.DoorOpen();
                        }
                    }
                    if (myChar.Stage % 10 == 0)
                    {
                        RoomManager.Instance.BossReward();
                    }
                }
            }
            else
            {
                if (myChar.Stage <= 29)
                {
                    if (GameObject.Find("Manager").transform.Find("RoomManager").gameObject.activeSelf == true)
                    {
                        if (!SummonMonsterCheck)
                        {
                            if (monsterCheckTime <= 0)
                            {
                                RoomManager.Instance.DoorOpen();
                            }
                        }
                        if (myChar.Stage % 10 == 0)
                        {
                            RoomManager.Instance.BossReward();
                        }
                    }
                }
            }
        }
        //if (myChar.Chapter != 0)        //숲보스제외하고 그외보스들
        //{
        //    if (MonsterList.Count <= 0)/* if (!MonsterCheck)*/
        //    {
        //        if (myChar.Chapter + 1 % 10 != 3 && myChar.Chapter + 1 % 10 != 6)
        //        {
        //            if (GameObject.Find("Manager").transform.Find("RoomManager").gameObject.activeSelf == true)
        //            {
        //                if (!SummonMonsterCheck)
        //                {
        //                    if (monsterCheckTime <= 0)
        //                    {
        //                        RoomManager.Instance.DoorOpen();
        //                    }
        //                }
        //                if (myChar.Stage % 10 == 0)
        //                {
        //                    RoomManager.Instance.BossReward();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (myChar.Stage <= 29)
        //            {
        //                if (GameObject.Find("Manager").transform.Find("RoomManager").gameObject.activeSelf == true)
        //                {
        //                    if (!SummonMonsterCheck)
        //                    {
        //                        if (monsterCheckTime <= 0)
        //                        {
        //                            RoomManager.Instance.DoorOpen();
        //                        }
        //                    }
        //                    if (myChar.Stage % 10 == 0)
        //                    {
        //                        RoomManager.Instance.BossReward();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //else if (myChar.Chapter == 0)       //숲보스 몬스터 체크를위해
        //{
        //    if (myChar.Stage < 50)
        //    {
        //        if (!MonsterCheck) //if (MonsterList.Count <= 0)
        //        {
        //            if (GameObject.Find("Manager").transform.Find("RoomManager").gameObject.activeSelf == true)
        //            {
        //                if (!SummonMonsterCheck)
        //                {
        //                    if (monsterCheckTime <= 0)
        //                    {
        //                        RoomManager.Instance.DoorOpen();
        //                    }
        //                }
        //                if (myChar.Stage % 10 == 0)
        //                {
        //                    if (!myChar.Tutorial)
        //                    {
        //                        RoomManager.Instance.BossReward();
        //                    }
                            
        //                }
        //            }
        //        }
        //    }
        //    else if (myChar.Stage == 50)
        //    {
        //        if (myChar.ForestBossTotalMonsterCheck <= 0)
        //        {
        //            if (GameObject.Find("Manager").transform.Find("RoomManager").gameObject.activeSelf == true)
        //            {
        //                if (monsterCheckTime <= 0)
        //                {
        //                    RoomManager.Instance.DoorOpen();
        //                }

        //                RoomManager.Instance.BossReward();
        //                if (myChar.SelectedStage == 1)
        //                {
        //                    myChar.NatureBossAllDeath = true;
        //                }
        //            }
        //        }
        //        if (myChar.ForestBossTotalMonsterCheck <= 1)
        //        {
        //            if (MonsterList.Count == 0)
        //            {
        //                if (myChar.CurrentBossHp > 0)
        //                {
        //                    myChar.CurrentBossHp = 0;
        //                }
        //                if (myChar.ForestBossTotalMonsterCheck > 0)
        //                {
        //                    myChar.ForestBossTotalMonsterCheck = 0;
        //                }
        //            }

        //        }
        //    }
        //}
        //if (myChar.SelectedStage != 1)
        //{
        //    if (!MonsterCheck)//if (MonsterList.Count <= 0)
        //    {
        //        if (GameObject.Find("Manager").transform.Find("RoomManager").gameObject.activeSelf == true)
        //        {
        //            if (!SummonMonsterCheck)
        //            {
        //                if (monsterCheckTime <= 0)
        //                {
        //                    RoomManager.Instance.DoorOpen();
        //                }                        
        //            }                    
        //            if (myChar.Stage == (myChar.BasicStage + 3))
        //            {
        //                RoomManager.Instance.BossReward();
        //            }
        //        }
        //    }
        //}
        //else if (myChar.SelectedStage == 1)
        //{
        //    if (myChar.Stage != myChar.BasicStage + 3)
        //    {
        //        if (!MonsterCheck) //if (MonsterList.Count <= 0)
        //        {
        //            if (GameObject.Find("Manager").transform.Find("RoomManager").gameObject.activeSelf == true)
        //            {
        //                if (!SummonMonsterCheck)
        //                {
        //                    if (monsterCheckTime <= 0)
        //                    {
        //                        RoomManager.Instance.DoorOpen();
        //                    }
        //                }

        //                if (myChar.Stage == (myChar.BasicStage + 3))
        //                {
        //                    RoomManager.Instance.BossReward();
        //                    if (myChar.SelectedStage == 1)
        //                    {
        //                        myChar.NatureBossAllDeath = true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else if (myChar.Stage == myChar.BasicStage + 3)
        //    {
        //        if (myChar.ForestBossTotalMonsterCheck == 0)
        //        {
        //            if (GameObject.Find("Manager").transform.Find("RoomManager").gameObject.activeSelf == true)
        //            {
        //                if (monsterCheckTime <= 0)
        //                {
        //                    RoomManager.Instance.DoorOpen();
        //                }

        //                if (myChar.Stage == (myChar.BasicStage + 3))
        //                {
        //                    RoomManager.Instance.BossReward();
        //                    if (myChar.SelectedStage == 1)
        //                    {
        //                        myChar.NatureBossAllDeath = true;
        //                    }
        //                }
        //            }
        //        }
        //        if (myChar.ForestBossTotalMonsterCheck <= 1)
        //        {
        //            if (MonsterList.Count == 0)
        //            {
        //                if (myChar.CurrentBossHp > 0)
        //                {
        //                    myChar.CurrentBossHp = 0;
        //                }
        //                if (myChar.ForestBossTotalMonsterCheck > 0)
        //                {
        //                    myChar.ForestBossTotalMonsterCheck = 0;
        //                }
        //            }

        //        }
        //    }
        //    else if (myChar.Chapter >= 3)
        //    {
        //        if (myChar.ForestBossTotalMonsterCheck == 0)
        //        {
        //            if (GameObject.Find("Manager").transform.Find("RoomManager").gameObject.activeSelf == true)
        //            {
        //                if (monsterCheckTime <= 0)
        //                {
        //                    RoomManager.Instance.DoorOpen();
        //                }

        //                if (myChar.Stage == (myChar.BasicStage + 3))
        //                {
        //                    RoomManager.Instance.BossReward();
        //                    if (myChar.SelectedStage == 1)
        //                    {
        //                        myChar.NatureBossAllDeath = true;
        //                    }
        //                }
        //            }
        //        }
        //    }   
        //}
        if (MonsterCheck)
        {
            monsterCheckTime = 1.5f;
        }
        else
        {
            monsterCheckTime -= Time.deltaTime;
        }
        //if (MonsterList.Count > 0)
        //{
        //    monsterCheckTime = 1.5f;
        //}
        //else
        //{
        //    monsterCheckTime -= Time.deltaTime;
        //}
    }
    private void CheckSurroundings()
    {
        isTarget = Physics2D.OverlapCircle(AttackPoint.position, attackCheckRadius, _layerMask);
        
        //isBoxTarget = Physics2D.OverlapCircle(AttackPoint.position, attackCheckRadius, _boxLayerMask);
        
        MonsterCheck = Physics2D.OverlapBox(transform.parent.position, new Vector2(15.5f, 10f), 0, _layerMask);
        isTarget2 = Physics2D.OverlapCircleAll(transform.position, attackCheckRadius, _layerMask);

    }

    //소환될 몬스터가 있는지 체크하는 코드
    private void CheckSummonMonster()
    {
        if (RoomManager.Instance.CurrentStage.transform.parent.Find("Monster").childCount > 0)
        {
            for (int i = 0; i < RoomManager.Instance.CurrentStage.transform.parent.Find("Monster").childCount; i++)
            {
                if (i == 0)
                {
                    SummonCnt = 0;
                }
                if (RoomManager.Instance.CurrentStage.transform.parent.Find("Monster").GetChild(i).tag == "Summon")
                {
                    SummonCnt++;
                }
                if (i == (RoomManager.Instance.CurrentStage.transform.parent.Find("Monster").childCount - 1))
                {
                    if (SummonCnt > 0)
                    {
                        SummonMonsterCheck = true;
                    }
                    else
                    {
                        SummonMonsterCheck = false;
                    }
                }
            }
        }
    }
    void TargetUp()
    {
        if (isTarget && MonsterCheck)
        {
            if (myChar.SelectStoneNum != 0)
            {
                myChar.ElementStone[myChar.SelectStoneNum] -= 1;
                if (myChar.ElementStone[myChar.SelectStoneNum] <= 0)
                {
                    if (myChar.SelectStoneNum <= 3)
                    {
                        myChar.SelectStoneNum++;
                    }
                    else if (myChar.SelectStoneNum >= 4)
                    {
                        myChar.SelectStoneNum = 0;
                    }

                }
            }

            GameObject projectile = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
            projectile.transform.parent = PlayerObjectManager.transform;
            projectile.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;

            if (GameManager.Instance.PlayerSkill[21] >= 1)
            {
                for (int i = 0; i < GameManager.Instance.PlayerSkill[21]; i++)
                {
                    Invoke("MultiShot", 0.1f * i + 0.1f);
                }
            }

            if (!myChar.Wizard)
            {
                switch (myChar.SelectStoneNum)
                {
                    case 0:
                        projectile.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[0];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 0;
                        break;
                    case 1:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[0];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 1;
                        break;
                    case 2:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[1];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 2;
                        break;
                    case 3:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[2];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 3;
                        break;
                    case 4:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[3];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 4;
                        break;
                }
            }
            else if (myChar.Wizard)
            {
                switch (myChar.SelectStoneNum)
                {
                    case 0:
                        projectile.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[0];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 0;
                        break;
                    case 1:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[0];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 1;
                        break;
                    case 2:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[1];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 2;
                        break;
                    case 3:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[2];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 3;
                        break;
                    case 4:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[3];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 4;
                        break;
                }
            }
            if (isTarget2[TargetIndex] != null)/*(isTarget2[TargetIndex] != null)*/
            {
                //어택수정 후
                Vector3 distance = isTarget2[TargetIndex].transform.Find("HitPoint").position;
                //어택수정 전
                //Vector3 distance = MonsterList[TargetIndex].transform.Find("HitPoint").position;

                if (/*!myChar.Warrior &&*/ !myChar.Wizard)
                {
                    //미사일 날라가는 방향
                    Vector3 dir = distance - AttackPoint.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                    //translate로 변경 중인 코드
                    //projectile.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
                    //projectile.GetComponent<Rigidbody2D>().AddForce((distance - AttackPoint.position).normalized * OrbSpeed);

                    _preDistance = distance;
                    //projectile.GetComponent<Rigidbody2D>().AddForce((distance - AttackPoint.position).normalized * OrbSpeed);
                    projectile.GetComponent<ProjectileScript>().dir = dir;
                    projectile.GetComponent<ProjectileScript>().AttackPoint = AttackPoint.position;
                    projectile.GetComponent<ProjectileScript>().distance = distance;

                    if (GameManager.Instance.PlayerSkill[12] >= 1)
                    {
                        for (float i = 1; i <= GameManager.Instance.PlayerSkill[12]; i++)
                        {
                            if (i % 2 == 1)
                            {
                                GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
                                projectile_i.transform.parent = PlayerObjectManager.transform;
                                projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + (15 * ((i / 2) + 0.5f)), Vector3.forward);
                                projectile_i.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
                                projectile_i.GetComponent<ProjectileScript>().dir = dir;
                                if (myChar.PowerShotCount == 1)
                                {
                                    projectile_i.GetComponent<ProjectileScript>().Damage = myChar.Damage * 2.5f;
                                    //projectile_i.transform.GetChild(0).localScale = CharacterProjcetileScale;
                                    PowerShotCheck(projectile_i);
                                }
                                switch (myChar.SelectStoneNum)
                                {
                                    case 0:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[0];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 0;
                                        break;
                                    case 1:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[0];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 1;
                                        break;
                                    case 2:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[1];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 2;
                                        break;
                                    case 3:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[2];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 3;
                                        break;
                                    case 4:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[3];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 4;
                                        break;
                                }
                            }
                            else
                            {
                                GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
                                projectile_i.transform.parent = PlayerObjectManager.transform;
                                projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - (15 * (i / 2)), Vector3.forward);
                                projectile_i.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
                                projectile_i.GetComponent<ProjectileScript>().dir = dir;
                                if (myChar.PowerShotCount == 1)
                                {
                                    projectile_i.GetComponent<ProjectileScript>().Damage = myChar.Damage * 2.5f;
                                    //projectile_i.transform.GetChild(0).localScale = CharacterProjcetileScale;
                                    PowerShotCheck(projectile_i);
                                }
                                switch (myChar.SelectStoneNum)
                                {
                                    case 0:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[0];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 0;
                                        break;
                                    case 1:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[0];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 1;
                                        break;
                                    case 2:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[1];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 2;
                                        break;
                                    case 3:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[2];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 3;
                                        break;
                                    case 4:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[3];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 4;
                                        break;
                                }
                            }
                        }
                    }
                }
                else if (myChar.Wizard)
                {
                    //Vector3 dir = distance - AttackPoint.position;
                    //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    //projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                    _preDistance = distance;
                    //translate로 변경중이면서 모든캐릭터 유도미사일 적용 코드
                    //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    //projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                    //어택수정 후
                    projectile.GetComponent<ProjectileScript>().targetPos = isTarget2[TargetIndex].transform.Find("HitPoint").transform;
                    //어택수정 전
                    //projectile.GetComponent<ProjectileScript>().targetPos = MonsterList[TargetIndex].transform.Find("HitPoint").transform;

                    //projectile.GetComponent<ProjectileScript>().distance = distance;
                    projectile.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;

                    if (GameManager.Instance.PlayerSkill[12] >= 1)
                    {
                        for (float i = 1; i <= GameManager.Instance.PlayerSkill[12]; i++)
                        {
                            if (i % 2 == 1)
                            {
                                GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
                                //projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + (15 * ((i / 2) + 0.5f)), Vector3.forward);
                                projectile_i.GetComponent<ProjectileScript>().transform.rotation = Quaternion.Euler(0, 0, (30 * ((i / 2) + 0.5f)));
                                projectile_i.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
                                //projectile_i.GetComponent<ProjectileScript>().distance = distance;

                                //어택수정 후
                                projectile_i.GetComponent<ProjectileScript>().targetPos = isTarget2[TargetIndex].transform.Find("HitPoint").transform;
                                //어택수정 전
                                //projectile_i.GetComponent<ProjectileScript>().targetPos = MonsterList[TargetIndex].transform.Find("HitPoint").transform;

                                if (myChar.PowerShotCount == 1)
                                {
                                    projectile_i.GetComponent<ProjectileScript>().Damage = myChar.Damage * 2.5f;
                                    //projectile_i.transform.GetChild(0).localScale = CharacterProjcetileScale;
                                    PowerShotCheck(projectile_i);
                                }
                                switch (myChar.SelectStoneNum)
                                {
                                    case 0:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[0];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 0;
                                        break;
                                    case 1:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[0];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 1;
                                        break;
                                    case 2:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[1];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 2;
                                        break;
                                    case 3:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[2];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 3;
                                        break;
                                    case 4:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[3];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 4;
                                        break;
                                }
                            }
                            else
                            {
                                GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
                                projectile_i.GetComponent<ProjectileScript>().transform.rotation = Quaternion.Euler(0, 0, -(30 * (i / 2)));
                                //projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - (15 * (i / 2)), Vector3.forward);
                                projectile_i.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
                                //projectile_i.GetComponent<ProjectileScript>().distance = distance;
                                //어택수정 후
                                projectile_i.GetComponent<ProjectileScript>().targetPos = isTarget2[TargetIndex].transform.Find("HitPoint").transform;
                                //어택 수정전
                                //projectile_i.GetComponent<ProjectileScript>().targetPos = MonsterList[TargetIndex].transform.Find("HitPoint").transform;

                                if (myChar.PowerShotCount == 1)
                                {
                                    projectile_i.GetComponent<ProjectileScript>().Damage = myChar.Damage * 2.5f;
                                    //projectile_i.transform.GetChild(0).localScale = CharacterProjcetileScale;
                                    PowerShotCheck(projectile_i);
                                }
                                switch (myChar.SelectStoneNum)
                                {
                                    case 0:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[0];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 0;
                                        break;
                                    case 1:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[0];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 1;
                                        break;
                                    case 2:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[1];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 2;
                                        break;
                                    case 3:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[2];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 3;
                                        break;
                                    case 4:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[3];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 4;
                                        break;
                                }
                            }
                        }

                    }

                }
            }
            else
            {
                projectile.GetComponent<Rigidbody>().AddForce((_preDistance - AttackPoint.position).normalized * OrbSpeed);
            }

            // Playwer스킬 파워샷 on일때
            if (GameManager.Instance.PlayerSkill[3] >= 1)
            {
                if (myChar.PowerShotCount > 1)
                {
                    projectile.GetComponent<ProjectileScript>().Damage = myChar.Damage;
                    myChar.PowerShotCount--;
                }
                else if (myChar.PowerShotCount == 1)
                {
                    myChar.PowerShotCount = 5;
                    projectile.GetComponent<ProjectileScript>().Damage = myChar.Damage * 1.5f;
                    //projectile.transform.GetChild(0).localScale = CharacterProjcetileScale;
                    PowerShotCheck(projectile);
                }
            }
            else
            {
                projectile.GetComponent<ProjectileScript>().Damage = myChar.Damage;
            }

            if (myChar.ASPD > 90)
            {
                fireCountdown = 20f / (66 + ((myChar.ASPD - 90) / 10));
            }
            else if (myChar.ASPD > 60)
            {
                fireCountdown = 20f / (60 + ((myChar.ASPD -60) / 5));
            }
            else
            {
                fireCountdown = 20f / myChar.ASPD;
            }
            
        }
        //else if (BoxList.Count != 0 && isBoxTarget)
        //{
        //    if (myChar.SelectStoneNum != 0)
        //    {
        //        myChar.ElementStone[myChar.SelectStoneNum] -= 1;

        //        //if (myChar.ElementStone[myChar.SelectStoneNum] <= 0)
        //        //{
        //        //    if (myChar.SelectStoneNum <= 3)
        //        //    {
        //        //        myChar.SelectStoneNum++;
        //        //        Debug.Log(11);
        //        //    }
        //        //    else if (myChar.SelectStoneNum >=4)
        //        //    {
        //        //        myChar.SelectStoneNum = 0;
        //        //        Debug.Log(22);
        //        //    }
                    
        //        //}
        //    }
        //    GameObject projectile = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
        //    projectile.transform.parent = PlayerObjectManager.transform;

        //    projectile.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
        //    if (!myChar.Wizard)
        //    {
        //        //projectile.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[0];
        //        switch (myChar.SelectStoneNum)
        //        {
        //            case 0:
        //                projectile.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[0];
        //                break;
        //            case 1:
        //                projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[0];
        //                break;
        //            case 2:
        //                projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[1];
        //                break;
        //            case 3:
        //                projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[2];
        //                break;
        //            case 4:
        //                projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[3];
        //                break;
        //        }
        //    }
        //    else if (myChar.Wizard)
        //    {
        //        projectile.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[1];
        //    }
        //    isBoxTarget = Physics2D.OverlapCircle(AttackPoint.position, attackCheckRadius, _boxLayerMask);
        //    if (isBoxTarget &&/*MonsterList.Count == 0 &&*/ BoxList[BoxTargetIndex] != null)
        //    {
                
        //        Vector3 distance = BoxList[BoxTargetIndex].transform.Find("HitPoint").position;

        //        if (/*!myChar.Warrior &&*/ !myChar.Wizard)
        //        {
        //            //미사일 날라가는 방향
        //            Vector3 dir = distance - AttackPoint.position;
        //            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //            projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //            //translate로 변경 중인 코드
        //            //projectile.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
        //            //projectile.GetComponent<Rigidbody2D>().AddForce((distance - AttackPoint.position).normalized * OrbSpeed);

        //            _preDistance = distance;
        //            //projectile.GetComponent<Rigidbody2D>().AddForce((distance - AttackPoint.position).normalized * OrbSpeed);
        //            projectile.GetComponent<ProjectileScript>().dir = dir;
        //            projectile.GetComponent<ProjectileScript>().AttackPoint = AttackPoint.position;
        //            projectile.GetComponent<ProjectileScript>().distance = distance;

        //            if (GameManager.Instance.PlayerSkill[12] >= 1)
        //            {
        //                for (float i = 1; i <= GameManager.Instance.PlayerSkill[12]; i++)
        //                {
        //                    if (i % 2 == 1)
        //                    {
        //                        GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
        //                        projectile_i.transform.parent = PlayerObjectManager.transform;
        //                        projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + (15 * ((i / 2) + 0.5f)), Vector3.forward);
        //                        projectile_i.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
        //                        projectile_i.GetComponent<ProjectileScript>().dir = dir;
        //                        if (myChar.PowerShotCount == 1)
        //                        {
        //                            projectile_i.GetComponent<ProjectileScript>().Damage = myChar.Damage * 2.5f;
        //                            //projectile_i.transform.GetChild(0).localScale = CharacterProjcetileScale;
        //                            PowerShotCheck(projectile_i);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
        //                        projectile_i.transform.parent = PlayerObjectManager.transform;
        //                        projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - (15 * (i / 2)), Vector3.forward);
        //                        projectile_i.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
        //                        projectile_i.GetComponent<ProjectileScript>().dir = dir;
        //                        if (myChar.PowerShotCount == 1)
        //                        {
        //                            projectile_i.GetComponent<ProjectileScript>().Damage = myChar.Damage * 2.5f;
        //                            //projectile_i.transform.GetChild(0).localScale = CharacterProjcetileScale;
        //                            PowerShotCheck(projectile_i);
        //                        }
        //                    }


        //                }
        //            }
        //        }
        //        //else if (myChar.Warrior)
        //        //{
        //        //    Vector2 Vo = WarriorTargeting(distance, AttackPoint.position, 1f);
        //        //    Vector3 dir = distance - AttackPoint.position;
        //        //    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //        //    projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //        //    projectile.GetComponent<Transform>().localScale *= -1;
        //        //    //transform.rotation = Quaternion.LookRotation(Vo);

        //        //    projectile.GetComponent<Rigidbody2D>().velocity = Vo;
        //        //    //projectile.GetComponent<Rigidbody2D>().AddForce(AttackPoint.position * OrbSpeed);
        //        //    if (GameManager.Instance.PlayerSkill[12] >= 1)
        //        //    {
        //        //        if (GameManager.Instance.PlayerSkill[12] >= 3)
        //        //        {
        //        //            GameObject projectile1 = Instantiate(PlayerBolt[1], AttackPoint.position, Quaternion.identity);
        //        //            projectile1.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + 15, Vector3.forward);

        //        //            GameObject projectile1_1 = Instantiate(PlayerBolt[1], AttackPoint.position, Quaternion.identity);
        //        //            projectile1_1.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + 30, Vector3.forward);

        //        //            GameObject projectile2 = Instantiate(PlayerBolt[1], AttackPoint.position, Quaternion.identity);
        //        //            projectile2.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - 15, Vector3.forward);

        //        //            GameObject projectile2_1 = Instantiate(PlayerBolt[1], AttackPoint.position, Quaternion.identity);
        //        //            projectile2_1.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - 30, Vector3.forward);
        //        //        }
        //        //        else if (GameManager.Instance.PlayerSkill[12] == 2)
        //        //        {
        //        //            GameObject projectile1 = Instantiate(PlayerBolt[1], AttackPoint.position, Quaternion.identity);
        //        //            projectile1.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + 15, Vector3.forward);

        //        //            GameObject projectile2 = Instantiate(PlayerBolt[1], AttackPoint.position, Quaternion.identity);
        //        //            projectile2.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - 15, Vector3.forward);
        //        //        }
        //        //        else if (GameManager.Instance.PlayerSkill[12] == 1)
        //        //        {
        //        //            //transform.rotation = Quaternion.LookRotation(Vo);

        //        //            GameObject projectile1 = Instantiate(PlayerBolt[1], AttackPoint.position, Quaternion.identity);
        //        //            projectile1.GetComponent<Rigidbody2D>().velocity = Vo;
        //        //            projectile1.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + 15, Vector3.forward);
        //        //            projectile1.GetComponent<Transform>().localScale *= -1;
        //        //        }
        //        //    }
        //        //}
        //        else if (myChar.Wizard)
        //        {
        //            //Vector3 dir = distance - AttackPoint.position;
        //            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //            //projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //            _preDistance = distance;
        //            //translate로 변경중이면서 모든캐릭터 유도미사일 적용 코드
        //            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //            //projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //            projectile.GetComponent<ProjectileScript>().targetPos = BoxList[BoxTargetIndex].transform.Find("HitPoint").transform;
        //            //projectile.GetComponent<ProjectileScript>().distance = distance;
        //            projectile.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;

        //            if (GameManager.Instance.PlayerSkill[12] >= 1)
        //            {
        //                for (float i = 1; i <= GameManager.Instance.PlayerSkill[12]; i++)
        //                {
        //                    if (i % 2 == 1)
        //                    {
        //                        GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
        //                        //projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + (15 * ((i / 2) + 0.5f)), Vector3.forward);
        //                        projectile_i.GetComponent<ProjectileScript>().transform.rotation = Quaternion.Euler(0, 0, (30 * ((i / 2) + 0.5f)));
        //                        projectile_i.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
        //                        //projectile_i.GetComponent<ProjectileScript>().distance = distance;
        //                        projectile_i.GetComponent<ProjectileScript>().targetPos = BoxList[BoxTargetIndex].transform.Find("HitPoint").transform;
        //                        if (GameManager.Instance.PlayerSkill[3] >= 1)
        //                        {
        //                            if (myChar.PowerShotCount == 1)
        //                            {
        //                                projectile_i.GetComponent<ProjectileScript>().Damage = myChar.Damage * 2.5f;
        //                                //projectile_i.transform.GetChild(0).localScale = CharacterProjcetileScale;
        //                                PowerShotCheck(projectile_i);
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
        //                        projectile_i.GetComponent<ProjectileScript>().transform.rotation = Quaternion.Euler(0, 0, -(30 * (i / 2)));
        //                        //projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - (15 * (i / 2)), Vector3.forward);
        //                        projectile_i.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
        //                        //projectile_i.GetComponent<ProjectileScript>().distance = distance;
        //                        projectile_i.GetComponent<ProjectileScript>().targetPos = BoxList[BoxTargetIndex].transform.Find("HitPoint").transform;
        //                        if (GameManager.Instance.PlayerSkill[3] >= 1)
        //                        {
        //                            if (myChar.PowerShotCount == 1)
        //                            {
        //                                projectile_i.GetComponent<ProjectileScript>().Damage = myChar.Damage * 2.5f;
        //                                //projectile_i.transform.GetChild(0).localScale = CharacterProjcetileScale;
        //                                PowerShotCheck(projectile_i);
        //                            }
        //                        }
        //                    }


        //                }
        //            }

        //        }
        //    }
        //    else
        //    {
        //        return;
        //    }
        //    //else
        //    //{
        //    //    projectile.GetComponent<Rigidbody>().AddForce((_preDistance - AttackPoint.position).normalized * OrbSpeed);
        //    //}

        //    // Playwer스킬 파워샷 on일때
        //    if (GameManager.Instance.PlayerSkill[3] >= 1)
        //    {
        //        if (myChar.PowerShotCount > 1)
        //        {
        //            projectile.GetComponent<ProjectileScript>().Damage = myChar.Damage;
        //            myChar.PowerShotCount--;
        //        }
        //        else if (myChar.PowerShotCount == 1)
        //        {
        //            myChar.PowerShotCount = 5;
        //            projectile.GetComponent<ProjectileScript>().Damage = myChar.Damage * 2.5f;
        //            PowerShotCheck(projectile);
        //            //projectile.transform.GetChild(0).localScale = CharacterProjcetileScale;
        //        }
        //    }
        //    else
        //    {
        //        projectile.GetComponent<ProjectileScript>().Damage = myChar.Damage;
        //    }

        //    fireCountdown = 2f / ((fireRate * ASPDActive) + atkSpd);
        //}


    }

    void TargetCheck()
    {
        if (fireCountdown <= 0f)
        {

            //transform.GetComponent<PlayerController>().isJumpShoot = true;
            //if (MonsterCheck)
            //{
            //    Debug.Log(GameObject.FindGameObjectsWithTag("Monster").Length);
            //}
            //MonsterList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
            //BoxList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Box"));

            float shortestDistance = Mathf.Infinity;

            foreach (GameObject Monster in MonsterList)
            {
                float distanceToMonster = Vector2.Distance(transform.position, Monster.transform.position);
                if (distanceToMonster < shortestDistance)
                {
                    shortestDistance = distanceToMonster;
                    nearestEnemy = Monster;
                }
            }

            //foreach (GameObject boxes in BoxList)
            //{

            //    float distanceToMonster = Vector2.Distance(transform.position, boxes.transform.position);
            //    if (distanceToMonster < shortestDistance)
            //    {
            //        shortestDistance = distanceToMonster;
            //        nearestEnemy = boxes;
            //    }
            //}
            //foreach (GameObject Monster in MonsterList)
            //{
            //    if (isTarget)
            //    {
            //        float distanceToMonster = Vector2.Distance(transform.position, Monster.transform.position);
            //        if (distanceToMonster < shortestDistance)
            //        {
            //            shortestDistance = distanceToMonster;
            //            nearestEnemy = Monster;
            //        }
            //    }
            //    else if(isBoxTarget)
            //    {
            //        float distanceToMonster = Vector2.Distance(transform.position, BoxList[0].transform.position);
            //        if (distanceToMonster < shortestDistance)
            //        {
            //            shortestDistance = distanceToMonster;
            //            nearestEnemy = BoxList[0];
            //        }
            //    }
            //}
            if (isTarget2.Length != 0)/*(MonsterList.Count != 0)*/
            {
                //prevTargetIndex = TargetIndex;
                currentDist = 0f;
                closeDistIndex = 0;
                TargetIndex = -1;
                //distanceCheckList.Clear();
                //for (int i = 0; i < isTarget2.Length; i++)
                //{
                //    if (isTarget2[i] == null) { return; }

                //    currentDist = Vector2.Distance(transform.position, isTarget2[i].transform.position);

                //    bool isHit = Physics2D.Raycast(AttackPoint.position, isTarget2[i].transform.position - AttackPoint.position);

                //    hit = Physics2D.Raycast(AttackPoint.position, isTarget2[i].transform.position - AttackPoint.position, 10f, (1 << LayerMask.NameToLayer("Ground")));

                //    distanceCheckList.Add(currentDist);
                //    if (isHit)
                //    {
                //        Debug.DrawRay(AttackPoint.position, isTarget2[i].transform.position - transform.position, Color.red);

                //        if (hit)
                //        {
                //            Debug.Log("벽걸림");
                //        }
                //        else
                //        {
                //            if (TargetDist >= currentDist)
                //            {
                //                Debug.Log("공격체크");
                //                TargetIndex = i;

                //                TargetDist = currentDist;

                //            }
                //        }                        
                //    }
                //    if (closetDist >= currentDist)
                //    {
                //        closeDistIndex = i;
                //        closetDist = currentDist;
                //        Debug.Log("가까운녀석");
                //    }
                //}
                //if (distanceCheckList.Count >= 2)
                //{
                //    //distanceCheckList.Sort();
                //    float preDistanceCheck;
                //    Collider2D preDistanceRigd;
                //    for (int i = 0; i < distanceCheckList.Count; i++)
                //    {
                //        for (int j = i + 1; j < distanceCheckList.Count; j++)
                //        {
                //            if (distanceCheckList[i] > distanceCheckList[j])
                //            {
                //                preDistanceCheck = distanceCheckList[i];
                //                preDistanceRigd = isTarget2[i];
                //                distanceCheckList[i] = distanceCheckList[j];
                //                isTarget2[i] = isTarget2[j];
                //                distanceCheckList[j] = preDistanceCheck;
                //                isTarget2[j] = preDistanceRigd;
                //            }
                //        }
                //    }

                //    for (int i = 0; i < isTarget2.Length; i++)
                //    {
                //        if (isTarget2[i] == null) { return; }

                //        currentDist = Vector2.Distance(transform.position, isTarget2[i].transform.position);

                //        bool isHit = Physics2D.Raycast(AttackPoint.position, isTarget2[i].transform.position - AttackPoint.position);

                //        hit = Physics2D.Raycast(AttackPoint.position, isTarget2[i].transform.position - AttackPoint.position, 10f/*, (1 << LayerMask.NameToLayer("Ground"))*/);

                //        distanceCheckList.Add(currentDist);
                //        if (isHit)
                //        {
                //            Debug.DrawRay(AttackPoint.position, isTarget2[i].transform.position - transform.position, Color.red);
                //            Debug.Log(hit.collider.gameObject.name);
                //            if (hit)
                //            {

                //            }
                //            else
                //            {
                //                if (TargetDist >= currentDist)
                //                {
                //                    Debug.Log("공격체크22");
                //                    TargetIndex = i;

                //                    TargetDist = currentDist;

                //                }
                //            }
                //        }
                //        if (closetDist >= currentDist)
                //        {
                //            closeDistIndex = i;
                //            closetDist = currentDist;
                //            Debug.Log("가까운녀석22");
                //        }
                //    }
                //}
                //else
                //{

                //}

                for (int i = 0; i < isTarget2.Length; i++)
                {
                    if (isTarget2[i] == null) { return; }

                    currentDist = Vector2.Distance(transform.position, isTarget2[i].transform.position);

                    RaycastHit2D hit = Physics2D.Raycast(AttackPoint.position, isTarget2[i].transform.position - AttackPoint.position, 20f, AttackCheck_layerMask);
                    
                    bool isHit = Physics2D.Raycast(AttackPoint.position, isTarget2[i].transform.position - AttackPoint.position, 20f, _layerMask);

                    if (isHit && hit.transform.CompareTag("Monster"))
                    {
                        if (TargetDist >= currentDist)
                        {
                            TargetIndex = i;
                            TargetDist = currentDist;
                        }
                    }
                    if (closetDist >= currentDist)
                    {
                        closeDistIndex = i;
                        closetDist = currentDist;
                    }
                }
                if (TargetIndex == -1)
                {
                    TargetIndex = closeDistIndex;
                }
                closetDist = 100f;
                TargetDist = 100f;
                getATarget = true;

                if (Time.timeScale != 0)
                {
                    if (nearestEnemy.GetComponent<BossPatternScript>())
                    {
                        if ((int)nearestEnemy.GetComponent<BossPatternScript>()._sprite.color.a >= 1)
                        {
                            TargetUp();
                        }                        
                    }
                    else
                    {
                        TargetUp();
                    }
                    
                }
            }
        }
        fireCountdown -= Time.deltaTime;

    }
    private void PowerShotCheck(GameObject projectile)
    {
        if (myChar.SelectHero != 3)
        {
            projectile.transform.GetChild(0).localScale = CharacterProjcetileScale;
        }
        else if (myChar.SelectHero == 3)
        {
            projectile.transform.localScale = CharacterProjcetileScale;
        }
        
    }
    private void ProjcetileScale()
    {
        if (myChar.SelectHero != 3)
        {
            CharacterProjcetileScale = new Vector3(1.2f, 1.2f, 1);
        }
        else if (myChar.SelectHero == 3)
        {
            CharacterProjcetileScale = new Vector3(0.75f, 0.75f, 0.75f);
        }

    }
    public void JumpShoot()
    {
        if (GameManager.Instance.PlayerSkill[18] >= 1)
        {
            GameObject projectile1 = Instantiate(PlayerBolt[1], AttackPoint.position, Quaternion.identity);
            projectile1.GetComponent<Transform>().rotation = Quaternion.AngleAxis(180, Vector3.forward);
            transform.GetComponent<PlayerController>().isJumpShoot = false;
        }
    }

    public void MultiShot()
    {
        //GameObject projectile = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, transform.rotation);
        //projectile.transform.parent = PlayerObjectManager.transform;

        //projectile.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
        if (isTarget && MonsterCheck)
        {
            if (myChar.SelectStoneNum != 0)
            {
                myChar.ElementStone[myChar.SelectStoneNum] -= 1;
                if (myChar.ElementStone[myChar.SelectStoneNum] <= 0)
                {
                    if (myChar.SelectStoneNum <= 3)
                    {
                        myChar.SelectStoneNum++;
                    }
                    else if (myChar.SelectStoneNum >= 4)
                    {
                        myChar.SelectStoneNum = 0;
                    }

                }
            }

            GameObject projectile = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
            projectile.transform.parent = PlayerObjectManager.transform;
            projectile.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;

            if (!myChar.Wizard)
            {
                switch (myChar.SelectStoneNum)
                {
                    case 0:
                        projectile.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[0];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 0;
                        break;
                    case 1:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[0];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 1;
                        break;
                    case 2:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[1];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 2;
                        break;
                    case 3:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[2];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 3;
                        break;
                    case 4:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[3];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 4;
                        break;
                }
            }
            else if (myChar.Wizard)
            {
                switch (myChar.SelectStoneNum)
                {
                    case 0:
                        projectile.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[0];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 0;
                        break;
                    case 1:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[0];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 1;
                        break;
                    case 2:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[1];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 2;
                        break;
                    case 3:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[2];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 3;
                        break;
                    case 4:
                        projectile.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[3];
                        projectile.GetComponent<ProjectileScript>().ElementalCnt = 4;
                        break;
                }
            }
            if (isTarget2[TargetIndex] != null)/*(isTarget2[TargetIndex] != null)*/
            {
                //어택수정 후
                Vector3 distance = isTarget2[TargetIndex].transform.Find("HitPoint").position;
                //어택수정 전
                //Vector3 distance = MonsterList[TargetIndex].transform.Find("HitPoint").position;

                if (/*!myChar.Warrior &&*/ !myChar.Wizard)
                {
                    //미사일 날라가는 방향
                    Vector3 dir = distance - AttackPoint.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                    //translate로 변경 중인 코드
                    //projectile.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
                    //projectile.GetComponent<Rigidbody2D>().AddForce((distance - AttackPoint.position).normalized * OrbSpeed);

                    _preDistance = distance;
                    //projectile.GetComponent<Rigidbody2D>().AddForce((distance - AttackPoint.position).normalized * OrbSpeed);
                    projectile.GetComponent<ProjectileScript>().dir = dir;
                    projectile.GetComponent<ProjectileScript>().AttackPoint = AttackPoint.position;
                    projectile.GetComponent<ProjectileScript>().distance = distance;

                    if (GameManager.Instance.PlayerSkill[12] >= 1)
                    {
                        for (float i = 1; i <= GameManager.Instance.PlayerSkill[12]; i++)
                        {
                            if (i % 2 == 1)
                            {
                                GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
                                projectile_i.transform.parent = PlayerObjectManager.transform;
                                projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + (15 * ((i / 2) + 0.5f)), Vector3.forward);
                                projectile_i.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
                                projectile_i.GetComponent<ProjectileScript>().dir = dir;
                                if (myChar.PowerShotCount == 1)
                                {
                                    projectile_i.GetComponent<ProjectileScript>().Damage = myChar.Damage * 2.5f;
                                    //projectile_i.transform.GetChild(0).localScale = CharacterProjcetileScale;
                                    PowerShotCheck(projectile_i);
                                }
                                switch (myChar.SelectStoneNum)
                                {
                                    case 0:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[0];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 0;
                                        break;
                                    case 1:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[0];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 1;
                                        break;
                                    case 2:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[1];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 2;
                                        break;
                                    case 3:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[2];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 3;
                                        break;
                                    case 4:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[3];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 4;
                                        break;
                                }
                            }
                            else
                            {
                                GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
                                projectile_i.transform.parent = PlayerObjectManager.transform;
                                projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - (15 * (i / 2)), Vector3.forward);
                                projectile_i.GetComponent<ProjectileScript>().WeaponSpeed = OrbSpeed;
                                projectile_i.GetComponent<ProjectileScript>().dir = dir;
                                if (myChar.PowerShotCount == 1)
                                {
                                    projectile_i.GetComponent<ProjectileScript>().Damage = myChar.Damage * 2.5f;
                                    //projectile_i.transform.GetChild(0).localScale = CharacterProjcetileScale;
                                    PowerShotCheck(projectile_i);
                                }
                                switch (myChar.SelectStoneNum)
                                {
                                    case 0:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = BoltHitEffect[0];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 0;
                                        break;
                                    case 1:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[0];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 1;
                                        break;
                                    case 2:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[1];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 2;
                                        break;
                                    case 3:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[2];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 3;
                                        break;
                                    case 4:
                                        projectile_i.GetComponent<ProjectileScript>().HitEffect = SeasnalHitEffect[3];
                                        projectile_i.GetComponent<ProjectileScript>().ElementalCnt = 4;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                projectile.GetComponent<Rigidbody>().AddForce((_preDistance - AttackPoint.position).normalized * OrbSpeed);
            }

            // Playwer스킬 파워샷 on일때
            if (GameManager.Instance.PlayerSkill[3] >= 1)
            {
                if (myChar.PowerShotCount > 1)
                {
                    projectile.GetComponent<ProjectileScript>().Damage = myChar.Damage;
                    myChar.PowerShotCount--;
                }
                else if (myChar.PowerShotCount == 1)
                {
                    myChar.PowerShotCount = 5;
                    projectile.GetComponent<ProjectileScript>().Damage = myChar.Damage * 2.5f;
                    //projectile.transform.GetChild(0).localScale = CharacterProjcetileScale;
                    PowerShotCheck(projectile);
                }
            }
            else
            {
                projectile.GetComponent<ProjectileScript>().Damage = myChar.Damage;
            }
        }
    }

    Vector2 WarriorTargeting(Vector2 target, Vector2 origin, float time)
    {
        Vector2 distance = target - origin;
        Vector2 distanceXZ = distance;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        Vector2 result = distanceXZ.normalized;

        result *= Vxz;
        result.y = Vy;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, attackCheckRadius);
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(transform.parent.position, new Vector2(TestX, TestY));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.parent.position, new Vector2(15.5f, 10f));

        if (getATarget)
        {
            for (int i = 0; i < isTarget2.Length; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(AttackPoint.position, isTarget2[i].transform.position - AttackPoint.position, 20f, AttackCheck_layerMask);
                bool isHit = Physics2D.Raycast(AttackPoint.position, isTarget2[i].transform.position - AttackPoint.position, 20f, _layerMask);
                //bool isHit = Physics2D.Raycast(AttackPoint.position, isTarget2[i].transform.position - AttackPoint.position);

                if (isHit && hit.transform.CompareTag("Monster"))
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(AttackPoint.position, isTarget2[i].transform.position - AttackPoint.position);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(AttackPoint.position, isTarget2[i].transform.position - AttackPoint.position);
                }
            }
        }
    }
}
