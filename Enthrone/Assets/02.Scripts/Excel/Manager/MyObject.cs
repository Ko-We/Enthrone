using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
using CodeStage.AntiCheat.ObscuredTypes;


public class MyObject : MonoBehaviour
{
    /********************************** 싱 글 톤 *******************************************/
    private static MyObject s_MyObject = null;
    public static MyObject MyChar
    {
        get
        {
            if (s_MyObject == null)
            {
                s_MyObject = FindObjectOfType(typeof(MyObject)) as MyObject;
                if (s_MyObject == null)
                {
                    GameObject obj = new GameObject("MyChar");
                    s_MyObject = obj.AddComponent(typeof(MyObject)) as MyObject;
                }
            }
            return s_MyObject;
        }
    }
    /*************************************************************************************/
    

    public MonsterHPMultiTemplate MHpMultiData;
    public MonsterHPMultiTemplateMgr MHpMultiDataMgr;
    public ItemTemplate itemData;
    public ItemTemplateMgr itemDataMgr;
    public SkillTemplate skillData;
    public SkillTemplateMgr skillDataMgr;
    public ActiveItemTemplate ActiveitemData;
    public ActiveItemTemplateMgr ActiveitemDataMgr;
    public AntiqueItemTemplate AntiqueitemData;
    public AntiqueItemTemplateMgr AntiqueitemDataMgr;
    public TextDataTemplate TextData;
    public TextDataTemplateMgr TextDataMgr;
    public HeroTemplate HeroData;
    public HeroTemplateMgr HeroDataMgr;
    public ThroneSkillTemplate ThroneSkillData;
    public ThroneSkillTemplateMgr ThroneSkillDataMgr;
    public ThroneStatTemplate ThroneStatData;
    public ThroneStatTemplateMgr ThroneStatDataMgr;

    public String Throne_Version;
    // 맵(Stage)관련
    public bool Tutorial;
    public int TutorialNum = 0;
    public bool TouchInfo = false;
    public bool TutorialNextStage = false;
    public bool TutoActiveItemUse = false;
    public bool TutoThreeCheck = false;
    public bool TutoStoneUse = false;       //튜토리얼에서 속성석변경확인bool값
    public bool TutoItemUse = false;        //튜토리얼 쇼크웨이브 사용 bool값
    public bool BossCheck = false;
    public bool BossInfoCheck = false;
    public bool EnthroneCameraOn = false;
    [Header("아이템 장착")]
    public int ItemCustomLv = 0;
    public int ItemCustomNum = 0;
    [Header("맵의 챕터")]
    public ObscuredInt Chapter;
    [Header("챕터안에서 스테이지")]
    public ObscuredInt Stage = 0;

    public ObscuredBool[] StageClearCheck = { true, false, true, false, false, true };
    public int SelectedStage = 0;
    public int BasicStage = 5; // 일반맵 갯수 0부터 스테이지 계산(ex : 3 = 4스테이지)
    public GameObject SelectLocation;
    public bool CrownGetBgm = false;
    public bool RewardGetCheck = false;
    public int Chapter1_Cnt, Chapter2_Cnt, Chapter3_Cnt;
    public bool ChapterCntCheck = false;
    //리꼬셰 관련
    public int BounceLv = 1;
    public int bounceCnt;
    //튕김 관련
    public int WallbounceCnt;

    //영웅 관련
    public int HeroNum;    //존재하는 영웅만큼의 숫자
    [Header("캐릭터 선택 0 : 나이트 / 1 : 워리어 / 2 : 아처 / 3 : 위자드 / 4 : 닌자")]
    public int SelectHero = 0;
    public ObscuredInt[] SelectWeapon = { -1, -1, -1, -1, -1 };
    public ObscuredInt[] SelectCostume = { -1, -1, -1, -1, -1 };
    public ObscuredInt[] EquipmentWeapon = { 0, 0, 0, 0, 0 };
    public ObscuredInt[] EquipmentCostume = { 0, 0, 0, 0, 0 };

    //영웅 스킨 관련
    public ObscuredInt[] KnightWeaponSkin = { 0, 1, 2, 3, 4, 5 };
    public ObscuredInt[] KnightCostumeSkin = { 50, 51, 52, 53, 54, 55 };
    public ObscuredInt[] KnightWeaponSkinPurchase = { 0, -1, -1, -1, -1, -1 };
    public ObscuredInt[] KnightCostumeSkinPurchase = { 0, -1, -1, -1, -1, -1 };
    public ObscuredInt[] WarriorWeaponSkin = { 100, 101, 102, 103, 104, 105 };
    public ObscuredInt[] WarriorCostumeSkin = { 150, 151, 152, 153, 154, 155 };
    public ObscuredInt[] WarriorWeaponSkinPurchase = { 0, -1, -1, -1, -1, -1 };
    public ObscuredInt[] WarriorCostumeSkinPurchase = { 0, -1, -1, -1, -1, -1 };
    public ObscuredInt[] ArcherWeaponSkin = { 200, 201, 202, 203, 204, 205 };
    public ObscuredInt[] ArcherCostumeSkin = { 250, 251, 252, 253, 254, 255 };
    public ObscuredInt[] ArcherWeaponSkinPurchase = { 0, -1, -1, -1, -1, -1 };
    public ObscuredInt[] ArcherCostumeSkinPurchase = { 0, -1, -1, -1, -1, -1 };
    public ObscuredInt[] WizardWeaponSkin = { 300, 301, 302, 303, 304, 305 };
    public ObscuredInt[] WizardCostumeSkin = { 350, 351, 352, 353, 354, 355 };
    public ObscuredInt[] WizardWeaponSkinPurchase = { 0, -1, -1, -1, -1, -1 };
    public ObscuredInt[] WizardCostumeSkinPurchase = { 0, -1, -1, -1, -1, -1 };
    public ObscuredInt[] NinjaWeaponSkin = { 400, 401, 402, 403, 404, 405 };
    public ObscuredInt[] NinjaCostumeSkin = { 450, 451, 452, 453, 454, 455 };
    public ObscuredInt[] NinjaWeaponSkinPurchase = { 0, -1, -1, -1, -1, -1 };
    public ObscuredInt[] NinjaCostumeSkinPurchase = { 0, -1, -1, -1, -1, -1 };

    //0 : 나이트 / 1 : 워리어 / 2 : 아처 / 3 : 위자드 / 4 : 닌자
    public ObscuredInt[] HeroLv = { 0, 0, 0, 0, 0 };
    public ObscuredBool[] HeroPurchase = { true, true, false, false, false };
    public ObscuredInt ResurectionCost;
    public ObscuredInt NumberOfResurections = 1;
    public bool Finished = false;
    public bool Resurrection = false;
    public bool SwordMan = false;
    public bool Warrior = false;
    public bool Archer = false;
    public bool Ninja = false;
    public bool Wizard = false;

    public int SelectClotihingCostume;
    public int SelectWeaponCostume;
    public int WeaponCostume;
    public int ClothingCostume;

    //스킬?(능력치) 관련
    public bool BounceShoot = false;
    public bool Fly = false;
    public bool WindWalk = false;

    public bool EffectOnCheck = false;
    public bool Slow = false;
    public bool Reflection = false;
    public bool ASPDPotion = false;
    public bool PowerPotion = false;
    public bool InvinciblePotion = false;

    public bool SeasonalShield = false;
    public bool YetiKingIcicleCheck = false;

    //1 : 화염 / 2 : 냉기 / 3 : 대지 / 4 : 자연
    public int SeasonNumber = 3;

    public int PowerShotCount = 5;
    
    //영웅 능력치    
    public ObscuredInt maxHp = 1000;               //영웅이 게임 시작시 가지는 HP
    public ObscuredInt currentHp;               //영웅이 게임 진행중에 가지고있는 HP
    public ObscuredFloat ASPD = 20;
    public ObscuredFloat Range = 10;
    public ObscuredInt Shield = 0;              //영웅 쉴드량
    public ObscuredInt JumpCnt = 2;             //영웅 점프 횟수
    public ObscuredInt BloodPer = 0;            // 몬스터 제거시 HP흡혈할 확률
    public ObscuredFloat Luck = 0;                //아이템 드랍될 확률 수치 추가용
    public ObscuredInt HeadShootPer = 0;
    public ObscuredInt HealingShieldCheck;

    public ObscuredFloat Damage;            //영웅 발사체 데미지 
    public ObscuredFloat CurrentDamage = 4;         
    public ObscuredFloat WindWalkSpeed;         //영웅 윈드워크 발동시 적용되는 추가속도
    public ObscuredFloat JumpForce = 8.0f;      //영웅 점프 크기 8.0이 3칸정도됨
    public ObscuredFloat MagnetRid;

    //왕좌 보스 관련
    [Header("왕좌 보스 0 : 나이트 / 1 : 워리어 / 2 : 아처 / 3 : 위자드 / 4 : 닌자")]
    public int EnthroneHeroNum = 1;
    public int ThroneHeroLv = 0;
    public int ThroneCostumSkin;
    public int ThroneWeaponSkin;
    public int ThronemaxHp;
    public int ThronecurrentHp;
    public float ThroneASPD;
    public float ThroneRange;
    public float ThroneJumpForce = 8.0f;
    public float ThroneDamage;
    public float ThroneCurrentDamage;
    public bool ThroneBossKill = false;
    public List<int> ThroneItemList = new List<int>();
    public List<int> ThroneItemLvList = new List<int>();
    [Header("왕좌보스 난이도")]
    public int EquipmentTotalScore;
    public int SkillNo;
    public int ThroneIndex;

    //몬스터 관련
    public float SlowSpeed = 1;         //모든 몬스터 & 몬스터미사일의 속도의 %를 조절한다.
    public float Stun = 1;
    public float MultiHp = 1f;
    public float MultiDamage = 1f;
    public int MHpMultiIndex = 0;
    public int ThroneDropMin;
    public int ThroneDropMax;
    //public bool NatureBossAllDeath = false;
    public bool BossClear = false;

    //보스 몬스터 HP관련 숲보스때문에 추가됨
    public bool StartCheck = false;
    public float TotalBossHp;
    public float CurrentBossHp;
    //public int ForestBossTotalMonsterCheck = 16;

    //Enthron 정보
    public List<int> EnthroneEquipment = new List<int>();


    //재화 관련
    [Header("재화")]
    public ObscuredInt Coin = 0;
    public ObscuredInt Gem = 0;
    public ObscuredInt Key = 0;
    public ObscuredInt SlotCoin = 0;
    public ObscuredInt SoulSpark = 0;
    public ObscuredInt HeroHeart = 0;
    public ObscuredInt MissionCoin = 0;
    public GameObject CurrentMap;
    //gain은 게임도중 얻은것 multi는 광고보고 배수줄거
    public float durationTiem;
    public ObscuredInt GainGem;
    public ObscuredInt GainSoulSpark;
    public ObscuredInt GainHeroHeart;
    public ObscuredInt Gain_Multiple = 1;     //광고 시청시 배수
    public bool MultipleCheck = false;
    public float PlayTime;

    public ObscuredBool ADClearCheck = false;
    public ObscuredBool AD_Enought_Check = false;
    public ObscuredFloat AD_Road_Time;

    public ObscuredInt Dia_1_FirstCheck = 0;
    public ObscuredInt Dia_2_FirstCheck = 0;
    public ObscuredInt Dia_3_FirstCheck = 0;

    public int GachaBox;

    public bool AD_FailedToLoad;
    public int AD_Num;

    //UI관련
    [Header("UI관련")]
    public bool BossHPAnim = false;
    public bool BossNamePanel = false;
    public bool TimeCheck = false;
    public ObscuredInt SpecialPackCnt = 2;
    public ObscuredInt LuxuryPackCnt = 2;
    public ObscuredInt StarterPackCnt = 1;
    public ObscuredInt SlotPackCnt = 2;
    public ObscuredInt AdRemove = 1;
    public ObscuredInt TopDownCnt = 0;          //인게임 조이스틱 위치 상하중간 체크함수 (0 : 중간 / 1 : 하단 / 2 : 상단)
    public ObscuredInt HandCheck = 0;          //인게임 조이스틱 오른손 왼존 체크 함수 (0 : 오른손 / -1 왼손)

    public bool ADEquimentCheck = false;
    public bool ADGemreductionCheck = false;
    public bool ADSoulreductionCheck = false;
    public bool ADHeroreductionCheck = false;
    public DateTime ADEquimentStartTime;
    public DateTime ADGemStartTime;
    public DateTime ADSoulStartTime;
    public DateTime ADHeroStartTime;

    //인게임 장비 관련
    public bool ItemOwnCheck;   //띄워줄 장비 체크무한으로 안하기위함

