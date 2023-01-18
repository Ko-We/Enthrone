using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerController>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerController");
                    instance = instanceContainer.AddComponent<PlayerController>();
                }
            }
            return instance;
        }
    }

    MyObject myChar;
    private static PlayerController instance;
    public int TestRan1, TestRan2;

    private float movementInputDirection;

    private Rigidbody2D _rigidbody;
    public Animator _anim;

    public int amountOfJumpsLeft;

    private bool isRightDir = true;     //isFacingRight
    private bool isMoving;              //isWalking
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallRight;
    private bool isTouchingWallLeft;
    private bool isMonster;
    private bool isWallSliding;
    private bool isJump = true;         //canJump;
    private bool isJumping = false;
    private bool isSlow = false;
    private bool Sliding = false;
    private bool isResurrection = false;

    public bool HitAttack = true;
    public bool isJumpShoot = false;
    public bool isCrown = false;

    public int amountOfJumps; //점프 횟수

    public int PlayerHp;
    public float btnmoveSpeed = 0;

    public float SlowNum = 1;
    public float WindWalk = 0;
    public float movementSpeed = 3.5f;
    public float JumpForce;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallCheckDistanceRL;
    public float wallSlideSpeed;

    public Transform groundCheck;
    public Transform wallCheck;
    public Transform wallCheckRight;
    public Transform wallCheckLeft;

    public GameObject DeathEffect;
    public GameObject ShockWave;
    public GameObject FootDust;

    private IEnumerator SlowCoroutine;


    public LayerMask _layerMask;
    //public LayerMask _layerMask2;

    private void Awake()
    {
        myChar = MyObject.MyChar;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerHp = myChar.maxHp;
        myChar.currentHp = PlayerHp;
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        SlowCoroutine = SlowEffect(2f, 0.25f);
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"), true);
    }


    private void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (myChar.SelectedStage == 1)
        {
            if (PlayerTargeting.Instance.MonsterList.Count <= 0)
            {
                GameManager.Instance.StageClear();
            }
        }
        FrozenEffect();
        CrownCheck();

        PlayerHp = myChar.currentHp;
        if (!myChar.Wizard)
        {
            JumpForce = myChar.JumpForce;
        }
        else if (myChar.Wizard)
        {
            JumpForce = myChar.JumpForce * 1.5f;
        }

        amountOfJumps = myChar.JumpCnt;

        if (myChar.currentHp > 0)
        {
#if UNITY_IPHONE && !UNITY_EDITOR
         BtnMovement();
#elif UNITY_ANDROID && !UNITY_EDITOR
         BtnMovement();
#else
            ApplyMovement();
            CheckInput();
#endif
            //BtnMovement();
            //ApplyMovement();

            //빌드시 뺴야함
            if (Input.GetKeyDown(KeyCode.X))
            {
                GameManager.Instance.Flash();
                GameManager.Instance.ShockWave();
                SoundManager.Instance.PlaySfx(24);
                GameManager.Instance.MonsterDamage(10000f);

                Instantiate(ShockWave, myChar.SelectLocation.transform.position, Quaternion.identity);
            }
            //------------------------------------------
            SkinChekc();            
            
            WindWalk = myChar.WindWalkSpeed;            
            CheckSurroundings();
            FlipCheck();
            UpdateAnimations();
            CheckIfCanJump();
            CheckIfWallSliding();

            if (myChar.Resurrection)
            {
                Time.timeScale = 1f;
                _anim.updateMode = AnimatorUpdateMode.Normal;

                StartCoroutine(Resurrection(3f));
                _anim.SetBool("Death", false);
                isResurrection = true;
                myChar.Resurrection = false;
            }
        }

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    var graphtoscan = AstarPath.active.data.gridGraph;
        //    AstarPath.active.Scan(graphtoscan);

        //    AstarPath.active.Scan();
        //}
        //currentOrb = myChar.EquipmentWeapon[myChar.SelectHero];
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.transform.CompareTag("Monster"))
        //{
        //    Debug.Log("1 : "+GameManager.Instance.UnitDataMgr.GetTemplate(col.gameObject.GetComponent<MonsterScript>().MonsterIndex).Damage);
        //}
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        //if (col.transform.CompareTag("YetiKingWall"))
        //{
        //    if (_rigidbody.velocity.x != 0)
        //    {
        //        if (_rigidbody.velocity.x > -0.5f)
        //        {
        //            _rigidbody.AddForce(new Vector2(5f,0));
        //            Debug.Log(_rigidbody.velocity.x);
        //            if (_rigidbody.velocity.x >= 5f)
        //            {
                        
                        
        //            }
        //        }
        //    }
            
        //}
    }

    private void OnTriggerStay2D(Collider2D col)
    {        
        if (col.transform.CompareTag("SlowTrap"))
        {
            isSlow = true;
            SlowNum = 0.25f;
        }
        myChar.StayEquipment = col.gameObject;
        if (col.transform.CompareTag("AD_Merchant"))
        {
            GameManager.Instance.AdMerchin = col.gameObject;
            GameManager.Instance.AD_Btn.SetActive(true);
            GameManager.Instance.Select_Btn.SetActive(false);
            GameManager.Instance.Jump_Btn.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.transform.CompareTag("SlowTrap"))
        {
            isSlow = false;
            SlowNum = 1f;
        }
        if (col.transform.CompareTag("AD_Merchant"))
        {
            GameManager.Instance.AD_Btn.SetActive(false);
            GameManager.Instance.Select_Btn.SetActive(false);
            GameManager.Instance.Jump_Btn.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.transform.CompareTag("NextRoom"))
        {
            //맵이동 문Panel관련
            //RoomManager.Instance.NextStage();
            if (myChar.Stage < 50)
            {
                GameManager.Instance.DoorPanel.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else
            {
                RoomManager.Instance.NextStage();
            }
            
        }
    }


    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "Monster")
    //    {
    //        Debug.Log("oh");
    //        //col.collider.SendMessage("PlayerHit", 1);
    //        PlayerHit(1);
    //    }
    //}
    public void PlayerHit(float Damage)
    {
        if (HitAttack)
        {
            SoundManager.Instance.PlaySfx(8);
            HitAttack = false;
            if (myChar.Shield > 0)
            {
                myChar.Shield--;
                GameObject up_Text = Instantiate(GameManager.Instance.PlayerHP_UpText, transform.position, Quaternion.identity);
                up_Text.GetComponent<Text>().text = "-1";
                up_Text.transform.SetParent(GameManager.Instance.PlayerShield.transform.Find("UpText"));
                up_Text.transform.localScale = new Vector3(1, 1, 1);
                up_Text.transform.localPosition = new Vector3(0, 0, 0);
                Destroy(up_Text, 0.5f);

                if (GameManager.Instance.PlayerSkill[20] >= 1)
                {
                    //Instantiate(ShockWave, myChar.SelectLocation.transform.position, Quaternion.identity);
                    //Instantiate(ShockWave, SWTR.position, Quaternion.identity);
                    GameManager.Instance.ShockWave();
                }

            }
            else if (myChar.Shield <= 0)
            {
                myChar.currentHp -= Mathf.RoundToInt(Damage);
                GameObject up_Text = Instantiate(GameManager.Instance.PlayerHP_UpText, transform.position, Quaternion.identity);
                up_Text.GetComponent<Text>().text = "-" + Mathf.RoundToInt(Damage);
                up_Text.transform.SetParent(GameManager.Instance.PlayerHP.transform.Find("UpText"));
                up_Text.transform.localScale = new Vector3(1, 1, 1);
                up_Text.transform.localPosition = new Vector3(0, 0, 0);
                Destroy(up_Text, 0.5f);
                if (myChar.Tutorial)
                {
                    if (myChar.currentHp <= 0)
                    {
                        myChar.currentHp = 1;
                    }
                    
                }
            }

            if (myChar.currentHp <= 0)
            {
                if (myChar.currentHp < 0)
                {
                    myChar.currentHp = 0;
                }
                SoundManager.Instance.PlaySfx(1);
                _anim.updateMode = AnimatorUpdateMode.UnscaledTime;

                if (GameManager.Instance.PlayerSkill[5] >= 1)
                {
                    myChar.currentHp = myChar.maxHp;
                    GameManager.Instance.PlayerSkill[5] = 0;
                }
                else if (GameManager.Instance.PlayerSkill[5] == 0)
                {
                    //GameManager.Instance.InfoWindow.SetActive(true);
                    _anim.SetBool("Death", true);

                    if (!myChar.Resurrection)
                    {
                        //_anim.updateMode = AnimatorUpdateMode.UnscaledTime;
                        //GameManager.Instance.InfoWindow.SetActive(true);
                    }
                    //else if (myChar.Resurrection)
                    //{
                    //    StartCoroutine(LobbyGo(3f));
                    //}
                }
                myChar.Reflection = false;
                myChar.EffectOnCheck = false;
                myChar.ASPDPotion = false;
                myChar.PowerPotion = false;
                myChar.InvinciblePotion = false;
            }
            //StartCoroutine(HitCheck());
            transform.GetComponent<PlayerDamage>().Damage();
        }
        else
        {
            return;
        }
    }

    private void SkinChekc()
    {
        switch (myChar.EquipmentCostume[myChar.SelectHero])
        {
            case 0:
                for (int i = 0; i < 5; i++)
                {
                    _anim.SetLayerWeight(i + 3, 0);
                }
                break;
            case 1:
                for (int i = 1; i <= 5; i++)
                {
                    if (i == myChar.EquipmentCostume[myChar.SelectHero])
                    {
                        _anim.SetLayerWeight(i + 2, 1);
                    }
                    else
                    {
                        _anim.SetLayerWeight(i + 2, 0);
                    }
                }
                break;
            case 2:
                for (int i = 1; i <= 5; i++)
                {
                    if (i == myChar.EquipmentCostume[myChar.SelectHero])
                    {
                        _anim.SetLayerWeight(i + 2, 1);
                    }
                    else
                    {
                        _anim.SetLayerWeight(i + 2, 0);
                    }
                }
                break;
            case 3:
                for (int i = 1; i <= 5; i++)
                {
                    if (i == myChar.EquipmentCostume[myChar.SelectHero])
                    {
                        _anim.SetLayerWeight(i + 2, 1);
                    }
                    else
                    {
                        _anim.SetLayerWeight(i + 2, 0);
                    }
                }
                break;
            case 4:
                for (int i = 1; i <= 5; i++)
                {
                    if (i == myChar.EquipmentCostume[myChar.SelectHero])
                    {
                        _anim.SetLayerWeight(i + 2, 1);
                    }
                    else
                    {
                        _anim.SetLayerWeight(i + 2, 0);
                    }
                }
                break;
            case 5:
                for (int i = 1; i <= 5; i++)
                {
                    if (i == myChar.EquipmentCostume[myChar.SelectHero])
                    {
                        _anim.SetLayerWeight(i + 2, 1);
                    }
                    else
                    {
                        _anim.SetLayerWeight(i + 2, 0);
                    }
                }
                break;
        }
    }
    private void RessurectionPop()
    {
        if (myChar.NumberOfResurections <= 1)
        {
            if (myChar.SelectHero == 0)
            {
                StartCoroutine(TimeControll(0.3f));
            }
            else
            {
                StartCoroutine(TimeControll(0f));
            }
            GameManager.Instance.InfoWindow.SetActive(true);
        }
        else
        {
            if (myChar.SelectHero == 0)
            {
                StartCoroutine(TimeControll(0.3f));
            }
            else
            {
                StartCoroutine(TimeControll(0f));
            }
            GameManager.Instance.Fail_ResultPanel.SetActive(true);
        }
    }

    // 바닥에 접지중인지 확인
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, _layerMask);

        //선형으로 체크해서 몸통중앙에 선이 닿아야 체크가능
        //isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, _layerMask);
        //isTouchingWallRight = Physics2D.Raycast(wallCheckRight.position, transform.right, wallCheckDistanceRL, _layerMask);
        //isTouchingWallLeft = Physics2D.Raycast(wallCheckLeft.position, transform.right, -wallCheckDistanceRL, _layerMask);

        //상자식으로 체크해서 발끝이나 머리끝부터 체크가능
        isTouchingWall = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.51f, wallCheckDistance - 0.04f), 0, _layerMask);
        isTouchingWallRight = Physics2D.OverlapBox(new Vector2(wallCheckRight.position.x + 0.14f, wallCheckRight.position.y - 0.01f), new Vector2(0.28f, 0.5f), 0, _layerMask);
        isTouchingWallLeft = Physics2D.OverlapBox(new Vector2(wallCheckLeft.position.x - 0.14f, wallCheckLeft.position.y - 0.01f), new Vector2(0.28f, 0.5f), 0, _layerMask);
        //isMonster = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.45f, 0.51f), 0, _layerMask2);
    }

    //슬라이딩 지점
    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded /*&& _rigidbody.velocity.y < 0*/)
        {
            isWallSliding = true;

            if (isTouchingWallRight)
            {
                if (transform.localScale.x > 0)
                {
                    Flip();
                }
            }
            else if (isTouchingWallLeft)
            {
                if (transform.localScale.x < 0)
                {
                    Flip();
                }
            }
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && _rigidbody.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (amountOfJumpsLeft <= 0)
        {
            isJump = false;
        }
        else
        {
            isJump = true; //canJump;
        }
    }

    //움직이는방향으로 Flip 확인
    private void FlipCheck()
    {
        //키보드조작용
        if (isRightDir && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isRightDir && movementInputDirection > 0)
        {
            Flip();
        }
        //버튼조작용
        if (isRightDir && btnmoveSpeed < 0)
        {
            Flip();
        }
        else if (!isRightDir && btnmoveSpeed > 0)
        {
            Flip();
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

    private void UpdateAnimations()
    {
        if (!myChar.Wizard)
        {            
            //switch (myChar.SelectSkin)
            //{
            //    case 0:
            //        _anim.SetLayerWeight(3, 0);
            //        _anim.SetLayerWeight(4, 0);
            //        break;
            //    case 1:
            //        _anim.SetLayerWeight(3, 1);
            //        _anim.SetLayerWeight(4, 0);
            //        break;
            //    case 2:
            //        _anim.SetLayerWeight(3, 0);
            //        _anim.SetLayerWeight(4, 1);
            //        break;
            //    default:
            //        break;
            //}
            _anim.SetBool("Move", isMoving);
            _anim.SetBool("isWallSliding", isWallSliding);

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
                if (isJumpShoot)
                {
                    transform.GetComponent<PlayerTargeting>().JumpShoot();
                }
            }
        }
    }
    //PC 조작용
    public void CheckInput()
    {
        //movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (!myChar.Wizard && !myChar.Fly)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _rigidbody.gravityScale = 3;
                Jump();
            }
        }
        else if (myChar.Wizard || myChar.Fly)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce / 3);
                isJumping = true;
                _rigidbody.gravityScale = 0;
            }
        }

        if (myChar.Wizard || myChar.Fly)
        {
            if (isJumping)
            {
                isJumping = false;
                _rigidbody.gravityScale = 1;
                if (Input.GetKeyUp(KeyCode.Z))
                {
                    isJumping = false;
                    _rigidbody.gravityScale = 1;
                }
            }
        }
    }

    //폰 조작용
    public void CheckInputPhone()
    {
        //movementInputDirection = Input.GetAxisRaw("Horizontal");
        if (!myChar.Wizard && !myChar.Fly)
        {
            _rigidbody.gravityScale = 3;
            Jump();
        }
        else if (myChar.Wizard || myChar.Fly)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce / 3);
            isJumping = true;
            _rigidbody.gravityScale = 0;
        }

        if (myChar.Wizard || myChar.Fly)
        {
            if (isJumping)
            {
                isJumping = false;
                _rigidbody.gravityScale = 1;
            }
        }
    }

    //점프
    public void Jump()
    {
        if (isJump && isTouchingWall && _rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce);
            SoundManager.Instance.PlaySfx(7);
        }
        else if (isJump && !isWallSliding)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce);
            SoundManager.Instance.PlaySfx(7);
            amountOfJumpsLeft--;
        }
        else if (isJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce);
            SoundManager.Instance.PlaySfx(7);
        }
        if (isJumpShoot)
        {
            transform.GetComponent<PlayerTargeting>().JumpShoot();
        }

    }

    public void BtnMovement()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        //transform.Translate(new Vector2(movementInputDirection, 0).normalized * (movementSpeed + WindWalk) * second);
        // (기본속도 * windwalk * slow);
        _rigidbody.velocity = new Vector2(((movementSpeed * btnmoveSpeed) * WindWalk) * SlowNum, _rigidbody.velocity.y);

        if (isWallSliding)
        {
            if (_rigidbody.velocity.y < -wallSlideSpeed)
            {
                amountOfJumpsLeft = amountOfJumps;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, (-wallSlideSpeed * WindWalk) * (SlowNum));
            }
        }
    }

    //이동
    private void ApplyMovement()
    {
        
        //CheckInput에 있던 movementInputDirection = Input.GetAxisRaw("Horizontal");를 movement로 옮김
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        //transform.Translate(new Vector2(movementInputDirection, 0).normalized * (movementSpeed + WindWalk) * second);
        // (기본속도 * windwalk * slow);
        _rigidbody.velocity = new Vector2(((movementSpeed * movementInputDirection) * WindWalk) * SlowNum, _rigidbody.velocity.y);
        //Debug.Log(_rigidbody.velocity.x);
        if (isWallSliding)
        {
            if (_rigidbody.velocity.y < -wallSlideSpeed)
            {
                amountOfJumpsLeft = amountOfJumps;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, (-wallSlideSpeed * WindWalk) * (SlowNum));
            }
        }
    }
    private void FrozenEffect()
    {
        if (isSlow)
        {
            //SlowNum = 0.25f;
            transform.Find("Effect").GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            //SlowNum = 1f;
            transform.Find("Effect").GetChild(0).gameObject.SetActive(false);
        }
    }
    private void ObtainOff()
    {
        _anim.SetBool("Obtain", false);
    }

    private void CrownCheck()
    {
        if (isCrown)
        {
            transform.Find("Crown").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("Crown").gameObject.SetActive(false);
        }
    }
    private void Flip()
    {

        // 오브젝트 반전
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        //wallCheck.localPosition *= -1;

        // 시선 정보 변경
        isRightDir = !isRightDir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        // DrawLine로 할경우 WallCheck의 Position.x값을 -0.093 변경해야됨 WallCheckDistance는 0.56이 적당함
        //Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(wallCheckRight.position, new Vector3(wallCheckRight.position.x + wallCheckDistanceRL, wallCheckRight.position.y, wallCheckRight.position.z));
        //Gizmos.color = Color.green;        
        //Gizmos.DrawLine(wallCheckLeft.position, new Vector3(wallCheckLeft.position.x - wallCheckDistanceRL, wallCheckLeft.position.y, wallCheckLeft.position.z));


        Gizmos.DrawWireCube(wallCheck.position, new Vector3(0.56f, wallCheckDistance, wallCheck.position.z));
        //Gizmos로 보이는것보다 Physics2D가 0.04정도 더 크기때문에 Physics2D 0.5라면 Gizmos는 0.54로 수치를 맞추면 보이는것과 동일함
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(wallCheckLeft.position.x - 0.14f, wallCheckLeft.position.y - 0.01f, wallCheckLeft.position.z), new Vector3(0.28f, 0.54f, wallCheckLeft.position.z));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(wallCheckRight.position.x + 0.14f, wallCheckRight.position.y - 0.01f, wallCheckRight.position.z), new Vector3(0.28f, 0.54f, wallCheckRight.position.z));

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireCube(wallCheck.position, new Vector2(0.45f, 0.51f));

    }
    public void DamageEnd()
    {
        HitAttack = true;
    }
    public void SlowUse(float Sec , float Slowper)
    {
        StopCoroutine(SlowCoroutine);
        SlowCoroutine = SlowEffect(Sec, Slowper);
        StartCoroutine(SlowCoroutine);
    }

    private void HeroDeathEffect()
    {
        GameObject Effect = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(Effect, 2f);
    }
    private void SwordManDeath()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce / 2f);
    }
    public void SFXHeroSound(int Num)
    {
        SoundManager.Instance.PlaySfx(Num);
    }
    IEnumerator LobbyGo(float Time)
    {
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene("LobbiScene");
        //myChar.Chapter = 0;
        myChar.Stage = 0;
        myChar.SelectedStage = 0;
        myChar.Resurrection = false;
    }
    public IEnumerator SlowEffect(float Time, float slowper)
    {
        isSlow = true;
        SlowNum = slowper;
        yield return new WaitForSeconds(Time);
        SlowNum = 1f;
        isSlow = false;
    }
    public IEnumerator Resurrection(float Time)
    {
        HitAttack = false;
        yield return new WaitForSeconds(Time);
        HitAttack = true;
    }
    private void HeroActiveFals()
    {
        GameManager.Instance.SelectCharacter.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameManager.Instance.SelectCharacter.SetActive(false);
    }
    IEnumerator TimeControll(float TimeNum)
    {
        yield return new WaitForSeconds(TimeNum);
        Time.timeScale = 0f;
    }
}
