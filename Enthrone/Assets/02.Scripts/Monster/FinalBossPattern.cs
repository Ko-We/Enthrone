using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBossPattern : MonoBehaviour
{
    public enum ThroneType
    {
        Swordsman,
        Warrior,
        Archer,
        Wizard,
        Ninja,
        SkeletonKing,
        Corrupted_Swordsman
    }

    public ThroneType throneType;
    public int MonsterIndex;
    public float TestSpeed;
    public float Gackdo;
    public LayerMask _layerMask;
    public LayerMask _PlayerlayerMask;
    Material _shader;
    MyObject myChar;
    GameManager instance;
    private Animator _anim;
    public Rigidbody2D _rigidbody;
    private Collider2D _collider;
    public bool StartCheck = false;
    public GameObject Crown;
    public GameObject BoxKey;
    private GameObject Effect;
    public GameObject SkillEffectObject;
    public int PatternNum;
    public int AttackNum;

    public float wallCheckDistance;
    public float wallCheckDistanceRL;

    public Transform AttackPoint;
    public Transform wallCheck;
    public Transform wallCheckRight;
    public Transform wallCheckLeft;
    public Transform groundCheck;


    public float monsterHp;
    private float MaxHp;
    public float MoveCheckRange;
    //워리어 스킬 시간
    public float WhirlwindTime = 5f;
    private float WhirlwindASPD = 1f;
    private bool WhirlwindMoveCheck;
    private bool WhirlwindRLChoice;
    
    [SerializeField]
    private float WhirlwindMoveTime;
    private int WizardSkillCnt = 2;
    private int Corrupted_Swordsman_SkillType = 0;
    private int Corrupted_SwordsmanStrikeCnt = 4;
    private int NinjaSkillCnt;
    public bool isSkill;
    public bool SkillMoveRLCheck;
    //캐스팅 모션 끝나기전까지 true로 해서 다른행동 못하게 해주는 bool값
    public bool SkillEffect = true;
    public bool ArcherTargetCheck = false;      //궁수가 저격공격중인지 체크
    public bool TargetCooltimeCheck = false;    //궁수 저격공격 쿨타임동안 공격모션해야되는지 체크

    public int JumpCnt;
    private GameObject MonsterProjectile;
    private float timeNum;
    private float movementSpeed = 2f;
    public float SlowNum = 1f;
    [SerializeField]
    private float PatternCoolTime;
    [SerializeField]
    public float SkillCoolTime;
    [SerializeField]
    private float JumpCoolTime;
    private float PlayerPointPostion;
    private bool RightCheck = false;
    private bool isMoving;
    private bool isGrounded;
    [SerializeField]
    private bool isJumping;
    private bool isPlayerMoveCheck;
    private bool isJumptop;
    private bool isTouchingWall;
    private bool isTouchingWallRight;
    private bool isTouchingWallLeft;
    private bool SkillTouchingWallRight;    //소드맨 스킬시전시 반대벽도착 확인위함
    private bool SkillTouchingWallLeft;     //소드맨 스킬시전시 반대벽도착 확인위함
    private bool isWallSliding;
    [SerializeField]
    private bool isPattern;
    private bool isPointJump;                   //3번 패턴 현재 플레이어 위치(Pont)를 찾기위해 
    [SerializeField]
    private bool PatternJumpCheck = false;      //해당 함수가 true라면 JumpCoolTime이 감소하면서 정해진 패턴의 초마다 점프시전이가능하다.
    private bool TargettingCheck;

    public int SkillNo;
    public List<GameObject> SkillProjectile = new List<GameObject>();
    public GameObject RSwordParent, LSwordParent;
    public List<GameObject> RSwordManPos = new List<GameObject>();
    public List<GameObject> LSwordManPos = new List<GameObject>();
    public List<GameObject> TopArrowPos = new List<GameObject>();
       
    private void Awake()
    {
        instance = GameManager.Instance;
    }
    private void Start()
    {
        myChar = MyObject.MyChar;
        MonsterProjectile = GameObject.Find("MonsterProjectileManager");
        _shader = GetComponent<Renderer>().material;
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        Effect = gameObject.transform.Find("Effect").gameObject;

        if (myChar.EquipmentTotalScore >= 300)
        {
            SkillNo = 5;
        }
        else if (myChar.EquipmentTotalScore < 300 && myChar.EquipmentTotalScore >= 250)
        {
            SkillNo = 4;
        }
        else if (myChar.EquipmentTotalScore < 250 && myChar.EquipmentTotalScore >= 200)
        {
            SkillNo = 3;
        }
        else if (myChar.EquipmentTotalScore < 200 && myChar.EquipmentTotalScore >= 150)
        {
            SkillNo = 2;
        }
        else if (myChar.EquipmentTotalScore < 150 && myChar.EquipmentTotalScore >= 125)
        {
            SkillNo = 1;
        }
        else
        {
            SkillNo = 0;
        }

        if (throneType == ThroneType.Swordsman)
        {
            SkillCoolTime = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).K_CoolTime;
        }
        else if (throneType == ThroneType.Warrior)
        {
            SkillCoolTime = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).W_CoolTime;
        }
        else if (throneType == ThroneType.Archer)
        {
            SkillCoolTime = 10f;
            //SkillCoolTime = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).A_CoolTime;
        }
        else if (throneType == ThroneType.Wizard)
        {
            SkillCoolTime = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).Wi_CoolTime;
        }
        else if (throneType == ThroneType.Ninja)
        {
            SkillCoolTime = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).N_CoolTime;
        }

    }


    // Update is called once per frame
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    isSkill = true;
        //    _anim.SetBool("Casting", true);
        //    if (throneType == ThroneType.SwordMan)
        //    {
        //        _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        //        SkillEffect = true;
        //    }

        //}
        if (!StartCheck)
        {
            if (!myChar.Tutorial)
            {
                //MaxHp = GameManager.Instance.UnitDataMgr.GetTemplate(24 + myChar.EnthroneHeroNum).HP * myChar.MultiHp * myChar.ThroneStatDataMgr.GetTemplate(myChar.ThroneIndex).ThroneStageHP;
                MaxHp = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).HP * myChar.MultiHp;
                monsterHp = MaxHp;
                SkillCoolTime = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P2;
            }
            else
            {
                MaxHp = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).HP;
                monsterHp = MaxHp;
                
            }

            StartCheck = true;
        }
        if (monsterHp > 0)
        {
            if (throneType != ThroneType.Corrupted_Swordsman)
            {
                if (isPlayerMoveCheck)
                {
                    if (movementSpeed >= 0.1f)
                    {
                        movementSpeed -= 2f * Time.deltaTime;
                    }
                    //movementSpeed = 0.1f;
                }
                else if (!isPlayerMoveCheck)
                {
                    movementSpeed = 2f;
                }
            }
            else if(throneType == ThroneType.Corrupted_Swordsman)
            {
                if (isPlayerMoveCheck)
                {
                    if (movementSpeed >= 0.1f)
                    {
                        movementSpeed -= GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Speed * Time.deltaTime;
                    }
                    //movementSpeed = 0.1f;
                }
                else if (!isPlayerMoveCheck)
                {
                    movementSpeed = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Speed;
                }
            }
            //if (isPlayerMoveCheck)
            //{
            //    if (movementSpeed >= 0.1f)
            //    {
            //        movementSpeed -= 2f * Time.deltaTime;
            //    }
            //    //movementSpeed = 0.1f;
            //}
            //else if (!isPlayerMoveCheck)
            //{
            //    movementSpeed = 2f;
            //}
            EffectCheck();
            IndividualEffectCheck();
            FlipCheck();
            UpdateAnimations();
            CheckSurroundings();
            CheckWallSliding();
            if (!isSkill)
            {
                Pattern();
                _shader.DisableKeyword("OUTBASE_ON");

                if (throneType == ThroneType.Swordsman)
                {
                    if (RSwordManPos.Count != RSwordParent.transform.childCount)
                    {
                        RSwordManPos.Clear();
                        for (int i = 0; i < RSwordParent.transform.childCount; i++)
                        {
                            RSwordManPos.Add(RSwordParent.transform.GetChild(i).gameObject);
                        }
                    }
                    if (LSwordManPos.Count != LSwordParent.transform.childCount)
                    {
                        LSwordManPos.Clear();
                        for (int i = 0; i < LSwordParent.transform.childCount; i++)
                        {
                            LSwordManPos.Add(LSwordParent.transform.GetChild(i).gameObject);
                        }
                    }
                }
                if (throneType == ThroneType.Corrupted_Swordsman)
                {
                    if (RSwordManPos.Count != RSwordParent.transform.childCount)
                    {
                        RSwordManPos.Clear();
                        for (int i = 0; i < RSwordParent.transform.childCount; i++)
                        {
                            RSwordManPos.Add(RSwordParent.transform.GetChild(i).gameObject);
                        }
                    }
                    if (LSwordManPos.Count != LSwordParent.transform.childCount)
                    {
                        LSwordManPos.Clear();
                        for (int i = 0; i < LSwordParent.transform.childCount; i++)
                        {
                            LSwordManPos.Add(LSwordParent.transform.GetChild(i).gameObject);
                        }
                    }
                }
                if (!myChar.Tutorial)
                {
                    if (SkillCoolTime > 0)
                    {
                        SkillCoolTime -= Time.deltaTime;
                    }
                    else
                    {
                        isSkill = true;
                        

                        if (throneType == ThroneType.Swordsman)
                        {
                            _anim.SetBool("Casting", true);
                            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                            SkillEffect = true;
                        }
                        else if (throneType == ThroneType.Warrior)
                        {
                            _anim.SetBool("Casting", true);
                            WhirlwindTime = 5f;
                            SkillEffect = true;
                        }
                        else if (throneType == ThroneType.Corrupted_Swordsman)
                        {
                            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                            SkillEffect = true;
                            if (Corrupted_Swordsman_SkillType == 0)
                            {
                                _anim.SetBool("Casting", true);
                            }
                            else if (Corrupted_Swordsman_SkillType == 1)
                            {
                                _anim.SetBool("Casting2", true);
                            }
                        }
                        else if (throneType == ThroneType.Archer)
                        {
                            _anim.SetBool("Casting", true);
                            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                            SkillEffect = true;
                        }
                    }
                }
                else
                {
                    isSkill = false;
                }
            }
            else
            {
                if (throneType == ThroneType.Swordsman)
                {
                    SwordmanSkill();
                }
                else if (throneType == ThroneType.Warrior)
                {
                    if (WhirlwindTime > 0)
                    {
                        WhirlwindTime -= Time.deltaTime;

                        WarriorWhirlwind();
                    }
                    else
                    {
                        WarriorWhirlwindEnd();
                    }
                }
                else if (throneType == ThroneType.Archer)
                {
                    if (FinalBossTargeting.Instance.TargetShot)
                    {
                        if (TargettingCheck)
                        {
                            if (SkillCoolTime > 0)
                            {
                                SkillCoolTime -= Time.deltaTime;
                            }
                            else
                            {
                                _anim.SetBool("Attack", true);
                            }
                            
                        }
                    }
                }
                else if (throneType == ThroneType.Ninja)
                {
                    if (NinjaSkillCnt <= 0)
                    {
                        isSkill = false;
                        _anim.SetBool("Casting", false);
                        _anim.SetBool("Skill", false);
                        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                        SkillCoolTime = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).N_CoolTime;
                        SkillEffectObject.SetActive(false);
                        OutLineOff();
                    }
                }
                else if (throneType == ThroneType.Corrupted_Swordsman)
                {
                    SwordmanSkill();
                }
            }
            SkinChekc();
            //if (RightCheck)
            //{
            //    Vector3 dir = new Vector3(1, 0, 0);
            //    transform.Translate(dir.normalized * movementSpeed * Time.deltaTime);
            //}
            //else if (!RightCheck)
            //{
            //    Vector3 dir = new Vector3(-1, 0, 0);
            //    transform.Translate(dir.normalized * movementSpeed * Time.deltaTime);
            //}

        }
        else
        {
            myChar.ThroneBossKill = true;
        }

        if (myChar.BossHPAnim)
        {
            GameManager.Instance.BossUI.transform.GetChild(0).Find("HP").GetComponent<Image>().fillAmount = monsterHp / MaxHp;
        }

    }
    private void CheckSurroundings()
    {
        isPlayerMoveCheck = Physics2D.OverlapCircle(AttackPoint.position, MoveCheckRange, _PlayerlayerMask);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, _layerMask);

        isJumptop = Physics2D.OverlapCircle(new Vector3(groundCheck.position.x, groundCheck.position.y + +0.5f), 0.1f, _layerMask);

        isTouchingWall = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.51f, wallCheckDistance - 0.04f), 0, _layerMask);
        isTouchingWallRight = Physics2D.OverlapBox(new Vector2(wallCheckRight.position.x + 0.14f, wallCheckRight.position.y - 0.01f), new Vector2(0.28f, 0.5f), 0, _layerMask);
        isTouchingWallLeft = Physics2D.OverlapBox(new Vector2(wallCheckLeft.position.x - 0.14f, wallCheckLeft.position.y - 0.01f), new Vector2(0.28f, 0.5f), 0, _layerMask);

        //소드맨 Skill시전시 반대편 벽 도착확인하기위함
        SkillTouchingWallRight = Physics2D.OverlapBox(new Vector2(wallCheckRight.position.x + 0.14f, wallCheckRight.position.y - 0.01f), new Vector2(0.28f, 0.2f), 0, _layerMask);
        SkillTouchingWallLeft = Physics2D.OverlapBox(new Vector2(wallCheckLeft.position.x - 0.14f, wallCheckLeft.position.y - 0.01f), new Vector2(0.28f, 0.2f), 0, _layerMask);
    }

    public void AttackHit(float Damage)
    {
        _shader.EnableKeyword("HITEFFECT_ON");
        Invoke("HitEffectEnd", 0.1f);
        monsterHp -= Damage;
        FinalBossTargeting.Instance.monsterHp = monsterHp;
        if (monsterHp <= 0)
        {
            SoundManager.Instance.PlaySfx(20);
            _anim.SetBool("Death", true);
            _rigidbody.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("DeathMonster");
            gameObject.tag = "DeathMonster";
            //gameObject.layer = 0;

            if (myChar.Tutorial)
            {
                RoomManager.Instance.TutoCheck = 1;
            }
        }
    }
    private void FlipCheck()
    {
        GameObject Player = GameObject.Find("Player");
        float FlipPos;
        if (instance.SelectCharacter)
        {
            FlipPos = instance.SelectCharacter.transform.position.x - transform.position.x;

            if (PatternNum == 1 || PatternNum == 2)
            {
                if (AttackNum != 0)
                {
                    if (FlipPos >= 0.01f)
                    {
                        transform.localScale = new Vector3(6, 6, 1);
                        RightCheck = true;
                    }
                    else if (FlipPos <= -0.01f)
                    {
                        transform.localScale = new Vector3(-6, 6, 1);
                        RightCheck = false;
                    }
                }
                else if (AttackNum == 0)
                {
                    if (!isTouchingWall)
                    {
                        if (FlipPos >= 0.01f)
                        {
                            transform.localScale = new Vector3(-6, 6, 1);
                            RightCheck = true;
                        }
                        else if (FlipPos <= -0.01f)
                        {
                            transform.localScale = new Vector3(6, 6, 1);
                            RightCheck = false;
                        }
                    }
                    else
                    {
                        if (FlipPos >= 0.01f)
                        {
                            transform.localScale = new Vector3(6, 6, 1);
                            RightCheck = true;
                        }
                        else if (FlipPos <= -0.01f)
                        {
                            transform.localScale = new Vector3(-6, 6, 1);
                            RightCheck = false;
                        }
                    }
                }
            }
            else
            {
                if (FlipPos >= 0.01f)
                {
                    transform.localScale = new Vector3(6, 6, 1);
                    RightCheck = true;
                }
                else if (FlipPos <= -0.01f)
                {
                    transform.localScale = new Vector3(-6, 6, 1);
                    RightCheck = false;
                }
            }
        }
        if (_rigidbody.velocity.x != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
    private void SkinChekc()
    {
        switch (myChar.ThroneCostumSkin)
        {
            case 0:
                for (int i = 0; i < 5; i++)
                {
                    if (_anim.layerCount >= i)
                    {
                        _anim.SetLayerWeight(i, 0);
                    }
                }
                break;
            case 1:
                for (int i = 1; i <= 5; i++)
                {
                    if (i == myChar.ThroneCostumSkin)
                    {
                        _anim.SetLayerWeight(i, 1);
                    }
                    else
                    {
                        _anim.SetLayerWeight(i, 0);
                    }
                }
                break;
            case 2:
                for (int i = 1; i <= 5; i++)
                {
                    if (i == myChar.ThroneCostumSkin)
                    {
                        _anim.SetLayerWeight(i, 1);
                    }
                    else
                    {
                        _anim.SetLayerWeight(i, 0);
                    }
                }
                break;
            case 3:
                for (int i = 1; i <= 5; i++)
                {
                    if (i == myChar.ThroneCostumSkin)
                    {
                        _anim.SetLayerWeight(i, 1);
                    }
                    else
                    {
                        _anim.SetLayerWeight(i, 0);
                    }
                }
                break;
            case 4:
                for (int i = 1; i <= 5; i++)
                {
                    if (i == myChar.ThroneCostumSkin)
                    {
                        _anim.SetLayerWeight(i, 1);
                    }
                    else
                    {
                        _anim.SetLayerWeight(i, 0);
                    }
                }
                break;
            case 5:
                for (int i = 1; i <= 5; i++)
                {
                    if (i == myChar.ThroneCostumSkin)
                    {
                        _anim.SetLayerWeight(i, 1);
                    }
                    else
                    {
                        _anim.SetLayerWeight(i + 2, 0);
                    }
                }
                break;
        }
    }
    //private void FlipCheck()
    //{
    //    GameObject Player = GameObject.Find("Player");
    //    float FlipPos;
    //    for (int i = 0; i < myChar.HeroNum; i++)
    //    {
    //        if (Player.transform.GetChild(i).gameObject.activeSelf == true)
    //        {
    //            if (Player.transform.GetChild(i).name != "SeasnalShield")
    //            {
    //                Debug.Log(Player.transform.GetChild(i).localPosition.x);
    //                FlipPos = Player.transform.GetChild(i).position.x - transform.position.x;
    //                if (PatternNum == 1 || PatternNum == 2)
    //                {
    //                    if (AttackNum != 0)
    //                    {
    //                        if (FlipPos >= 0.01f)
    //                        {
    //                            transform.localScale = new Vector3(6, 6, 1);
    //                            RightCheck = true;
    //                        }
    //                        else if (FlipPos <= -0.01f)
    //                        {
    //                            transform.localScale = new Vector3(-6, 6, 1);
    //                            RightCheck = false;
    //                        }
    //                    }
    //                    else if (AttackNum == 0)
    //                    {
    //                        if (!isTouchingWall)
    //                        {
    //                            if (FlipPos >= 0.01f)
    //                            {
    //                                transform.localScale = new Vector3(-6, 6, 1);
    //                                RightCheck = true;
    //                            }
    //                            else if (FlipPos <= -0.01f)
    //                            {
    //                                transform.localScale = new Vector3(6, 6, 1);
    //                                RightCheck = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (FlipPos >= 0.01f)
    //                            {
    //                                transform.localScale = new Vector3(6, 6, 1);
    //                                RightCheck = true;
    //                            }
    //                            else if (FlipPos <= -0.01f)
    //                            {
    //                                transform.localScale = new Vector3(-6, 6, 1);
    //                                RightCheck = false;
    //                            }
    //                        }

    //                    }
    //                }
    //                else
    //                {
    //                    if (FlipPos >= 0.01f)
    //                    {
    //                        transform.localScale = new Vector3(6, 6, 1);
    //                        RightCheck = true;
    //                    }
    //                    else if (FlipPos <= -0.01f)
    //                    {
    //                        transform.localScale = new Vector3(-6, 6, 1);
    //                        RightCheck = false;
    //                    }
    //                }

    //            }

    //        }
    //    }
    //    if (_rigidbody.velocity.x != 0)
    //    {
    //        isMoving = true;
    //    }
    //    else
    //    {
    //        isMoving = false;
    //    }
    //}
    private void EffectCheck()
    {
        //if (myChar.Stun == 0)
        //{
        //    Effect.transform.GetChild(1).gameObject.SetActive(true);
        //}
        //else if (myChar.Stun != 0)
        //{
        //    Effect.transform.GetChild(1).gameObject.SetActive(false);
        //}


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
    private void CheckWallSliding()
    {
        if (isTouchingWall && !isGrounded)
        {
            isWallSliding = true;

            if (_rigidbody.velocity.y < -1)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -1 * (myChar.SlowSpeed));
            }
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void UpdateAnimations()
    {
        if (!myChar.Wizard)
        {
            _anim.SetBool("Move", isMoving);
            _anim.SetBool("isWallSlide", isWallSliding);
        }
        else
        {
            if (!isJumping)
            {
                _anim.SetBool("Move", isMoving);
            }
            else
            {
                _anim.SetBool("Levitation", isJumping);

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (monsterHp > 0)
        {
            //if (col.transform.CompareTag("Pattern"))
            //{
            //    Debug.Log("패턴");
            //    if (PatternNum == 5)
            //    {
            //        isJumping = true;
            //    }

            //}
            //else if (col.transform.CompareTag("JumpPattern"))
            //{
            //    Debug.Log("점프패턴");
            //    if (Random.Range(0, 2) == 0)
            //    {
            //        if (JumpCnt > 0)
            //        {
            //            Jump();
            //            JumpCnt--;
            //        }
            //    }
            //    switch (PatternNum)
            //    {
            //        case 0:
            //            JumpCoolTime = 3f;
            //            break;
            //        case 1:
            //            JumpCoolTime = 2f;
            //            break;
            //        case 2:
            //            JumpCoolTime = 2.5f;
            //            break;

            //    }
            //}
        }
    }
    private void Pattern()
    {
        if (PatternCoolTime > 0)
        {
            isPattern = true;
        }
        else if (PatternCoolTime <= 0)
        {
            isPattern = false;
            int RandomValue = Random.Range(0, 100);
            if (RandomValue < 40)
            {
                PatternNum = 0;
            }
            else if (RandomValue >= 40 && RandomValue < 50)
            {
                PatternNum = 1;
            }
            else if (RandomValue >= 50 && RandomValue < 60)
            {
                PatternNum = 2;
            }
            else if (RandomValue >= 60 && RandomValue < 70)
            {
                PatternNum = 3;
            }
            else if (RandomValue >= 70 && RandomValue < 80)
            {
                PatternNum = 4;
            }
            else if (RandomValue >= 80 && RandomValue < 90)
            {
                PatternNum = 5;
            }
            else if (RandomValue >= 90 && RandomValue < 100)
            {
                PatternNum = 6;
            }
            switch (PatternNum)
            {
                case 0:
                    PatternCoolTime = 8f;
                    JumpCoolTime = 3f;
                    PatternJumpCheck = true;
                    break;
                case 1:
                    PatternCoolTime = 6f;
                    JumpCoolTime = 2f;
                    AttackNum = 3;
                    PatternJumpCheck = true;
                    break;
                case 2:
                    PatternCoolTime = 3f;
                    JumpCoolTime = 3f;
                    AttackNum = 1;
                    PatternJumpCheck = true;
                    break;
                case 3:
                    PatternCoolTime = 5f;
                    PatternJumpCheck = false;
                    if (instance.SelectCharacter)
                    {
                        PlayerPointPostion = instance.SelectCharacter.transform.localPosition.x;
                        isPointJump = false;
                    }
                    break;
                case 4:
                    PatternCoolTime = 3f;
                    PatternJumpCheck = false;
                    isJumping = false;
                    break;
                case 5:
                    PatternCoolTime = 4f;
                    isJumping = false;
                    PatternJumpCheck = false;
                    JumpCnt = 1;
                    break;
                case 6:
                    PatternCoolTime = 5f;
                    PatternJumpCheck = false;
                    if (instance.SelectCharacter)
                    {
                        PlayerPointPostion = instance.SelectCharacter.transform.localPosition.x;
                        isPointJump = false;
                    }
                    break;
                case 7:
                    PatternCoolTime = 4f;
                    PatternJumpCheck = false;
                    isJumping = false;
                    break;
                default:
                    break;
            }
        }
        if (isPattern)
        {
            switch (PatternNum)
            {
                case 0:
                    if (RightCheck)
                    {
                        Vector3 dir = new Vector3(1, 0, 0);
                        transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                    }
                    else if (!RightCheck)
                    {
                        Vector3 dir = new Vector3(-1, 0, 0);
                        transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                    }
                    if (JumpCoolTime <= 0)
                    {
                        JumpPattern();
                    }
                    break;
                case 1:
                    TracePattern(3);
                    break;
                case 2:
                    TracePattern(1);
                    break;
                case 3:
                    WallJumpPattern(1);
                    break;
                case 4:
                    if (RightCheck)
                    {
                        Vector3 dir = new Vector3(1, 0, 0);
                        transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                    }
                    else if (!RightCheck)
                    {
                        Vector3 dir = new Vector3(-1, 0, 0);
                        transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                    }

                    if (!isJumping)
                    {
                        isJumping = true;
                    }
                    else if (isJumping)
                    {
                        if (isGrounded)
                        {
                            Jump();
                            JumpCnt = 1;
                            isJumping = false;
                        }
                    }
                    break;
                case 5:
                    if (!isJumping)
                    {
                        if (transform.localPosition.x > 0.3)
                        {
                            Vector3 dir = new Vector3(-1, 0, 0);
                            transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                        }
                        else if (transform.localPosition.x < -0.3)
                        {
                            Vector3 dir = new Vector3(1, 0, 0);
                            transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                        }
                        else if (transform.localPosition.x <= 0.3 || transform.localPosition.x >= -0.3)
                        {
                            isJumping = true;
                        }
                    }
                    else if (isJumping)
                    {
                        if (isGrounded)
                        {
                            _rigidbody.velocity = Vector2.zero;
                            Jump();
                            JumpCnt = 1;
                            isJumping = false;
                        }

                    }
                    break;
                case 6:
                    WallJumpPattern(-1);
                    break;
                case 7:
                    if (!RightCheck)
                    {
                        Vector3 dir = new Vector3(1, 0, 0);
                        transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                    }
                    else if (RightCheck)
                    {
                        Vector3 dir = new Vector3(-1, 0, 0);
                        transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                    }
                    if (!isJumping)
                    {
                        isJumping = true;
                    }
                    else if (isJumping)
                    {
                        if (isGrounded)
                        {
                            Jump();
                            JumpCnt = 1;
                            isJumping = false;
                        }
                    }
                    break;
            }
        }
        PatternCoolTime -= Time.deltaTime;
        if (JumpCoolTime >= 0 && PatternJumpCheck)
        {
            if (isGrounded)
            {
                JumpCoolTime -= Time.deltaTime;
            }
        }
    }

    public void BossSkill()
    {
        if (true)
        {

        }
    }
    public void Jump()
    {
        if (isTouchingWall && _rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 8f);
            SoundManager.Instance.PlaySfx(7);
        }
        else if (!isWallSliding)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 8f);
            SoundManager.Instance.PlaySfx(7);
        }

        //if (isJumpShoot)
        //{
        //    transform.GetComponent<PlayerTargeting>().JumpShoot();
        //}

    }
    private void JumpPattern()
    {
        if (isGrounded && PatternJumpCheck)
        {
            Jump();
            JumpCnt = 1;
        }
    }
    private void TracePattern(int Cnt)
    {
        if (AttackNum != 0)
        {
            if (RightCheck)
            {
                Vector3 dir = new Vector3(1, 0, 0);
                transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
            }
            else if (!RightCheck)
            {
                Vector3 dir = new Vector3(-1, 0, 0);
                transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
            }
        }
        else if (AttackNum == 0)
        {
            if (!isTouchingWall)
            {
                if (!RightCheck)
                {
                    Vector3 dir = new Vector3(1, 0, 0);
                    transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                }
                else if (RightCheck)
                {
                    Vector3 dir = new Vector3(-1, 0, 0);
                    transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                }
            }
            else if (isTouchingWall)
            {
                AttackNum = Cnt;
            }
        }

        if (JumpCoolTime <= 0)
        {
            JumpPattern();
        }
    }
    private void WallJumpPattern(int Cnt)
    {
        if (!isPointJump)
        {
            if (!isTouchingWall)
            {
                if (PlayerPointPostion >= 0)
                {
                    Vector3 dir = new Vector3(Cnt, 0, 0);
                    transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                }
                else
                {
                    Vector3 dir = new Vector3(-Cnt, 0, 0);
                    transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                }
            }
        }
        else if (isPointJump)
        {
            if (RightCheck)
            {
                Vector3 dir = new Vector3(1, 0, 0);
                transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
            }
            else if (!RightCheck)
            {
                Vector3 dir = new Vector3(-1, 0, 0);
                transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
            }
            if (isGrounded)
            {
                PlayerPointPostion = instance.SelectCharacter.transform.localPosition.x;
                isPointJump = false;
            }
        }
        if (isTouchingWall)
        {
            Jump();
            if (isJumptop)
            {
                isPointJump = true;
            }
        }
    }
    //소드맨 텔레포트 후 모션끝나면 반대벽으로 날라가는코드
    private void SwordmanSkill()
    {
        if (!SkillEffect)   //소드맨이 벽으로 순간이동후 직선방향으로 날아가게 하기위함(이 조건문이 없으면 벽으로 소환되면서 날라감)
        {
            if (SkillMoveRLCheck)   //좌우벽 시작방향조건문
            {
                transform.Translate(Vector2.left * myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).K_Speed * myChar.SlowSpeed * Time.deltaTime);
                if (SkillTouchingWallLeft)
                {
                    isSkill = false;
                    _anim.SetBool("Casting", false);
                    _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    SkillEffect = true;
                    //if (throneType == ThroneType.Swordsman)
                    //{
                    //    SkillCoolTime = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).K_CoolTime;
                    //}
                    //else if ( throneType == ThroneType.Corrupted_Swordsman)
                    //{
                    //    SkillCoolTime = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P3;
                    //}
                    
                }
            }
            else
            {
                transform.Translate(Vector2.right * myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).K_Speed * myChar.SlowSpeed * Time.deltaTime);
                if (SkillTouchingWallRight)
                {
                    isSkill = false;
                    _anim.SetBool("Casting", false);
                    _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    SkillEffect = true;
                    
                }
            }

            if (throneType == ThroneType.Swordsman)
            {
                SkillCoolTime = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).K_CoolTime;
            }
            else if (throneType == ThroneType.Corrupted_Swordsman)
            {
                SkillCoolTime = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P3;
                Corrupted_Swordsman_SkillType = 1;
            }
        }
    }
    //소드맨 순간이동 위치와 웨폰 소환
    private void SwordsmanTeleport()
    {
        SkillEffect = true;
        //True가 오른쪽 False가 왼쪽
        bool RLChoice = (Random.value > 0.5f);
        _collider.enabled = true;
        _anim.SetBool("Skill", true);
        _rigidbody.velocity = Vector2.zero;

        if (RLChoice)
        {
            SkillMoveRLCheck = true;
            //int Pos = Random.Range(0, RSwordManPos.Count);

            //transform.localPosition = new Vector3(8.5f, 6 - Pos, 0);

            int Pos = Random.Range(0, 2);

            if (Pos == 0)
            {
                transform.localPosition = new Vector3(8.5f, 4.9f, 0);
            }
            else if (Pos == 1)
            {
                transform.localPosition = new Vector3(8.5f, -2.075f, 0);
            }
            RSwordManPos.RemoveAt(Pos);
            if (throneType == ThroneType.Swordsman)
            {
                for (int i = 0; i < myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).K_ProjectileNo; i++)
                {
                    int projectilePos = Random.Range(0, RSwordManPos.Count);
                    GameObject projectile = Instantiate(SkillProjectile[0], transform.position, Quaternion.identity);
                    //projectile.transform.parent = RSwordParent.transform.GetChild(projectilePos);
                    projectile.GetComponent<FinalBossSword_Projectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage;
                    projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
                    projectile.transform.parent = RSwordManPos[projectilePos].transform;
                    //projectile.transform.parent = MonsterProjectile.transform;
                    projectile.transform.localPosition = new Vector3(0, 0, 0);
                    projectile.transform.rotation = Quaternion.Euler(0, 0, 90f);
                    RSwordManPos.RemoveAt(projectilePos);
                }
            }
            else if (throneType == ThroneType.Corrupted_Swordsman)
            {
                for (int i = 0; i < 4; i++)
                {
                    int projectilePos = Random.Range(0, RSwordManPos.Count);
                    GameObject projectile = Instantiate(SkillProjectile[0], transform.position, Quaternion.identity);
                    //projectile.transform.parent = RSwordParent.transform.GetChild(projectilePos);
                    projectile.GetComponent<FinalBossSword_Projectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage;
                    projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
                    projectile.transform.parent = RSwordManPos[projectilePos].transform;
                    //projectile.transform.parent = MonsterProjectile.transform;
                    projectile.transform.localPosition = new Vector3(0, 0, 0);
                    projectile.transform.rotation = Quaternion.Euler(0, 0, 90f);
                    RSwordManPos.RemoveAt(projectilePos);
                }
            }
            
        }
        else
        {
            SkillMoveRLCheck = false;
            //int Pos = Random.Range(0, LSwordManPos.Count);

            //transform.localPosition = new Vector3(-8.5f, 6 - Pos, 0);

            int Pos = Random.Range(0, 2);

            if (Pos == 0)
            {
                transform.localPosition = new Vector3(-8.5f, 4.9f, 0);
            }
            else if (Pos == 1)
            {
                transform.localPosition = new Vector3(-8.5f, -2.075f, 0);
            }

            LSwordManPos.RemoveAt(Pos);

            if (throneType == ThroneType.Swordsman)
            {
                for (int i = 0; i < myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).K_ProjectileNo; i++)
                {
                    int projectilePos = Random.Range(0, LSwordManPos.Count);
                    GameObject projectile = Instantiate(SkillProjectile[0], transform.position, Quaternion.identity);
                    //projectile.transform.parent = LSwordParent.transform.GetChild(projectilePos);
                    projectile.GetComponent<FinalBossSword_Projectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                    projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
                    projectile.transform.parent = LSwordManPos[projectilePos].transform;
                    //projectile.transform.parent = MonsterProjectile.transform;
                    projectile.transform.localPosition = new Vector3(0, 0, 0);
                    projectile.transform.rotation = Quaternion.Euler(0, 0, -90f);
                    LSwordManPos.RemoveAt(projectilePos);
                }
            }
            else if (throneType == ThroneType.Corrupted_Swordsman)
            {
                for (int i = 0; i < 4; i++)
                {
                    int projectilePos = Random.Range(0, LSwordManPos.Count);
                    GameObject projectile = Instantiate(SkillProjectile[0], transform.position, Quaternion.identity);
                    //projectile.transform.parent = LSwordParent.transform.GetChild(projectilePos);
                    projectile.GetComponent<FinalBossSword_Projectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                    projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
                    projectile.transform.parent = LSwordManPos[projectilePos].transform;
                    //projectile.transform.parent = MonsterProjectile.transform;
                    projectile.transform.localPosition = new Vector3(0, 0, 0);
                    projectile.transform.rotation = Quaternion.Euler(0, 0, -90f);
                    LSwordManPos.RemoveAt(projectilePos);
                }
            }
            
        }
    }
    //소드맨 벽으로 순간이동 후 나타나는 장면 끝남 알려주는 코드
    private void SwordsmanSkillEnd()
    {
        _anim.SetBool("Skill", false);
        _anim.SetBool("Casting", false);
        SkillEffect = false;
    }

    private void WarriorSkill()
    {
        _anim.SetBool("Skill", true);
        SkillEffect = false;
        WhirlwindMoveCheck = false;
        WhirlwindTime = 5f;
        WhirlwindASPD = 1f;
        SkillEffectObject.SetActive(true);
    }
    
    private void WarriorWhirlwind()
    {
        if (!SkillEffect)
        {
            //True가 오른쪽 False가 왼쪽
            if (!WhirlwindMoveCheck)
            {
                WhirlwindRLChoice = (Random.value > 0.5f);

                WhirlwindMoveTime = Random.Range(0.5f, 1f);

                WhirlwindMoveCheck = true;
            }
            else
            {
                //PlayerPointPostion = instance.SelectCharacter.transform.localPosition.x;
                float FlipPos = instance.SelectCharacter.transform.position.x - transform.position.x;

                if (FlipPos >= 0.01f)
                {
                    transform.localScale = new Vector3(6, 6, 1);
                    RightCheck = true;
                }
                else if (FlipPos <= -0.01f)
                {
                    transform.localScale = new Vector3(-6, 6, 1);
                    RightCheck = false;
                }

                if (RightCheck)
                {
                    Vector3 dir = new Vector3(1, 0, 0);
                    transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                }
                else if (!RightCheck)
                {
                    Vector3 dir = new Vector3(-1, 0, 0);
                    transform.Translate(dir.normalized * movementSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                }

                //if (PlayerPointPostion >= 0)
                //{
                //    Vector3 dir = new Vector3(1, 0, 0);
                //    transform.Translate(dir.normalized * myChar.ThroneSkillDataMgr.GetTemplate(myChar.EnthroneHeroNum).HeroSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                //}
                //else
                //{
                //    Vector3 dir = new Vector3(-1, 0, 0);
                //    transform.Translate(dir.normalized * myChar.ThroneSkillDataMgr.GetTemplate(myChar.EnthroneHeroNum).HeroSpeed * SlowNum * myChar.SlowSpeed * Time.deltaTime);
                //}

                //if (WhirlwindRLChoice)
                //{
                //    transform.Translate(Vector2.left * myChar.ThroneSkillDataMgr.GetTemplate(myChar.EnthroneHeroNum).HeroSpeed * myChar.SlowSpeed * Time.deltaTime);
                //}
                //else
                //{
                //    transform.Translate(Vector2.right * myChar.ThroneSkillDataMgr.GetTemplate(myChar.EnthroneHeroNum).HeroSpeed * myChar.SlowSpeed * Time.deltaTime);
                //}

                WhirlwindMoveTime -= Time.deltaTime;

                if (WhirlwindMoveTime < 0)
                {
                    WhirlwindMoveCheck = false;
                }
            }

            if (WhirlwindASPD > 0)
            {
                WhirlwindASPD -= Time.deltaTime;
            }
            else
            {
                for (int i = 0; i < myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).W_ProjectileNo; i++)
                {
                    if (i == 0)
                    {
                        GameObject projectile = Instantiate(SkillProjectile[myChar.ThroneWeaponSkin], AttackPoint.position, Quaternion.identity);
                        projectile.GetComponent<FinalBossSword_Projectile>().Effect = false;
                        projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
                        projectile.GetComponent<FinalBossSword_Projectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(24 + myChar.EnthroneHeroNum).Damage * myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).W_ATK;
                        projectile.transform.parent = MonsterProjectile.transform;
                        projectile.transform.rotation = Quaternion.Euler(0, 0, 90f);

                        GameObject projectile_2 = Instantiate(SkillProjectile[myChar.ThroneWeaponSkin], AttackPoint.position, Quaternion.identity);
                        projectile_2.GetComponent<FinalBossSword_Projectile>().Effect = false;
                        projectile_2.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
                        projectile_2.GetComponent<FinalBossSword_Projectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(24 + myChar.EnthroneHeroNum).Damage * myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).W_ATK;
                        projectile_2.transform.parent = MonsterProjectile.transform;
                        projectile_2.transform.rotation = Quaternion.Euler(0, 0, -90f);
                    }
                    StartCoroutine(WarriorWhirlwindProjectile(i * 0.15f));
                    
                    //float Angle = Random.Range(-75, 75);
                    //GameObject projectile = Instantiate(SkillProjectile[myChar.ThroneWeaponSkin], AttackPoint.position, Quaternion.identity);
                    //projectile.GetComponent<FinalBossSword_Projectile>().Effect = false;
                    //projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = SkillNo;
                    //projectile.GetComponent<FinalBossSword_Projectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(24 + myChar.EnthroneHeroNum).Damage * myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).W_ATK;
                    //projectile.transform.parent = MonsterProjectile.transform;
                    //projectile.transform.rotation = Quaternion.Euler(0, 0, Angle);
                }

                WhirlwindASPD = 1f;
            }
        }
    }
    private void WarriorWhirlwindEnd()
    {
        isSkill = false;
        _anim.SetBool("Casting", false);
        _anim.SetBool("Skill", false);
        SkillEffectObject.SetActive(false);
        SkillCoolTime = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).W_CoolTime;
    }

    private void ArcherSkill()
    {
        //transform.localPosition = new Vector3(0, -4.0f, 0);

        //_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        _rigidbody.velocity = Vector3.zero;
    }

    public void ArcherArrowRain()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i != 9)
            {
                StartCoroutine(ArrowRainProjectile((i * 0.3f), false));
            }
            else
            {
                StartCoroutine(ArrowRainProjectile((i * 0.3f), true));
            }
        }
        //for (int i = 0; i < myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).A_ProjectileNo; i++)
        //{
        //    if (i != myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).A_ProjectileNo - 1)
        //    {
        //        StartCoroutine(ArrowRainProjectile((i * 0.3f), false));
        //    }
        //    else
        //    {
        //        StartCoroutine(ArrowRainProjectile((i * 0.3f), true));
        //    }
        //}
    }

    public void ArcherTeleport()
    {
        //True가 오른쪽 False가 왼쪽
        bool RLChoice = (Random.value > 0.5f);        

        SkillEffect = true;
        _collider.enabled = true;
        _anim.SetBool("Skill", true);
        _rigidbody.velocity = Vector2.zero;

        if (RLChoice)
        {
            SkillMoveRLCheck = true;
            //int Pos = Random.Range(0, 7);
            //transform.localPosition = new Vector3(14.5f, 9 - Pos, 0);
            transform.localPosition = new Vector3(8.5f, 4.9f, 0);
        }
        else
        {
            SkillMoveRLCheck = false;
            //int Pos = Random.Range(0, 7);
            //transform.localPosition = new Vector3(-14.5f, 9 - Pos, 0);
            transform.localPosition = new Vector3(-8.5f, 4.9f, 0);
        }
    }
    private void ArcherTargetShot()
    {
        _anim.SetBool("Attack", true);
    }
    private void ArcherTargetShotEnd()
    {
        if (FinalBossTargeting.Instance.TargetShotCnt >= 3)
        {
            _anim.SetBool("Attack", false);
            ArcherArrowRainEnd();
        }
        else
        {
            TargettingCheck = true;
            _anim.SetBool("Attack", false);
            _anim.SetBool("Casting", false);
        }
    }
    private void ArcherArrowRainEnd()
    {
        //isSkill = false;
        _anim.SetBool("Casting", false);
        _anim.SetBool("Skill", false);
        _collider.enabled = true;
        //SkillEffectObject.SetActive(false);
        //_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        SkillCoolTime = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P2;
        //SkillCoolTime = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).A_CoolTime;
    }

    public void ArrowShot()     //화살 위로 날려주는 스킬모션중하나
    {
        float ArrowSpeed = Random.Range(2f, 3f);
        GameObject projectile = Instantiate(SkillProjectile[myChar.ThroneWeaponSkin], AttackPoint.position, Quaternion.identity);
        projectile.GetComponent<FinalBossSword_Projectile>().Effect = false;
        projectile.GetComponent<FinalBossSword_Projectile>().ArcherShot = true;
        projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
        projectile.GetComponent<FinalBossSword_Projectile>().ArcherArrowLastCheck = false;
        projectile.GetComponent<FinalBossSword_Projectile>().ArrowSpeed = ArrowSpeed;
        projectile.GetComponent<FinalBossSword_Projectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
        projectile.transform.parent = MonsterProjectile.transform;
    }
    public void ArrowLastShot()
    {
        float ArrowSpeed = Random.Range(2f, 3f);
        GameObject projectile = Instantiate(SkillProjectile[myChar.ThroneWeaponSkin], AttackPoint.position, Quaternion.identity);
        projectile.GetComponent<FinalBossSword_Projectile>().Effect = false;
        projectile.GetComponent<FinalBossSword_Projectile>().ArcherShot = true;
        projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
        projectile.GetComponent<FinalBossSword_Projectile>().ArcherArrowLastCheck = true;
        projectile.GetComponent<FinalBossSword_Projectile>().FinalBoss = transform.gameObject;
        projectile.GetComponent<FinalBossSword_Projectile>().ArrowSpeed = ArrowSpeed;
        projectile.GetComponent<FinalBossSword_Projectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
        projectile.transform.parent = MonsterProjectile.transform;        
    }

    private void WizardSkill()
    {
        WizardSkillCnt = 2;

        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        _rigidbody.velocity = Vector3.zero;
    }
    private void FreezeAll()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        _rigidbody.velocity = Vector3.zero;
    }
    private void WizardTeleport()
    {
        transform.localPosition = new Vector3(0, 2.5f, 0);
    }
    private void WizardCastingEnd()
    {
        _anim.SetBool("Skill", true);
    }
    
    private void WizardFireStrike()
    {
        for (int i = 0; i < myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).Wi_ProjectileNo; i++)
        {
            GameObject projectile = Instantiate(SkillProjectile[myChar.ThroneWeaponSkin], AttackPoint.position, Quaternion.identity);
            projectile.transform.rotation = Quaternion.Euler(0, 0, ((360f / myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).Wi_ProjectileNo) * i));
            projectile.GetComponent<FinalBossSword_Projectile>().Effect = false;
            projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
            projectile.GetComponent<FinalBossSword_Projectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(24 + myChar.EnthroneHeroNum).Damage * myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).Wi_ATK;
            projectile.transform.parent = MonsterProjectile.transform;
        }
    }
    private void WizardFireStrikeEndCheck()
    {
        WizardSkillCnt--;
        if (WizardSkillCnt <= 0)
        {
            isSkill = false;
            _anim.SetBool("Casting", false);
            _anim.SetBool("Skill", false);
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            SkillCoolTime = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).Wi_CoolTime;
        }
    }

    private void NinjaShot()
    {
        if (SkillMoveRLCheck)
        {
            float randomY = Random.Range(0.15f, 1.8f);
            GameObject projectile = Instantiate(SkillProjectile[myChar.ThroneWeaponSkin], AttackPoint.position, Quaternion.identity);
            projectile.GetComponent<FinalBossSword_Projectile>().Effect = false;
            projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
            //projectile.GetComponent<FinalBossSword_Projectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(24 + myChar.EnthroneHeroNum).Damage * myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).N_ATK;
            projectile.transform.parent = MonsterProjectile.transform;
            projectile.GetComponentInChildren<ProjectileRotate>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(24 + myChar.EnthroneHeroNum).Damage * myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).N_ATK; ;
            projectile.GetComponent<FinalBossSword_Projectile>().VecX = -3f;
            projectile.GetComponent<FinalBossSword_Projectile>().VecY = randomY;
            projectile.GetComponent<FinalBossSword_Projectile>().speed = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).N_ProjectileSpeed * 100;
        }
        else
        {
            float randomY = Random.Range(0.15f, 1.8f);
            GameObject projectile = Instantiate(SkillProjectile[myChar.ThroneWeaponSkin], AttackPoint.position, Quaternion.identity);
            projectile.GetComponent<FinalBossSword_Projectile>().Effect = false;
            projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
            //projectile.GetComponent<FinalBossSword_Projectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(24 + myChar.EnthroneHeroNum).Damage * myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).N_ATK;
            projectile.transform.parent = MonsterProjectile.transform;
            projectile.GetComponentInChildren<ProjectileRotate>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(24 + myChar.EnthroneHeroNum).Damage * myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).N_ATK; ;
            projectile.GetComponent<FinalBossSword_Projectile>().VecX = 3f;
            projectile.GetComponent<FinalBossSword_Projectile>().VecY = randomY;
            projectile.GetComponent<FinalBossSword_Projectile>().speed = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).N_ProjectileSpeed * 100;
        }
        NinjaSkillCnt--;
        
    }
    private void NinjaShotEndcheck()
    {
        if (NinjaSkillCnt <= 0)
        {
            isSkill = false;
            _anim.SetBool("Casting", false);
            _anim.SetBool("Skill", false);
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            SkillCoolTime = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).N_CoolTime;
            SkillEffectObject.SetActive(false);
            OutLineOff();
        }
    }
    private void NinjaSkill()
    {
        SkillEffect = true;
        _collider.enabled = true;
        NinjaSkillCnt = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).N_ProjectileNo;
        SkillEffectObject.SetActive(true);

        //_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        _rigidbody.velocity = Vector3.zero;
    }
    private void NinjaTeleport()
    {
        //True가 오른쪽 False가 왼쪽
        bool RLChoice = (Random.value > 0.5f);

        //SkillEffect = true;
        //_collider.enabled = true;
        //NinjaSkillCnt = myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).N_ProjectileNo;
        //_anim.SetBool("Casting", true);
        //_rigidbody.velocity = Vector2.zero;
        //SkillEffectObject.SetActive(true);

        if (RLChoice)
        {
            SkillMoveRLCheck = true;
            transform.localPosition = new Vector3(13.5f, -4, 0);
        }
        else
        {
            SkillMoveRLCheck = false;
            transform.localPosition = new Vector3(-13.5f, -4, 0);
        }
    }
    private void NinjaCastingEnd()
    {
        _anim.SetBool("Skill", true);
    }

    public void SkillEffectOff()
    {
        SkillEffectObject.SetActive(false);
    }
    private void CenterTeleport()
    {
        transform.localPosition = new Vector3(0, 0.5f, 0);
    }
    private void Corrupted_Swordsman_Casting2End()
    {
        _anim.SetBool("Skill2", true);
        Corrupted_SwordsmanStrikeCnt = 4;
    }
    private void Corrupted_SwordsmanFireStrike()
    {
        if (Corrupted_SwordsmanStrikeCnt%2 == 0)
        {
            for (int i = 0; i < 360; i+= 45)
            {
                GameObject projectile = Instantiate(SkillProjectile[1], AttackPoint.position, Quaternion.identity);
                projectile.transform.rotation = Quaternion.Euler(0, 0, i);
                projectile.GetComponent<MonsterProjectile>().BoltSpeed = 2.5f;
                projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage; 
                projectile.transform.parent = MonsterProjectile.transform;
            }
        }
        else
        {
            for (int i = 0; i < 360; i += 45)
            {
                GameObject projectile = Instantiate(SkillProjectile[1], AttackPoint.position, Quaternion.identity);
                projectile.transform.rotation = Quaternion.Euler(0, 0, 22.5f + i);
                projectile.GetComponent<MonsterProjectile>().BoltSpeed = 2.5f;
                projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
                projectile.transform.parent = MonsterProjectile.transform;
            }
        }
    }    
    private void Corrupted_SwordsmanFireStrikeEndCheck()
    {
        Corrupted_SwordsmanStrikeCnt--;
        if (Corrupted_SwordsmanStrikeCnt <= 0)
        {
            isSkill = false;
            _anim.SetBool("Casting2", false);
            _anim.SetBool("Skill2", false);
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            SkillCoolTime = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).P2;
            Corrupted_Swordsman_SkillType = 0;
        }
    }
    public void ColliderEnabled()
    {
        _collider.enabled = false;
        if (throneType == ThroneType.Archer)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    public void IndividualSlowCheck(float Time)
    {
        StartCoroutine(SlowActive(Time));
    }
    private void CrownCreate()
    {
        GameObject CrownItem = Instantiate(Crown, transform.Find("Crown").transform.position, Quaternion.identity);
        CrownItem.transform.parent = transform.parent.parent.parent;
        CrownItem.transform.localScale = new Vector2(6, 6);
        GameObject BoxKeyItem = Instantiate(BoxKey, transform.Find("KeyPos").transform.position, Quaternion.identity);
        BoxKeyItem.transform.parent = transform.parent.parent.parent;
        BoxKeyItem.transform.localScale = new Vector2(6, 6);
    }
    private void EnthroneBossKill()
    {
        myChar.CrownGetBgm = true;
    }
    private void RoomNum_1()
    {
        if (instance.RoomManagerObj.activeSelf)
        {
            RoomManager.Instance.RoomNum = 1;
        }        
    }
    private void RoomNum_2()
    {
        if (instance.RoomManagerObj.activeSelf)
        {
            RoomManager.Instance.RoomNum = 2;
        }
    }
    private void RoomNum_3()
    {
        if (instance.RoomManagerObj.activeSelf)
        {
            RoomManager.Instance.RoomNum = 3;
        }
    }
    private void HitEffectEnd()
    {
        _shader.DisableKeyword("HITEFFECT_ON");
    }
    private void OutLineOn()
    {
        _shader.EnableKeyword("OUTBASE_ON");
    }
    private void OutLineOff()
    {
        _shader.DisableKeyword("OUTBASE_ON");
    }

    private void CrownDropSound()
    {
        SoundManager.Instance.PlaySfx(21);
    }
    private void SFXSound(int Num)
    {
        SoundManager.Instance.PlaySfx(Num);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
        Gizmos.DrawWireSphere(new Vector3(groundCheck.position.x, groundCheck.position.y + +0.5f), 0.1f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(AttackPoint.position, MoveCheckRange);

        Gizmos.DrawWireCube(wallCheck.position, new Vector3(0.56f, wallCheckDistance, wallCheck.position.z));
        //Gizmos로 보이는것보다 Physics2D가 0.04정도 더 크기때문에 Physics2D 0.5라면 Gizmos는 0.54로 수치를 맞추면 보이는것과 동일함
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(wallCheckLeft.position.x - 0.14f, wallCheckLeft.position.y - 0.01f, wallCheckLeft.position.z), new Vector3(0.28f, 0.54f, wallCheckLeft.position.z));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(wallCheckRight.position.x + 0.14f, wallCheckRight.position.y - 0.01f, wallCheckRight.position.z), new Vector3(0.28f, 0.54f, wallCheckRight.position.z));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(wallCheckLeft.position.x - 0.14f, wallCheckLeft.position.y - 0.01f, wallCheckLeft.position.z), new Vector3(0.28f, 0.2f, wallCheckLeft.position.z));
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(new Vector3(wallCheckRight.position.x + 0.14f, wallCheckRight.position.y - 0.01f, wallCheckRight.position.z), new Vector3(0.28f, 0.2f, wallCheckRight.position.z));
    }
    IEnumerator WarriorWhirlwindProjectile(float Time)
    {
        yield return new WaitForSeconds(Time);

        float Angle = Random.Range(-75, 75);
        GameObject projectile = Instantiate(SkillProjectile[myChar.ThroneWeaponSkin], AttackPoint.position, Quaternion.identity);
        projectile.GetComponent<FinalBossSword_Projectile>().Effect = false;
        projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
        projectile.GetComponent<FinalBossSword_Projectile>().Damage = GameManager.Instance.UnitDataMgr.GetTemplate(24 + myChar.EnthroneHeroNum).Damage * myChar.ThroneSkillDataMgr.GetTemplate(myChar.SkillNo).W_ATK;
        projectile.transform.parent = MonsterProjectile.transform;
        projectile.transform.rotation = Quaternion.Euler(0, 0, Angle);
    }
    IEnumerator ArrowRainProjectile(float Time, bool LastProjcetileCheck)
    {
        yield return new WaitForSeconds(Time);

        int projectilePos = Random.Range(0, TopArrowPos.Count);
        //GameObject projectile = Instantiate(SkillProjectile[myChar.ThroneWeaponSkin], transform.position, Quaternion.identity);
        GameObject projectile = Instantiate(SkillProjectile[0], transform.position, Quaternion.identity);
        //projectile.transform.parent = RSwordParent.transform.GetChild(projectilePos);
        projectile.GetComponent<FinalBossSword_Projectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
        projectile.GetComponent<FinalBossSword_Projectile>().Effect = false;
        projectile.GetComponent<FinalBossSword_Projectile>().ArcherShot = false;
        projectile.GetComponent<FinalBossSword_Projectile>().SkillNo = myChar.SkillNo;
        projectile.transform.parent = TopArrowPos[projectilePos].transform;
        projectile.transform.localPosition = new Vector3(0, 0, 0);
        projectile.transform.rotation = Quaternion.Euler(0, 0, 180f);
        if (LastProjcetileCheck)
        {
            projectile.GetComponent<FinalBossSword_Projectile>().FinalBoss = transform.gameObject;
            projectile.GetComponent<FinalBossSword_Projectile>().ArcherArrowLastCheck = true;
        }
        else
        {
            projectile.GetComponent<FinalBossSword_Projectile>().ArcherArrowLastCheck = false;
        }
        if (MonsterIndex == 41)
        {
            SFXSound(37);
        }
    }
    IEnumerator SlowActive(float Time)              //슬로우아이템
    {
        SlowNum = 0.5f;
        yield return new WaitForSeconds(Time);
        SlowNum = 1f;
    }
    IEnumerator Pattern_1()
    {
        PatternNum = Random.Range(0, 1);

        switch (PatternNum)
        {
            case 0:
                timeNum = 8f;
                break;
        }
        yield return new WaitForSeconds(timeNum);
        isPattern = false;
    }
}