    //슬롯머신관련
    [Header("슬롯머신 설정")]
    public int MachinSlotCnt = 1;
    public int[] LobbyMachineEquipmentNum  = new int[3];
    public bool SlotActiveCheck = false;

    public bool ADSlotCheck = false;
    public DateTime ADSlotStartTime;

    //인벤토리 관련
    public bool InventoryCheck = false;
    public int[] InventoryItemNum = new int[4];

    //대장간 관련
    [Header("대장간 설정")]
    public bool Crafting = false;           //아이템제작 여부체크
    public int CraftingItemNum = -1;        //제작될 아이템 
    public int CraftingTime;                //제작되는 시간
    public DateTime StartCraftingTime;      //제작이 들어간 시작시간
    public int UntilCompleteTime;           //남은 제작시간
    public bool ADreductionCheck = false;   //시간단축 AD버튼 사용가능체크
    public bool CompleteCheck = false;      //제작중인 장비 완료된지 체크해주는
    public bool CraftingItemPickupCheck = false;
    public bool FirstBlackSmithCheck = true;    //처음 대장간들어갔을때 인포창 띄워주기위한값

    [Header("언어 설정")]
    //한국어 : 0 , 영어 : 1
    public int LanguageNum;
    SystemLanguage language;
    [Space(10f)]
    //public DateTime SaveAttendanceDay;          //몇일 동안 받고있는지알기위해 시작위치
    public DateTime ContinuAttendanceCheck;     //연속출책 확인하기위한 
    public int TotalAttendanceDay = 0;
    public int AttendanceDay = 0;
    public int NowDate;
    public bool Attendancereward;

    //보유중인 아이템 관련
    [Header("아이템 값 설정")]
    public ObscuredInt itemNum;
    public ObscuredInt ItemLv;
    public ObscuredInt[] EquipmentAll = new ObscuredInt[88];
    public ObscuredInt[] EquipmentQuantity = new ObscuredInt[88];

    public List<ObscuredInt> EquipmentActive = new List<ObscuredInt>();
    public List<ObscuredInt> EquipmentCost = new List<ObscuredInt>();

    public List<ObscuredInt> EquipmentUsed = new List<ObscuredInt>();
    public List<bool> EquipmentUsedTrueCheck = new List<bool>();

    public GameObject StayEquipment; //현재 앞에서있는 아이템 

    public int SelectStoneNum;
    public ObscuredInt[] ElementStone = new ObscuredInt[5];
    public ObscuredInt[] ElementStoneAll = new ObscuredInt[4];
    public ObscuredInt[] ElementStoneQuantity = new ObscuredInt[4];
    public List<ObscuredInt> ElementStoneActive = new List<ObscuredInt>();
    public int OnElementStoneCnt = 5;

    //사용 아이템 관련
    public ObscuredInt[] ActiveitemAll = new ObscuredInt[13];
    public ObscuredInt[] ActiveitemQuantity = new ObscuredInt[13];
    public List<ObscuredInt> ActiveitemActive = new List<ObscuredInt>();
    public List<ObscuredInt> ActiveItemCost = new List<ObscuredInt>();
    [Header("사용아이템 설정")]
    public int ActiveItem;
    public int ActiveItme_Lv = 1;

    public bool GiftboxDropCheck = false;
    public bool InGameStart = false;
    public bool muteEffectSound = false;
    public bool muteBGM = false;

    public List<int> PlayerEquipment = new List<int>();

