using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPatternScript : MonoBehaviour
{
    public enum BossType
    {
        FlameBoss,
        NaturalBoss, NaturalBossDivision,
        RockBoss,
        SnowBoss
    }

    public BossType bossType;
    [SerializeField]
    public int MonsterIndex;

    public bool StartCheck = false;

    public LayerMask _layerMask;
    MyObject myChar;
    GameManager instance;
    Material _shader;
    public SpriteRenderer _sprite;
    public Animator _anim;
    private Rigidbody2D _rigidbody;
    private MonsterTargeting myMonster;
    private Collider2D _collider;

    public GameObject RecallPortal;
    public GameObject SummonedMonster;
    public GameObject Icicle;
    public GameObject IceWall;
    public Transform MovePoint;
    public Transform GroundCheck;
    public Transform wallCheck;
    public Transform DropPos;
    public GameObject EmergencyCheck;
    public GameObject RockBossHeadPos;
    public GameObject[] VineParent;

    [System.Serializable]
    public class Vine
    {
        public List<GameObject> ThornVine = new List<GameObject>();
        
    }

    public Vine[] VineArrays;

    [SerializeField]
    private int VineType;
    private Transform target;
    private Transform tf;

    public List<Transform> IciclePos = new List<Transform>();
    public List<Transform> IceWallPos = new List<Transform>();
    [SerializeField]
    private List<bool> IcicleOnCheck = new List<bool>();
    [SerializeField]
    private List<bool> IceWallNumOnCheck = new List<bool>();
    [SerializeField]
    private List<int> IcicleNum = new List<int>();
    private List<int> IceWallNum = new List<int>();
    public List<GameObject> wayPoint = new List<GameObject>();    
    private GameObject Effect;

    public int NextWayCheck = 0;
    public int attackCount = 1;
    public int attackCountingCheck;
    private int StormCount = 3;
    [SerializeField]
    private int StormCountingCheck;
    public int NextWayNum, PreWayNum;
    public int DivisionCount;
    [SerializeField]
    private int FrozenStormCnt = 3;

    public float monsterHp = 20f;
    public float wallCheckDistance;
    public float movementSpeed = 2f;
    public float fireCountdown = 0f;
    private float prefireCountdown;
    public float fireRate = 1f;
    public float SlowNum = 1f;
    private float MaxHp;
    [SerializeField]
    private int YetiKingType;
    private int JumpCnt = 2;
    private int AttackCnt;
    [SerializeField]
    private int EmptyIciclePosNum;
    private int EmptyIceWallPosNum;
    private int CrushNum;
    private IEnumerator IcicleCoroutine;
    private bool isTouchingWall;
    private bool isRightDir = true;
    private bool AttackMotionCheck = true;
    private bool MoveMotionCheck = true;
    private bool JumpAttackCheck = false;
    private bool DivisionMinuCheck = false;
    [SerializeField]
    private bool ItemDropCheck = false;    
    public bool FadeCheck = false;
    [SerializeField]
    private bool FadeOut = false;
    public bool FadeIn = false;
    public bool Teleport = false;

    public bool movingCheck = true;
    public bool DeathGroundCheck;
    public bool MiddleDivisionCheck;
    public bool VineStartCheck = false;
    public bool VinePatternCheck = false;
    public bool EarthquakeCheck = false;    //해당부분으로 미사일발사랑 어스퀘이크 패턴구분
    public bool Crush = false;              //Crush가 있어야 어스퀘이크 코루틴시작하고 2초뒤에 벽으로 박치기를함 없으면 바로박치기함
    public bool Earthquake = false;         //Earthquake없으면 코루틴이 반복실행되서 사방이경고표시가됨 1회표시를위해서 필요
    public bool HeadCrushCheck = false;
    public Vector2 StopPos;
    public Vector3 HeadPos;
    public GameObject FlameBossSummonePos;

    // Start is called before the first frame update
    void Start()
    {
        //MaxHp = monsterHp;
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
        _shader = GetComponent<Renderer>().material;
        _sprite = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        attackCountingCheck = attackCount;
        StormCountingCheck = StormCount;
        myMonster = GetComponent<MonsterTargeting>();
        IcicleCoroutine = YetiKingIcicle();

        MovePoint = transform.Find("MovePoint");
        Effect = gameObject.transform.Find("Effect").gameObject;
        GameObject Player = GameObject.Find("Player");
        tf = GetComponent<Transform>().transform;

        //불보스
        if (bossType == BossType.FlameBoss)
        {
            NextWayNum = Random.Range(0, wayPoint.Count);
        }
        //숲보스
        if (bossType == BossType.NaturalBoss)
        {
            NextWayNum = 0;
            //VineParent = EmergencyCheck.transform.parent.gameObject;
            if (!VineStartCheck)
            {
                StartCoroutine(StartVine());
            }
        }
        //눈보스
        if (bossType == BossType.SnowBoss)
        {
            for (int i = 0; i < IciclePos.Count; i++)
            {
                IcicleOnCheck.Add(false);
                IceWallNumOnCheck.Add(false);
            }

            StartCoroutine(IcicleCoroutine);            
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    _anim.SetBool("Crush", true);
        //    StartCoroutine(CrushAttack());
        //    //Crush = true;
        //}
        //if(Input.GetKeyDown(KeyCode.L))
        //{
        //    Crush = false;
        //    _anim.SetBool("Crush", false);
        //}
        if (bossType == BossType.NaturalBoss)
        {
            if (!VineStartCheck)
            {
                StartCoroutine(StartVine());
            }
            if (VinePatternCheck)
            {
                for (int i = 0; i < 32; i++)
                {
                    StartCoroutine(ThornVineAttack(i * 0.5f, i * 2));

                    VinePatternCheck = false;

                    //IEnumerator ThornVineAttack(float Time, int checkNum)
                    //{
                    //    yield return new WaitForSeconds(Time);
                    //    for (int i = checkNum; i < checkNum + 2; i++)
                    //    {
                    //        ThornVine[i].SetActive(true);
                    //    }
                    //}
                }
            }
            //if (myChar.Chapter >= 1)
            //{
            //    if (!VineStartCheck)
            //    {
            //        StartCoroutine(StartVine());
            //    }
            //    if (VinePatternCheck)
            //    {
            //        for (int i = 0; i < 20; i++)
            //        {
            //            StartCoroutine(ThornVineAttack(i * 0.5f, i * 2));
            //            if (i == 14)
            //            {
            //                VinePatternCheck = false;
            //            }
            //        }
            //    }
            //}
        }

        if (!StartCheck)
        {
            movementSpeed = 0;
            movementSpeed = instance.UnitDataMgr.GetTemplate(MonsterIndex).Speed;
            monsterHp = instance.UnitDataMgr.GetTemplate(MonsterIndex).HP * myChar.MultiHp * myChar.ThroneStatDataMgr.GetTemplate(myChar.ThroneIndex).ThroneStageHP;
            MaxHp = monsterHp;
            StartCheck = true;
        }
        if (!myChar.StartCheck)
        {
            myChar.TotalBossHp = MaxHp;
            myChar.CurrentBossHp = monsterHp;
            //if (bossType != BossType.NaturalBoss && bossType != BossType.NaturalBossDivision)
            //{
            //    myChar.TotalBossHp = MaxHp;
            //    myChar.CurrentBossHp = monsterHp;
            //}
            //else if (bossType == BossType.NaturalBoss)
            //{
            //    float divisionMidiumHp = instance.UnitDataMgr.GetTemplate(MonsterIndex + 1).HP * myChar.MultiHp * 5;
            //    float divisionSmallHp = instance.UnitDataMgr.GetTemplate(MonsterIndex + 2).HP * myChar.MultiHp * 5 * 2;
            //    myChar.TotalBossHp = MaxHp + divisionMidiumHp + divisionSmallHp;
            //    myChar.CurrentBossHp = monsterHp + divisionMidiumHp + divisionSmallHp;
            //}
            myChar.StartCheck = true;
        }

        if (bossType == BossType.FlameBoss)
        {
            if (monsterHp > 0)
            {
                EffectCheck();
                IndividualEffectCheck();
                Attackfire();
                movementPattern();
                FlipCheck();
                MovePoint.position = transform.position; // 해당 코드가없으면 벽에 막혔을때 무브포인트혼자 이동함
                if (myChar.Stun != 0)
                {
                    _anim.SetFloat("Stun", 1f);
                }
                else
                {
                    _anim.SetFloat("Stun", 0f);
                }
            }
            else
            {
                if (!ItemDropCheck)
                {
                    FlameBossSummonePos.SetActive(false);
                    DropTemplateMgr Drop = instance.DropDataMgr;
                    DropItem(Drop.GetTemplate(1).HeroMin, Drop.GetTemplate(1).HeroMax, Drop.GetTemplate(1).HeroPer, Drop.GetTemplate(2).HeroMin, Drop.GetTemplate(2).HeroMax, Drop.GetTemplate(2).HeroPer,
                         Drop.GetTemplate(3).HeroMin, Drop.GetTemplate(3).HeroMax, Drop.GetTemplate(3).HeroPer, Drop.GetTemplate(4).HeroMin, Drop.GetTemplate(4).HeroMax, Drop.GetTemplate(4).HeroPer, Drop.GetTemplate(5).HeroMin, Drop.GetTemplate(5).HeroMax, Drop.GetTemplate(5).HeroPer,
                         Drop.GetTemplate(6).HeroMin, Drop.GetTemplate(6).HeroMax, Drop.GetTemplate(6).HeroPer, Drop.GetTemplate(7).HeroMin, Drop.GetTemplate(7).HeroMax, Drop.GetTemplate(7).HeroPer);
                }
            }
        }
        else if (bossType == BossType.NaturalBoss)
        {
            if (monsterHp > 0)
            {
                EffectCheck();
                Attackfire();
                MovePoint.position = transform.position;    // 해당 코드가없으면 벽에 막혔을때 무브포인트혼자 이동함
                //RotationPattern();
                movementPattern();
                FlipCheck();
            }
            else if (monsterHp <= 0)
            {
                BossGroundCheck();
                if (RoomManager.Instance.BossRewardDropCheck)
                {
                    for (int i = 0; i < VineParent.Length; i++)
                    {
                        VineParent[i].SetActive(false);
                    }
                    
                }

                if (DeathGroundCheck && _rigidbody.velocity.y <= 0)
                {
                    _anim.SetBool("Ground", true);
                }
                if (!ItemDropCheck)
                {
                    DropTemplateMgr Drop = instance.DropDataMgr;
                    DropItem(Drop.GetTemplate(1).HeroMin, Drop.GetTemplate(1).HeroMax, Drop.GetTemplate(1).HeroPer, Drop.GetTemplate(2).HeroMin, Drop.GetTemplate(2).HeroMax, Drop.GetTemplate(2).HeroPer,
                    Drop.GetTemplate(3).HeroMin, Drop.GetTemplate(3).HeroMax, Drop.GetTemplate(3).HeroPer, Drop.GetTemplate(4).HeroMin, Drop.GetTemplate(4).HeroMax, Drop.GetTemplate(4).HeroPer, Drop.GetTemplate(5).HeroMin, Drop.GetTemplate(5).HeroMax, Drop.GetTemplate(5).HeroPer,
                    Drop.GetTemplate(6).HeroMin, Drop.GetTemplate(6).HeroMax, Drop.GetTemplate(6).HeroPer, Drop.GetTemplate(7).HeroMin, Drop.GetTemplate(7).HeroMax, Drop.GetTemplate(7).HeroPer);
                }
                //if (myChar.NatureBossAllDeath)
                //{
                //    if (!ItemDropCheck)
                //    {
                //        DropTemplateMgr Drop = instance.DropDataMgr;
                //        DropItem(Drop.GetTemplate(1).HeroMin, Drop.GetTemplate(1).HeroMax, Drop.GetTemplate(1).HeroPer, Drop.GetTemplate(2).HeroMin, Drop.GetTemplate(2).HeroMax, Drop.GetTemplate(2).HeroPer,
                //        Drop.GetTemplate(3).HeroMin, Drop.GetTemplate(3).HeroMax, Drop.GetTemplate(3).HeroPer, Drop.GetTemplate(4).HeroMin, Drop.GetTemplate(4).HeroMax, Drop.GetTemplate(4).HeroPer, Drop.GetTemplate(5).HeroMin, Drop.GetTemplate(5).HeroMax, Drop.GetTemplate(5).HeroPer,
                //        Drop.GetTemplate(6).HeroMin, Drop.GetTemplate(6).HeroMax, Drop.GetTemplate(6).HeroPer, Drop.GetTemplate(7).HeroMin, Drop.GetTemplate(7).HeroMax, Drop.GetTemplate(7).HeroPer);
                //    }
                //}

            }

        }
        else if (bossType == BossType.NaturalBossDivision)
        {
            if (transform.parent.GetComponent<Pathfinding.AIDestinationSetter>().monsterHp <= 0)
            {
                transform.parent.GetComponent<Pathfinding.AIPath>().canMove = false;
                //Destroy(gameObject.transform.parent.gameObject, 5f);
            }
        }
        else if (bossType == BossType.RockBoss)
        {
            if (monsterHp > 0)
            {
                EffectCheck();
                if (!EarthquakeCheck)
                {
                    Attackfire();
                    if (HeadCrushCheck)//rock보스 벽에 박고 돌아가는 스크립트
                    {
                        transform.Translate(HeadPos.normalized * 7.5f * Time.deltaTime);
                    }
                }
                else
                {
                    _anim.SetBool("Crush", true);
                    if (!Earthquake)
                    {
                        StartCoroutine(CrushAttack());
                        Earthquake = true;
                    }
                    if (Crush)
                    {
                        if (!HeadCrushCheck)
                        {
                            transform.parent.Find("Earthquake").GetChild(CrushNum).GetComponent<EarthquakeWallScript>().isEarthquake = true;
                            transform.Translate(transform.parent.Find("Earthquake").GetChild(CrushNum).transform.localPosition.normalized * 7.5f * Time.deltaTime);
                        }                       
                    }
                }
                FlipCheck();

                if (myChar.Stun == 0)
                {
                    transform.parent.gameObject.GetComponent<Animator>().SetFloat("Speed", myChar.Stun);
                }
                else if (myChar.SlowSpeed < 1)
                {
                    transform.parent.gameObject.GetComponent<Animator>().SetFloat("Speed", myChar.SlowSpeed);
                }
                else
                {
                    transform.parent.gameObject.GetComponent<Animator>().SetFloat("Speed", 1f);
                }
            }
            else
            {
                transform.parent.GetComponent<Animator>().enabled = false;
                transform.GetComponent<SpriteRenderer>().sortingLayerName = "BackGround";
                transform.GetComponent<CircleCollider2D>().radius = 0.1f;
                if (!ItemDropCheck)
                {
                    DropTemplateMgr Drop = instance.DropDataMgr;
                    DropItem(Drop.GetTemplate(1).HeroMin, Drop.GetTemplate(1).HeroMax, Drop.GetTemplate(1).HeroPer, Drop.GetTemplate(2).HeroMin, Drop.GetTemplate(2).HeroMax, Drop.GetTemplate(2).HeroPer,
                        Drop.GetTemplate(3).HeroMin, Drop.GetTemplate(3).HeroMax, Drop.GetTemplate(3).HeroPer, Drop.GetTemplate(4).HeroMin, Drop.GetTemplate(4).HeroMax, Drop.GetTemplate(4).HeroPer, Drop.GetTemplate(5).HeroMin, Drop.GetTemplate(5).HeroMax, Drop.GetTemplate(5).HeroPer,
                        Drop.GetTemplate(6).HeroMin, Drop.GetTemplate(6).HeroMax, Drop.GetTemplate(6).HeroPer, Drop.GetTemplate(7).HeroMin, Drop.GetTemplate(7).HeroMax, Drop.GetTemplate(7).HeroPer);
                }
            }
        }
        else if (bossType == BossType.SnowBoss)
        {
            if (monsterHp > 0)
            {
                EffectCheck();
                Horizontalmovement();
                HorizontalGroundCheck();
                if (YetiKingType == 0)
                {
                    Attackfire();
                }
                else if (YetiKingType == 1)
                {
                    YetiKingJump();
                }
                else if (YetiKingType == 2)
                {
                    YetiKingStormPattern();
                    StormSpell();
                }

                if (myChar.Stun != 0)
                {
                    _anim.SetFloat("Stun", 1f);
                }
                else
                {
                    _anim.SetFloat("Stun", 0f);
                }

                if (isTouchingWall)
                {
                    Flip();
                }
            }
            else
            {
                StopCoroutine(IcicleCoroutine);
                if (!ItemDropCheck)
                {
                    DropTemplateMgr Drop = instance.DropDataMgr;
                    DropItem(Drop.GetTemplate(1).HeroMin, Drop.GetTemplate(1).HeroMax, Drop.GetTemplate(1).HeroPer, Drop.GetTemplate(2).HeroMin, Drop.GetTemplate(2).HeroMax, Drop.GetTemplate(2).HeroPer,
                        Drop.GetTemplate(3).HeroMin, Drop.GetTemplate(3).HeroMax, Drop.GetTemplate(3).HeroPer, Drop.GetTemplate(4).HeroMin, Drop.GetTemplate(4).HeroMax, Drop.GetTemplate(4).HeroPer, Drop.GetTemplate(5).HeroMin, Drop.GetTemplate(5).HeroMax, Drop.GetTemplate(5).HeroPer,
                        Drop.GetTemplate(6).HeroMin, Drop.GetTemplate(6).HeroMax, Drop.GetTemplate(6).HeroPer, Drop.GetTemplate(7).HeroMin, Drop.GetTemplate(7).HeroMax, Drop.GetTemplate(7).HeroPer);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.collider.SendMessage("PlayerHit", instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage);
        }

    }

    private void BossGroundCheck()
    {
        DeathGroundCheck = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, 1 << LayerMask.NameToLayer("Ground"));
    }
    private void Attackfire()
    {
        if (fireCountdown <= 0)
        {
            AttackMotionCheck = false;

            _rigidbody.velocity = Vector2.zero;
            for (int i = 0; i < attackCount; i++)
            {
                if (myChar.Stun != 0)
                {
                    _anim.SetBool("Attack", true);
                }
            }
        }
        if (AttackMotionCheck)
        {
            if (bossType != BossType.FlameBoss)
            {
                if (myChar.Stun != 0)
                {
                    fireCountdown -= Time.deltaTime;
                }

            }
            else
            {
                if (!FadeCheck)
                {
                    if (myChar.Stun != 0)
                    {
                        fireCountdown -= Time.deltaTime;
                    }
                }
            }

        }
    }
    private void StormSpell()
    {
        if (StormCountingCheck > 0)
        {
            _rigidbody.velocity = Vector2.zero;
            AttackMotionCheck = false;
            if (myChar.Stun != 0)
            {
                _anim.SetBool("Storm", true);
            }
        }
    }
    private void attackEnd()
    {
        if (attackCountingCheck <= 1)
        {
            attackCountingCheck = attackCount;
            AttackMotionCheck = true;
            _anim.SetBool("Attack", false);
            YetiKingType = 1;

            JumpCnt--;
            if (JumpCnt == 0)
            {
                StopCoroutine(IcicleCoroutine);
            }
            //if (myChar.Chapter >= 1)
            //{
            //    JumpCnt--;
            //    if (JumpCnt == 0)
            //    {
            //        StopCoroutine(IcicleCoroutine);
            //    }
            //}
            //if (myChar.Chapter == 0)
            //{
            //    YetiKingType = 1;
            //}
            //else if (myChar.Chapter >=1)
            //{
            //    YetiKingType = 2;
            //}

            if (bossType != BossType.NaturalBoss)
            {
                fireCountdown = 3f / fireRate;
            }
            else if (bossType == BossType.NaturalBoss)
            {
                fireCountdown = 4f / fireRate;
            }

            if (bossType == BossType.FlameBoss)
            {
                StartCoroutine(SummonedMonsterCreate((int)GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P2));
                FadeCheck = true;

                //이전 단계별 보스공격패턴
                //if (myChar.Chapter >= 1)
                //{
                //    if (myChar.Chapter == 1)
                //    {
                //        if (AttackCnt % 2 == 0)
                //        {
                //            StartCoroutine(SummonedMonsterCreate((int)GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P2));
                //        }
                //    }
                //    else
                //    {
                //        StartCoroutine(SummonedMonsterCreate((int)GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P2));
                //    }

                //    AttackCnt++;

                //}
                //if (myChar.Chapter >= 2)
                //{
                //    FadeCheck = true;
                //}
            }
            if (bossType == BossType.RockBoss)
            {
                EarthquakeCheck = true;
                //if (myChar.Chapter >= 1)
                //{
                //    EarthquakeCheck = true;
                //}

            }

        }
        else
        {
            --attackCountingCheck;
        }
    }

    private void StormEnd()
    {
        if (StormCountingCheck <= 1)
        {
            StormCountingCheck = StormCount;
            _anim.SetBool("Storm", false);
            AttackMotionCheck = true;

            FrozenStormCnt--;

            YetiKingType = 1;
        }
        else
        {
            --StormCountingCheck;
        }
    }

    public void AttackHit(float Damage)
    {
        if (monsterHp > 0)
        {
            if (monsterHp > Damage)
            {
                myChar.CurrentBossHp -= Damage;
            }
            else
            {
                myChar.CurrentBossHp -= monsterHp;
            }
        }

        monsterHp -= Damage;

        _shader.EnableKeyword("HITEFFECT_ON");
        Invoke("HitEffectEnd", 0.1f);
        if (monsterHp <= 0)
        {
            _anim.SetBool("Death", true);
            _rigidbody.velocity = Vector2.zero;
            instance.StageClear();
            gameObject.layer = 0;
            //if (bossType == BossType.NaturalBoss)
            //{
            //    if (!DivisionMinuCheck)
            //    {
            //        myChar.ForestBossTotalMonsterCheck--;
            //        DivisionMinuCheck = true;
            //    }
            //}
            if (bossType == BossType.RockBoss)
            {
                transform.parent.GetComponent<Animator>().SetBool("Death", true);
                transform.parent.GetComponent<Animator>().enabled = false;
                transform.GetComponent<CircleCollider2D>().radius = 0.1f;
            }

        }
    }
    private void EarthGuardianCrush()
    {
        transform.parent.GetComponent<Animator>().enabled = false;
        if (myChar.Stun != 0)
        {
            _anim.SetBool("Crush", true);
        }
    }
    private void YetiKingJump()
    {
        _anim.SetBool("Jump", true);
        JumpAttackCheck = true;
    }
    private void YetiKingJumpEnd()
    {
        _anim.SetBool("Jump", false);
        JumpAttackCheck = false;
        myChar.YetiKingIcicleCheck = false;
        if (JumpCnt > 0)
        {
            YetiKingType = 0;
        }
        else if (JumpCnt == 0)
        {
            if (FrozenStormCnt == 0)
            {
                YetiKingType = 0;
                FrozenStormCnt = 3;
                JumpCnt = 2;
                IcicleOnCheck.Clear();
                for (int i = 0; i < IciclePos.Count; i++)
                {
                    IcicleOnCheck.Add(false);
                }
                isIciclePosCheck();
                StartCoroutine(IcicleCoroutine);
            }
            else
            {
                YetiKingType = 2;
            }
        }
        //if (myChar.Chapter == 0)
        //{
        //    YetiKingType = 0;
        //}
        //else if (myChar.Chapter >= 1)
        //{
        //    if (JumpCnt > 0)
        //    {
        //        YetiKingType = 0;
        //    }
        //    else if (JumpCnt == 0)
        //    {
        //        if (FrozenStormCnt == 0)
        //        {
        //            YetiKingType = 0;
        //            FrozenStormCnt = 3;
        //            JumpCnt = 2;
        //            IcicleOnCheck.Clear();
        //            for (int i = 0; i < IciclePos.Count; i++)
        //            {
        //                IcicleOnCheck.Add(false);
        //            }
        //            isIciclePosCheck();
        //            StartCoroutine(IcicleCoroutine);                    
        //        }
        //        else
        //        {
        //            YetiKingType = 2;
        //        }
        //    }
        //}

        //if (myChar.Chapter >= 1)
        //{
        //    if (FrozenStormCnt == 0)
        //    {
        //        YetiKingType = 0;
        //        FrozenStormCnt = 2;
        //    }
        //    else
        //    {
        //        YetiKingType = 2;
        //    }
        //}
        //else
        //{
        //    YetiKingType = 0;
        //}



        //if (myChar.Chapter < 1)
        //{

        //}
        //else if (myChar.Chapter >= 1)
        //{
        //    YetiKingType = 2;
        //}

    }
    private void YetiKingIcicleTrue()
    {
        myChar.YetiKingIcicleCheck = true;
        IcicleOnCheck.Clear();
        for (int i = 0; i < IciclePos.Count; i++)
        {
            IcicleOnCheck.Add(false);
        }

        if (FrozenStormCnt < 2)
        {
            IceWallNumOnCheck.Clear();
            for (int i = 0; i < IceWallPos.Count; i++)
            {
                IceWallNumOnCheck.Add(false);
            }
            for (int i = 0; i < 5; i++)
            {
                isIceWallPosCheck();
                int randomPos = Random.Range(0, IceWallNum.Count);
                GameObject Wall = Instantiate(IceWall, IceWallPos[IceWallNum[randomPos]].transform.position, Quaternion.identity);
                Wall.transform.parent = IceWallPos[IceWallNum[randomPos]].transform;
                Wall.transform.parent = transform.parent.parent.parent.Find("BackGround");
                //Wall.transform.localPosition = new Vector2(0, 0.05f);
                Wall.GetComponent<MonsterYetiKingWall>().ThawTime = 2.5f;
                IceWallNumOnCheck[IceWallNum[randomPos]] = true;
                isIceWallPosCheck();
            }
        }
        //if (myChar.Chapter == 1)
        //{
        //    if (FrozenStormCnt < 3)
        //    {
        //        IceWallNumOnCheck.Clear();
        //        for (int i = 0; i < IceWallPos.Count; i++)
        //        {
        //            IceWallNumOnCheck.Add(false);
        //        }
        //        for (int i = 0; i < 5; i++)
        //        {
        //            isIceWallPosCheck();
        //            int randomPos = Random.Range(0, IceWallNum.Count);
        //            GameObject Wall = Instantiate(IceWall, IceWallPos[IceWallNum[randomPos]].transform.position, Quaternion.identity);
        //            Wall.transform.parent = transform.parent.parent.parent.Find("BackGround");
        //            Wall.GetComponent<MonsterYetiKingWall>().ThawTime = 2.5f;
        //            IceWallNumOnCheck[IceWallNum[randomPos]] = true;
        //            isIceWallPosCheck();
        //        }
        //    }
        //}
        //else if (myChar.Chapter == 2)
        //{
        //    if (FrozenStormCnt < 2)
        //    {
        //        IceWallNumOnCheck.Clear();
        //        for (int i = 0; i < IceWallPos.Count; i++)
        //        {
        //            IceWallNumOnCheck.Add(false);
        //        }
        //        for (int i = 0; i < 5; i++)
        //        {
        //            isIceWallPosCheck();
        //            int randomPos = Random.Range(0, IceWallNum.Count);
        //            GameObject Wall = Instantiate(IceWall, IceWallPos[IceWallNum[randomPos]].transform.position, Quaternion.identity);
        //            Wall.transform.parent = transform.parent.parent.parent.Find("BackGround");
        //            Wall.GetComponent<MonsterYetiKingWall>().ThawTime = 2.5f;
        //            IceWallNumOnCheck[IceWallNum[randomPos]] = true;
        //            isIceWallPosCheck();
        //        }
        //    }
        //}
    }
    private void isIceWallPosCheck()
    {
        for (int i = 0; i < IceWallNumOnCheck.Count; i++)
        {
            if (i == 0)
            {
                EmptyIceWallPosNum = 0;
            }
            if (!IceWallNumOnCheck[i])
            {
                EmptyIceWallPosNum++;
            }
        }
        if (EmptyIceWallPosNum != IceWallNum.Count)
        {
            IceWallNum.Clear();
            for (int i = 0; i < IceWallNumOnCheck.Count; i++)
            {
                if (!IceWallNumOnCheck[i])
                {
                    IceWallNum.Add(i);
                }
            }
        }
    }
    private void YetiKingStormPattern()
    {
        if (StormCountingCheck >= 1)
        {
            _anim.SetBool("Storm", true);
        }
        

    }
    private void YetiKingStorm()
    {
        for (int j = 0; j < 2; j++)
        {
            isIciclePosCheck();
            int randomPos = Random.Range(0, IcicleNum.Count);
            GameObject icicle = Instantiate(Icicle, IciclePos[IcicleNum[randomPos]].transform.position, Quaternion.identity);
            icicle.transform.parent = transform.parent.parent.parent.Find("BackGround");
            IcicleOnCheck[IcicleNum[randomPos]] = true;
            SoundManager.Instance.PlaySfx(69);
            isIciclePosCheck();
        }
    }

    private void isIciclePosCheck()
    {
        for (int i = 0; i < IcicleOnCheck.Count; i++)
        {
            if (i == 0)
            {
                EmptyIciclePosNum = 0;
            }
            if (!IcicleOnCheck[i])
            {
                EmptyIciclePosNum++;
            }
        }
        if (EmptyIciclePosNum != IcicleNum.Count)
        {
            IcicleNum.Clear();
            for (int i = 0; i < IcicleOnCheck.Count; i++)
            {
                if (!IcicleOnCheck[i])
                {
                    IcicleNum.Add(i);
                }
            }
        }
    }
    
    private void movementPattern()
    {
        int wayPointCheck = wayPoint.Count;
        if (movingCheck)
        {
            NextWayNum = Random.Range(0, wayPointCheck);
            if (NextWayNum != PreWayNum)
            {
                PreWayNum = NextWayNum;
                movingCheck = false;
            }

        }
        else if (!movingCheck)
        {
            Vector3 distance = wayPoint[NextWayNum].transform.position;

            Vector3 dir = distance - MovePoint.position;


            //transform.up = dir.normalized;            
            if (AttackMotionCheck)
            {
                if (MoveMotionCheck)
                {
                    tf.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * myChar.Stun * Time.deltaTime);
                }
            }

        }
        if (bossType == BossType.FlameBoss)
        {
            if (FadeCheck)
            {
                if (!FadeOut)
                {
                    StartCoroutine(PadeOut());
                    _collider.enabled = false;
                    FadeOut = true;
                }
                else
                {
                    if (_sprite.color.a <= 0)
                    {
                        if (!FadeIn)
                        {
                            if (!Teleport)
                            {
                                float RandomX = Random.Range(-0.6f, 0.7f);
                                float RandomY = Random.Range(-0.6f, 0.8f);
                                transform.localPosition = new Vector3(RandomX, RandomY, 0);
                                Teleport = true;
                            }
                            MoveMotionCheck = false;
                            transform.parent.Find("PadeIn_Circle").GetComponent<MagicCircleScript>()._anim.enabled = true;
                            FadeIn = true;
                            SFXBossSound(57);
                        }
                    }
                }
            }
            else
            {
                MoveMotionCheck = true;
            }
        }

    }
    private void RotationPattern()
    {
        if (movingCheck)
        {
            int NextWayNumCheck = Random.Range(0, 2);

            if (NextWayNumCheck == 0)
            {
                //if (NextWayNum < 3)
                //{
                //    NextWayNum++;
                //}
                //else if (NextWayNum == 3)
                //{
                //    NextWayNum = 0;
                //}                
                target = wayPoint[NextWayNum].transform;
            }
            else if (NextWayNumCheck == 1)
            {
                //if (NextWayNum > 0)
                //{
                //    NextWayNum--;
                //}
                //else if (NextWayNum == 0)
                //{
                //    NextWayNum = 3;
                //}
                target = wayPoint[NextWayNum].transform;
            }
            movingCheck = false;
        }
        if (!movingCheck)
        {
            Vector3 dir = target.position - MovePoint.position;

            transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * myChar.Stun * Time.deltaTime);
        }
    }

    private void DivisionPattern()
    {
        if (MiddleDivisionCheck)
        {
            StartCoroutine(DivisionMonsterCreate());
        }
        else
        {
            for (int i = 0; i < DivisionCount; i++)
            {
                GameObject DivisionMonster = Instantiate(SummonedMonster, transform.position, Quaternion.identity);

                DivisionMonster.transform.parent = transform.parent.parent;
                DivisionMonster.GetComponent<Pathfinding.AIDestinationSetter>().Speed = Random.Range(2f, 3f);
                DivisionMonster.transform.localScale = new Vector2(1, 1);
                if (MiddleDivisionCheck)
                {
                    int RanNum = Random.Range(0, wayPoint.Count);
                    DivisionMonster.transform.position = wayPoint[RanNum].transform.position;
                    wayPoint.RemoveAt(RanNum);
                }

                if (!MiddleDivisionCheck)
                {
                    if (i == 0)
                    {
                        DivisionMonster.transform.position += new Vector3(0.4f, 0.4f, 0);
                    }
                    else if (i == 1)
                    {
                        DivisionMonster.transform.position += new Vector3(-0.4f, -0.4f, 0);
                    }
                    else if (i == 2)
                    {
                        DivisionMonster.transform.position += new Vector3(0.15f, 0.15f, 0);
                    }
                }
            }
        }
    }
    private void Horizontalmovement()
    {
        if (AttackMotionCheck && !JumpAttackCheck)
        {
            //_rigidbody.velocity = new Vector2(10f, _rigidbody.velocity.y);
            transform.Translate(Vector2.right * SlowNum * myChar.SlowSpeed * movementSpeed * myChar.Stun * Time.deltaTime);
        }

    }
    private void FlipCheck()
    {
        GameObject Player = GameObject.Find("Player");
        float FlipPos;
        for (int i = 0; i < myChar.HeroNum; i++)
        {
            if (Player.transform.GetChild(i).gameObject.activeSelf == true)
            {
                if (Player.transform.GetChild(i).name != "SeasnalShield")
                {
                    FlipPos = Player.transform.GetChild(i).position.x - transform.position.x;
                    if (FlipPos >= 0.01f)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else if (FlipPos <= -0.01f)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                }

            }
        }
    }

    private void Flip()
    {
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        movementSpeed *= -1;

        wallCheckDistance *= -1;

        isRightDir = !isRightDir;
    }
    private void WarrningCheck()
    {
        Color color = EmergencyCheck.GetComponent<SpriteRenderer>().color;
        //yield return new WaitForSeconds(0.3f);
        color.a = 1f;
        _sprite.color = color;
    }
    public void FadeInColor()
    {
        Color color = _sprite.color;
        //yield return new WaitForSeconds(0.3f);
        color.a = 1f;
        _sprite.color = color;
    }
    public void ColliderOnCheck()
    {
        _collider.enabled = true;
        FadeCheck = false;
        FadeOut = false;
        FadeIn = false;
    }
    private void HorizontalGroundCheck()
    {
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, _layerMask);
    }

    //private void WayPointDestinationCheck()
    //{
    //    if (MovePoint.position == wayPoint[NextWayNum].transform.position)
    //    {
    //        movingCheck = true;
    //    }

    //}
    private void EffectCheck()
    {
        if (myChar.Stun == 0)
        {
            Effect.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (myChar.Stun != 0)
        {
            Effect.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (myChar.SlowSpeed == 1 && SlowNum == 1)
        {
            Effect.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (myChar.SlowSpeed < 1 || SlowNum < 1)
        {
            Effect.transform.GetChild(0).gameObject.SetActive(true);
        }
        //if (myChar.SlowSpeed < 1)
        //{
        //    Effect.transform.GetChild(0).gameObject.SetActive(true);
        //}
        //else if (myChar.SlowSpeed == 1)
        //{
        //    Effect.transform.GetChild(0).gameObject.SetActive(false);
        //}
    }

    private void IndividualEffectCheck()
    {
        //if (myChar.Stun == 0)
        //{
        //    Effect.transform.GetChild(1).gameObject.SetActive(true);
        //}
        //else if (myChar.Stun != 0)
        //{
        //    Effect.transform.GetChild(1).gameObject.SetActive(false);
        //}

        if (SlowNum < 1)
        {
            Effect.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (SlowNum == 1)
        {
            Effect.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private void BossDestroySec(float TimeCnt)
    {
        Destroy(gameObject.transform.parent.gameObject, TimeCnt);
    }

    public void IndividualSlowCheck(float Time)
    {
        StartCoroutine(SlowActive(Time));
    }

    private void DropItem(int GoldMin, int GoldMax, float GoldPer, int DiaMin, int DiaMax, float DiaPer, int HeartMin, int HeartMax, float HeartPer,
        int ShieldMin, int ShieldMax, float ShieldPer, int StoneMin, int StoneMax, float StonePer,int SoulMin, int SoulMax, float SoulPer, int HeroMin, int HeroMax, float HeroPer)
    {
        //룰렛 코인 드랍
        //if (CoinPer > 0)
        //{
        //    int Coin = Random.Range(0, 100);
        //    if (Coin < (CoinPer + myChar.Luck))
        //    {
        //        int ItemNum = Random.Range(CoinMin, CoinMax);
        //        for (int i = 0; i < ItemNum; i++)
        //        {
        //            GameObject Item = Instantiate(instance.DropItem[0], DropPos.position, Quaternion.identity);
        //            Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
        //            Item.transform.parent = transform.parent.parent;
        //            Item.transform.localScale = new Vector2(6, 6);
        //        }
        //    }
        //}


        //골드 드랍
        if (GoldPer > 0)
        {
            int Gold = Random.Range(0, 100);
            if (Gold < (int)(GoldPer + (GoldPer * myChar.Luck)))
            {
                int ItemNum = Random.Range(GoldMin, GoldMax);
                for (int i = 0; i < ItemNum; i++)
                {
                    GameObject Item = Instantiate(instance.DropItem[1], DropPos.position, Quaternion.identity);
                    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector2(1, 1);
                }
            }
        }


        //다이아 드랍
        if (DiaPer > 0)
        {
            int Dia = Random.Range(0, 100);
            if (Dia < (int)(DiaPer + (DiaPer * myChar.Luck)))
            {
                int ItemNum = Random.Range(DiaMin, DiaMax);
                for (int i = 0; i < ItemNum; i++)
                {
                    GameObject Item = Instantiate(instance.DropItem[2], DropPos.position, Quaternion.identity);
                    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector2(1, 1);
                }
            }
        }


        //하트 드랍
        if (HeartPer > 0)
        {
            int Heart = Random.Range(0, 100);
            if (Heart < (int)(HeartPer + (HeartPer * myChar.Luck)))
            {
                int ItemNum = Random.Range(HeartMin, HeartMax);
                for (int i = 0; i < ItemNum; i++)
                {
                    GameObject Item = Instantiate(instance.DropItem[3], DropPos.position, Quaternion.identity);
                    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector2(1, 1);
                }
            }
        }


        //쉴드 드랍
        if (ShieldPer > 0)
        {
            int Shield = Random.Range(0, 100);
            if (Shield < (int)(ShieldPer + (ShieldPer * myChar.Luck)))
            {
                int ItemNum = Random.Range(ShieldMin, ShieldMax);
                for (int i = 0; i < ItemNum; i++)
                {
                    GameObject Item = Instantiate(instance.DropItem[4], DropPos.position, Quaternion.identity);
                    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector2(1, 1);
                }
            }
        }


        //속성석 드랍
        if (StonePer > 0)
        {
            int Stone = Random.Range(0, 100);
            if (Stone < (int)(StonePer + (StonePer * myChar.Luck)))
            {
                int ItemNum = Random.Range(StoneMin, StoneMax);
                for (int i = 0; i < ItemNum; i++)
                {
                    int ElementalStoneNum = Random.Range(0, instance.ElementalStone.Count);
                    GameObject Item = Instantiate(instance.ElementalStone[ElementalStoneNum], DropPos.position, Quaternion.identity);
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector3(6f, 6f);
                }
            }
        }


        //사용아이템 드랍
        //if (ActivePer > 0)
        //{
        //    int Active = Random.Range(0, 100);
        //    if (Active < (ActivePer + myChar.Luck))
        //    {
        //        int ItemNum = Random.Range(ActiveMin, ActiveMax);
        //        for (int i = 0; i < ItemNum; i++)
        //        {
        //            int ActiveItemNum = Random.Range(0, instance.ActiveItem.Count);
        //            GameObject Item = Instantiate(instance.ActiveItem[ActiveItemNum], DropPos.position, Quaternion.identity);
        //            Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
        //            Item.transform.parent = transform.parent.parent;
        //            //Item.transform.localScale = new Vector2(1, 1);
        //        }
        //    }
        //}

        //소울플레임 드랍
        if (SoulPer > 0)
        {
            int Soul = Random.Range(0, 100);
            if (Soul < (int)(SoulPer + (SoulPer * myChar.Luck)))
            {
                int ItemNum = Random.Range(SoulMin, SoulMax);
                for (int i = 0; i < ItemNum; i++)
                {
                    GameObject Item = Instantiate(instance.DropItem[5], DropPos.position, Quaternion.identity);
                    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector2(1, 1);
                }
            }
        }

        //히어로 토큰 드랍
        if (HeroPer > 0)
        {
            int Hero = Random.Range(0, 100);
            if (Hero < (int)(HeroPer + (HeroPer * myChar.Luck)))
            {
                int ItemNum = Random.Range(HeroMin, HeroMax);
                for (int i = 0; i < ItemNum; i++)
                {
                    GameObject Item = Instantiate(instance.DropItem[6], DropPos.position, Quaternion.identity);
                    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector2(1, 1);
                }
            }
        }
        ItemDropCheck = true;
    }
    private void OnDrawGizmos()
    {
        if (bossType == BossType.NaturalBoss)
        {
            Gizmos.DrawWireSphere(GroundCheck.position, 0.2f);
        }

        if (bossType == BossType.SnowBoss)
        {
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        }
    }
    
    IEnumerator YetiKingIcicle()
    {
        while (true)
        {
            isIciclePosCheck();
            int randomPos = Random.Range(0, IcicleNum.Count);
            yield return new WaitForSeconds(2.5f);
            GameObject icicle = Instantiate(Icicle, IciclePos[IcicleNum[randomPos]].transform.position, Quaternion.identity);
            icicle.transform.parent = transform.parent.parent.parent.Find("BackGround");
            IcicleOnCheck[IcicleNum[randomPos]] = true;
            SoundManager.Instance.PlaySfx(69);
            isIciclePosCheck();
        }
    }
    IEnumerator DivisionCreate()
    {
        int RanNum = Random.Range(0, wayPoint.Count);
        GameObject Portal = Instantiate(RecallPortal, transform.position, Quaternion.identity);
        SFXBossSound(59);
        Portal.transform.parent = transform.parent.parent;
        Portal.transform.localScale = new Vector2(1, 1);
        Portal.transform.position = wayPoint[RanNum].transform.position;
        Vector3 CreatePos = wayPoint[RanNum].transform.position;
        wayPoint.RemoveAt(RanNum);
        yield return new WaitForSeconds(2f);
        Destroy(Portal);
        GameObject DivisionMonster = Instantiate(SummonedMonster, CreatePos, Quaternion.identity);

        DivisionMonster.transform.parent = transform.parent.parent;
        //DivisionMonster.GetComponent<Pathfinding.AIDestinationSetter>().Speed = Random.Range(2f, 3f);
        DivisionMonster.transform.localScale = new Vector2(1, 1);
        //if (MiddleDivisionCheck)
        //{
        //    DivisionMonster.transform.position = wayPoint[RanNum].transform.position;
        //    wayPoint.RemoveAt(RanNum);
        //}
    }

    IEnumerator DivisionMonsterCreate()
    {
        int i = 5;
        while (i != 0)
        {
            StartCoroutine(DivisionCreate());
            i--;
            yield return new WaitForSeconds(2f);

        }
    }
    IEnumerator SummonedCreate()
    {
        if (GameManager.Instance.SelectCharacter.GetComponent<PlayerTargeting>().MonsterList.Count <= 6)
        {
            float RandomX = Random.Range(-3.25f, 5.3f);
            float RandomY = Random.Range(-0.25f, 8.25f);
            //int RanNum = Random.Range(0, wayPoint.Count);
            GameObject Portal = Instantiate(RecallPortal, transform.position, Quaternion.identity);
            Portal.transform.parent = FlameBossSummonePos.transform;
            SFXBossSound(56);
            //Portal.transform.parent = transform.parent.parent;
            Portal.transform.localScale = new Vector2(1, 1);
            Portal.transform.localPosition = new Vector3(RandomX, RandomY, 0);
            yield return new WaitForSeconds(2f);
            Destroy(Portal);
            GameObject Monster = Instantiate(SummonedMonster, transform.position, Quaternion.identity);
            Monster.transform.parent = FlameBossSummonePos.transform;
            //Monster.transform.parent = transform.parent.parent;
            Monster.transform.localScale = new Vector2(1, 1);
            Monster.transform.localPosition = new Vector3(RandomX, RandomY, 0);
        }
    }

    IEnumerator SummonedMonsterCreate(int SummonedCnt)
    {
        int i = SummonedCnt;
        while (i != 0)
        {
            StartCoroutine(SummonedCreate());
            i--;
            yield return new WaitForSeconds(2f);

        }        
    }
    //IEnumerator BossKillTimeScale()
    //{
    //    Time.timeScale = 0.3f;
    //    SoundManager.Instance.audioSource.pitch = 0.3f;

    //    while (Time.timeScale < 1)
    //    {
    //        yield return new WaitForSeconds(0.3f);
    //        Time.timeScale += 0.1f;
    //        SoundManager.Instance.audioSource.pitch += 0.1f;
    //        Debug.Log(SoundManager.Instance.audioSource);
    //    }       
    //}
    IEnumerator PadeOut()
    {
        yield return new WaitForSeconds(0f);
        while (_sprite.color.a > 0)
        {
            Color color = _sprite.color;
            //yield return new WaitForSeconds(0.3f);
            color.a -= 1f * Time.deltaTime;
            _sprite.color = color;
            yield return null;
        }
    }

    //숲보스 벽송곳 패턴 회전해주는 곳
    IEnumerator StartVine()
    {
        VineStartCheck = true;
        VineType = Random.Range(0, 4);
        yield return new WaitForSeconds(5f);
        VineParent[VineType].SetActive(true);
        //switch (VineType)
        //{
        //    case 0:
        //        VineParent[VineType].SetActive(true);
        //        break;
        //    case 1:
        //        VineParent[1].transform.rotation = Quaternion.Euler(0, 0, (int)90f);
        //        VineParent[1].transform.localPosition = new Vector3(-0.008f, 0.0025f, 0);
        //        break;
        //    case 2:
        //        VineParent[2].transform.rotation = Quaternion.Euler(0, 0, (int)180f);
        //        VineParent[2].transform.localPosition = new Vector3(-0.01f, -0.65f, 0);
        //        break;
        //    case 3:
        //        VineParent[3].transform.rotation = Quaternion.Euler(0, 0, (int)270f);
        //        VineParent[3].transform.localPosition = new Vector3(-0.002f, -0.0085f, 0);
        //        break;
        //}
        StartCoroutine(VinePattern());
    }
    //숲보스 벽송곳 패턴 나오기전에 알려주는 곳 WaitForSeconds 후 송곳노출됨
    IEnumerator VinePattern()
    {
        VineParent[VineType].transform.Find("1st").GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(2f);
        VineParent[VineType].transform.Find("1st").GetComponent<SpriteRenderer>().enabled = false;
        VinePatternCheck = true;
        //EmergencyCheck.GetComponent<SpriteRenderer>().enabled = true;
        //yield return new WaitForSeconds(2f);
        //EmergencyCheck.GetComponent<SpriteRenderer>().enabled = false;
        //VinePatternCheck = true;
    }
    //숲보스 벽송곳 공격 패턴 for문이 순식간에 돌기때문에 Time로 시간간격으로 노출되게하고 CheckNum으로 2의배수로 단계별로 노출되게함
    IEnumerator ThornVineAttack(float Time, int checkNum)
    {
        yield return new WaitForSeconds(Time);
        for (int i = checkNum; i < checkNum + 2; i++)
        {
            VineArrays[VineType].ThornVine[i].SetActive(true);
        }
    }
    IEnumerator CrushAttack()
    {
        CrushNum = Random.Range(0, 3);
        transform.parent.GetComponent<Animator>().enabled = false;
        transform.parent.Find("Earthquake").GetChild(CrushNum).GetComponent<SpriteRenderer>().enabled = true;
        transform.parent.Find("Earthquake").GetChild(CrushNum).GetComponent<EarthquakeWallScript>().isEarthquake = true;
        yield return new WaitForSeconds(2f);
        StopPos = transform.localPosition;
        Crush = true;
        transform.parent.Find("Earthquake").GetChild(CrushNum).GetComponent<SpriteRenderer>().enabled = false;
    }

    //IEnumerator PadeInTeleport()
    //{
    //    float RandomX = Random.Range(-0.6f, 0.7f);
    //    float RandomY = Random.Range(-0.6f, 0.8f);
    //    MoveMotionCheck = false;

    //    transform.localPosition = new Vector3(RandomX, RandomY, 0);

    //    yield return new WaitForSeconds(0.5f);
    //    while (_sprite.color.a < 1)
    //    {
    //        Color color = _sprite.color;
    //        //yield return new WaitForSeconds(0.3f);
    //        color.a += 1f * Time.deltaTime;
    //        _sprite.color = color;
    //        if (_sprite.color.a >= 1)
    //        {
    //            GetComponent<MonsterTargeting>().FadeShot(GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P3);

    //            _collider.enabled = true;
    //            FadeCheck = false;
    //            FadeOut = false;
    //            FadeIn = false;
    //        }
    //        yield return null;
    //    }
    //}
    IEnumerator SlowActive(float Time)              //슬로우아이템
    {
        SlowNum = 0.5f;
        yield return new WaitForSeconds(Time);
        SlowNum = 1f;
    }
    private void HitEffectEnd()
    {
        _shader.DisableKeyword("HITEFFECT_ON");
    }

    public void BossSound()
    {
        SoundManager.Instance.PlaySfx(27);
    }
    public void SFXBossSound(int Num)
    {
        SoundManager.Instance.PlaySfx(Num);
    }
}
