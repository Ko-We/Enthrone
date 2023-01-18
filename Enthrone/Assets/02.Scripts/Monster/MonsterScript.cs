using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public enum MonsterType
    {
        SquareMoveRight,
        SquareMoveLeft,
        HorizontalMove,
        HorizontalLeftMove,
        JumpMove,
        Fixed,
        GiantSpider,
        AncientSlime,
        SendSlime,
        Lava_SpikeBeetle
    }
    public enum AttributeType
    {
        None,
        Flame,
        Forest,
        Rock,
        Snow
    }
    public int MonsterIndex;
    MyObject myChar;
    Material _shader;

    [SerializeField]
    private bool StatCheck = false;
    public bool EliteMonster;
    public bool MiddleBoss;
    public bool NoDropBoss;

    public MonsterType monsterType;
    public AttributeType attributeMonsterType;
    public Animator _anim;
    private Rigidbody2D _rigidbody;
    public LayerMask _layerMask;
    public GameObject HpbarCanvas;

    [SerializeField]
    private int rotationNum = 1;
    public int attackCount;
    private int attackCountingCheck;
    private int AngerCountingCheck;
    public int AngerCount;
    private int speed = 1; //회전 몬스터스피드 2가 넘어가면 속도를 주체못함 비교문새로짜야됨
    [SerializeField]
    private int AngerCnt;       //라바골렘 분노누적데미지 체크

    public float monsterHp = 5f;
    public float wallCheckDistance;
    public float fireCountdown = 0f;
    public float preFireCount;
    public float fireRate = 1f;
    public float movementSpeed = 3f;
    private float PremovementSpeed;
    public float SlowNum = 1f;
    public float groundCheckRidus;
    [SerializeField]
    private float JumpPower;
    [SerializeField]
    private float JumpMove;
    [SerializeField]
    private float jumpTime = 1f;
    public int JumpCnt;
    private float MoveCountdown = 5f;
    private float CrouchCountdown = 2.5f;
    [SerializeField]
    private bool isRightDir = true;
    private bool isAngerCheck = false;      //챕터3일때 UnitDataMgr에서 라바골렘 분노누적데미지 받았는지 확인하는 함수
    [Header("MonsterType")]
    public bool HorizontalMoveMonster;
    public bool SquareMoveMonster;
    public bool JumpMoveMonster;
    public bool FixedMonster;
    public bool AttackType;
    public bool SnailMonster;
    public bool InvincibilityMonster;
    public bool Individual_Slow = false;
    public bool isItemDropPos;

    [Header("CheckType")]
    //public bool isStun = false;
    private bool isGround;
    [SerializeField]
    private bool endGrounded;
    private bool isTouchingWall;
    private bool isfrontWall;
    [SerializeField]
    private bool AttackMotionCheck = true;        //몬스터가 공격모션중인지 체크
    [SerializeField]
    private bool isDrop;
    [SerializeField]
    private bool AngerRun = false;
    private GameObject Effect;
    public GameObject MasterMonster;
    
    public Transform wallCheck;
    public Transform frontwallCheck;
    public Transform endCheckRight;
    public Transform itemDropPos;
    public Transform groundCheck;

    public List<GameObject> SummonedMonster = new List<GameObject>();
    public List<int> Type = new List<int>(); //스킬 리스트 관리

    public bool Page2, Page3;
    public bool TagUnchange;        //이게 없으면 스파이크비틀 몬스터가 죽고나서 태그가 deathmonster로 변경되면서 플레이어를 타격할 수 없음
    private IEnumerator AngerSpeedCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        myChar = MyObject.MyChar;
        _shader = GetComponent<Renderer>().material;
        _anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        attackCountingCheck = attackCount;
        AngerCountingCheck = AngerCount;
        if (monsterType == MonsterType.SquareMoveLeft || monsterType == MonsterType.HorizontalLeftMove)
        {
            Flip();
        }
    }
    private void Start()
    {
        Effect = gameObject.transform.Find("Effect").gameObject;
        AngerSpeedCoroutine = AngerSpeed(1f);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
        if (!StatCheck)
        {
            movementSpeed = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Speed;
            fireRate = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).ASPD;
            monsterHp = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).HP * myChar.MultiHp; //* myChar.MultiHp * myChar.ThroneStatDataMgr.GetTemplate(myChar.ThroneIndex).ThroneStageHP
            JumpPower = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Jump;
            JumpMove = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Speed;
            fireCountdown = fireRate;
            PremovementSpeed = movementSpeed;
            //monsterHp = 10000000000000000f;
            if (MiddleBoss)
            {
                HpbarCanvas.GetComponent<MonsterHpBar>().maxHp = monsterHp;
            }
            if (attributeMonsterType == AttributeType.Flame && EliteMonster)
            {
                //if (myChar.Chapter >= 2)
                //{
                //    AngerCnt = (int)GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P3;
                //}
            }
            StatCheck = true;
        }
        
        if (monsterHp > 0)
        {
            EffectCheck();
            //IndividualEffectCheck();
            MonsterStatusCheck();
            MonsterMove();
            Attackfire();
            //IndividualSlowCheck();
            if (SnailMonster)
            {
                CrouchCheck();
            }
            if (AttackType)
            {
                if (myChar.Stun != 0)
                {
                    _anim.SetFloat("Stun", 1f);
                }
                else
                {
                    _anim.SetFloat("Stun", 0f);
                }
            }
        }
        if (MiddleBoss)
        {
            HpbarCanvas.GetComponent<MonsterHpBar>().currentHp = monsterHp;
        }
        if (MasterMonster)
        {
            if(MasterMonster.GetComponent<MonsterScript>().monsterHp <= 0)
            {
                _anim.SetBool("Death", true);
            }

        }

    }


    private void SquareGroundCheck()
    {
        endGrounded = Physics2D.OverlapCircle(endCheckRight.position, groundCheckRidus, _layerMask);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, groundCheckRidus, _layerMask);
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRidus, _layerMask);
    }
    private void HorizontalGroundCheck()
    {
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, _layerMask);
        endGrounded = Physics2D.OverlapCircle(endCheckRight.position, groundCheckRidus, _layerMask);
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRidus, _layerMask);
    }
    private void JumpWalldCheck()
    {
        endGrounded = Physics2D.OverlapBox(endCheckRight.position, new Vector2(groundCheckRidus + 0.3f, groundCheckRidus), 0, _layerMask);
        if (endGrounded)
        {
            isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, _layerMask);
            isfrontWall = Physics2D.Raycast(frontwallCheck.position, transform.right, wallCheckDistance, _layerMask);
        }
        
    }
    private void PageCheck()
    {
        //if (Page2)
        //{
        //    if (myChar.Chapter >= 1)
        //    {
        //        _anim.SetLayerWeight(1, 1);
        //    }
        //}
        //else if (Page3)
        //{
        //    if (myChar.Chapter >= 2)
        //    {
        //        _anim.SetLayerWeight(1, 1);

        //        if (attributeMonsterType == AttributeType.Flame && EliteMonster)
        //        {
        //            if (AngerCnt <= 0)
        //            {
        //                AngerAttackfire();
        //            }
        //        }
        //    }
        //}
        //if (myChar.Chapter == 0)
        //{
        //    if (_anim.layerCount > 1)
        //    {
        //        _anim.SetLayerWeight(1, 0);
        //    }            
        //}
    }

    private void MonsterStatusCheck()
    {
        //if (SquareMoveMonster)
        //{
        //    SquareGroundCheck();
        //}
        if (monsterType == MonsterType.SquareMoveRight || monsterType == MonsterType.SquareMoveLeft || monsterType == MonsterType.GiantSpider || monsterType == MonsterType.SendSlime || monsterType == MonsterType.Lava_SpikeBeetle)
        {
            SquareGroundCheck();
        }
        else if (HorizontalMoveMonster)
        {
            HorizontalGroundCheck();
            FlipCheck();
        }
        else if (JumpMoveMonster || monsterType == MonsterType.AncientSlime)
        {
            JumpWalldCheck();

            FlipCheck();
        }
        else if (FixedMonster)
        {
            FlipCheck();
        }

    }

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == "Player")
    //    {
    //        Debug.Log(22);
    //        col.SendMessage("PlayerHit", 1);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.collider.SendMessage("PlayerHit", GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage);
        }
    }
    public void AttackHit(float Damage)
    {
        _shader.EnableKeyword("HITEFFECT_ON");
        Invoke("HitEffectEnd", 0.1f);
        //SoundManager.Instance.PlaySfx(10);
        
        if (!SnailMonster && !InvincibilityMonster)
        {
            if (GameManager.Instance.PlayerSkill[14] >= 1)      //헤드샷
            {
                if (!MiddleBoss)
                {
                    if (Random.Range(0, 100) < myChar.HeadShootPer)
                    {
                        monsterHp = 0;
                    }
                }
            }
            monsterHp -= Damage;
            //if (attributeMonsterType == AttributeType.Flame && EliteMonster)    //라바골렘 전용
            //{
            //    if (!isAngerCheck)
            //    {
            //        if (AngerCnt > 0)
            //        {
            //            AngerCnt--;
            //        }
            //    }
            //}
            if (attributeMonsterType == AttributeType.Snow && !EliteMonster && HorizontalMoveMonster)       //화이트캣전용
            {
                if (MonsterIndex == 37)
                {
                    if (AngerRun)
                    {
                        StartCoroutine(AngerSpeedCoroutine);
                        AngerSpeedCoroutine = AngerSpeed(1f);
                        StartCoroutine(AngerSpeedCoroutine);
                    }
                    else
                    {
                        AngerSpeedCoroutine = AngerSpeed(1f);
                        StartCoroutine(AngerSpeedCoroutine);
                    }
                }
                

                //if (myChar.Chapter >= 1)
                //{
                //    if (AngerRun)
                //    {
                //        StartCoroutine(AngerSpeedCoroutine);
                //        AngerSpeedCoroutine = AngerSpeed(1f);
                //        StartCoroutine(AngerSpeedCoroutine);
                //    }
                //    else
                //    {
                //        AngerSpeedCoroutine = AngerSpeed(1f);
                //        StartCoroutine(AngerSpeedCoroutine);
                //    }
                //}                
            }

            if (monsterHp <= 0)
            {
                //Invoke("HitEffectEnd", 0.1f);
                
                ItemDrop();
                SoundManager.Instance.PlaySfx(9);
                if (GameManager.Instance.PlayerSkill[11] >= 1)
                {
                    if (myChar.currentHp < myChar.maxHp)
                    {
                        if (Random.Range(0, 100) < myChar.BloodPer)
                        {
                            myChar.currentHp += (int)Mathf.Ceil(myChar.maxHp * 0.01f);
                            SFXSound(43);
                        }
                    }

                }
                _anim.SetBool("Death", true);
                if (!TagUnchange)
                {
                    gameObject.layer = LayerMask.NameToLayer("DeathMonster");
                    gameObject.tag = "DeathMonster";
                }
                else if (TagUnchange)   //스파이크 비틀 전용 
                {
                    gameObject.layer = LayerMask.NameToLayer("Trap");
                    gameObject.tag = "Trap";
                    if (transform.gameObject.GetComponent<CapsuleCollider2D>())
                    {
                        transform.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                    }
                    if (transform.gameObject.GetComponent<BoxCollider2D>())
                    {
                        transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    }                    
                    _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                }
                _rigidbody.velocity = Vector2.zero;
                //PlayerTargeting.Instance.MonsterList.RemoveAt(0);
            }
        }
        else if (SnailMonster)
        {
            if (MoveCountdown > 0)
            {
                if (GameManager.Instance.PlayerSkill[14] >= 1)
                {
                    if (!MiddleBoss)
                    {
                        if (Random.Range(0, 100) < myChar.HeadShootPer)
                        {
                            monsterHp = 0;
                        }
                    }
                }
                monsterHp -= Damage;
                if (monsterHp <= 0)
                {
                    ItemDrop();
                    //SoundManager.Instance.PlaySfx(9);
                    if (GameManager.Instance.PlayerSkill[11] >= 1)
                    {
                        if (myChar.currentHp < myChar.maxHp)
                        {
                            if (Random.Range(0, 100) < myChar.BloodPer)
                            {
                                myChar.currentHp += (int)Mathf.Ceil(myChar.maxHp * 0.01f);
                                SFXSound(43);
                            }
                        }

                    }
                    _anim.SetBool("Death", true);
                    gameObject.layer = LayerMask.NameToLayer("DeathMonster");
                    gameObject.tag = "DeathMonster";
                    _rigidbody.velocity = Vector2.zero;
                    PlayerTargeting.Instance.MonsterList.RemoveAt(0);
                }
            }
        }   
        else if (InvincibilityMonster)
        {
            if (!_anim.GetBool("Invincibility"))
            {
                if (GameManager.Instance.PlayerSkill[14] >= 1)
                {
                    if (!MiddleBoss)
                    {
                        if (Random.Range(0, 100) < myChar.HeadShootPer)
                        {
                            monsterHp = 0;
                        }
                    }
                }
                monsterHp -= Damage;
                if (monsterHp <= 0)
                {
                    ItemDrop();
                    //SoundManager.Instance.PlaySfx(9);
                    if (GameManager.Instance.PlayerSkill[11] >= 1)
                    {
                        if (myChar.currentHp < myChar.maxHp)
                        {
                            if (Random.Range(0, 100) < myChar.BloodPer)
                            {
                                myChar.currentHp += (int)Mathf.Ceil(myChar.maxHp * 0.01f);
                                SFXSound(43);
                            }
                        }
                    }
                    _anim.SetBool("Death", true);
                    gameObject.layer = LayerMask.NameToLayer("DeathMonster");
                    gameObject.tag = "DeathMonster";
                    _rigidbody.velocity = Vector2.zero;
                    PlayerTargeting.Instance.MonsterList.RemoveAt(0);
                }
            }
        }
    }

    public void MonsterDestroy()
    {
        Destroy(gameObject, 3f);
    }
    private void WaitDestroyMonster()
    {
        Destroy(gameObject);

        //if (myChar.Chapter < 1)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject, GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P2);
        //}
        
    }

    private void MonsterMove()
    {
        if (monsterType == MonsterType.SquareMoveRight)
        {
            if (endGrounded)
            {
                transform.Translate(Vector2.right * movementSpeed * SlowNum * myChar.SlowSpeed * myChar.Stun * Time.deltaTime);
                if (isTouchingWall)
                {
                    movementSpeed = 0f;
                    if (isGround)
                    {
                        if (Mathf.Round(transform.eulerAngles.z) == 0)
                        {
                            RotateMonsterState(-0.4f, -0.5f, 90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == 90)
                        {
                            RotateMonsterState(0.5f, -0.4f, 90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == 180)
                        {
                            RotateMonsterState(0.4f, 0.5f, 90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == 270)
                        {
                            RotateMonsterState(-0.5f, 0.4f, 90f);
                        }
                    }
                    Invoke("MonsterSpeedManage", 0.2f);

                }
            }
            else if (!endGrounded)
            {
                movementSpeed = 0f;
                if (isGround)
                {
                    if (Mathf.Round(transform.eulerAngles.z) == 0 || Mathf.Round(transform.eulerAngles.z) == 360)
                    {
                        RotateMonsterState(1.2f, -1.25f, -90f);
                    }
                    else if (Mathf.Round(transform.eulerAngles.z) == -90 || Mathf.Round(transform.eulerAngles.z) == 270)
                    {
                        RotateMonsterState(-1.25f, -1.2f, -90f);
                    }
                    else if (Mathf.Round(transform.eulerAngles.z) == -180 || Mathf.Round(transform.eulerAngles.z) == 180)
                    {
                        RotateMonsterState(-1.2f, 1.25f, -90f);
                    }
                    else if (Mathf.Round(transform.eulerAngles.z) == -270 || Mathf.Round(transform.eulerAngles.z) == 90)
                    {
                        RotateMonsterState(1.25f, 1.2f, -90f);
                    }
                }
                Invoke("MonsterSpeedManage", 0.2f);
            }
        }
        else if (monsterType == MonsterType.SquareMoveLeft)
        {
            if (endGrounded)
            {
                transform.Translate(Vector2.right * -movementSpeed * SlowNum * myChar.SlowSpeed * myChar.Stun * Time.deltaTime);
                if (isTouchingWall)
                {
                    movementSpeed = 0f;
                    if (isGround)
                    {
                        if (Mathf.Round(transform.eulerAngles.z) == 0)
                        {
                            RotateMonsterState(0.4f, -0.5f, -90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == 270)
                        {
                            RotateMonsterState(-0.5f, -0.4f, -90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == 180)
                        {
                            RotateMonsterState(-0.4f, 0.5f, -90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == 90)
                        {
                            RotateMonsterState(0.5f, 0.4f, -90f);
                        }
                    }
                    Invoke("MonsterSpeedManage", 0.2f);
                }
            }
            else if (!endGrounded)
            {
                movementSpeed = 0f;
                if (isGround)
                {
                    if (Mathf.Round(transform.eulerAngles.z) == 0 || Mathf.Round(transform.eulerAngles.z) == 360)
                    {
                        RotateMonsterState(-1.2f, -1.25f, 90f);
                    }
                    else if (Mathf.Round(transform.eulerAngles.z) == -90 || Mathf.Round(transform.eulerAngles.z) == 270)
                    {
                        RotateMonsterState(-1.25f, 1.2f, 90f);
                    }
                    else if (Mathf.Round(transform.eulerAngles.z) == -180 || Mathf.Round(transform.eulerAngles.z) == 180)
                    {
                        RotateMonsterState(1.2f, 1.25f, 90f);
                    }
                    else if (Mathf.Round(transform.eulerAngles.z) == -270 || Mathf.Round(transform.eulerAngles.z) == 90)
                    {
                        RotateMonsterState(1.25f, -1.2f, 90f);
                    }
                }                
                Invoke("MonsterSpeedManage", 0.2f);
            }
            
        }
        else if (HorizontalMoveMonster)
        {
            if (AttackMotionCheck)
            {
                if (isGround)
                {
                    if (monsterType == MonsterType.HorizontalMove)
                    {
                        _rigidbody.velocity = new Vector2(movementSpeed * SlowNum * myChar.SlowSpeed * myChar.Stun, _rigidbody.velocity.y);
                    }
                    else if (monsterType == MonsterType.HorizontalLeftMove)
                    {
                        _rigidbody.velocity = new Vector2(-movementSpeed * SlowNum * myChar.SlowSpeed * myChar.Stun, _rigidbody.velocity.y);
                    }
                }
            }

        }
        else if (JumpMoveMonster)
        {
            if (endGrounded)
            {
                if (jumpTime <= 0)
                {
                    //transform.Translate(new Vector2(0.5f, 0.8f));
                    if (isRightDir)
                    {
                        _rigidbody.velocity = new Vector2(JumpMove, JumpPower);
                    }
                    else if (!isRightDir)
                    {
                        _rigidbody.velocity = new Vector2(-JumpMove, JumpPower);
                    }
                    isDrop = true;
                }
                else if (jumpTime == 1f)
                {
                    Invoke("MonsterSpeedZero", 0.3f);
                }
                //떯어질때 값

                if (myChar.Stun != 0)
                {
                    jumpTime -= Time.deltaTime;
                }

            }
            else if(!endGrounded)
            {
                jumpTime = 1f;
            }

            if (isDrop)
            {
                if (_rigidbody.velocity.y < 0)
                {
                    if (isRightDir)
                    {
                        if (isfrontWall)
                        {
                            _rigidbody.velocity = new Vector2(3f, 0f);
                        }

                    }
                    else if (!isRightDir)
                    {
                        if (isfrontWall)
                        {
                            _rigidbody.velocity = new Vector2(-3f, 0f);
                        }
                    }
                    isDrop = false;
                }
                
            }
        }
        else if (monsterType == MonsterType.GiantSpider)
        {
            if (AttackMotionCheck)
            {
                if (endGrounded)
                {
                    transform.Translate(Vector2.right * movementSpeed * SlowNum * myChar.SlowSpeed * myChar.Stun * Time.deltaTime);
                    if (isTouchingWall)
                    {
                        movementSpeed = 0f;
                        if (isGround)
                        {
                            if (Mathf.Round(transform.eulerAngles.z) == 0)
                            {
                                RotateMonsterState(0, -0, 90f);
                            }
                            else if (Mathf.Round(transform.eulerAngles.z) == 90)
                            {
                                RotateMonsterState(0, -0, 90f);
                            }
                            else if (Mathf.Round(transform.eulerAngles.z) == 180)
                            {
                                RotateMonsterState(0, 0, 90f);
                            }
                            else if (Mathf.Round(transform.eulerAngles.z) == 270)
                            {
                                RotateMonsterState(-0, 0, 90f);
                            }
                        }
                        Invoke("MonsterSpeedManage", 0.2f);

                    }
                }
                else if (!endGrounded)
                {
                    movementSpeed = 0f;
                    if (isGround)
                    {
                        if (Mathf.Round(transform.eulerAngles.z) == 0 || Mathf.Round(transform.eulerAngles.z) == 360)
                        {
                            RotateMonsterState(2.1f, -2.5f, -90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == -90 || Mathf.Round(transform.eulerAngles.z) == 270)
                        {
                            RotateMonsterState(-2.5f, -2.1f, -90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == -180 || Mathf.Round(transform.eulerAngles.z) == 180)
                        {
                            RotateMonsterState(-2.1f, 2.5f, -90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == -270 || Mathf.Round(transform.eulerAngles.z) == 90)
                        {
                            RotateMonsterState(2.5f, 2.1f, -90f);
                        }
                    }
                    Invoke("MonsterSpeedManage", 0.2f);
                }
            }
        }
        else if(monsterType == MonsterType.AncientSlime)
        {
            if (endGrounded)
            {
                _anim.SetBool("Jump", false);
                if (myChar.Stun != 0)
                {
                    jumpTime -= Time.deltaTime;
                }

                if (jumpTime <= 0)
                {
                    if (JumpCnt > 0)
                    {
                        //transform.Translate(new Vector2(0.5f, 0.8f));
                        if (isRightDir)
                        {
                            _rigidbody.velocity = new Vector2(JumpMove, JumpPower);
                        }
                        else if (!isRightDir)
                        {
                            _rigidbody.velocity = new Vector2(-JumpMove, JumpPower);
                        }
                    }
                    isDrop = true;
                }
                else
                {
                    if (JumpCnt <= 0)
                    {
                        transform.GetComponent<MonsterTargeting>().RadiationShot();
                        JumpCnt = 1;
                        SFXSound(77);
                        return;
                    }
                }
                //떯어질때 값
            }
            else if (!endGrounded)
            {
                if (JumpCnt <= 0)
                {
                    jumpTime = 3f;
                    //JumpCnt = 2;
                }
                _anim.SetBool("Jump", true);
            }
            if (isDrop)
            {
                if (_rigidbody.velocity.y < 0)
                {
                    JumpCnt--;
                    if (isRightDir)
                    {
                        if (isfrontWall)
                        {
                            _rigidbody.velocity = new Vector2(3f, 0f);
                        }

                    }
                    else if (!isRightDir)
                    {
                        if (isfrontWall)
                        {
                            _rigidbody.velocity = new Vector2(-3f, 0f);
                        }
                    }

                    isDrop = false;
                }
            }
        }
        else if(monsterType == MonsterType.SendSlime)
        {
            if (AttackMotionCheck)
            {
                if (endGrounded)
                {
                    transform.Translate(Vector2.right * movementSpeed * SlowNum * myChar.SlowSpeed * myChar.Stun * Time.deltaTime);
                    if (isTouchingWall)
                    {
                        movementSpeed = 0f;
                        if (isGround)
                        {
                            if (Mathf.Round(transform.eulerAngles.z) == 0)
                            {
                                RotateMonsterState(0.25f, 0, 90f);
                            }
                            else if (Mathf.Round(transform.eulerAngles.z) == 90)
                            {
                                RotateMonsterState(0, 0.25f, 90f);
                            }
                            else if (Mathf.Round(transform.eulerAngles.z) == 180)
                            {
                                RotateMonsterState(-0.25f, 0, 90f);
                            }
                            else if (Mathf.Round(transform.eulerAngles.z) == 270)
                            {
                                RotateMonsterState(-0, -0.25f, 90f);
                            }
                        }
                        Invoke("MonsterSpeedManage", 0.2f);

                    }
                }
                else if (!endGrounded)
                {
                    movementSpeed = 0f;
                    if (isGround)
                    {
                        if (Mathf.Round(transform.eulerAngles.z) == 0 || Mathf.Round(transform.eulerAngles.z) == 360)
                        {
                            RotateMonsterState(1.65f, -1.5f, -90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == -90 || Mathf.Round(transform.eulerAngles.z) == 270)
                        {
                            RotateMonsterState(-1.5f, -1.65f, -90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == -180 || Mathf.Round(transform.eulerAngles.z) == 180)
                        {
                            RotateMonsterState(-1.65f, 1.5f, -90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == -270 || Mathf.Round(transform.eulerAngles.z) == 90)
                        {
                            RotateMonsterState(1.5f, 1.65f, -90f);
                        }
                    }
                    Invoke("MonsterSpeedManage", 0.2f);
                }
            }
        }
        else if (monsterType == MonsterType.Lava_SpikeBeetle)
        {
            if (AttackMotionCheck)
            {
                if (endGrounded)
                {
                    transform.Translate(Vector2.right * movementSpeed * SlowNum * myChar.SlowSpeed * myChar.Stun * Time.deltaTime);
                    if (isTouchingWall)
                    {
                        movementSpeed = 0f;
                        if (isGround)
                        {
                            if (Mathf.Round(transform.eulerAngles.z) == 0)
                            {
                                RotateMonsterState(0.15f, 0, 90f);
                            }
                            else if (Mathf.Round(transform.eulerAngles.z) == 90)
                            {
                                RotateMonsterState(0, 0.15f, 90f);
                            }
                            else if (Mathf.Round(transform.eulerAngles.z) == 180)
                            {
                                RotateMonsterState(-0.15f, 0, 90f);
                            }
                            else if (Mathf.Round(transform.eulerAngles.z) == 270)
                            {
                                RotateMonsterState(-0, -0.15f, 90f);
                            }
                        }
                        Invoke("MonsterSpeedManage", 0.2f);

                    }
                }
                else if (!endGrounded)
                {
                    movementSpeed = 0f;
                    if (isGround)
                    {
                        if (Mathf.Round(transform.eulerAngles.z) == 0 || Mathf.Round(transform.eulerAngles.z) == 360)
                        {
                            RotateMonsterState(1.65f, -1.5f, -90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == -90 || Mathf.Round(transform.eulerAngles.z) == 270)
                        {
                            RotateMonsterState(-1.5f, -1.65f, -90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == -180 || Mathf.Round(transform.eulerAngles.z) == 180)
                        {
                            RotateMonsterState(-1.65f, 1.5f, -90f);
                        }
                        else if (Mathf.Round(transform.eulerAngles.z) == -270 || Mathf.Round(transform.eulerAngles.z) == 90)
                        {
                            RotateMonsterState(1.5f, 1.65f, -90f);
                        }
                    }
                    Invoke("MonsterSpeedManage", 0.2f);
                }
            }
        }
    }    
    private void MonsterSpeedZero()
    {
        _rigidbody.velocity = Vector2.zero;
    }
    private void CrouchCheck()
    {
        if (MoveCountdown <= 0)
        {
            _anim.SetBool("Move", false);
            _rigidbody.velocity = Vector2.zero;
            if (CrouchCountdown > 0)
            {
                CrouchCountdown -= Time.deltaTime;
            }
            else
            {
                MoveCountdown = 5f;
                _anim.SetBool("Move", true);
            }
        }
        else
        {
            MoveCountdown -= Time.deltaTime;
            CrouchCountdown = 2.5f;
        }
    }

    private void Attackfire()
    {
        if (AttackType)
        {            
            if (fireCountdown <= 0)
            {
                AttackMotionCheck = false;

                _rigidbody.velocity = Vector2.zero;
                for (int i = 0; i < attackCount; i++)
                {                    
                    _anim.SetBool("Attack", true);
                }
            }
            if (AttackMotionCheck)
            {
                fireCountdown -= Time.deltaTime;
            }
        }        
    }

    private void AngerAttackfire()
    {
        isAngerCheck = true;
        preFireCount = fireCountdown;
        fireCountdown = 500f;
        AttackMotionCheck = false;
        _rigidbody.velocity = Vector2.zero;

        for (int i = 0; i < AngerCount; i++)
        {
            _anim.SetBool("Anger", true);
        }
        if (AttackMotionCheck)
        {
            fireCountdown = preFireCount;
        }
    }

    private void FlipCheck()
    {
        if (isTouchingWall && !JumpMoveMonster && monsterType != MonsterType.Fixed || !endGrounded && !JumpMoveMonster && monsterType != MonsterType.Fixed)
        {
            if (isGround)
            {
                Flip();
            }
            
        }

        if (JumpMoveMonster && isTouchingWall || monsterType == MonsterType.AncientSlime && isTouchingWall)
        {
            Flip();
        }

        if (FixedMonster)
        {
            float FlipPosX, FlipPosY;
            GameObject Player = GetComponent<MonsterTargeting>().PlayerList;

            if (!Player)
            {
                return;
            }
                
            if (Player.transform.gameObject.activeSelf == true)
            {
                FlipPosX = Player.transform.position.x - transform.position.x;
                FlipPosY = Player.transform.position.y - transform.position.y;

                if (transform.eulerAngles.z == 0)
                {
                    if (FlipPosX >= 0)
                    {
                        transform.localScale = new Vector3(-6, 6, 6);
                    }
                    else if (FlipPosX <= 0)
                    {
                        transform.localScale = new Vector3(6, 6, 6);
                    }
                }
                else if (transform.eulerAngles.z == 90)
                {
                    if (FlipPosY >= 0)
                    {
                        transform.localScale = new Vector3(6, 6, 6);
                    }
                    else if (FlipPosY <= 0)
                    {
                        transform.localScale = new Vector3(-6, 6, 6);
                    }
                }
                else if (transform.eulerAngles.z == 180)
                {
                    if (FlipPosX >= 0)
                    {
                        transform.localScale = new Vector3(6, 6, 6);
                    }
                    else if (FlipPosX <= 0)
                    {
                        transform.localScale = new Vector3(-6, 6, 6);
                    }
                }
                else if (transform.eulerAngles.z == 270)
                {
                    if (FlipPosY >= 0)
                    {
                        transform.localScale = new Vector3(-6, 6, 6);
                    }
                    else if (FlipPosY <= 0)
                    {
                        transform.localScale = new Vector3(6, 6, 6);
                    }
                }

            }

        }
    }
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

    private void Flip()
    {
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        movementSpeed *= -1;

        wallCheckDistance *= -1;

        isRightDir = !isRightDir;
    }

    public void attackEnd()
    {
        if (attackCountingCheck <= 1)
        {
            attackCountingCheck = attackCount;
            AttackMotionCheck = true;
            _anim.SetBool("Attack", false);
            if (myChar.Stun != 0)
            {
                fireCountdown = fireRate;
                //fireCountdown = 3f / fireRate;
            }
            else if (myChar.Stun == 0)
            {
                fireCountdown = 500f;
            }
        }
        else
        {
            --attackCountingCheck;
        }
    }
    public void AngerEnd()
    {
        if (AngerCountingCheck <= 1)
        {
            AngerCountingCheck = AngerCount;
            AttackMotionCheck = true;
            _anim.SetBool("Anger", false);
            if (myChar.Stun != 0)
            {
                fireCountdown = fireRate;
                //fireCountdown = 3f / fireRate;
            }
            else if (myChar.Stun == 0)
            {
                fireCountdown = 500f;
            }
            AngerCnt = (int)GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P3;
            StartCoroutine(WaitingAnger(2f));
        }
        else
        {
            --AngerCountingCheck;
        }
    }

    //강화형 데저트 폭스(중간보스) 공격모션체크용
    public void DesrtFox_attackEnd()
    {
        if (attackCountingCheck <= 1)
        {
            attackCountingCheck = attackCount;
            _anim.SetBool("Attack", false);
            AttackMotionCheck = true;
            if (myChar.Stun != 0)
            {
                //fireCountdown = 3f / fireRate;

                if (transform.GetComponent<MonsterTargeting>().ContinuousShot)
                {
                    fireCountdown = 2f;     //시간을 많이추가해서 공격패턴이 연속발생하는걸 방지하기위함
                    transform.GetComponent<MonsterTargeting>().ContinuousShot = false;
                    transform.GetComponent<MonsterTargeting>().MultiShot = true;
                }
                else
                {
                    fireCountdown = fireRate;       //어차피여기서 원래 공격속도로 변경됨
                    transform.GetComponent<MonsterTargeting>().ContinuousShot = true;
                    transform.GetComponent<MonsterTargeting>().MultiShot = false;
                }
            }
            else if (myChar.Stun == 0)
            {
                fireCountdown = 500f;
            }
        }
        else
        {            
            --attackCountingCheck;
        }
    }

    public void ChampionLavaGolem_attackEnd()
    {
        if (attackCountingCheck <= 1)
        {
            attackCountingCheck = attackCount;
            _anim.SetBool("Attack", false);
            AttackMotionCheck = true;
            if (myChar.Stun != 0)
            {
                //fireCountdown = 3f / fireRate;

                if (transform.GetComponent<MonsterTargeting>().MultiShot_Cnt == 1)
                {
                    fireCountdown = 3f;
                }
                else if (transform.GetComponent<MonsterTargeting>().MultiShot_Cnt == 2)
                {
                    fireCountdown = 5f; 
                }
                else
                {
                    fireCountdown = fireRate;
                }
            }
            else if (myChar.Stun == 0)
            {
                fireCountdown = 500f;
            }
        }
        else
        {
            --attackCountingCheck;
        }
    }
    //무적 몬스터 공격끝나고 무적패턴으로 진행하기위해필요
    public void AttackFalse()
    {
        _anim.SetBool("Attack", false);
        _anim.SetBool("Invincibility", true);
    }

    //무적패턴 후 N초 뒤에 무적해제를 위해 필요
    public void InvincibilityOff()
    {
        StartCoroutine(InvincibilityOff(3f));
    }

    private void RotateMonsterState(float xPos, float yPos, float rotateNum)
    {
        transform.localPosition += new Vector3(xPos, yPos);
        transform.Rotate(0, 0, rotateNum);
    }

    private void OnDrawGizmos()
    {
        if (monsterType == MonsterType.SquareMoveRight || monsterType == MonsterType.SquareMoveLeft || monsterType == MonsterType.GiantSpider || monsterType == MonsterType.SendSlime || monsterType == MonsterType.Lava_SpikeBeetle)
        {
            Gizmos.DrawWireSphere(endCheckRight.position, groundCheckRidus);
            Gizmos.DrawWireSphere(wallCheck.position, groundCheckRidus);
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRidus);
        }
        else if (HorizontalMoveMonster)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));

            Gizmos.DrawWireSphere(endCheckRight.position, groundCheckRidus);
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRidus);

        }
        else if (JumpMoveMonster || monsterType == MonsterType.AncientSlime)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
            Gizmos.DrawLine(frontwallCheck.position, new Vector3(frontwallCheck.position.x + wallCheckDistance, frontwallCheck.position.y, frontwallCheck.position.z));
            //Gizmos.DrawWireSphere(endCheckRight.position, groundCheckRidus);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(endCheckRight.position, new Vector2(groundCheckRidus + 0.3f, groundCheckRidus));
        }
    }
    //private void IndividualSlowCheck(float Time)
    //{
    //    if (Individual_Slow)
    //    {
    //        StartCoroutine(SlowActive(Time));
    //        Individual_Slow = false;
    //    }
    //}

    public void IndividualSlowCheck(float Time)
    {
        StartCoroutine(SlowActive(Time));
    }

    //private void SummonSlime()
    //{
    //    if (myChar.Chapter >= 1)
    //    {
    //        for (int i = 0; i < SummonedMonster.Count; i++)
    //        {
    //            GameObject Monseter = Instantiate(SummonedMonster[i], transform.position, Quaternion.identity);
    //            Monseter.transform.parent = transform.parent;
    //            Monseter.transform.localScale = new Vector2(5, 5);

    //        }
    //    }
    //}

    private void GravityScale()
    {
        _rigidbody.gravityScale = 1;
    }
    private void ItemDrop()
    {
        //DropItem(슬롯확률 / 슬롯코인최소드랍 / 슬롯코인최대드랍 / 골드 / 다이아 / 하트 / 쉴드 / 속성석 / 사용템) 확률순
        if (!EliteMonster && !MiddleBoss)
        {
            DropTemplateMgr Drop = GameManager.Instance.DropDataMgr;
            DropItem(Drop.GetTemplate(1).BasicMin, Drop.GetTemplate(1).BasicMax, Drop.GetTemplate(1).BasicPer, Drop.GetTemplate(2).BasicMin, Drop.GetTemplate(2).BasicMax, Drop.GetTemplate(2).BasicPer,
                Drop.GetTemplate(3).BasicMin, Drop.GetTemplate(3).BasicMax, Drop.GetTemplate(3).BasicPer, Drop.GetTemplate(4).BasicMin, Drop.GetTemplate(4).BasicMax, Drop.GetTemplate(4).BasicPer, Drop.GetTemplate(5).BasicMin, Drop.GetTemplate(5).BasicMax, Drop.GetTemplate(5).BasicPer,
                Drop.GetTemplate(6).BasicMin, Drop.GetTemplate(6).BasicMax, Drop.GetTemplate(6).BasicPer, Drop.GetTemplate(7).BasicMin, Drop.GetTemplate(7).BasicMax, Drop.GetTemplate(7).BasicPer);
        }
        else if (EliteMonster)
        {
            DropTemplateMgr Drop = GameManager.Instance.DropDataMgr;
            DropItem(Drop.GetTemplate(1).EliteMin, Drop.GetTemplate(1).EliteMax, Drop.GetTemplate(1).ElitePer, Drop.GetTemplate(2).EliteMin, Drop.GetTemplate(2).EliteMax, Drop.GetTemplate(2).ElitePer,
                Drop.GetTemplate(3).EliteMin, Drop.GetTemplate(3).EliteMax, Drop.GetTemplate(3).ElitePer, Drop.GetTemplate(4).EliteMin, Drop.GetTemplate(4).EliteMax, Drop.GetTemplate(4).ElitePer, Drop.GetTemplate(5).EliteMin, Drop.GetTemplate(5).EliteMax, Drop.GetTemplate(5).ElitePer,
                Drop.GetTemplate(6).EliteMin, Drop.GetTemplate(6).EliteMax, Drop.GetTemplate(6).ElitePer, Drop.GetTemplate(7).EliteMin, Drop.GetTemplate(7).EliteMax, Drop.GetTemplate(7).ElitePer);
        }
        else if (MiddleBoss)
        {
            if (!NoDropBoss)
            {
                DropTemplateMgr Drop = GameManager.Instance.DropDataMgr;
                DropItem(Drop.GetTemplate(1).BossMin, Drop.GetTemplate(1).BossMax, Drop.GetTemplate(1).BossPer, Drop.GetTemplate(2).BossMin, Drop.GetTemplate(2).BossMax, Drop.GetTemplate(2).BossPer,
                    Drop.GetTemplate(3).BossMin, Drop.GetTemplate(3).BossMax, Drop.GetTemplate(3).BossPer, Drop.GetTemplate(4).BossMin, Drop.GetTemplate(4).BossMax, Drop.GetTemplate(4).BossPer, Drop.GetTemplate(5).BossMin, Drop.GetTemplate(5).BossMax, Drop.GetTemplate(5).BossPer,
                    Drop.GetTemplate(6).BossMin, Drop.GetTemplate(6).BossMax, Drop.GetTemplate(6).BossPer, Drop.GetTemplate(7).BossMin, Drop.GetTemplate(7).BossMax, Drop.GetTemplate(7).BossPer);
            }            
        }
    }
    private void DropItem(int GoldMin, int GoldMax, float GoldPer, int DiaMin, int DiaMax, float DiaPer, int HeartMin, int HeartMax, float HeartPer,int ShieldMin, int ShieldMax, float ShieldPer,
        int StoneMin, int StoneMax, float StonePer, int SoulMin, int SoulMax, float SoulPer, int HeroMin, int HeroMax, float HeroPer)
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
        //            if (!isItemDropPos)
        //            {
        //                GameObject Item = Instantiate(GameManager.Instance.DropItem[0], transform.position, Quaternion.identity);
        //                Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
        //                Item.transform.parent = transform.parent;
        //                Item.transform.localScale = new Vector2(6, 6);
        //            }
        //            else if (isItemDropPos)
        //            {
        //                GameObject Item = Instantiate(GameManager.Instance.DropItem[0], itemDropPos.position, Quaternion.identity);
        //                Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
        //                Item.transform.parent = transform.parent;
        //                Item.transform.localScale = new Vector2(6, 6);
        //            }
                    
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
                    if (itemDropPos == null)
                    {
                        GameObject Item = Instantiate(GameManager.Instance.DropItem[1], transform.position, Quaternion.identity);
                        Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector2(1, 1);
                    }
                    else if (itemDropPos != null)
                    {
                        GameObject Item = Instantiate(GameManager.Instance.DropItem[1], itemDropPos.position, Quaternion.identity);
                        Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector2(1, 1);
                    }
                    
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
                    if (itemDropPos == null)
                    {
                        GameObject Item = Instantiate(GameManager.Instance.DropItem[2], transform.position, Quaternion.identity);
                        Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector2(1, 1);
                    }
                    else if (itemDropPos != null)
                    {
                        GameObject Item = Instantiate(GameManager.Instance.DropItem[2], itemDropPos.position, Quaternion.identity);
                        Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector2(1, 1);
                    }
                    
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
                    if (itemDropPos == null)
                    {
                        GameObject Item = Instantiate(GameManager.Instance.DropItem[3], transform.position, Quaternion.identity);
                        Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector2(1, 1);
                    }
                    else if (itemDropPos != null)
                    {
                        GameObject Item = Instantiate(GameManager.Instance.DropItem[3], itemDropPos.position, Quaternion.identity);
                        Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector2(1, 1);
                    }
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
                    if (itemDropPos == null)
                    {
                        GameObject Item = Instantiate(GameManager.Instance.DropItem[4], transform.position, Quaternion.identity);
                        Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector2(1, 1);
                    }
                    else if (itemDropPos != null)
                    {
                        GameObject Item = Instantiate(GameManager.Instance.DropItem[4], itemDropPos.position, Quaternion.identity);
                        Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector2(1, 1);
                    }
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
                    if (itemDropPos == null)
                    {
                        int ElementalStoneNum = Random.Range(0, GameManager.Instance.ElementalStone.Count);
                        GameObject Item = Instantiate(GameManager.Instance.ElementalStone[ElementalStoneNum], transform.position, Quaternion.identity);
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector3(6, 6f);
                    }
                    else if (itemDropPos != null)
                    {
                        int ElementalStoneNum = Random.Range(0, GameManager.Instance.ElementalStone.Count);
                        GameObject Item = Instantiate(GameManager.Instance.ElementalStone[ElementalStoneNum], itemDropPos.position, Quaternion.identity);
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector3(6f, 6f);
                    }
                    
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
        //            if (!isItemDropPos)
        //            {
        //                int ActiveItemNum = Random.Range(0, GameManager.Instance.ActiveItem.Count);
        //                GameObject Item = Instantiate(GameManager.Instance.ActiveItem[ActiveItemNum], transform.position, Quaternion.identity);
        //                Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
        //                Item.transform.parent = transform.parent;
        //            }
        //            else if (isItemDropPos)
        //            {
        //                int ActiveItemNum = Random.Range(0, GameManager.Instance.ActiveItem.Count);
        //                GameObject Item = Instantiate(GameManager.Instance.ActiveItem[ActiveItemNum], itemDropPos.position, Quaternion.identity);
        //                Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
        //                Item.transform.parent = transform.parent;
        //            }
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
                    if (itemDropPos == null)
                    {
                        GameObject Item = Instantiate(GameManager.Instance.DropItem[5], transform.position, Quaternion.identity);
                        Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector2(1, 1);
                    }
                    else if (itemDropPos != null)
                    {
                        GameObject Item = Instantiate(GameManager.Instance.DropItem[5], itemDropPos.position, Quaternion.identity);
                        Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector2(1, 1);
                    }
                    
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
                    if (itemDropPos == null)
                    {
                        GameObject Item = Instantiate(GameManager.Instance.DropItem[6], transform.position, Quaternion.identity);
                        Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector2(1, 1);
                    }
                    else if (itemDropPos != null)
                    {
                        GameObject Item = Instantiate(GameManager.Instance.DropItem[6], itemDropPos.position, Quaternion.identity);
                        Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                        Item.transform.parent = transform.parent;
                        Item.transform.localScale = new Vector2(1, 1);
                    }
                }
            }
        }        
    }
    private void MonsterSpeedManage()
    {
        movementSpeed = PremovementSpeed;
    }

    private void AttackMoveCheck()
    {
        AttackMotionCheck = true;
    }
    private void SFXSound(int Num)
    {
        SoundManager.Instance.PlaySfx(Num);
    }
    IEnumerator AngerSpeed(float Time)
    {
        _anim.SetBool("Anger", true);
        AngerRun = true;

        movementSpeed *= GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P2;
        // 챕터별로 작동되게할때 사용했음
        //if (myChar.Chapter == 1)
        //{
        //    movementSpeed *= GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P2;

        //}
        //else if (myChar.Chapter == 2)
        //{
        //    movementSpeed *= GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P3;
        //}

        yield return new WaitForSeconds(Time);
        _anim.SetBool("Anger", false);
        AngerRun = false;
        if (isRightDir)
        {
            movementSpeed = PremovementSpeed;
        }
        else
        {
            movementSpeed = -PremovementSpeed;
        }

    }
    IEnumerator SlowActive(float Time)              //슬로우아이템
    {
        SlowNum = 0.5f;
        yield return new WaitForSeconds(Time);
        SlowNum = 1f;
    }
    IEnumerator WaitingAnger(float Time)
    {
        yield return new WaitForSeconds(Time);
        isAngerCheck = false;
    }
    IEnumerator InvincibilityOff(float Time)
    {
        yield return new WaitForSeconds(Time);
        _anim.SetBool("Invincibility", false);
    }
    private void HitEffectEnd()
    {
        _shader.DisableKeyword("HITEFFECT_ON");
    }
}
