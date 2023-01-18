using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinalBossTargeting : MonoBehaviour
{
    public static FinalBossTargeting Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FinalBossTargeting>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("FinalBossTargeting");
                    instance = instanceContainer.AddComponent<FinalBossTargeting>();
                }
            }
            return instance;
        }
    }

    public enum BossType
    {
        Tutorial,
        Swordsman,
        Warrior,
        Archer,
        Wizard,
        Ninja,
        Corrupted_Swordsman
    }

    MyObject myChar;
    private static FinalBossTargeting instance;

    [SerializeField]
    private bool StatCheck = false;

    public int MonsterIndex;
    private GameObject PlayerObjectManager;

    public LineRenderer _lineRender;
    //protected CharacterStat characterState;
    public float monsterHp;
    public bool getATarget = true;
    public bool isTarget;
    public bool MonsterCheck;
    public bool LayerPoint;
    public bool TargetShot;
    private GameObject MonsterProjectile;

    public BossType bossType;

    float currentDist = 0;      //현재 거리
    float closetDist = 100f;    //가까운 거리
    float TargetDist = 100f;   //타겟 거리
    int closeDistIndex = 0;    //가장 가까운 인덱스
    public int TargetIndex = -1;      //타겟팅 할 인덱스
    private int ShootNum;
    int prevTargetIndex = 0;
    public float OrbSpeed = 500f; //발사체 속도
    RaycastHit2D hit;
    public LayerMask _layerMask;

    public float attackCheckRadius;

    [SerializeField]
    GameObject nearestEnemy = null;
    public GameObject PlayerList;
    Vector3 _preDistance;

    public float fireCountdown = 0f;
    public float fireRate = 1;
    public float ASPDActive = 1;

    public float atkSpd = 0f;

    public List<GameObject> MonsterList = new List<GameObject>();

    public int currentOrb;
    public int TargetShotCnt;
    public int ProjectileCnt;
    public List<GameObject> PlayerBolt = new List<GameObject>();
    public List<GameObject> BoltHitEffect = new List<GameObject>();
    public Transform AttackPoint;
    public Transform target;
    //Monster를 담는 List 
    public float OrbDamage = 1f;




    private void Awake()
    {
        myChar = MyObject.MyChar;
        MonsterProjectile = GameObject.Find("MonsterProjectileManager");
        //characterState = GetComponent<CharacterStat>();
    }

    private void Start()
    {

    }
    void Update()
    {
        if (!StatCheck)
        {
            //movementSpeed = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Speed;
            fireRate = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).ASPD;
            fireCountdown = fireRate;
            attackCheckRadius = 15f + GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Range;
            monsterHp = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).HP;
            //monsterHp = 10000000000000000f;

            StatCheck = true;
        }

        if (monsterHp > 0)
        {
            if (!transform.GetComponent<FinalBossPattern>().isSkill)
            {
                TargetCheck();
            }
            
        }

        CheckSurroundings();
        TargetAutoCheck();

        if (MonsterCheck)
        {
            //RoomManager.Instance.NextDoorClose();
        }
        else if (!MonsterCheck)
        {
            //RoomManager.Instance.NextDoorOpen();
            getATarget = false;
        }
        if (!myChar.Tutorial)
        {            
            //attackCheckRadius = 15f + (GameManager.Instance.UnitDataMgr.GetTemplate(24 + myChar.EnthroneHeroNum).Range * 0.125f);
            //atkSpd = (GameManager.Instance.UnitDataMgr.GetTemplate(24 + myChar.EnthroneHeroNum).ASPD * 0.5f);
        }
        else
        {
            attackCheckRadius = 2f + (GameManager.Instance.UnitDataMgr.GetTemplate(29).Range * 0.125f);
            atkSpd = (GameManager.Instance.UnitDataMgr.GetTemplate(29).ASPD * 0.5f);
        }
        if (bossType == BossType.Archer)
        {
            if (LayerPoint)
            {
                Vector3 distance = PlayerList.transform.position;

                Vector3 dir = distance - AttackPoint.position;
                _lineRender.SetPosition(0, AttackPoint.position);
                _lineRender.SetPosition(1, distance);
            }
        }
    }
    private void CheckSurroundings()
    {
        isTarget = Physics2D.OverlapCircle(AttackPoint.position, attackCheckRadius, _layerMask);
        MonsterCheck = Physics2D.OverlapBox(transform.parent.position, new Vector2(16f, 10f), 0, _layerMask);
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

    void TargetUp()
    {        
        if (isTarget && MonsterCheck)
        {
            if (myChar.SelectStoneNum != 0)
            {
                myChar.ElementStone[myChar.SelectStoneNum] -= 1;
            }
            GameObject projectile = Instantiate(PlayerBolt[0], AttackPoint.position, Quaternion.identity);
            projectile.transform.parent = MonsterProjectile.transform;
            //projectile.transform.parent = PlayerObjectManager.transform;

            projectile.GetComponent<FinalBossProjectile>().WeaponSpeed = OrbSpeed;
            if (bossType != BossType.Wizard)
            {
                projectile.GetComponent<FinalBossProjectile>().HitEffect = BoltHitEffect[0];
            }
            else if (bossType == BossType.Wizard)
            {
                projectile.GetComponent<FinalBossProjectile>().HitEffect = BoltHitEffect[1];
            }

            if (MonsterList[TargetIndex] != null)
            {
                Vector3 distance = MonsterList[TargetIndex].transform.Find("HitPoint").position;

                if (bossType != BossType.Wizard)
                {
                    //미사일 날라가는 방향
                    Vector3 dir = distance - AttackPoint.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                    //translate로 변경 중인 코드
                    //projectile.GetComponent<FinalBossProjectile>().WeaponSpeed = OrbSpeed;
                    //projectile.GetComponent<Rigidbody2D>().AddForce((distance - AttackPoint.position).normalized * OrbSpeed);

                    _preDistance = distance;
                    //projectile.GetComponent<Rigidbody2D>().AddForce((distance - AttackPoint.position).normalized * OrbSpeed);
                    projectile.GetComponent<FinalBossProjectile>().dir = dir;
                    projectile.GetComponent<FinalBossProjectile>().AttackPoint = AttackPoint.position;
                    projectile.GetComponent<FinalBossProjectile>().distance = distance;

                    ShootNum++;
                    if (GetComponent<FinalBossPattern>().AttackNum != 0)
                    {
                        GetComponent<FinalBossPattern>().AttackNum--;
                    }

                    if (ShootNum >= 5)
                    {
                        for (float i = 1; i <= 2; i++)
                        {
                            if (i % 2 == 1)
                            {
                                GameObject projectile_i = Instantiate(PlayerBolt[myChar.ThroneWeaponSkin], AttackPoint.position, Quaternion.identity);
                                projectile_i.transform.parent = MonsterProjectile.transform;
                                projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + (15 * ((i / 2) + 0.5f)), Vector3.forward);
                                projectile_i.GetComponent<FinalBossProjectile>().WeaponSpeed = OrbSpeed;
                                projectile_i.GetComponent<FinalBossProjectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                                projectile_i.GetComponent<FinalBossProjectile>().dir = dir;
                            }
                            else
                            {
                                GameObject projectile_i = Instantiate(PlayerBolt[myChar.ThroneWeaponSkin], AttackPoint.position, Quaternion.identity);
                                projectile_i.transform.parent = MonsterProjectile.transform;
                                projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - (15 * (i / 2)), Vector3.forward);
                                projectile_i.GetComponent<FinalBossProjectile>().WeaponSpeed = OrbSpeed;
                                projectile_i.GetComponent<FinalBossProjectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                                projectile_i.GetComponent<FinalBossProjectile>().dir = dir;
                            }
                        }
                        ShootNum = 0;
                    }
                    //if (GameManager.Instance.PlayerSkill[12] >= 1)
                    //{
                    //    for (float i = 1; i <= GameManager.Instance.PlayerSkill[12]; i++)
                    //    {
                    //        if (i % 2 == 1)
                    //        {
                    //            GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
                    //            projectile_i.transform.parent = PlayerObjectManager.transform;
                    //            projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + (15 * ((i / 2) + 0.5f)), Vector3.forward);
                    //            projectile_i.GetComponent<FinalBossProjectile>().WeaponSpeed = OrbSpeed;
                    //            projectile_i.GetComponent<FinalBossProjectile>().dir = dir;
                    //        }
                    //        else
                    //        {
                    //            GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
                    //            projectile_i.transform.parent = PlayerObjectManager.transform;
                    //            projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - (15 * (i / 2)), Vector3.forward);
                    //            projectile_i.GetComponent<FinalBossProjectile>().WeaponSpeed = OrbSpeed;
                    //            projectile_i.GetComponent<FinalBossProjectile>().dir = dir;
                    //        }
                    //    }
                    //}
                }
                else if (bossType == BossType.Wizard)
                {
                    //Vector3 dir = distance - AttackPoint.position;
                    //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    //projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                    _preDistance = distance;
                    //translate로 변경중이면서 모든캐릭터 유도미사일 적용 코드
                    //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    //projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    projectile.GetComponent<FinalBossProjectile>().targetPos = MonsterList[TargetIndex].transform.Find("HitPoint").transform;
                    //projectile.GetComponent<FinalBossProjectile>().distance = distance;
                    projectile.GetComponent<FinalBossProjectile>().WeaponSpeed = OrbSpeed / 2.5f;
                    ShootNum++;
                    if (GetComponent<FinalBossPattern>().AttackNum != 0)
                    {
                        GetComponent<FinalBossPattern>().AttackNum--;
                    }

                    //if (GameManager.Instance.PlayerSkill[12] >= 1)
                    //{
                    //    for (float i = 1; i <= GameManager.Instance.PlayerSkill[12]; i++)
                    //    {
                    //        if (i % 2 == 1)
                    //        {
                    //            GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
                    //            //projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) + (15 * ((i / 2) + 0.5f)), Vector3.forward);
                    //            projectile_i.GetComponent<FinalBossProjectile>().transform.rotation = Quaternion.Euler(0, 0, (30 * ((i / 2) + 0.5f)));
                    //            projectile_i.GetComponent<FinalBossProjectile>().WeaponSpeed = OrbSpeed;
                    //            //projectile_i.GetComponent<FinalBossProjectile>().distance = distance;
                    //            projectile_i.GetComponent<FinalBossProjectile>().targetPos = MonsterList[TargetIndex].transform.Find("HitPoint").transform;
                    //        }
                    //        else
                    //        {
                    //            GameObject projectile_i = Instantiate(PlayerBolt[currentOrb], AttackPoint.position, Quaternion.identity);
                    //            projectile_i.GetComponent<FinalBossProjectile>().transform.rotation = Quaternion.Euler(0, 0, -(30 * (i / 2)));
                    //            //projectile_i.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle - 90) - (15 * (i / 2)), Vector3.forward);
                    //            projectile_i.GetComponent<FinalBossProjectile>().WeaponSpeed = OrbSpeed;
                    //            //projectile_i.GetComponent<FinalBossProjectile>().distance = distance;
                    //            projectile_i.GetComponent<FinalBossProjectile>().targetPos = MonsterList[TargetIndex].transform.Find("HitPoint").transform;
                    //        }


                    //    }
                    //}

                }
            }
            else
            {
                projectile.GetComponent<Rigidbody>().AddForce((_preDistance - AttackPoint.position).normalized * OrbSpeed);
            }

            projectile.GetComponent<FinalBossProjectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;

            //// Playwer스킬 파워샷 on일때
            //if (GameManager.Instance.PlayerSkill[3] >= 1)
            //{
            //    if (myChar.PowerShotCount > 1)
            //    {
            //        projectile.GetComponent<FinalBossProjectile>().Damage = myChar.Damage;
            //        myChar.PowerShotCount--;
            //    }
            //    else if (myChar.PowerShotCount == 1)
            //    {
            //        myChar.PowerShotCount = 5;
            //        projectile.GetComponent<FinalBossProjectile>().Damage = myChar.Damage * 2.5f;

            //    }
            //}
            //else
            //{
            //    projectile.GetComponent<FinalBossProjectile>().Damage = myChar.Damage;
            //}
            fireCountdown = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).ASPD;
        }
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
            if (MonsterList.Count < 1)
            {
                if (GameManager.Instance.SelectCharacter != null)
                {
                    MonsterList.Add(GameManager.Instance.SelectCharacter);
                }
                
            }
            //MonsterList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            

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
            if (MonsterList.Count != 0)
            {

                prevTargetIndex = TargetIndex;
                currentDist = Mathf.Infinity;
                closeDistIndex = 0;
                TargetIndex = -1;

                for (int i = 0; i < MonsterList.Count; i++)
                {
                    if (MonsterList[i] == null) { return; }

                    currentDist = Vector2.Distance(transform.position, MonsterList[i].transform.position);

                    bool isHit = Physics2D.Raycast(transform.position, MonsterList[i].transform.position - transform.position);

                    if (isHit /*&& hit.transform.CompareTag("Monster")*/)
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
                    TargetUp();
                    if (MonsterIndex == 41)
                    {
                        SoundManager.Instance.PlaySfx(37);
                    }
                    if (MonsterIndex == 51)
                    {
                        SoundManager.Instance.PlaySfx(87);
                    }
                }
            }
        }
        fireCountdown -= Time.deltaTime;

    }

    public void Attack()
    {
        TargetShot = true;
        ProjectileCnt = 0;
        for (int i = 0; i < 3; i++)
        {
            Invoke("TargetAttack", i * 0.3f);
        }
        TargetShotCnt++;
        transform.GetComponent<FinalBossPattern>().SkillCoolTime = 1.5f;
    }
    public void TargetshotReset()
    {
        TargetShotCnt = 0;
    }
    public void TargetAttack()
    {
        GameObject projectile = Instantiate(PlayerBolt[myChar.ThroneWeaponSkin], AttackPoint.position, Quaternion.identity);
        //미사일 생성을 ProjectileManager자식으로 생성하기위함
        projectile.transform.parent = MonsterProjectile.transform;
        projectile.GetComponent<FinalBossProjectile>().WeaponSpeed = OrbSpeed;
        projectile.GetComponent<FinalBossProjectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
        ProjectileCnt++;
        if (LayerPoint)
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
        if (TargetShotCnt >= 3 && ProjectileCnt >= 3)
        {
            transform.GetComponent<FinalBossPattern>()._rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.GetComponent<FinalBossPattern>().isSkill = false;
        }

        if (MonsterIndex == 41)
        {
            SoundManager.Instance.PlaySfx(37);
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPoint.position, attackCheckRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.parent.position, new Vector2(16f, 10f));
    }
}
