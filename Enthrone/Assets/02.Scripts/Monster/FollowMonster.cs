using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FollowMonster : MonoBehaviour
{
    MyObject myChar;
    Material _shader;
    public Animator _anim;
    public bool EliteMonster;
    public bool MiddleBoss;
    public GameObject HpbarCanvas;
    public int MonsterIndex;
    public int SummonMonsterCnt;
    public AIPath aiPath;
    public AIDestinationSetter aiDestination;
    public float ScaleNum;
    public float SlowNum;
    public bool NoDropBoss;
    public bool AttackType;
    public bool InvisibleType;
    public bool RightLeftCheck;
    public bool StatCheck = false;
    private bool ItemDropCheck = false;
    public bool Non_DropCheck;
    public GameObject Projectile;
    public GameObject RecallPortal;
    public GameObject SummonedMonster;
    public GameObject SummonMonsterPos;
    public GameObject SummonParent;             //소환하는 부모의 gameobject저장용
    private GameObject Effect;
    private GameObject MonsterProjectileManager;

    public Transform itemDropPos;


    private bool StunActive = false;
    private bool DivisionMinuCheck = false;
    [SerializeField]
    private bool AttackMotionCheck = true;
    public float preFirecount;
    public float preSpeed;
    public float fireCountdown = 0f;
    public float preFireCount;
    public float fireRate = 1f;
    public bool NaturalDivisionCheck;
    public bool SummonMonsterType;

    public bool Page2, Page3;

    public int AttackCnt = 3;
    public int attackCount;
    private int attackCountingCheck;
    [SerializeField]
    private bool TutoMonsterCheck = false;

    private void Awake()
    {
        myChar = MyObject.MyChar;
        _shader = GetComponent<Renderer>().material;
        _anim = GetComponent<Animator>();
        aiDestination = GetComponentInParent<AIDestinationSetter>();
        MonsterProjectileManager = GameObject.Find("MonsterProjectileManager");
        attackCountingCheck = attackCount;
        if (AttackType)
        {
            AttackMotionCheck = true;
        }
    }
    private void Start()
    {
        Effect = gameObject.transform.Find("Effect").gameObject;
    }
    // Update is called once per frame
    
    void Update()
    {
        aiDestination.SlowSpeed = myChar.SlowSpeed;
        aiDestination.ChapterCheck = myChar.Chapter;
        aiDestination.MonsterProjectile = MonsterProjectileManager;
        SlowNum = aiDestination.SlowNum;
        IndividualEffectCheck();
        if (NaturalDivisionCheck)
        {
            if (aiDestination.isHit)
            {
                myChar.CurrentBossHp -= aiDestination.AttackDamage;
                aiDestination.isHit = false;
            }
        }
        
        if (!StatCheck)
        {
            aiDestination.Speed = 0f;
            fireRate = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).ASPD;
            aiDestination.Speed = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Speed;
            aiDestination.Damage = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
            fireCountdown = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).ASPD;
            
            if (MiddleBoss)
            {
                HpbarCanvas.GetComponent<MonsterHpBar>().maxHp = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).HP * myChar.MultiHp * myChar.ThroneStatDataMgr.GetTemplate(myChar.ThroneIndex).ThroneStageHP;
            }

            //if (MonsterIndex >= 20)
            //{
            //    aiDestination.Speed = 0f;
            //    aiDestination.Speed = 0f;
            //    aiDestination.Damage = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
            //}
            //else
            //{
            //aiDestination.Speed = 0f;
            //aiDestination.Speed = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Speed;
            //aiDestination.Damage = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
            //}

            if (!myChar.Tutorial)
            {
                aiDestination.monsterHp = GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).HP * myChar.MultiHp * myChar.ThroneStatDataMgr.GetTemplate(myChar.ThroneIndex).ThroneStageHP;
            }
            else
            {
                if (myChar.TutorialNum !=3)
                {
                    aiDestination.monsterHp = 9f;
                }
                else
                {
                    aiDestination.monsterHp = 50f;
                    myChar.TotalBossHp = 50f;
                    myChar.CurrentBossHp = 50f;
                }
            }
            
            StatCheck = true;
        }
        
        transform.localPosition = new Vector3(0, 0, 0);
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-ScaleNum, ScaleNum, 1);
            RightLeftCheck = true;
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(ScaleNum, ScaleNum, 1);
            RightLeftCheck = false;
        }

        if (aiDestination.monsterHp <= 0)
        {
            if (!myChar.Tutorial)
            {
                if (!ItemDropCheck)
                {
                    if (!Non_DropCheck)
                    {
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
                }
                //if (NaturalDivisionCheck)
                //{
                //    if (!DivisionMinuCheck)
                //    {
                //        myChar.ForestBossTotalMonsterCheck--;
                //        DivisionMinuCheck = true;
                //    }
                    
                //}
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
            }
            else if (myChar.Tutorial)
            {
                if (!ItemDropCheck)
                {
                    RoomManager.Instance.TutoCheck++;
                    ItemDropCheck = true;
                }               
            }

            if (SummonMonsterType)
            {
                SummonParent.GetComponent<FollowMonster>().SummonMonsterCnt--;
                SummonMonsterType = false;
            }
        }
        else if (aiDestination.monsterHp > 0)
        {
            EffectCheck();
            Attackfire();

            if (myChar.Stun == 0)
            {
                if (!StunActive)
                {
                    preFirecount = aiDestination.fireCountdown;
                    aiDestination.fireCountdown = 500;
                    preSpeed = aiDestination.Speed;
                    aiDestination.Speed = 0;
                    StunActive = true;
                }                
            }
            else if (myChar.Stun != 0)
            {
                if (StunActive)
                {
                    aiDestination.fireCountdown = 3f;
                    aiDestination.Speed = preSpeed;
                    StunActive = false;
                }
            }
            if (myChar.Tutorial)
            {
                if (AttackCnt <= 0)
                {
                    if (!TutoMonsterCheck)
                    {
                        StartCoroutine(MonsterInvisible());
                    }
                }
            }
        }
        //if (aiDestination.isHit)
        //{
        //    MonsterHit();
        //}
        //PageCheck();
        if (aiDestination.HitAttackType)
        {
            if (aiDestination.HitCnt >= 5)
            {
                _anim.SetBool("Attack", true);
            }            
        }
        if (MiddleBoss)
        {
            HpbarCanvas.GetComponent<MonsterHpBar>().currentHp = aiDestination.monsterHp;
        }
    }
    public void HeadShot()
    {
        if (GameManager.Instance.PlayerSkill[14] >= 1)
        {
            if (!MiddleBoss)
            {
                if (Random.Range(0, 100) < myChar.HeadShootPer)
                {
                    aiDestination.monsterHp = 0;
                }
            }
        }
    }
    private void Attackfire()
    {
        if (AttackType)
        {
            if (fireCountdown <= 0)
            {
                AttackMotionCheck = false;
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
    public void attackEnd()
    {
        if (attackCountingCheck <= 1)
        {
            attackCountingCheck = attackCount;
            if (!InvisibleType)
            {
                AttackMotionCheck = true;
            }

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
            Debug.Log(MonsterIndex + " : " + 3);
        }
    }

    public void SummonAttackEnd()
    {
        if (attackCountingCheck <= 1)
        {
            attackCountingCheck = attackCount;

            _anim.SetBool("Attack", false);
            Invoke("SummonedMonsterCreate", 2f);
            if (myChar.Stun != 0)
            {
                fireCountdown = fireRate;
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
    public void ResetHitcnt()
    {
        aiDestination.HitCnt = 0;
        _anim.SetBool("Attack", false);
    }
    public void InvisiblePattern()
    {
        transform.GetComponentInParent<Collider2D>().enabled = false;
    }
    public void VisiblePattern()
    {
        transform.GetComponentInParent<Collider2D>().enabled = true;
        _anim.SetBool("Visible", false);
        AttackMotionCheck = true;
    }
    public void RateVisibleOn()
    {
        StartCoroutine(InvisibleMonsterRateOn());
    }
    private void PageCheck()
    {
        if (Page2)
        {
            if (myChar.Chapter >= 1)
            {
                _anim.SetLayerWeight(1, 1);
            }
        }
        else if (Page3)
        {
            if (myChar.Chapter >= 2)
            {
                _anim.SetLayerWeight(1, 1);
            }
        }
        if (myChar.Chapter == 0)
        {
            if (_anim.layerCount > 1)
            {
                _anim.SetLayerWeight(1, 0);
            }
        }
    }

    private void ChargingCheck()
    {
        if (RightLeftCheck)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (!RightLeftCheck)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        gameObject.GetComponentInParent<AIDestinationSetter>().ChargingShot();
        if (myChar.Stun == 0)
        {
            aiDestination.fireCountdown = 500;
        }
        else if (myChar.Stun != 0)
        {
            aiDestination.fireCountdown = 3;
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
    private void HpBar_Visible()
    {
        HpbarCanvas.SetActive(true);
    }
    private void HpBar_Invisible()
    {
        HpbarCanvas.SetActive(false);
    }
    public void HitSound()
    {
        SoundManager.Instance.PlaySfx(10);
    }
    public void SFXSound(int Num)
    {
        SoundManager.Instance.PlaySfx(Num);
    }
    private void SummonedMonsterCreate()
    {
        if (SummonMonsterCnt < 3)
        {
            StartCoroutine(SummonedCreate());
            SummonMonsterCnt++;
        }
        AttackMotionCheck = true;

    }
    private void DropItem(int GoldMin, int GoldMax, float GoldPer, int DiaMin, int DiaMax, float DiaPer, int HeartMin, int HeartMax, float HeartPer,
        int ShieldMin, int ShieldMax, float ShieldPer, int StoneMin, int StoneMax, float StonePer, int SoulMin, int SoulMax, float SoulPer, int HeroMin, int HeroMax, float HeroPer)
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
        //            GameObject Item = Instantiate(GameManager.Instance.DropItem[0], transform.position, Quaternion.identity);
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
                    GameObject Item = Instantiate(GameManager.Instance.DropItem[1], transform.position, Quaternion.identity);
                    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector2(1, 1);
                    //if (itemDropPos == null)
                    //{
                    //    GameObject Item = Instantiate(GameManager.Instance.DropItem[1], transform.position, Quaternion.identity);
                    //    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    //    Item.transform.parent = transform.parent.parent;
                    //    Item.transform.localScale = new Vector2(1, 1);
                    //}
                    //if (itemDropPos != null)
                    //{
                    //    GameObject Item = Instantiate(GameManager.Instance.DropItem[1], itemDropPos.position, Quaternion.identity);
                    //    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    //    Item.transform.parent = transform.parent.parent;
                    //    Debug.Log(transform.parent);
                    //    Item.transform.localScale = new Vector2(1, 1);
                    //}

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
                    GameObject Item = Instantiate(GameManager.Instance.DropItem[2], transform.position, Quaternion.identity);
                    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector2(1, 1);
                    //if (itemDropPos == null)
                    //{
                    //    GameObject Item = Instantiate(GameManager.Instance.DropItem[2], transform.position, Quaternion.identity);
                    //    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    //    Item.transform.parent = transform.parent.parent;
                    //    Item.transform.localScale = new Vector2(1, 1);
                    //}
                    //if (itemDropPos != null)
                    //{
                    //    GameObject Item = Instantiate(GameManager.Instance.DropItem[2], itemDropPos.position, Quaternion.identity);
                    //    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    //    Item.transform.parent = transform.parent;
                    //    Item.transform.localScale = new Vector2(1, 1);
                    //}

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
                    GameObject Item = Instantiate(GameManager.Instance.DropItem[3], transform.position, Quaternion.identity);
                    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector2(1, 1);
                    //if (itemDropPos == null)
                    //{
                    //    GameObject Item = Instantiate(GameManager.Instance.DropItem[3], transform.position, Quaternion.identity);
                    //    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    //    Item.transform.parent = transform.parent.parent;
                    //    Item.transform.localScale = new Vector2(1, 1);
                    //}
                    //if (itemDropPos != null)
                    //{
                    //    GameObject Item = Instantiate(GameManager.Instance.DropItem[3], itemDropPos.position, Quaternion.identity);
                    //    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    //    Item.transform.parent = transform.parent;
                    //    Item.transform.localScale = new Vector2(1, 1);
                    //}
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
                    GameObject Item = Instantiate(GameManager.Instance.DropItem[4], transform.position, Quaternion.identity);
                    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector2(1, 1);
                    //if (itemDropPos == null)
                    //{
                    //    GameObject Item = Instantiate(GameManager.Instance.DropItem[4], transform.position, Quaternion.identity);
                    //    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    //    Item.transform.parent = transform.parent.parent;
                    //    Item.transform.localScale = new Vector2(1, 1);
                    //}
                    //if (itemDropPos != null)
                    //{
                    //    GameObject Item = Instantiate(GameManager.Instance.DropItem[4], itemDropPos.position, Quaternion.identity);
                    //    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    //    Item.transform.parent = transform.parent;
                    //    Item.transform.localScale = new Vector2(1, 1);
                    //}
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
                    int ElementalStoneNum = Random.Range(0, GameManager.Instance.ElementalStone.Count);
                    GameObject Item = Instantiate(GameManager.Instance.ElementalStone[ElementalStoneNum], transform.position, Quaternion.identity);
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector3(6, 6f);
                    //if (itemDropPos == null)
                    //{
                    //    int ElementalStoneNum = Random.Range(0, GameManager.Instance.ElementalStone.Count);
                    //    GameObject Item = Instantiate(GameManager.Instance.ElementalStone[ElementalStoneNum], transform.position, Quaternion.identity);
                    //    Item.transform.parent = transform.parent.parent;
                    //    Item.transform.localScale = new Vector3(6, 6f);
                    //}
                    //if (itemDropPos != null)
                    //{
                    //    int ElementalStoneNum = Random.Range(0, GameManager.Instance.ElementalStone.Count);
                    //    GameObject Item = Instantiate(GameManager.Instance.ElementalStone[ElementalStoneNum], itemDropPos.position, Quaternion.identity);
                    //    Item.transform.parent = transform.parent;
                    //    Item.transform.localScale = new Vector3(6f, 6f);
                    //}

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
        //            int ActiveItemNum = Random.Range(0, GameManager.Instance.ActiveItem.Count);
        //            GameObject Item = Instantiate(GameManager.Instance.ActiveItem[ActiveItemNum], transform.position, Quaternion.identity);
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
                    GameObject Item = Instantiate(GameManager.Instance.DropItem[5], transform.position, Quaternion.identity);
                    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector2(1, 1);
                    //if (itemDropPos == null)
                    //{
                    //    GameObject Item = Instantiate(GameManager.Instance.DropItem[5], transform.position, Quaternion.identity);
                    //    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    //    Item.transform.parent = transform.parent.parent;
                    //    Item.transform.localScale = new Vector2(1, 1);
                    //}
                    //if (itemDropPos != null)
                    //{
                    //    GameObject Item = Instantiate(GameManager.Instance.DropItem[5], itemDropPos.position, Quaternion.identity);
                    //    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    //    Item.transform.parent = transform.parent;
                    //    Item.transform.localScale = new Vector2(1, 1);
                    //}
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
                    GameObject Item = Instantiate(GameManager.Instance.DropItem[6], transform.position, Quaternion.identity);
                    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    Item.transform.parent = transform.parent.parent;
                    Item.transform.localScale = new Vector2(1, 1);
                    //if (itemDropPos == null)
                    //{
                    //    GameObject Item = Instantiate(GameManager.Instance.DropItem[6], transform.position, Quaternion.identity);
                    //    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    //    Item.transform.parent = transform.parent.parent;
                    //    Item.transform.localScale = new Vector2(1, 1);
                    //}
                    //if (itemDropPos != null)
                    //{
                    //    GameObject Item = Instantiate(GameManager.Instance.DropItem[6], itemDropPos.position, Quaternion.identity);
                    //    Item.GetComponent<ItemScript>().StartPostion = gameObject.transform.position;
                    //    Item.transform.parent = transform.parent;
                    //    Item.transform.localScale = new Vector2(1, 1);
                    //}
                }
            }
        }
        ItemDropCheck = true;
    }
    IEnumerator MonsterInvisible()
    {
        _shader.SetFloat("_Alpha", 0f);
        transform.parent.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        AttackCnt = 3;
        _shader.SetFloat("_Alpha", 1f);
        transform.parent.GetComponent<Collider2D>().enabled = true;
        TutoMonsterCheck = false;
    }

    IEnumerator InvisibleMonsterRateOn()
    {
        yield return new WaitForSeconds(3f);
        _anim.SetBool("Invisible", false);
        _anim.SetBool("Visible", true);
    }

    IEnumerator SummonedCreate()
    {
        if (GameManager.Instance.SelectCharacter.GetComponent<PlayerTargeting>().MonsterList.Count <= 6)
        {
            float RandomX = Random.Range(-6.5f, 8.5f);
            float RandomY = Random.Range(1.5f, 12.5f);
            GameObject Portal = Instantiate(RecallPortal, transform.position, Quaternion.identity);
            Portal.transform.parent = SummonMonsterPos.transform;
            SoundManager.Instance.PlaySfx(56);
            Portal.transform.localScale = new Vector2(1, 1);
            Portal.transform.localPosition = new Vector3(RandomX, RandomY, 0);
            yield return new WaitForSeconds(2f);
            Destroy(Portal);
            GameObject Monster = Instantiate(SummonedMonster, transform.position, Quaternion.identity);
            Monster.transform.parent = SummonMonsterPos.transform;
            Monster.transform.localScale = new Vector2(1, 1);
            Monster.transform.localPosition = new Vector3(RandomX, RandomY, 0);
            Monster.transform.GetChild(0).GetComponent<FollowMonster>().SummonMonsterType = true;
            Monster.transform.GetChild(0).GetComponent<FollowMonster>().SummonParent = transform.gameObject;
        }
    }
}
