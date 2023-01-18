using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.Purchasing;
using WeightedRandomization;
using CodeStage.AntiCheat.ObscuredTypes;

public class LobbiManager : MonoBehaviour
{
    //private static LobbiManager s_MyObject = null;
    //public static LobbiManager instance
    //{
    //    get
    //    {
    //        if (s_MyObject == null)
    //        {
    //            s_MyObject = FindObjectOfType(typeof(LobbiManager)) as LobbiManager;
    //            if (s_MyObject == null)
    //            {
    //                GameObject obj = new GameObject("MyChar");
    //                s_MyObject = obj.AddComponent(typeof(LobbiManager)) as LobbiManager;
    //            }
    //        }
    //        return s_MyObject;
    //    }
    //}
    private static LobbiManager _instance = null;
    public static LobbiManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Singleton == null");
            }
            return _instance;
        }
    }
    /*************************************************************************************/
    MyObject myChar;
    public int TEST1, Cnt;
    public Text Ver_Text;
    public GameObject Panels;
    [SerializeField]
    private List<int> WeaponSkinCnt = new List<int>();
    [SerializeField]
    private List<int> CostumeSkinCnt = new List<int>();
    private int SkinCheck = -1;

    public int DayTest;
    public int Box1_Index, Box2_Index, ItemCnt, BoxNo;
    public bool SkinCardOpenCheck = false;
    public bool SkinCollocateCheck = false; //이거없으면 패키지에서 스킨주는창다음에 아이템창에서 아이템배열계쏙 2(스킨수치)로 고정됨
    public HeroTemplate HeroData;
    public HeroTemplateMgr HeroDataMgr;
    public SkillInfoTemplate SkillData;
    public SkillInfoTemplateMgr SkillDataMgr;
    public ShopTemplate ShopData;
    public ShopTemplateMgr ShopDataMgr;
    public SkinDataTemplate SkinData;
    public SkinDataTemplateMgr SkinDataMgr;
    public CashShopTemplate CashShopData;
    public CashShopTemplateMgr CashShopDataMgr;
    public GachaTemplate GachaData;
    public GachaTemplateMgr GachaDataMgr;
    public BlackSmithTemplate BlackSmithData;
    public BlackSmithTemplateMgr BlackSmithDataMgr;
    public AttendanceTemplate AttendanceData;
    public AttendanceTemplateMgr AttendanceDataMgr;

    public List<GameObject> ItemAtkGage, ItemASPDGage, ItemRangeGage, ItemHPGage = new List<GameObject>();
    public List<GameObject> ThroneItemAtkGage, ThroneItemASPDGage, ThroneItemRangeGage, ThroneItemHPGage = new List<GameObject>();

    private int ItemAtk, ItemHP;
    private float ItemASPD, ItemRange;
    [SerializeField]
    private int ThroneItemAtk, ThroneItemHP;
    private float ThroneItemASPD, ThroneItemRange;
    private int ContentInfoNum;
    private string AddText;
    [SerializeField]
    private int StageClearCheck;
    private bool StageCheckTF;

    //SkinCard_Panel
    //GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("Card_Panel").GetChild(0).GetChild(0)
    public GameObject GachaPanel;
    private GameObject Gacha_PackPanel;
    private GameObject Card_Panel;
    private GameObject SkinCard_Panel;
    private GameObject AD_Box;
    private GameObject Play_Box;
    private GameObject Gem_Box;
    public GameObject[] SkinCard;
    public GameObject[] Card;
    public GameObject[] CardParent;
    private GameObject Btn_UI_Panel;
    private GameObject SelectBar;
    private GameObject HeroInfo_Panel;
    private GameObject CharacterBackground;
    private GameObject HeroNameWindow;
    private GameObject HeroStats_Panel;
    private GameObject HeroViewport;
    private GameObject HeroCharacterViewport;
    [SerializeField] private GameObject SkinInfoPanel;
    public GameObject InventoryPanel;
    private GameObject UnpurchasedInfo_Panel;
    private GameObject EnthroneHeroInfo_Panel;
    //private GameObject ThroneItemInfo_Panel;
    public GameObject BlackSmithPanel;
    [SerializeField] private GameObject BlackSmithInfoPanel;
    public GameObject CashShopPanel;
    private GameObject ContentInfoPanel;
    private GameObject ADRemoveAndSlotPanel;
    private GameObject CashShopInfoPanel;
    private GameObject LuxuryPackPanel;
    private GameObject SpecialPackPanel;
    private GameObject StaterPackPanel;
    private GameObject BasicProductPanel;
    private GameObject ThroneItemPanel;
    //private GameObject EnthroneHero_Panel;


    private Text KeyText;
    private Text Key2Text;
    private Text Key3Text;
    private Text Key4Text;

    [Header("========== Text ==========")]
    [SerializeField] private Text HeroUI_SkinText;
    [SerializeField] private Text HeroUI_LvUpText;
    [SerializeField] private Text HeroUI_MaxLvText;

    //private Text Slot_Coin_Btn_Text;
    private List<GameObject> ClothingCostum;
    private List<GameObject> WeaponeCostum;
    private GameObject CoinBtn;
    private GameObject CashBtn;
    private GameObject Gem_UI;
    private GameObject Soul_UI;
    private GameObject HeroCoin_UI;
    private GameObject Start_btn;
    private GameObject Throne_btn;
    private GameObject StageRect;
    public GameObject Hero_UP_Btn;
    public GameObject EnthroneHero_UP_Btn;

    private GameObject DontStart_btn;   //이건 로비이동모양
    public GameObject ShopClose_Btn;
    public GameObject ForgeClose_Btn;
    public GameObject DontStart2_btn;  //이건 철창모양

    public GameObject Coin_Img;
    [Header("========== Image ==========")]
    [SerializeField] private Image PreferencesUI_TopDown_Btn;
    [SerializeField] private Sprite[] TopDown_Img = new Sprite[3];
    [SerializeField] private Image PreferencesUI_Hand_Btn;
    [SerializeField] private Sprite[] Hand_Img = new Sprite[2];

    public List<GameObject> HeroBackPanel = new List<GameObject>();
    //public List<GameObject> AtkGage, ASPDGage, RangeGage, HPGage = new List<GameObject>();
    //public List<GameObject> KnightGrade, WarriorGrade, ArcherGrade, WizardGrade, NinjaGrade = new List<GameObject>();

    public List<Sprite> HeroBackPanelActive = new List<Sprite>();
    public List<Sprite> HeroBackPanelNoneActive = new List<Sprite>();

    [SerializeField]
    private List<int> GachaTable_1 = new List<int>();
    [SerializeField]
    private List<int> GachaTable_2 = new List<int>();

    public List<int> GachaNomal = new List<int>();
    public List<int> GachaRare = new List<int>();
    public List<int> GachaUnique = new List<int>();

    [SerializeField]
    private List<int> ChoiceGachaItem = new List<int>();

    [SerializeField]
    private List<int> BlackSmithitemlist = new List<int>();

    private Text[] StatsText;
    private Text HeroInfo_Text;

    public GameObject SlotAD_CoolTime_Btn;
    public Text SlotAD_CoolTime_Text;
    public Image[] SlotItem;
    public GameObject AttendanceNotice_1;
    public GameObject AttendanceNotice_2;

    public GameObject ADBox_Btn;
    public GameObject ADBox_CoolTime_Btn;
    public Text ADBox_CoolTime_Text;
    public GameObject MissionBox_Btn;
    public GameObject GemBox_Btn;
    public Text GemBox_Btn_Text;
    public GameObject GemBox_10_Btn;
    public Text GemBox_10_Btn_Text;
    //private GameObject SlotBackGroundPanel;
    public GameObject[] ItemClosePanel = new GameObject[2];
    public GameObject AttendanceWindow;
    public GameObject PreferencesWindow;
    public GameObject StageSelectWindow;
    public GameObject ForgeBtn_UI;
    public GameObject SkinBtn_UI;
    public GameObject[] CashBtn_Spark = new GameObject[3];
    public GameObject[] CashBtn_HeroHeart = new GameObject[3];
    public GameObject[] CashBtn_KeyAndSlot = new GameObject[3];
    public GameObject Blacksmith_TimeGem_Btn;
    public GameObject Blacksmith_DontTimeGem_Btn;
    
    [SerializeField]
    private bool StartTF = true;
    public bool SlotActiveCheck = false;
    [SerializeField]
    private int CheckCnt = 1;
    [SerializeField]
    private int BasicProductCnt = 0;
    private int ForgeCntCheck;
    public int SortCnt = 0;     //ADRemove = 1, Slot = 2 / ItemBox = 3, Cost = 4

    [Header("장비별최대레벨")]         //장비별 maxLv설정해줘야 대장간에서 업그레이드 오류안남(blacksmith시트에맞춰서 작성해주기)
    public int EquipmentMaxLv;
    public int ElementalStoneMaxLv;
    public int AntiqueitemMaxLv;    
    [Space(10f)]
    public int SelectInventoryNum;
    public int SelectInventoryIndex;
    public int SelectInventoryArrayNum;

    public int SelectMachineEquipmentNum;
    public int SelectForgeNum;
    public int preSelectForgeNum;
    public int SelectForgeIndex;
    public int ThroneItem_Num;

    public RuntimeAnimatorController[] anims;

    private bool ArrowCheck = false;
    [SerializeField]
    private bool ItemGageCheck = false;
    private bool SlotGageCheck = false;
    private bool ThroneItemCheck = false;
    [SerializeField]
    private List<int> EquipmentSortNum = new List<int>();
    public bool EquipmentArrangement = false;
    public bool EquipmentCheck = false;
    [SerializeField]
    private List<int> InventorySortNum = new List<int>();
    public bool InventoryArrangement = false;
    public bool InventoryCheck = false;
    public bool ActiveCheck = false;
    public int ForgeWindowNum = 0;      //1 : 장비, 2 : 아이템
    [SerializeField]
    private int Unique, Rare, Nomal;

    [SerializeField]
    private int GemPurchaseNum;

    public GameObject GachaCard;
    public int PurchasePackageNum;
    public List<bool> CardOpenCheckList = new List<bool>();
    public bool CatStretchCheck = false;

    private int CardOpenCnt;
    [SerializeField]
    private bool PackageCheck = false;              //패키지 상품인지 일반뽑기인지 체크하기위해 필요
    private bool PackagePurchaseCheck = false;      //패키지상품 구매했을경우 해당상품 1회만 정보변경을위해 필요
    private bool GachaTableCheck_1 = false;         //뽑기중 GachaIndex1의 무한한 변경이 안되게 해주기위해 필요 
    private bool GachaTableCheck_2 = false;         //뽑기중 GachaIndex2의 무한한 변경이 안되게 해주기위해 필요
    private bool CardChoiceCheck = false;
    [SerializeField]
    private bool CardCreateCheck = false;
    [SerializeField]
    private bool PackageCardCheck = false;

    public GameObject Notice_ComingSoon;
    public GameObject Notice_Notenough;
    public GameObject ComingSoon_Parent_Panel;
    public GameObject Notice_AD_Panel;
    public Text Notice_AD_Panel_Text;

    private int BlackSmithWindowCheck;
    private int ActiveItemIndex;        //ActiveItem의 index가 1000~20025로 시작해서 필요함

    private void Awake()
    {
        _instance = this;
        myChar = MyObject.MyChar;
        HeroDataMgr = new HeroTemplateMgr();
        SkillDataMgr = new SkillInfoTemplateMgr();
        ShopDataMgr = new ShopTemplateMgr();
        SkinDataMgr = new SkinDataTemplateMgr();
        CashShopDataMgr = new CashShopTemplateMgr();
        GachaDataMgr = new GachaTemplateMgr();
        BlackSmithDataMgr = new BlackSmithTemplateMgr();
        AttendanceDataMgr = new AttendanceTemplateMgr();
        EquipmentMaxLv = 11;
        ElementalStoneMaxLv = 11;
        AntiqueitemMaxLv = 11;

    SelectBar = GameObject.Find("SelectBar");
        Btn_UI_Panel = GameObject.Find("Btn_UI_Panel");
        HeroInfo_Panel = GameObject.Find("HeroInfo_Panel");
        CharacterBackground = GameObject.Find("Character_BackGround");
        HeroNameWindow = GameObject.Find("HeroName_Text");
        HeroStats_Panel = GameObject.Find("HeroStats_Panel");
        HeroViewport = GameObject.Find("HeroViewport");
        HeroCharacterViewport = GameObject.Find("HeroCharacterViewport");
        //InventoryPanel = GameObject.Find("Inventory_Panel");
        //SkinInfoPanel = GameObject.Find("SkinInfo_Panel");
        UnpurchasedInfo_Panel = GameObject.Find("UnpurchasedInfo_Panel");
        EnthroneHeroInfo_Panel = GameObject.Find("EnthroneHeroInfo_Panel");
        //ThroneItemInfo_Panel = GameObject.Find("ThroneItemInfo_Panel");
        BlackSmithPanel = GameObject.Find("BlackSmithPanel");
        CashShopPanel = GameObject.Find("CashShopPanel");
        ContentInfoPanel = GameObject.Find("ContentInfo_Panel");
        GachaPanel = GameObject.Find("Gacha_Panel");
        ADRemoveAndSlotPanel = GameObject.Find("ADRemoveAndSlot_Panel");
        CashShopInfoPanel = GameObject.Find("CashShopInfo_Panel");
        LuxuryPackPanel = GameObject.Find("LuxuryPack_Panel");
        SpecialPackPanel = GameObject.Find("SpecialPack_Panel");
        StaterPackPanel = GameObject.Find("StaterPack_Panel");
        BasicProductPanel = GameObject.Find("BasicProduct_Panel");
        //BlackSmithInfoPanel = GameObject.Find("BlackSmithInfo_Panel");
        ThroneItemPanel = GameObject.Find("ThroneItem_Panel");
        //EnthroneHero_Panel = GameObject.Find("EnthroneHero_Panel");
        KeyText = GameObject.Find("Key_Text").GetComponent<Text>();
        Key2Text = GameObject.Find("Key2_Text").GetComponent<Text>();
        Key3Text = GameObject.Find("Key3_Text").GetComponent<Text>();
        Key4Text = GameObject.Find("Key4_Text").GetComponent<Text>();
        //Slot_Coin_Btn_Text = GameObject.Find("Slot_Coin_Btn").GetComponentInChildren<Text>();
        CoinBtn = GameObject.Find("Slot_Coin_Btn");
        CashBtn = GameObject.Find("Slot_Cash_Btn");
        Coin_Img = GameObject.Find("Coin_Img");
        Gem_UI = GameObject.Find("Gem_UI");
        Soul_UI = GameObject.Find("Soul_UI");
        HeroCoin_UI = GameObject.Find("HeroCoin_UI");
        Start_btn = GameObject.Find("Start_btn");
        DontStart_btn = GameObject.Find("DontStart_btn");
        Throne_btn = GameObject.Find("Throne_btn");
        //Hero_UP_Btn = GameObject.Find("Hero_UP_Btn");
        HeroInfo_Text = GameObject.Find("HeroInfo_Text").GetComponent<Text>();

        //SlotBackGroundPanel = GameObject.Find("SlotBackGroundPanel");
        AttendanceWindow = GameObject.Find("Attendance_Panel");
        StageSelectWindow = GameObject.Find("StageSelect_Panel");
        //PreferencesWindow = GameObject.Find("Preferences_Panel");

        StatsText = new Text[HeroStats_Panel.transform.childCount];

        Card_Panel = GachaPanel.transform.GetChild(0).Find("Card_Panel").gameObject;
        Gacha_PackPanel = GachaPanel.transform.GetChild(0).Find("Pack_Panel").gameObject;
        SkinCard_Panel = GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("SkinCard_Panel").gameObject;
        AD_Box = GachaPanel.transform.GetChild(0).Find("Box_Panel").Find("AD_Box(Shadow)").gameObject;
        Play_Box = GachaPanel.transform.GetChild(0).Find("Box_Panel").Find("Play_Box(Shadow)").gameObject;
        Gem_Box = GachaPanel.transform.GetChild(0).Find("Box_Panel").Find("Gem_Box(Shadow)").gameObject;

        //for (int i = 0; i < 30; i++)
        //{
        //    AtkGage.Add(GameObject.Find("AtkGage").transform.GetChild(i).gameObject);
        //    ASPDGage.Add(GameObject.Find("ASPDGage").transform.GetChild(i).gameObject);
        //    RangeGage.Add(GameObject.Find("RangeGage").transform.GetChild(i).gameObject);
        //    HPGage.Add(GameObject.Find("HPGage").transform.GetChild(i).gameObject);
        //    AtkGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    ASPDGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    RangeGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    HPGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //}
        for (int i = 0; i < HeroViewport.transform.childCount; i++)
        {
            HeroBackPanel.Add(HeroViewport.transform.GetChild(i).GetChild(0).gameObject);
        }
        for (int i = 0; i < HeroStats_Panel.transform.childCount; i++)
        {
            StatsText[i] = HeroStats_Panel.transform.GetChild(i).GetComponentInChildren<Text>();
        }
        //for (int i = 0; i < 10; i++)
        //{
        //    ItemAtkGage.Add(GameObject.Find("ItemAtkGage").transform.GetChild(i).gameObject);
        //    ItemASPDGage.Add(GameObject.Find("ItemASPDGage").transform.GetChild(i).gameObject);
        //    ItemRangeGage.Add(GameObject.Find("ItemRangeGage").transform.GetChild(i).gameObject);
        //    ItemHPGage.Add(GameObject.Find("ItemHPGage").transform.GetChild(i).gameObject);
        //    ItemAtkGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    ItemASPDGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    ItemRangeGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    ItemHPGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //}
        InventoryPanel.SetActive(false);
        BlackSmithPanel.SetActive(false);
        AttendanceWindow.SetActive(false);
        StageSelectWindow.SetActive(false);
        PreferencesWindow.SetActive(false);
        ContentInfoPanel.SetActive(false);
        BlackSmithInfoPanel.SetActive(false);
        //ThroneItemInfo_Panel.SetActive(false);
        CashShopPanel.SetActive(false);
        GachaPanel.SetActive(false);
        SpecialPackPanel.SetActive(false);
        LuxuryPackPanel.SetActive(false);
        StaterPackPanel.SetActive(false);
        BasicProductPanel.SetActive(false);
        //EnthroneHero_Panel.SetActive(false);
        DontStart_btn.SetActive(false);
        Throne_btn.SetActive(false);
        SkinInfoPanel.SetActive(false);
        UnpurchasedInfo_Panel.SetActive(false);
        EnthroneHeroInfo_Panel.SetActive(false);
        ADRemoveAndSlotPanel.SetActive(false);
        CashShopInfoPanel.SetActive(false);
        //SlotBackGroundPanel.SetActive(false);

    }
    private void Start()
    {
        OnLoadHeroTemplateMgr();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    myChar.ContinuAttendanceCheck = UnbiasedTime.Instance.Now();
        //    //StartCoroutine(Count(true, myChar.Gem + 100, myChar.Gem));
        //}
        if (StartTF)
        {
            if (myChar.HeroPurchase[myChar.SelectHero])
            {
                if (!InventoryPanel.activeSelf)
                {
                    Start_btn.SetActive(true);
                    DontStart_btn.SetActive(false);
                    Throne_btn.SetActive(false);
                    DontStart2_btn.SetActive(false);
                    KeyText.text = myChar.Key.ToString("F0");
                }
                else
                {

                    Start_btn.SetActive(false);
                    DontStart_btn.SetActive(true);
                    Throne_btn.SetActive(false);
                    DontStart2_btn.SetActive(false);
                    Key2Text.text = myChar.Key.ToString("F0");
                }
                //if (myChar.EnthroneHeroNum != myChar.SelectHero)
                //{
                //    Start_btn.SetActive(true);
                //    DontStart_btn.SetActive(false);
                //    Throne_btn.SetActive(false);
                //    DontStart2_btn.SetActive(false);
                //    KeyText.text = myChar.Key.ToString("F0");
                //}
                //else
                //{
                //    Start_btn.SetActive(false);
                //    DontStart_btn.SetActive(false);
                //    Throne_btn.SetActive(true);
                //    DontStart2_btn.SetActive(false);
                //    Key3Text.text = myChar.Key.ToString("F0");
                //}

            }
            else
            {
                if (!InventoryPanel.activeSelf)
                {
                    Start_btn.SetActive(false);
                    Throne_btn.SetActive(false);
                    DontStart2_btn.SetActive(true);
                    DontStart_btn.SetActive(false);
                    Key4Text.text = myChar.Key.ToString("F0");
                }
                else
                {

                    Start_btn.SetActive(false);
                    DontStart_btn.SetActive(true);
                    Throne_btn.SetActive(false);
                    DontStart2_btn.SetActive(false);
                    Key2Text.text = myChar.Key.ToString("F0");
                }

                //Start_btn.SetActive(false);
                //Throne_btn.SetActive(false);
                //DontStart2_btn.SetActive(true);
                //DontStart_btn.SetActive(false);
                //Key4Text.text = myChar.Key.ToString("F0");

                //if (myChar.EnthroneHeroNum != myChar.SelectHero)
                //{
                //    Start_btn.SetActive(false);
                //    Throne_btn.SetActive(false);
                //    DontStart2_btn.SetActive(true);
                //    DontStart_btn.SetActive(false);
                //    Key4Text.text = myChar.Key.ToString("F0");
                //}
                //else
                //{
                //    Start_btn.SetActive(false);
                //    DontStart_btn.SetActive(false);
                //    Throne_btn.SetActive(true);
                //    Key3Text.text = myChar.Key.ToString("F0");
                //}
            }
        }
        else
        {
            Start_btn.SetActive(false);
            DontStart_btn.SetActive(true);
            Throne_btn.SetActive(false);
            DontStart2_btn.SetActive(false);
            Key2Text.text = myChar.Key.ToString("F0");
        }

        if (Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
        DayCheck();
        HeroUIManager();
        InventoryBtnCheck();
        //ThroneUIManager();
        GoodsUI_CHeck();
        //SlotItem_Img_Check();
        SelectCharacterCheck();
        HeroUpgradeCheck();
        InventoryPanelCheck();
        //ItemInfoPanelCheck();
        //ThroneItemInfoCheck();
        SkinInfoPanelCheck();
        BlacksmithPanelCheck();
        CashShopPanelCheck();
        StarterPackPanelCheck();
        SpecialPackPanelCheck();
        LuxuryPackPanelCheck();
        ADRemoveAndSlotPanelCheck();
        CashShopInfoPanelCheck();
        BasicProductPanelCheck();
        GachaPanelCheck();
        Package_Purchase(PurchasePackageNum);
        TextCheck();
        HeroWeaponeCheck();
        adOpenCheck();
        StageSelectPanelCheck();
        ThroneItemPanelCheck();

        //if (!ItemInfo_Panel.activeSelf && !BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").gameObject.activeSelf)
        //{
        //    ItemGageCheck = false;
        //}

        //Slot_Coin_Btn_Text.text = myChar.SlotCoin.ToString("F0");

        //SlotPanelControl();
        HeroBackPanelCheck();
        PreferencesWindowCheck();
    }
    public void ClothingCostumeItemCheck(int num)
    {
        myChar.SelectClotihingCostume = num;
    }
    public void WeaponCostumeItemCheck(int num)
    {
        myChar.SelectWeaponCostume = num;
    }

    private void adOpenCheck()
    {
        if (myChar.AD_Enought_Check)
        {
            myChar.AD_Road_Time -= Time.deltaTime;
            if (myChar.AD_Road_Time > 0)
            {
                Notice_AD_Panel.SetActive(true);

                switch ((int)myChar.AD_Road_Time % 3)
                {
                    case 0:
                        Notice_AD_Panel_Text.text = "Loading...";
                        break;
                    case 1:
                        Notice_AD_Panel_Text.text = "Loading..";
                        break;
                    case 2:
                        Notice_AD_Panel_Text.text = "Loading.";
                        break;
                }
            }
            else
            {
                myChar.AD_Enought_Check = false;
                if (myChar.AD_FailedToLoad)
                {
                    switch (myChar.AD_Num)
                    {
                        case 0:     //슬롯머신
                            SlotMachineManager.Instance.StartSlot(0);
                            break;
                        case 1:     //클리어 배수보상;
                            //Clear_AD_Btn.SetActive(false);
                            myChar.MultipleCheck = true;
                            myChar.Gain_Multiple = 2;
                            GameManager.Instance.ClearPlusEffect();
                            FirebaseManager.firebaseManager.ResultWatchAD("Clear");
                            break;
                        case 2:     //실패 배수보상
                            //Fail_AD_Btn.SetActive(false);
                            myChar.MultipleCheck = true;
                            myChar.Gain_Multiple = 2;
                            GameManager.Instance.FailPlusEffect();
                            FirebaseManager.firebaseManager.ResultWatchAD("Fail");
                            break;
                        case 3:
                            GameManager.Instance.AdMerchin.GetComponent<BossGiftBoxScript>().OpenBox();
                            GameManager.Instance.AdMerchin.GetComponent<BossGiftBoxScript>().WindowCheck = true;
                            if (!myChar.Tutorial)
                            {
                                StartCoroutine(RoomManager.Instance.GiftBoxOpen_DoorInvisible());
                            }
                            FirebaseManager.firebaseManager.IngameTreasureChestOpen("AD_GiftBox", 0);
                            break;
                        case 10:    //Admerchin보상
                            GameManager.Instance.ADUse();
                            break;
                        case 11:    //부활
                            GameManager.Instance.ResurrectionBtn(0);
                            //Resurrection_AD_Btn.SetActive(false);
                            break;
                        case 50:    //대장간광고
                            LobbiManager.instance.ForgeSkip_Btn(0);
                            break;
                        case 100:   //장비뽑기
                            LobbiManager.instance.AdCostCheck(100);
                            FirebaseManager.firebaseManager.ShopProductPurchase("AD_Equipment");
                            break;
                        case 200:   //보석뽑기
                            LobbiManager.instance.AdCostCheck(200);
                            FirebaseManager.firebaseManager.ShopProductPurchase("AD_Dia");
                            break;
                        case 201:   //soulspark뽑기
                            LobbiManager.instance.AdCostCheck(201);
                            FirebaseManager.firebaseManager.ShopProductPurchase("AD_SoulFlame");
                            break;
                        case 202:   //heroheart뽑기
                            LobbiManager.instance.AdCostCheck(202);
                            FirebaseManager.firebaseManager.ShopProductPurchase("AD_HeroToken");
                            break;
                    }
                }
            }
        }
        else
        {
            Notice_AD_Panel.SetActive(false);
        }
    }

    //private void SlotPanelControl()
    //{
    //    if (myChar.MachinSlotCnt == 1)
    //    {
    //        for (int i = 0; i < 2; i++)
    //        {
    //            ItemClosePanel[i].SetActive(true);
    //        }
    //    }
    //    else if (myChar.MachinSlotCnt == 2)
    //    {
    //        ItemClosePanel[0].SetActive(false);
    //        ItemClosePanel[1].SetActive(true);
    //    }
    //    else if (myChar.MachinSlotCnt == 3)
    //    {
    //        for (int i = 0; i < 2; i++)
    //        {
    //            ItemClosePanel[i].SetActive(false);
    //        }
    //    }
    //    if (SlotPanel.activeSelf == true)
    //    {
    //        HeroInfo_Panel.SetActive(false);
    //        SlotNotice.SetActive(false);
    //        if (myChar.SlotActiveCheck)
    //        {
    //            if (myChar.LobbyMachineEquipmentNum.Length < 3)
    //            {
    //                myChar.LobbyMachineEquipmentNum = new int[3];
    //            }
    //            switch (myChar.MachinSlotCnt)
    //            {
    //                case 1:
    //                    myChar.LobbyMachineEquipmentNum[1] = -1;
    //                    myChar.LobbyMachineEquipmentNum[2] = -1;
    //                    break;
    //                case 2:
    //                    myChar.LobbyMachineEquipmentNum[2] = -1;
    //                    break;
    //            }
    //        }
    //        if (!myChar.ADSlotCheck)
    //        {
    //            SlotAD_CoolTime_Btn.SetActive(false);
    //        }
    //        else
    //        {
    //            SlotAD_CoolTime_Btn.SetActive(true);
    //            System.TimeSpan ADSlot_conversion_Int = myChar.ADSlotStartTime - UnbiasedTime.Instance.Now();
    //            int UntilCompleteTime = System.Convert.ToInt32(ADSlot_conversion_Int.TotalSeconds);

    //            int a_hour = (UntilCompleteTime / 3600);
    //            int a_min = (UntilCompleteTime % 3600 / 60);
    //            int a_sec = (UntilCompleteTime % 3600 % 60);

    //            SlotAD_CoolTime_Text.text = string.Format("{0:D2}:{1:D2}:{2:D2}", a_hour, a_min, a_sec);
    //            if (UntilCompleteTime <= 0)
    //            {
    //                myChar.ADSlotCheck = false;
    //            }
    //        }

    //    }
    //    else
    //    {
    //        HeroInfo_Panel.SetActive(true);
    //        if (!myChar.SlotActiveCheck)
    //        {
    //            SlotNotice.SetActive(true);

    //            myChar.LobbyMachineEquipmentNum = new int[3];
    //            myChar.LobbyMachineEquipmentNum[0] = -1;
    //            myChar.LobbyMachineEquipmentNum[1] = -1;
    //            myChar.LobbyMachineEquipmentNum[2] = -1;
    //        }
    //        else
    //        {
    //            SlotNotice.SetActive(false);
    //        }
    //    }
    //}
    private void InventoryBtnCheck()
    {
        for (int i = 0; i < ThroneItemPanel.transform.childCount; i++)
        {
            if (i < myChar.SlotPackCnt)
            {
                ThroneItemPanel.transform.GetChild(3 - i).GetComponent<Button>().enabled = false;
            }
            else
            {
                ThroneItemPanel.transform.GetChild(3 - i).GetComponent<Button>().enabled = true;
            }
        }
        
    }
    private void HeroGage(float HeroAtk, float HeroASPD, float HeroRange, int HeroHP)
    {

        HeroStats_Panel.transform.GetChild(0).Find("LV_Text").GetComponent<Text>().text = HeroAtk.ToString();
        HeroStats_Panel.transform.GetChild(1).Find("LV_Text").GetComponent<Text>().text = HeroASPD.ToString();
        HeroStats_Panel.transform.GetChild(2).Find("LV_Text").GetComponent<Text>().text = HeroRange.ToString();
        HeroStats_Panel.transform.GetChild(3).Find("LV_Text").GetComponent<Text>().text = HeroHP.ToString();

        myChar.CurrentDamage = HeroAtk;
        myChar.ASPD = HeroASPD;
        myChar.Range = HeroRange;
        myChar.maxHp = HeroHP;
        //for (int i = 0; i < 30; i++)
        //{
        //    if (i < HeroAtk)
        //    {
        //        AtkGage[i].transform.GetChild(0).gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        AtkGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    }

        //    if (i < HeroASPD)
        //    {
        //        ASPDGage[i].transform.GetChild(0).gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        ASPDGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    }
        //    if (i < HeroRange)
        //    {
        //        RangeGage[i].transform.GetChild(0).gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        RangeGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    }
        //    if (i < HeroHP)
        //    {
        //        HPGage[i].transform.GetChild(0).gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        HPGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    }
        //}
    }
    //private void HeroGrade(List<GameObject> Hero, int HeroNum)
    //{
    //    for (int i = 0; i < Hero.Count; i++)
    //    {
    //        if (i < myChar.HeroLv[HeroNum])
    //        {
    //            Hero[i].transform.GetChild(0).gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            Hero[i].transform.GetChild(0).gameObject.SetActive(true);
    //        }
    //    }

    //}
    private void HeroUIManager()
    {
        //영웅 LV표시텍스트
        HeroViewport.transform.GetChild(myChar.SelectHero).GetChild(0).Find("Grade_Panel").GetChild(0).GetComponent<Text>().text = "LV " + (myChar.HeroLv[myChar.SelectHero] + 1).ToString();

        //영웅 공격/공속/시야/체력 표시 텍스트
        HeroStats_Panel.transform.GetChild(0).Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n");
        HeroStats_Panel.transform.GetChild(1).Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n");
        HeroStats_Panel.transform.GetChild(2).Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n");
        HeroStats_Panel.transform.GetChild(3).Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n");

        if (myChar.SelectHero == 0)
        {
            HeroNameWindow.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(185).Content[myChar.LanguageNum].Replace("\\n", "\n");

            HeroGage(HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KAtk,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KASPD,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KRange,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KHP);

            HeroInfo_Text.text = myChar.TextDataMgr.GetTemplate(186).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
        if (myChar.SelectHero == 1)
        {
            HeroNameWindow.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(187).Content[myChar.LanguageNum].Replace("\\n", "\n");

            HeroGage(HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WAtk,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WASPD,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WRange,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WHP);

            HeroInfo_Text.text = myChar.TextDataMgr.GetTemplate(188).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
        if (myChar.SelectHero == 2)
        {
            HeroNameWindow.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(189).Content[myChar.LanguageNum].Replace("\\n", "\n");


            HeroGage(HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).AAtk,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).AASPD,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).ARange,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).AHP);
            HeroInfo_Text.text = myChar.TextDataMgr.GetTemplate(190).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
        if (myChar.SelectHero == 3)
        {
            HeroNameWindow.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(191).Content[myChar.LanguageNum].Replace("\\n", "\n");


            HeroGage(HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WiAtk,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WiASPD,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WiRange,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WiHP);
            HeroInfo_Text.text = myChar.TextDataMgr.GetTemplate(192).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
        if (myChar.SelectHero == 4)
        {
            HeroNameWindow.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(193).Content[myChar.LanguageNum].Replace("\\n", "\n");

            HeroGage(HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).NAtk,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).NASPD,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).NRange,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).NHP);
            HeroInfo_Text.text = myChar.TextDataMgr.GetTemplate(194).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
    }

    private void ThroneItemPanelCheck()
    {
        for (int i = 0; i < myChar.InventoryItemNum.Length; i++)
        {
            if (i < myChar.InventoryItemNum.Length - myChar.SlotPackCnt)
            {
                if (myChar.InventoryItemNum[i] > -1)
                {
                    ThroneItemPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().enabled = true;
                    ThroneItemPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.InventoryItemNum[i]);
                    ThroneItemPanel.transform.GetChild(i).GetChild(2).GetComponent<Image>().enabled = false;
                }
                else
                {
                    ThroneItemPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().enabled = false;
                    ThroneItemPanel.transform.GetChild(i).GetChild(2).GetComponent<Image>().enabled = false;
                }
            }
            else
            {
                ThroneItemPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().enabled = false;
                ThroneItemPanel.transform.GetChild(i).GetChild(2).GetComponent<Image>().enabled = true;
            }
            
        }
        if (SelectInventoryArrayNum != -1)
        {
            for (int i = 0; i < ThroneItemPanel.transform.childCount; i++)
            {
                if (i == SelectInventoryArrayNum)
                {
                    ThroneItemPanel.transform.GetChild(i).Find("Select_Img").gameObject.SetActive(true);
                }
                else
                {
                    ThroneItemPanel.transform.GetChild(i).Find("Select_Img").gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < ThroneItemPanel.transform.childCount; i++)
            {
                ThroneItemPanel.transform.GetChild(i).Find("Select_Img").gameObject.SetActive(false);
            }
        }

    }
    //private void SlotItem_Img_Check()
    //{
    //    if (!SlotPanel.activeSelf && !SkinInfoPanel.activeSelf && !CashShopPanel.activeSelf && !BlackSmithPanel.activeSelf)
    //    {
    //        if (myChar.SlotActiveCheck)
    //        {
    //            for (int i = 0; i < myChar.LobbyMachineEquipmentNum.Length; i++)
    //            {
    //                if (myChar.LobbyMachineEquipmentNum[i] > -1)
    //                {
    //                    switch (i)
    //                    {
    //                        case 0:
    //                            SlotItem[i].enabled = true;
    //                            SlotItem[i].sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[i]]);
    //                            break;
    //                        case 1:
    //                            SlotItem[i].enabled = true;
    //                            SlotItem[i].sprite = Resources.Load<Sprite>("02_Equipment/Item/" + (myChar.LobbyMachineEquipmentNum[i] + 1999));
    //                            break;
    //                        case 2:
    //                            SlotItem[i].enabled = true;
    //                            SlotItem[i].sprite = Resources.Load<Sprite>("02_Equipment/Item/" + (myChar.LobbyMachineEquipmentNum[i] + 999));
    //                            break;
    //                    }
    //                }
    //                else
    //                {
    //                    SlotItem[i].enabled = false;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            for (int i = 0; i < myChar.LobbyMachineEquipmentNum.Length; i++)
    //            {
    //                SlotItem[i].enabled = false;
    //            }
    //        }
    //    }
    //}

    //private void ThroneGage()
    //{
    //    EnthroneHero_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Atk").Find("LV_Text").GetComponent<Text>().text = " LV." + myChar.ThroneDamage.ToString("F0");
    //    EnthroneHero_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ASPD").Find("LV_Text").GetComponent<Text>().text = " LV." + myChar.ThroneASPD.ToString("F0");
    //    EnthroneHero_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Range").Find("LV_Text").GetComponent<Text>().text = " LV." + myChar.ThroneRange.ToString("F0");
    //    EnthroneHero_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("HP").Find("LV_Text").GetComponent<Text>().text = " LV." + myChar.ThronemaxHp.ToString("F0");
    //    myChar.ThroneCurrentDamage = myChar.ThroneDamage;

    //    for (int i = 0; i < 30; i++)
    //    {
    //        if (i < myChar.ThroneDamage)
    //        {
    //            ThroneAtkGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            ThroneAtkGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //        }

    //        if (i < myChar.ThroneASPD)
    //        {
    //            ThroneASPDGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            ThroneASPDGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //        }
    //        if (i < myChar.ThroneRange)
    //        {
    //            ThroneRangeGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            ThroneRangeGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //        }
    //        if (i < myChar.ThronemaxHp)
    //        {
    //            ThroneHPGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            ThroneHPGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //        }
    //    }
    //}
    //private void ThroneUIManager()
    //{
    //    if (myChar.SelectHero != myChar.EnthroneHeroNum)
    //    {
    //        EnthroneHero_Panel.SetActive(false);
    //    }
    //    else
    //    {
    //        if (SkinInfoPanel.activeSelf)
    //        {
    //            EnthroneHero_Panel.SetActive(false);
    //        }
    //        else
    //        {
    //            EnthroneHero_Panel.SetActive(true);
    //            EnthroneHero_UP_Btn.transform.GetChild(0).gameObject.SetActive(false);
    //            EnthroneHero_UP_Btn.transform.GetChild(1).gameObject.SetActive(false);
    //            EnthroneHero_UP_Btn.transform.GetChild(2).gameObject.SetActive(false);
    //            EnthroneHero_UP_Btn.transform.GetChild(3).gameObject.SetActive(false);
    //            if (myChar.SelectHero == 0)
    //            {
    //                EnthroneHero_Panel.transform.GetChild(0).Find("CharacterName").Find("ThroneName_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(185).Content[myChar.LanguageNum].Replace("\\n", "\n");

    //                ThroneGage();

    //                EnthroneHero_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ThroneInfo_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(186).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //            }
    //            if (myChar.SelectHero == 1)
    //            {
    //                EnthroneHero_Panel.transform.GetChild(0).Find("CharacterName").Find("ThroneName_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(187).Content[myChar.LanguageNum].Replace("\\n", "\n");

    //                ThroneGage();

    //                EnthroneHero_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ThroneInfo_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(188).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //            }
    //            if (myChar.SelectHero == 2)
    //            {
    //                EnthroneHero_Panel.transform.GetChild(0).Find("CharacterName").Find("ThroneName_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(189).Content[myChar.LanguageNum].Replace("\\n", "\n");

    //                ThroneGage();

    //                EnthroneHero_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ThroneInfo_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(190).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //            }
    //            if (myChar.SelectHero == 3)
    //            {
    //                EnthroneHero_Panel.transform.GetChild(0).Find("CharacterName").Find("ThroneName_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(191).Content[myChar.LanguageNum].Replace("\\n", "\n");

    //                ThroneGage();

    //                EnthroneHero_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ThroneInfo_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(192).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //            }
    //            if (myChar.SelectHero == 4)
    //            {
    //                EnthroneHero_Panel.transform.GetChild(0).Find("CharacterName").Find("ThroneName_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(193).Content[myChar.LanguageNum].Replace("\\n", "\n");

    //                ThroneGage();

    //                EnthroneHero_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ThroneInfo_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(194).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //            }


    //            if (myChar.ThroneItemList.Count >= 0)
    //            {
    //                for (int i = 0; i < myChar.ThroneItemList.Count; i++)
    //                {
    //                    EnthroneHero_Panel.transform.Find("ThroneItem_Panel").GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.ThroneItemList[i]);
    //                    EnthroneHero_Panel.transform.Find("ThroneItem_Panel").GetChild(i).GetChild(0).GetComponent<Image>().enabled = true;
    //                    //EnthroneHero_Panel.transform.Find("ThroneItem_Panel").GetChild(i).GetComponent<Button>().enabled = true;

    //                }
    //                for (int i = myChar.ThroneItemList.Count; i < 4; i++)
    //                {
    //                    EnthroneHero_Panel.transform.Find("ThroneItem_Panel").GetChild(i).GetChild(0).GetComponent<Image>().enabled = false;
    //                    //EnthroneHero_Panel.transform.Find("ThroneItem_Panel").GetChild(i).GetComponent<Button>().enabled = false;
    //                }
    //            }
    //            else
    //            {
    //                for (int i = 0; i < 4; i++)
    //                {
    //                    EnthroneHero_Panel.transform.Find("ThroneItem_Panel").GetChild(i).GetChild(0).GetComponent<Image>().enabled = false;
    //                    //EnthroneHero_Panel.transform.Find("ThroneItem_Panel").GetChild(i).GetComponent<Button>().enabled = false;
    //                }
    //            }
    //        }

    //    }
    //}

    //public void PlusHeroBtn()
    //{
    //    if (myChar.SelectHero != 4)
    //    {
    //        myChar.SelectHero++;
    //    }
    //    else if (myChar.SelectHero == 4)
    //    {
    //        myChar.SelectHero = 0;
    //    }
    //}
    //public void MinusHeroBtn()
    //{
    //    if (myChar.SelectHero != 0)
    //    {
    //        myChar.SelectHero--;
    //    }
    //    else if (myChar.SelectHero == 0)
    //    {
    //        myChar.SelectHero = 4;
    //    }
    //}

    private void TextCheck()
    {
        // ContentInfoPanelCheck
        if (ContentInfoPanel.activeSelf)
        {
            Text Info_Text = ContentInfoPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();

            Info_Text.text = "\n" + myChar.TextDataMgr.GetTemplate(ContentInfoNum).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
        if (AttendanceWindow.activeSelf)
        {
            AttendanceWindow.transform.GetChild(0).Find("TitleBoard").Find("Title").Find("Title_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(9200).Content[myChar.LanguageNum].Replace("\\n", "\n");
            AttendanceWindow.transform.GetChild(0).Find("DayBoard").GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(9201).Content[myChar.LanguageNum].Replace("\\n", "\n") + myChar.TotalAttendanceDay;
            AttendanceWindow.transform.GetChild(0).Find("Help_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(9202).Content[myChar.LanguageNum].Replace("\\n", "\n");
            for (int i = 0; i < 30; i++)
            {
                AttendanceWindow.transform.GetChild(0).Find("DiaBoard").GetChild(i).Find("Day").GetComponentInChildren<Text>().text = AttendanceDataMgr.GetTemplate(i + 1).Day;
                AttendanceWindow.transform.GetChild(0).Find("DiaBoard").GetChild(i).Find("Text").GetComponent<Text>().text = AttendanceDataMgr.GetTemplate(i + 1).Dia.ToString();
            }
        }
        if (PreferencesWindow.activeSelf)
        {
            PreferencesWindow.transform.Find("BackGround").GetChild(0).Find("HeroCoin_Preferences").GetComponentInChildren<Text>().text = myChar.HeroHeart.ToString();
            PreferencesWindow.transform.Find("BackGround").GetChild(0).Find("Soul_Preferences").GetComponentInChildren<Text>().text = myChar.SoulSpark.ToString();
            PreferencesWindow.transform.Find("BackGround").GetChild(0).Find("Gem_Preferences").GetComponentInChildren<Text>().text = myChar.Gem.ToString();
            Ver_Text.text = myChar.Throne_Version;
        }
        if (UnpurchasedInfo_Panel.activeSelf)
        {
            Text Info_Text = UnpurchasedInfo_Panel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            Info_Text.text = myChar.TextDataMgr.GetTemplate(201).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
        if (EnthroneHeroInfo_Panel.activeSelf)
        {
            Text Info_Text = EnthroneHeroInfo_Panel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            Info_Text.text = myChar.TextDataMgr.GetTemplate(284).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
        if (BlackSmithInfoPanel.activeSelf)
        {
            Text Info_Text = BlackSmithInfoPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();

            Info_Text.text = "\n" + myChar.TextDataMgr.GetTemplate(400).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
    }

    private void BlacksmithPanelCheck()
    {
        if (BlackSmithPanel.activeSelf) //Blacksmith창 열려있을때
        {
            if (myChar.FirstBlackSmithCheck)    //처음 창들어왔을때 Info창 띄워주기용
            {
                BlackSmithInfoPanel.SetActive(true);
            }
            ForgeClose_Btn.SetActive(false);
            SelectBar.transform.Find("ForgeOn_Btn").gameObject.SetActive(true);

            if (SelectForgeNum == -1)   //SelectForgeNum : 아이템 선택유무
            {
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").gameObject.SetActive(false);
                //장비창 보여주는 부분
                if (ForgeWindowNum == 1)
                {
                    BlackSmithPanel.transform.Find("Equipment(Avtive)_Btn").gameObject.SetActive(false);
                    BlackSmithPanel.transform.Find("Item(Avtive)_Btn").gameObject.SetActive(true);
                    //BlackSmithPanel.transform.GetChild(1).Find("Equipment(Avtive)_Btn").gameObject.SetActive(true);

                    if (!EquipmentCheck)
                    {
                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                        EquipmentSortNum.Clear();
                        if (Nomal > 0)
                        {
                            Nomal = 0;
                            Rare = 0;
                            Unique = 0;
                        }
                        for (int i = 0; i < myChar.EquipmentAll.Length; i++)
                        {
                            switch (myChar.itemDataMgr.GetTemplate(i).Grade)
                            {
                                case 0:
                                    Nomal++;
                                    break;
                                case 1:
                                    Rare++;
                                    break;
                                case 2:
                                    Unique++;
                                    break;
                            }
                            EquipmentSortNum.Add(i);
                        }
                        for (int i = 0; i < myChar.EquipmentAll.Length; i++)
                        {
                            if (myChar.EquipmentAll[i] > -1)
                            {
                                int Cnt;
                                Cnt = EquipmentSortNum[i];                 //Cnt에 현재 i배열값을넣어두고
                                EquipmentSortNum.Remove(EquipmentSortNum[i]);     //해당 부분을 지움(Cnt를 안넣어두면 제거된 숫자를 찾을 수 없음)
                                EquipmentSortNum.Insert(0, Cnt);           //그리고 다시 배열 0번째에 Cnt값을 넣어서 최상단으로 배치해줌
                                                                           //Debug.Log("RemoveAt" + EquipmentSortNum[i + 1]);
                            }
                            if (i == myChar.EquipmentAll.Length - 1)
                            {
                                EquipmentCheck = true;
                            }
                        }
                    }
                    if (!EquipmentArrangement)
                    {
                        if (BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").childCount <= myChar.EquipmentAll.Length)
                        {
                            for (int i = 0; i < myChar.EquipmentAll.Length; i++)
                            {
                                if (i == myChar.EquipmentAll.Length - 1)
                                {
                                    InventoryArrangement = true;
                                }
                            }
                            for (int i = 0; i < myChar.EquipmentAll.Length; i++)
                            {
                                if (BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").childCount > i)
                                {
                                    BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).gameObject.SetActive(true);
                                    BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<ForgeItemNumScript>().ItemIndex = EquipmentSortNum[i];
                                    BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + EquipmentSortNum[i]);

                                    switch (myChar.itemDataMgr.GetTemplate(EquipmentSortNum[i]).Grade)
                                    {
                                        case 0:
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 0);
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[EquipmentSortNum[i]] + 1) + "</color>";
                                            break;
                                        case 1:
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 1);
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[EquipmentSortNum[i]] + 1) + "</color>";
                                            break;
                                        case 2:
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 2);
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[EquipmentSortNum[i]] + 1) + "</color>";
                                            break;
                                    }

                                    if (myChar.EquipmentAll[EquipmentSortNum[i]] != -1)
                                    {
                                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(false);
                                    }
                                    else
                                    {
                                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(true);
                                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "-";
                                    }
                                }
                                else
                                {
                                    GameObject ForgeUI = Instantiate(ForgeBtn_UI, transform.position, Quaternion.identity);
                                    ForgeUI.transform.parent = BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").transform;
                                    ForgeUI.transform.localScale = new Vector3(1, 1, 1);
                                    ForgeUI.GetComponent<ForgeItemNumScript>().ItemIndex = EquipmentSortNum[i];
                                    ForgeUI.transform.Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + EquipmentSortNum[i]);

                                    switch (myChar.itemDataMgr.GetTemplate(EquipmentSortNum[i]).Grade)
                                    {
                                        case 0:
                                            ForgeUI.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 0);
                                            ForgeUI.transform.Find("Lv_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[EquipmentSortNum[i]] + 1) + "</color>";
                                            break;
                                        case 1:
                                            ForgeUI.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 1);
                                            ForgeUI.transform.Find("Lv_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[EquipmentSortNum[i]] + 1) + "</color>";
                                            break;
                                        case 2:
                                            ForgeUI.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 2);
                                            ForgeUI.transform.Find("Lv_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[EquipmentSortNum[i]] + 1) + "</color>";
                                            break;
                                    }
                                    if (myChar.EquipmentAll[EquipmentSortNum[i]] != -1)
                                    {
                                        ForgeUI.transform.Find("DisablePanel").gameObject.SetActive(false);
                                    }
                                    else
                                    {
                                        ForgeUI.transform.Find("DisablePanel").gameObject.SetActive(true);
                                        ForgeUI.transform.Find("Lv_Text").GetComponent<Text>().text = "-";
                                    }
                                }

                                if (i == myChar.EquipmentAll.Length - 1)
                                {
                                    EquipmentArrangement = true;
                                }
                            }
                        }
                        else if (BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").childCount > myChar.EquipmentAll.Length)
                        {
                            for (int i = 0; i < BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").childCount; i++)
                            {
                                if (i < myChar.EquipmentAll.Length)
                                {
                                    BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).gameObject.SetActive(true);
                                    BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<ForgeItemNumScript>().ItemIndex = EquipmentSortNum[i];
                                    BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + EquipmentSortNum[i]);

                                    switch (myChar.itemDataMgr.GetTemplate(EquipmentSortNum[i]).Grade)
                                    {
                                        case 0:
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 0);
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[EquipmentSortNum[i]] + 1) + "</color>";
                                            break;
                                        case 1:
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 1);
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[EquipmentSortNum[i]] + 1) + "</color>";
                                            break;
                                        case 2:
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 2);
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[EquipmentSortNum[i]] + 1) + "</color>";
                                            break;
                                    }
                                    if (myChar.EquipmentAll[EquipmentSortNum[i]] != -1)
                                    {
                                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(false);
                                    }
                                    else
                                    {
                                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(true);
                                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "-";
                                    }
                                }
                                else
                                {
                                    BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).gameObject.SetActive(false);
                                }

                                if (i == BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").childCount - 1)
                                {
                                    EquipmentArrangement = true;
                                }
                            }
                        }
                    }
                }
                else if (ForgeWindowNum == 2)
                {
                    BlackSmithPanel.transform.Find("Equipment(Avtive)_Btn").gameObject.SetActive(true);
                    BlackSmithPanel.transform.Find("Item(Avtive)_Btn").gameObject.SetActive(false);
                    if (!EquipmentCheck)
                    {
                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                        EquipmentSortNum.Clear();
                        if (Nomal > 0)
                        {
                            Nomal = 0;
                            Rare = 0;
                            Unique = 0;
                        }
                        for (int i = 0; i < myChar.ElementStoneAll.Length + myChar.ActiveitemAll.Length - 1; i++)
                        {
                            if (i < myChar.ElementStoneAll.Length)
                            {
                                switch (myChar.AntiqueitemDataMgr.GetTemplate(i + 1000).Grade)
                                {
                                    case 0:
                                        Nomal++;
                                        break;
                                    case 1:
                                        Rare++;
                                        break;
                                    case 2:
                                        Unique++;
                                        break;
                                }
                                EquipmentSortNum.Add(i + 1000);
                            }
                            else
                            {
                                switch (myChar.AntiqueitemDataMgr.GetTemplate(i + 1996).Grade)
                                {
                                    case 0:
                                        Nomal++;
                                        break;
                                    case 1:
                                        Rare++;
                                        break;
                                    case 2:
                                        Unique++;
                                        break;
                                }
                                EquipmentSortNum.Add(i + 1996);
                            }

                        }
                        for (int i = 0; i < myChar.ElementStoneAll.Length + myChar.ActiveitemAll.Length - 1; i++)
                        {
                            if (i < myChar.ElementStoneAll.Length)
                            {
                                if (myChar.ElementStoneAll[i] > -1)
                                {
                                    int Cnt;
                                    Cnt = EquipmentSortNum[i];                 //Cnt에 현재 i배열값을넣어두고
                                    EquipmentSortNum.Remove(EquipmentSortNum[i]);     //해당 부분을 지움(Cnt를 안넣어두면 제거된 숫자를 찾을 수 없음)
                                    EquipmentSortNum.Insert(0, Cnt);           //그리고 다시 배열 0번째에 Cnt값을 넣어서 최상단으로 배치해줌
                                }
                            }
                            else
                            {
                                if (myChar.ActiveitemAll[i - 4] > -1)
                                {
                                    int Cnt;
                                    Cnt = EquipmentSortNum[i];                 //Cnt에 현재 i배열값을넣어두고
                                    EquipmentSortNum.Remove(EquipmentSortNum[i]);     //해당 부분을 지움(Cnt를 안넣어두면 제거된 숫자를 찾을 수 없음)
                                    EquipmentSortNum.Insert(0, Cnt);           //그리고 다시 배열 0번째에 Cnt값을 넣어서 최상단으로 배치해줌
                                }
                            }

                            if (i == (myChar.ElementStoneAll.Length + myChar.ActiveitemAll.Length) - 2)
                            {
                                EquipmentCheck = true;
                            }
                        }
                    }

                    if (!EquipmentArrangement)
                    {
                        for (int i = 0; i < BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").childCount; i++)
                        {
                            if (i < myChar.ElementStoneAll.Length + myChar.ActiveitemAll.Length - 1)
                            {
                                BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).gameObject.SetActive(true);
                                BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<ForgeItemNumScript>().ItemIndex = EquipmentSortNum[i];
                                BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + EquipmentSortNum[i]);

                                //switch (myChar.AntiqueitemDataMgr.GetTemplate(EquipmentSortNum[i]).Grade)
                                //{
                                //    case 0:
                                //        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 0);
                                //        break;
                                //    case 1:
                                //        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 1);
                                //        break;
                                //    case 2:
                                //        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 2);
                                //        break;
                                //}

                                if (EquipmentSortNum[i] >= 1000 && EquipmentSortNum[i] < 2000)
                                {
                                    if (myChar.ElementStone[EquipmentSortNum[i] - 1000] != -1)
                                    {
                                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(false);
                                    }
                                    else
                                    {
                                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(true);
                                    }
                                    switch (myChar.AntiqueitemDataMgr.GetTemplate(EquipmentSortNum[i]).Grade)
                                    {
                                        case 0:
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 0);
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.ElementStoneAll[EquipmentSortNum[i] - 1000] + 1) + "</color>";
                                            break;
                                        case 1:
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 1);
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.ElementStoneAll[EquipmentSortNum[i] - 1000] + 1) + "</color>";
                                            break;
                                        case 2:
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 2);
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.ElementStoneAll[EquipmentSortNum[i] - 1000] + 1) + "</color>";
                                            break;
                                    }
                                }
                                else
                                {
                                    if (myChar.ActiveitemAll[EquipmentSortNum[i] - 2000] != -1)
                                    {
                                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(false);
                                    }
                                    else
                                    {
                                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(true);
                                    }
                                    switch (myChar.AntiqueitemDataMgr.GetTemplate(EquipmentSortNum[i]).Grade)
                                    {
                                        case 0:
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 0);
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.ActiveitemAll[EquipmentSortNum[i] - 2000] + 1) + "</color>";
                                            break;
                                        case 1:
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 1);
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.ActiveitemAll[EquipmentSortNum[i] - 2000] + 1) + "</color>";
                                            break;
                                        case 2:
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 2);
                                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.ActiveitemAll[EquipmentSortNum[i] - 2000] + 1) + "</color>";
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).gameObject.SetActive(false);
                            }

                            if (i == BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").childCount - 1)
                            {
                                EquipmentArrangement = true;
                            }
                        }
                    }
                }

                //장비제작 부분-------------
                if (myChar.CraftingItemNum == -1)   //제작 아이템 선별 유무
                {
                    BlackSmithitemlist.Clear();
                    int ItemTypeSelect = Random.Range(0, 2);

                    int GradeSelect = Random.Range(0, 100);

                    if (ItemTypeSelect == 0)
                    {
                        if (GradeSelect < BlackSmithDataMgr.GetTemplate(0).Nper)
                        {
                            for (int i = 0; i < myChar.itemDataMgr.GetCount(); i++)
                            {
                                if (myChar.itemDataMgr.GetTemplate(i).Grade == 0)
                                {
                                    BlackSmithitemlist.Add(i);
                                }
                            }
                        }
                        else if (GradeSelect >= BlackSmithDataMgr.GetTemplate(0).Nper && GradeSelect < (BlackSmithDataMgr.GetTemplate(0).Nper + BlackSmithDataMgr.GetTemplate(0).Rper))
                        {
                            for (int i = 0; i < myChar.itemDataMgr.GetCount(); i++)
                            {
                                if (myChar.itemDataMgr.GetTemplate(i).Grade == 1)
                                {
                                    BlackSmithitemlist.Add(i);
                                }
                            }
                        }
                        else if ((GradeSelect >= BlackSmithDataMgr.GetTemplate(0).Nper + BlackSmithDataMgr.GetTemplate(0).Rper) && GradeSelect <= (BlackSmithDataMgr.GetTemplate(0).Nper + BlackSmithDataMgr.GetTemplate(0).Rper + BlackSmithDataMgr.GetTemplate(0).Uper))
                        {
                            for (int i = 0; i < myChar.itemDataMgr.GetCount(); i++)
                            {
                                if (myChar.itemDataMgr.GetTemplate(i).Grade == 2)
                                {
                                    BlackSmithitemlist.Add(i);
                                }
                            }
                        }
                    }
                    else if (ItemTypeSelect == 1)
                    {
                        if (GradeSelect < BlackSmithDataMgr.GetTemplate(101).Nper)
                        {

                            for (int i = 0; i < myChar.AntiqueitemDataMgr.GetCount(); i++)
                            {
                                if (i <= 3)
                                {
                                    ActiveItemIndex = i + 1000;
                                }
                                else
                                {
                                    ActiveItemIndex = i + 1996;
                                }

                                if (myChar.AntiqueitemDataMgr.GetTemplate(ActiveItemIndex).Grade == 0)
                                {
                                    BlackSmithitemlist.Add(ActiveItemIndex);
                                }
                            }
                        }
                        else if (GradeSelect >= BlackSmithDataMgr.GetTemplate(101).Nper && GradeSelect < (BlackSmithDataMgr.GetTemplate(101).Nper + BlackSmithDataMgr.GetTemplate(101).Rper))
                        {
                            for (int i = 0; i < myChar.AntiqueitemDataMgr.GetCount(); i++)
                            {
                                if (i <= 3)
                                {
                                    ActiveItemIndex = i + 1000;
                                }
                                else
                                {
                                    ActiveItemIndex = i + 1996;
                                }
                                if (myChar.AntiqueitemDataMgr.GetTemplate(ActiveItemIndex).Grade == 1)
                                {
                                    BlackSmithitemlist.Add(ActiveItemIndex);
                                }
                            }
                        }
                        else if (GradeSelect >= (BlackSmithDataMgr.GetTemplate(101).Nper + BlackSmithDataMgr.GetTemplate(101).Rper) && GradeSelect <= (BlackSmithDataMgr.GetTemplate(101).Nper + BlackSmithDataMgr.GetTemplate(101).Rper + BlackSmithDataMgr.GetTemplate(101).Uper))
                        {
                            for (int i = 0; i < myChar.AntiqueitemDataMgr.GetCount(); i++)
                            {
                                if (i <= 3)
                                {
                                    ActiveItemIndex = i + 1000;
                                }
                                else
                                {
                                    ActiveItemIndex = i + 1996;
                                }
                                if (myChar.AntiqueitemDataMgr.GetTemplate(ActiveItemIndex).Grade == 2)
                                {
                                    BlackSmithitemlist.Add(ActiveItemIndex);
                                }
                            }
                        }
                    }
                    int ItemCheck = Random.Range(0, BlackSmithitemlist.Count);
                    myChar.CraftingItemNum = BlackSmithitemlist[ItemCheck];
                    myChar.SaveCraftingItemNum();
                    if (myChar.CraftingItemNum < 1000)
                    {
                        BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.CraftingItemNum);
                    }
                    else
                    {
                        BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + myChar.CraftingItemNum);
                    }
                }
                else
                {
                    if (!myChar.Crafting)       //장비제작 못들어감
                    {
                        BlackSmithPanel.transform.Find("Top_Panel").Find("Tanning").GetComponent<Animator>().enabled = false;
                        BlackSmithPanel.transform.Find("Top_Panel").Find("Tanning").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/06_BlackSmith/Tanning");
                        BlackSmithPanel.transform.Find("Top_Panel").Find("Tanning").GetChild(0).GetComponentInChildren<Text>().text = "00:00:00";
                        BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Slider").GetComponent<Slider>().value = 0f;
                        BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Brazier_Text").GetComponent<Text>().text = "";
                        BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Fire").GetComponent<Animator>().enabled = false;
                        //BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Equipment").GetComponent<Image>().enabled = false;

                        BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("Timer_Text").GetComponent<Text>().text = "";//"–" + (BlackSmithDataMgr.GetTemplate(0).ADSkip / 60).ToString("D2") + ":" + (BlackSmithDataMgr.GetTemplate(0).ADSkip % 60).ToString("D2");
                        BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("DontTimeAD_Btn").gameObject.SetActive(true);
                        BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("TimeAD_Btn").gameObject.SetActive(false);
                        BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("TimeGem_Btn").gameObject.SetActive(false);
                        BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("DontTimeGem_Btn").gameObject.SetActive(false);
                        myChar.ADreductionCheck = false;

                        myChar.SaveADreductionCheck();




                        if (myChar.CraftingItemNum < 1000)
                        {
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.CraftingItemNum);
                            switch (myChar.itemDataMgr.GetTemplate(myChar.CraftingItemNum).Grade)
                            {
                                case 0:
                                    if (myChar.SoulSpark >= BlackSmithDataMgr.GetTemplate(0).Nsoul)
                                    {
                                        myChar.StartCraftingTime = UnbiasedTime.Instance.Now();
                                        myChar.CraftingTime = BlackSmithDataMgr.GetTemplate(0).Ntime;
                                        myChar.SoulSpark -= BlackSmithDataMgr.GetTemplate(0).Nsoul;
                                        myChar.Crafting = true;
                                    }
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("SoulSpark_UI").Find("BlackSmithSoul_Text").GetComponent<Text>().text = "<color=#FFFFFF>(" + myChar.SoulSpark + "/" + BlackSmithDataMgr.GetTemplate(0).Nsoul + ")</color>";
                                    break;
                                case 1:
                                    if (myChar.SoulSpark >= BlackSmithDataMgr.GetTemplate(0).Rsoul)
                                    {
                                        myChar.StartCraftingTime = UnbiasedTime.Instance.Now();
                                        myChar.CraftingTime = BlackSmithDataMgr.GetTemplate(0).Rtime;
                                        myChar.SoulSpark -= BlackSmithDataMgr.GetTemplate(0).Rsoul;
                                        myChar.Crafting = true;
                                    }
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("SoulSpark_UI").Find("BlackSmithSoul_Text").GetComponent<Text>().text = "<color=#FFFFFF>(" + myChar.SoulSpark + "/" + BlackSmithDataMgr.GetTemplate(0).Rsoul + ")</color>";
                                    break;
                                case 2:
                                    if (myChar.SoulSpark >= BlackSmithDataMgr.GetTemplate(0).Usoul)
                                    {
                                        myChar.StartCraftingTime = UnbiasedTime.Instance.Now();
                                        myChar.CraftingTime = BlackSmithDataMgr.GetTemplate(0).Utime;
                                        myChar.SoulSpark -= BlackSmithDataMgr.GetTemplate(0).Usoul;
                                        myChar.Crafting = true;
                                    }
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("SoulSpark_UI").Find("BlackSmithSoul_Text").GetComponent<Text>().text = "<color=#FFFFFF>(" + myChar.SoulSpark + "/" + BlackSmithDataMgr.GetTemplate(0).Usoul + ")</color>";
                                    break;
                            }
                            myChar.SaveCrafting();
                            myChar.SaveCraftingTime();
                            myChar.SaveStartCraftingTime();
                            myChar.SaveSoulSpark();
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + myChar.CraftingItemNum);
                            switch (myChar.AntiqueitemDataMgr.GetTemplate(myChar.CraftingItemNum).Grade)
                            {
                                case 0:
                                    if (myChar.SoulSpark >= BlackSmithDataMgr.GetTemplate(101).Nsoul)
                                    {
                                        myChar.StartCraftingTime = UnbiasedTime.Instance.Now();
                                        myChar.CraftingTime = BlackSmithDataMgr.GetTemplate(101).Ntime;
                                        myChar.SoulSpark -= BlackSmithDataMgr.GetTemplate(101).Nsoul;
                                        myChar.Crafting = true;
                                    }
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("SoulSpark_UI").Find("BlackSmithSoul_Text").GetComponent<Text>().text = "<color=#FFFFFF>(" + myChar.SoulSpark + "/" + BlackSmithDataMgr.GetTemplate(101).Nsoul + ")</color>";
                                    break;
                                case 1:
                                    if (myChar.SoulSpark >= BlackSmithDataMgr.GetTemplate(101).Rsoul)
                                    {
                                        myChar.StartCraftingTime = UnbiasedTime.Instance.Now();
                                        myChar.CraftingTime = BlackSmithDataMgr.GetTemplate(101).Rtime;
                                        myChar.SoulSpark -= BlackSmithDataMgr.GetTemplate(101).Rsoul;
                                        myChar.Crafting = true;
                                    }
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("SoulSpark_UI").Find("BlackSmithSoul_Text").GetComponent<Text>().text = "<color=#FFFFFF>(" + myChar.SoulSpark + "/" + BlackSmithDataMgr.GetTemplate(101).Rsoul + ")</color>";
                                    break;
                                case 2:
                                    if (myChar.SoulSpark >= BlackSmithDataMgr.GetTemplate(101).Usoul)
                                    {
                                        myChar.StartCraftingTime = UnbiasedTime.Instance.Now();
                                        myChar.CraftingTime = BlackSmithDataMgr.GetTemplate(101).Utime;
                                        myChar.SoulSpark -= BlackSmithDataMgr.GetTemplate(101).Usoul;
                                        myChar.Crafting = true;
                                    }
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("SoulSpark_UI").Find("BlackSmithSoul_Text").GetComponent<Text>().text = "<color=#FFFFFF>(" + myChar.SoulSpark + "/" + BlackSmithDataMgr.GetTemplate(101).Usoul + ")</color>";
                                    break;
                            }
                            myChar.SaveCrafting();
                            myChar.SaveCraftingTime();
                            myChar.SaveStartCraftingTime();
                            myChar.SaveSoulSpark();
                        }
                    }
                    else        //장비제작들어감
                    {
                        int a_hour = (int)((myChar.CraftingTime - myChar.UntilCompleteTime) / 3600);
                        int a_min = (int)((myChar.CraftingTime - myChar.UntilCompleteTime) % 3600 / 60);
                        int a_sec = (int)((myChar.CraftingTime - myChar.UntilCompleteTime) % 3600 % 60);

                        if (myChar.CraftingItemNum < 1000)
                        {
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.CraftingItemNum);
                            switch (myChar.itemDataMgr.GetTemplate(myChar.CraftingItemNum).Grade)
                            {
                                case 0:
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("SoulSpark_UI").Find("BlackSmithSoul_Text").GetComponent<Text>().text = "<color=#00FF00>(" + BlackSmithDataMgr.GetTemplate(0).Nsoul + "/" + BlackSmithDataMgr.GetTemplate(0).Nsoul + ")</color>";
                                    break;
                                case 1:
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("SoulSpark_UI").Find("BlackSmithSoul_Text").GetComponent<Text>().text = "<color=#00FF00>(" + BlackSmithDataMgr.GetTemplate(0).Rsoul + "/" + BlackSmithDataMgr.GetTemplate(0).Rsoul + ")</color>";
                                    break;
                                case 2:
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("SoulSpark_UI").Find("BlackSmithSoul_Text").GetComponent<Text>().text = "<color=#00FF00>(" + BlackSmithDataMgr.GetTemplate(0).Usoul + "/" + BlackSmithDataMgr.GetTemplate(0).Usoul + ")</color>";
                                    break;
                            }
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + myChar.CraftingItemNum);
                            switch (myChar.AntiqueitemDataMgr.GetTemplate(myChar.CraftingItemNum).Grade)
                            {
                                case 0:
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("SoulSpark_UI").Find("BlackSmithSoul_Text").GetComponent<Text>().text = "<color=#00FF00>(" + BlackSmithDataMgr.GetTemplate(101).Nsoul + "/" + BlackSmithDataMgr.GetTemplate(101).Nsoul + ")</color>";
                                    break;
                                case 1:
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("SoulSpark_UI").Find("BlackSmithSoul_Text").GetComponent<Text>().text = "<color=#00FF00>(" + BlackSmithDataMgr.GetTemplate(101).Rsoul + "/" + BlackSmithDataMgr.GetTemplate(101).Rsoul + ")</color>";
                                    break;
                                case 2:
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("SoulSpark_UI").Find("BlackSmithSoul_Text").GetComponent<Text>().text = "<color=#00FF00>(" + BlackSmithDataMgr.GetTemplate(101).Usoul + "/" + BlackSmithDataMgr.GetTemplate(101).Usoul + ")</color>";
                                    break;
                            }
                        }

                        if (!myChar.CompleteCheck)      //장비 제작중
                        {
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Tanning").GetComponent<Animator>().enabled = true;
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Tanning").GetChild(0).GetComponentInChildren<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", a_hour, a_min, a_sec);
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Fire").GetComponent<Animator>().enabled = true;
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Equipment").GetComponent<Image>().enabled = true;
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Slider").GetComponent<Slider>().value = (float)myChar.UntilCompleteTime / (float)myChar.CraftingTime;
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Brazier_Text").GetComponent<Text>().text = (((float)myChar.UntilCompleteTime / (float)myChar.CraftingTime) * 100).ToString("F0") + "%";

                            if (!myChar.ADreductionCheck)
                            {
                                if (myChar.CraftingItemNum < 1000)
                                {
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("Timer_Text").GetComponent<Text>().text = "–" + (BlackSmithDataMgr.GetTemplate(0).ADSkip / 60).ToString("D2") + ":" + (BlackSmithDataMgr.GetTemplate(0).ADSkip % 60).ToString("D2");
                                }
                                else
                                {
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("Timer_Text").GetComponent<Text>().text = "–" + (BlackSmithDataMgr.GetTemplate(101).ADSkip / 60).ToString("D2") + ":" + (BlackSmithDataMgr.GetTemplate(101).ADSkip % 60).ToString("D2");
                                }
                                
                                BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("TimeAD_Btn").gameObject.SetActive(true);
                                BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("DontTimeAD_Btn").gameObject.SetActive(false);
                                BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("TimeGem_Btn").gameObject.SetActive(false);
                                BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("DontTimeGem_Btn").gameObject.SetActive(false);
                            }
                            else
                            {
                                if (myChar.CraftingItemNum < 1000)
                                {
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("Timer_Text").GetComponent<Text>().text = "-" + (BlackSmithDataMgr.GetTemplate(0).CHSkip / 60).ToString("D2") + ":" + (BlackSmithDataMgr.GetTemplate(0).CHSkip % 60).ToString("D2");
                                }
                                else
                                {
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("Timer_Text").GetComponent<Text>().text = "-" + (BlackSmithDataMgr.GetTemplate(101).CHSkip / 60).ToString("D2") + ":" + (BlackSmithDataMgr.GetTemplate(101).CHSkip % 60).ToString("D2");
                                }
                                BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("Timer_Text").GetComponent<Text>().text = "-" + (BlackSmithDataMgr.GetTemplate(0).CHSkip / 60).ToString("D2") + ":" + (BlackSmithDataMgr.GetTemplate(0).CHSkip % 60).ToString("D2");
                                if (myChar.SoulSpark >= BlackSmithDataMgr.GetTemplate(0).CHCost)
                                {
                                    Blacksmith_TimeGem_Btn.GetComponentInChildren<Text>().text = BlackSmithDataMgr.GetTemplate(0).CHCost.ToString();
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("TimeGem_Btn").gameObject.SetActive(true);
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("DontTimeGem_Btn").gameObject.SetActive(false);
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("TimeAD_Btn").gameObject.SetActive(false);
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("DontTimeAD_Btn").gameObject.SetActive(false);
                                }
                                else
                                {
                                    Blacksmith_DontTimeGem_Btn.GetComponentInChildren<Text>().text = BlackSmithDataMgr.GetTemplate(0).CHCost.ToString();
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("TimeGem_Btn").gameObject.SetActive(false);
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("DontTimeGem_Btn").gameObject.SetActive(true);
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("TimeAD_Btn").gameObject.SetActive(false);
                                    BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("DontTimeAD_Btn").gameObject.SetActive(false);
                                }
                            }
                        }
                        else                //장비 제작완료
                        {
                            PackageCheck = false;
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Tanning").GetComponent<Animator>().enabled = false;
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Tanning").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/06_BlackSmith/Tanning");
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Tanning").GetChild(0).GetComponentInChildren<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", a_hour, a_min, a_sec);
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Fire").GetComponent<Animator>().enabled = false;
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Equipment").GetComponent<Image>().enabled = true;
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Slider").GetComponent<Slider>().value = (float)myChar.UntilCompleteTime / (float)myChar.CraftingTime;
                            BlackSmithPanel.transform.Find("Top_Panel").Find("Brazier").Find("Brazier_Text").GetComponent<Text>().text = (((float)myChar.UntilCompleteTime / (float)myChar.CraftingTime) * 100).ToString("F0") + "%";

                            BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("Timer_Text").GetComponent<Text>().text = ""; //"–" + (BlackSmithDataMgr.GetTemplate(0).ADSkip / 60).ToString("D2") + ":" + (BlackSmithDataMgr.GetTemplate(0).ADSkip % 60).ToString("D2");
                            BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("TimeAD_Btn").gameObject.SetActive(false);
                            BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("DontTimeAD_Btn").gameObject.SetActive(true);
                            BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("TimeGem_Btn").gameObject.SetActive(false);
                            BlackSmithPanel.transform.Find("Top_Panel").Find("AD_Timer").Find("DontTimeGem_Btn").gameObject.SetActive(false);

                            BoxNo = 0;
                            GachaPanel.SetActive(true);
                            GachaPanel.transform.Find("BackGround").GetComponent<Image>().enabled = false;
                            if (!myChar.CraftingItemPickupCheck)
                            {
                                BlackSmitComplete();
                                ItemCnt = 1;
                                myChar.CraftingItemPickupCheck = true;
                            }

                        }
                    }
                }
            }
            else            //Forge에서 아이템 선택됐을때 보여주는 정보창 
            {
                if (SelectForgeNum != preSelectForgeNum)
                {
                    preSelectForgeNum = SelectForgeNum;
                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition.x, 0);
                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Arrow").gameObject.GetComponent<Image>().enabled = true;
                }
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Equipment_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(401).Content[myChar.LanguageNum].Replace("\\n", "\n");
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = myChar.HeroHeart.ToString();

                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n");
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n");
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n");
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n");

                if (SelectForgeNum < 1000)      //인포창 게이지 새로 배열에 정렬해주는부분
                {
                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").gameObject.SetActive(true);
                    BlackSmithPanel.transform.Find("Equipment(Avtive)_Btn").gameObject.SetActive(false);
                    BlackSmithPanel.transform.Find("Item(Avtive)_Btn").gameObject.SetActive(false);
                    if (SelectForgeIndex != -1)
                    {
                        if (SelectForgeIndex < 1000)
                        {
                            switch (myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Grade)
                            {
                                case 0:
                                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("ItemName_Text").GetComponent<Text>().text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(SelectForgeIndex + 97).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Text").GetComponent<Text>().text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(195).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[SelectForgeIndex] + 1) + "</color>";
                                    EquipmentUpgradeDsplay(myChar.EquipmentAll[SelectForgeIndex]);
                                    break;
                                case 1:
                                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("ItemName_Text").GetComponent<Text>().text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(SelectForgeIndex + 97).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Text").GetComponent<Text>().text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(196).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[SelectForgeIndex] + 1) + "</color>";
                                    EquipmentUpgradeDsplay(myChar.EquipmentAll[SelectForgeIndex]);
                                    break;
                                case 2:
                                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("ItemName_Text").GetComponent<Text>().text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(SelectForgeIndex + 97).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Text").GetComponent<Text>().text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(197).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[SelectForgeIndex] + 1) + "</color>";
                                    EquipmentUpgradeDsplay(myChar.EquipmentAll[SelectForgeIndex]);
                                    break;
                            }
                            
                            //아이템 설명 부분
                            switch (SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).StatIndex)
                            {
                                case 0:
                                    AddText = null;
                                    break;
                                case 1:     //공격력
                                    if (SelectForgeIndex == 1002 || SelectForgeIndex == 5)
                                    {
                                        AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString() + "%";
                                    }
                                    else
                                    {
                                        AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString() + "%";
                                    }
                                    break;
                                case 2:     //사정거리
                                    AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString() + "%";
                                    break;
                                case 3:     //체력
                                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString() + "%";
                                    break;
                                case 4:     //속도
                                    if (SelectForgeIndex < 1000)
                                    {
                                        AddText = myChar.TextDataMgr.GetTemplate(4).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString() + "%";
                                    }
                                    else
                                    {
                                        AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString() + "%";
                                    }
                                    break;
                                case 5:     //부활 횟수
                                    AddText = myChar.TextDataMgr.GetTemplate(5).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString();
                                    break;
                                case 6:     //하트 획득
                                    if (SelectForgeIndex == 38)
                                    {
                                        AddText = myChar.TextDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n") + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString() + "%";
                                    }
                                    else
                                    {
                                        AddText = myChar.TextDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString();
                                    }                                    
                                    break;
                                case 7:     //쉴드 획득
                                    AddText = myChar.TextDataMgr.GetTemplate(7).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString();
                                    break;
                                case 8:     //높이
                                    AddText = myChar.TextDataMgr.GetTemplate(8).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString();
                                    break;
                                case 9:     //점프 횟수
                                    AddText = myChar.TextDataMgr.GetTemplate(9).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString();
                                    break;
                                case 10:    //튕김
                                    AddText = myChar.TextDataMgr.GetTemplate(10).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString();
                                    break;
                                case 11:    //확률
                                    if (SelectInventoryIndex == 1003)
                                    {
                                        AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString() + "%";
                                    }
                                    else
                                    {
                                        AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString() + "%";
                                    }
                                    break;
                                case 12:    //개수
                                    AddText = myChar.TextDataMgr.GetTemplate(12).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString();
                                    break;
                                case 13:    //지속 시간 (초)
                                    AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).Value.ToString();
                                    break;
                            }

                            if (myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1 != 0)
                            {
                                string ItemInfo = myChar.TextDataMgr.GetTemplate((SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1).InfoIndex)).Content[myChar.LanguageNum].Replace("\\n", "\n");

                                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text =
                                    ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
                            }
                            else if (myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Option1 == 0)
                            {
                                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text = "-\n";
                            }
                            if (myChar.EquipmentAll[SelectForgeIndex] > -1)
                            {
                                switch (myChar.EquipmentAll[SelectForgeIndex])
                                {
                                    case 0:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Atk0.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).ASPD0.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Range0.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).HP0.ToString();
                                        break;
                                    case 1:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Atk1.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).ASPD1.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Range1.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).HP1.ToString();
                                        break;
                                    case 2:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Atk2.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).ASPD2.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Range2.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).HP2.ToString();
                                        break;
                                    case 3:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Atk3.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).ASPD3.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Range3.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).HP3.ToString();
                                        break;
                                    case 4:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Atk4.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).ASPD4.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Range4.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).HP4.ToString();
                                        break;
                                    case 5:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Atk5.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).ASPD5.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Range5.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).HP5.ToString();
                                        break;
                                    case 6:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Atk6.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).ASPD6.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Range6.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).HP6.ToString();
                                        break;
                                    case 7:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Atk7.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).ASPD7.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Range7.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).HP7.ToString();
                                        break;
                                    case 8:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Atk8.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).ASPD8.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Range8.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).HP8.ToString();
                                        break;
                                    case 9:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Atk9.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).ASPD9.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Range9.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).HP9.ToString();
                                        break;
                                    case 10:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Atk10.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).ASPD10.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Range10.ToString();
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).HP10.ToString();
                                        break;
                                }
                            }
                            else
                            {
                                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Atk0.ToString();
                                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).ASPD0.ToString();
                                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Range0.ToString();
                                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = myChar.itemDataMgr.GetTemplate(SelectForgeIndex).HP0.ToString();
                            }
                        }
                        else
                        {
                            if (SelectForgeIndex >= 1000 && SelectForgeIndex < 2000)
                            {
                                switch (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Grade)
                                {
                                    case 0:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("ItemName_Text").GetComponent<Text>().text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(SelectForgeIndex).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Text").GetComponent<Text>().text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(195).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.ElementStoneAll[SelectForgeIndex - 1000] + 1) + "</color>";
                                        EquipmentUpgradeDsplay(myChar.ElementStoneAll[SelectForgeIndex - 1000]);
                                        break;
                                    case 1:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("ItemName_Text").GetComponent<Text>().text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(SelectForgeIndex).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Text").GetComponent<Text>().text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(196).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.ElementStoneAll[SelectForgeIndex - 1000] + 1) + "</color>";
                                        EquipmentUpgradeDsplay(myChar.ElementStoneAll[SelectForgeIndex - 1000]);
                                        break;
                                    case 2:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("ItemName_Text").GetComponent<Text>().text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(SelectForgeIndex).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Text").GetComponent<Text>().text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(197).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.ElementStoneAll[SelectForgeIndex - 1000] + 1) + "</color>";
                                        EquipmentUpgradeDsplay(myChar.ElementStoneAll[SelectForgeIndex - 1000]);
                                        break;
                                }
                                //아이템 설명 부분
                                switch (myChar.ElementStoneAll[SelectForgeIndex - 1000])
                                {
                                    case 0:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv1);
                                        break;
                                    case 1:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv2);
                                        break;
                                    case 2:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv3);
                                        break;
                                    case 3:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv4);
                                        break;
                                    case 4:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv5);
                                        break;
                                    case 5:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv6);
                                        break;
                                    case 6:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv7);
                                        break;
                                    case 7:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv8);
                                        break;
                                    case 8:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv9);
                                        break;
                                    case 9:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv10);
                                        break;
                                    case 10:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv11);
                                        break;
                                }
                                if (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).StatIndex != 0)
                                {
                                    string ItemInfo = myChar.TextDataMgr.GetTemplate(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).InfoIndex).Content[myChar.LanguageNum].Replace("\\n", "\n");

                                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text =
                                        ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
                                }
                                else if (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).StatIndex == 0)
                                {
                                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text = "-\n";
                                }
                            }
                            else if (SelectForgeIndex >= 2000)
                            {
                                switch (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Grade)
                                {
                                    case 0:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("ItemName_Text").GetComponent<Text>().text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(SelectForgeIndex).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Text").GetComponent<Text>().text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(195).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." +( myChar.ActiveitemAll[SelectForgeIndex - 2000] + 1) + "</color>";
                                        EquipmentUpgradeDsplay(myChar.ActiveitemAll[SelectForgeIndex - 2000]);
                                        break;
                                    case 1:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("ItemName_Text").GetComponent<Text>().text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(SelectForgeIndex).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Text").GetComponent<Text>().text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(196).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.ActiveitemAll[SelectForgeIndex - 2000] + 1) + "</color>";
                                        EquipmentUpgradeDsplay(myChar.ActiveitemAll[SelectForgeIndex - 2000]);
                                        break;
                                    case 2:
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("ItemName_Text").GetComponent<Text>().text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(SelectForgeIndex).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Text").GetComponent<Text>().text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(197).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.ActiveitemAll[SelectForgeIndex - 2000] + 1) + "</color>";
                                        EquipmentUpgradeDsplay(myChar.ActiveitemAll[SelectForgeIndex - 2000]);
                                        break;
                                }
                                //아이템 설명 부분
                                switch (myChar.ActiveitemAll[SelectForgeIndex - 2000])
                                {
                                    case 0:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv1);
                                        break;
                                    case 1:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv2);
                                        break;
                                    case 2:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv3);
                                        break;
                                    case 3:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv4);
                                        break;
                                    case 4:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv5);
                                        break;
                                    case 5:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv6);
                                        break;
                                    case 6:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv7);
                                        break;
                                    case 7:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv8);
                                        break;
                                    case 8:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv9);
                                        break;
                                    case 9:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv10);
                                        break;
                                    case 10:
                                        EquipmentInfo(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Lv11);
                                        break;
                                }

                                string ItemInfo = myChar.TextDataMgr.GetTemplate(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).InfoIndex).Content[myChar.LanguageNum].Replace("\\n", "\n");
                                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text =
                                ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";

                                if (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).StatIndex != 0)
                                {
                                    

                                    //if (SelectForgeIndex != 2015)
                                    //{
                                    //    string ItemInfo = myChar.TextDataMgr.GetTemplate(SelectForgeIndex - 1954).Content[myChar.LanguageNum].Replace("\\n", "\n");
                                    //    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text =
                                    //    ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
                                    //}
                                    //else
                                    //{
                                    //    string ItemInfo = myChar.TextDataMgr.GetTemplate(SelectForgeIndex - 1763).Content[myChar.LanguageNum].Replace("\\n", "\n");
                                    //    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text =
                                    //    ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
                                    //}
                                }
                                else if (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).StatIndex == 0)
                                {
                                    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text = "-\n";
                                }
                            }
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = "";
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = "";
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = "";
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = "";
                        }
                    }
                }
            }
        }
        else        //Blacksmith창 닫혔을때
        {
            if (myChar.Crafting)
            {
                if (!myChar.CompleteCheck)
                {
                    int a_hour = (int)((myChar.CraftingTime - myChar.UntilCompleteTime) / 3600);
                    int a_min = (int)((myChar.CraftingTime - myChar.UntilCompleteTime) % 3600 / 60);
                    int a_sec = (int)((myChar.CraftingTime - myChar.UntilCompleteTime) % 3600 % 60);

                    SelectBar.transform.Find("ForgeClose_Btn").Find("Timer").gameObject.SetActive(true);
                    SelectBar.transform.Find("ForgeClose_Btn").Find("Timer").GetComponentInChildren<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", a_hour, a_min, a_sec);
                    SelectBar.transform.Find("ForgeClose_Btn").Find("Notice").gameObject.SetActive(false);
                }
                else
                {
                    SelectBar.transform.Find("ForgeClose_Btn").Find("Timer").gameObject.SetActive(false);
                    SelectBar.transform.Find("ForgeClose_Btn").Find("Notice").gameObject.SetActive(true);
                }

            }
            else
            {
                SelectBar.transform.Find("ForgeClose_Btn").Find("Timer").gameObject.SetActive(false);
                SelectBar.transform.Find("ForgeClose_Btn").Find("Notice").gameObject.SetActive(false);
            }

            ForgeClose_Btn.SetActive(true);
            SelectBar.transform.Find("ForgeOn_Btn").gameObject.SetActive(false);
        }
    }

    private void InventoryPanelCheck()
    {
        if (InventoryPanel.activeSelf) //Inventory창 열려있을때
        {
            if (SelectInventoryNum == -1)   //SelectInventoryNum : 아이템 선택유무
            {
                InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").gameObject.SetActive(false);

                if (!InventoryCheck)
                {
                    InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    InventorySortNum.Clear();
                    if (Nomal > 0)
                    {
                        Nomal = 0;
                        Rare = 0;
                        Unique = 0;
                    }
                    for (int i = 0; i < myChar.EquipmentAll.Length; i++)
                    {
                        switch (myChar.itemDataMgr.GetTemplate(i).Grade)
                        {
                            case 0:
                                Nomal++;
                                break;
                            case 1:
                                Rare++;
                                break;
                            case 2:
                                Unique++;
                                break;
                        }
                        InventorySortNum.Add(i);
                    }
                    //장비 있는거랑 없는거 체크해서 보여주는 순서 변경해주는 코드
                    for (int i = 0; i < myChar.EquipmentAll.Length; i++)
                    {
                        if (myChar.EquipmentAll[i] > -1)
                        {
                            int Cnt;
                            Cnt = InventorySortNum[i];                 //Cnt에 현재 i배열값을넣어두고
                            InventorySortNum.Remove(InventorySortNum[i]);     //해당 부분을 지움(Cnt를 안넣어두면 제거된 숫자를 찾을 수 없음)
                            InventorySortNum.Insert(0, Cnt);           //그리고 다시 배열 0번째에 Cnt값을 넣어서 최상단으로 배치해줌
                                                                       //Debug.Log("RemoveAt" + EquipmentSortNum[i + 1]);
                        }
                        if (i == myChar.EquipmentAll.Length - 1)
                        {
                            InventoryCheck = true;
                        }
                    }
                }
                if (!InventoryArrangement)
                {
                    if (InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").childCount <= myChar.EquipmentAll.Length)
                    {
                        for (int i = 0; i < myChar.EquipmentAll.Length; i++)
                        {
                            if (InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").childCount > i)
                            {
                                InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).gameObject.SetActive(true);
                                InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<ForgeItemNumScript>().ItemIndex = InventorySortNum[i];
                                InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + InventorySortNum[i]);

                                switch (myChar.itemDataMgr.GetTemplate(InventorySortNum[i]).Grade)
                                {
                                    case 0:
                                        InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 0);
                                        InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[InventorySortNum[i]] + 1) + "</color>";
                                        break;
                                    case 1:
                                        InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 1);
                                        InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[InventorySortNum[i]] + 1) + "</color>";
                                        break;
                                    case 2:
                                        InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 2);
                                        InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[InventorySortNum[i]] + 1) + "</color>";
                                        break;
                                }

                                if (myChar.EquipmentAll[InventorySortNum[i]] != -1)
                                {
                                    InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(false);
                                }
                                else
                                {
                                    InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(true);
                                    InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "-";
                                }

                            }
                            else
                            {
                                GameObject InventoryUI = Instantiate(ForgeBtn_UI, transform.position, Quaternion.identity);
                                InventoryUI.transform.parent = InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").transform;
                                InventoryUI.transform.localScale = new Vector3(1, 1, 1);
                                InventoryUI.GetComponent<ForgeItemNumScript>().ItemIndex = InventorySortNum[i];
                                InventoryUI.transform.Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + InventorySortNum[i]);

                                switch (myChar.itemDataMgr.GetTemplate(InventorySortNum[i]).Grade)
                                {
                                    case 0:
                                        InventoryUI.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 0);
                                        InventoryUI.transform.Find("Lv_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[InventorySortNum[i]] + 1) + "</color>";
                                        break;
                                    case 1:
                                        InventoryUI.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 1);
                                        InventoryUI.transform.Find("Lv_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[InventorySortNum[i]] + 1) + "</color>";
                                        break;
                                    case 2:
                                        InventoryUI.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 2);
                                        InventoryUI.transform.Find("Lv_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[InventorySortNum[i]] + 1) + "</color>";
                                        break;
                                }
                                if (myChar.EquipmentAll[InventorySortNum[i]] != -1)
                                {
                                    InventoryUI.transform.Find("DisablePanel").gameObject.SetActive(false);
                                }
                                else
                                {
                                    InventoryUI.transform.Find("DisablePanel").gameObject.SetActive(true);
                                    InventoryUI.transform.Find("Lv_Text").GetComponent<Text>().text = "-";
                                }
                            }

                            for (int j = 0; j < myChar.InventoryItemNum.Length; j++)
                            {
                                if (myChar.InventoryItemNum[j] != -1)
                                {
                                    if (myChar.InventoryItemNum[j] == InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<ForgeItemNumScript>().ItemIndex)
                                    {
                                        InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(false);
                                    }
                                }
                            }

                            if (i == myChar.EquipmentAll.Length - 1)
                            {
                                InventoryArrangement = true;
                            }
                        }
                    }
                    else if (InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").childCount > myChar.EquipmentAll.Length)
                    {
                        for (int i = 0; i < InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").childCount; i++)
                        {
                            if (i < myChar.EquipmentAll.Length)
                            {
                                InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).gameObject.SetActive(true);
                                InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<ForgeItemNumScript>().ItemIndex = InventorySortNum[i];
                                InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + InventorySortNum[i]);

                                switch (myChar.itemDataMgr.GetTemplate(InventorySortNum[i]).Grade)
                                {
                                    case 0:
                                        InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 0);
                                        InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[InventorySortNum[i]] + 1) + "</color>";
                                        break;
                                    case 1:
                                        InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 1);
                                        InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[InventorySortNum[i]] + 1) + "</color>";
                                        break;
                                    case 2:
                                        InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 2);
                                        InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[InventorySortNum[i]] + 1) + "</color>";
                                        break;
                                }
                                if (myChar.EquipmentAll[InventorySortNum[i]] != -1)
                                {
                                    InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(false);
                                }
                                else
                                {
                                    InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(true);
                                    InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Lv_Text").GetComponent<Text>().text = "-";
                                }
                            }
                            else
                            {
                                InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").GetChild(i).gameObject.SetActive(false);
                            }

                            if (i == InventoryPanel.transform.GetChild(0).GetChild(0).Find("Content").childCount - 1)
                            {
                                InventoryArrangement = true;
                            }
                        }
                    }
                }
            }
            else            //Forge에서 아이템 선택됐을때 보여주는 정보창 
            {
                //스크롤 맨위로 올려주는 코드
                //if (SelectForgeNum != preSelectForgeNum)
                //{
                //    preSelectForgeNum = SelectForgeNum;
                //    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition =
                //        new Vector2(BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition.x, 0);
                //    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Arrow").gameObject.GetComponent<Image>().enabled = true;
                //}
                //InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").gameObject.SetActive(true);

                if (SelectInventoryIndex < 1000)      //인포창 게이지 새로 배열에 정렬해주는부분
                {
                    //장비를 보유하고있을대만 장착 창이 열리게 해주는 코드
                    if (myChar.EquipmentAll[SelectInventoryIndex] > -1)
                    {
                        InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").gameObject.SetActive(true);

                        if (myChar.InventoryItemNum[SelectInventoryArrayNum] == SelectInventoryIndex)
                        {
                            InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").GetChild(0).Find("Equip_Btn").gameObject.SetActive(false);
                            InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").GetChild(0).Find("Release_Btn").gameObject.SetActive(true);
                            InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").GetChild(0).Find("Release_Btn").GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(204).Content[myChar.LanguageNum].Replace("\\n", "\n");
                        }
                        else
                        {
                            InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").GetChild(0).Find("Equip_Btn").gameObject.SetActive(true);
                            InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").GetChild(0).Find("Release_Btn").gameObject.SetActive(false);
                            InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").GetChild(0).Find("Equip_Btn").GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(203).Content[myChar.LanguageNum].Replace("\\n", "\n");
                        }
                    }

                    GameObject EquipmentInfo = InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").GetChild(0).GetChild(0).gameObject;

                    EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n");

                    switch (myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Grade)
                    {
                        case 0:
                            EquipmentInfo.transform.Find("ItemName_Text").GetComponent<Text>().text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(SelectInventoryIndex + 97).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            EquipmentInfo.transform.Find("Grade_Text").GetComponent<Text>().text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(195).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            EquipmentInfo.transform.Find("LV_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[SelectInventoryIndex] + 1) + "</color>";
                            break;
                        case 1:
                            EquipmentInfo.transform.Find("ItemName_Text").GetComponent<Text>().text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(SelectInventoryIndex + 97).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            EquipmentInfo.transform.Find("Grade_Text").GetComponent<Text>().text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(196).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            EquipmentInfo.transform.Find("LV_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[SelectInventoryIndex] + 1) + "</color>";
                            break;
                        case 2:
                            EquipmentInfo.transform.Find("ItemName_Text").GetComponent<Text>().text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(SelectInventoryIndex + 97).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            EquipmentInfo.transform.Find("Grade_Text").GetComponent<Text>().text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(197).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            EquipmentInfo.transform.Find("LV_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[SelectInventoryIndex] + 1) + "</color>";
                            break;
                    }

                    switch (myChar.EquipmentAll[SelectInventoryIndex])
                    {
                        case 0:
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Atk0.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).ASPD0.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Range0.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).HP0.ToString();
                            break;
                        case 1:
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Atk1.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).ASPD1.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Range1.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).HP1.ToString();
                            break;
                        case 2:
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Atk2.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).ASPD2.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Range2.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).HP2.ToString();
                            break;
                        case 3:
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Atk3.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).ASPD3.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Range3.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).HP3.ToString();
                            break;
                        case 4:
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Atk4.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).ASPD4.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Range4.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).HP4.ToString();
                            break;
                        case 5:
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Atk5.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).ASPD5.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Range5.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).HP5.ToString();
                            break;
                        case 6:
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Atk6.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).ASPD6.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Range6.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).HP6.ToString();
                            break;
                        case 7:
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Atk7.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).ASPD7.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Range7.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).HP7.ToString();
                            break;
                        case 8:
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Atk8.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).ASPD8.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Range8.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).HP8.ToString();
                            break;
                        case 9:
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Atk9.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).ASPD9.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Range9.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).HP9.ToString();
                            break;
                        case 10:
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Atk10.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(1).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).ASPD10.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(2).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Range10.ToString();
                            EquipmentInfo.transform.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).GetChild(3).Find("LV_Text").GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).HP10.ToString();
                            break;
                    }
                    //아이템 설명 부분
                    switch (SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).StatIndex)
                    {
                        case 0:
                            AddText = null;
                            break;
                        case 1:     //공격력
                            if(SelectInventoryIndex == 1002 || SelectInventoryIndex == 5)
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString() + "%";
                            }
                            else
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString() + "%";
                            }
                            break;
                        case 2:     //사정거리
                            AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString() + "%";
                            break;
                        case 3:     //체력
                            AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString() + "%";
                            break;
                        case 4:     //속도
                            if (SelectInventoryIndex < 1000)
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(4).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString() + "%";
                            }
                            else
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString() + "%";
                            }
                            break;
                        case 5:     //부활 횟수
                            AddText = myChar.TextDataMgr.GetTemplate(5).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString();
                            break;
                        case 6:     //하트 획득
                            if (SelectInventoryIndex == 38)
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n") + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString() + "%";
                            }
                            else
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString();
                            }                            
                            break;
                        case 7:     //쉴드 획득
                            AddText = myChar.TextDataMgr.GetTemplate(7).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString();
                            break;
                        case 8:     //높이
                            AddText = myChar.TextDataMgr.GetTemplate(8).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString();
                            break;
                        case 9:     //점프 횟수
                            AddText = myChar.TextDataMgr.GetTemplate(9).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString();
                            break;
                        case 10:    //튕김
                            AddText = myChar.TextDataMgr.GetTemplate(10).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString();
                            break;
                        case 11:    //확률
                            AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString() + "%";
                            break;
                        case 12:    //개수
                            AddText = myChar.TextDataMgr.GetTemplate(12).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString();
                            break;
                        case 13:    //지속 시간 (초)
                            AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).Value.ToString();
                            break;

                    }
                    if (myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1 != 0)
                    {
                        string ItemInfo = myChar.TextDataMgr.GetTemplate((SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1).InfoIndex)).Content[myChar.LanguageNum].Replace("\\n", "\n");

                        InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text =
                            ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
                    }
                    else if (myChar.itemDataMgr.GetTemplate(SelectInventoryIndex).Option1 == 0)
                    {
                        InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text = "-\n";
                    }
                }
            }
        }
        else        //Inventory창 닫혔을때
        {
            SelectInventoryNum = -1;
            SelectInventoryArrayNum = -1;
            InventoryArrangement = false;
        }
    }
    private void StageSelectPanelCheck()
    {
        if (StageSelectWindow.activeSelf)
        {
            Text Info_Text = StageSelectWindow.transform.Find("StageName").GetChild(0).GetComponent<Text>();

            if (myChar.Chapter < myChar.StageClearCheck.Length)
            {
                Info_Text.text = myChar.TextDataMgr.GetTemplate(myChar.Chapter + 9000).Content[myChar.LanguageNum].Replace("\\n", "\n");

                if (myChar.StageClearCheck[myChar.Chapter])
                {
                    StageSelectWindow.transform.GetChild(0).GetChild(0).GetChild(myChar.Chapter).GetChild(0).gameObject.SetActive(false);
                    StageSelectWindow.transform.GetChild(0).GetChild(0).GetChild(myChar.Chapter).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/08_Stage/01.Possible/" + myChar.Chapter);
                    StageSelectWindow.transform.Find("StartBtn").gameObject.SetActive(true);
                    StageSelectWindow.transform.Find("Impassible_Text").gameObject.SetActive(false);
                }
                else
                {
                    StageSelectWindow.transform.GetChild(0).GetChild(0).GetChild(myChar.Chapter).GetChild(0).gameObject.SetActive(true);
                    StageSelectWindow.transform.GetChild(0).GetChild(0).GetChild(myChar.Chapter).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/08_Stage/02.Impossible/" + myChar.Chapter);
                    StageSelectWindow.transform.Find("StartBtn").gameObject.SetActive(false);
                    StageSelectWindow.transform.Find("Impassible_Text").gameObject.SetActive(true);
                    StageSelectWindow.transform.Find("Impassible_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate((myChar.Chapter - 1) + 9100).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }
            }
            else
            {
                Info_Text.text = myChar.TextDataMgr.GetTemplate(9099).Content[myChar.LanguageNum].Replace("\\n", "\n");

                StageSelectWindow.transform.Find("StartBtn").gameObject.SetActive(false);
                StageSelectWindow.transform.Find("Impassible_Text").gameObject.SetActive(true);
                StageSelectWindow.transform.Find("Impassible_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(9199).Content[myChar.LanguageNum].Replace("\\n", "\n");
            }

        }
        else
        {
        }
    }
    //private void ItemInfoPanelCheck()
    //{
    //    if (ItemInfo_Panel.activeSelf)
    //    {
    //        Text Name_Text = ItemInfo_Panel.transform.GetChild(0).Find("ItemName_Text").GetComponent<Text>();
    //        Text Grade_Text = ItemInfo_Panel.transform.GetChild(0).Find("Grade_Text").GetComponent<Text>();
    //        Text Info_Text = ItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();

    //        if (!SlotGageCheck)
    //        {
    //            ItemAtkGage.Clear();
    //            ItemASPDGage.Clear();
    //            ItemRangeGage.Clear();
    //            ItemHPGage.Clear();
    //            for (int i = 0; i < 10; i++)
    //            {
    //                ItemAtkGage.Add(ItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").Find("ItemAtkGage").transform.GetChild(i).gameObject);
    //                ItemASPDGage.Add(ItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").Find("ItemASPDGage").transform.GetChild(i).gameObject);
    //                ItemRangeGage.Add(ItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").Find("ItemRangeGage").transform.GetChild(i).gameObject);
    //                ItemHPGage.Add(ItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").Find("ItemHPGage").transform.GetChild(i).gameObject);
    //                //ItemAtkGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //                //ItemASPDGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //                //ItemRangeGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //                //ItemHPGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //            }
    //            SlotGageCheck = true;
    //        }
    //        if (myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] >= 0)
    //        {
    //            if (SelectMachineEquipmentNum == 0)
    //            {
    //                ItemInfoGrade(myChar.EquipmentAll[myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]]);
    //                PreferencesItemState(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]);
    //                //EquipmentInfoText();

    //                switch (myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Grade)
    //                {
    //                    case 0:
    //                        Name_Text.text = "<color=#e4e4e4>" + myChar.TextDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]] + 101).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        Grade_Text.text = "<color=#e4e4e4>" + myChar.TextDataMgr.GetTemplate(195).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        break;
    //                    case 1:
    //                        Name_Text.text = "<color=#fff200>" + myChar.TextDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]] + 101).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        Grade_Text.text = "<color=#fff200>" + myChar.TextDataMgr.GetTemplate(196).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        break;
    //                    case 2:
    //                        Name_Text.text = "<color=#ff00fe>" + myChar.TextDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]] + 101).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        Grade_Text.text = "<color=#ff00fe>" + myChar.TextDataMgr.GetTemplate(197).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        break;
    //                }
    //                switch (SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1).StatIndex)
    //                {
    //                    case 0:
    //                        AddText = null;
    //                        break;
    //                    case 1:     //공격력
    //                        AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1).Value.ToString() + "%";
    //                        break;
    //                    case 4:     //속도
    //                        AddText = myChar.TextDataMgr.GetTemplate(4).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1).Value.ToString() + "%";
    //                        break;
    //                    case 5:     //부활 횟수
    //                        AddText = myChar.TextDataMgr.GetTemplate(5).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1).Value.ToString();
    //                        break;
    //                    case 6:     //하트 획득
    //                        AddText = myChar.TextDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1).Value.ToString();
    //                        break;
    //                    case 8:     //높이
    //                        AddText = myChar.TextDataMgr.GetTemplate(8).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1).Value.ToString();
    //                        break;
    //                    case 9:     //점프 횟수
    //                        AddText = myChar.TextDataMgr.GetTemplate(9).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1).Value.ToString();
    //                        break;
    //                    case 10:    //튕김
    //                        AddText = myChar.TextDataMgr.GetTemplate(10).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1).Value.ToString();
    //                        break;
    //                    case 11:    //확률
    //                        AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1).Value.ToString() + "%";
    //                        break;
    //                    case 12:    //개수
    //                        AddText = myChar.TextDataMgr.GetTemplate(12).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1).Value.ToString();
    //                        break;
    //                }
    //                if (myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1 != 0)
    //                {
    //                    string ItemInfo = myChar.TextDataMgr.GetTemplate((SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1).InfoIndex)).Content[myChar.LanguageNum].Replace("\\n", "\n");

    //                    ItemInfo_Panel.transform.GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text =
    //                        ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
    //                }
    //                else if (myChar.itemDataMgr.GetTemplate(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum]]).Option1 == 0)
    //                {
    //                    ItemInfo_Panel.transform.GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text = "-\n";
    //                }
    //            }
    //            else if (SelectMachineEquipmentNum == 1)
    //            {
    //                ItemInfoGrade(myChar.ActiveitemAll[myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] - 2000]);
    //                PreferencesItemState(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]);

    //                switch (myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]).Grade)
    //                {
    //                    case 0:
    //                        Name_Text.text = "<color=#e4e4e4>" + myChar.TextDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] - 1969).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        Grade_Text.text = "<color=#e4e4e4>" + myChar.TextDataMgr.GetTemplate(195).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        break;
    //                    case 1:
    //                        Name_Text.text = "<color=#fff200>" + myChar.TextDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] - 1969).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        Grade_Text.text = "<color=#fff200>" + myChar.TextDataMgr.GetTemplate(196).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        break;
    //                    case 2:
    //                        Name_Text.text = "<color=#ff00fe>" + myChar.TextDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] - 1969).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        Grade_Text.text = "<color=#ff00fe>" + myChar.TextDataMgr.GetTemplate(197).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        break;
    //                }
    //                switch (myChar.ActiveitemAll[myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] - 2000])
    //                {
    //                    case 0:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] ).Lv0, myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] );
    //                        break;
    //                    case 1:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] ).Lv1, myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] );
    //                        break;
    //                    case 2:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] ).Lv2, myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] );
    //                        break;
    //                    case 3:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] ).Lv3, myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] );
    //                        break;
    //                    case 4:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] ).Lv4, myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] );
    //                        break;
    //                    case 5:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] ).Lv5, myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] );
    //                        break;
    //                    case 6:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] ).Lv6, myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] );
    //                        break;
    //                }
    //                if (myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] ).StatIndex != 0)
    //                {
    //                    string ItemInfo = myChar.TextDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] - 1954).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                    ItemInfo_Panel.transform.GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text =
    //                    ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
    //                                        }
    //                else if (myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]).StatIndex == 0)
    //                {
    //                    ItemInfo_Panel.transform.GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text = "-\n";
    //                }
    //            }
    //            else if (SelectMachineEquipmentNum == 2)
    //            {
    //                ItemInfoGrade(myChar.ElementStoneAll[myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] -1] - 1000]);
    //                PreferencesItemState(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]);

    //                switch (myChar.ActiveitemDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]).Grade)
    //                {
    //                    case 0:
    //                        Name_Text.text = "<color=#e4e4e4>" + myChar.TextDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] - 977).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        Grade_Text.text = "<color=#e4e4e4>" + myChar.TextDataMgr.GetTemplate(195).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        break;
    //                    case 1:
    //                        Name_Text.text = "<color=#fff200>" + myChar.TextDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] - 977).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        Grade_Text.text = "<color=#fff200>" + myChar.TextDataMgr.GetTemplate(196).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        break;
    //                    case 2:
    //                        Name_Text.text = "<color=#ff00fe>" + myChar.TextDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] - 977).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        Grade_Text.text = "<color=#ff00fe>" + myChar.TextDataMgr.GetTemplate(197).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                        break;
    //                }
    //                switch (myChar.ElementStoneAll[myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] - 1000])
    //                {
    //                    case 0:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]).Lv0, myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]);
    //                        break;
    //                    case 1:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]).Lv1, myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]);
    //                        break;
    //                    case 2:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]).Lv2, myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]);
    //                        break;
    //                    case 3:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]).Lv3, myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]);
    //                        break;
    //                    case 4:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]).Lv4, myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]);
    //                        break;
    //                    case 5:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]).Lv5, myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]);
    //                        break;
    //                    case 6:
    //                        SlotEquipmentInfo(myChar.ActiveitemDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]).Lv6, myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]);
    //                        break;
    //                }
    //                if (myChar.ActiveitemDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]).StatIndex != 0)
    //                {
    //                    string ItemInfo = myChar.TextDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1] - 973).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                    ItemInfo_Panel.transform.GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text = ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
    //                }
    //                else if (myChar.ActiveitemDataMgr.GetTemplate(myChar.ElementStoneActive[myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] - 1]).StatIndex == 0)
    //                {
    //                    ItemInfo_Panel.transform.GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text = "-\n";
    //                }

    //            }

    //        }
    //        else if (myChar.LobbyMachineEquipmentNum[SelectMachineEquipmentNum] < 0)
    //        {
    //            ItemInfoGrade(0);
    //            PreferencesDontItemState();
    //            ItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = "";
    //            Name_Text.text = "";
    //            Grade_Text.text = "";
    //        }
    //    }
    //    else
    //    {
    //        SlotGageCheck = false;
    //    }
    //}
    private void SkinArrayCheck(ObscuredInt[] WeaponSkin, ObscuredInt[] CostumeSkin)
    {
        Transform SkinWeaponContent = SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinWeapon_Background").Find("Scroll View").GetChild(0).GetChild(0).Find("Skin_Panel");
        Transform SkinCostumeContent = SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinCostume_Background").Find("Scroll View").GetChild(0).GetChild(0).Find("Skin_Panel");

        SkinWeaponContent.GetComponent<RectTransform>().sizeDelta = new Vector2(405, Mathf.CeilToInt((WeaponSkin.Length) / 2f) * 215 - 25);
        SkinCostumeContent.GetComponent<RectTransform>().sizeDelta = new Vector2(405, Mathf.CeilToInt(CostumeSkin.Length / 2f) * 215 - 25);
        for (int i = 0; i < WeaponSkin.Length; i++)
        {
            WeaponSkinCnt.Add(WeaponSkin[i]);
        }
        for (int i = 0; i < CostumeSkin.Length; i++)
        {
            CostumeSkinCnt.Add(CostumeSkin[i]);
        }
        if (SkinWeaponContent.childCount <= WeaponSkin.Length)
        {
            for (int i = 0; i < WeaponSkin.Length; i++)
            {
                if (i < SkinWeaponContent.childCount)
                {
                    SkinWeaponContent.transform.GetChild(i).gameObject.SetActive(true);
                    SkinWeaponContent.transform.GetChild(i).GetComponent<SkinInfoScript>().SkinIndex = WeaponSkin[i];
                    SkinWeaponContent.transform.GetChild(i).GetComponent<SkinInfoScript>().BtnNum = i;
                    SkinWeaponContent.transform.GetChild(i).GetComponent<SkinInfoScript>().Weapon = true;
                    SkinWeaponContent.transform.GetChild(i).GetComponent<SkinInfoScript>().Costume = false;
                    SkinWeaponContent.transform.GetChild(i).Find("Skin_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + WeaponSkin[i]);
                    switch (SkinDataMgr.GetTemplate(WeaponSkin[i]).Grade)
                    {
                        case 0:
                            SkinWeaponContent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/0");
                            break;
                        case 1:
                            SkinWeaponContent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/1");
                            break;
                        case 2:
                            SkinWeaponContent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/2");
                            break;
                    }
                }
                else
                {
                    GameObject SkinUI = Instantiate(SkinBtn_UI, transform.position, Quaternion.identity);
                    SkinUI.transform.parent = SkinWeaponContent;
                    SkinUI.transform.localScale = new Vector3(1, 1, 1);
                    SkinUI.GetComponent<SkinInfoScript>().SkinIndex = WeaponSkin[i];
                    SkinUI.GetComponent<SkinInfoScript>().BtnNum = i;
                    //SkinUI.transform.Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + EquipmentSortNum[i]);
                    SkinUI.GetComponent<SkinInfoScript>().Weapon = true;
                    SkinUI.GetComponent<SkinInfoScript>().Costume = false;
                    SkinUI.transform.Find("Skin_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + WeaponSkin[i]);
                    switch (SkinDataMgr.GetTemplate(WeaponSkin[i]).Grade)
                    {
                        case 0:
                            SkinUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/0");
                            break;
                        case 1:
                            SkinUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/1");
                            break;
                        case 2:
                            SkinUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/2");
                            break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < SkinWeaponContent.childCount; i++)
            {
                if (i < WeaponSkin.Length)
                {
                    SkinWeaponContent.transform.GetChild(i).gameObject.SetActive(true);
                    SkinWeaponContent.transform.GetChild(i).GetComponent<SkinInfoScript>().SkinIndex = WeaponSkin[i];
                    SkinWeaponContent.transform.GetChild(i).GetComponent<SkinInfoScript>().BtnNum = i;
                    SkinWeaponContent.transform.GetChild(i).GetComponent<SkinInfoScript>().Weapon = true;
                    SkinWeaponContent.transform.GetChild(i).GetComponent<SkinInfoScript>().Costume = false;
                    SkinWeaponContent.transform.GetChild(i).Find("Skin_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + WeaponSkin[i]);
                    switch (SkinDataMgr.GetTemplate(WeaponSkin[i]).Grade)
                    {
                        case 0:
                            SkinWeaponContent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/0");
                            break;
                        case 1:
                            SkinWeaponContent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/1");
                            break;
                        case 2:
                            SkinWeaponContent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/2");
                            break;
                    }
                }
                else
                {
                    SkinWeaponContent.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        if (SkinCostumeContent.childCount <= CostumeSkin.Length)
        {
            for (int i = 0; i < CostumeSkin.Length; i++)
            {
                if (i < SkinCostumeContent.childCount)
                {
                    SkinCostumeContent.transform.GetChild(i).gameObject.SetActive(true);
                    SkinCostumeContent.transform.GetChild(i).GetComponent<SkinInfoScript>().SkinIndex = CostumeSkin[i];
                    SkinCostumeContent.transform.GetChild(i).GetComponent<SkinInfoScript>().BtnNum = i;
                    SkinCostumeContent.transform.GetChild(i).GetComponent<SkinInfoScript>().Weapon = false;
                    SkinCostumeContent.transform.GetChild(i).GetComponent<SkinInfoScript>().Costume = true;
                    SkinCostumeContent.transform.GetChild(i).Find("Skin_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + CostumeSkin[i]);
                    SkinCostumeContent.transform.GetChild(i).transform.Find("Skin_Icon").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 35);
                    switch (SkinDataMgr.GetTemplate(CostumeSkin[i]).Grade)
                    {
                        case 0:
                            SkinCostumeContent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/0");
                            break;
                        case 1:
                            SkinCostumeContent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/1");
                            break;
                        case 2:
                            SkinCostumeContent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/2");
                            break;
                    }
                }
                else
                {
                    GameObject SkinUI = Instantiate(SkinBtn_UI, transform.position, Quaternion.identity);
                    SkinUI.transform.parent = SkinCostumeContent;
                    SkinUI.transform.localScale = new Vector3(1, 1, 1);
                    SkinUI.GetComponent<SkinInfoScript>().SkinIndex = CostumeSkin[i];
                    SkinUI.GetComponent<SkinInfoScript>().BtnNum = i;
                    SkinUI.transform.Find("Skin_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + CostumeSkin[i]);
                    SkinUI.transform.Find("Skin_Icon").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 35);
                    //SkinUI.transform.Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + EquipmentSortNum[i]);
                    SkinUI.GetComponent<SkinInfoScript>().Weapon = false;
                    SkinUI.GetComponent<SkinInfoScript>().Costume = true;
                    switch (SkinDataMgr.GetTemplate(CostumeSkin[i]).Grade)
                    {
                        case 0:
                            SkinUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/0");
                            break;
                        case 1:
                            SkinUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/1");
                            break;
                        case 2:
                            SkinUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/2");
                            break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < SkinCostumeContent.childCount; i++)
            {
                if (i < CostumeSkin.Length)
                {
                    SkinCostumeContent.transform.GetChild(i).gameObject.SetActive(true);
                    SkinCostumeContent.transform.GetChild(i).GetComponent<SkinInfoScript>().SkinIndex = CostumeSkin[i];
                    SkinCostumeContent.transform.GetChild(i).GetComponent<SkinInfoScript>().BtnNum = i;
                    SkinCostumeContent.transform.GetChild(i).GetComponent<SkinInfoScript>().Weapon = false;
                    SkinCostumeContent.transform.GetChild(i).GetComponent<SkinInfoScript>().Costume = true;
                    SkinCostumeContent.transform.GetChild(i).Find("Skin_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + CostumeSkin[i]);
                    SkinCostumeContent.transform.GetChild(i).transform.Find("Skin_Icon").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 35);
                    switch (SkinDataMgr.GetTemplate(CostumeSkin[i]).Grade)
                    {
                        case 0:
                            SkinCostumeContent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/0");
                            break;
                        case 1:
                            SkinCostumeContent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/1");
                            break;
                        case 2:
                            SkinCostumeContent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/02_SkinBoard/2");
                            break;
                    }
                }
                else
                {
                    SkinCostumeContent.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
    private void SkinCostPurchaseCheck(ObscuredInt[] WeaponPurchase, ObscuredInt[] CostumePurchase)
    {
        if (myChar.SelectWeapon[myChar.SelectHero] == -1 && myChar.SelectCostume[myChar.SelectHero] == -1)//코스튬 선택안된상태(입고있는 장비 선택으로 봄)
        {
            SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").gameObject.SetActive(false);
            SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").gameObject.SetActive(true);
            SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(0).gameObject.SetActive(false);
            SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(1).gameObject.SetActive(false);
            SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(2).gameObject.SetActive(true);
            SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(2).GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(9206).Content[myChar.LanguageNum].Replace("\\n", "\n");
            SkinInfoPanel.transform.GetChild(0).Find("SkinName").GetChild(0).GetComponent<Text>().text = SkinDataMgr.GetTemplate(CostumeSkinCnt[myChar.EquipmentCostume[myChar.SelectHero]]).Content[myChar.LanguageNum].Replace("\\n", "\n");
            for (int i = 0; i < CostumeSkinCnt.Count; i++)
            {
                if (i == myChar.EquipmentCostume[myChar.SelectHero])
                {
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 1);
                }
                else
                {
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 0);
                }
            }

            switch (myChar.SelectHero)
            {
                case 0:
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + myChar.EquipmentWeapon[myChar.SelectHero]);
                    break;
                case 1:
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + (myChar.EquipmentWeapon[myChar.SelectHero] + 100));
                    break;
                case 2:
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + (myChar.EquipmentWeapon[myChar.SelectHero] + 200));
                    break;
                case 3:
                    for (int i = 0; i < myChar.WizardWeaponSkin.Length; i++)
                    {
                        if (i == myChar.EquipmentWeapon[myChar.SelectHero])
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetChild(i).gameObject.SetActive(true);
                        }
                        else
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetChild(i).gameObject.SetActive(false);
                        }

                    }
                    break;
                case 4:
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + (myChar.EquipmentWeapon[myChar.SelectHero] + 400));
                    break;
            }
        }
        else if (myChar.SelectCostume[myChar.SelectHero] != -1)      //의복 코스튬이 선택되었을때
        {
            SkinInfoPanel.transform.GetChild(0).Find("SkinName").GetChild(0).GetComponent<Text>().text = SkinDataMgr.GetTemplate(CostumeSkinCnt[myChar.SelectCostume[myChar.SelectHero]]).Content[myChar.LanguageNum].Replace("\\n", "\n");
            for (int i = 0; i < CostumeSkinCnt.Count; i++)
            {
                if (i == myChar.SelectCostume[myChar.SelectHero])
                {
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 1);
                }
                else
                {
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 0);
                }
            }
            if (CostumePurchase[myChar.SelectCostume[myChar.SelectHero]] != -1)    //코스튬이 구매된상태
            {
                SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").gameObject.SetActive(false);
                SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").gameObject.SetActive(true);
                if (myChar.EquipmentCostume[myChar.SelectHero] != myChar.SelectCostume[myChar.SelectHero])
                {
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(0).gameObject.SetActive(true);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(1).gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(2).gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(0).GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(203).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }
                else
                {
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(0).gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(1).gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(2).gameObject.SetActive(true);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(2).GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(9206).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }

            }
            else        //코스튬 구매 안된 상태
            {
                for (int i = 0; i < CostumeSkinCnt.Count; i++)
                {
                    if (i == myChar.SelectCostume[myChar.SelectHero])
                    {
                        HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 1);
                    }
                    else
                    {
                        HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 0);
                    }
                }
                if (SkinDataMgr.GetTemplate(CostumeSkinCnt[myChar.SelectCostume[myChar.SelectHero]]).Resource == 0)
                {
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").gameObject.SetActive(true);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(0).gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(1).gameObject.SetActive(true);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(2).gameObject.SetActive(false);

                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(0).GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(9207).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(1).GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(9207).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }
                else if (SkinDataMgr.GetTemplate(CostumeSkinCnt[myChar.SelectCostume[myChar.SelectHero]]).Resource == 1)
                {
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").gameObject.SetActive(true);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(2).gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").GetChild(0).GetComponentInChildren<Text>().text = SkinDataMgr.GetTemplate(CostumeSkinCnt[myChar.SelectCostume[myChar.SelectHero]]).Cost.ToString();
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").GetChild(1).GetComponentInChildren<Text>().text = SkinDataMgr.GetTemplate(CostumeSkinCnt[myChar.SelectCostume[myChar.SelectHero]]).Cost.ToString();
                    if (myChar.Gem >= SkinDataMgr.GetTemplate(CostumeSkinCnt[myChar.SelectCostume[myChar.SelectHero]]).Cost)
                    {
                        SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").GetChild(0).gameObject.SetActive(true);
                        SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").GetChild(1).gameObject.SetActive(false);
                    }
                    else
                    {
                        SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").GetChild(0).gameObject.SetActive(false);
                        SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").GetChild(1).gameObject.SetActive(true);
                    }
                }

            }
        }
        else if (myChar.SelectWeapon[myChar.SelectHero] != -1)      //무기 코스튬이 선택되었을때
        {
            switch (myChar.SelectHero)
            {
                case 0:
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + myChar.SelectWeapon[myChar.SelectHero]);
                    break;
                case 1:
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + (myChar.SelectWeapon[myChar.SelectHero] + 100));
                    break;
                case 2:
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + (myChar.SelectWeapon[myChar.SelectHero] + 200));
                    break;
                case 3:
                    for (int i = 0; i < myChar.WizardWeaponSkin.Length; i++)
                    {
                        if (i == myChar.SelectWeapon[myChar.SelectHero])
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetChild(i).gameObject.SetActive(true);
                        }
                        else
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetChild(i).gameObject.SetActive(false);
                        }

                    }
                    break;
                case 4:
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + (myChar.SelectWeapon[myChar.SelectHero] + 400));
                    break;
            }

            SkinInfoPanel.transform.GetChild(0).Find("SkinName").GetChild(0).GetComponent<Text>().text = SkinDataMgr.GetTemplate(WeaponSkinCnt[myChar.SelectWeapon[myChar.SelectHero]]).Content[myChar.LanguageNum].Replace("\\n", "\n");
            if (WeaponPurchase[myChar.SelectWeapon[myChar.SelectHero]] != -1)    //코스튬이 구매된상태
            {
                SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").gameObject.SetActive(false);
                SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").gameObject.SetActive(true);

                if (myChar.EquipmentWeapon[myChar.SelectHero] != myChar.SelectWeapon[myChar.SelectHero])
                {
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(0).gameObject.SetActive(true);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(1).gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(2).gameObject.SetActive(false);

                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(0).GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(203).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }
                else
                {
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(0).gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(1).gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(2).gameObject.SetActive(true);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(2).GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(9206).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }
            }
            else        //코스튬 구매 안된 상태
            {
                if (SkinDataMgr.GetTemplate(WeaponSkinCnt[myChar.SelectWeapon[myChar.SelectHero]]).Resource == 0)
                {
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").gameObject.SetActive(true);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(0).gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(1).gameObject.SetActive(true);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(2).gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(0).GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(9207).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(1).GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(9207).Content[myChar.LanguageNum].Replace("\\n", "\n");

                }
                else if (SkinDataMgr.GetTemplate(WeaponSkinCnt[myChar.SelectWeapon[myChar.SelectHero]]).Resource == 1)
                {
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").gameObject.SetActive(true);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").gameObject.SetActive(false);
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").GetChild(0).GetComponentInChildren<Text>().text = SkinDataMgr.GetTemplate(WeaponSkinCnt[myChar.SelectWeapon[myChar.SelectHero]]).Cost.ToString();
                    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").GetChild(1).GetComponentInChildren<Text>().text = SkinDataMgr.GetTemplate(WeaponSkinCnt[myChar.SelectWeapon[myChar.SelectHero]]).Cost.ToString();
                    if (myChar.Gem >= SkinDataMgr.GetTemplate(WeaponSkinCnt[myChar.SelectWeapon[myChar.SelectHero]]).Cost)
                    {
                        SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").GetChild(0).gameObject.SetActive(true);
                        SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").GetChild(1).gameObject.SetActive(false);
                    }
                    else
                    {
                        SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").GetChild(0).gameObject.SetActive(false);
                        SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").GetChild(1).gameObject.SetActive(true);
                    }
                }
            }
        }
        //else        //코스튬 선택안된상태(입고있는 장비 선택으로 봄)
        //{
        //    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Purchase_Btn").gameObject.SetActive(false);
        //    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").gameObject.SetActive(true);
        //    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(0).gameObject.SetActive(false);
        //    SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinBtn_Panel").Find("Hero_Select_Btn").GetChild(1).gameObject.SetActive(true);
        //}
    }

    private void SkinInfoPanelCheck()
    {
        if (SkinInfoPanel.activeSelf)
        {
            if (SkinCheck != myChar.SelectHero)
            {
                WeaponSkinCnt.Clear();
                CostumeSkinCnt.Clear();
                GameObject SkinWeaponContent = SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinWeapon_Background").Find("Scroll View").GetChild(0).GetChild(0).Find("Skin_Panel").gameObject;
                GameObject SkinCostumeContent = SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinCostume_Background").Find("Scroll View").GetChild(0).GetChild(0).Find("Skin_Panel").gameObject;
                switch (myChar.SelectHero)
                {
                    case 0:
                        SkinArrayCheck(myChar.KnightWeaponSkin, myChar.KnightCostumeSkin);
                        break;
                    case 1:
                        SkinArrayCheck(myChar.WarriorWeaponSkin, myChar.WarriorCostumeSkin);
                        break;
                    case 2:
                        SkinArrayCheck(myChar.ArcherWeaponSkin, myChar.ArcherCostumeSkin);
                        break;
                    case 3:
                        SkinArrayCheck(myChar.WizardWeaponSkin, myChar.WizardCostumeSkin);
                        break;
                    case 4:
                        SkinArrayCheck(myChar.NinjaWeaponSkin, myChar.NinjaCostumeSkin);
                        break;
                }
                SkinCheck = myChar.SelectHero;
            }

            switch (myChar.SelectHero)
            {
                case 0:
                    SkinCostPurchaseCheck(myChar.KnightWeaponSkinPurchase, myChar.KnightCostumeSkinPurchase);
                    break;
                case 1:
                    SkinCostPurchaseCheck(myChar.WarriorWeaponSkinPurchase, myChar.WarriorCostumeSkinPurchase);
                    break;
                case 2:
                    SkinCostPurchaseCheck(myChar.ArcherWeaponSkinPurchase, myChar.ArcherCostumeSkinPurchase);
                    break;
                case 3:
                    SkinCostPurchaseCheck(myChar.WizardWeaponSkinPurchase, myChar.WizardCostumeSkinPurchase);
                    break;
                case 4:
                    SkinCostPurchaseCheck(myChar.NinjaWeaponSkinPurchase, myChar.NinjaCostumeSkinPurchase);
                    break;

            }
            //SkinCostPurchaseCheck(myChar.KnightWeaponSkin, myChar.KnightCostumeSkin, myChar.KnightWeaponSkinPurchase, myChar.KnightCostumeSkinPurchase);
        }
        else
        {
            for (int i = 0; i < myChar.HeroNum; i++)
            {
                myChar.SelectWeapon[i] = -1;
                myChar.SelectCostume[i] = -1;
            }
            HeroUI_SkinText.text = myChar.TextDataMgr.GetTemplate(9203).Content[myChar.LanguageNum].Replace("\\n", "\n");
            for (int i = 0; i < CostumeSkinCnt.Count; i++)
            {
                if (i == myChar.EquipmentCostume[myChar.SelectHero])
                {
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 1);
                }
                else
                {
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 0);
                }
            }
            switch (myChar.SelectHero)
            {
                case 0:
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + myChar.EquipmentWeapon[myChar.SelectHero]);

                    for (int i = 0; i < myChar.KnightCostumeSkin.Length; i++)
                    {
                        if (i == myChar.EquipmentCostume[myChar.SelectHero])
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 1);
                        }
                        else
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 0);
                        }
                    }
                    break;
                case 1:
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + (myChar.EquipmentWeapon[myChar.SelectHero] + 100));
                    for (int i = 0; i < myChar.WarriorCostumeSkin.Length; i++)
                    {
                        if (i == myChar.EquipmentCostume[myChar.SelectHero])
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 1);
                        }
                        else
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 0);
                        }
                    }
                    break;
                case 2:
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + (myChar.EquipmentWeapon[myChar.SelectHero] + 200));
                    for (int i = 0; i < myChar.ArcherCostumeSkin.Length; i++)
                    {
                        if (i == myChar.EquipmentCostume[myChar.SelectHero])
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 1);
                        }
                        else
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 0);
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < myChar.WizardCostumeSkin.Length; i++)
                    {
                        if (i == myChar.EquipmentCostume[myChar.SelectHero])
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 1);
                        }
                        else
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 0);
                        }
                    }
                    for (int i = 0; i < myChar.WizardWeaponSkin.Length; i++)
                    {
                        if (i == myChar.EquipmentWeapon[myChar.SelectHero])
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetChild(i).gameObject.SetActive(true);
                        }
                        else
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetChild(i).gameObject.SetActive(false);
                        }

                    }
                    break;
                case 4:
                    for (int i = 0; i < myChar.NinjaCostumeSkin.Length; i++)
                    {
                        if (i == myChar.EquipmentCostume[myChar.SelectHero])
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 1);
                        }
                        else
                        {
                            HeroCharacterViewport.transform.GetChild(myChar.SelectHero).GetChild(0).GetComponent<Animator>().SetLayerWeight(i, 0);
                        }
                    }
                    HeroCharacterViewport.transform.GetChild(myChar.SelectHero).Find("Weapone").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/07_Skin/01_SkinIcon/" + (myChar.EquipmentWeapon[myChar.SelectHero] + 400));
                    break;
            }
        }
    }

    private void CashShopPanelCheck()
    {
        if (CashShopPanel.activeSelf == true)
        {
            if (!StageCheckTF)
            {
                StageClearCheck = 0;
                for (int i = 0; i < myChar.StageClearCheck.Length; i++)
                {
                    if (myChar.StageClearCheck[i])
                    {
                        StageClearCheck++;
                    }
                }
                StageCheckTF = true;
            }
            

            if (CatStretchCheck)
            {
                CashShopPanel.transform.Find("Botton_Panel").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Cat").GetComponent<Animator>().SetBool("Stretch", true);
            }
            else
            {
                CashShopPanel.transform.Find("Botton_Panel").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Cat").GetComponent<Animator>().SetBool("Stretch", false);
            }
            Btn_UI_Panel.GetComponent<Image>().enabled = true;
            ShopClose_Btn.SetActive(false);
            SelectBar.transform.Find("ShopOn_Btn").gameObject.SetActive(true);

            if (CashShopPanel.transform.Find("Botton_Panel").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Banner_AD").GetChild(0).Find("Index Table").GetChild(0).childCount > 0)
            {
                CashShopPanel.transform.Find("Botton_Panel").GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Banner_AD").GetChild(0).Find("Index Table").GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            }

            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Special_ProductPanel").Find("Product_Name").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(31).Content[myChar.LanguageNum].Replace("\\n", "\n");
            //스페셜팩 구매체크
            if (myChar.SpecialPackCnt > 0)
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Special_ProductPanel").gameObject.SetActive(true);
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Special_ProductPanel").Find("Product_Img").GetChild(0).GetComponentInChildren<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("special_pack").metadata.localizedPriceString);
                //CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Special_ProductPanel").Find("Product_Img").GetChild(0).GetComponentInChildren<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(1).Cost).ToString();
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Special_ProductPanel").Find("Product_Img").Find("Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + myChar.SpecialPackCnt + "/" + 2;

            }
            else if (myChar.SpecialPackCnt <= 0)
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Special_ProductPanel").gameObject.SetActive(false);
            }

            if (myChar.LuxuryPackCnt <= 0 && myChar.StarterPackCnt <= 0 && myChar.SlotPackCnt <= 0)
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").gameObject.SetActive(false);
            }
            else
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").gameObject.SetActive(true);
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Product_Name").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(32).Content[myChar.LanguageNum].Replace("\\n", "\n");
                //럭셔리팩 구매체크
                if (myChar.LuxuryPackCnt > 0)
                {
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_00").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + myChar.LuxuryPackCnt + "/" + 2;
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_00").Find("Purchase_Btn").GetComponentInChildren<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("luxury_pack").metadata.localizedPriceString);
                    //CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_00").Find("Purchase_Btn").GetComponentInChildren<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(3).Cost).ToString();
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_00").Find("Purchase_Btn").gameObject.SetActive(true);
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_00").Find("NotPurchase_Btn").gameObject.SetActive(false);
                }
                else if (myChar.LuxuryPackCnt <= 0)
                {
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_00").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(27).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_00").Find("NotPurchase_Btn").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(30).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_00").Find("Purchase_Btn").gameObject.SetActive(false);
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_00").Find("NotPurchase_Btn").gameObject.SetActive(true);
                }
                //스타터팩 구매체크
                if (myChar.StarterPackCnt > 0)
                {
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_01").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + myChar.StarterPackCnt + "/" + 1;
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_01").Find("Purchase_Btn").GetComponentInChildren<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("starter_pack").metadata.localizedPriceString);
                    //CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_01").Find("Purchase_Btn").GetComponentInChildren<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(5).Cost).ToString();
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_01").Find("Purchase_Btn").gameObject.SetActive(true);
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_01").Find("NotPurchase_Btn").gameObject.SetActive(false);
                }
                else if (myChar.StarterPackCnt <= 0)
                {
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_01").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(27).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_01").Find("NotPurchase_Btn").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(30).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_01").Find("Purchase_Btn").gameObject.SetActive(false);
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_01").Find("NotPurchase_Btn").gameObject.SetActive(true);
                }
                //슬롯팩 구매체크
                if (myChar.SlotPackCnt == 2)
                {
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_02").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + myChar.SlotPackCnt + "/" + 2;
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_02").Find("Purchase_Btn").GetComponentInChildren<Text>().text = CashShopDataMgr.GetTemplate(6).Cost.ToString();
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_02").Find("Purchase_Btn").gameObject.SetActive(true);
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_02").Find("NotPurchase_Btn").gameObject.SetActive(false);
                }
                else if (myChar.SlotPackCnt == 1)
                {
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_02").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + myChar.SlotPackCnt + "/" + 2;
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_02").Find("Purchase_Btn").GetComponentInChildren<Text>().text = CashShopDataMgr.GetTemplate(7).Cost.ToString();
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_02").Find("Purchase_Btn").gameObject.SetActive(true);
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_02").Find("NotPurchase_Btn").gameObject.SetActive(false);
                }
                else if (myChar.SlotPackCnt <= 0)
                {
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_02").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(27).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_02").Find("NotPurchase_Btn").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(30).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_02").Find("Purchase_Btn").gameObject.SetActive(false);
                    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_02").Find("NotPurchase_Btn").gameObject.SetActive(true);
                }
                //광고제거 구매체크
                //if (!myChar.ADClearCheck)
                //{
                //    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_03").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + myChar.AdRemove + "/" + 1;
                //    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_03").Find("Purchase_Btn").GetComponentInChildren<Text>().text = "\\" + CashShopDataMgr.GetTemplate(8).Cost.ToString();
                //    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_03").Find("Purchase_Btn").gameObject.SetActive(true);
                //    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_03").Find("NotPurchase_Btn").gameObject.SetActive(false);
                //}
                //else
                //{
                //    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_03").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(27).Content[myChar.LanguageNum].Replace("\\n", "\n");
                //    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_03").Find("NotPurchase_Btn").GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(30).Content[myChar.LanguageNum].Replace("\\n", "\n");
                //    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_03").Find("Purchase_Btn").gameObject.SetActive(false);
                //    CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Luxury_ProductPanel").Find("Extra_Product_03").Find("NotPurchase_Btn").gameObject.SetActive(true);
                //}
            }
            //일반 구매 버튼 체크
            if (!myChar.ADEquimentCheck)
            {
                ADBox_CoolTime_Btn.SetActive(false);
            }
            else
            {
                ADBox_CoolTime_Btn.SetActive(true);
                System.TimeSpan ADEquiment_conversion_Int = myChar.ADEquimentStartTime - UnbiasedTime.Instance.Now();
                int UntilCompleteTime = System.Convert.ToInt32(ADEquiment_conversion_Int.TotalSeconds);

                int a_hour = (UntilCompleteTime / 3600);
                int a_min = (UntilCompleteTime % 3600 / 60);
                int a_sec = (UntilCompleteTime % 3600 % 60);

                ADBox_CoolTime_Text.text = string.Format("{0:D2}:{1:D2}:{2:D2}", a_hour, a_min, a_sec);
                if (UntilCompleteTime <= 0)
                {
                    myChar.ADEquimentCheck = false;
                }
            }

            MissionBox_Btn.GetComponentInChildren<Text>().text = myChar.MissionCoin.ToString();
            GemBox_Btn_Text.text = CashShopDataMgr.GetTemplate(11).Cost.ToString();
            GemBox_10_Btn_Text.text = CashShopDataMgr.GetTemplate(12).Cost.ToString();

            //AD 재화 구매 버튼 체크
            if (!myChar.ADGemreductionCheck)
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("AD_ProductPanel").Find("Box_Product_00").Find("CoolTime_Btn").gameObject.SetActive(false);
            }
            else
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("AD_ProductPanel").Find("Box_Product_00").Find("CoolTime_Btn").gameObject.SetActive(true);
                System.TimeSpan ADGemTime_conversion_Int = myChar.ADGemStartTime - UnbiasedTime.Instance.Now();
                int UntilCompleteTime = System.Convert.ToInt32(ADGemTime_conversion_Int.TotalSeconds);

                int a_hour = (UntilCompleteTime / 3600);
                int a_min = (UntilCompleteTime % 3600 / 60);
                int a_sec = (UntilCompleteTime % 3600 % 60);

                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("AD_ProductPanel").Find("Box_Product_00").Find("CoolTime_Btn").Find("Timer").GetComponentInChildren<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", a_hour, a_min, a_sec);
                if (UntilCompleteTime <= 0)
                {
                    myChar.ADGemreductionCheck = false;
                }
            }
            if (!myChar.ADSoulreductionCheck)
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("AD_ProductPanel").Find("Box_Product_01").Find("CoolTime_Btn").gameObject.SetActive(false);
            }
            else
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("AD_ProductPanel").Find("Box_Product_01").Find("CoolTime_Btn").gameObject.SetActive(true);
                System.TimeSpan ADSoulTime_conversion_Int = myChar.ADSoulStartTime - UnbiasedTime.Instance.Now();
                int UntilCompleteTime = System.Convert.ToInt32(ADSoulTime_conversion_Int.TotalSeconds);

                int a_hour = (UntilCompleteTime / 3600);
                int a_min = (UntilCompleteTime % 3600 / 60);
                int a_sec = (UntilCompleteTime % 3600 % 60);

                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("AD_ProductPanel").Find("Box_Product_01").Find("CoolTime_Btn").Find("Timer").GetComponentInChildren<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", a_hour, a_min, a_sec);
                if (UntilCompleteTime <= 0)
                {
                    myChar.ADSoulreductionCheck = false;
                }
            }
            if (!myChar.ADHeroreductionCheck)
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("AD_ProductPanel").Find("Box_Product_02").Find("CoolTime_Btn").gameObject.SetActive(false);
            }
            else
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("AD_ProductPanel").Find("Box_Product_02").Find("CoolTime_Btn").gameObject.SetActive(true);
                System.TimeSpan ADHeroTime_conversion_Int = myChar.ADHeroStartTime - UnbiasedTime.Instance.Now();
                int UntilCompleteTime = System.Convert.ToInt32(ADHeroTime_conversion_Int.TotalSeconds);

                int a_hour = (UntilCompleteTime / 3600);
                int a_min = (UntilCompleteTime % 3600 / 60);
                int a_sec = (UntilCompleteTime % 3600 % 60);

                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("AD_ProductPanel").Find("Box_Product_02").Find("CoolTime_Btn").Find("Timer").GetComponentInChildren<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", a_hour, a_min, a_sec);
                if (UntilCompleteTime <= 0)
                {
                    myChar.ADHeroreductionCheck = false;
                }
            }

            if (myChar.Dia_1_FirstCheck <= 0)
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(0).GetChild(1).gameObject.SetActive(true);
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "<color=#FFFFFF>" + ShopDataMgr.GetTemplate(47).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>" + "<color=#00FF00> +" + CashShopDataMgr.GetTemplate(13).Dia + "</color>";
            }
            else
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
            if (myChar.Dia_2_FirstCheck <= 0)
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(1).GetChild(1).gameObject.SetActive(true);
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = "<color=#FFFFFF>" + ShopDataMgr.GetTemplate(47).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>" + "<color=#00FF00> +" + CashShopDataMgr.GetTemplate(14).Dia + "</color>";
            }
            else
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(1).GetChild(1).gameObject.SetActive(false);
            }
            if (myChar.Dia_3_FirstCheck <= 0)
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(2).GetChild(1).gameObject.SetActive(true);
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = "<color=#FFFFFF>" + ShopDataMgr.GetTemplate(47).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>" + "<color=#00FF00> +" + CashShopDataMgr.GetTemplate(15).Dia + "</color>";
            }
            else
            {
                CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(2).GetChild(1).gameObject.SetActive(false);
            }

            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("dia_1").metadata.localizedPriceString);
            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("dia_2").metadata.localizedPriceString);
            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("dia_3").metadata.localizedPriceString);
            //CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(13).Cost).ToString();
            //CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(14).Cost).ToString();
            //CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(15).Cost).ToString();
            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = CashShopDataMgr.GetTemplate(16).Cost.ToString();
            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>().text = CashShopDataMgr.GetTemplate(17).Cost.ToString();
            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(5).GetChild(0).GetChild(0).GetComponent<Text>().text = CashShopDataMgr.GetTemplate(18).Cost.ToString();
            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(6).GetChild(0).GetChild(0).GetComponent<Text>().text = CashShopDataMgr.GetTemplate(19).Cost.ToString();
            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(7).GetChild(0).GetChild(0).GetComponent<Text>().text = CashShopDataMgr.GetTemplate(20).Cost.ToString();
            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(8).GetChild(0).GetChild(0).GetComponent<Text>().text = CashShopDataMgr.GetTemplate(21).Cost.ToString();
            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(9).GetChild(0).GetChild(0).GetComponent<Text>().text = CashShopDataMgr.GetTemplate(25).Cost.ToString();
            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(10).GetChild(0).GetChild(0).GetComponent<Text>().text = CashShopDataMgr.GetTemplate(26).Cost.ToString();
            CashShopPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).Find("Jewelry_ProductPanel").GetChild(11).GetChild(0).GetChild(0).GetComponent<Text>().text = CashShopDataMgr.GetTemplate(27).Cost.ToString();

        }
        else
        {
            Btn_UI_Panel.GetComponent<Image>().enabled = false;
            ShopClose_Btn.SetActive(true);
            SelectBar.transform.Find("ShopOn_Btn").gameObject.SetActive(false);
            StageCheckTF = false;
        }

    }

    private void ADRemoveAndSlotPanelCheck()
    {
        if (ADRemoveAndSlotPanel.activeSelf == true)
        {
            if (!ArrowCheck)
            {
                ADRemoveAndSlotPanel.transform.GetChild(0).GetChild(1).GetChild(0).Find("Content").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                ADRemoveAndSlotPanel.transform.GetChild(0).GetChild(1).Find("Arrow").GetComponent<Image>().enabled = true;
                ArrowCheck = true;
            }

            if (SortCnt == 1)   //ADRemove
            {
                ADRemoveAndSlotPanel.transform.GetChild(0).Find("Icon").GetChild(0).gameObject.SetActive(true);
                ADRemoveAndSlotPanel.transform.GetChild(0).Find("Icon").GetChild(1).gameObject.SetActive(false);

                ADRemoveAndSlotPanel.transform.GetChild(0).Find("Title_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(15).Content[myChar.LanguageNum].Replace("\\n", "\n");
                ADRemoveAndSlotPanel.transform.GetChild(0).GetChild(1).GetChild(0).Find("Content").GetChild(0).GetComponent<Text>().text = ShopDataMgr.GetTemplate(16).Content[myChar.LanguageNum].Replace("\\n", "\n");
                ADRemoveAndSlotPanel.transform.GetChild(0).Find("Purchase_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + myChar.AdRemove + "/" + 1;
                ADRemoveAndSlotPanel.transform.GetChild(0).Find("Cash_Btn").GetChild(0).GetComponent<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(8).Cost).ToString();
                ADRemoveAndSlotPanel.transform.GetChild(0).Find("Cash_Btn").gameObject.SetActive(true);
                ADRemoveAndSlotPanel.transform.GetChild(0).Find("Jem_Btn").gameObject.SetActive(false);
            }
            else if (SortCnt == 2)  //Slot
            {
                ADRemoveAndSlotPanel.transform.GetChild(0).Find("Icon").GetChild(0).gameObject.SetActive(false);
                ADRemoveAndSlotPanel.transform.GetChild(0).Find("Icon").GetChild(1).gameObject.SetActive(true);

                ADRemoveAndSlotPanel.transform.GetChild(0).Find("Purchase_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + myChar.SlotPackCnt + "/" + 2;
                if (myChar.SlotPackCnt == 2)
                {
                    ADRemoveAndSlotPanel.transform.GetChild(0).Find("Title_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    ADRemoveAndSlotPanel.transform.GetChild(0).Find("Jem_Btn").GetChild(0).GetComponent<Text>().text = CashShopDataMgr.GetTemplate(6).Cost.ToString();
                    ADRemoveAndSlotPanel.transform.GetChild(0).GetChild(1).GetChild(0).Find("Content").GetChild(0).GetComponent<Text>().text = ShopDataMgr.GetTemplate(12).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    ADRemoveAndSlotPanel.transform.GetChild(0).Find("Cash_Btn").gameObject.SetActive(false);
                    ADRemoveAndSlotPanel.transform.GetChild(0).Find("Jem_Btn").gameObject.SetActive(true);
                }
                else if (myChar.SlotPackCnt == 1)
                {
                    ADRemoveAndSlotPanel.transform.GetChild(0).Find("Title_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    ADRemoveAndSlotPanel.transform.GetChild(0).Find("Jem_Btn").GetChild(0).GetComponent<Text>().text = CashShopDataMgr.GetTemplate(7).Cost.ToString();
                    ADRemoveAndSlotPanel.transform.GetChild(0).GetChild(1).GetChild(0).Find("Content").GetChild(0).GetComponent<Text>().text = ShopDataMgr.GetTemplate(14).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    ADRemoveAndSlotPanel.transform.GetChild(0).Find("Cash_Btn").gameObject.SetActive(false);
                    ADRemoveAndSlotPanel.transform.GetChild(0).Find("Jem_Btn").gameObject.SetActive(true);
                }

            }
        }
    }
    private void CashShopInfoPanelCheck()
    {
        if (CashShopInfoPanel.activeSelf == true)
        {
            if (!ArrowCheck)
            {
                CashShopInfoPanel.transform.GetChild(0).GetChild(1).GetChild(0).Find("Content").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                CashShopInfoPanel.transform.GetChild(0).GetChild(1).Find("Arrow").GetComponent<Image>().enabled = true;
                ArrowCheck = true;
            }
        }
        if (SortCnt == 3)   //ItemBoxInfo
        {
            CashShopInfoPanel.transform.GetChild(0).Find("Icon").GetChild(0).gameObject.SetActive(true);
            CashShopInfoPanel.transform.GetChild(0).Find("Icon").GetChild(1).gameObject.SetActive(false);

            CashShopInfoPanel.transform.GetChild(0).Find("Title_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(17).Content[myChar.LanguageNum].Replace("\\n", "\n");
            CashShopInfoPanel.transform.GetChild(0).GetChild(1).GetChild(0).Find("Content").GetChild(0).GetComponent<Text>().text = ShopDataMgr.GetTemplate(18).Content[myChar.LanguageNum].Replace("\\n", "\n") + ShopDataMgr.GetTemplate(19).Content[myChar.LanguageNum].Replace("\\n", "\n") + ShopDataMgr.GetTemplate(20).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
        else if (SortCnt == 4)      //CosInfo
        {
            CashShopInfoPanel.transform.GetChild(0).Find("Icon").GetChild(0).gameObject.SetActive(false);
            CashShopInfoPanel.transform.GetChild(0).Find("Icon").GetChild(1).gameObject.SetActive(true);

            CashShopInfoPanel.transform.GetChild(0).Find("Title_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(21).Content[myChar.LanguageNum].Replace("\\n", "\n");
            CashShopInfoPanel.transform.GetChild(0).GetChild(1).GetChild(0).Find("Content").GetChild(0).GetComponent<Text>().text = ShopDataMgr.GetTemplate(22).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
        else if (SortCnt == 5)      //ADInfo
        {
            CashShopInfoPanel.transform.GetChild(0).Find("Icon").GetChild(0).gameObject.SetActive(false);
            CashShopInfoPanel.transform.GetChild(0).Find("Icon").GetChild(1).gameObject.SetActive(true);

            CashShopInfoPanel.transform.GetChild(0).Find("Title_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(21).Content[myChar.LanguageNum].Replace("\\n", "\n");
            CashShopInfoPanel.transform.GetChild(0).GetChild(1).GetChild(0).Find("Content").GetChild(0).GetComponent<Text>().text = ShopDataMgr.GetTemplate(42).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
    }
    public void SortCntCheck(int Cnt)
    {
        SortCnt = Cnt;
    }
    private void BasicProductPanelCheck()
    {
        if (BasicProductPanel.activeSelf == true)
        {
            BasicProductPanel.transform.GetChild(0).Find("Title_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(28).Content[myChar.LanguageNum].Replace("\\n", "\n");
            BasicProductPanel.transform.GetChild(0).Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/03_CashShop/" + BasicProductCnt);

            switch (BasicProductCnt)
            {
                case 0:
                    BasicProductPanelFrame("Jem", CashShopDataMgr.GetTemplate(13).Dia, "<color=#1DF0D8>", 0, true, false, new Vector2(-90, 5));
                    BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(0).GetComponentInChildren<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("dia_1").metadata.localizedPriceString);
                    //BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(0).GetComponentInChildren<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(13).Cost).ToString();
                    if (myChar.Dia_1_FirstCheck <= 0)
                    {
                        BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(true);
                        BasicProductPanel.transform.GetChild(0).Find("First").GetComponentInChildren<Text>().text = "<color=#FFFFFF>" + ShopDataMgr.GetTemplate(47).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>" + "<color=#00FF00> +" + CashShopDataMgr.GetTemplate(13).Dia + "</color>";
                    }
                    else
                    {
                        BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    BasicProductPanelFrame("Jem", CashShopDataMgr.GetTemplate(14).Dia, "<color=#1DF0D8>", 0, true, false, new Vector2(-115, 5));
                    BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(0).GetComponentInChildren<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("dia_2").metadata.localizedPriceString);
                    //BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(0).GetComponentInChildren<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(14).Cost).ToString();
                    if (myChar.Dia_2_FirstCheck <= 0)
                    {
                        BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(true);
                        BasicProductPanel.transform.GetChild(0).Find("First").GetComponentInChildren<Text>().text = "<color=#FFFFFF>" + ShopDataMgr.GetTemplate(47).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>" + "<color=#00FF00> +" + CashShopDataMgr.GetTemplate(14).Dia + "</color>";
                    }
                    else
                    {
                        BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    BasicProductPanelFrame("Jem", CashShopDataMgr.GetTemplate(15).Dia, "<color=#1DF0D8>", 0, true, false, new Vector2(-115, 5));
                    BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(0).GetComponentInChildren<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("dia_3").metadata.localizedPriceString);
                    //BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(0).GetComponentInChildren<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(15).Cost).ToString();
                    if (myChar.Dia_3_FirstCheck <= 0)
                    {
                        BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(true);
                        BasicProductPanel.transform.GetChild(0).Find("First").GetComponentInChildren<Text>().text = "<color=#FFFFFF>" + ShopDataMgr.GetTemplate(47).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>" + "<color=#00FF00> +" + CashShopDataMgr.GetTemplate(15).Dia + "</color>";
                    }
                    else
                    {
                        BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    BasicProductPanelFrame("Soul", CashShopDataMgr.GetTemplate(16).Soul, "<color=#ED3E0B>", 1, false, true, new Vector2(-90, 5));
                    BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(1).GetComponentInChildren<Text>().text = CashShopDataMgr.GetTemplate(16).Cost.ToString();
                    BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(false);
                    GemPurchaseNum = 0;
                    break;
                case 4:
                    BasicProductPanelFrame("Soul", CashShopDataMgr.GetTemplate(17).Soul, "<color=#ED3E0B>", 1, false, true, new Vector2(-90, 5));
                    BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(1).GetComponentInChildren<Text>().text = CashShopDataMgr.GetTemplate(17).Cost.ToString();
                    BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(false);
                    GemPurchaseNum = 1;
                    break;
                case 5:
                    BasicProductPanelFrame("Soul", CashShopDataMgr.GetTemplate(18).Soul, "<color=#ED3E0B>", 1, false, true, new Vector2(-115, 5));
                    BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(1).GetComponentInChildren<Text>().text = CashShopDataMgr.GetTemplate(18).Cost.ToString();
                    BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(false);
                    GemPurchaseNum = 2;
                    break;
                case 6:
                    BasicProductPanelFrame("Hero", CashShopDataMgr.GetTemplate(1000 + ((StageClearCheck - 1) * 3)).HeroToken, "<color=#F0BD24>", 1, false, true, new Vector2(-90, 5));
                    BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(1).GetComponentInChildren<Text>().text = CashShopDataMgr.GetTemplate(19).Cost.ToString();
                    BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(false);
                    GemPurchaseNum = 3;
                    break;
                case 7:
                    BasicProductPanelFrame("Hero", CashShopDataMgr.GetTemplate(1001 + ((StageClearCheck - 1) * 3)).HeroToken, "<color=#F0BD24>", 1, false, true, new Vector2(-115, 5));
                    BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(1).GetComponentInChildren<Text>().text = CashShopDataMgr.GetTemplate(20).Cost.ToString();
                    BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(false);
                    GemPurchaseNum = 4;
                    break;
                case 8:
                    BasicProductPanelFrame("Hero", CashShopDataMgr.GetTemplate(1002 + ((StageClearCheck - 1) * 3)).HeroToken, "<color=#F0BD24>", 1, false, true, new Vector2(-115, 5));
                    BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(1).GetComponentInChildren<Text>().text = CashShopDataMgr.GetTemplate(21).Cost.ToString();
                    BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(false);
                    GemPurchaseNum = 5;
                    break;
                case 9:
                    BasicProductPanelFrame("Key", CashShopDataMgr.GetTemplate(25).Key, "<color=#FFCC33>", 1, false, true, new Vector2(-90, 5));
                    BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(1).GetComponentInChildren<Text>().text = CashShopDataMgr.GetTemplate(25).Cost.ToString();
                    BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(false);
                    GemPurchaseNum = 6;
                    break;
                case 10:
                    BasicProductPanelFrame("Key", CashShopDataMgr.GetTemplate(26).Key, "<color=#FFCC33>", 1, false, true, new Vector2(-115, 5));
                    BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(1).GetComponentInChildren<Text>().text = CashShopDataMgr.GetTemplate(26).Cost.ToString();
                    BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(false);
                    GemPurchaseNum = 7;
                    break;
                case 11:
                    BasicProductPanelFrame("Key", CashShopDataMgr.GetTemplate(27).Key, "<color=#FFCC33>", 1, false, true, new Vector2(-115, 5));
                    BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(1).GetComponentInChildren<Text>().text = CashShopDataMgr.GetTemplate(27).Cost.ToString();
                    BasicProductPanel.transform.GetChild(0).Find("First").gameObject.SetActive(false);
                    GemPurchaseNum = 8;
                    break;
            }
        }
    }
    public void BasicProductCntCheck(int Cnt)
    {
        BasicProductCnt = Cnt;
    }
    private void BasicProductPanelFrame(string ImgName, int RewardCnt, string FontColor, int BackBoardCnt, bool ZeroTF, bool OneTF, Vector2 iconPos)
    {
        BasicProductPanel.transform.GetChild(0).Find("Cost_Background").GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/04_CashShopIcon/" + ImgName);
        BasicProductPanel.transform.GetChild(0).Find("Cost_Background").GetChild(0).GetComponentInChildren<Text>().text = FontColor + "x" + RewardCnt + "</color>";
        BasicProductPanel.transform.GetChild(0).Find("Cost_Background").GetChild(0).GetComponent<RectTransform>().anchoredPosition = iconPos;
        BasicProductPanel.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/04_CashShopIcon/" + BackBoardCnt);
        BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(0).gameObject.SetActive(ZeroTF);
        BasicProductPanel.transform.GetChild(0).Find("BtnPanel").GetChild(1).gameObject.SetActive(OneTF);

        if (BasicProductCnt != 6  && BasicProductCnt != 7 && BasicProductCnt != 8)
        {
            BasicProductPanel.transform.GetChild(0).Find("Cost_Background").GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            BasicProductPanel.transform.GetChild(0).Find("Cost_Background").GetChild(1).gameObject.SetActive(true);
            if (BasicProductCnt == 6)
            {
                BasicProductPanel.transform.GetChild(0).Find("Cost_Background").GetChild(1).GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(44).Content[myChar.LanguageNum].Replace("\\n", "\n");
            }
            else if (BasicProductCnt == 7)
            {
                BasicProductPanel.transform.GetChild(0).Find("Cost_Background").GetChild(1).GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(45).Content[myChar.LanguageNum].Replace("\\n", "\n");
            }
            else if (BasicProductCnt == 8)
            {
                BasicProductPanel.transform.GetChild(0).Find("Cost_Background").GetChild(1).GetComponentInChildren<Text>().text = ShopDataMgr.GetTemplate(46).Content[myChar.LanguageNum].Replace("\\n", "\n");
            }
        }
    }
    //스페셜팩구매창
    private void SpecialPackPanelCheck()
    {
        if (SpecialPackPanel.activeSelf == true)
        {
            if (myChar.SpecialPackCnt == 2)             //스페셜 팩 1
            {
                SpecialPackPanel.transform.GetChild(0).Find("Title_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n");
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Jem_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(1).Dia;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Soul_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(1).Soul;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Heart_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(1).HeroToken;
                //SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("SlotCoin_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(1).SlotCoin;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Key_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(1).Key;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Skin_Img").GetChild(0).GetComponent<Text>().text = "x" + 2;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Skin_Img").GetChild(1).GetComponent<Text>().text = ShopDataMgr.GetTemplate(40).Content[myChar.LanguageNum].Replace("\\n", "\n");
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Equipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 3;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Equipment_Img").GetChild(1).GetComponent<Text>().text = ShopDataMgr.GetTemplate(39).Content[myChar.LanguageNum].Replace("\\n", "\n");
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("RandomEquipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 7;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("RandomEquipment_Img").GetChild(1).GetComponent<Text>().text = ShopDataMgr.GetTemplate(41).Content[myChar.LanguageNum].Replace("\\n", "\n");
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n");
                SpecialPackPanel.transform.GetChild(0).Find("Cnt_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + myChar.SpecialPackCnt + "/" + 2;
                SpecialPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("special_pack").metadata.localizedPriceString);
                //SpecialPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(1).Cost).ToString();
            }
            else if (myChar.SpecialPackCnt == 1)        //스페셜 팩 2
            {
                SpecialPackPanel.transform.GetChild(0).Find("Title_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n");
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Jem_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(2).Dia;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Soul_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(2).Soul;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Heart_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(2).HeroToken;
                //SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("SlotCoin_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(2).SlotCoin;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Key_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(2).Key;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Skin_Img").GetChild(0).GetComponent<Text>().text = "x" + 2;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Skin_Img").GetChild(1).GetComponent<Text>().text = ShopDataMgr.GetTemplate(40).Content[myChar.LanguageNum].Replace("\\n", "\n");
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Equipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 3;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Equipment_Img").GetChild(1).GetComponent<Text>().text = ShopDataMgr.GetTemplate(39).Content[myChar.LanguageNum].Replace("\\n", "\n");
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("RandomEquipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 7;
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("RandomEquipment_Img").GetChild(1).GetComponent<Text>().text = ShopDataMgr.GetTemplate(41).Content[myChar.LanguageNum].Replace("\\n", "\n");
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(4).Content[myChar.LanguageNum].Replace("\\n", "\n");
                SpecialPackPanel.transform.GetChild(0).Find("Cnt_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + myChar.SpecialPackCnt + "/" + 2;
                SpecialPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("special_pack").metadata.localizedPriceString);
                //SpecialPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(2).Cost).ToString();
            }
            else
            {
                SpecialPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(27).Content[myChar.LanguageNum].Replace("\\n", "\n");
                SpecialPackPanel.transform.GetChild(0).Find("Cnt_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(30).Content[myChar.LanguageNum].Replace("\\n", "\n");
                SpecialPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = ShopDataMgr.GetTemplate(30).Content[myChar.LanguageNum].Replace("\\n", "\n");
            }
        }
    }
    //럭셔리팩 구매창
    private void LuxuryPackPanelCheck()
    {
        if (LuxuryPackPanel.activeSelf == true)
        {
            if (myChar.LuxuryPackCnt == 2)      //럭셔리 팩 1
            {
                LuxuryPackPanel.transform.GetChild(0).Find("Title_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(5).Content[myChar.LanguageNum].Replace("\\n", "\n");
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Jem_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(3).Dia;
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Soul_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(3).Soul;
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Heart_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(3).HeroToken;
                //LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("SlotCoin_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(3).SlotCoin;
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Key_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(3).Key;
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Equipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 1;
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Equipment_Img").GetChild(1).GetComponent<Text>().text = ShopDataMgr.GetTemplate(39).Content[myChar.LanguageNum].Replace("\\n", "\n");
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("RandomEquipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 5;
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("RandomEquipment_Img").GetChild(1).GetComponent<Text>().text = ShopDataMgr.GetTemplate(41).Content[myChar.LanguageNum].Replace("\\n", "\n");
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n");
                LuxuryPackPanel.transform.GetChild(0).Find("Cnt_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + 2 + " / " + myChar.LuxuryPackCnt;
                LuxuryPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("luxury_pack").metadata.localizedPriceString);
                //LuxuryPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(3).Cost).ToString();
            }
            else if (myChar.LuxuryPackCnt == 1)     //럭셔리 팩 2
            {
                LuxuryPackPanel.transform.GetChild(0).Find("Title_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(7).Content[myChar.LanguageNum].Replace("\\n", "\n");
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Jem_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(4).Dia;
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Soul_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(4).Soul;
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Heart_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(4).HeroToken;
                //LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("SlotCoin_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(4).SlotCoin;
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Key_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(4).Key;
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Equipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 1;
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Equipment_Img").GetChild(1).GetComponent<Text>().text = ShopDataMgr.GetTemplate(39).Content[myChar.LanguageNum].Replace("\\n", "\n");
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("RandomEquipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 5;
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("RandomEquipment_Img").GetChild(1).GetComponent<Text>().text = ShopDataMgr.GetTemplate(41).Content[myChar.LanguageNum].Replace("\\n", "\n");
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(8).Content[myChar.LanguageNum].Replace("\\n", "\n");
                LuxuryPackPanel.transform.GetChild(0).Find("Cnt_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + 2 + " / " + myChar.LuxuryPackCnt;
                LuxuryPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("luxury_pack").metadata.localizedPriceString);
                //LuxuryPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(4).Cost).ToString();
            }
            else
            {
                LuxuryPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(27).Content[myChar.LanguageNum].Replace("\\n", "\n");
                LuxuryPackPanel.transform.GetChild(0).Find("Cnt_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(30).Content[myChar.LanguageNum].Replace("\\n", "\n");
                LuxuryPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = ShopDataMgr.GetTemplate(30).Content[myChar.LanguageNum].Replace("\\n", "\n");
            }
        }
    }
    //스타터팩 구매창
    private void StarterPackPanelCheck()
    {
        if (StaterPackPanel.activeSelf == true)
        {
            if (myChar.StarterPackCnt == 1)
            {
                StaterPackPanel.transform.GetChild(0).Find("Title_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(9).Content[myChar.LanguageNum].Replace("\\n", "\n");
                StaterPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Jem_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(5).Dia;
                StaterPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Soul_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(5).Soul;
                StaterPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Heart_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(5).HeroToken;
                //StaterPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("SlotCoin_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(5).SlotCoin;
                StaterPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Key_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(5).Key;
                StaterPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Equipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 5;
                StaterPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Equipment_Img").GetChild(1).GetComponent<Text>().text = ShopDataMgr.GetTemplate(37).Content[myChar.LanguageNum].Replace("\\n", "\n");
                StaterPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(10).Content[myChar.LanguageNum].Replace("\\n", "\n");
                StaterPackPanel.transform.GetChild(0).Find("Cnt_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(29).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + 1 + " / " + 1;
                StaterPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = string.Format("{0}", IAPManager.Instance.GetProduct("starter_pack").metadata.localizedPriceString);
                //StaterPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = "₩" + Comma(CashShopDataMgr.GetTemplate(5).Cost).ToString();
            }
            else
            {
                StaterPackPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(27).Content[myChar.LanguageNum].Replace("\\n", "\n");
                StaterPackPanel.transform.GetChild(0).Find("Cnt_Text").GetComponent<Text>().text = ShopDataMgr.GetTemplate(30).Content[myChar.LanguageNum].Replace("\\n", "\n");
                StaterPackPanel.transform.GetChild(0).Find("Price_Btn").GetChild(0).GetComponent<Text>().text = ShopDataMgr.GetTemplate(30).Content[myChar.LanguageNum].Replace("\\n", "\n");
            }
        }
    }


    public void ArrowCloseBtn()
    {
        ArrowCheck = false;
    }
    
    //private void ItemInfoGrade(int Equipment)
    //{
    //아이템 정보창 업그레이드 수치체크
    //GameObject Grade = ItemInfo_Panel.transform.GetChild(0).Find("Grade_Panel").gameObject;

    //    switch (Equipment)
    //    {
    //        case 0:
    //            Grade.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    //            Grade.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
    //            Grade.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
    //            Grade.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    //            Grade.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
    //            Grade.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
    //            break;
    //        case 1:
    //            Grade.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
    //            Grade.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
    //            Grade.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    //            Grade.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
    //            Grade.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
    //            break;
    //        case 2:
    //            Grade.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
    //            Grade.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    //            Grade.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
    //            Grade.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
    //            break;
    //        case 3:
    //            Grade.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    //            Grade.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
    //            Grade.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
    //            break;
    //        case 4:
    //            Grade.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    //            Grade.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
    //            Grade.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
    //            break;
    //        case 5:
    //            Grade.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    //            Grade.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
    //            Grade.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
    //            break;
    //        case 6:
    //            Grade.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
    //            Grade.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    //            Grade.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
    //            Grade.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
    //            break;
    //    }
    //}

    //private void PreferencesItemState(int EquipmentCnt)
    //{
    //    if (EquipmentCnt < 1000)
    //    {
    //        GameObject StatePanel = ItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
    //        switch (myChar.EquipmentAll[EquipmentCnt])
    //        {
    //            case 0:
    //                ItemAtk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk0;
    //                ItemASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD0;
    //                ItemRange = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range0;
    //                ItemHP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP0;
    //                break;
    //            case 1:
    //                ItemAtk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk1;
    //                ItemASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD1;
    //                ItemRange = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range1;
    //                ItemHP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP1;
    //                break;
    //            case 2:
    //                ItemAtk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk2;
    //                ItemASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD2;
    //                ItemRange = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range2;
    //                ItemHP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP2;
    //                break;
    //            case 3:
    //                ItemAtk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk3;
    //                ItemASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD3;
    //                ItemRange = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range3;
    //                ItemHP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP3;
    //                break;
    //            case 4:
    //                ItemAtk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk4;
    //                ItemASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD4;
    //                ItemRange = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range4;
    //                ItemHP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP4;
    //                break;
    //            case 5:
    //                ItemAtk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk5;
    //                ItemASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD5;
    //                ItemRange = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range5;
    //                ItemHP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP5;
    //                break;
    //            case 6:
    //                ItemAtk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk6;
    //                ItemASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD6;
    //                ItemRange = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range6;
    //                ItemHP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP6;
    //                break;
    //        }

    //        StatePanel.transform.Find("ItemAtk").GetComponentInChildren<Text>().text = "Lv. " + ItemAtk.ToString();
    //        StatePanel.transform.Find("ItemASPD").GetComponentInChildren<Text>().text = "Lv. " + ItemASPD.ToString();
    //        StatePanel.transform.Find("ItemRange").GetComponentInChildren<Text>().text = "Lv. " + ItemRange.ToString();
    //        StatePanel.transform.Find("ItemHP").GetComponentInChildren<Text>().text = "Lv. " + ItemHP.ToString();

    //        for (int i = 0; i < 10; i++)
    //        {
    //            if (i < ItemAtk)
    //            {
    //                ItemAtkGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                ItemAtkGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //            }

    //            if (i < ItemASPD)
    //            {
    //                ItemASPDGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                ItemASPDGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //            }

    //            if (i < ItemRange)
    //            {
    //                ItemRangeGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                ItemRangeGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //            }

    //            if (i < ItemHP)
    //            {
    //                ItemHPGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                ItemHPGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            ItemAtkGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //            ItemASPDGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //            ItemRangeGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //            ItemHPGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //        }
    //        GameObject StatePanel = ItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;

    //        StatePanel.transform.Find("ItemAtk").GetComponentInChildren<Text>().text = "Lv. -";
    //        StatePanel.transform.Find("ItemASPD").GetComponentInChildren<Text>().text = "Lv. -";
    //        StatePanel.transform.Find("ItemRange").GetComponentInChildren<Text>().text = "Lv. -";
    //        StatePanel.transform.Find("ItemHP").GetComponentInChildren<Text>().text = "Lv. -";
    //    }
    //}
    //private void PreferencesDontItemState()
    //{
    //    GameObject StatePanel = ItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
    //    ItemAtk = 0;
    //    ItemASPD = 0;
    //    ItemRange = 0;
    //    ItemHP = 0;

    //    StatePanel.transform.Find("ItemAtk").GetComponentInChildren<Text>().text = "Lv. " + ItemAtk.ToString();
    //    StatePanel.transform.Find("ItemASPD").GetComponentInChildren<Text>().text = "Lv. " + ItemASPD.ToString();
    //    StatePanel.transform.Find("ItemRange").GetComponentInChildren<Text>().text = "Lv. " + ItemRange.ToString();
    //    StatePanel.transform.Find("ItemHP").GetComponentInChildren<Text>().text = "Lv. " + ItemHP.ToString();

    //    for (int i = 0; i < 10; i++)
    //    {
    //        if (i < ItemAtk)
    //        {
    //            ItemAtkGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            ItemAtkGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //        }

    //        if (i < ItemASPD)
    //        {
    //            ItemASPDGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            ItemASPDGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //        }
    //        if (i < ItemRange)
    //        {
    //            ItemRangeGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            ItemRangeGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //        }
    //        if (i < ItemHP)
    //        {
    //            ItemHPGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            ItemHPGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //        }
    //    }
    //}

    public void HeroBackPanelCheck()
    {
        for (int i = 0; i < myChar.HeroLv.Length; i++)
        {
            if (i == myChar.SelectHero)
            {
                HeroBackPanel[i].GetComponent<Image>().sprite = HeroBackPanelActive[i];
            }
            else
            {
                HeroBackPanel[i].GetComponent<Image>().sprite = HeroBackPanelNoneActive[i];
            }
        }
    }

    public void ReGacha_Btn()
    {
        if (BoxNo == 11)
        {
            if (myChar.Gem > CashShopDataMgr.GetTemplate(11).Cost)
            {
                PackageCheck = false;

                Box1_Index = myChar.GachaBox;
                //Box2_Index = 10;    
                ItemCnt = 1;
                BoxNo = 11;

                GachaPanel.SetActive(true);
                GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
                GachaChoice();
                myChar.Gem -= CashShopDataMgr.GetTemplate(11).Cost;

                myChar.SaveGem();
                FirebaseManager.firebaseManager.ShopProductPurchase("dia_Box");
            }
        }
        else if (BoxNo == 12)
        {
            if (myChar.Gem > CashShopDataMgr.GetTemplate(12).Cost)
            {
                PackageCheck = false;

                Box1_Index = myChar.GachaBox;
                Box2_Index = 10;
                ItemCnt = 10;
                BoxNo = 12;

                GachaPanel.SetActive(true);
                GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
                GachaChoice();
                myChar.Gem -= CashShopDataMgr.GetTemplate(12).Cost;

                myChar.SaveGem();
                FirebaseManager.firebaseManager.ShopProductPurchase("dia_10_Box");
            }
        }
    }

    private void GachaPanelCheck()
    {
        if (GachaPanel.activeSelf)
        {
            if (CardOpenCnt != CardOpenCheckList.Count)          //카드뽑기창
            {
                GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("AllOpen_Btn").gameObject.SetActive(true);
                GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("RePurchase_Btn").gameObject.SetActive(false);
                GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("NotRePurchase_Btn").gameObject.SetActive(false);
                GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("Close_Btn").gameObject.SetActive(false);
                GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("BlackSmithClose_Btn").gameObject.SetActive(false);

                for (int i = 0; i < CardOpenCheckList.Count; i++)
                {
                    if (i == 0)
                    {
                        CardOpenCnt = 0;
                    }
                    if (CardOpenCheckList[i] == true)
                    {
                        CardOpenCnt++;
                    }
                }
            }
            else            //패키치창
            {
                GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("AllOpen_Btn").gameObject.SetActive(false);
                if (BoxNo == 11)
                {
                    if (myChar.Gem >= CashShopDataMgr.GetTemplate(11).Cost)
                    {
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("Close_Btn").gameObject.SetActive(true);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("RePurchase_Btn").gameObject.SetActive(true);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("NotRePurchase_Btn").gameObject.SetActive(false);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("RePurchase_Btn").Find("Text").GetComponent<Text>().text = CashShopDataMgr.GetTemplate(11).Cost.ToString();
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("RePurchase_Btn").GetChild(1).Find("Text").GetComponent<Text>().text = "재구매하기";
                    }
                    else
                    {
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("Close_Btn").gameObject.SetActive(true);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("RePurchase_Btn").gameObject.SetActive(false);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("NotRePurchase_Btn").gameObject.SetActive(true);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("NotRePurchase_Btn").Find("Text").GetComponent<Text>().text = CashShopDataMgr.GetTemplate(11).Cost.ToString();
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("NotRePurchase_Btn").GetChild(1).Find("Text").GetComponent<Text>().text = "재구매하기";
                    }

                }
                else if (BoxNo == 12)
                {
                    if (myChar.Gem >= CashShopDataMgr.GetTemplate(12).Cost)
                    {
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("Close_Btn").gameObject.SetActive(true);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("RePurchase_Btn").gameObject.SetActive(true);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("NotRePurchase_Btn").gameObject.SetActive(false);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("RePurchase_Btn").Find("Text").GetComponent<Text>().text = CashShopDataMgr.GetTemplate(12).Cost.ToString();
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("RePurchase_Btn").GetChild(1).Find("Text").GetComponent<Text>().text = "재구매하기";
                    }
                    else
                    {
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("Close_Btn").gameObject.SetActive(true);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("RePurchase_Btn").gameObject.SetActive(false);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("NotRePurchase_Btn").gameObject.SetActive(true);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("NotRePurchase_Btn").Find("Text").GetComponent<Text>().text = CashShopDataMgr.GetTemplate(12).Cost.ToString();
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("NotRePurchase_Btn").GetChild(1).Find("Text").GetComponent<Text>().text = "재구매하기";
                    }
                }
                else        //패키지
                {
                    if (BoxNo == 0) //0번은 대장간클릭했을때 들어가게 해둠
                    {
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("Close_Btn").gameObject.SetActive(false);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("BlackSmithClose_Btn").gameObject.SetActive(true);
                    }
                    else
                    {
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("Close_Btn").gameObject.SetActive(true);
                        GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("BlackSmithClose_Btn").gameObject.SetActive(false);
                    }

                    GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("RePurchase_Btn").gameObject.SetActive(false);
                    GachaPanel.transform.GetChild(0).Find("Card_Panel").Find("NotRePurchase_Btn").gameObject.SetActive(false);
                }
            }
            //Transform CardParent = GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0);
            switch (ItemCnt)
            {
                case 1:
                    CardParent[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
                    break;
                case 2:
                    CardParent[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 100);
                    CardParent[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 100);
                    break;
                case 3:
                    CardParent[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 300);
                    CardParent[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 300);
                    CardParent[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150);
                    break;
                case 4:
                    CardParent[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 300);
                    CardParent[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 300);
                    CardParent[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, -150);
                    CardParent[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(200, -150);
                    break;
                case 5:
                    CardParent[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, 350);
                    CardParent[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, 350);
                    CardParent[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, -200);
                    CardParent[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, -200);
                    CardParent[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-0, 100);
                    break;
                case 6:
                    CardParent[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, 375);
                    CardParent[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, 375);
                    CardParent[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, -25);
                    CardParent[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, -25);
                    CardParent[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 450);
                    CardParent[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100);
                    break;
                case 7:
                    CardParent[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, 350);
                    CardParent[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, 350);
                    CardParent[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, -200);
                    CardParent[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, -200);
                    CardParent[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 550);
                    CardParent[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
                    CardParent[6].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -350);
                    break;
                case 8:
                    CardParent[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, 450);
                    CardParent[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, 450);
                    CardParent[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, -350);
                    CardParent[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, -350);
                    CardParent[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 550);
                    CardParent[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 50);
                    CardParent[6].GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 50);
                    CardParent[7].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -450);
                    break;
                case 9:
                    CardParent[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 550);
                    CardParent[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, 450);
                    CardParent[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, 450);
                    CardParent[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 50);
                    CardParent[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, 50);
                    CardParent[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, 50);
                    CardParent[6].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -450);
                    CardParent[7].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, -350);
                    CardParent[8].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, -350);
                    break;
                case 10:
                    CardParent[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 650);
                    CardParent[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, 450);
                    CardParent[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, 450);
                    CardParent[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 250);
                    CardParent[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, 50);
                    CardParent[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, 50);
                    CardParent[6].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150);
                    CardParent[7].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, -350);
                    CardParent[8].GetComponent<RectTransform>().anchoredPosition = new Vector2(350, -350);
                    CardParent[9].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -550);
                    break;
            }
        }
    }
    //패키지 뽑기
    public void Package_Purchase(int PackageNum)
    {
        if (GachaPanel.activeSelf)
        {
            if (PackageCheck)
            {
                if (!PackagePurchaseCheck)
                {
                    Gacha_PackPanel.SetActive(true);
                    AD_Box.SetActive(false);
                    Gem_Box.SetActive(false);
                    Play_Box.SetActive(false);
                    Card_Panel.SetActive(false);
                    SkinCard_Panel.SetActive(false);

                    if (PackageNum == 0)
                    {
                        if (SkinCard[0].transform.childCount >= 1)
                        {
                            Destroy(SkinCard[0].transform.GetChild(0).gameObject);
                        }
                        if (SkinCard[1].transform.childCount >= 1)
                        {
                            Destroy(SkinCard[1].transform.GetChild(0).gameObject);
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            GameObject Card = Instantiate(GachaCard);
                            Card.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            Card.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/05_CardBoard/" + 2);
                            Card.transform.parent = SkinCard[i].transform;
                            Card.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                            Card.transform.GetComponent<GachaCardScript>().ItemCheck = true;
                            Card.transform.GetComponent<GachaCardScript>().OpenCheck = true;
                        }
                    }


                    if (PackageNum == 0)
                    {
                        myChar.SpecialPackCnt--;
                        myChar.SaveSpecialPackCnt();
                    }
                    else if (PackageNum == 1)
                    {
                        myChar.LuxuryPackCnt--;
                        myChar.SaveLuxuryPackCnt();
                    }
                    else if (PackageNum == 2)
                    {
                        myChar.StarterPackCnt--;
                        myChar.SaveStarterPackCnt();
                    }
                    PackagePurchaseCheck = true;
                }
                if (PackageNum == 0)    // 0 = SpecialPack
                {
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").Find("BackGround").GetComponent<PackOpenScript>().CardPanelCheck = true;
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("Pack_Img").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/04_CashShopIcon/SpecialPack");
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").gameObject.SetActive(true);
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").gameObject.SetActive(false);
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("StarterPack").gameObject.SetActive(false);
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").Find("Background").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -350);

                    Box1_Index = myChar.GachaBox;
                    //Box2_Index = 1;
                    ItemCnt = 10;

                    if (myChar.SpecialPackCnt == 1)
                    {
                        BoxNo = 1;

                        if (!SkinCollocateCheck)
                        {
                            SkinCardOpenCheck = false;
                            CardOpenCheckList.Clear();
                            for (int i = 0; i < 2; i++)
                            {
                                CardOpenCheckList.Add(false);
                            }
                            SkinCollocateCheck = true;
                        }
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("Jem_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(1).Dia;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("Soul_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(1).Soul;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("Heart_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(1).HeroToken;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("SlotCoin_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(1).SlotCoin;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("Key_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(1).Key;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("Skin_Img").GetChild(0).GetComponent<Text>().text = "x" + 2;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("Equipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 3;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("RandomEquipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 7;
                        SkinCard[0].transform.GetChild(0).Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/04_CashShopIcon/Knight_Skin");
                        SkinCard[0].transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        SkinCard[1].transform.GetChild(0).Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/04_CashShopIcon/Knight_Weapon");
                        SkinCard[1].transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    }
                    else if (myChar.SpecialPackCnt == 0)
                    {
                        BoxNo = 2;
                        if (!SkinCollocateCheck)
                        {
                            SkinCardOpenCheck = false;
                            CardOpenCheckList.Clear();
                            for (int i = 0; i < 2; i++)
                            {
                                CardOpenCheckList.Add(false);
                            }
                            SkinCollocateCheck = true;
                        }
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("Jem_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(2).Dia;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("Soul_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(2).Soul;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("Heart_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(2).HeroToken;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("SlotCoin_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(2).SlotCoin;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("Key_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(2).Key;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("Skin_Img").GetChild(0).GetComponent<Text>().text = "x" + 2;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("Equipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 3;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").Find("RandomEquipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 7;
                        SkinCard[0].transform.GetChild(0).Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/04_CashShopIcon/Archer_Skin");
                        SkinCard[0].transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        SkinCard[1].transform.GetChild(0).Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/04_CashShopIcon/Archer_Weapon");
                        SkinCard[1].transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    }
                }
                else if (PackageNum == 1)       // 1 = LuxuryPack
                {
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").Find("BackGround").GetComponent<PackOpenScript>().CardPanelCheck = false;
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("Pack_Img").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/04_CashShopIcon/LuxuryPack");
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").gameObject.SetActive(false);
                    SkinCard_Panel.SetActive(false);
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").gameObject.SetActive(true);
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("StarterPack").gameObject.SetActive(false);
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").Find("Background").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

                    Box1_Index = myChar.GachaBox;
                    //Box2_Index = 1;
                    ItemCnt = 6;

                    if (myChar.LuxuryPackCnt == 1)
                    {
                        BoxNo = 3;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("Jem_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(3).Dia;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("Soul_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(3).Soul;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("Heart_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(3).HeroToken;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("SlotCoin_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(3).SlotCoin;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("Key_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(3).Key;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("Equipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 1;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("RandomEquipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 5;
                    }
                    else if (myChar.LuxuryPackCnt == 0)
                    {
                        BoxNo = 4;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("Jem_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(4).Dia;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("Soul_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(4).Soul;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("Heart_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(4).HeroToken;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("SlotCoin_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(4).SlotCoin;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("Key_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(4).Key;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("Equipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 1;
                        GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").Find("RandomEquipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 5;
                    }
                }
                else if (PackageNum == 2)       // 2 = StarterPack
                {
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").Find("BackGround").GetComponent<PackOpenScript>().CardPanelCheck = false;
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("Pack_Img").GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/04_CashShopIcon/StarterPack");
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("SpecialPack").gameObject.SetActive(false);
                    SkinCard_Panel.SetActive(false);
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("LuxuryPack").gameObject.SetActive(false);
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("StarterPack").gameObject.SetActive(true);
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").Find("Background").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

                    Box1_Index = myChar.GachaBox;
                    //Box2_Index = 10;
                    ItemCnt = 5;
                    BoxNo = 5;

                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("StarterPack").Find("Jem_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(5).Dia;
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("StarterPack").Find("Soul_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(5).Soul;
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("StarterPack").Find("Heart_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(5).HeroToken;
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("StarterPack").Find("SlotCoin_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(5).SlotCoin;
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("StarterPack").Find("Key_Img").GetComponentInChildren<Text>().text = "x" + CashShopDataMgr.GetTemplate(5).Key;
                    GachaPanel.transform.GetChild(0).Find("Pack_Panel").GetChild(0).Find("GoodsInfo_Panel").GetChild(0).Find("StarterPack").Find("Equipment_Img").GetChild(0).GetComponent<Text>().text = "x" + 5;
                }
                if (!PackageCardCheck)
                {
                    switch (BoxNo)
                    {
                        case 1:         //스페셜 팩1 구매확정되는곳
                            myChar.Gem += CashShopDataMgr.GetTemplate(1).Dia;
                            myChar.SoulSpark += CashShopDataMgr.GetTemplate(1).Soul;
                            myChar.HeroHeart += CashShopDataMgr.GetTemplate(1).HeroToken;
                            //myChar.SlotCoin += CashShopDataMgr.GetTemplate(1).SlotCoin;
                            myChar.Key += CashShopDataMgr.GetTemplate(1).Key;
                            myChar.KnightWeaponSkinPurchase[4] = 0;
                            myChar.KnightCostumeSkinPurchase[4] = 0;

                            myChar.SaveGem();
                            myChar.SaveSoulSpark();
                            myChar.SaveHeroHeart();
                            //myChar.SaveSlotCoin();
                            myChar.SaveKey();
                            myChar.SaveKnightWeaponSkinPurchase();
                            myChar.SaveKnightCostumeSkinPurchase();
                            break;
                        case 2:         //스페셜 팩2 구매확정되는곳
                            myChar.Gem += CashShopDataMgr.GetTemplate(2).Dia;
                            myChar.SoulSpark += CashShopDataMgr.GetTemplate(2).Soul;
                            myChar.HeroHeart += CashShopDataMgr.GetTemplate(2).HeroToken;
                            //myChar.SlotCoin += CashShopDataMgr.GetTemplate(2).SlotCoin;
                            myChar.Key += CashShopDataMgr.GetTemplate(2).Key;
                            myChar.ArcherWeaponSkinPurchase[4] = 0;
                            myChar.ArcherCostumeSkinPurchase[4] = 0;

                            myChar.SaveGem();
                            myChar.SaveSoulSpark();
                            myChar.SaveHeroHeart();
                            //myChar.SaveSlotCoin();
                            myChar.SaveKey();
                            myChar.SaveArcherWeaponSkinPurchase();
                            myChar.SaveArcherCostumeSkinPurchase();
                            break;
                        case 3:         //럭셔리 팩1 구매확정되는곳
                            myChar.Gem += CashShopDataMgr.GetTemplate(3).Dia;
                            myChar.SoulSpark += CashShopDataMgr.GetTemplate(3).Soul;
                            myChar.HeroHeart += CashShopDataMgr.GetTemplate(3).HeroToken;
                            //myChar.SlotCoin += CashShopDataMgr.GetTemplate(3).SlotCoin;
                            myChar.Key += CashShopDataMgr.GetTemplate(3).Key;

                            myChar.SaveGem();
                            myChar.SaveSoulSpark();
                            myChar.SaveHeroHeart();
                            //myChar.SaveSlotCoin();
                            myChar.SaveKey();
                            break;
                        case 4:         //럭셔리 팩2 구매확정되는곳
                            myChar.Gem += CashShopDataMgr.GetTemplate(4).Dia;
                            myChar.SoulSpark += CashShopDataMgr.GetTemplate(4).Soul;
                            myChar.HeroHeart += CashShopDataMgr.GetTemplate(4).HeroToken;
                            //myChar.SlotCoin += CashShopDataMgr.GetTemplate(4).SlotCoin;
                            myChar.Key += CashShopDataMgr.GetTemplate(4).Key;

                            myChar.SaveGem();
                            myChar.SaveSoulSpark();
                            myChar.SaveHeroHeart();
                            //myChar.SaveSlotCoin();
                            myChar.SaveKey();
                            break;
                        case 5:         //스타터 팩1 구매확정되는곳
                            myChar.Gem += CashShopDataMgr.GetTemplate(5).Dia;
                            myChar.SoulSpark += CashShopDataMgr.GetTemplate(5).Soul;
                            myChar.HeroHeart += CashShopDataMgr.GetTemplate(5).HeroToken;
                            //myChar.SlotCoin += CashShopDataMgr.GetTemplate(5).SlotCoin;
                            myChar.Key += CashShopDataMgr.GetTemplate(5).Key;

                            myChar.SaveGem();
                            myChar.SaveSoulSpark();
                            myChar.SaveHeroHeart();
                            //myChar.SaveSlotCoin();
                            myChar.SaveKey();
                            break;
                    }
                    GachaTable_1.Clear();
                    GachaTable_2.Clear();
                    ChoiceGachaItem.Clear();
                    GachaNomal.Clear();
                    GachaRare.Clear();
                    GachaUnique.Clear();

                    if (!GachaTableCheck_1)
                    {
                        for (int i = 0; i < GachaDataMgr.GetCount(); i++)
                        {
                            if (GachaDataMgr.GetTemplate(i).GachaNum == Box1_Index)
                            {
                                GachaTable_1.Add(GachaDataMgr.GetTemplate(i).ItemNum);

                                if (GachaDataMgr.GetTemplate(i).ItemNum < 1000)
                                {
                                    switch (myChar.itemDataMgr.GetTemplate(GachaDataMgr.GetTemplate(i).ItemNum).Grade)
                                    {
                                        case 0:
                                            GachaNomal.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                                            break;
                                        case 1:
                                            GachaRare.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                                            break;
                                        case 2:
                                            GachaUnique.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (myChar.AntiqueitemDataMgr.GetTemplate(GachaDataMgr.GetTemplate(i).ItemNum).Grade)
                                    {
                                        case 0:
                                            GachaNomal.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                                            break;
                                        case 1:
                                            GachaRare.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                                            break;
                                        case 2:
                                            GachaUnique.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                                            break;
                                    }
                                }
                            }
                            if (i == GachaDataMgr.GetCount() - 1)
                            {
                                GachaTableCheck_1 = true;
                            }
                        }
                    }
                    ////1번 카드팩 구매확정되는곳
                    //if (!GachaTableCheck_1)
                    //{
                    //    for (int i = 0; i < GachaDataMgr.GetCount(); i++)
                    //    {
                    //        if (GachaDataMgr.GetTemplate(i).GachaNum == Box1_Index)
                    //        {
                    //            GachaTable_1.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                    //        }
                    //        if (i == GachaDataMgr.GetCount() - 1)
                    //        {
                    //            GachaTableCheck_1 = true;
                    //        }
                    //    }
                    //}
                    ////2번 카드팩 구매확정되는곳
                    //if (!GachaTableCheck_2)
                    //{
                    //    for (int i = 0; i < GachaDataMgr.GetCount(); i++)
                    //    {
                    //        if (GachaDataMgr.GetTemplate(i).GachaNum == Box2_Index)
                    //        {
                    //            GachaTable_2.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                    //        }
                    //        if (i == GachaDataMgr.GetCount() - 1)
                    //        {
                    //            GachaTableCheck_2 = true;
                    //        }
                    //    }
                    //}
                    switch (BoxNo)
                    {
                        case 1:         //스페셜 팩1
                            CardChoiceRenewal(3, ItemCnt, 1);    //CardChoiceRenewal(A, B, C) A는 100%확률 유니크 뽑을수량 / B : 뽑는수량 0,1 / C : 확률 종류
                            //CardChoice(3, ItemCnt);
                            break;
                        case 2:         //스페셜 팩2
                            CardChoiceRenewal(3, ItemCnt, 1);
                            //CardChoice(3, ItemCnt);
                            break;
                        case 3:         //럭셔리 팩1
                            CardChoiceRenewal(1, ItemCnt, 1);
                            //CardChoice(1, ItemCnt);
                            break;
                        case 4:         //럭셔리 팩1
                            CardChoiceRenewal(1, ItemCnt, 1);
                            //CardChoice(1, ItemCnt);
                            break;
                        case 5:         //스타터 팩1
                            OnlyRareChoice(ItemCnt);    //유일하게 레어만 골라내는 패키지라 함수가다름
                            //CardChoice(5, ItemCnt);
                            break;
                    }

                    for (int i = 0; i < ChoiceGachaItem.Count; i++)
                    {
                        if (ChoiceGachaItem[i] >= 0 && ChoiceGachaItem[i] < 1000)
                        {
                            if (myChar.EquipmentAll[ChoiceGachaItem[i]] >= 0)
                            {
                                myChar.EquipmentQuantity[ChoiceGachaItem[i]]++;

                                myChar.SaveEquipmentQuantity();
                            }
                            else
                            {
                                myChar.EquipmentAll[ChoiceGachaItem[i]]++;

                                myChar.SaveEquipmentAll();
                            }

                        }
                        else if (ChoiceGachaItem[i] >= 1000 && ChoiceGachaItem[i] < 2000)
                        {
                            if (myChar.ElementStoneAll[ChoiceGachaItem[i] - 1000] >= 0)
                            {
                                myChar.ElementStoneQuantity[ChoiceGachaItem[i] - 1000]++;

                                myChar.SaveElementStoneQuantity();
                            }
                            else
                            {
                                myChar.ElementStoneAll[ChoiceGachaItem[i] - 1000]++;

                                myChar.SaveElementStoneAll();
                            }

                        }
                        else if (ChoiceGachaItem[i] >= 2000 && ChoiceGachaItem[i] < 3000)
                        {
                            if (myChar.ActiveitemAll[ChoiceGachaItem[i] - 2000] >= 0)
                            {
                                myChar.ActiveitemQuantity[ChoiceGachaItem[i] - 2000]++;

                                myChar.SaveActiveitemQuantity();
                            }
                            else
                            {
                                myChar.ActiveitemAll[ChoiceGachaItem[i] - 2000]++;

                                myChar.SaveActiveitemAll();
                            }
                        }
                    }
                    PackageCardCheck = true;
                }
            }
        }
        else
        {
            PackagePurchaseCheck = false;
            PackageCardCheck = false;
        }
    }

    //카드뽑기
    public void GachaChoice()
    {
        Gacha_PackPanel.SetActive(false);
        AD_Box.SetActive(false);
        Gem_Box.SetActive(false);
        Play_Box.SetActive(false);
        Card_Panel.SetActive(false);
        if (BoxNo >= 9)
        {
            GachaTable_1.Clear();
            GachaTable_2.Clear();
            ChoiceGachaItem.Clear();
            GachaNomal.Clear();
            GachaRare.Clear();
            GachaUnique.Clear();
            //1번 카드팩
            if (!GachaTableCheck_1)
            {
                for (int i = 0; i < GachaDataMgr.GetCount(); i++)
                {
                    if (GachaDataMgr.GetTemplate(i).GachaNum == Box1_Index)
                    {
                        GachaTable_1.Add(GachaDataMgr.GetTemplate(i).ItemNum);

                        if (GachaDataMgr.GetTemplate(i).ItemNum < 1000)
                        {
                            switch (myChar.itemDataMgr.GetTemplate(GachaDataMgr.GetTemplate(i).ItemNum).Grade)
                            {
                                case 0:
                                    GachaNomal.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                                    break;
                                case 1:
                                    GachaRare.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                                    break;
                                case 2:
                                    GachaUnique.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                                    break;
                            }
                        }
                        else
                        {
                            switch (myChar.AntiqueitemDataMgr.GetTemplate(GachaDataMgr.GetTemplate(i).ItemNum).Grade)
                            {
                                case 0:
                                    GachaNomal.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                                    break;
                                case 1:
                                    GachaRare.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                                    break;
                                case 2:
                                    GachaUnique.Add(GachaDataMgr.GetTemplate(i).ItemNum);
                                    break;
                            }
                        }
                    }
                    if (i == GachaDataMgr.GetCount() - 1)
                    {
                        GachaTableCheck_1 = true;
                    }
                }
            }
        }
        if (!CardChoiceCheck)
        {
            switch (BoxNo)  //1~5번 case없으면 카드 뒤집어지는창 안나옴
            {
                case 1:         //스페셜 팩1
                    Card_Panel.SetActive(true);
                    break;
                case 2:         //스페셜 팩2
                    Card_Panel.SetActive(true);
                    break;
                case 3:         //럭셔리 팩1
                    Card_Panel.SetActive(true);
                    break;
                case 4:         //럭셔리 팩2
                    Card_Panel.SetActive(true);
                    break;
                case 5:         //스타터 팩1
                    Card_Panel.SetActive(true);
                    break;
                case 9:         //광고 상자
                    //CardChoice(1, ItemCnt);
                    CardChoiceRenewal(0, ItemCnt, 0);
                    AD_Box.SetActive(true);
                    //Card_Panel.SetActive(true);
                    break;
                case 10:         //미션 상자
                    Play_Box.SetActive(true);
                    //CardChoice(1, ItemCnt);
                    CardChoiceRenewal(0, ItemCnt, 0);
                    break;
                case 11:         //다이아 상자
                    //CardChoice(1, ItemCnt);
                    CardChoiceRenewal(0, ItemCnt, 1);
                    Gem_Box.SetActive(true);
                    break;
                case 12:         //다이아 상자 10회
                    CardChoiceRenewal(0, ItemCnt, 1);      //CardChoiceRenewal(A, B, C) A는 100%확률 유니크 뽑을수량 / B : 뽑는수량 0,1 / C : 확률 종류
                    Gem_Box.SetActive(true);
                    break;
            }

            if (BoxNo > 5)
            {
                for (int i = 0; i < ChoiceGachaItem.Count; i++)
                {
                    if (ChoiceGachaItem[i] >= 0 && ChoiceGachaItem[i] < 1000)
                    {
                        if (myChar.EquipmentAll[ChoiceGachaItem[i]] >= 0)
                        {
                            myChar.EquipmentQuantity[ChoiceGachaItem[i]]++;
                            myChar.SaveEquipmentQuantity();
                        }
                        else
                        {
                            myChar.EquipmentAll[ChoiceGachaItem[i]]++;
                            myChar.SaveEquipmentAll();
                        }

                    }
                    else if (ChoiceGachaItem[i] >= 1000 && ChoiceGachaItem[i] < 2000)
                    {
                        if (myChar.ElementStoneAll[ChoiceGachaItem[i] - 1000] >= 0)
                        {
                            myChar.ElementStoneQuantity[ChoiceGachaItem[i] - 1000]++;

                            myChar.SaveEquipmentQuantity();
                        }
                        else
                        {
                            myChar.ElementStoneAll[ChoiceGachaItem[i] - 1000]++;

                            myChar.SaveElementStoneAll();
                        }

                    }
                    else if (ChoiceGachaItem[i] >= 2000 && ChoiceGachaItem[i] < 3000)
                    {
                        if (myChar.ActiveitemAll[ChoiceGachaItem[i] - 2000] >= 0)
                        {
                            myChar.ActiveitemQuantity[ChoiceGachaItem[i] - 2000]++;

                            myChar.SaveActiveitemQuantity();
                        }
                        else
                        {
                            myChar.ActiveitemAll[ChoiceGachaItem[i] - 2000]++;

                            myChar.SaveActiveitemAll();
                        }
                    }
                }
            }
        }
        //if (!CardChoiceCheck)
        //{
        //    for (int i = 0; i < ItemCnt; i++)
        //    {
        //        ChoiceGachaItem.Add(GachaTable_1[Random.Range(0, GachaTable_1.Count)]);

        //        if (i == ItemCnt - 1)
        //        {
        //            CardChoiceCheck = true;
        //        }
        //    }
        //}

        if (!CardCreateCheck)
        {
            CardOpenCheckList.Clear();
            for (int i = 0; i < 10; i++)
            {
                if (i < ItemCnt)
                {
                    CardOpenCnt = 0;
                    CardOpenCheckList.Add(false);
                    CardParent[i].SetActive(true);
                    //if (GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).childCount > 0)
                    //{
                    //    Destroy(GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).GetChild(0).gameObject);
                    //}
                    //GameObject Card = Instantiate(GachaCard);
                    //Card.transform.parent = GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).transform;
                    //Card.transform.SetParent(GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).transform);
                    Card[i].transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    Card[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    Card[i].GetComponent<GachaCardScript>().CardNum = i;
                    Card[i].GetComponent<GachaCardScript>().ItemIndex = ChoiceGachaItem[i];
                    Card[i].GetComponent<GachaCardScript>().CardOpenCheck = false;
                    Card[i].GetComponent<GachaCardScript>().CardOpenAnim = false;

                    Card[i].GetComponent<GachaCardScript>().ItemCheck = false;
                    //if (ChoiceGachaItem[i] >= 0 && ChoiceGachaItem[i] < 1000)
                    //{
                    //    Card[i].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + ChoiceGachaItem[i]);
                    //CardGradeCheck(Card[i], myChar.itemDataMgr.GetTemplate(ChoiceGachaItem[i]).Grade);
                    //}
                    //else if (ChoiceGachaItem[i] >= 1000 && ChoiceGachaItem[i] < 2000)
                    //{
                    //    Card[i].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + ChoiceGachaItem[i]);
                    //    CardGradeCheck(Card[i], myChar.ActiveitemDataMgr.GetTemplate(ChoiceGachaItem[i]).Grade);
                    //}
                    //else if (ChoiceGachaItem[i] >= 2000 && ChoiceGachaItem[i] < 3000)
                    //{
                    //    Card[i].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + ChoiceGachaItem[i]);
                    //    CardGradeCheck(Card[i], myChar.ActiveitemDataMgr.GetTemplate(ChoiceGachaItem[i]).Grade);
                    //}
                }
                else
                {
                    CardParent[i].SetActive(false);
                    //if (GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).childCount > 0)
                    //{
                    //    GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).GetChild(0).gameObject.SetActive(false);
                    //}
                }

            }
        }
    }
    
    //대장간 선택된 사용 아이템 설명부분
    private void EquipmentInfo(float ItemLvInfo)
    {
        switch (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).StatIndex)
        {
            case 0:
                AddText = null;
                break;
            case 1:     //공격력
                if (SelectForgeIndex == 1002 || SelectForgeIndex == 5)
                {
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + ItemLvInfo.ToString() + "%";
                }
                else
                {
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString() + "%";
                }
                break;
            case 2:     //사정거리
                AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString() + "%";
                break;
            case 3:     //체력
                AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString() + "%";
                break;
            case 4:     //속도
                if (SelectForgeIndex < 1000)
                {
                    AddText = myChar.TextDataMgr.GetTemplate(4).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString() + "%";
                }
                else
                {
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString() + "%";
                }
                break;
            case 5:     //부활 횟수
                AddText = myChar.TextDataMgr.GetTemplate(5).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString();
                break;
            case 6:     //하트 획득
                if (SelectForgeIndex == 38)
                {
                    AddText = myChar.TextDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n") + ItemLvInfo.ToString() + "%";
                }
                else
                {
                    AddText = myChar.TextDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString();
                }                
                break;
            case 7:     //쉴드 획득
                AddText = myChar.TextDataMgr.GetTemplate(7).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString();
                break;
            case 8:     //높이
                AddText = myChar.TextDataMgr.GetTemplate(8).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString();
                break;
            case 9:     //점프 횟수
                AddText = myChar.TextDataMgr.GetTemplate(9).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString();
                break;
            case 10:    //튕김
                AddText = myChar.TextDataMgr.GetTemplate(10).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString();
                break;
            case 11:    //확률
                if (SelectForgeIndex == 1003)
                {
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + ItemLvInfo.ToString() + "%";
                }
                else
                {
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString() + "%";
                }
                break;
            case 12:    //개수
                AddText = myChar.TextDataMgr.GetTemplate(12).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString();
                break;
            case 13:    //지속 시간 (초)
                AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + ItemLvInfo.ToString();
                break;
        }
    }

    //대장간 아이템 업그레이드 표기 부분
    private void EquipmentUpgradeDsplay(int EquipmentNum)
    {
        if (SelectForgeIndex < 1000)
        {
            if ((EquipmentNum + 1) < EquipmentMaxLv && EquipmentNum > -1)
            {
                switch (myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Grade)
                {
                    case 0:
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Nhero.ToString();
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "(" + myChar.EquipmentQuantity[SelectForgeIndex].ToString() + "/" + BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Nenchan.ToString() + ")";
                        if (BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Nenchan > 0)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = (float)myChar.EquipmentQuantity[SelectForgeIndex] / BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Nenchan;
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                        }

                        if (myChar.HeroHeart >= BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Nhero && myChar.EquipmentQuantity[SelectForgeIndex] >= BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Nenchan)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(true);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(true);
                        }
                        break;
                    case 1:
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Rhero.ToString();
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "(" + myChar.EquipmentQuantity[SelectForgeIndex].ToString() + "/" + BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Renchan.ToString() + ")";
                        if (BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Renchan > 0)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = (float)myChar.EquipmentQuantity[SelectForgeIndex] / BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Renchan;
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                        }

                        if (myChar.HeroHeart >= BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Rhero && myChar.EquipmentQuantity[SelectForgeIndex] >= BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Renchan)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(true);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(true);
                        }
                        break;
                    case 2:
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Uhero.ToString();
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "(" + myChar.EquipmentQuantity[SelectForgeIndex].ToString() + "/" + BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Uenchan.ToString() + ")";
                        if (BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Uenchan > 0)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = (float)myChar.EquipmentQuantity[SelectForgeIndex] / BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Uenchan;
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                        }

                        if (myChar.HeroHeart >= BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Uhero && myChar.EquipmentQuantity[SelectForgeIndex] >= BlackSmithDataMgr.GetTemplate(EquipmentNum + 1).Uenchan)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(true);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(true);
                        }
                        break;
                }
            }
            else if(EquipmentNum == -1)
            {
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = myChar.HeroHeart.ToString();
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "( " + myChar.EquipmentQuantity[SelectForgeIndex].ToString() + " )";
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
            }
            else
            {
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(9205).Content[myChar.LanguageNum].Replace("\\n", "\n");
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "( " + myChar.EquipmentQuantity[SelectForgeIndex].ToString() + " )";
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
            }
        }
        else if (SelectForgeIndex >= 1000 && SelectForgeIndex < 2000)
        {
            if ((EquipmentNum + 1) < ElementalStoneMaxLv && EquipmentNum > -1)
            {
                switch (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Grade)
                {
                    case 0:
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = BlackSmithDataMgr.GetTemplate(202 + EquipmentNum).Nhero.ToString();
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "(" + myChar.ElementStoneQuantity[SelectForgeIndex - 1000].ToString() + "/" + BlackSmithDataMgr.GetTemplate(202 + EquipmentNum).Nenchan.ToString() + ")";
                        if (BlackSmithDataMgr.GetTemplate(EquipmentNum + 202).Nenchan > 0)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = (float)myChar.ElementStoneQuantity[SelectForgeIndex - 1000] / BlackSmithDataMgr.GetTemplate(EquipmentNum + 202).Nenchan;
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                        }

                        if (myChar.HeroHeart >= BlackSmithDataMgr.GetTemplate(202 + EquipmentNum).Nhero && myChar.ElementStoneQuantity[SelectForgeIndex - 1000] >= BlackSmithDataMgr.GetTemplate(202 + EquipmentNum).Nenchan)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(true);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(true);
                        }
                        break;
                    case 1:
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = BlackSmithDataMgr.GetTemplate(202 + EquipmentNum).Rhero.ToString();
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "(" + myChar.ElementStoneQuantity[SelectForgeIndex - 1000].ToString() + "/" + BlackSmithDataMgr.GetTemplate(202 + EquipmentNum).Renchan.ToString() + ")";
                        if (BlackSmithDataMgr.GetTemplate(EquipmentNum + 202).Renchan > 0)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = (float)myChar.ElementStoneQuantity[SelectForgeIndex - 1000] / BlackSmithDataMgr.GetTemplate(EquipmentNum + 202).Renchan;
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                        }

                        if (myChar.HeroHeart >= BlackSmithDataMgr.GetTemplate(202 + EquipmentNum).Rhero && myChar.ElementStoneQuantity[SelectForgeIndex - 1000] >= BlackSmithDataMgr.GetTemplate(202 + EquipmentNum).Renchan)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(true);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(true);
                        }
                        break;
                    case 2:
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = BlackSmithDataMgr.GetTemplate(202 + EquipmentNum).Uhero.ToString();
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "(" + myChar.ElementStoneQuantity[SelectForgeIndex - 1000].ToString() + "/" + BlackSmithDataMgr.GetTemplate(202 + EquipmentNum).Uenchan.ToString() + ")";
                        if (BlackSmithDataMgr.GetTemplate(EquipmentNum + 202).Uenchan > 0)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = (float)myChar.ElementStoneQuantity[SelectForgeIndex - 1000] / BlackSmithDataMgr.GetTemplate(EquipmentNum + 202).Uenchan;
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                        }

                        if (myChar.HeroHeart >= BlackSmithDataMgr.GetTemplate(202 + EquipmentNum).Uhero && myChar.ElementStoneQuantity[SelectForgeIndex - 1000] >= BlackSmithDataMgr.GetTemplate(202 + EquipmentNum).Uenchan)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(true);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(true);
                        }
                        break;
                }
            }
            else if (EquipmentNum == -1)
            {
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = myChar.HeroHeart.ToString();
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "( " + myChar.EquipmentQuantity[SelectForgeIndex].ToString() + " )";
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
            }
            else
            {
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(9205).Content[myChar.LanguageNum].Replace("\\n", "\n");
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "( " + myChar.EquipmentQuantity[SelectForgeIndex - 1000].ToString() + " )";
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
            }
        }
        else if (SelectForgeIndex >= 2000)
        {
            if ((EquipmentNum + 1) < AntiqueitemMaxLv && EquipmentNum > -1)
            {
                switch (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Grade)
                {
                    case 0:
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = BlackSmithDataMgr.GetTemplate(102 + EquipmentNum).Nhero.ToString();
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "(" + myChar.ActiveitemQuantity[SelectForgeIndex - 2000].ToString() + "/" + BlackSmithDataMgr.GetTemplate(102 + EquipmentNum).Nenchan.ToString() + ")";
                        if (BlackSmithDataMgr.GetTemplate(EquipmentNum + 102).Nenchan > 0)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = (float)myChar.ActiveitemQuantity[SelectForgeIndex - 2000] / BlackSmithDataMgr.GetTemplate(EquipmentNum + 102).Nenchan;
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                        }

                        if (myChar.HeroHeart >= BlackSmithDataMgr.GetTemplate(102 + EquipmentNum).Nhero && myChar.ActiveitemQuantity[SelectForgeIndex - 2000] >= BlackSmithDataMgr.GetTemplate(102 + EquipmentNum).Nenchan)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(true);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(true);
                        }
                        break;
                    case 1:
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = BlackSmithDataMgr.GetTemplate(102 + EquipmentNum).Rhero.ToString();
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "(" + myChar.ActiveitemQuantity[SelectForgeIndex - 2000].ToString() + "/" + BlackSmithDataMgr.GetTemplate(102 + EquipmentNum).Renchan.ToString() + ")";
                        if (BlackSmithDataMgr.GetTemplate(EquipmentNum + 102).Renchan > 0)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = (float)myChar.ActiveitemQuantity[SelectForgeIndex - 2000] / BlackSmithDataMgr.GetTemplate(EquipmentNum + 102).Renchan;
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                        }

                        if (myChar.HeroHeart >= BlackSmithDataMgr.GetTemplate(102 + EquipmentNum).Rhero && myChar.ActiveitemQuantity[SelectForgeIndex - 2000] >= BlackSmithDataMgr.GetTemplate(102 + EquipmentNum).Renchan)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(true);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(true);
                        }
                        break;
                    case 2:
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = BlackSmithDataMgr.GetTemplate(102 + EquipmentNum).Uhero.ToString();
                        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "(" + myChar.ActiveitemQuantity[SelectForgeIndex - 2000].ToString() + "/" + BlackSmithDataMgr.GetTemplate(102 + EquipmentNum).Uenchan.ToString() + ")";
                        if (BlackSmithDataMgr.GetTemplate(EquipmentNum + 102).Uenchan > 0)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = (float)myChar.ActiveitemQuantity[SelectForgeIndex - 2000] / BlackSmithDataMgr.GetTemplate(EquipmentNum + 102).Uenchan;
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                        }

                        if (myChar.HeroHeart >= BlackSmithDataMgr.GetTemplate(102 + EquipmentNum).Uhero && myChar.ActiveitemQuantity[SelectForgeIndex - 2000] >= BlackSmithDataMgr.GetTemplate(102 + EquipmentNum).Uenchan)
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(true);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
                        }
                        else
                        {
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(true);
                        }
                        break;
                }
            }
            else if (EquipmentNum == -1)
            {
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = myChar.HeroHeart.ToString();
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "( " + myChar.EquipmentQuantity[SelectForgeIndex].ToString() + " )";
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
            }
            else
            {
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("HeroHeart").GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(9205).Content[myChar.LanguageNum].Replace("\\n", "\n");
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("EquipmentCnt_Text").GetComponent<Text>().text = "( " + myChar.EquipmentQuantity[SelectForgeIndex - 2000].ToString() + " )";
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Item_Gage").GetComponent<Slider>().value = 1f;
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("Enough_Btn").gameObject.SetActive(false);
                BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(1).Find("NotEnough_Btn").gameObject.SetActive(false);
            }
        }

    }

    //장비 업그레이드 
    public void BlacksmithUpgrade_Btn()
    {
        if (SelectForgeIndex < 1000)
        {
            if (myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Grade == 0)
            {
                myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[SelectForgeIndex] + 1).Nhero;
                myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[SelectForgeIndex] + 1).Nenchan;
                FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[SelectForgeIndex] + 1).Nhero);
                
                myChar.EquipmentAll[SelectForgeIndex]++;
                myChar.SaveHeroHeart();
                myChar.SaveEquipmentQuantity();
                myChar.SaveEquipmentAll();
            }
            else if (myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Grade == 1)
            {
                myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[SelectForgeIndex] + 1).Rhero;
                myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[SelectForgeIndex] +  1).Renchan;
                FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[SelectForgeIndex] + 1).Rhero);
                //switch (myChar.EquipmentAll[SelectForgeIndex])
                //{
                //    case 0:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(1).Rhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(1).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(1).Rhero);
                //        break;
                //    case 1:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(2).Rhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(2).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(2).Rhero);
                //        break;
                //    case 2:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(3).Rhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(3).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(3).Rhero);
                //        break;
                //    case 3:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(4).Rhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(4).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(4).Rhero);
                //        break;
                //    case 4:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(5).Rhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(5).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(5).Rhero);
                //        break;
                //    case 5:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(6).Rhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(6).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(6).Rhero);
                //        break;
                //    case 6:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(6).Rhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(6).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(6).Rhero);
                //        break;
                //    case 7:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(8).Rhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(8).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(8).Rhero);
                //        break;
                //    case 8:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(9).Rhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(9).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(9).Rhero);
                //        break;
                //    case 9:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(10).Rhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(10).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(10).Rhero);
                //        break;
                //    case 10:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(11).Rhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(11).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(11).Rhero);
                //        break;
                //}
                myChar.EquipmentAll[SelectForgeIndex]++;
                myChar.SaveHeroHeart();
                myChar.SaveEquipmentQuantity();
                myChar.SaveEquipmentAll();
            }
            else if (myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Grade == 2)
            {
                myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[SelectForgeIndex] + 1).Uhero;
                myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[SelectForgeIndex] + 1).Uenchan;
                FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[SelectForgeIndex] + 1).Uhero);

                //switch (myChar.EquipmentAll[SelectForgeIndex])
                //{
                //    case 0:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(1).Uhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(1).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(1).Uhero);
                //        break;
                //    case 1:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(2).Uhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(2).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(2).Uhero);
                //        break;
                //    case 2:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(3).Uhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(3).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(3).Uhero);
                //        break;
                //    case 3:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(4).Uhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(4).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(4).Uhero);
                //        break;
                //    case 4:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(5).Uhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(5).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(5).Uhero);
                //        break;
                //    case 5:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(6).Uhero;
                //        myChar.EquipmentQuantity[SelectForgeIndex] -= BlackSmithDataMgr.GetTemplate(6).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.itemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.EquipmentAll[SelectForgeIndex], BlackSmithDataMgr.GetTemplate(6).Uhero);
                //        break;
                //}
                myChar.EquipmentAll[SelectForgeIndex]++;
                myChar.SaveHeroHeart();
                myChar.SaveEquipmentQuantity();
                myChar.SaveEquipmentAll();
            }
        }
        else if (SelectForgeIndex >= 1000 && SelectForgeIndex < 2000)
        {
            if (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Grade == 0)
            {
                myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[SelectForgeIndex - 1000] + 202).Nhero;
                myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[SelectForgeIndex - 1000] + 202).Nenchan;
                FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[SelectForgeIndex - 1000] + 202).Nhero);
                //switch (myChar.ElementStoneAll[SelectForgeIndex - 1000])
                //{
                //    case 0:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(15).Nhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(15).Nenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(15).Nhero);
                //        break;
                //    case 1:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(16).Nhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(16).Nenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(16).Nhero);
                //        break;
                //    case 2:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(17).Nhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(17).Nenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(17).Nhero);
                //        break;
                //    case 3:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(18).Nhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(18).Nenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(18).Nhero);
                //        break;
                //    case 4:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(19).Nhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(19).Nenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(19).Nhero);
                //        break;
                //    case 5:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(20).Nhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(20).Nenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(20).Nhero);
                //        break;
                //}
                myChar.ElementStoneAll[SelectForgeIndex - 1000]++;
                myChar.SaveHeroHeart();
                myChar.SaveElementStoneQuantity();
                myChar.SaveElementStoneAll();
            }
            else if (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Grade == 1)
            {
                myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[SelectForgeIndex - 1000] + 202).Rhero;
                myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[SelectForgeIndex - 1000] + 202).Renchan;
                FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[SelectForgeIndex - 1000] + 202).Rhero);

                //switch (myChar.ElementStoneAll[SelectForgeIndex - 1000])
                //{
                //    case 0:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(15).Rhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(15).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(15).Rhero);
                //        break;
                //    case 1:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(16).Rhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(16).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(16).Rhero);
                //        break;
                //    case 2:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(17).Rhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(17).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(17).Rhero);
                //        break;
                //    case 3:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(18).Rhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(18).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(18).Rhero);
                //        break;
                //    case 4:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(19).Rhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(19).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(19).Rhero);
                //        break;
                //    case 5:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(20).Rhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(20).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(20).Rhero);
                //        break;
                //}
                myChar.ElementStoneAll[SelectForgeIndex - 1000]++;
                myChar.SaveHeroHeart();
                myChar.SaveElementStoneQuantity();
                myChar.SaveElementStoneAll();
            }
            else if (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Grade == 2)
            {
                myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[SelectForgeIndex - 1000] + 202).Uhero;
                myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[SelectForgeIndex - 1000] + 202).Uenchan;
                FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[SelectForgeIndex - 1000] + 202).Uhero);
                //switch (myChar.ElementStoneAll[SelectForgeIndex - 1000])
                //{
                //    case 0:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(15).Uhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(15).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(15).Uhero);
                //        break;
                //    case 1:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(16).Uhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(16).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(16).Uhero);
                //        break;
                //    case 2:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(17).Uhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(17).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(17).Uhero);
                //        break;
                //    case 3:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(18).Uhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(18).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(18).Uhero);
                //        break;
                //    case 4:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(19).Uhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(19).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(19).Uhero);
                //        break;
                //    case 5:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(20).Uhero;
                //        myChar.ElementStoneQuantity[SelectForgeIndex - 1000] -= BlackSmithDataMgr.GetTemplate(20).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ElementStoneAll[SelectForgeIndex - 1000], BlackSmithDataMgr.GetTemplate(20).Uhero);
                //        break;
                //}
                myChar.ElementStoneAll[SelectForgeIndex - 1000]++;
                myChar.SaveHeroHeart();
                myChar.SaveElementStoneQuantity();
                myChar.SaveElementStoneAll();
            }
        }
        else if (SelectForgeIndex >= 2000)
        {
            if (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Grade == 0)
            {
                myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[SelectForgeIndex - 2000] + 102).Nhero;
                myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[SelectForgeIndex - 2000] + 102).Nenchan;
                FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[SelectForgeIndex - 2000] + 102).Nhero);

                //switch (myChar.ActiveitemAll[SelectForgeIndex - 2000])
                //{
                //    case 0:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(8).Nhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(8).Nenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(8).Nhero);
                //        break;
                //    case 1:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(9).Nhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(9).Nenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(9).Nhero);
                //        break;
                //    case 2:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(10).Nhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(10).Nenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(10).Nhero);
                //        break;
                //    case 3:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(11).Nhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(11).Nenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(11).Nhero);
                //        break;
                //    case 4:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(12).Nhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(12).Nenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(12).Nhero);
                //        break;
                //    case 5:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(13).Nhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(13).Nenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(13).Nhero);
                //        break;
                //}
                myChar.ActiveitemAll[SelectForgeIndex - 2000]++;
                myChar.SaveHeroHeart();
                myChar.SaveActiveitemQuantity();
                myChar.SaveActiveitemAll();
            }
            else if (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Grade == 1)
            {
                myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[SelectForgeIndex - 2000] + 102).Rhero;
                myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[SelectForgeIndex - 2000] + 102).Renchan;
                FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[SelectForgeIndex - 2000] + 102).Rhero);
                //switch (myChar.ActiveitemAll[SelectForgeIndex - 2000])
                //{
                //    case 0:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(8).Rhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(8).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(8).Rhero);
                //        break;
                //    case 1:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(9).Rhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(9).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(9).Rhero);
                //        break;
                //    case 2:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(10).Rhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(10).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(10).Rhero);
                //        break;
                //    case 3:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(11).Rhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(11).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(11).Rhero);
                //        break;
                //    case 4:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(12).Rhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(12).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(12).Rhero);
                //        break;
                //    case 5:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(13).Rhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(13).Renchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(13).Rhero);
                //        break;
                //}
                myChar.ActiveitemAll[SelectForgeIndex - 2000]++;
                myChar.SaveHeroHeart();
                myChar.SaveActiveitemQuantity();
                myChar.SaveActiveitemAll();
            }
            else if (myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Grade == 2)
            {
                
                myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[SelectForgeIndex - 2000] + 102).Uhero;
                myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[SelectForgeIndex - 2000] + 102).Uenchan;
                FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[SelectForgeIndex - 2000] + 102).Uhero);
                //switch (myChar.ActiveitemAll[SelectForgeIndex - 2000])
                //{
                //    case 0:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(8).Uhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(8).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(8).Uhero);
                //        break;
                //    case 1:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(9).Uhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(9).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(9).Uhero);
                //        break;
                //    case 2:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(10).Uhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(10).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(10).Uhero);
                //        break;
                //    case 3:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(11).Uhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(11).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(11).Uhero);
                //        break;
                //    case 4:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(12).Uhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(12).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(12).Uhero);
                //        break;
                //    case 5:
                //        myChar.HeroHeart -= BlackSmithDataMgr.GetTemplate(13).Uhero;
                //        myChar.ActiveitemQuantity[SelectForgeIndex - 2000] -= BlackSmithDataMgr.GetTemplate(13).Uenchan;
                //        FirebaseManager.firebaseManager.ForgeItemUpgrade(myChar.AntiqueitemDataMgr.GetTemplate(SelectForgeIndex).Kor, myChar.ActiveitemAll[SelectForgeIndex - 2000], BlackSmithDataMgr.GetTemplate(13).Uhero);
                //        break;
                //}
                myChar.ActiveitemAll[SelectForgeIndex - 2000]++;
                myChar.SaveHeroHeart();
                myChar.SaveActiveitemQuantity();
                myChar.SaveActiveitemAll();
            }
        }
    }
    public void BlackSmitComplete()
    {
        Gacha_PackPanel.SetActive(false);
        AD_Box.SetActive(false);
        Gem_Box.SetActive(false);
        Play_Box.SetActive(false);
        Card_Panel.SetActive(false);

        if (!myChar.CraftingItemPickupCheck)
        {
            if (myChar.CraftingItemNum > -1)
            {
                if (myChar.CraftingItemNum < 1000)
                {
                    if (myChar.EquipmentAll[myChar.CraftingItemNum] >= 0)
                    {
                        myChar.EquipmentQuantity[myChar.CraftingItemNum]++;
                    }
                    else
                    {
                        myChar.EquipmentAll[myChar.CraftingItemNum]++;
                        myChar.SaveEquipmentAll();
                    }

                }
                else if (myChar.CraftingItemNum >= 1000 && myChar.CraftingItemNum < 2000)
                {
                    if (myChar.ElementStoneAll[myChar.CraftingItemNum - 1000] >= 0)
                    {
                        myChar.ElementStoneQuantity[myChar.CraftingItemNum - 1000]++;
                        myChar.SaveElementStoneQuantity();
                    }
                    else
                    {
                        myChar.ElementStoneAll[myChar.CraftingItemNum - 1000]++;
                        myChar.SaveElementStoneAll();
                    }

                }
                else if (myChar.CraftingItemNum >= 2000 && myChar.CraftingItemNum < 3000)
                {
                    if (myChar.ActiveitemAll[myChar.CraftingItemNum - 2000] >= 0)
                    {
                        myChar.ActiveitemQuantity[myChar.CraftingItemNum - 2000]++;
                        myChar.SaveActiveitemQuantity();
                    }
                    else
                    {
                        myChar.ActiveitemAll[myChar.CraftingItemNum - 2000]++;
                        myChar.SaveActiveitemAll();
                    }
                }
            }
        }

        if (!CardCreateCheck)
        {
            CardOpenCheckList.Clear();
            for (int i = 0; i < 10; i++)
            {
                if (i < 1)
                {
                    CardOpenCnt = 0;
                    CardOpenCheckList.Add(false);
                    //if (GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).childCount > 0)
                    //{
                    //    Destroy(GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).GetChild(0).gameObject);
                    //}
                    //GameObject Card = Instantiate(GachaCard);
                    //Card.transform.parent = GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).transform;
                    //Card.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    //Card.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    //Card.GetComponent<GachaCardScript>().CardNum = i;
                    //Card.GetComponent<GachaCardScript>().ItemIndex = myChar.CraftingItemNum;
                    //Card.GetComponent<GachaCardScript>().CardOpenCheck = false;
                    //Card.GetComponent<GachaCardScript>().ItemCheck = false;

                    Card[i].transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    Card[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    Card[i].GetComponent<GachaCardScript>().CardNum = i;
                    Card[i].GetComponent<GachaCardScript>().ItemIndex = myChar.CraftingItemNum;
                    Card[i].GetComponent<GachaCardScript>().CardOpenCheck = false;
                    Card[i].GetComponent<GachaCardScript>().CardOpenAnim = false;
                    Card[i].GetComponent<GachaCardScript>().ItemCheck = false;

                    //if (myChar.CraftingItemNum >= 0 && myChar.CraftingItemNum < 1000)
                    //{
                    //    Card.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.CraftingItemNum);
                    //    CardGradeCheck(Card, myChar.itemDataMgr.GetTemplate(myChar.CraftingItemNum).Grade);
                    //}
                    //else if (myChar.CraftingItemNum >= 1000 && myChar.CraftingItemNum < 2000)
                    //{
                    //    Card.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + myChar.CraftingItemNum);
                    //    CardGradeCheck(Card, myChar.ActiveitemDataMgr.GetTemplate(myChar.CraftingItemNum).Grade);
                    //}
                    //else if (myChar.CraftingItemNum >= 2000 && myChar.CraftingItemNum < 3000)
                    //{
                    //    Card.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + myChar.CraftingItemNum);
                    //    CardGradeCheck(Card, myChar.ActiveitemDataMgr.GetTemplate(myChar.CraftingItemNum).Grade);
                    //}
                }
                else
                {
                    CardParent[i].SetActive(false);
                    //if (GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).childCount > 0)
                    //{
                    //    GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).GetChild(0).gameObject.SetActive(false);
                    //}
                }
            }
        }

        Card_Panel.SetActive(true);
    }
    private void CardChoice(int Num, int itemCnt)
    {
        for (int i = 0; i < itemCnt; i++)
        {
            if (i < Num)
            {
                ChoiceGachaItem.Add(GachaTable_1[Random.Range(0, GachaTable_1.Count)]);
            }
            else
            {
                ChoiceGachaItem.Add(GachaTable_2[Random.Range(0, GachaTable_2.Count)]);
            }

            if (i == itemCnt - 1)
            {
                CardChoiceCheck = true;
            }
        }
    }
    private void CardChoiceRenewal(int Num, int itemCnt, int WeightType)
    {
        for (int i = 0; i < itemCnt; i++)
        {
            WeightedRandomizer<float> TypeNum = new WeightedRandomizer<float>();

            if (WeightType == 0)
            {
                TypeNum.RandomPercent(1, 0.7f);
                TypeNum.RandomPercent(2, 0.2f);
                TypeNum.RandomPercent(3, 0.1f);
                Debug.Log(00);
            }
            else if (WeightType == 1)
            {
                TypeNum.RandomPercent(1, 0.5f);
                TypeNum.RandomPercent(2, 0.3f);
                TypeNum.RandomPercent(3, 0.2f);
            }

            float GachaWin = TypeNum.GetNext();

            if (i < Num)
            {
                ChoiceGachaItem.Add(GachaUnique[Random.Range(0, GachaUnique.Count)]);
            }
            else
            {
                if (GachaWin == 1)
                {
                    ChoiceGachaItem.Add(GachaNomal[Random.Range(0, GachaNomal.Count)]);
                }
                else if (GachaWin == 2)
                {
                    ChoiceGachaItem.Add(GachaRare[Random.Range(0, GachaRare.Count)]);
                }
                else if (GachaWin == 3)
                {
                    ChoiceGachaItem.Add(GachaUnique[Random.Range(0, GachaUnique.Count)]);
                }
            }
            if (i == itemCnt - 1)
            {
                CardChoiceCheck = true;
            }
        }
    }
    private void OnlyRareChoice(int itemCnt)
    {
        for (int i = 0; i < itemCnt; i++)
        {
            ChoiceGachaItem.Add(GachaRare[Random.Range(0, GachaRare.Count)]);

            if (i == itemCnt - 1)
            {
                CardChoiceCheck = true;
            }
        }
    }
    private void CardGradeCheck(GameObject Card, int GradeNum)
    {
        if (GradeNum == 0)
        {
            Card.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/05_CardBoard/" + 0);
        }
        else if (GradeNum == 1)
        {
            Card.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/05_CardBoard/" + 1);
        }
        else if (GradeNum == 2)
        {
            Card.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/05_CardBoard/" + 2);
        }
    }
    public void CardAllOpen()
    {
        StartCoroutine(CardAllDraw(0.2f));
        //for (int i = 0; i < ItemCnt; i++)
        //{
        //    GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).GetChild(0).GetComponent<GachaCardScript>().CardOpenAnim = true;
        //}
    }
    public void StageInput()
    {
        if (myChar.StageClearCheck[myChar.Chapter])
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync("StageScene"); //로드 (비동기)
                                                                           //SceneManager.LoadScene("StageScene");
            LoadingSceneManager.LoadScene("StageScene");
            myChar.InGameStart = true;
            myChar.ThroneBossKill = false;
            myChar.MultipleCheck = false;
            myChar.MHpMultiIndex = 0;
            //myChar.Chapter = 0;
            myChar.Stage = 0;
            myChar.SelectedStage = 0;
            myChar.Gain_Multiple = 1;
            myChar.Resurrection = false;
            myChar.Resetitem();
            myChar.Finished = false;
            myChar.CrownGetBgm = false;
            myChar.SeasonalShield = false;
            myChar.EnthroneCameraOn = false;
            myChar.ItemOwnCheck = false;
            
            myChar.Gain_Multiple = 1;

            for (int i = 0; i < myChar.InventoryItemNum.Length; i++)
            {
                if (myChar.InventoryItemNum[i] != -1)
                {
                    myChar.EquipmentUsed.Add(myChar.InventoryItemNum[i]);
                    myChar.EquipmentUsedTrueCheck.Add(false);
                    myChar.PlayerEquipment.Add(myChar.InventoryItemNum[i]);
                }
            }
            //myChar.Chapter1_Cnt = 0;
            //myChar.Chapter2_Cnt = 0;
            //myChar.Chapter3_Cnt = 0;
            //myChar.ChapterCntCheck = false;
            myChar.SaveChapter();
            myChar.SaveSelectHero();

            //if (myChar.SlotActiveCheck) //스테이지 입장전에 장비 장착 체크 부분
            //{
            //    switch (myChar.MachinSlotCnt)
            //    {
            //        case 1:
            //            myChar.EquipmentUsed.Add(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[0]]);
            //            myChar.EquipmentUsedTrueCheck.Add(false);
            //            myChar.PlayerEquipment.Add(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[0]]);
            //            myChar.SlotActiveCheck = false;
            //            break;
            //        case 2:
            //            myChar.EquipmentUsed.Add(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[0]]);
            //            myChar.EquipmentUsedTrueCheck.Add(false);
            //            myChar.PlayerEquipment.Add(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[0]]);
            //            myChar.ActiveItem = myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[1]] - 2000;
            //            myChar.SlotActiveCheck = false;
            //            break;
            //        case 3:
            //            myChar.EquipmentUsed.Add(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[0]]);
            //            myChar.EquipmentUsedTrueCheck.Add(false);
            //            myChar.PlayerEquipment.Add(myChar.EquipmentActive[myChar.LobbyMachineEquipmentNum[0]]);
            //            myChar.ActiveItem = myChar.ActiveitemActive[myChar.LobbyMachineEquipmentNum[1]] - 2000;
            //            myChar.ElementStone[myChar.LobbyMachineEquipmentNum[2]] = 20;
            //            myChar.SlotActiveCheck = false;
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //myChar.LobbyMachineEquipmentNum[0] = -1;
            //myChar.LobbyMachineEquipmentNum[1] = -1;
            //myChar.LobbyMachineEquipmentNum[2] = -1;

            //myChar.SaveLobbyMachineEquipmentNum();
            //myChar.SaveSlotActiveCheck();

            //왕좌 보스가 착용하고 있는 아이템 등급으로 EquipEXP 계산
            //if (myChar.ThroneItemList.Count > 0)
            //{
            //    for (int i = 0; i < myChar.ThroneItemList.Count; i++)
            //    {
            //        if (myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[i]).Grade == 0)
            //        {
            //            myChar.EquipmentTotalScore += 25;
            //        }
            //        else if (myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[i]).Grade == 1)
            //        {
            //            myChar.EquipmentTotalScore += 50;
            //        }
            //        else if (myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[i]).Grade == 2)
            //        {
            //            myChar.EquipmentTotalScore += 75;
            //        }
            //    }
            //}
        }
        else
        {
            //EnthroneHeroInfo_Panel.SetActive(true);
            Debug.Log("이전맵을 클리어하세요");
        }
    }

    void OnLoadHeroTemplateMgr()
    {
        string TextResource = "01_Excel/Hero_info";
        HeroDataMgr.OnDataLoad(TextResource);
        string SkillTextResource = "01_Excel/SkillInfo";
        SkillDataMgr.OnDataLoad(SkillTextResource);
        string ShopTextResource = "01_Excel/ShopText_Info";
        ShopDataMgr.OnDataLoad(ShopTextResource);
        string SkinextResource = "01_Excel/SkinDataText";
        SkinDataMgr.OnDataLoad(SkinextResource);
        string CashShopResource = "01_Excel/CashShopData";
        CashShopDataMgr.OnDataLoad(CashShopResource);
        string GachaTextResource = "01_Excel/GachaData";
        GachaDataMgr.OnDataLoad(GachaTextResource);
        string BlackSmithResource = "01_Excel/BlackSmith";
        BlackSmithDataMgr.OnDataLoad(BlackSmithResource);
        string AttendanceResource = "01_Excel/Attendance";
        AttendanceDataMgr.OnDataLoad(AttendanceResource);
    }
    private void PreferencesWindowCheck()
    {
        if (PreferencesWindow.activeSelf == true)
        {
            if (myChar.muteEffectSound)
            {
                PreferencesWindow.transform.GetChild(0).GetChild(1).Find("Effect_On_Btn").gameObject.SetActive(false);
                PreferencesWindow.transform.GetChild(0).GetChild(1).Find("Effect_Off_Btn").gameObject.SetActive(true);
            }
            else if (!myChar.muteEffectSound)
            {
                PreferencesWindow.transform.GetChild(0).GetChild(1).Find("Effect_On_Btn").gameObject.SetActive(true);
                PreferencesWindow.transform.GetChild(0).GetChild(1).Find("Effect_Off_Btn").gameObject.SetActive(false);
            }

            if (myChar.muteBGM)
            {
                PreferencesWindow.transform.GetChild(0).GetChild(1).Find("BGM_On_Btn").gameObject.SetActive(false);
                PreferencesWindow.transform.GetChild(0).GetChild(1).Find("BGM_Off_Btn").gameObject.SetActive(true);
            }
            else if (!myChar.muteBGM)
            {
                PreferencesWindow.transform.GetChild(0).GetChild(1).Find("BGM_On_Btn").gameObject.SetActive(true);
                PreferencesWindow.transform.GetChild(0).GetChild(1).Find("BGM_Off_Btn").gameObject.SetActive(false);
            }
            PreferencesUI_TopDown_Btn.sprite = TopDown_Img[myChar.TopDownCnt];
            PreferencesUI_Hand_Btn.sprite = Hand_Img[myChar.HandCheck + 1];
        }
    }
    public void TopDown_Btn()
    {
        if (myChar.TopDownCnt < 2)
        {
            myChar.TopDownCnt++;
        }
        else
        {
            myChar.TopDownCnt = 0;
        }

        myChar.SaveTopDownCnt();
    }
    public void Hand_Btn()
    {
        if (myChar.HandCheck == 0)
        {
            myChar.HandCheck--;
        }
        else if(myChar.HandCheck == -1)
        {
            myChar.HandCheck++;
        }
        myChar.SaveHandCheck();
    }
    public void PurchaseCheck(int PackageNum)
    {
        if (PackageNum == 0)
        {
            IAPManager.Instance.Purchase("special_pack");
            SkinCollocateCheck = false;
        }
        else if (PackageNum == 1)
        {
            IAPManager.Instance.Purchase("luxury_pack");
            SkinCollocateCheck = false;
        }
        else if (PackageNum == 2)
        {
            IAPManager.Instance.Purchase("starter_pack");
            SkinCollocateCheck = false;
        }
    }
    public void ADPurchaseCheck()
    {
        IAPManager.Instance.Purchase("ad_skip");
    }

    public void CashGemPurchaseCheck()  //상점 현금구매 다이아사는거체크
    {
        if (BasicProductCnt == 0)
        {
            IAPManager.Instance.Purchase("dia_1");
        }
        else if (BasicProductCnt == 1)
        {
            IAPManager.Instance.Purchase("dia_2");
        }
        else if (BasicProductCnt == 2)
        {
            IAPManager.Instance.Purchase("dia_3");
        }
    }

    private void SkinPurchaseManager(ObscuredInt[] Weapon, ObscuredInt[] Costume)
    {
        if (myChar.SelectWeapon[myChar.SelectHero] != -1)
        {
            if (myChar.Gem >= SkinDataMgr.GetTemplate(WeaponSkinCnt[myChar.SelectWeapon[myChar.SelectHero]]).Cost)
            {
                FirebaseManager.firebaseManager.LobbySkinPurchase(SkinDataMgr.GetTemplate(WeaponSkinCnt[myChar.SelectWeapon[myChar.SelectHero]]).Content[0].Replace("\\n", "\n"));
                myChar.Gem -= SkinDataMgr.GetTemplate(WeaponSkinCnt[myChar.SelectWeapon[myChar.SelectHero]]).Cost;
                Weapon[myChar.SelectWeapon[myChar.SelectHero]] = 0;

                myChar.SaveGem();
            }
        }
        else if (myChar.SelectCostume[myChar.SelectHero] != -1)
        {
            if (myChar.Gem >= SkinDataMgr.GetTemplate(CostumeSkinCnt[myChar.SelectCostume[myChar.SelectHero]]).Cost)
            {
                FirebaseManager.firebaseManager.LobbySkinPurchase(SkinDataMgr.GetTemplate(CostumeSkinCnt[myChar.SelectCostume[myChar.SelectHero]]).Content[0].Replace("\\n", "\n"));
                myChar.Gem -= SkinDataMgr.GetTemplate(CostumeSkinCnt[myChar.SelectCostume[myChar.SelectHero]]).Cost;
                Costume[myChar.SelectCostume[myChar.SelectHero]] = 0;

                myChar.SaveGem();
            }
        }
    }
    public void SkinPurchased()
    {
        switch (myChar.SelectHero)
        {
            case 0:
                SkinPurchaseManager(myChar.KnightWeaponSkinPurchase, myChar.KnightCostumeSkinPurchase);
                myChar.SaveKnightWeaponSkinPurchase();
                myChar.SaveKnightCostumeSkinPurchase();
                break;
            case 1:
                SkinPurchaseManager(myChar.WarriorWeaponSkinPurchase, myChar.WarriorCostumeSkinPurchase);
                myChar.SaveWarriorWeaponSkinPurchase();
                myChar.SaveWarriorCostumeSkinPurchase();
                break;
            case 2:
                SkinPurchaseManager(myChar.ArcherWeaponSkinPurchase, myChar.ArcherCostumeSkinPurchase);
                myChar.SaveArcherWeaponSkinPurchase();
                myChar.SaveArcherCostumeSkinPurchase();
                break;
            case 3:
                SkinPurchaseManager(myChar.WizardWeaponSkinPurchase, myChar.WizardCostumeSkinPurchase);
                myChar.SaveWizardWeaponSkinPurchase();
                myChar.SaveWizardCostumeSkinPurchase();
                break;
            case 4:
                SkinPurchaseManager(myChar.NinjaWeaponSkinPurchase, myChar.NinjaCostumeSkinPurchase);
                myChar.SaveNinjaWeaponSkinPurchase();
                myChar.SaveNinjaCostumeSkinPurchase();
                break;
        }
    }

    public void SkinEquipment()
    {
        if (myChar.SelectWeapon[myChar.SelectHero] != -1)
        {
            myChar.EquipmentWeapon[myChar.SelectHero] = myChar.SelectWeapon[myChar.SelectHero];
            myChar.SaveEquipmentWeapon();
        }
        else if (myChar.SelectCostume[myChar.SelectHero] != -1)
        {
            myChar.EquipmentCostume[myChar.SelectHero] = myChar.SelectCostume[myChar.SelectHero];
            myChar.SaveEquipmentCostume();
        }
    }

    public void SkinSelectReset()
    {
        if (SkinInfoPanel.activeSelf)
        {
            Transform SkinWeaponContent = SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinWeapon_Background").Find("Scroll View").GetChild(0).GetChild(0);
            Transform SkinCostumeContent = SkinInfoPanel.transform.GetChild(0).GetChild(0).Find("SkinCostume_Background").Find("Scroll View").GetChild(0).GetChild(0);

            myChar.SelectHero = 0;
            SkinWeaponContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(-235, 0);
            SkinCostumeContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(-235, 0);
            for (int i = 0; i < myChar.HeroLv.Length; i++)
            {
                myChar.SelectWeapon[i] = -1;
                myChar.SelectCostume[i] = -1;
            }
        }        
    }

    public void PurchaseCompleat(int PackageNum)
    {
        GachaPanel.SetActive(true);
        GachaPanel.transform.Find("BackGround").GetComponent<Image>().enabled = true;
        PackageCheck = true;
        PurchasePackageNum = PackageNum;
        GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
    }

    //public void GachaGoodsCheck(int PackageNum)
    //{       
    //    PackageCheck = false;
    //    switch (PackageNum)     //box2_index에 10을 주면 뽑기 배열을 재배치안한다는 조건문
    //    {
    //        case 9:
    //            Box1_Index = 0;
    //            Box2_Index = 10;
    //            ItemCnt = 1;
    //            BoxNo = 9;
    //            break;
    //        case 10:
    //            Box1_Index = 0;
    //            Box2_Index = 10;
    //            ItemCnt = 1;
    //            BoxNo = 10;
    //            break;
    //        case 11:
    //            Box1_Index = 0;
    //            Box2_Index = 10;
    //            ItemCnt = 1;
    //            BoxNo = 11;
    //            break;
    //        case 12: 
    //            Box1_Index = 0;
    //            Box2_Index = 10;
    //            ItemCnt = 10;
    //            BoxNo = 12;
    //            break;
    //    }
    //    GachaPanel.SetActive(true);
    //    GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
    //    GachaChoice();
    //}
    public void AdCostCheck(int AdNum)
    {
        if (AdNum == 100)
        {
            GachaPanel.transform.Find("BackGround").GetComponent<Image>().enabled = true;
        }
        switch (AdNum)
        {
            case 100:
                PackageCheck = false;

                Box1_Index = myChar.GachaBox;
                Box2_Index = 10;
                ItemCnt = 1;
                BoxNo = 9;

                myChar.ADEquimentStartTime = UnbiasedTime.Instance.Now().AddSeconds(CashShopDataMgr.GetTemplate(9).CoolTime);
                myChar.ADEquimentCheck = true;
                GachaPanel.SetActive(true);
                GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
                GachaChoice();

                myChar.SaveADEquimentStartTime();
                myChar.SaveADEquimentCheck();
                break;
            case 200:
                if (!myChar.ADGemreductionCheck)
                {
                    myChar.ADGemStartTime = UnbiasedTime.Instance.Now().AddSeconds(CashShopDataMgr.GetTemplate(22).CoolTime);
                    myChar.ADGemreductionCheck = true;
                    myChar.Gem += CashShopDataMgr.GetTemplate(22).Dia;

                    myChar.SaveADGemStartTime();
                    myChar.SaveADGemreductionCheck();
                    myChar.SaveGem();
                }
                break;
            case 201:
                if (!myChar.ADSoulreductionCheck)
                {
                    myChar.ADSoulStartTime = UnbiasedTime.Instance.Now().AddSeconds(CashShopDataMgr.GetTemplate(23).CoolTime);
                    myChar.ADSoulreductionCheck = true;
                    myChar.SoulSpark += CashShopDataMgr.GetTemplate(23).Soul;

                    myChar.SaveADSoulStartTime();
                    myChar.SaveADSoulreductionCheck();
                    myChar.SaveSoulSpark();
                }
                break;
            case 202:
                if (!myChar.ADHeroreductionCheck)
                {
                    myChar.ADHeroStartTime = UnbiasedTime.Instance.Now().AddSeconds(CashShopDataMgr.GetTemplate(24).CoolTime);
                    myChar.ADHeroreductionCheck = true;
                    myChar.HeroHeart += CashShopDataMgr.GetTemplate(24).HeroToken;

                    myChar.SaveADHeroStartTime();
                    myChar.SaveADHeroreductionCheck();
                    myChar.SaveHeroHeart();
                }
                break;

        }

    }
    //캐시샵 광고카드뽑기 관련
    public void GachaADCheck()
    {
        PackageCheck = false;

        Box1_Index = myChar.GachaBox;
        Box2_Index = 10;
        ItemCnt = 1;
        BoxNo = 9;

        GachaPanel.SetActive(true);
        GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
        GachaChoice();
    }

    //캐시샵 미션카드 뽑기 관련
    public void GachaMissionCoinCheck()
    {
        StartCoroutine(Commingsoon());
        //if (myChar.MissionCoin > 0)
        //{
        //    PackageCheck = false;

        //    Box1_Index = 1;
        //    Box2_Index = 0;
        //    ItemCnt = 1;
        //    BoxNo = 10;

        //    GachaPanel.SetActive(true);
        //    GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
        //    GachaChoice();
        //    myChar.MissionCoin--;
        //}
    }
    //캐시샵 보석 카드뽑기 관련
    public void GachaGemCheck(int PackageNum)
    {
        GachaPanel.transform.Find("BackGround").GetComponent<Image>().enabled = true;
        //if (PackageNum == 9)
        //{
        //    PackageCheck = false;

        //    Box1_Index = 0;
        //    Box2_Index = 0;
        //    ItemCnt = 1;
        //    BoxNo = 9;

        //    GachaPanel.SetActive(true);
        //    GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
        //    GachaChoice();
        //}
        if (PackageNum == 11)
        {
            if (myChar.Gem >= CashShopDataMgr.GetTemplate(11).Cost)
            {
                PackageCheck = false;

                Box1_Index = myChar.GachaBox;
                Box2_Index = 10;
                ItemCnt = 1;
                BoxNo = 11;

                GachaPanel.SetActive(true);
                GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
                GachaChoice();
                myChar.Gem -= CashShopDataMgr.GetTemplate(11).Cost;
                
                myChar.SaveGem();

                FirebaseManager.firebaseManager.ShopProductPurchase("dia_Box");
            }
        }
        else if (PackageNum == 12)
        {
            if (myChar.Gem >= CashShopDataMgr.GetTemplate(12).Cost)
            {
                PackageCheck = false;
                Box1_Index = myChar.GachaBox;
                Box2_Index = 10;
                ItemCnt = 10;
                BoxNo = 12;

                GachaPanel.SetActive(true);
                GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
                GachaChoice();
                myChar.Gem -= CashShopDataMgr.GetTemplate(12).Cost;

                myChar.SaveGem();

                FirebaseManager.firebaseManager.ShopProductPurchase("dia_10_Box");
            }
        }        
    }
    public void SlotPurchase()
    {
        if (myChar.SlotPackCnt == 1)
        {
            if (myChar.Gem >= CashShopDataMgr.GetTemplate(7).Cost)
            {
                myChar.MachinSlotCnt++;
                myChar.Gem -= CashShopDataMgr.GetTemplate(7).Cost;
                myChar.SlotPackCnt--;

                myChar.SaveMachinSlotCnt();
                myChar.SaveGem();
                myChar.SaveSlotPackCnt();
                FirebaseManager.firebaseManager.ShopProductPurchase("Slot_1");
            }
        }
        else if (myChar.SlotPackCnt == 2)
        {
            if (myChar.Gem >= CashShopDataMgr.GetTemplate(6).Cost)
            {
                myChar.MachinSlotCnt++;
                myChar.Gem -= CashShopDataMgr.GetTemplate(6).Cost;
                myChar.SlotPackCnt--;

                myChar.SaveMachinSlotCnt();
                myChar.SaveGem();
                myChar.SaveSlotPackCnt();
                FirebaseManager.firebaseManager.ShopProductPurchase("Slot_2");
            }            
        }

    }
    public void GemPurchaseCheck(int GemNum)
    {
        GemPurchaseNum = GemNum;
    }

    public void GemPurchase()
    {
        switch (BasicProductCnt)
        {
            case 0:
                if (myChar.Dia_1_FirstCheck <= 0)
                {
                    myChar.Gem += (CashShopDataMgr.GetTemplate(13).Dia * 2);
                    myChar.SaveGem();
                    myChar.Dia_1_FirstCheck++;
                    myChar.SaveDia_1_FirstCheck();
                }
                else
                {
                    myChar.Gem += CashShopDataMgr.GetTemplate(13).Dia;
                    myChar.SaveGem();
                    myChar.Dia_1_FirstCheck++;
                    myChar.SaveDia_1_FirstCheck();
                }                
                break;
            case 1:
                if (myChar.Dia_2_FirstCheck <= 0)
                {
                    myChar.Gem += (CashShopDataMgr.GetTemplate(14).Dia * 2);
                    myChar.SaveGem();
                    myChar.Dia_2_FirstCheck++;
                    myChar.SaveDia_2_FirstCheck();
                }
                else
                {
                    myChar.Gem += CashShopDataMgr.GetTemplate(14).Dia;
                    myChar.SaveGem();
                    myChar.Dia_2_FirstCheck++;
                    myChar.SaveDia_2_FirstCheck();
                }
                break;
            case 2:
                if (myChar.Dia_3_FirstCheck <= 0)
                {
                    myChar.Gem += (CashShopDataMgr.GetTemplate(15).Dia * 2);
                    myChar.SaveGem();
                    myChar.Dia_3_FirstCheck++;
                    myChar.SaveDia_3_FirstCheck();
                }
                else
                {
                    myChar.Gem += CashShopDataMgr.GetTemplate(15).Dia;
                    myChar.SaveGem();
                    myChar.Dia_3_FirstCheck++;
                    myChar.SaveDia_3_FirstCheck();
                }
                break;
            case 3:
                if (myChar.Gem >= CashShopDataMgr.GetTemplate(16).Cost)
                {
                    myChar.SoulSpark += CashShopDataMgr.GetTemplate(16).Soul;
                    myChar.Gem -= CashShopDataMgr.GetTemplate(16).Cost;

                    myChar.SaveSoulSpark();
                    myChar.SaveGem();
                    BasicProductPanel.SetActive(false);
                    FirebaseManager.firebaseManager.ShopProductPurchase("SoulFlame_1");
                }
                break;
            case 4:
                if (myChar.Gem >= CashShopDataMgr.GetTemplate(17).Cost)
                {
                    myChar.SoulSpark += CashShopDataMgr.GetTemplate(17).Soul;
                    myChar.Gem -= CashShopDataMgr.GetTemplate(17).Cost;

                    myChar.SaveSoulSpark();
                    myChar.SaveGem();
                    BasicProductPanel.SetActive(false);
                    FirebaseManager.firebaseManager.ShopProductPurchase("SoulFlame_2");
                }
                break;
            case 5:
                if (myChar.Gem >= CashShopDataMgr.GetTemplate(18).Cost)
                {
                    myChar.SoulSpark += CashShopDataMgr.GetTemplate(18).Soul;
                    myChar.Gem -= CashShopDataMgr.GetTemplate(18).Cost;

                    myChar.SaveSoulSpark();
                    myChar.SaveGem();
                    BasicProductPanel.SetActive(false);
                    FirebaseManager.firebaseManager.ShopProductPurchase("SoulFlame_3");
                }
                break;
            case 6:
                if (myChar.Gem >= CashShopDataMgr.GetTemplate(19).Cost)
                {
                    myChar.HeroHeart += CashShopDataMgr.GetTemplate(1000 + ((StageClearCheck - 1) * 3)).HeroToken;
                    myChar.Gem -= CashShopDataMgr.GetTemplate(19).Cost;

                    myChar.SaveHeroHeart();
                    myChar.SaveGem();
                    BasicProductPanel.SetActive(false);
                    FirebaseManager.firebaseManager.ShopProductPurchase("HeroToken_1");
                }
                break;
            case 7:
                if (myChar.Gem >= CashShopDataMgr.GetTemplate(20).Cost)
                {
                    myChar.HeroHeart += CashShopDataMgr.GetTemplate(1001 + ((StageClearCheck - 1) * 3)).HeroToken;
                    myChar.Gem -= CashShopDataMgr.GetTemplate(20).Cost;

                    myChar.SaveHeroHeart();
                    myChar.SaveGem();
                    BasicProductPanel.SetActive(false);
                    FirebaseManager.firebaseManager.ShopProductPurchase("HeroToken_2");
                }
                break;
            case 8:
                if (myChar.Gem >= CashShopDataMgr.GetTemplate(21).Cost)
                {
                    myChar.HeroHeart += CashShopDataMgr.GetTemplate(1002 + ((StageClearCheck - 1) * 3)).HeroToken;
                    myChar.Gem -= CashShopDataMgr.GetTemplate(21).Cost;

                    myChar.SaveHeroHeart();
                    myChar.SaveGem();
                    BasicProductPanel.SetActive(false);
                    FirebaseManager.firebaseManager.ShopProductPurchase("HeroToken_3");
                }
                break;
            case 9:
                if (myChar.Gem >= CashShopDataMgr.GetTemplate(25).Cost)
                {
                    //myChar.SlotCoin += CashShopDataMgr.GetTemplate(25).SlotCoin;
                    myChar.Key += CashShopDataMgr.GetTemplate(25).Key;
                    myChar.Gem -= CashShopDataMgr.GetTemplate(25).Cost;

                    myChar.SaveSlotCoin();
                    myChar.SaveKey();
                    myChar.SaveGem();
                    BasicProductPanel.SetActive(false);
                    FirebaseManager.firebaseManager.ShopProductPurchase("Key_RoulletteCoin_1");
                }
                break;
            case 10:
                if (myChar.Gem >= CashShopDataMgr.GetTemplate(26).Cost)
                {
                    //myChar.SlotCoin += CashShopDataMgr.GetTemplate(26).SlotCoin;
                    myChar.Key += CashShopDataMgr.GetTemplate(26).Key;
                    myChar.Gem -= CashShopDataMgr.GetTemplate(26).Cost;

                    myChar.SaveSlotCoin();
                    myChar.SaveKey();
                    myChar.SaveGem();
                    BasicProductPanel.SetActive(false);
                    FirebaseManager.firebaseManager.ShopProductPurchase("Key_RoulletteCoin_2");
                }
                break;
            case 11:
                if (myChar.Gem >= CashShopDataMgr.GetTemplate(27).Cost)
                {
                    //myChar.SlotCoin += CashShopDataMgr.GetTemplate(27).SlotCoin;
                    myChar.Key += CashShopDataMgr.GetTemplate(27).Key;
                    myChar.Gem -= CashShopDataMgr.GetTemplate(27).Cost;

                    myChar.SaveSlotCoin();
                    myChar.SaveKey();
                    myChar.SaveGem();
                    BasicProductPanel.SetActive(false);
                    FirebaseManager.firebaseManager.ShopProductPurchase("Key_RoulletteCoin_3");
                }
                break;
            default:
                break;
        }
        //if (GemPurchaseNum == 0)
        //{
        //    if (myChar.Gem >= 200)
        //    {
        //        myChar.SoulSpark += 50;

        //        myChar.Gem -= 200;
        //        BasicProductPanel.SetActive(false);
        //    }
        //}
        //else if (GemPurchaseNum == 1)
        //{
        //    if (myChar.Gem >= 380)
        //    {
        //        myChar.SoulSpark += 110;

        //        myChar.Gem -= 380;
        //        BasicProductPanel.SetActive(false);
        //    }
        //}
        //else if (GemPurchaseNum == 2)
        //{
        //    if (myChar.Gem >= 750)
        //    {
        //        myChar.SoulSpark += 230;

        //        myChar.Gem -= 750;
        //        BasicProductPanel.SetActive(false);
        //    }
        //}
        //else if (GemPurchaseNum == 3)
        //{
        //    if (myChar.Gem >= 200)
        //    {
        //        myChar.HeroHeart += 100;

        //        myChar.Gem -= 200;
        //        BasicProductPanel.SetActive(false);
        //    }
        //}
        //else if (GemPurchaseNum == 4)
        //{
        //    if (myChar.Gem >= 380)
        //    {
        //        myChar.HeroHeart += 220;

        //        myChar.Gem -= 380;
        //        BasicProductPanel.SetActive(false);
        //    }
        //}
        //else if (GemPurchaseNum == 5)
        //{
        //    if (myChar.Gem >= 750)
        //    {
        //        myChar.HeroHeart += 460;

        //        myChar.Gem -= 750;
        //        BasicProductPanel.SetActive(false);
        //    }
        //}
    }
    public void InventoryOpen_Btn(int SelectNum)
    {
        InventoryCheck = false;
        SelectInventoryArrayNum = SelectNum;
        if (SelectNum < 2)
        {
            if (!InventoryPanel.activeSelf)
            {
                InventoryPanel.gameObject.SetActive(true);
                PopUpSound();
            }
            else
            {
                if (InventoryPanel.activeSelf)
                {
                    if (myChar.InventoryItemNum[SelectInventoryArrayNum] > -1)
                    {
                        for (int i = 0; i < InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).childCount; i++)
                        {
                            if (InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<ForgeItemNumScript>().ItemIndex == myChar.InventoryItemNum[SelectInventoryArrayNum])
                            {
                                SelectInventoryNum = i;
                            }
                        }
                        InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").gameObject.SetActive(true);
                        SelectInventoryIndex = myChar.InventoryItemNum[SelectInventoryArrayNum];
                    }
                    else if (myChar.InventoryItemNum[SelectInventoryArrayNum] == -1)
                    {
                        SelectInventoryNum = -1;
                    }
                    ClickSound();
                }
            }
        }
        else
        {
            if (SelectNum == 2)
            {
                if (myChar.SlotPackCnt < 2)
                {
                    if (!InventoryPanel.activeSelf)
                    {
                        InventoryPanel.gameObject.SetActive(true);
                        PopUpSound();
                    }
                    else
                    {
                        if (InventoryPanel.activeSelf)
                        {
                            if (myChar.InventoryItemNum[SelectInventoryArrayNum] > -1)
                            {
                                for (int i = 0; i < InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).childCount; i++)
                                {
                                    if (InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<ForgeItemNumScript>().ItemIndex == myChar.InventoryItemNum[SelectInventoryArrayNum])
                                    {
                                        SelectInventoryNum = i;
                                    }
                                }
                                InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").gameObject.SetActive(true);
                                SelectInventoryIndex = myChar.InventoryItemNum[SelectInventoryArrayNum];
                            }
                            else if (myChar.InventoryItemNum[SelectInventoryArrayNum] == -1)
                            {
                                SelectInventoryNum = -1;
                            }
                        }
                        ClickSound();
                    }
                }
            }
            else if (SelectNum == 3)
            {
                if (myChar.SlotPackCnt < 1)
                {
                    Debug.Log(11);
                    if (!InventoryPanel.activeSelf)
                    {
                        InventoryPanel.gameObject.SetActive(true);
                    }
                    else
                    {
                        if (InventoryPanel.activeSelf)
                        {
                            if (myChar.InventoryItemNum[SelectInventoryArrayNum] > -1)
                            {
                                for (int i = 0; i < InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).childCount; i++)
                                {
                                    if (InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<ForgeItemNumScript>().ItemIndex == myChar.InventoryItemNum[SelectInventoryArrayNum])
                                    {
                                        SelectInventoryNum = i;
                                    }
                                }
                                InventoryPanel.transform.Find("InventoryEquipmentInfo_Panel").gameObject.SetActive(true);
                                SelectInventoryIndex = myChar.InventoryItemNum[SelectInventoryArrayNum];
                            }
                            else if (myChar.InventoryItemNum[SelectInventoryArrayNum] == -1)
                            {
                                SelectInventoryNum = -1;
                            }
                        }
                    }
                }
            }
        }
       
    }
    public void InventoryClose_Btn()
    {
        ////InventoryArrangement = false;
        //InventoryCheck = false;
        //SelectInventoryArrayNum = -1;
        SelectInventoryNum = -1;
    }
    public void InventoryItemOn()
    {
        if (myChar.InventoryItemNum[SelectInventoryArrayNum] == -1)
        {
            myChar.InventoryItemNum[SelectInventoryArrayNum] = SelectInventoryIndex;

            InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(SelectInventoryNum).gameObject.SetActive(false);
        }
        else
        {
            for (int i = 0; i < InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).childCount; i++)
            {
                if (InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<ForgeItemNumScript>().ItemIndex == myChar.InventoryItemNum[SelectInventoryArrayNum])
                {
                    InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(true);
                }
            }
            myChar.InventoryItemNum[SelectInventoryArrayNum] = SelectInventoryIndex;

            InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(SelectInventoryNum).gameObject.SetActive(false);
        }
        myChar.SaveInventoryItemNum();
        SelectInventoryNum = -1;
    }

    public void InventoryItemOff()
    {
        for (int i = 0; i < InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).childCount; i++)
        {
            if (InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<ForgeItemNumScript>().ItemIndex == myChar.InventoryItemNum[SelectInventoryArrayNum])
            {
                InventoryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(true);
            }
        }
        myChar.InventoryItemNum[SelectInventoryArrayNum] = -1;
        myChar.SaveInventoryItemNum();
        SelectInventoryNum = -1;
    }

    public void BlackSmitCompletClose_Btn()
    {
        myChar.Crafting = false;
        myChar.CraftingItemNum = -1;
        myChar.ADreductionCheck = false;
        myChar.CompleteCheck = false;
        myChar.CraftingItemPickupCheck = false;
        EquipmentArrangement = false;

        myChar.SaveCrafting();
        myChar.SaveCraftingItemNum();
        myChar.SaveADreductionCheck();

        if (ForgeWindowNum == 1)
        {
            EquipmentSortNum.Clear();
            if (Nomal > 0)
            {
                Nomal = 0;
                Rare = 0;
                Unique = 0;
            }
            for (int i = 0; i < myChar.EquipmentAll.Length; i++)
            {
                switch (myChar.itemDataMgr.GetTemplate(i).Grade)
                {
                    case 0:
                        Nomal++;
                        break;
                    case 1:
                        Rare++;
                        break;
                    case 2:
                        Unique++;
                        break;
                }
                EquipmentSortNum.Add(i);
            }
            for (int i = 0; i < myChar.EquipmentAll.Length; i++)
            {
                if (myChar.EquipmentAll[i] > -1)
                {
                    int Cnt;
                    Cnt = EquipmentSortNum[i];                 //Cnt에 현재 i배열값을넣어두고
                    EquipmentSortNum.Remove(EquipmentSortNum[i]);     //해당 부분을 지움(Cnt를 안넣어두면 제거된 숫자를 찾을 수 없음)
                    EquipmentSortNum.Insert(0, Cnt);           //그리고 다시 배열 0번째에 Cnt값을 넣어서 최상단으로 배치해줌
                                                              //Debug.Log("RemoveAt" + EquipmentSortNum[i + 1]);
                }
                if (i == myChar.EquipmentAll.Length - 1)
                {
                    EquipmentCheck = true;
                }
            }

            for (int i = 0; i < myChar.EquipmentAll.Length; i++)
            {
                if (BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").childCount > i)
                {
                    BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).gameObject.SetActive(true);
                    BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<ForgeItemNumScript>().ItemIndex = EquipmentSortNum[i];
                    BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + EquipmentSortNum[i]);

                    switch (myChar.itemDataMgr.GetTemplate(EquipmentSortNum[i]).Grade)
                    {
                        case 0:
                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 0);
                            break;
                        case 1:
                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 1);
                            break;
                        case 2:
                            BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 2);
                            break;
                    }
                    if (myChar.EquipmentAll[EquipmentSortNum[i]] != -1)
                    {
                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(false);
                    }
                    else
                    {
                        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetChild(i).Find("DisablePanel").gameObject.SetActive(true);
                    }
                }
                else
                {
                    GameObject ForgeUI = Instantiate(ForgeBtn_UI, transform.position, Quaternion.identity);
                    ForgeUI.transform.parent = BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").transform;
                    ForgeUI.transform.localScale = new Vector3(1, 1, 1);
                    ForgeUI.GetComponent<ForgeItemNumScript>().ItemIndex = EquipmentSortNum[i];
                    ForgeUI.transform.Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + EquipmentSortNum[i]);

                    switch (myChar.itemDataMgr.GetTemplate(EquipmentSortNum[i]).Grade)
                    {
                        case 0:
                            ForgeUI.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 0);
                            break;
                        case 1:
                            ForgeUI.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 1);
                            break;
                        case 2:
                            ForgeUI.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 2);
                            break;
                    }
                    if (myChar.EquipmentAll[EquipmentSortNum[i]] != -1)
                    {
                        ForgeUI.transform.Find("DisablePanel").gameObject.SetActive(false);
                    }
                    else
                    {
                        ForgeUI.transform.Find("DisablePanel").gameObject.SetActive(true);
                    }
                }

                if (i == myChar.EquipmentAll.Length - 1)
                {
                    EquipmentArrangement = true;
                }
            }
        }             
    }
    private void GoodsUI_CHeck()
    {
        Gem_UI.GetComponentInChildren<Text>().text = myChar.Gem.ToString();
        Soul_UI.GetComponentInChildren<Text>().text = myChar.SoulSpark.ToString();
        HeroCoin_UI.GetComponentInChildren<Text>().text = myChar.HeroHeart.ToString();
    }

    public void ClickSound()
    {
        SoundManager.Instance.PlaySfx(16);
    }
    public void CloseSound()
    {
        SoundManager.Instance.PlaySfx(17);
    }
    public void PopUpSound()
    {
        SoundManager.Instance.PlaySfx(18);
    }
    public void DrawAllSound()
    {
        SoundManager.Instance.PlaySfx(44);
    }
    public void HeroTransitionSound()
    {
        SoundManager.Instance.PlaySfx(51);
    }
    public void HeroRankUpSound()
    {
        SoundManager.Instance.PlaySfx(50);
    }
    public void DiaButtonSound()
    {
        SoundManager.Instance.PlaySfx(49);
    }

    public void EffectSoundMute()
    {
        myChar.muteEffectSound = true;
        myChar.SavemuteEffectSound();
    }
    public void EffectSoundPlay()
    {
        myChar.muteEffectSound = false;
        myChar.SavemuteEffectSound();
    }
    public void BGMSoundMute()
    {
        myChar.muteBGM = true;
        myChar.SavemuteBGM();
    }
    public void BGMSoundPlay()
    {
        myChar.muteBGM = false;
        myChar.SavemuteBGM();
    }
    private void HeroUpgrade(int Cost)
    {
        if (myChar.HeroHeart >= Cost)
        {
            myChar.HeroHeart -= Cost;
            myChar.HeroLv[myChar.SelectHero]++;
            switch (myChar.SelectHero)
            {
                case 0:
                    FirebaseManager.firebaseManager.LobbyHeroPurchaseUpgrade("Knight", 0, Cost);
                    break;
                case 1:
                    FirebaseManager.firebaseManager.LobbyHeroPurchaseUpgrade("Warrior", 0, Cost);
                    break;
                case 2:
                    FirebaseManager.firebaseManager.LobbyHeroPurchaseUpgrade("Archer", 0, Cost);
                    break;
                case 3:
                    FirebaseManager.firebaseManager.LobbyHeroPurchaseUpgrade("Wizard", 0, Cost);
                    break;
                case 4:
                    FirebaseManager.firebaseManager.LobbyHeroPurchaseUpgrade("Ninja", 0, Cost);
                    break;
                default:
                    break;
            }
            myChar.SaveHeroLv();
            myChar.SaveHeroHeart();
        }
    }
    public void HeroLvCheck()
    {
        HeroUpgrade(myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero] + 1).Cost);

        //switch (myChar.HeroLv[myChar.SelectHero])
        //{
        //    case 0:
        //        HeroUpgrade(myChar.HeroDataMgr.GetTemplate(1).Cost);
        //        break;
        //    case 1:
        //        HeroUpgrade(myChar.HeroDataMgr.GetTemplate(2).Cost);
        //        break;
        //    case 2:
        //        HeroUpgrade(myChar.HeroDataMgr.GetTemplate(3).Cost);
        //        break;
        //    case 3:
        //        HeroUpgrade(myChar.HeroDataMgr.GetTemplate(4).Cost);
        //        break;
        //    case 4:
        //        HeroUpgrade(myChar.HeroDataMgr.GetTemplate(5).Cost);
        //        break;
        //}
    }
    public void HeroUpgradeCheck()
    {
        if (myChar.HeroPurchase[myChar.SelectHero])
        {
            Hero_UP_Btn.transform.GetChild(2).gameObject.SetActive(false);
            Hero_UP_Btn.transform.GetChild(3).gameObject.SetActive(false);
            Hero_UP_Btn.transform.GetChild(4).gameObject.SetActive(false);

            if (myChar.HeroLv[myChar.SelectHero] < (myChar.HeroDataMgr.GetCount() - 1))
            {
                HeroUpgradeCostCheck(myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero] + 1).Cost);
            }
            else
            {
                Hero_UP_Btn.transform.GetChild(0).gameObject.SetActive(false);
                Hero_UP_Btn.transform.GetChild(1).gameObject.SetActive(false);
                Hero_UP_Btn.transform.GetChild(4).gameObject.SetActive(true);
                HeroUI_MaxLvText.text = myChar.TextDataMgr.GetTemplate(9205).Content[myChar.LanguageNum].Replace("\\n", "\n");
            }
            //switch (myChar.HeroLv[myChar.SelectHero])
            //{
            //    case 0:
            //        HeroUpgradeCostCheck(myChar.HeroDataMgr.GetTemplate(1).Cost);
            //        break;
            //    case 1:
            //        HeroUpgradeCostCheck(myChar.HeroDataMgr.GetTemplate(2).Cost);
            //        break;
            //    case 2:
            //        HeroUpgradeCostCheck(myChar.HeroDataMgr.GetTemplate(3).Cost);
            //        break;
            //    case 3:
            //        HeroUpgradeCostCheck(myChar.HeroDataMgr.GetTemplate(4).Cost);
            //        break;
            //    case 4:
            //        HeroUpgradeCostCheck(myChar.HeroDataMgr.GetTemplate(5).Cost);
            //        break;
            //    case 5:
            //        //히어로 업그레이드 Max일때 업그레이드 버튼 안보이게해줌
            //        Hero_UP_Btn.transform.GetChild(0).gameObject.SetActive(false);
            //        Hero_UP_Btn.transform.GetChild(1).gameObject.SetActive(false);
            //        Hero_UP_Btn.transform.GetChild(4).gameObject.SetActive(true);
            //        break;
            //}
        }
        else
        {
            if (myChar.Gem >= myChar.HeroDataMgr.GetTemplate(0).Cost)
            {
                Hero_UP_Btn.transform.GetChild(0).gameObject.SetActive(false);
                Hero_UP_Btn.transform.GetChild(1).gameObject.SetActive(false);
                Hero_UP_Btn.transform.GetChild(2).gameObject.SetActive(true);
                Hero_UP_Btn.transform.GetChild(2).GetComponentInChildren<Text>().text = myChar.HeroDataMgr.GetTemplate(0).Cost.ToString();
                Hero_UP_Btn.transform.GetChild(3).gameObject.SetActive(false);
                Hero_UP_Btn.transform.GetChild(4).gameObject.SetActive(false);
            }
            else
            {
                Hero_UP_Btn.transform.GetChild(0).gameObject.SetActive(false);
                Hero_UP_Btn.transform.GetChild(1).gameObject.SetActive(false);
                Hero_UP_Btn.transform.GetChild(2).gameObject.SetActive(false);
                Hero_UP_Btn.transform.GetChild(3).gameObject.SetActive(true);
                Hero_UP_Btn.transform.GetChild(4).gameObject.SetActive(false);
                Hero_UP_Btn.transform.GetChild(3).GetComponentInChildren<Text>().text = myChar.HeroDataMgr.GetTemplate(0).Cost.ToString();
            }
            
        }
    }
    private void ForgeEquipmentGageCheck(int EquipmentNum, bool Silver1, bool Silver2, bool Silver3, bool Gold1, bool Gold2, bool Gold3, int Atk, float ASPD, float Range, int HP)
    {
        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Panel").GetChild(0).GetChild(0).gameObject.SetActive(Silver1);
        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Panel").GetChild(1).GetChild(0).gameObject.SetActive(Silver2);
        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Panel").GetChild(2).GetChild(0).gameObject.SetActive(Silver3);
        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Panel").GetChild(0).GetChild(1).gameObject.SetActive(Gold1);
        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Panel").GetChild(1).GetChild(1).gameObject.SetActive(Gold2);
        BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Grade_Panel").GetChild(2).GetChild(1).gameObject.SetActive(Gold3);
        ItemAtk = Atk;
        ItemASPD = ASPD;
        ItemRange = Range;
        ItemHP = HP;
        if (EquipmentNum != -1)
        {
            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = "Lv. " + ItemAtk.ToString();
            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = "Lv. " + ItemASPD.ToString();
            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = "Lv. " + ItemRange.ToString();
            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = "Lv. " + ItemHP.ToString();

        }
        else
        {
            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = "Lv. ?";
            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = "Lv. ?";
            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = "Lv. ?";
            BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = "Lv. ?";
        }
    }

    private void HeroUpgradeCostCheck(int Cnt)
    {
        if (myChar.HeroHeart >= Cnt)
        {
            Hero_UP_Btn.transform.GetChild(0).gameObject.SetActive(true);
            Hero_UP_Btn.transform.GetChild(1).gameObject.SetActive(false);
            Hero_UP_Btn.transform.GetChild(0).GetComponentInChildren<Text>().text = Cnt.ToString();
            HeroUI_LvUpText.text = myChar.TextDataMgr.GetTemplate(9204).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
        else
        {
            Hero_UP_Btn.transform.GetChild(0).gameObject.SetActive(false);
            Hero_UP_Btn.transform.GetChild(1).gameObject.SetActive(true);
            Hero_UP_Btn.transform.GetChild(1).GetComponentInChildren<Text>().text = Cnt.ToString();
        }
    }
    
    public void DayCheck()
    {
        if (AttendanceWindow.activeSelf == true)
        {
            //다이아 30일치 모두 수령했을때 리셋해주는 코드
            if (int.Parse(myChar.ContinuAttendanceCheck.AddDays(1).ToString("yyyyMMdd")) - int.Parse(UnbiasedTime.Instance.Now().ToString("yyyyMMdd")) <= 0)
            {
                if (myChar.AttendanceDay >= 30)
                {
                    myChar.AttendanceDay = 0;
                }
            }

            //Dia 수령 이미지로 보여주기위함
            for (int i = 0; i < 30; i++)
            {
                if (i >= myChar.AttendanceDay)
                {
                    AttendanceWindow.transform.GetChild(0).Find("DiaBoard").GetChild(i).Find("Image").gameObject.SetActive(false);
                }
                else
                {
                    AttendanceWindow.transform.GetChild(0).Find("DiaBoard").GetChild(i).Find("Image").gameObject.SetActive(true);
                }
            }
        }
        else
        {
            if (myChar.Attendancereward)
            {
                AttendanceNotice_2.SetActive(false);
                AttendanceNotice_1.SetActive(false);

                //Debug.Log(UnbiasedTime.Instance.Now().AddDays(DayTest).ToString("yyyyMMdd") + " ////" + myChar.ContinuAttendanceCheck.AddDays(1).ToString("yyyyMMdd"));
                if (int.Parse(myChar.ContinuAttendanceCheck.AddDays(1).ToString("yyyyMMdd")) - int.Parse(UnbiasedTime.Instance.Now().ToString("yyyyMMdd")) <= 0)
                {
                    myChar.Attendancereward = false;
                }
            }
            else
            {
                AttendanceNotice_2.SetActive(true);
                AttendanceNotice_1.SetActive(true);
            }
        }
    }
    public void Reward(GameObject Panel)
    {
        if (int.Parse(myChar.ContinuAttendanceCheck.AddDays(1).ToString("yyyyMMdd")) - int.Parse(UnbiasedTime.Instance.Now().ToString("yyyyMMdd")) <= 0)
        {
            myChar.AttendanceDay++;
            myChar.TotalAttendanceDay++;

            myChar.Gem += AttendanceDataMgr.GetTemplate(myChar.AttendanceDay).Dia;
            myChar.Attendancereward = true;
            myChar.ContinuAttendanceCheck = UnbiasedTime.Instance.Now();

            myChar.SaveAttendanceDay();
            myChar.SaveTotalAttendanceDay();
            myChar.SaveGem();
            myChar.SaveAttendancereward();
            myChar.SaveContinuAttendanceCheck();
            ClickSound();
        }
        else
        {
            Panel.SetActive(false);
            CloseSound();
        }
    }


    private void SelectCharacterCheck()
    {
        if (-540 < HeroCharacterViewport.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x && HeroCharacterViewport.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x < 540)
        {
            myChar.SelectHero = 0;
        }
        else if (-540 < HeroCharacterViewport.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition.x && HeroCharacterViewport.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition.x < 540)
        {
            myChar.SelectHero = 1;
        }
        else if (-540 < HeroCharacterViewport.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition.x && HeroCharacterViewport.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition.x < 540)
        {
            myChar.SelectHero = 2;
        }
        else if (-540 < HeroCharacterViewport.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition.x && HeroCharacterViewport.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition.x < 540)
        {
            myChar.SelectHero = 3;
        }
        else if (-540 < HeroCharacterViewport.transform.GetChild(4).GetComponent<RectTransform>().anchoredPosition.x && HeroCharacterViewport.transform.GetChild(4).GetComponent<RectTransform>().anchoredPosition.x < 540)
        {
            myChar.SelectHero = 4;
        }
        for (int i = 0; i < HeroCharacterViewport.transform.childCount; i++)
        {
            HeroCharacterViewport.transform.GetChild(i).GetChild(0).GetComponent<Animator>().SetBool("Throne", false);
            HeroCharacterViewport.transform.GetChild(i).GetChild(0).Find("Sit").gameObject.SetActive(false);
            HeroCharacterViewport.transform.GetChild(i).GetChild(0).Find("Crown").gameObject.SetActive(false);

            //if (i == myChar.EnthroneHeroNum)
            //{
            //    HeroCharacterViewport.transform.GetChild(i).GetChild(0).GetComponent<Animator>().SetBool("Throne", true);
            //    HeroCharacterViewport.transform.GetChild(i).GetChild(0).Find("Sit").gameObject.SetActive(true);
            //    HeroCharacterViewport.transform.GetChild(i).GetChild(0).Find("Crown").gameObject.SetActive(true);
            //}
            //else
            //{
            //    HeroCharacterViewport.transform.GetChild(i).GetChild(0).GetComponent<Animator>().SetBool("Throne", false);
            //    HeroCharacterViewport.transform.GetChild(i).GetChild(0).Find("Sit").gameObject.SetActive(false);
            //    HeroCharacterViewport.transform.GetChild(i).GetChild(0).Find("Crown").gameObject.SetActive(false);
            //}
        }
    }


    public void HeroPurchased()
    {
        if (myChar.Gem >= myChar.HeroDataMgr.GetTemplate(0).Cost)
        {
            myChar.HeroPurchase[myChar.SelectHero] = true;
            myChar.Gem -= myChar.HeroDataMgr.GetTemplate(0).Cost;
            switch (myChar.SelectHero)
            {
                case 0:
                    FirebaseManager.firebaseManager.LobbyHeroPurchaseUpgrade("Knight", myChar.HeroDataMgr.GetTemplate(0).Cost, 0);
                    break;
                case 1:
                    FirebaseManager.firebaseManager.LobbyHeroPurchaseUpgrade("Warrior", myChar.HeroDataMgr.GetTemplate(0).Cost, 0);
                    break;
                case 2:
                    FirebaseManager.firebaseManager.LobbyHeroPurchaseUpgrade("Archer", myChar.HeroDataMgr.GetTemplate(0).Cost, 0);
                    break;
                case 3:
                    FirebaseManager.firebaseManager.LobbyHeroPurchaseUpgrade("Wizard", myChar.HeroDataMgr.GetTemplate(0).Cost, 0);
                    break;
                case 4:
                    FirebaseManager.firebaseManager.LobbyHeroPurchaseUpgrade("Ninja", myChar.HeroDataMgr.GetTemplate(0).Cost, 0);
                    break;
            }

            myChar.SaveHeroPurchase();
            myChar.SaveGem();
        }        
    }
    
    public void CashBtn_PriceCheck()
    {
        for (int i = 0; i < 3; i++)
        {
            CashBtn_Spark[i].GetComponent<Text>().text = CashShopDataMgr.GetTemplate(16 + i).Cost.ToString();
            CashBtn_HeroHeart[i].GetComponent<Text>().text = CashShopDataMgr.GetTemplate(19 + i).Cost.ToString();
            CashBtn_KeyAndSlot[i].GetComponent<Text>().text = CashShopDataMgr.GetTemplate(25 + i).Cost.ToString();
        }
        
    }
    public void StartCheck(int Cnt)
    {
        if (Cnt == 0)
        {
            StartTF = false;
            CheckCnt = 0;
        }
        else if (Cnt == 1)
        {
            StartTF = true;
            if (CheckCnt <= 0)
            {
                CheckCnt++;
            }
            
        }
    }
    public void HeroStartCheck()
    {
        if (StartTF)
        {
            if (!myChar.HeroPurchase[myChar.SelectHero])
            {
                if (CheckCnt <= 0)
                {
                    CheckCnt++;
                }
                else if (CheckCnt > 0)
                {
                    UnpurchasedInfo_Panel.SetActive(true);
                }
                
            }

        }
    }
    public void SelectBtn_InfoNum(int Num)
    {
        ContentInfoNum = Num;
    }

    //private void ThroneItemGageCheck(int EquipmentNum, bool Silver1, bool Silver2, bool Silver3, bool Gold1, bool Gold2, bool Gold3, int Atk, float ASPD, float Range, int HP)
    //{
    //    ThroneItemInfo_Panel.transform.GetChild(0).Find("Grade_Panel").GetChild(0).GetChild(0).gameObject.SetActive(Silver1);
    //    ThroneItemInfo_Panel.transform.GetChild(0).Find("Grade_Panel").GetChild(1).GetChild(0).gameObject.SetActive(Silver2);
    //    ThroneItemInfo_Panel.transform.GetChild(0).Find("Grade_Panel").GetChild(2).GetChild(0).gameObject.SetActive(Silver3);
    //    ThroneItemInfo_Panel.transform.GetChild(0).Find("Grade_Panel").GetChild(0).GetChild(1).gameObject.SetActive(Gold1);
    //    ThroneItemInfo_Panel.transform.GetChild(0).Find("Grade_Panel").GetChild(1).GetChild(1).gameObject.SetActive(Gold2);
    //    ThroneItemInfo_Panel.transform.GetChild(0).Find("Grade_Panel").GetChild(2).GetChild(1).gameObject.SetActive(Gold3);
    //    ItemAtk = Atk;
    //    ItemASPD = ASPD;
    //    ItemRange = Range;
    //    ItemHP = HP;

    //    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = "Lv. " + ItemAtk.ToString();
    //    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = "Lv. " + ItemASPD.ToString();
    //    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = "Lv. " + ItemRange.ToString();
    //    BlackSmithPanel.transform.Find("ForgeEquipmentInfo_Panel").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = "Lv. " + ItemHP.ToString();
    //}

    //public void ThroneItemInfo_Panel_Open(int Cnt)
    //{
    //    ThroneItem_Num = Cnt;
    //    if (myChar.ThroneItemList.Count > ThroneItem_Num)
    //    {
    //        ThroneItemInfo_Panel.SetActive(true);
    //        if (!ThroneItemCheck)
    //        {
    //            ThroneItemAtkGage.Clear();
    //            ThroneItemASPDGage.Clear();
    //            ThroneItemRangeGage.Clear();
    //            ThroneItemHPGage.Clear();
    //            for (int i = 0; i < 10; i++)
    //            {
    //                ThroneItemAtkGage.Add(ThroneItemInfo_Panel.transform.GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").Find("ItemAtkGage").transform.GetChild(i).gameObject);
    //                ThroneItemASPDGage.Add(ThroneItemInfo_Panel.transform.GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").Find("ItemASPDGage").transform.GetChild(i).gameObject);
    //                ThroneItemRangeGage.Add(ThroneItemInfo_Panel.transform.GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").Find("ItemRangeGage").transform.GetChild(i).gameObject);
    //                ThroneItemHPGage.Add(ThroneItemInfo_Panel.transform.GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").Find("ItemHPGage").transform.GetChild(i).gameObject);
    //            }
    //            ThroneItemCheck = true;
    //        }
    //    }
    //    else
    //    {
    //        ThroneItemInfo_Panel.SetActive(false);
    //    }
    //}

    //public void ThroneItemInfoCheck()
    //{
    //    if (ThroneItemInfo_Panel.activeSelf)
    //    {
    //        switch (myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Grade)
    //        {
    //            case 0:
    //                ThroneItemInfo_Panel.transform.GetChild(0).Find("ItemName_Text").GetComponent<Text>().text = "<color=#e4e4e4>" + myChar.TextDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num] + 101).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                ThroneItemInfo_Panel.transform.GetChild(0).Find("Grade_Text").GetComponent<Text>().text = "<color=#e4e4e4>" + myChar.TextDataMgr.GetTemplate(195).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                break;
    //            case 1:
    //                ThroneItemInfo_Panel.transform.GetChild(0).Find("ItemName_Text").GetComponent<Text>().text = "<color=#fff200>" + myChar.TextDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num] + 101).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                ThroneItemInfo_Panel.transform.GetChild(0).Find("Grade_Text").GetComponent<Text>().text = "<color=#fff200>" + myChar.TextDataMgr.GetTemplate(196).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                break;
    //            case 2:
    //                ThroneItemInfo_Panel.transform.GetChild(0).Find("ItemName_Text").GetComponent<Text>().text = "<color=#ff00fe>" + myChar.TextDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num] + 101).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                ThroneItemInfo_Panel.transform.GetChild(0).Find("Grade_Text").GetComponent<Text>().text = "<color=#ff00fe>" + myChar.TextDataMgr.GetTemplate(197).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
    //                break;
    //        }
    //        switch (myChar.ThroneItemLvList[ThroneItem_Num])   //아이템 강화수치에따라 강화등급(금,은,동)체크와 강화 수치 보여주는부분
    //        {
    //            case 0:
    //                ThroneItemGageCheck(myChar.ThroneItemList[ThroneItem_Num], false, false, false, false, false, false, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk0,
    //                    myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD0, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range0, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP0);
    //                break;
    //            case 1:
    //                ThroneItemGageCheck(myChar.ThroneItemList[ThroneItem_Num], true, false, false, false, false, false, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk1,
    //                    myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD1, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range1, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP1);
    //                break;
    //            case 2:
    //                ThroneItemGageCheck(myChar.ThroneItemList[ThroneItem_Num], true, true, false, false, false, false, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk2,
    //                    myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD2, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range2, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP2);
    //                break;
    //            case 3:
    //                ThroneItemGageCheck(myChar.ThroneItemList[ThroneItem_Num], true, true, true, false, false, false, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk3,
    //                    myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD3, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range3, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP3);
    //                break;
    //            case 4:
    //                ThroneItemGageCheck(myChar.ThroneItemList[ThroneItem_Num], true, true, true, true, false, false, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk4,
    //                    myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD4, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range4, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP4);
    //                break;
    //            case 5:
    //                ThroneItemGageCheck(myChar.ThroneItemList[ThroneItem_Num], true, true, true, true, true, false, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk5,
    //                    myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD5, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range5, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP5);
    //                break;
    //            case 6:
    //                ThroneItemGageCheck(myChar.ThroneItemList[ThroneItem_Num], true, true, true, true, true, true, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk6,
    //                    myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD6, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range6, myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP6);
    //                break;
    //        }

    //        switch (myChar.ThroneItemLvList[ThroneItem_Num])
    //        {
    //            case 0:
    //                ThroneItemAtk = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk0;
    //                ThroneItemASPD = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD0;
    //                ThroneItemRange = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range0;
    //                ThroneItemHP = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP0;
    //                break;
    //            case 1:
    //                ThroneItemAtk = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk1;
    //                ThroneItemASPD = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD1;
    //                ThroneItemRange = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range1;
    //                ThroneItemHP = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP1;
    //                break;
    //            case 2:
    //                ThroneItemAtk = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk2;
    //                ThroneItemASPD = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD2;
    //                ThroneItemRange = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range2;
    //                ThroneItemHP = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP2;
    //                break;
    //            case 3:
    //                ThroneItemAtk = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk3;
    //                ThroneItemASPD = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD3;
    //                ThroneItemRange = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range3;
    //                ThroneItemHP = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP3;
    //                break;
    //            case 4:
    //                ThroneItemAtk = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk4;
    //                ThroneItemASPD = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD4;
    //                ThroneItemRange = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range4;
    //                ThroneItemHP = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP4;
    //                break;
    //            case 5:
    //                ItemAtk = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk5;
    //                ThroneItemASPD = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD5;
    //                ThroneItemRange = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range5;
    //                ThroneItemHP = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP5;
    //                break;
    //            case 6:
    //                ThroneItemAtk = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Atk6;
    //                ThroneItemASPD = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).ASPD6;
    //                ThroneItemRange = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Range6;
    //                ThroneItemHP = myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).HP6;
    //                break;
    //        }
    //        for (int i = 0; i < 10; i++)    //아이템 강화수치에따라 게이지보여주는부분
    //        {
    //            if (i < ItemAtk)
    //            {
    //                ThroneItemAtkGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                ThroneItemAtkGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //            }

    //            if (i < ItemASPD)
    //            {
    //                ThroneItemASPDGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                ThroneItemASPDGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //            }
    //            if (i < ItemRange)
    //            {
    //                ThroneItemRangeGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                ThroneItemRangeGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //            }
    //            if (i < ItemHP)
    //            {
    //                ThroneItemHPGage[i].transform.GetChild(0).gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                ThroneItemHPGage[i].transform.GetChild(0).gameObject.SetActive(false);
    //            }
    //        }

    //        ThroneItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ItemAtk").GetComponentInChildren<Text>().text = "Lv. " + ThroneItemAtk.ToString();
    //        ThroneItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ItemASPD").GetComponentInChildren<Text>().text = "Lv. " + ThroneItemASPD.ToString();
    //        ThroneItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ItemRange").GetComponentInChildren<Text>().text = "Lv. " + ThroneItemRange.ToString();
    //        ThroneItemInfo_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ItemHP").GetComponentInChildren<Text>().text = "Lv. " + ThroneItemHP.ToString();
    //        //아이템 설명 부분
    //        switch (SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1).StatIndex)
    //        {
    //            case 0:
    //                AddText = null;
    //                break;
    //            case 1:     //공격력
    //                AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1).Value.ToString() + "%";
    //                break;
    //            case 4:     //속도
    //                AddText = myChar.TextDataMgr.GetTemplate(4).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1).Value.ToString() + "%";
    //                break;
    //            case 5:     //부활 횟수
    //                AddText = myChar.TextDataMgr.GetTemplate(5).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1).Value.ToString();
    //                break;
    //            case 6:     //하트 획득
    //                AddText = myChar.TextDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1).Value.ToString();
    //                break;
    //            case 8:     //높이
    //                AddText = myChar.TextDataMgr.GetTemplate(8).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1).Value.ToString();
    //                break;
    //            case 9:     //점프 횟수
    //                AddText = myChar.TextDataMgr.GetTemplate(9).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1).Value.ToString();
    //                break;
    //            case 10:    //튕김
    //                AddText = myChar.TextDataMgr.GetTemplate(10).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1).Value.ToString();
    //                break;
    //            case 11:    //확률
    //                AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1).Value.ToString() + "%";
    //                break;
    //            case 12:    //개수
    //                AddText = myChar.TextDataMgr.GetTemplate(12).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1).Value.ToString();
    //                break;
    //        }
    //        if (myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1 != 0)
    //        {
    //            string ItemInfo = myChar.TextDataMgr.GetTemplate((SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1).InfoIndex)).Content[myChar.LanguageNum].Replace("\\n", "\n");

    //            ThroneItemInfo_Panel.transform.GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text =
    //                ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
    //        }
    //        else if (myChar.itemDataMgr.GetTemplate(myChar.ThroneItemList[ThroneItem_Num]).Option1 == 0)
    //        {
    //            ThroneItemInfo_Panel.transform.GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("ItemInfo_Text").GetComponent<Text>().text = "-\n";
    //        }
    //    }
    //    else
    //    {
    //        ThroneItemCheck = false;
    //    }
    //}

    //public void ItemInfo_Panel_Open(int Cnt)
    //{
    //    if (!ItemInfo_Panel.activeSelf)
    //    {
    //        SelectMachineEquipmentNum = Cnt;
    //        if (myChar.LobbyMachineEquipmentNum[Cnt] > -1)
    //        {
    //            ItemInfo_Panel.SetActive(true);
    //        }
    //        return;
    //    }

    //    if (ItemInfo_Panel.activeSelf)
    //    {
    //        if (Cnt == SelectMachineEquipmentNum)
    //        {
    //            ItemInfo_Panel.SetActive(false);
    //        }
    //        else
    //        {
    //            SelectMachineEquipmentNum = Cnt;
    //        }
    //    }
    //}
    public void ForgeWindowCheck(int Cnt)
    {
        ForgeWindowNum = Cnt;
        if (Cnt == 1)
        {
            EquipmentArrangement = false;
            EquipmentCheck = false;
        }
        if (Cnt == 2)
        {
            EquipmentArrangement = false;
            EquipmentCheck = false;
        }
    }
    public void ForgeEquipmentWindowClose()
    {
        SelectForgeNum = -1;
        EquipmentArrangement = false;
    }
    public void ForgeSkip_Btn(int Type)
    {
        if (Type == 0)
        {            
            if (myChar.CraftingItemNum < 1000)
            {
                FirebaseManager.firebaseManager.ForgeItemShorten(myChar.itemDataMgr.GetTemplate(myChar.CraftingItemNum).Kor, "AD", 0);
            }
            else
            {
                FirebaseManager.firebaseManager.ForgeItemShorten(myChar.AntiqueitemDataMgr.GetTemplate(myChar.CraftingItemNum).Kor, "AD", 0);
            }
            
            myChar.CraftingTime -= BlackSmithDataMgr.GetTemplate(0).ADSkip;
            myChar.ADreductionCheck = true;

            myChar.SaveADreductionCheck();
            myChar.SaveCraftingTime();
        }
        else if(Type == 1)
        {
            if (myChar.SoulSpark >= BlackSmithDataMgr.GetTemplate(0).CHCost)
            {
                if (myChar.CraftingItemNum < 1000)
                {
                    FirebaseManager.firebaseManager.ForgeItemShorten(myChar.itemDataMgr.GetTemplate(myChar.CraftingItemNum).Kor, "Jewel", BlackSmithDataMgr.GetTemplate(0).CHCost);
                }
                else
                {
                    FirebaseManager.firebaseManager.ForgeItemShorten(myChar.AntiqueitemDataMgr.GetTemplate(myChar.CraftingItemNum).Kor, "Jewel", BlackSmithDataMgr.GetTemplate(0).CHCost);
                }
                //myChar.StartCraftingTime.AddSeconds(BlackSmithDataMgr.GetTemplate(0).CHSkip);
                myChar.CraftingTime -= BlackSmithDataMgr.GetTemplate(0).CHSkip;
                myChar.SoulSpark -= BlackSmithDataMgr.GetTemplate(0).CHCost;

                myChar.SaveCraftingTime();
                myChar.SaveSoulSpark();
            }            
        }
        
    }
    public void GachaBoxCheck()
    {
        myChar.GachaBox = 0;
        for (int i = 0; i < myChar.StageClearCheck.Length; i++)
        {
            if (myChar.StageClearCheck[i])
            {
                myChar.GachaBox++;
            }
        }
    }
    public void ForgeOpenCheck()
    {
        ForgeWindowNum = 1;
        SelectForgeNum = -1;
        EquipmentArrangement = false;
        EquipmentCheck = false;
        ActiveCheck = false;
        BlackSmithPanel.transform.GetChild(0).GetChild(0).GetChild(0).Find("Content").GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
    }
    public void BtnJumpOn(GameObject Btn)
    {
        Btn.GetComponent<Animator>().enabled = true;
    }
    public void TestSSSSS()
    {
        StartCoroutine(ADGachaTest());
    }
    
    public IEnumerator ADGachaTest()
    {
        yield return new WaitForSeconds(1f);
        GachaPanel.transform.Find("BackGround").GetComponent<Image>().enabled = true;

        PackageCheck = false;

        Box1_Index = myChar.GachaBox;
        Box2_Index = 0;
        ItemCnt = 1;
        BoxNo = 9;

        GachaPanel.SetActive(true);
        GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
        GachaChoice();
    }

    IEnumerator CardAllDraw(float Time)
    {
        yield return new WaitForSeconds(Time);
        for (int i = 0; i < ItemCnt; i++)
        {
            GachaPanel.transform.GetChild(0).Find("Card_Panel").GetChild(0).GetChild(i).GetChild(0).GetComponent<GachaCardScript>().CardOpenAnim = true;
        }
    }
    //void CheckDay() //출석체크, 일일업적 체크
    //{
    //    //if (myChar.tutorialNum == 0) return;
    //    myChar.NowDate = myChar.LoadNowDate();

    //    Debug.Log("<color=red>before : " + System.DateTime.Parse(myChar.NowDate).Day + ", now : " + UnbiasedTime.Instance.Now().Day + "</color>");

    //    int day = UnbiasedTime.Instance.Now().Day;
    //    int month = UnbiasedTime.Instance.Now().Month;

    //    if (day > System.DateTime.Parse(myChar.NowDate).Day || month > System.DateTime.Parse(myChar.NowDate).Month
    //         || UnbiasedTime.Instance.Now().Year > System.DateTime.Parse(myChar.NowDate).Year)
    //    {
    //        for (int nCnt = 0; nCnt < myChar.dailyQuest.Length; nCnt++)
    //        {
    //            myChar.dailyQuest[nCnt] = 0;
    //            myChar.dailyQuestClear[nCnt] = false;
    //            myChar.adCoinCount = 0;
    //            myChar.adDiaCount = 0;

    //            myChar.SaveDailyQuest(nCnt);
    //            myChar.SaveDailyQuestClear(nCnt);
    //            myChar.SaveAdDiaCount();
    //            myChar.SaveAdCoinCount();
    //        }

    //        if (myChar.buyDailyBasicPackage)
    //        {
    //            System.TimeSpan basicSpan = UnbiasedTime.Instance.Now() - System.DateTime.Parse(myChar.dailyBasicPackageDate);
    //            Debug.Log("<color=green>before : " + basicSpan.Days + "</color>");
    //            if (basicSpan.Days <= 30)
    //            {
    //                myChar.dia += 3;
    //                myChar.SaveDia();
    //                SetResource(false, 2);
    //            }
    //            else
    //            {
    //                myChar.buyDailyBasicPackage = false;
    //                myChar.SaveBuyDailyBasicPackage();
    //            }
    //        }

    //        if (myChar.buyDailyPremiumPackage)
    //        {
    //            System.TimeSpan premiumSpan = UnbiasedTime.Instance.Now() - System.DateTime.Parse(myChar.dailyBasicPackageDate);
    //            Debug.Log("<color=blue>before : " + premiumSpan.Days + "</color>");
    //            if (premiumSpan.Days <= 30)
    //            {
    //                myChar.dia += 5;
    //                myChar.SaveDia();
    //                SetResource(false, 2);
    //            }
    //            else
    //            {
    //                myChar.buyDailyPremiumPackage = false;
    //                myChar.SaveBuyDailyPremiumPackage();
    //            }
    //        }
    //    }

    //    if (myChar.buyAutoPlayDay > 0)
    //    {
    //        System.TimeSpan autoPlay = UnbiasedTime.Instance.Now() - System.DateTime.Parse(myChar.buyAutoPlayData);
    //        Debug.Log("<color=blue>before : " + autoPlay.Days + "</color>");

    //        if (autoPlay.TotalHours / 24 > myChar.buyAutoPlayDay)
    //        {
    //            myChar.buyAutoPlay7 = false;
    //            myChar.buyAutoPlay30 = false;
    //            myChar.buyAutoPlayDay = 0;

    //            myChar.SaveBuyAutoPlay7();
    //            myChar.SaveBuyAutoPlay30();
    //            myChar.SaveBuyAutoPlayDay();
    //        }
    //    }

    //    myChar.NowDate = UnbiasedTime.Instance.Now().ToString();
    //}
    private string Comma(int data)
    {
        return string.Format("{0:#,###}", data);
    }

    private void HeroWeaponeCheck()
    {
        if (myChar.SelectHero != 3)
        {
            HeroCharacterViewport.transform.GetChild(3).Find("Weapone").gameObject.SetActive(false);
        }
        else
        {
            HeroCharacterViewport.transform.GetChild(3).Find("Weapone").gameObject.SetActive(true);
        }
    }
    public void BlackSmithFirstEnd()
    {
        myChar.FirstBlackSmithCheck = false;
        myChar.SaveFirstBlackSmithCheck();
        BlackSmithInfoPanel.SetActive(false);
    }
    public void TutorialInput()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("StageScene"); //로드 (비동기)
                                                                       //SceneManager.LoadScene("StageScene");
        LoadingSceneManager.LoadScene("StageScene");
        myChar.Tutorial = true;
        myChar.InGameStart = true;
        myChar.ThroneBossKill = false;
        myChar.MultipleCheck = false;
        myChar.MHpMultiIndex = 0;
        //myChar.Chapter = 0;
        myChar.Stage = 0;
        myChar.SelectedStage = 0;
        myChar.Gain_Multiple = 1;
        myChar.Resurrection = false;
        myChar.Resetitem();
        myChar.Finished = false;
        myChar.CrownGetBgm = false;
        myChar.SeasonalShield = false;
        myChar.EnthroneCameraOn = false;
        myChar.ItemOwnCheck = false;

        myChar.Gain_Multiple = 1;

        for (int i = 0; i < myChar.InventoryItemNum.Length; i++)
        {
            if (myChar.InventoryItemNum[i] != -1)
            {
                myChar.EquipmentUsed.Add(myChar.InventoryItemNum[i]);
                myChar.EquipmentUsedTrueCheck.Add(false);
                myChar.PlayerEquipment.Add(myChar.InventoryItemNum[i]);
            }
        }
    }
    public void ResetBtn()
    {
        myChar.Tutorial = true;
        myChar.SelectHero = 0;
        myChar.EnthroneHeroNum = 1;
        myChar.Gem = 0;
        //myChar.Key = 0;
        //myChar.SlotCoin = 0;
        myChar.SoulSpark = 0;
        myChar.HeroHeart = 0;
        for (int i = 0; i < myChar.MachinSlotCnt; i++)
        {
            myChar.LobbyMachineEquipmentNum[i] = -1;
        }
        myChar.LanguageNum = 0;
        myChar.HeroLv = new ObscuredInt[] { 0, 0, 0, 0, 0 };
        myChar.HeroPurchase = new ObscuredBool[] { true, true, false, false, false };

        myChar.SaveTutorial();
        myChar.SaveSelectHero();
        myChar.SaveEnthroneHeroNum();
        myChar.SaveGem();
        myChar.SaveKey();
        myChar.SaveSlotCoin();
        myChar.SaveSoulSpark();
        myChar.SaveHeroHeart();
        myChar.SaveMachinSlotCnt();
        myChar.SaveLanguageNum();
        myChar.SaveHeroLv();
        myChar.SaveHeroPurchase();
    }

    public void PackClose_Btn()
    {
        if (BoxNo == 1 || BoxNo == 2)
        {
            if (SkinCard[0].transform.GetChild(0).GetComponent<GachaCardScript>().CardOpenCheck && SkinCard[1].transform.GetChild(0).GetComponent<GachaCardScript>().CardOpenCheck)
            {
                GachaPanel.SetActive(true);
                GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
                GachaChoice();
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    SkinCard[i].transform.GetChild(0).GetComponent<GachaCardScript>().CardOpenAnim = true;
                }
            }
        }
        else if (BoxNo == 3 || BoxNo == 4 || BoxNo == 5)
        {
            GachaPanel.SetActive(true);
            GachaTableCheck_1 = false; GachaTableCheck_2 = false; CardChoiceCheck = false;
            GachaChoice();
        }
        //if (BoxNo == 1 || BoxNo == 2)
        //{
        //    if (SkinCard[0].transform.GetChild(0).GetComponent<GachaCardScript>().CardOpenCheck && SkinCard[1].transform.GetChild(0).GetComponent<GachaCardScript>().CardOpenCheck)
        //    {
        //        ReGacha_Btn();
        //    }
        //    else
        //    {
        //        for (int i = 0; i < 2; i++)
        //        {
        //            SkinCard[i].transform.GetChild(0).GetComponent<GachaCardScript>().CardOpenAnim = true;
        //        }
        //    }
        //}
        //else if(BoxNo == 3 || BoxNo == 4 || BoxNo == 5)
        //{
        //    ReGacha_Btn();
        //}

    }
    public void CommingsoonInfo()
    {
        StartCoroutine(Commingsoon());
    }
    //public void TestGoldPlus()
    //{
    //    myChar.Gem += 10000;
    //    //myChar.SlotCoin += 10000;
    //    myChar.SoulSpark += 10000;
    //    myChar.HeroHeart += 10000;
    //}

    public IEnumerator Not_Enough_AD()
    {
        GameObject AD_Panel = Instantiate(Notice_Notenough, transform.position, Quaternion.identity);
        AD_Panel.transform.SetParent(ComingSoon_Parent_Panel.transform);
        AD_Panel.transform.localScale = new Vector3(1, 1, 1);
        AD_Panel.transform.localPosition = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(1f);
        Destroy(AD_Panel);
    }
    public IEnumerator Commingsoon()
    {
        GameObject ComingSoon_Panel = Instantiate(Notice_ComingSoon, transform.position, Quaternion.identity);
        ComingSoon_Panel.transform.SetParent(ComingSoon_Parent_Panel.transform);
        ComingSoon_Panel.transform.GetChild(0).GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(9300).Content[myChar.LanguageNum].Replace("\\n", "\n");
        //ComingSoon_Panel.transform.parent = ComingSoon_Panel.transform;
        ComingSoon_Panel.transform.localScale = new Vector3(1, 1, 1);
        ComingSoon_Panel.transform.localPosition = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(1f);
        Destroy(ComingSoon_Panel);
    }
    IEnumerator Count(bool _plus, float target, float current)
    {
        float duration = 0.7f; // 카운팅에 걸리는 시간 설정. 

        if (_plus)
        {
            //Debug.Log("plus");
            float offset = (target - current) / duration;

            //Debug.Log(current + ", " + target);

            while (current < target)
            {
                current += offset * Time.deltaTime;
                Gem_UI.transform.GetChild(0).GetComponent<Text>().text = current.ToString("F0");
                yield return null;
            }
        }
        else
        {
            Debug.Log("minus");
            float offset = (current - target) / duration;

            while (current > target)
            {
                current -= offset * Time.deltaTime;
                Gem_UI.transform.GetChild(0).GetComponent<Text>().text = current.ToString("F0");
                yield return null;
            }
        }

        current = target;
        myChar.Gem = (int)target;
        Gem_UI.transform.GetChild(0).GetComponent<Text>().text = myChar.Gem.ToString("F0");

    }
}