    private void Awake()
    {
        Throne_Version = "ver.01_00.1";
        MHpMultiIndex = 0;
        language = Application.systemLanguage;
        //for (int i = 0; i < StageClearCheck.Length; i++)
        //{
        //    if (i < 3)
        //    {
        //        StageClearCheck[i] = true;
        //    }
        //    else
        //    {
        //        StageClearCheck[i] = false;
        //    }
        //}
        Tutorial = true;
        ResurectionCost = 5;
        EnthroneCameraOn = true;
        HeroPurchase[0] = true;
        HeroPurchase[1] = true;
        HeroPurchase[2] = false;
        HeroPurchase[3] = false;
        HeroPurchase[4] = false;

        HeroNum = 5;
        HeroStartItem();

        if (!InventoryCheck)
        {
            for (int i = 0; i < InventoryItemNum.Length; i++)
            {
                InventoryItemNum[i] = -1;
            }
        }

        MHpMultiDataMgr = new MonsterHPMultiTemplateMgr();
        itemDataMgr = new ItemTemplateMgr();
        skillDataMgr = new SkillTemplateMgr();
        ActiveitemDataMgr = new ActiveItemTemplateMgr();
        AntiqueitemDataMgr = new AntiqueItemTemplateMgr();
        TextDataMgr = new TextDataTemplateMgr();
        HeroDataMgr = new HeroTemplateMgr();
        ThroneSkillDataMgr = new ThroneSkillTemplateMgr();
        ThroneStatDataMgr = new ThroneStatTemplateMgr();

        FirstSupplementEquipment();            //초기 아이템 열어주는거 정하기       
        switch (language)
        {
            case SystemLanguage.English:
                LanguageNum = 1;
                break;
            case SystemLanguage.Korean:
                LanguageNum = 0;
                break;
            default:
                LanguageNum = 1;
                break;
        }
        Load();

        if (s_MyObject != null && s_MyObject != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        s_MyObject = this;
    }
    void Start()
    {
        OnLoadDataMgr();
        
        //if (!SlotActiveCheck)
        //{
        //    for (int i = 0; i < LobbyMachineEquipmentNum.Length; i++)
        //    {
        //        LobbyMachineEquipmentNum[i] = -1;
        //    }
        //}        
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "LobbiScene")
        {
            if (Crafting)
            {
                if (!CompleteCheck)
                {
                    TimeSpan Time_Conversion_Int = UnbiasedTime.Instance.Now() - StartCraftingTime;

                    UntilCompleteTime = Convert.ToInt32(Time_Conversion_Int.TotalSeconds);
                    if (UntilCompleteTime >= CraftingTime)
                    {
                        CompleteCheck = true;
                    }
                }
                else
                {
                    UntilCompleteTime = CraftingTime;
                }
            }
            
        }

        //TimeSpan Time = (UnbiasedTime.Instance.Now().Date.AddHours(8) - UnbiasedTime.Instance.Now());
        //Test.text = string.Format("{0:D2}:{1:D2}:{2:D2}", Time.Hours, Time.Minutes, Time.Seconds);

        EquipmentCarry();
        HeroDataSetUp();
        //ThroneDateSetUp();
        //MonsterHpMultiCheck();

        
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    HeroSetupTest();
        //}

        if (SceneManager.GetActiveScene().name == "StageScene")
        {
            SkillCheck();
            EquipmentUse();

            if (!ItemOwnCheck)
            {
                if (GameManager.Instance.RoomManagerObj.activeSelf == true)
                {
                    if (RoomManager.Instance.CarryEquipment.Count <= 0)
                    {
                        for (int i = 0; i < EquipmentActive.Count; i++)
                        {
                            RoomManager.Instance.CarryEquipment.Add(EquipmentActive[i]);
                        }
                    }
                    if (RoomManager.Instance.CarryAntique.Count <= 0)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            RoomManager.Instance.CarryAntique.Add(2000 + i);
                        }
                    }
                }
                ItemOwnCheck = true;
            }
        }

        if (!Tutorial)
        {
            if (SceneManager.GetActiveScene().name == "StageScene")
            {
                if (GameManager.Instance.RoomManagerObj.activeSelf)
                {
                    ChapterCheck();
                    //임시로 엉뚱한값들어가는게 1999라서 1999면 획득한아이템 제거하는코드추가
                    for (int i = 0; i < PlayerEquipment.Count; i++)
                    {
                        if (PlayerEquipment[i] == 1999)
                        {
                            PlayerEquipment.RemoveAt(i);
                        }
                    }
                }
            }
            //if (EquipmentTotalScore >= 300)
            //{
            //    SkillNo = 5;
            //    switch (HeroLv[EnthroneHeroNum])
            //    {
            //        case 0:
            //            ThroneIndex = 5;
            //            break;
            //        case 1:
            //            ThroneIndex = 11;
            //            break;
            //        case 2:
            //            ThroneIndex = 17;
            //            break;
            //        case 3:
            //            ThroneIndex = 23;
            //            break;
            //        case 4:
            //            ThroneIndex = 29;
            //            break;
            //        case 5:
            //            ThroneIndex = 35;
            //            break;
            //    }
            //}
            //else if (EquipmentTotalScore < 300 && EquipmentTotalScore >= 250)
            //{
            //    SkillNo = 4;
            //    switch (HeroLv[EnthroneHeroNum])
            //    {
            //        case 0:
            //            ThroneIndex = 4;
            //            break;
            //        case 1:
            //            ThroneIndex = 10;
            //            break;
            //        case 2:
            //            ThroneIndex = 16;
            //            break;
            //        case 3:
            //            ThroneIndex = 22;
            //            break;
            //        case 4:
            //            ThroneIndex = 28;
            //            break;
            //        case 5:
            //            ThroneIndex = 34;
            //            break;
            //    }
            //}
            //else if (EquipmentTotalScore < 250 && EquipmentTotalScore >= 200)
            //{
            //    SkillNo = 3;
            //    switch (HeroLv[EnthroneHeroNum])
            //    {
            //        case 0:
            //            ThroneIndex = 3;
            //            break;
            //        case 1:
            //            ThroneIndex = 9;
            //            break;
            //        case 2:
            //            ThroneIndex = 15;
            //            break;
            //        case 3:
            //            ThroneIndex = 21;
            //            break;
            //        case 4:
            //            ThroneIndex = 27;
            //            break;
            //        case 5:
            //            ThroneIndex = 33;
            //            break;
            //    }
            //}
            //else if (EquipmentTotalScore < 200 && EquipmentTotalScore >= 150)
            //{
            //    SkillNo = 2;
            //    switch (HeroLv[EnthroneHeroNum])
            //    {
            //        case 0:
            //            ThroneIndex = 2;
            //            break;
            //        case 1:
            //            ThroneIndex = 8;
            //            break;
            //        case 2:
            //            ThroneIndex = 14;
            //            break;
            //        case 3:
            //            ThroneIndex = 20;
            //            break;
            //        case 4:
            //            ThroneIndex = 26;
            //            break;
            //        case 5:
            //            ThroneIndex = 32;
            //            break;
            //    }
            //}
            //else if (EquipmentTotalScore < 150 && EquipmentTotalScore >= 125)
            //{
            //    SkillNo = 1;
            //    switch (HeroLv[EnthroneHeroNum])
            //    {
            //        case 0:
            //            ThroneIndex = 1;
            //            break;
            //        case 1:
            //            ThroneIndex = 7;
            //            break;
            //        case 2:
            //            ThroneIndex = 13;
            //            break;
            //        case 3:
            //            ThroneIndex = 19;
            //            break;
            //        case 4:
            //            ThroneIndex = 25;
            //            break;
            //        case 5:
            //            ThroneIndex = 31;
            //            break;
            //    }
            //}
            //else
            //{
            //    SkillNo = 0;
            //    switch (HeroLv[EnthroneHeroNum])
            //    {
            //        case 0:
            //            ThroneIndex = 0;
            //            break;
            //        case 1:
            //            ThroneIndex = 6;
            //            break;
            //        case 2:
            //            ThroneIndex = 12;
            //            break;
            //        case 3:
            //            ThroneIndex = 18;
            //            break;
            //        case 4:
            //            ThroneIndex = 24;
            //            break;
            //        case 5:
            //            ThroneIndex = 30;
            //            break;
            //    }
            //}
        }

        //if (Shield >= 5)    //쉴드는 5가 넘으면안되기 때문에 max치를 5로잡아준다.
        //{
        //    Shield = 5;
        //}


        //currentHP : HP / range : range / CurrentDamage : Damage / ASPD : ASPD

    }

    void Load()
    {
        Tutorial = LoadTutorial();
        SelectHero = LoadSelectHero();
        Chapter = LoadChapter();
        LoadStageClearCheck();
        LoadHeroLv();
        LoadHeroPurchase();
        LoadSelectWeapon();
        LoadSelectCostume();
        LoadEquipmentWeapon();
        LoadEquipmentCostume();
        LoadKnightWeaponSkinPurchase();
        LoadKnightCostumeSkinPurchase();
        LoadWarriorWeaponSkinPurchase();
        LoadWarriorCostumeSkinPurchase();
        LoadArcherWeaponSkinPurchase();
        LoadArcherCostumeSkinPurchase();
        LoadWizardWeaponSkinPurchase();
        LoadWizardCostumeSkinPurchase();
        LoadNinjaWeaponSkinPurchase();
        LoadNinjaCostumeSkinPurchase();

        EnthroneHeroNum = LoadEnthroneHeroNum();
        ThroneHeroLv = LoadThroneHeroLv();
        ThroneCostumSkin = LoadThroneCostumSkin();
        ThroneWeaponSkin = LoadThroneWeaponSkin();
        ThronemaxHp = LoadThronemaxHp();
        ThroneASPD = LoadThroneASPD();
        ThroneRange = LoadThroneRange();
        ThroneDamage = LoadThroneDamage();
        LoadThroneItemList();
        LoadThroneItemLvList();

        Gem = LoadGem();
        Key = LoadKey();
        SlotCoin = LoadSlotCoin();
        SoulSpark = LoadSoulSpark();
        HeroHeart = LoadHeroHeart();

        LoadEquipmentAll();
        LoadEquipmentQuantity();
        LoadElementStoneAll();
        LoadElementStoneQuantity();
        LoadActiveitemAll();
        LoadActiveitemQuantity();

        LoadInventoryItemNum();
        MachinSlotCnt = LoadMachinSlotCnt();
        LoadLobbyMachineEquipmentNum();
        SlotActiveCheck = LoadSlotActiveCheck();       //슬롯머신돌렸는지 확인해주는 함수 없으면 장비를 뽑은지 인지못해서 슬롯장비체크에서 장비가있다고 체크될 수 있음
        ADSlotCheck = LoadADSlotCheck();
        ADSlotStartTime = LoadADSlotStartTime();
        SpecialPackCnt = LoadSpecialPackCnt();
        LuxuryPackCnt = LoadLuxuryPackCnt();
        StarterPackCnt = LoadStarterPackCnt();
        SlotPackCnt = LoadSlotPackCnt();
        ADClearCheck = LoadADClearCheck();
        AdRemove = LoadAdRemove();
        TopDownCnt = LoadTopDownCnt();
        HandCheck = LoadHandCheck();

        Dia_1_FirstCheck = LoadDia_1_FirstCheck();
        Dia_2_FirstCheck = LoadDia_2_FirstCheck();
        Dia_3_FirstCheck = LoadDia_3_FirstCheck();

        ADEquimentStartTime = LoadADEquimentStartTime();
        ADGemStartTime = LoadADGemStartTime();
        ADSoulStartTime = LoadADSoulStartTime();
        ADHeroStartTime = LoadADHeroStartTime();

        ADEquimentCheck = LoadADEquimentCheck();
        ADGemreductionCheck = LoadADGemreductionCheck();
        ADSoulreductionCheck = LoadADSoulreductionCheck();
        ADHeroreductionCheck = LoadADHeroreductionCheck();
        LanguageNum = LoadLanguageNum();

        //SaveAttendanceDay = LoadSaveAttendanceDay();
        ContinuAttendanceCheck = LoadContinuAttendanceCheck();
        TotalAttendanceDay = LoadTotalAttendanceDay();
        AttendanceDay = LoadAttendanceDay();
        NowDate = LoadNowDate();
        Attendancereward = LoadAttendancereward();
        Crafting = LoadCrafting();
        CraftingItemNum = LoadCraftingItemNum();
        CraftingTime = LoadCraftingTime();
        StartCraftingTime = LoadStartCraftingTime();
        ADreductionCheck = LoadADreductionCheck();

        muteEffectSound = LoadmuteEffectSound();
        muteBGM = LoadmuteBGM();
        FirstBlackSmithCheck = LoadFirstBlackSmithCheck();
    }


    public void Resetitem()
    {
        Coin = 0;
        GainGem = 0;
        GainHeroHeart = 0;
        GainSoulSpark = 0;
        PlayTime = 0;

        EquipmentUsed.Clear();
        EquipmentUsedTrueCheck.Clear();
        ActiveItem = 0;
        SelectStoneNum = 0;
        BossHPAnim = false;
        BossNamePanel = false;
        //ForestBossTotalMonsterCheck = 16;
        //NatureBossAllDeath = false;


        for (int i = 0; i < ElementStone.Length; i++)
        {
            ElementStone[i] = 0;
        }
        if (SceneManager.GetActiveScene().name == "StageScene")
        {
            Time.timeScale = 0;
        }
        
    }

    private void ChapterCheck()
    {
        switch (Chapter)
        {
            case 0:
                MultiHp = MHpMultiDataMgr.GetTemplate(Stage).MultiHp;
                MultiDamage = MHpMultiDataMgr.GetTemplate(Stage).MultiDamage;
                break;
            case 1:
                MultiHp = MHpMultiDataMgr.GetTemplate(Stage + 50).MultiHp;
                MultiDamage = MHpMultiDataMgr.GetTemplate(Stage + 50).MultiDamage;
                break;
            case 2:
                MultiHp = MHpMultiDataMgr.GetTemplate(Stage + 100).MultiHp;
                MultiDamage = MHpMultiDataMgr.GetTemplate(Stage + 100).MultiDamage;
                break;
            case 3:
                MultiHp = MHpMultiDataMgr.GetTemplate(Stage + 130).MultiHp;
                MultiDamage = MHpMultiDataMgr.GetTemplate(Stage + 130).MultiDamage;
                break;
            case 4:
                MultiHp = MHpMultiDataMgr.GetTemplate(Stage + 180).MultiHp;
                MultiDamage = MHpMultiDataMgr.GetTemplate(Stage + 180).MultiDamage;
                break;
            case 5:
                MultiHp = MHpMultiDataMgr.GetTemplate(Stage + 230).MultiHp;
                MultiDamage = MHpMultiDataMgr.GetTemplate(Stage + 230).MultiDamage;
                break;
        }

        //게임 플레이 타임 체크해주는 부분
        if (GameManager.Instance.Clear_ResultPanel.activeSelf == false && GameManager.Instance.Fail_ResultPanel.activeSelf == false)
        {
            PlayTime += Time.deltaTime;
        }
        //if (!ChapterCntCheck)
        //{
        //    for (int i = 1; i < MHpMultiDataMgr.GetCount() + 1; i++)
        //    {
        //        switch (MHpMultiDataMgr.GetTemplate(i).Chapter)
        //        {
        //            case 1:
        //                Chapter1_Cnt++;
        //                break;
        //            case 2:
        //                Chapter2_Cnt++;
        //                break;
        //            case 3:
        //                Chapter3_Cnt++;
        //                break;
        //            case 100:
        //                Chapter1_Cnt++;
        //                break;
        //            case 200:
        //                Chapter2_Cnt++;
        //                break;
        //            case 300:
        //                Chapter3_Cnt++;
        //                break;
        //            case 1000:
        //                Chapter1_Cnt++;
        //                break;
        //            case 2000:
        //                Chapter2_Cnt++;
        //                break;
        //            case 3000:
        //                Chapter3_Cnt++;
        //                break;
        //        }
        //    }
        //    ChapterCntCheck = true;
        //}

        //switch (Chapter)
        //{
        //    case 0:
        //        BasicStage = Chapter1_Cnt - 1;
        //        break;
        //    case 1:
        //        BasicStage = Chapter2_Cnt - 1;
        //        break;
        //    case 2:
        //        BasicStage = Chapter3_Cnt - 1;
        //        break;
        //}
    }
    /* [0 : 리꼬셰], [1 : 플라이], [2 : 윈드워크], [3 : 파워샷], [4: 데스 붐], [5 : 신의가호], [6 : 강한 심장]
    [7 : 파워점프], [8 : 에어점프], [9 : 자석], [10 : 데스 스파이크], [11 : 뱀파이어], [12 : 멀티샷]
    [13 : 행운의 재물], [14 :  헤드샷], [15: 분노], [16 : 하드슈즈], [17 : 반동의 벽], [18 : 점프 샷], [19 : 관통샷]
    [20 : 충격파], [21 : 더블샷]*/
    //점프샷은 공격 쿨타임이아니라 개별 쿨타임으로 변경해야됨 안그럼 몬스터 없을때 무한으로 나옴
    private void SkillCheck()
    {
        if (Ninja || GameManager.Instance.PlayerSkill[0] >= 1)
        {
            if (Ninja)
            {
                bounceCnt = 3 + GameManager.Instance.PlayerSkill[0] + 1;
                BounceShoot = true;
            }
            else if(!Ninja)
            {
                bounceCnt = GameManager.Instance.PlayerSkill[0] + 1;
                BounceShoot = true;
            }            
        }
        else if (GameManager.Instance.PlayerSkill[0] == 0)
        {
            BounceShoot = false;
        }

        if (GameManager.Instance.PlayerSkill[1] == 1)
        {
            Fly = true;
        }
        else if(GameManager.Instance.PlayerSkill[1] == 0)
        {
            Fly = false;
        }

        if (GameManager.Instance.PlayerSkill[2] >= 1)
        {
            WindWalk = true;
            WindWalkSpeed = 1.5f;
        }
        else if (GameManager.Instance.PlayerSkill[2] == 0)
        {
            WindWalk = false;
            WindWalkSpeed = 1;
        }

        //점프 높이 추가
        if (GameManager.Instance.PlayerSkill[7] == 1)
        {
            JumpForce = 10f;
        }
        else if (GameManager.Instance.PlayerSkill[7] == 0)
        {
            JumpForce = 8f;
        }

        //점프 횟수 추가
        if (GameManager.Instance.PlayerSkill[8] == 1)
        {
            JumpCnt = 3;
        }
        else if (GameManager.Instance.PlayerSkill[8] == 0)
        {
            JumpCnt = 2;
        }

        //몬스터 처치 시 HP 흡혈 확률
        if (GameManager.Instance.PlayerSkill[11] >= 1)
        {
            BloodPer = 1 * GameManager.Instance.PlayerSkill[11];

            //if (GameManager.Instance.PlayerSkill[11] >= 3)
            //{
            //    BloodPer = 10;
            //}
            //else if (GameManager.Instance.PlayerSkill[11] == 2)
            //{
            //    BloodPer = 7;
            //}
            //else if (GameManager.Instance.PlayerSkill[11] == 1)
            //{
            //    BloodPer = 5;
            //}
        }
        else if(GameManager.Instance.PlayerSkill[11] == 0)
        {
            BloodPer = 0;
        }

        //아이템Box에서 드랍확률
        if (GameManager.Instance.PlayerSkill[13] >= 1)
        {
            Luck = (0.1f * GameManager.Instance.PlayerSkill[13]);
        }
        else if (GameManager.Instance.PlayerSkill[13] == 0)
        {
            Luck = 0;
        }

        //해드샷 확률
        if (GameManager.Instance.PlayerSkill[14] >= 3)
        {
            HeadShootPer = 10;
        }
        else if (GameManager.Instance.PlayerSkill[14] == 2)
        {
            HeadShootPer = 10;
        }
        else if (GameManager.Instance.PlayerSkill[14] == 1)
        {
            HeadShootPer = 5;
        }
        else if (GameManager.Instance.PlayerSkill[14] == 0)
        {
            HeadShootPer = 0;
        }

        //분노 데미지증가
        if (GameManager.Instance.PlayerSkill[15] >= 1)
        {
            if (currentHp <= (maxHp / 2))
            {
                Damage = CurrentDamage + (CurrentDamage * (0.01f * 2 * (GameManager.Instance.PlayerSkill[15])));
            }
            else
            {
                Damage = CurrentDamage;
            }
        }
        else if (GameManager.Instance.PlayerSkill[15] == 0)
        {
            Damage = CurrentDamage;
        }

        if (GameManager.Instance.PlayerSkill[17] >= 1)
        {
            WallbounceCnt = GameManager.Instance.PlayerSkill[17] + 1;
        }

    }
    private void HeroStartItem()
    {

    }
    private void HeroDataSetUp()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            switch (SelectHero)
            {
                case 0:
                    Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).KAtk;
                    maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).KHP;
                    Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).KRange;
                    ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).KASPD;
                    break;
                case 1:
                    Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WAtk;
                    maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WHP;
                    Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WRange;
                    ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WASPD;
                    break;
                case 2:
                    Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).AAtk;
                    maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).AHP;
                    Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).ARange;
                    ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).AASPD;
                    break;
                case 3:
                    Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WiAtk;
                    maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WiHP;
                    Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WiRange;
                    ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WiASPD;
                    break;
                case 4:
                    Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).NAtk;
                    maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).NHP;
                    Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).NRange;
                    ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).NASPD;
                    break;
                default:
                    break;
            }
            CurrentDamage = Damage;
        }
        if (SceneManager.GetActiveScene().name == "LobbiScene")
        {
            switch (SelectHero)
            {
                case 0:
                    Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).KAtk;
                    maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).KHP;
                    Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).KRange;
                    ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).KASPD;
                    break;
                case 1:
                    Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WAtk;
                    maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WHP;
                    Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WRange;
                    ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WASPD;
                    break;
                case 2:
                    Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).AAtk;
                    maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).AHP;
                    Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).ARange;
                    ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).AASPD;
                    break;
                case 3:
                    Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WiAtk;
                    maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WiHP;
                    Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WiRange;
                    ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WiASPD;
                    break;
                case 4:
                    Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).NAtk;
                    maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).NHP;
                    Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).NRange;
                    ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).NASPD;
                    break;
                default:
                    break;
            }
            CurrentDamage = Damage;
        }
    }
    private void HeroSetupTest()
    {
        switch (SelectHero)
        {
            case 0:
                Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).KAtk;
                maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).KHP;
                Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).KRange;
                ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).KASPD;
                break;
            case 1:
                Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WAtk;
                maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WHP;
                Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WRange;
                ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WASPD;
                break;
            case 2:
                Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).AAtk;
                maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).AHP;
                Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).ARange;
                ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).AASPD;
                break;
            case 3:
                Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WiAtk;
                maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WiHP;
                Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WiRange;
                ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).WiASPD;
                break;
            case 4:
                Damage = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).NAtk;
                maxHp = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).NHP;
                Range = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).NRange;
                ASPD = HeroDataMgr.GetTemplate(HeroLv[SelectHero]).NASPD;
                break;
            default:
                break;
        }
        CurrentDamage = Damage;
    }

    private void ThroneDateSetUp()
    {
        if (SceneManager.GetActiveScene().name == "LobbiScene")
        {
            ThroneHeroLv = HeroLv[EnthroneHeroNum];
            ThroneCostumSkin = EquipmentCostume[EnthroneHeroNum];
            ThroneWeaponSkin = EquipmentWeapon[EnthroneHeroNum];
            switch (EnthroneHeroNum)
            {
                case 0:
                    ThroneDamage = HeroDataMgr.GetTemplate(ThroneHeroLv).KAtk;
                    ThronemaxHp = HeroDataMgr.GetTemplate(ThroneHeroLv).KHP;
                    ThroneRange = HeroDataMgr.GetTemplate(ThroneHeroLv).KRange;
                    ThroneASPD = HeroDataMgr.GetTemplate(ThroneHeroLv).KASPD;
                    break;
                case 1:
                    ThroneDamage = HeroDataMgr.GetTemplate(ThroneHeroLv).WAtk;
                    ThronemaxHp = HeroDataMgr.GetTemplate(ThroneHeroLv).WHP;
                    ThroneRange = HeroDataMgr.GetTemplate(ThroneHeroLv).WRange;
                    ThroneASPD = HeroDataMgr.GetTemplate(ThroneHeroLv).WASPD;
                    break;
                case 2:
                    ThroneDamage = HeroDataMgr.GetTemplate(ThroneHeroLv).AAtk;
                    ThronemaxHp = HeroDataMgr.GetTemplate(ThroneHeroLv).AHP;
                    ThroneRange = HeroDataMgr.GetTemplate(ThroneHeroLv).ARange;
                    ThroneASPD = HeroDataMgr.GetTemplate(ThroneHeroLv).AASPD;
                    break;
                case 3:
                    ThroneDamage = HeroDataMgr.GetTemplate(ThroneHeroLv).WiAtk;
                    ThronemaxHp = HeroDataMgr.GetTemplate(ThroneHeroLv).WiHP;
                    ThroneRange = HeroDataMgr.GetTemplate(ThroneHeroLv).WiRange;
                    ThroneASPD = HeroDataMgr.GetTemplate(ThroneHeroLv).WiASPD;
                    break;
                case 4:
                    ThroneDamage = HeroDataMgr.GetTemplate(ThroneHeroLv).NAtk;
                    ThronemaxHp = HeroDataMgr.GetTemplate(ThroneHeroLv).NHP;
                    ThroneRange = HeroDataMgr.GetTemplate(ThroneHeroLv).NRange;
                    ThroneASPD = HeroDataMgr.GetTemplate(ThroneHeroLv).NASPD;
                    break;
            }
        }
    }
    private void MonsterHpMultiCheck()
    {
        if (Chapter == 0)
        {
            switch (Stage)
            {
                case 1:
                    MHpMultiIndex = 0;
                    break;
                case 2:
                    MHpMultiIndex = 1;
                    break;
                case 3:
                    MHpMultiIndex = 2;
                    break;
                case 4:
                    MHpMultiIndex = 3;
                    break;
                case 5:
                    MHpMultiIndex = 4;
                    break;
                case 6:
                    MHpMultiIndex = 5;
                    break;
                case 8:
                    MHpMultiIndex = 6;
                    break;
            }
        }
        else if (Chapter == 1)
        {
            switch (Stage)
            {
                case 1:
                    MHpMultiIndex = 7;
                    break;
                case 2:
                    MHpMultiIndex = 8;
                    break;
                case 3:
                    MHpMultiIndex = 9;
                    break;
                case 4:
                    MHpMultiIndex = 10;
                    break;
                case 5:
                    MHpMultiIndex = 11;
                    break;
                case 6:
                    MHpMultiIndex = 12;
                    break;
                case 8:
                    MHpMultiIndex = 13;
                    break;
            }
        }
        else if (Chapter == 2)
        {
            switch (Stage)
            {
                case 1:
                    MHpMultiIndex = 14;
                    break;
                case 2:
                    MHpMultiIndex = 15;
                    break;
                case 3:
                    MHpMultiIndex = 16;
                    break;
                case 4:
                    MHpMultiIndex = 17;
                    break;
                case 5:
                    MHpMultiIndex = 18;
                    break;
                case 6:
                    MHpMultiIndex = 19;
                    break;
                case 8:
                    MHpMultiIndex = 20;
                    break;
            }
        }
    }
    private void FirstSupplementEquipment()
    {
        for (int i = 0; i < EquipmentAll.Length; i++)
        {
            EquipmentAll[i] = -1;
            //EquipmentAll[i] = 0;
            switch (i)
            {
                //case 5:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 6:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 7:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 8:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 18:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 19:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 20:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 34:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 35:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 36:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 37:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 38:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 49:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 50:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 51:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 64:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 65:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 67:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 72:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 73:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 76:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 77:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 78:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 80:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 83:
                //    EquipmentAll[i] = 0;
                //    break;
                //case 85:
                //    EquipmentAll[i] = 0;
                //    break;



                case 0:
                    EquipmentAll[i] = 0;
                    break;
                case 1:
                    EquipmentAll[i] = 0;
                    break;
                case 2:
                    EquipmentAll[i] = 0;
                    break;
                case 3:
                    EquipmentAll[i] = 0;
                    break;
                case 4:
                    EquipmentAll[i] = 0;
                    break;
                case 5:
                    EquipmentAll[i] = 0;
                    break;
                case 6:
                    EquipmentAll[i] = 0;
                    break;
                case 7:
                    EquipmentAll[i] = 0;
                    break;
                case 8:
                    EquipmentAll[i] = 0;
                    break;
                case 9:
                    EquipmentAll[i] = 0;
                    break;
                case 10:
                    EquipmentAll[i] = 0;
                    break;
                case 11:
                    EquipmentAll[i] = 0;
                    break;
                case 12:
                    EquipmentAll[i] = 0;
                    break;
                case 13:
                    EquipmentAll[i] = 0;
                    break;
                case 14:
                    EquipmentAll[i] = 0;
                    break;
                case 15:
                    EquipmentAll[i] = 0;
                    break;
                case 16:
                    EquipmentAll[i] = 0;
                    break;
                case 17:
                    EquipmentAll[i] = 0;
                    break;
                case 18:
                    EquipmentAll[i] = 0;
                    break;
                case 19:
                    EquipmentAll[i] = 0;
                    break;
                case 20:
                    EquipmentAll[i] = 0;
                    break;
                case 21:
                    EquipmentAll[i] = 0;
                    break;
                case 22:
                    EquipmentAll[i] = 0;
                    break;
                case 23:
                    EquipmentAll[i] = 0;
                    break;
                case 24:
                    EquipmentAll[i] = 0;
                    break;
                case 25:
                    EquipmentAll[i] = 0;
                    break;
                case 26:
                    EquipmentAll[i] = 0;
                    break;
                case 27:
                    EquipmentAll[i] = 0;
                    break;
                case 28:
                    EquipmentAll[i] = 0;
                    break;
                case 29:
                    EquipmentAll[i] = 0;
                    break;
                case 34:
                    EquipmentAll[i] = 0;
                    break;
                case 35:
                    EquipmentAll[i] = 0;
                    break;
                case 36:
                    EquipmentAll[i] = 0;
                    break;
                case 37:
                    EquipmentAll[i] = 0;
                    break;
                case 38:
                    EquipmentAll[i] = 0;
                    break;
                case 39:
                    EquipmentAll[i] = 0;
                    break;
                case 40:
                    EquipmentAll[i] = 0;
                    break;
                case 41:
                    EquipmentAll[i] = 0;
                    break;
                case 42:
                    EquipmentAll[i] = 0;
                    break;
                case 43:
                    EquipmentAll[i] = 0;
                    break;
                case 44:
                    EquipmentAll[i] = 0;
                    break;
                case 45:
                    EquipmentAll[i] = 0;
                    break;
                case 46:
                    EquipmentAll[i] = 0;
                    break;
                case 47:
                    EquipmentAll[i] = 0;
                    break;
                case 48:
                    EquipmentAll[i] = 0;
                    break;
                case 49:
                    EquipmentAll[i] = 0;
                    break;
                case 50:
                    EquipmentAll[i] = 0;
                    break;
                case 51:
                    EquipmentAll[i] = 0;
                    break;
                case 52:
                    EquipmentAll[i] = 0;
                    break;
                case 53:
                    EquipmentAll[i] = 0;
                    break;
                case 54:
                    EquipmentAll[i] = 0;
                    break;
                case 55:
                    EquipmentAll[i] = 0;
                    break;
                case 56:
                    EquipmentAll[i] = 0;
                    break;
                case 57:
                    EquipmentAll[i] = 0;
                    break;
                case 58:
                    EquipmentAll[i] = 0;
                    break;
                case 59:
                    EquipmentAll[i] = 0;
                    break;
                case 60:
                    EquipmentAll[i] = 0;
                    break;

            }
        }
    }

    void OnLoadDataMgr()
    {
        string MultiTextResource = "01_Excel/MonsterHPMulti";
        MHpMultiDataMgr.OnDataLoad(MultiTextResource);
        string ItemTextResource = "01_Excel/Item_info";
        itemDataMgr.OnDataLoad(ItemTextResource);
        string SkillTextResource = "01_Excel/Skill";
        skillDataMgr.OnDataLoad(SkillTextResource);
        string ActiveitemTextResource = "01_Excel/ActiveItem";
        ActiveitemDataMgr.OnDataLoad(ActiveitemTextResource);
        string AntiqueitemTextResource = "01_Excel/AntiqueItem";
        AntiqueitemDataMgr.OnDataLoad(AntiqueitemTextResource);
        string TextDataResource = "01_Excel/Text_info";
        TextDataMgr.OnDataLoad(TextDataResource);
        string TextResource = "01_Excel/Hero_info";
        HeroDataMgr.OnDataLoad(TextResource);
        string ThroneSkillResource = "01_Excel/ThroneSkill";
        ThroneSkillDataMgr.OnDataLoad(ThroneSkillResource);
        string ThroneStatResource = "01_Excel/ThroneStat";
        ThroneStatDataMgr.OnDataLoad(ThroneStatResource);
    }

    //모든장비중에 가지고있는장비를 Active(활성화) 된장비칸으로 옮겨주는 코드
    public void EquipmentCarry()   
    {
        for (int i = 0; i < EquipmentAll.Length; i++)
        {
            bool Check = false;
            if (EquipmentAll[i] > -1)
            {
                for (int j = 0; j < EquipmentActive.Count; j++)
                {
                    if (EquipmentActive[j] == i)
                    {
                        Check = true;
                    }
                }

                if (!Check)
                {
                    EquipmentActive.Add(i);
                    EquipmentActive.Sort();
                }
                Check = false;
            }
        }
        for (int i = 0; i < ElementStoneAll.Length; i++)
        {
            bool Check = false;
            if (ElementStoneAll[i] > -1)
            {
                for (int j = 0; j < ElementStoneActive.Count; j++)
                {
                    if (ElementStoneActive[j] == (i + 1000))
                    {
                        Check = true;
                    }
                }
                if (!Check)
                {
                    ElementStoneActive.Add(i + 1000);
                    ElementStoneActive.Sort();                  
                }
            }
        }
        for (int i = 0; i < ActiveitemAll.Length; i++)
        {
            bool Check = false;
            if (ActiveitemAll[i] > -1)
            {
                for (int j = 0; j < ActiveitemActive.Count; j++)
                {
                    if (ActiveitemActive[j] == (i + 2000))
                    {
                        Check = true;
                    }
                }
                if (!Check)
                {
                    if (i + 2000 != 2015)
                    {
                        ActiveitemActive.Add(i + 2000);
                        ActiveitemActive.Sort();
                    }             
                }
            }
        }
    }
    public void EquipmentUse()
    {
        for (int i = 0; i < EquipmentUsedTrueCheck.Count; i++)
        {
            if (!EquipmentUsedTrueCheck[i])
            {
                EquipmentUsedTrueCheck[i] = true;
                switch (EquipmentAll[EquipmentUsed[i]])
                {
                    case 0:
                        maxHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP0;
                        currentHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP0;
                        Range += itemDataMgr.GetTemplate(EquipmentUsed[i]).Range0;
                        CurrentDamage += itemDataMgr.GetTemplate(EquipmentUsed[i]).Atk0;
                        ASPD += itemDataMgr.GetTemplate(EquipmentUsed[i]).ASPD0;
                        break;
                    case 1:
                        maxHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP1;
                        currentHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP1;
                        Range += itemDataMgr.GetTemplate(EquipmentUsed[i]).Range1;
                        CurrentDamage += itemDataMgr.GetTemplate(EquipmentUsed[i]).Atk1;
                        ASPD += itemDataMgr.GetTemplate(EquipmentUsed[i]).ASPD1;
                        break;
                    case 2:
                        maxHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP2;
                        currentHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP2;
                        Range += itemDataMgr.GetTemplate(EquipmentUsed[i]).Range2;
                        CurrentDamage += itemDataMgr.GetTemplate(EquipmentUsed[i]).Atk2;
                        ASPD += itemDataMgr.GetTemplate(EquipmentUsed[i]).ASPD2;
                        break;
                    case 3:
                        maxHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP3;
                        currentHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP3;
                        Range += itemDataMgr.GetTemplate(EquipmentUsed[i]).Range3;
                        CurrentDamage += itemDataMgr.GetTemplate(EquipmentUsed[i]).Atk3;
                        ASPD += itemDataMgr.GetTemplate(EquipmentUsed[i]).ASPD3;
                        break;
                    case 4:
                        maxHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP4;
                        currentHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP4;
                        Range += itemDataMgr.GetTemplate(EquipmentUsed[i]).Range4;
                        CurrentDamage += itemDataMgr.GetTemplate(EquipmentUsed[i]).Atk4;
                        ASPD += itemDataMgr.GetTemplate(EquipmentUsed[i]).ASPD4;
                        break;
                    case 5:
                        maxHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP5;
                        currentHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP5;
                        Range += itemDataMgr.GetTemplate(EquipmentUsed[i]).Range5;
                        CurrentDamage += itemDataMgr.GetTemplate(EquipmentUsed[i]).Atk5;
                        ASPD += itemDataMgr.GetTemplate(EquipmentUsed[i]).ASPD5;
                        break;
                    case 6:
                        maxHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP6;
                        currentHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP6;
                        Range += itemDataMgr.GetTemplate(EquipmentUsed[i]).Range6;
                        CurrentDamage += itemDataMgr.GetTemplate(EquipmentUsed[i]).Atk6;
                        ASPD += itemDataMgr.GetTemplate(EquipmentUsed[i]).ASPD6;
                        break;
                    case 7:
                        maxHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP7;
                        currentHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP7;
                        Range += itemDataMgr.GetTemplate(EquipmentUsed[i]).Range7;
                        CurrentDamage += itemDataMgr.GetTemplate(EquipmentUsed[i]).Atk7;
                        ASPD += itemDataMgr.GetTemplate(EquipmentUsed[i]).ASPD7;
                        break;
                    case 8:
                        maxHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP8;
                        currentHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP8;
                        Range += itemDataMgr.GetTemplate(EquipmentUsed[i]).Range8;
                        CurrentDamage += itemDataMgr.GetTemplate(EquipmentUsed[i]).Atk8;
                        ASPD += itemDataMgr.GetTemplate(EquipmentUsed[i]).ASPD8;
                        break;
                    case 9:
                        maxHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP9;
                        currentHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP9;
                        Range += itemDataMgr.GetTemplate(EquipmentUsed[i]).Range9;
                        CurrentDamage += itemDataMgr.GetTemplate(EquipmentUsed[i]).Atk9;
                        ASPD += itemDataMgr.GetTemplate(EquipmentUsed[i]).ASPD9;
                        break;
                    case 10:
                        maxHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP10;
                        currentHp += itemDataMgr.GetTemplate(EquipmentUsed[i]).HP10;
                        Range += itemDataMgr.GetTemplate(EquipmentUsed[i]).Range10;
                        CurrentDamage += itemDataMgr.GetTemplate(EquipmentUsed[i]).Atk10;
                        ASPD += itemDataMgr.GetTemplate(EquipmentUsed[i]).ASPD10;
                        break;
                }
                //EquipmentUsed[i]
                //스킬 좀복잡함 플레이어스킬 구하기위해서 아이템인덱스번호를 스킬 인덱스와 비교해서 스킬에서 실제 스킬값을 가져옴
                GameManager.Instance.PlayerSkill[skillDataMgr.GetTemplate(itemDataMgr.GetTemplate(EquipmentUsed[i]).Option1).CodeIndex] += skillDataMgr.GetTemplate(itemDataMgr.GetTemplate(EquipmentUsed[i]).Option1).Lv;
                GameManager.Instance.PlayerSkill[skillDataMgr.GetTemplate(itemDataMgr.GetTemplate(EquipmentUsed[i]).Option2).CodeIndex] += skillDataMgr.GetTemplate(itemDataMgr.GetTemplate(EquipmentUsed[i]).Option2).Lv;
                GameManager.Instance.PlayerSkill[skillDataMgr.GetTemplate(itemDataMgr.GetTemplate(EquipmentUsed[i]).Option3).CodeIndex] += skillDataMgr.GetTemplate(itemDataMgr.GetTemplate(EquipmentUsed[i]).Option3).Lv;
            }
        }
    }


    //슬롯머신 오픈시 보유 아이템 노출해주는 코드
    public void SlotOpenCheck()
    {
        SlotMachineManager.Instance.WindowOpenCheck = true;

        if (EquipmentActive.Count != SlotMachineManager.Instance.SlotItemObject[0].transform.childCount)
        {
            SlotMachineManager.Instance.DisplayItemSlots[0].SlotSprite.Clear();
            if (SlotMachineManager.Instance.SlotItemObject[0].transform.childCount > 0)
            {
                for (int i = 0; i < SlotMachineManager.Instance.SlotItemObject[0].transform.childCount; i++)
                {
                    Destroy(SlotMachineManager.Instance.SlotItemObject[0].transform.GetChild(i).gameObject);
                }
            }
            for (int i = 0; i < EquipmentActive.Count; i++)
            {
                if (i == 0)
                {
                    GameObject EquipmentSlotFirst = Instantiate(SlotMachineManager.Instance.Slot_Img, SlotMachineManager.Instance.SlotItemObject[0].transform.position, Quaternion.identity);
                    EquipmentSlotFirst.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + EquipmentActive[EquipmentActive.Count - 1].ToString());
                    EquipmentSlotFirst.GetComponent<SlotItemCheckScript>().ItemNum = EquipmentActive[i];
                    EquipmentSlotFirst.transform.parent = SlotMachineManager.Instance.SlotItemObject[0].transform;
                    SlotMachineManager.Instance.DisplayItemSlots[0].SlotSprite.Add(EquipmentSlotFirst.GetComponent<Image>());
                }
                GameObject EquipmentFirst = Instantiate(SlotMachineManager.Instance.Slot_Img, SlotMachineManager.Instance.SlotItemObject[0].transform.position, Quaternion.identity);
                EquipmentFirst.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + EquipmentActive[i].ToString());
                EquipmentFirst.GetComponent<SlotItemCheckScript>().ItemNum = EquipmentActive[i];
                EquipmentFirst.transform.parent = SlotMachineManager.Instance.SlotItemObject[0].transform;
                SlotMachineManager.Instance.DisplayItemSlots[0].SlotSprite.Add(EquipmentFirst.GetComponent<Image>());

                if (i == (EquipmentActive.Count - 1))
                {
                    GameObject EquipmentSlotFirst = Instantiate(SlotMachineManager.Instance.Slot_Img, SlotMachineManager.Instance.SlotItemObject[0].transform.position, Quaternion.identity);
                    EquipmentSlotFirst.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + EquipmentActive[0].ToString());
                    EquipmentSlotFirst.GetComponent<SlotItemCheckScript>().ItemNum = EquipmentActive[i];
                    EquipmentSlotFirst.transform.parent = SlotMachineManager.Instance.SlotItemObject[0].transform;
                    SlotMachineManager.Instance.DisplayItemSlots[0].SlotSprite.Add(EquipmentSlotFirst.GetComponent<Image>());
                }
            }
        }
    }

    //Tutorial
    public void SaveTutorial()
    {
        SecurityPlayerPrefs.SetInt("Tutorial", Convert.ToInt32(Tutorial));
    }
    public bool LoadTutorial()
    {
        bool LoadTutorial = Convert.ToBoolean(SecurityPlayerPrefs.GetInt("Tutorial", Convert.ToInt32(Tutorial)));
        return LoadTutorial;
    }
    //SelectHero
    public void SaveSelectHero()
    {
        SecurityPlayerPrefs.SetInt("SelectHero", SelectHero);
    }
    public int LoadSelectHero()
    {
        int LoadSelectHero = SecurityPlayerPrefs.GetInt("SelectHero", SelectHero);
        return LoadSelectHero;
    }
    //Chapter
    public void SaveChapter()
    {
        SecurityPlayerPrefs.SetInt("Chapter", Chapter);
    }
    public int LoadChapter()
    {
        int LoadChapter = SecurityPlayerPrefs.GetInt("Chapter", Chapter);
        return LoadChapter;
    }

    //StageClearCheck(Bool 배열)
    public void SaveStageClearCheck()
    {
        string strArr = "";
        for (int i = 0; i < StageClearCheck.Length; i++)
        {
            strArr = strArr + StageClearCheck[i];
            if (i < StageClearCheck.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("StageClearCheck", strArr);
    }
    public void LoadStageClearCheck()
    {
        string[] dataArr = PlayerPrefs.GetString("StageClearCheck").Split(',');
        int[] number2 = new int[dataArr.Length];

        if (dataArr.Length != StageClearCheck.Length)
        {
            for (int i = 0; i < StageClearCheck.Length; i++)
            {
                if (i < 1)
                {
                    StageClearCheck[i] = true;
                }
                else
                {
                    StageClearCheck[i] = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < dataArr.Length; i++)
            {
                if (dataArr[i] == "True")
                {
                    StageClearCheck[i] = true;
                }
                else
                {
                    StageClearCheck[i] = false;
                }
            }
        }
    }
    //EnthroneHeroNum
    public void SaveEnthroneHeroNum()
    {
        SecurityPlayerPrefs.SetInt("EnthroneHeroNum", EnthroneHeroNum);
    }
    public int LoadEnthroneHeroNum()
    {
        int LoadEnthroneHeroNum = SecurityPlayerPrefs.GetInt("EnthroneHeroNum", EnthroneHeroNum);
        return LoadEnthroneHeroNum;
    }

    //ThroneHeroLv
    public void SaveThroneHeroLv()
    {
        SecurityPlayerPrefs.SetInt("ThroneHeroLv", ThroneHeroLv);
    }
    public int LoadThroneHeroLv()
    {
        int LoadThroneHeroLv = SecurityPlayerPrefs.GetInt("ThroneHeroLv", ThroneHeroLv);
        return LoadThroneHeroLv;
    }

    //ThroneCostumSkin
    public void SaveThroneCostumSkin()
    {
        SecurityPlayerPrefs.SetInt("ThroneCostumSkin", ThroneCostumSkin);
    }
    public int LoadThroneCostumSkin()
    {
        int LoadThroneCostumSkin = SecurityPlayerPrefs.GetInt("ThroneCostumSkin", ThroneCostumSkin);
        return LoadThroneCostumSkin;
    }

    //ThroneWeaponSkin
    public void SaveThroneWeaponSkin()
    {
        SecurityPlayerPrefs.SetInt("ThroneWeaponSkin", ThroneWeaponSkin);
    }
    public int LoadThroneWeaponSkin()
    {
        int LoadThroneWeaponSkin = SecurityPlayerPrefs.GetInt("ThroneWeaponSkin", ThroneWeaponSkin);
        return LoadThroneWeaponSkin;
    }

    //ThronemaxHp
    public void SaveThronemaxHp()
    {
        SecurityPlayerPrefs.SetInt("ThronemaxHp", ThronemaxHp);
    }
    public int LoadThronemaxHp()
    {
        int LoadThronemaxHp = SecurityPlayerPrefs.GetInt("ThronemaxHp", ThronemaxHp);
        return LoadThronemaxHp;
    }

    //ThronecurrentHp
    public void SaveThronecurrentHp()
    {
        SecurityPlayerPrefs.SetInt("ThronecurrentHp", ThronecurrentHp);
    }
    public int LoadThronecurrentHp()
    {
        int LoadThronecurrentHp = SecurityPlayerPrefs.GetInt("ThronecurrentHp", ThronecurrentHp);
        return LoadThronecurrentHp;
    }

    //ThroneASPD
    public void SaveThroneASPD()
    {
        SecurityPlayerPrefs.SetFloat("ThroneASPD", ThroneASPD);
    }
    public float LoadThroneASPD()
    {
        float LoadThroneASPD = SecurityPlayerPrefs.GetFloat("ThroneASPD", ThroneASPD);
        return LoadThroneASPD;
    }

    //ThroneDamage
    public void SaveThroneDamage()
    {
        SecurityPlayerPrefs.SetFloat("ThroneDamage", ThroneDamage);
    }
    public float LoadThroneDamage()
    {
        float LoadThroneDamage = SecurityPlayerPrefs.GetFloat("ThroneDamage", ThroneDamage);
        return LoadThroneDamage;
    }

    //ThroneRange
    public void SaveThroneRange()
    {
        SecurityPlayerPrefs.SetFloat("ThroneRange", ThroneRange);
    }
    public float LoadThroneRange()
    {
        float LoadThroneRange = SecurityPlayerPrefs.GetFloat("ThroneRange", ThroneRange);
        return LoadThroneRange;
    }

    //ThroneItemList(int배열)
    public void SaveThroneItemList()
    {
        string strArr = "";
        for (int i = 0; i < ThroneItemList.Count; i++)
        {
            strArr = strArr + ThroneItemList[i];
            if (i < ThroneItemList.Count - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("ThroneItemList", strArr);
    }

    public void LoadThroneItemList()
    {
        string[] dataArr = PlayerPrefs.GetString("ThroneItemList").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                ThroneItemList.Add(number2[i]);
            }
        }
    }

    //ThroneItemLvList(int배열)
    public void SaveThroneItemLvList()
    {
        string strArr = "";
        for (int i = 0; i < ThroneItemLvList.Count; i++)
        {
            strArr = strArr + ThroneItemLvList[i];
            if (i < ThroneItemLvList.Count - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("ThroneItemLvList", strArr);
    }

    public void LoadThroneItemLvList()
    {
        string[] dataArr = PlayerPrefs.GetString("ThroneItemLvList").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                ThroneItemLvList.Add(number2[i]);
            }
        }
    }

    ////Coin
    //public void SaveCoin()
    //{
    //    SecurityPlayerPrefs.SetInt("Coin", Coin);
    //}
    //public int LoadCoin()
    //{
    //    int LoadCoin = SecurityPlayerPrefs.GetInt("Coin", Coin);
    //    return LoadCoin;
    //}

    //Gem
    public void SaveGem()
    {
        SecurityPlayerPrefs.SetInt("Gem", Gem);
    }
    public int LoadGem()
    {
        ObscuredInt LoadGem = SecurityPlayerPrefs.GetInt("Gem", Gem);
        return LoadGem;

    }

    //ADEquimentStartTime
    public void SaveADEquimentStartTime()
    {
        SecurityPlayerPrefs.SetString("ADEquimentStartTime", ADEquimentStartTime.ToString());
    }
    public DateTime LoadADEquimentStartTime()
    {
        string saveTime = SecurityPlayerPrefs.GetString("ADEquimentStartTime", ADEquimentStartTime.ToString());
        DateTime LoadADEquimentStartTime = DateTime.Parse(saveTime);

        return LoadADEquimentStartTime;
    }
    
    //ADGemStartTime
    public void SaveADGemStartTime()
    {
        SecurityPlayerPrefs.SetString("ADGemStartTime", ADGemStartTime.ToString());
    }
    public DateTime LoadADGemStartTime()
    {
        string saveTime = SecurityPlayerPrefs.GetString("ADGemStartTime", ADGemStartTime.ToString());
        DateTime LoadADGemStartTime = DateTime.Parse(saveTime);

        return LoadADGemStartTime;
    }

    //ADSoulStartTime
    public void SaveADSoulStartTime()
    {
        SecurityPlayerPrefs.SetString("ADSoulStartTime", ADSoulStartTime.ToString());
    }
    public DateTime LoadADSoulStartTime()
    {
        string saveTime = SecurityPlayerPrefs.GetString("ADSoulStartTime", ADSoulStartTime.ToString());
        DateTime LoadADSoulStartTime = DateTime.Parse(saveTime);

        return LoadADSoulStartTime;
    }

    //ADHeroStartTime
    public void SaveADHeroStartTime()
    {
        SecurityPlayerPrefs.SetString("ADHeroStartTime", ADHeroStartTime.ToString());
    }
    public DateTime LoadADHeroStartTime()
    {
        string saveTime = SecurityPlayerPrefs.GetString("ADHeroStartTime", ADHeroStartTime.ToString());
        DateTime LoadADHeroStartTime = DateTime.Parse(saveTime);

        return LoadADHeroStartTime;
    }

    //Key
    public void SaveKey()
    {
        SecurityPlayerPrefs.SetInt("Key", Key);
    }
    public int LoadKey()
    {
        int LoadKey = SecurityPlayerPrefs.GetInt("Key", Key);
        return LoadKey;
    }

    //Crafting
    public void SaveCrafting()
    {
        SecurityPlayerPrefs.SetInt("Crafting", Convert.ToInt32(Crafting));
    }
    public bool LoadCrafting()
    {
        bool LoadCrafting = Convert.ToBoolean(SecurityPlayerPrefs.GetInt("Crafting", Convert.ToInt32(Crafting)));
        return LoadCrafting;
    }

    //CraftingItemNum
    public void SaveCraftingItemNum()
    {
        SecurityPlayerPrefs.SetInt("CraftingItemNum", CraftingItemNum);
    }
    public int LoadCraftingItemNum()
    {
        int LoadCraftingItemNum = SecurityPlayerPrefs.GetInt("CraftingItemNum", CraftingItemNum);
        return LoadCraftingItemNum;
    }

    //CraftingTime
    public void SaveCraftingTime()
    {
        SecurityPlayerPrefs.SetInt("CraftingTime", CraftingTime);
    }
    public int LoadCraftingTime()
    {
        int LoadCraftingTime = SecurityPlayerPrefs.GetInt("CraftingTime", CraftingTime);
        return LoadCraftingTime;
    }

    //StartCraftingTime
    public void SaveStartCraftingTime()
    {
        SecurityPlayerPrefs.SetString("StartCraftingTime", StartCraftingTime.ToString());
    }
    public DateTime LoadStartCraftingTime()
    {
        string saveTime = SecurityPlayerPrefs.GetString("StartCraftingTime", StartCraftingTime.ToString());
        DateTime LoadStartCraftingTime = DateTime.Parse(saveTime);

        return LoadStartCraftingTime;
    }

    //ADreductionCheck
    public void SaveADreductionCheck()
    {
        SecurityPlayerPrefs.SetInt("ADreductionCheck", Convert.ToInt32(ADreductionCheck));
    }
    public bool LoadADreductionCheck()
    {
        bool LoadADreductionCheck = Convert.ToBoolean(SecurityPlayerPrefs.GetInt("ADreductionCheck", Convert.ToInt32(ADreductionCheck)));
        return LoadADreductionCheck;
    }

    //SlotCoin
    public void SaveSlotCoin()
    {
        SecurityPlayerPrefs.SetInt("SlotCoin", SlotCoin);
    }
    public int LoadSlotCoin()
    {
        int LoadSlotCoin = SecurityPlayerPrefs.GetInt("SlotCoin", SlotCoin);
        return LoadSlotCoin;
    }
    
    //SoulSpark
    public void SaveSoulSpark()
    {
        SecurityPlayerPrefs.SetInt("SoulSpark", SoulSpark);
    }
    public int LoadSoulSpark()
    {
        int LoadSoulSpark = SecurityPlayerPrefs.GetInt("SoulSpark", SoulSpark);
        return LoadSoulSpark;
    }

    //HeroHeart
    public void SaveHeroHeart()
    {
        SecurityPlayerPrefs.SetInt("HeroHeart", HeroHeart);
    }
    public int LoadHeroHeart()
    {
        int LoadHeroHeart = SecurityPlayerPrefs.GetInt("HeroHeart", HeroHeart);
        return LoadHeroHeart;
    }

    //GainGem 
    public void SaveGainGem()
    {
        SecurityPlayerPrefs.SetInt("GainGem", GainGem);
    }
    public int LoadGainGem()
    {
        int LoadGainGem = SecurityPlayerPrefs.GetInt("GainGem", GainGem);
        return LoadGainGem;
    }

    //GainSoulSpark
    public void SaveGainSoulSpark()
    {
        SecurityPlayerPrefs.SetInt("GainSoulSpark", GainSoulSpark);
    }
    public int LoadGainSoulSpark()
    {
        int LoadGainSoulSpark = SecurityPlayerPrefs.GetInt("GainSoulSpark", GainSoulSpark);
        return LoadGainSoulSpark;
    }

    //GainHeroHeart
    public void SaveGainHeroHeart()
    {
        SecurityPlayerPrefs.SetInt("GainHeroHeart", GainHeroHeart);
    }
    public int LoadGainHeroHeart()
    {
        int LoadGainHeroHeart = SecurityPlayerPrefs.GetInt("GainHeroHeart", GainHeroHeart);
        return LoadGainHeroHeart;
    }

    //MachinSlotCnt
    public void SaveMachinSlotCnt()
    {
        SecurityPlayerPrefs.SetInt("MachinSlotCnt", MachinSlotCnt);
    }
    public int LoadMachinSlotCnt()
    {
        int LoadMachinSlotCnt = SecurityPlayerPrefs.GetInt("MachinSlotCnt", MachinSlotCnt);
        return LoadMachinSlotCnt;
    }

    //LobbyMachineEquipmentNum(int배열)
    public void SaveLobbyMachineEquipmentNum()
    {
        string strArr = "";
        for (int i = 0; i < LobbyMachineEquipmentNum.Length; i++)
        {
            strArr = strArr + LobbyMachineEquipmentNum[i];
            if (i < LobbyMachineEquipmentNum.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("LobbyMachineEquipmentNum", strArr);
    }

    public void LoadLobbyMachineEquipmentNum()
    {
        string[] dataArr = PlayerPrefs.GetString("LobbyMachineEquipmentNum").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                LobbyMachineEquipmentNum[i] = number2[i];
            }
        }
    }

    //SlotActiveCheck
    public void SaveSlotActiveCheck()
    {
        SecurityPlayerPrefs.SetInt("SlotActiveCheck", Convert.ToInt32(SlotActiveCheck));
    }
    public bool LoadSlotActiveCheck()
    {
        bool LoadSlotActiveCheck = Convert.ToBoolean(SecurityPlayerPrefs.GetInt("SlotActiveCheck", Convert.ToInt32(SlotActiveCheck)));
        return LoadSlotActiveCheck;
    }

    //ADEquimentCheck
    public void SaveADEquimentCheck()
    {
        SecurityPlayerPrefs.SetInt("ADEquimentCheck", Convert.ToInt32(ADEquimentCheck));
    }
    public bool LoadADEquimentCheck()
    {
        bool LoadADEquimentCheck = Convert.ToBoolean(SecurityPlayerPrefs.GetInt("ADEquimentCheck", Convert.ToInt32(ADEquimentCheck)));
        return LoadADEquimentCheck;
    }

    //SpecialPackCnt
    public void SaveSpecialPackCnt()
    {
        SecurityPlayerPrefs.SetInt("SpecialPackCnt", SpecialPackCnt);
    }
    public int LoadSpecialPackCnt()
    {
        int LoadSpecialPackCnt = SecurityPlayerPrefs.GetInt("SpecialPackCnt", SpecialPackCnt);
        return LoadSpecialPackCnt;
    }

    //LuxuryPackCnt
    public void SaveLuxuryPackCnt()
    {
        SecurityPlayerPrefs.SetInt("LuxuryPackCnt", LuxuryPackCnt);
    }
    public int LoadLuxuryPackCnt()
    {
        int LoadLuxuryPackCnt = SecurityPlayerPrefs.GetInt("LuxuryPackCnt", LuxuryPackCnt);
        return LoadLuxuryPackCnt;
    }

    //StarterPackCnt
    public void SaveStarterPackCnt()
    {
        SecurityPlayerPrefs.SetInt("StarterPackCnt", StarterPackCnt);
    }
    public int LoadStarterPackCnt()
    {
        int LoadStarterPackCnt = SecurityPlayerPrefs.GetInt("StarterPackCnt", StarterPackCnt);
        return LoadStarterPackCnt;
    }

    //ADClearCheck
    public void SaveADClearCheck()
    {
        SecurityPlayerPrefs.SetInt("ADClearCheck", Convert.ToInt32(ADClearCheck));
    }
    public bool LoadADClearCheck()
    {
        bool LoadADClearCheck = Convert.ToBoolean(SecurityPlayerPrefs.GetInt("ADClearCheck", Convert.ToInt32(ADClearCheck)));
        return LoadADClearCheck;
    }

    //AdRemove
    public void SaveAdRemove()
    {
        SecurityPlayerPrefs.SetInt("AdRemove", AdRemove);
    }
    public int LoadAdRemove()
    {
        int LoadAdRemove = SecurityPlayerPrefs.GetInt("AdRemove", AdRemove);
        return LoadAdRemove;
    }


    //TopDownCnt
    public void SaveTopDownCnt()
    {
        SecurityPlayerPrefs.SetInt("TopDownCnt", TopDownCnt);
    }
    public int LoadTopDownCnt()
    {
        int LoadTopDownCnt = SecurityPlayerPrefs.GetInt("TopDownCnt", TopDownCnt);
        return LoadTopDownCnt;
    }

    //HandCheck
    public void SaveHandCheck()
    {
        SecurityPlayerPrefs.SetInt("HandCheck", HandCheck);
    }
    public int LoadHandCheck()
    {
        int LoadHandCheck = SecurityPlayerPrefs.GetInt("HandCheck", HandCheck);
        return LoadHandCheck;
    }

    //SlotPackCnt
    public void SaveSlotPackCnt()
    {
        SecurityPlayerPrefs.SetInt("SlotPackCnt", SlotPackCnt);
    }
    public int LoadSlotPackCnt()
    {
        int LoadSlotPackCnt = SecurityPlayerPrefs.GetInt("SlotPackCnt", SlotPackCnt);
        return LoadSlotPackCnt;
    }

    //LanguageNum
    public void SaveLanguageNum()
    {
        SecurityPlayerPrefs.SetInt("LanguageNum", LanguageNum);
    }
    public int LoadLanguageNum()
    {
        int LoadLanguageNum = SecurityPlayerPrefs.GetInt("LanguageNum", LanguageNum);
        return LoadLanguageNum;
    }

    //TotalAttendanceDay
    public void SaveTotalAttendanceDay()
    {
        SecurityPlayerPrefs.SetInt("TotalAttendanceDay", TotalAttendanceDay);
    }
    public int LoadTotalAttendanceDay()
    {
        int LoadTotalAttendanceDay = SecurityPlayerPrefs.GetInt("TotalAttendanceDay", TotalAttendanceDay);
        return LoadTotalAttendanceDay;
    }

    //AttendanceDay
    public void SaveAttendanceDay()
    {
        SecurityPlayerPrefs.SetInt("AttendanceDay", AttendanceDay);
    }
    public int LoadAttendanceDay()
    {
        int LoadAttendanceDay = SecurityPlayerPrefs.GetInt("AttendanceDay", AttendanceDay);
        return LoadAttendanceDay;
    }

    //NowDate
    public void SaveNowDate()
    {
        SecurityPlayerPrefs.SetInt("NowDate", NowDate);
    }
    public int LoadNowDate()
    {
        int LoadNowDate = SecurityPlayerPrefs.GetInt("NowDate", NowDate);
        return LoadNowDate;
    }

    //Attendancereward
    public void SaveAttendancereward()
    {
        SecurityPlayerPrefs.SetInt("Attendancereward", Convert.ToInt32(Attendancereward));
    }
    public bool LoadAttendancereward()
    {
        bool LoadAttendancereward = Convert.ToBoolean(SecurityPlayerPrefs.GetInt("Attendancereward", Convert.ToInt32(Attendancereward)));
        return LoadAttendancereward;
    }

    ////SaveAttendanceDay
    //public void SaveSaveAttendanceDay()
    //{
    //    SecurityPlayerPrefs.SetString("SaveAttendanceDay", SaveAttendanceDay.ToString());
    //}
    //public DateTime LoadSaveAttendanceDay()
    //{
    //    string saveTime = SecurityPlayerPrefs.GetString("SaveAttendanceDay", SaveAttendanceDay.ToString());
    //    DateTime LoadSaveAttendanceDay = DateTime.Parse(saveTime);

    //    return LoadSaveAttendanceDay;
    //}

    //ContinuAttendanceCheck
    public void SaveContinuAttendanceCheck()
    {
        SecurityPlayerPrefs.SetString("ContinuAttendanceCheck", ContinuAttendanceCheck.ToString());
    }
    public DateTime LoadContinuAttendanceCheck()
    {
        string saveTime = SecurityPlayerPrefs.GetString("ContinuAttendanceCheck", ContinuAttendanceCheck.ToString());
        DateTime LoadContinuAttendanceCheck = DateTime.Parse(saveTime);

        return LoadContinuAttendanceCheck;
    }
    //Dia_1_FirstCheck
    public void SaveDia_1_FirstCheck()
    {
        SecurityPlayerPrefs.SetInt("Dia_1_FirstCheck", Dia_1_FirstCheck);
    }
    public int LoadDia_1_FirstCheck()
    {
        int LoadDia_1_FirstCheck = SecurityPlayerPrefs.GetInt("Dia_1_FirstCheck", Dia_1_FirstCheck);
        return LoadDia_1_FirstCheck;
    }
    //Dia_2_FirstCheck
    public void SaveDia_2_FirstCheck()
    {
        SecurityPlayerPrefs.SetInt("Dia_2_FirstCheck", Dia_2_FirstCheck);
    }
    public int LoadDia_2_FirstCheck()
    {
        int LoadDia_2_FirstCheck = SecurityPlayerPrefs.GetInt("Dia_2_FirstCheck", Dia_2_FirstCheck);
        return LoadDia_2_FirstCheck;
    }
    //Dia_3_FirstCheck
    public void SaveDia_3_FirstCheck()
    {
        SecurityPlayerPrefs.SetInt("Dia_3_FirstCheck", Dia_3_FirstCheck);
    }
    public int LoadDia_3_FirstCheck()
    {
        int LoadDia_3_FirstCheck = SecurityPlayerPrefs.GetInt("Dia_3_FirstCheck", Dia_3_FirstCheck);
        return LoadDia_3_FirstCheck;
    }

    //ADGemreductionCheck
    public void SaveADGemreductionCheck()
    {
        SecurityPlayerPrefs.SetInt("ADGemreductionCheck", Convert.ToInt32(ADGemreductionCheck));
    }
    public bool LoadADGemreductionCheck()
    {
        bool LoadADGemreductionCheck = Convert.ToBoolean(SecurityPlayerPrefs.GetInt("ADGemreductionCheck", Convert.ToInt32(ADGemreductionCheck)));
        return LoadADGemreductionCheck;
    }

    //ADSlotCheck
    public void SaveADSlotCheck()
    {
        SecurityPlayerPrefs.SetInt("ADSlotCheck", Convert.ToInt32(ADSlotCheck));
    }
    public bool LoadADSlotCheck()
    {
        bool LoadADSlotCheck = Convert.ToBoolean(SecurityPlayerPrefs.GetInt("ADSlotCheck", Convert.ToInt32(ADSlotCheck)));
        return LoadADSlotCheck;
    }

    //ADSlotStartTime
    public void SaveADSlotStartTime()
    {
        SecurityPlayerPrefs.SetString("ADSlotStartTime", ADSlotStartTime.ToString());
    }
    public DateTime LoadADSlotStartTime()
    {
        string saveTime = SecurityPlayerPrefs.GetString("ADSlotStartTime", ADSlotStartTime.ToString());
        DateTime LoadADSlotStartTime = DateTime.Parse(saveTime);

        return LoadADSlotStartTime;
    }

    //ADSoulreductionCheck
    public void SaveADSoulreductionCheck()
    {
        SecurityPlayerPrefs.SetInt("ADSoulreductionCheck", Convert.ToInt32(ADSoulreductionCheck));
    }
    public bool LoadADSoulreductionCheck()
    {
        bool LoadADSoulreductionCheck = Convert.ToBoolean(SecurityPlayerPrefs.GetInt("ADSoulreductionCheck", Convert.ToInt32(ADSoulreductionCheck)));
        return LoadADSoulreductionCheck;
    }
    
    //ADHeroreductionCheck
    public void SaveADHeroreductionCheck()
    {
        SecurityPlayerPrefs.SetInt("ADHeroreductionCheck", Convert.ToInt32(ADHeroreductionCheck));
    }
    public bool LoadADHeroreductionCheck()
    {
        bool LoadADHeroreductionCheck = Convert.ToBoolean(SecurityPlayerPrefs.GetInt("ADHeroreductionCheck", Convert.ToInt32(ADHeroreductionCheck)));
        return LoadADHeroreductionCheck;
    }


    //HeroLv(int배열)
    public void SaveHeroLv()
    {
        string strArr = "";
        for (int i = 0; i < HeroLv.Length; i++)
        {
            strArr = strArr + HeroLv[i];
            if (i < HeroLv.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("HeroLv", strArr);
    }

    public void LoadHeroLv()
    {
        string[] dataArr = PlayerPrefs.GetString("HeroLv").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                HeroLv[i] = number2[i];
            }
        }

    }

    //InventoryItemNum(int배열)
    public void SaveInventoryItemNum()
    {
        string strArr = "";
        for (int i = 0; i < InventoryItemNum.Length; i++)
        {
            strArr = strArr + InventoryItemNum[i];
            if (i < InventoryItemNum.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("InventoryItemNum", strArr);
    }

    public void LoadInventoryItemNum()
    {
        string[] dataArr = PlayerPrefs.GetString("InventoryItemNum").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                InventoryItemNum[i] = number2[i];
            }
        }
    }

    //SelectWeapon(int배열)
    public void SaveSelectWeapon()
    {
        string strArr = "";
        for (int i = 0; i < SelectWeapon.Length; i++)
        {
            strArr = strArr + SelectWeapon[i];
            if (i < SelectWeapon.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("SelectWeapon", strArr);
    }

    public void LoadSelectWeapon()
    {
        string[] dataArr = PlayerPrefs.GetString("SelectWeapon").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                SelectWeapon[i] = number2[i];
            }
        }
    }
    //SelectCostume(int배열)
    public void SaveSelectCostume()
    {
        string strArr = "";
        for (int i = 0; i < SelectCostume.Length; i++)
        {
            strArr = strArr + SelectCostume[i];
            if (i < SelectCostume.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("SelectCostume", strArr);
    }

    public void LoadSelectCostume()
    {
        string[] dataArr = PlayerPrefs.GetString("SelectCostume").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                SelectCostume[i] = number2[i];
            }
        }
    }

    //EquipmentWeapon(int배열)
    public void SaveEquipmentWeapon()
    {
        string strArr = "";
        for (int i = 0; i < EquipmentWeapon.Length; i++)
        {
            strArr = strArr + EquipmentWeapon[i];
            if (i < EquipmentWeapon.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("EquipmentWeapon", strArr);
    }

    public void LoadEquipmentWeapon()
    {
        string[] dataArr = PlayerPrefs.GetString("EquipmentWeapon").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                EquipmentWeapon[i] = number2[i];
            }
        }
    }

    //EquipmentCostume(int배열)
    public void SaveEquipmentCostume()
    {
        string strArr = "";
        for (int i = 0; i < EquipmentCostume.Length; i++)
        {
            strArr = strArr + EquipmentCostume[i];
            if (i < EquipmentCostume.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("EquipmentCostume", strArr);
    }

    public void LoadEquipmentCostume()
    {
        string[] dataArr = PlayerPrefs.GetString("EquipmentCostume").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                EquipmentCostume[i] = number2[i];
            }
        }
    }

    //KnightWeaponSkinPurchase(int배열)
    public void SaveKnightWeaponSkinPurchase()
    {
        string strArr = "";
        for (int i = 0; i < KnightWeaponSkinPurchase.Length; i++)
        {
            strArr = strArr + KnightWeaponSkinPurchase[i];
            if (i < KnightWeaponSkinPurchase.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("KnightWeaponSkinPurchase", strArr);
    }

    public void LoadKnightWeaponSkinPurchase()
    {
        string[] dataArr = PlayerPrefs.GetString("KnightWeaponSkinPurchase").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                KnightWeaponSkinPurchase[i] = number2[i];
            }
        }
    }
    //KnightCostumeSkinPurchase(int배열)
    public void SaveKnightCostumeSkinPurchase()
    {
        string strArr = "";
        for (int i = 0; i < KnightCostumeSkinPurchase.Length; i++)
        {
            strArr = strArr + KnightCostumeSkinPurchase[i];
            if (i < KnightCostumeSkinPurchase.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("KnightCostumeSkinPurchase", strArr);
    }

    public void LoadKnightCostumeSkinPurchase()
    {
        string[] dataArr = PlayerPrefs.GetString("KnightCostumeSkinPurchase").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                KnightCostumeSkinPurchase[i] = number2[i];
            }
        }
    }

    //WarriorWeaponSkinPurchase(int배열)
    public void SaveWarriorWeaponSkinPurchase()
    {
        string strArr = "";
        for (int i = 0; i < WarriorWeaponSkinPurchase.Length; i++)
        {
            strArr = strArr + WarriorWeaponSkinPurchase[i];
            if (i < WarriorWeaponSkinPurchase.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("WarriorWeaponSkinPurchase", strArr);
    }

    public void LoadWarriorWeaponSkinPurchase()
    {
        string[] dataArr = PlayerPrefs.GetString("WarriorWeaponSkinPurchase").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                WarriorWeaponSkinPurchase[i] = number2[i];
            }
        }
    }

    //WarriorCostumeSkinPurchase(int배열)
    public void SaveWarriorCostumeSkinPurchase()
    {
        string strArr = "";
        for (int i = 0; i < WarriorCostumeSkinPurchase.Length; i++)
        {
            strArr = strArr + WarriorCostumeSkinPurchase[i];
            if (i < WarriorCostumeSkinPurchase.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("WarriorCostumeSkinPurchase", strArr);
    }

    public void LoadWarriorCostumeSkinPurchase()
    {
        string[] dataArr = PlayerPrefs.GetString("WarriorCostumeSkinPurchase").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                WarriorCostumeSkinPurchase[i] = number2[i];
            }
        }
    }

    //ArcherWeaponSkinPurchase(int배열)
    public void SaveArcherWeaponSkinPurchase()
    {
        string strArr = "";
        for (int i = 0; i < ArcherWeaponSkinPurchase.Length; i++)
        {
            strArr = strArr + ArcherWeaponSkinPurchase[i];
            if (i < ArcherWeaponSkinPurchase.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("ArcherWeaponSkinPurchase", strArr);
    }

    public void LoadArcherWeaponSkinPurchase()
    {
        string[] dataArr = PlayerPrefs.GetString("ArcherWeaponSkinPurchase").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                ArcherWeaponSkinPurchase[i] = number2[i];
            }
        }
    }

    //ArcherCostumeSkinPurchase(int배열)
    public void SaveArcherCostumeSkinPurchase()
    {
        string strArr = "";
        for (int i = 0; i < ArcherCostumeSkinPurchase.Length; i++)
        {
            strArr = strArr + ArcherCostumeSkinPurchase[i];
            if (i < ArcherCostumeSkinPurchase.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("ArcherCostumeSkinPurchase", strArr);
    }

    public void LoadArcherCostumeSkinPurchase()
    {
        string[] dataArr = PlayerPrefs.GetString("ArcherCostumeSkinPurchase").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                ArcherCostumeSkinPurchase[i] = number2[i];
            }
        }
    }

    //WizardWeaponSkinPurchase(int배열)
    public void SaveWizardWeaponSkinPurchase()
    {
        string strArr = "";
        for (int i = 0; i < WizardWeaponSkinPurchase.Length; i++)
        {
            strArr = strArr + WizardWeaponSkinPurchase[i];
            if (i < WizardWeaponSkinPurchase.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("WizardWeaponSkinPurchase", strArr);
    }

    public void LoadWizardWeaponSkinPurchase()
    {
        string[] dataArr = PlayerPrefs.GetString("WizardWeaponSkinPurchase").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                WizardWeaponSkinPurchase[i] = number2[i];
            }
        }
    }
    //WizardCostumeSkinPurchase(int배열)
    public void SaveWizardCostumeSkinPurchase()
    {
        string strArr = "";
        for (int i = 0; i < WizardCostumeSkinPurchase.Length; i++)
        {
            strArr = strArr + WizardCostumeSkinPurchase[i];
            if (i < WizardCostumeSkinPurchase.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("WizardCostumeSkinPurchase", strArr);
    }

    public void LoadWizardCostumeSkinPurchase()
    {
        string[] dataArr = PlayerPrefs.GetString("WizardCostumeSkinPurchase").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                WizardCostumeSkinPurchase[i] = number2[i];
            }
        }
    }

    //NinjaWeaponSkinPurchase(int배열)
    public void SaveNinjaWeaponSkinPurchase()
    {
        string strArr = "";
        for (int i = 0; i < NinjaWeaponSkinPurchase.Length; i++)
        {
            strArr = strArr + NinjaWeaponSkinPurchase[i];
            if (i < NinjaWeaponSkinPurchase.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("NinjaWeaponSkinPurchase", strArr);
    }

    public void LoadNinjaWeaponSkinPurchase()
    {
        string[] dataArr = PlayerPrefs.GetString("NinjaWeaponSkinPurchase").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                NinjaWeaponSkinPurchase[i] = number2[i];
            }
        }
    }

    //NinjaCostumeSkinPurchase(int배열)
    public void SaveNinjaCostumeSkinPurchase()
    {
        string strArr = "";
        for (int i = 0; i < NinjaCostumeSkinPurchase.Length; i++)
        {
            strArr = strArr + NinjaCostumeSkinPurchase[i];
            if (i < NinjaCostumeSkinPurchase.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("NinjaCostumeSkinPurchase", strArr);
    }

    public void LoadNinjaCostumeSkinPurchase()
    {
        string[] dataArr = PlayerPrefs.GetString("NinjaCostumeSkinPurchase").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                NinjaCostumeSkinPurchase[i] = number2[i];
            }
        }
    }

    //HeroPurchase(Bool 배열)
    public void SaveHeroPurchase()
    {
        string strArr = "";
        for (int i = 0; i < HeroPurchase.Length; i++)
        {
            strArr = strArr + HeroPurchase[i];
            if (i < HeroPurchase.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("HeroPurchase", strArr);
    }
    public void LoadHeroPurchase()
    {        
        string[] dataArr = PlayerPrefs.GetString("HeroPurchase").Split(',');
        int[] number2 = new int[dataArr.Length];
        if (dataArr.Length != 5)
        {
            for (int i = 0; i < HeroPurchase.Length; i++)
            {
                if (i < 2)
                {
                    HeroPurchase[i] = true;
                }
                else
                {
                    HeroPurchase[i] = false;
                }
                
            }
        }
        else
        {
            for (int i = 0; i < dataArr.Length; i++)
            {
                if (dataArr[i] == "True")
                {
                    HeroPurchase[i] = true;
                }
                else
                {
                    HeroPurchase[i] = false;
                }
            }
        }
    }
    //EquipmentAll(int배열)
    public void SaveEquipmentAll()
    {
        string strArr = "";
        for (int i = 0; i < EquipmentAll.Length; i++)
        {
            strArr = strArr + EquipmentAll[i];
            if (i < EquipmentAll.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("EquipmentAll", strArr);
    }

    public void LoadEquipmentAll()
    {
        string[] dataArr = PlayerPrefs.GetString("EquipmentAll").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                EquipmentAll[i] = number2[i];
            }
        }

    }

    //EquipmentQuantity(int배열)
    public void SaveEquipmentQuantity()
    {
        string strArr = "";
        for (int i = 0; i < EquipmentQuantity.Length; i++)
        {
            strArr = strArr + EquipmentQuantity[i];
            if (i < EquipmentQuantity.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("EquipmentQuantity", strArr);
    }

    public void LoadEquipmentQuantity()
    {
        string[] dataArr = PlayerPrefs.GetString("EquipmentQuantity").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                EquipmentQuantity[i] = number2[i];
            }
        }
    }

    //ElementStoneAll(int배열)
    public void SaveElementStoneAll()
    {
        
        string strArr = "";
        for (int i = 0; i < ElementStoneAll.Length; i++)
        {
            strArr = strArr + ElementStoneAll[i];
            if (i < ElementStoneAll.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("ElementStoneAll", strArr);
    }

    public void LoadElementStoneAll()
    {
        string[] dataArr = PlayerPrefs.GetString("ElementStoneAll").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                ElementStoneAll[i] = number2[i];
            }
        }
    }

    //ElementStoneQuantity(int배열)
    public void SaveElementStoneQuantity()
    {
        string strArr = "";
        for (int i = 0; i < ElementStoneQuantity.Length; i++)
        {
            strArr = strArr + ElementStoneQuantity[i];
            if (i < ElementStoneQuantity.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("ElementStoneQuantity", strArr);
    }

    public void LoadElementStoneQuantity()
    {
        string[] dataArr = PlayerPrefs.GetString("ElementStoneQuantity").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                ElementStoneQuantity[i] = number2[i];
            }
        }
    }

    //ActiveitemAll(int배열)
    public void SaveActiveitemAll()
    {
        string strArr = "";
        for (int i = 0; i < ActiveitemAll.Length; i++)
        {
            strArr = strArr + ActiveitemAll[i];
            if (i < ActiveitemAll.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("ActiveitemAll", strArr);
    }

    public void LoadActiveitemAll()
    {
        string[] dataArr = PlayerPrefs.GetString("ActiveitemAll").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                ActiveitemAll[i] = number2[i];
            }
        }
    }

    //ActiveitemQuantity(int배열)
    public void SaveActiveitemQuantity()
    {
        string strArr = "";
        for (int i = 0; i < ActiveitemQuantity.Length; i++)
        {
            strArr = strArr + ActiveitemQuantity[i];
            if (i < ActiveitemQuantity.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("ActiveitemQuantity", strArr);
    }

    public void LoadActiveitemQuantity()
    {
        string[] dataArr = PlayerPrefs.GetString("ActiveitemQuantity").Split(',');

        int[] number2 = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            if (dataArr[i] != "")
            {
                number2[i] = Convert.ToInt32(dataArr[i]);
                ActiveitemQuantity[i] = number2[i];
            }
        }
    }
    //FirstBlackSmithCheck
    public void SaveFirstBlackSmithCheck()
    {
        SecurityPlayerPrefs.SetInt("FirstBlackSmithCheck", Convert.ToInt32(FirstBlackSmithCheck));
    }
    public bool LoadFirstBlackSmithCheck()
    {
        bool LoadFirstBlackSmithCheck = Convert.ToBoolean(SecurityPlayerPrefs.GetInt("FirstBlackSmithCheck", Convert.ToInt32(FirstBlackSmithCheck)));
        return LoadFirstBlackSmithCheck;
    }
    ////EquipmentAll
    //public void SaveEquipmentAll()
    //{
    //    string EquipmentArr = "";
    //    for (int i = 0; i < EquipmentAll.Length; i++)
    //    {
    //        EquipmentArr = EquipmentArr + EquipmentAll[i];
    //        if (i < EquipmentAll.Length -1)
    //        {
    //            EquipmentArr = EquipmentArr + ",";
    //        }
    //    }
    //    PlayerPrefs.SetString("EquipmentAll", EquipmentArr);
    //}
    //public void LoadEquipmentAll()
    //{
    //    string[] EquipmentAllDataArr = PlayerPrefs.GetString("EquipmentAll").Split(',');
    //    Debug.Log(EquipmentAllDataArr);

    //    //int[] EquipmentAll2 = new int[EquipmentAllDataArr.Length];

    //    //for (int i = 0; i < EquipmentAllDataArr.Length; i++)
    //    //{
    //    //    EquipmentAll[i] = Convert.ToInt32(EquipmentAllDataArr[i]);
    //    //}
    //}

    //muteEffectSound
    public void SavemuteEffectSound()
    {
        PlayerPrefs.SetInt("muteEffectSound", Convert.ToInt32(muteEffectSound));
    }
    public bool LoadmuteEffectSound()
    {
        bool LoadmuteEffectSound = Convert.ToBoolean(PlayerPrefs.GetInt("muteEffectSound"));
        return LoadmuteEffectSound;
    }

    //muteBGM
    public void SavemuteBGM()
    {
        PlayerPrefs.SetInt("muteBGM", Convert.ToInt32(muteBGM));
    }
    public bool LoadmuteBGM()
    {
        bool LoadmuteBGM = Convert.ToBoolean(PlayerPrefs.GetInt("muteBGM"));
        return LoadmuteBGM;
    }
}
