using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    /********************************** 싱 글 톤 *******************************************/
    //private static GameManager instance = null;
    //public static GameManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = FindObjectOfType(typeof(GameManager)) as GameManager;
    //            if (Instance == null)
    //            {
    //                GameObject obj = new GameObject("GameManager");
    //                instance = obj.AddComponent(typeof(GameManager)) as GameManager;
    //            }
    //        }
    //        return instance;
    //    }
    //}
    private static GameManager _instance = null;

    public static GameManager Instance
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
    PlayerController playerChar;

    public GameObject Light;
    string Test222;
    public int ItemNum;
    public int ItemLv;
    public UnitTemplate UnitData;
    public UnitTemplateMgr UnitDataMgr;
    public DropTemplate DropData;
    public DropTemplateMgr DropDataMgr;
    public DropMultiTemplate DropMultiData;
    public DropMultiTemplateMgr DropMultiDataMgr;
    public SkillInfoTemplate SkillData;
    public SkillInfoTemplateMgr SkillDataMgr;

    public int TestA;
    private string AddText;
    private int ActiveItemLv;
    private float ResurTime = 10f;
    private float CurrentTime;
    [SerializeField]
    private int iconCntCheck = 0;

    public Transform TestMapPos;
    public GameObject RoomManagerObj;
    private GameObject PlayerObjectManager;
    public GameObject MonsterProjectile;
    public GameObject Pause_Btn;
    public GameObject JoyStickObj;
    public GameObject JoyStick;

    public GameObject Player;
    public string ClassName;
    public GameObject SelectCharacter;
    public GameObject ItemInfo_icon;
    public List<GameObject> DropItem = new List<GameObject>();
    public List<GameObject> ElementalStone = new List<GameObject>();
    public List<GameObject> ActiveItem = new List<GameObject>();

    public GameObject[] PlayerBolt;
    public GameObject PlayerShield;
    public GameObject HitEffect;
    public GameObject ShockWaveEffect;
    public GameObject FlameBoob;
    public GameObject FrozenBoob;
    public GameObject FlashEffect;
    public GameObject HealEffect;
    public GameObject ShieldEffect;
    public Sprite[] RightBtnImg;
    public Sprite[] LeftBtnImg;


    public GameObject BossUI;
    public GameObject[] BossObj;

    public GameObject PlayerPanel;
    public GameObject CurrencyPanel;
    public GameObject PlayerHP;
    public GameObject PlayerSeasonalShield;
    public GameObject Coin;
    public GameObject Gem;
    public GameObject Key;
    public GameObject Left_Btn;
    public GameObject Right_Btn;
    public GameObject Jump_Btn;
    public GameObject Select_Btn;
    public GameObject GiftBox_Btn;
    public GameObject AD_Btn;
    public GameObject Tutorial_Anim;
    public GameObject TutorialSkip_Btn;
    public GameObject ClearSkip_Btn;

    public GameObject StoreUI;
    public GameObject EquipmentWindow;
    public GameObject ActiveItemImg;
    public GameObject GiftBoxWindow;
    public GameObject BossNamePanel;
    public GameObject DoorPanel;
    public GameObject LoadingPanel;
    public GameObject[] LodingDot;
    public List<GameObject> BossImgPanel = new List<GameObject>();
    public List<GameObject> AtkGage, ASPDGage, RangeGage, HPGage = new List<GameObject>();
    private int Atk, HP;
    private float ASPD, Range;
    public GameObject BossName_Text;
    public GameObject Loding_Text;
    public GameObject InfoWindow;
    public GameObject PreferencesWindow;
    public GameObject PreferencesMorePanel;
    public GameObject ItemInfoPanel;
    public GameObject Tutorial_Info;
    public GameObject Tutorial_Mask;
    public GameObject Clear_ResultPanel;
    public GameObject Fail_ResultPanel;
    public GameObject GiftBox_InfoUI;
    public GameObject EndingPanel;
    private bool UI_ItemInfoTF = false;
    private bool StoneUsedCheck = false;
    private bool ActiveItemUsedCheck = false;
    private bool ThroneOnCheck = false;
    private bool JoyStickCheck = false;

    public GameObject ElementCountImg;
    public GameObject ElementItemImg;

    public GameObject Pause_Btn_UI;
    public GameObject Result_Equipment;
    public GameObject Crown;
    public GameObject ObtainItem;
    public GameObject AdMerchin;
    public int SelectItemNum = 0;
    public GameObject SoulSpark_UpText;
    public GameObject HeroHeart_UpText;
    public GameObject Gem_UpText;
    public GameObject ClearUpParent;
    public GameObject FailUpParent;

    public GameObject PlayerHP_UpText;
    public GameObject PlayerShield_UpText;
    public GameObject LobbiInfoPanel;

    public GameObject Notice_AD_Panel;
    public Text Notice_AD_Panel_Text;

    public bool BtnOnCheck = false;
    public bool TutoBossInfoCheck = false;

    public int UniqueNum;

    private Text GiftBoxWindow_Text;

    public GameObject Clear_AD_Btn;
    public GameObject Fail_AD_Btn;
    public GameObject Resurrection_AD_Btn;
    /* [0 : 리꼬셰], [1 : 플라이], [2 : 윈드워크], [3 : 파워샷], [4: 데스 붐], [5 : 신의가호], [6 : 강한 심장]
    */
    [Header("[0 : 리꼬셰], [1 : 플라이], [2 : 윈드워크], [3 : 파워샷], [4: 데스 붐], [5 : 신의가호], [6 : 리커버리], [7 : 파워점프], [8 : 에어점프], [9 : 자석], [10 : 데스 스파이크], [11 : 뱀파이어], [12 : 멀티샷],[13 : 행운의 재물], [14 :  헤드샷], [15: 분노], [16 : 하드슈즈], [17 : 반동의 벽], [18 : 점프 샷], [19 : 관통샷], [20 : 충격파], [21 : 더블샷]")]
    public List<int> PlayerSkill = new List<int>(); //스킬 리스트 관리

    private void Awake()
    {
        _instance = this;

        myChar = MyObject.MyChar;
        playerChar = PlayerController.Instance;

        UnitDataMgr = new UnitTemplateMgr();
        DropDataMgr = new DropTemplateMgr();
        DropMultiDataMgr = new DropMultiTemplateMgr();
        SkillDataMgr = new SkillInfoTemplateMgr();


        BossUI = GameObject.Find("BossUI");
        PlayerPanel = GameObject.Find("PlayerPanel");
        CurrencyPanel = GameObject.Find("CurrencyPanel");
        MonsterProjectile = GameObject.Find("MonsterProjectileManager");
        PlayerHP = GameObject.Find("HP_UI");
        PlayerShield = GameObject.Find("Shield_UI");
        Coin = GameObject.Find("Currency_Gold_UI");
        Gem = GameObject.Find("Currency_Gem_UI");
        Key = GameObject.Find("Key_UI");
        //Left_Btn = GameObject.Find("Left_Btn");
        //Right_Btn = GameObject.Find("Right_Btn");
        //Jump_Btn = GameObject.Find("Jump_Btn");
        //Select_Btn = GameObject.Find("Select_Btn");
        //GiftBox_Btn = GameObject.Find("GiftBox_Btn");
        //AD_Btn = GameObject.Find("AD_Btn");
        Tutorial_Anim = GameObject.Find("Tutorial_Anim");
        TutorialSkip_Btn = GameObject.Find("Tutorial_SkipPanel");
        ClearSkip_Btn = GameObject.Find("Clear_SkipPanel");

        StoreUI = GameObject.Find("StoreUI");
        EquipmentWindow = GameObject.Find("EquipmentWindow");
        ActiveItemImg = GameObject.Find("Item_Image");
        GiftBoxWindow = GameObject.Find("GiftBoxWindow");
        PreferencesWindow = GameObject.Find("PreferencesWindow");
        ElementCountImg = GameObject.Find("ElementCount_Image");
        ElementItemImg = GameObject.Find("Element_Image");
        PlayerObjectManager = GameObject.Find("PlayerObjectManager");
        BossName_Text = GameObject.Find("BossName_Text");
        Loding_Text = GameObject.Find("Loding_Text");
        InfoWindow = GameObject.Find("InfoWindow");
        ItemInfoPanel = GameObject.Find("ItemInfoPanel");
        Tutorial_Info = GameObject.Find("Tutorial_Info");
        Tutorial_Mask = GameObject.Find("UnMaskPanel");
        //Clear_ResultPanel = GameObject.Find("Clear_ResultPanel");
        //Fail_ResultPanel = GameObject.Find("Fail_ResultPanel");
        GiftBox_InfoUI = GameObject.Find("GiftBox_InfoUI");
        EndingPanel = GameObject.Find("EndingPanel");

        //for (int i = 0; i < 10; i++)
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
        AD_Btn.SetActive(false);
        Select_Btn.SetActive(false);
        GiftBox_Btn.SetActive(false);
        StoreUI.SetActive(false);
        EquipmentWindow.SetActive(false);
        GiftBoxWindow.SetActive(false);
        InfoWindow.SetActive(false);
        PreferencesWindow.SetActive(false);
        Clear_ResultPanel.SetActive(false);
        Fail_ResultPanel.SetActive(false);
        Tutorial_Anim.SetActive(false);
        TutorialSkip_Btn.SetActive(false);
        ClearSkip_Btn.SetActive(false);
        Tutorial_Info.SetActive(false);
        Tutorial_Mask.SetActive(false);
        EndingPanel.SetActive(false);
        LobbiInfoPanel.SetActive(false);

        //게임 실행이아니라 제작일때 꺼두면 좋은것들
        BossNamePanel = GameObject.Find("BossNamePanel");
        DoorPanel = GameObject.Find("DoorPanel");
        LoadingPanel = GameObject.Find("LoadingPanel");
        BossNamePanel.SetActive(false);
        DoorPanel.SetActive(false);
        LoadingPanel.SetActive(false);
        BossUI.SetActive(false);
        GiftBox_InfoUI.SetActive(false);

        for (int i = 0; i < BossNamePanel.transform.GetChild(0).Find("BossImgPanel").childCount; i++)
        {
            BossImgPanel.Add(BossNamePanel.transform.GetChild(0).Find("BossImgPanel").GetChild(i).gameObject);
        }

        GiftBoxWindow_Text = GiftBoxWindow.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();

    }

    void Start()
    {
        OnLoadHeroTemplateMgr();
        ResurTime = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    GamePlay();
        //}
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    ActiveItemUse();
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    ElementStonUse();
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    //Clear_ResultPanel.transform.Find("Soul_Spark").GetComponentInChildren<Text>().text = (myChar.GainSoulSpark * myChar.Gain_Multiple).ToString();
        //    myChar.MultipleCheck = true;
        //    myChar.Gain_Multiple = 2;
        //    if (Fail_ResultPanel.activeSelf)
        //    {
        //        FailPlusEffect();
        //        //StartCoroutine(Count(Fail_ResultPanel.transform.Find("Soul_Spark").GetComponentInChildren<Text>(), true, myChar.GainSoulSpark * myChar.Gain_Multiple, myChar.GainSoulSpark, SoulSpark_UpText, Fail_ResultPanel.transform.Find("UpText").Find("SoulSpark_UpText").gameObject));
        //        //StartCoroutine(Count(Fail_ResultPanel.transform.Find("Hero_Heart").GetComponentInChildren<Text>(), true, myChar.GainHeroHeart * myChar.Gain_Multiple, myChar.GainHeroHeart, HeroHeart_UpText, Fail_ResultPanel.transform.Find("UpText").Find("Hero_UpText").gameObject));
        //        //StartCoroutine(Count(Fail_ResultPanel.transform.Find("Gem").GetComponentInChildren<Text>(), true, myChar.GainGem * myChar.Gain_Multiple, myChar.GainGem, Gem_UpText, Fail_ResultPanel.transform.Find("UpText").Find("Gem_UpText").gameObject));
        //    }
        //    else if (Clear_ResultPanel.activeSelf)
        //    {
        //        ClearPlusEffect();
        //        //StartCoroutine(Count(Clear_ResultPanel.transform.Find("Soul_Spark").GetComponentInChildren<Text>(), true, myChar.GainSoulSpark * myChar.Gain_Multiple, myChar.GainSoulSpark, SoulSpark_UpText, Clear_ResultPanel.transform.Find("UpText").Find("SoulSpark_UpText").gameObject));
        //        //StartCoroutine(Count(Clear_ResultPanel.transform.Find("Hero_Heart").GetComponentInChildren<Text>(), true, myChar.GainHeroHeart * myChar.Gain_Multiple, myChar.GainHeroHeart, HeroHeart_UpText, Clear_ResultPanel.transform.Find("UpText").Find("Hero_UpText").gameObject));
        //        //StartCoroutine(Count(Clear_ResultPanel.transform.Find("Gem").GetComponentInChildren<Text>(), true, myChar.GainGem * myChar.Gain_Multiple, myChar.GainGem, Gem_UpText, Clear_ResultPanel.transform.Find("UpText").Find("Gem_UpText").gameObject));
        //    }
        //}
        if (!JoyStickCheck)
        {
            //조이스틱 컨트롤부분 상하중 교정해주는부분
            switch (myChar.TopDownCnt)
            {
                case 0:
                    JoyStickObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    break;
                case 1:
                    JoyStickObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100);
                    break;
                case 2:
                    JoyStickObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
                    break;
            }
            //오른손 왼손 잡이 교정해주는 부분
            if (myChar.HandCheck == 0)
            {
                JoyStickObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                JoyStick.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            else if (myChar.HandCheck == -1)
            {
                JoyStickObj.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
                JoyStick.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            }
            
            JoyStickCheck = true;
        }

        if (!Clear_ResultPanel.activeSelf && !Fail_ResultPanel.activeSelf)
        {
            UI_ItemInfoTF = false;
        }
        CharacterChoice();
        PlayerHPControl();
        PlayerShieldControl();
        CurrencyControl();
        ActiveItemDisplayCheck();
        ElementItemDisplayCheck();
        SeasonalShieldCheck();
        
        LoadingPanelCheck();
        PanelOn();
        PreferencesWindowCheck();
        //ItemInfoCheck();
        PauseBtnCheck();
        ResultPanelControl();
        GiftBoxWindowCheck();
        Tutorial_InfoText();
        GiftBoxUI_TextCheck();
        TextCheck();
        adOpenCheck();
        LobbiInfoCheck();

        if (RoomManagerObj.activeSelf)
        {
            BossUIControl();
        }

        if (BossUI.activeSelf)
        {
            if (!myChar.BossHPAnim)
            {
                if (BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount < 1)
                {
                    if (LoadingPanel.activeSelf == false)
                    {
                        BossUI.GetComponent<Animator>().enabled = true;
                        //BossUI.GetComponent<Animator>().SetBool("HPIncrease", true);
                        SoundManager.Instance.PlaySfx(19);
                    }
                }
                else if (BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount >= 1)
                {
                    BossUI.GetComponent<Animator>().enabled = false;
                    if (!myChar.Tutorial)
                    {
                        //if (myChar.Chapter >= 3)
                        //{
                        //    myChar.SelectLocation.transform.parent.Find("Throne").GetChild(0).gameObject.SetActive(false);
                        //    myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).gameObject.SetActive(false);
                        //    myChar.SelectLocation.transform.parent.Find("Monster").GetChild(myChar.EnthroneHeroNum).gameObject.SetActive(true);
                        //    RoomManager.Instance.DontTouchBtn_Panel.SetActive(false);
                        //}
                        myChar.BossHPAnim = true;
                        myChar.BossClear = true;
                    }
                    else if (myChar.Tutorial)
                    {
                        if (myChar.TutorialNum == 3)
                        {
                            myChar.BossHPAnim = true;
                        }
                         else if (myChar.TutorialNum == 5)
                        {
                            myChar.CurrentBossHp = 50f;
                            RoomManager.Instance.Tutorial_Map.transform.Find("Throne").GetChild(1).GetChild(5).GetComponent<Animator>().SetBool("Battle", true);
                            myChar.BossHPAnim = true;
                        }
                        //if (myChar.SelectLocation.transform.parent.Find("Throne"))
                        //{
                        //    myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).GetChild(5).gameObject.SetActive(false);
                        //}                        
                        //myChar.SelectLocation.transform.parent.Find("Monster").gameObject.SetActive(true);
                        //RoomManager.Instance.DontTouchBtn_Panel.SetActive(false);
                        //myChar.BossHPAnim = true;
                    }
                }

            }
            else if (myChar.BossHPAnim)
            {
                if (myChar.SelectedStage != 1)
                {
                    BossUI.transform.GetChild(0).Find("HP").GetComponent<Image>().fillAmount = myChar.CurrentBossHp / myChar.TotalBossHp;
                }
                else if (myChar.SelectedStage == 1)
                {
                    BossUI.transform.GetChild(0).Find("HP").GetComponent<Image>().fillAmount = myChar.CurrentBossHp / myChar.TotalBossHp;
                }
            }
        }
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    StartCoroutine(MonsterStun((myChar.ActiveItme_Lv * 0.5f) + 1.5f));
        //}
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    PlayerSkill[2] = 0;
        //}
        if (myChar.currentHp > 0)
        {
            if (!myChar.Finished)
            {
                for (int i = 0; i < myChar.HeroLv.Length; i++)
                {
                    if (i == myChar.SelectHero)
                    {
                        Player.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else if (i != myChar.SelectHero)
                    {
                        Player.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
            else if (myChar.Finished)
            {
                for (int i = 0; i < myChar.HeroLv.Length; i++)
                {
                    Player.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        else if (myChar.currentHp <= 0)
        {
            if (myChar.Resurrection)
            {
                //StartCoroutine(LobbyGo(3f));
            }
        }

        if (InfoWindow.activeSelf)
        {
            if (myChar.Gem >= ((int)Mathf.Pow(myChar.ResurectionCost, myChar.NumberOfResurections) * 10))
            {
                InfoWindow.transform.Find("Cash_Btn").GetComponentInChildren<Text>().text = ((int)Mathf.Pow(myChar.ResurectionCost, myChar.NumberOfResurections) * 10).ToString("n0");
                InfoWindow.transform.Find("Cash_Btn").gameObject.SetActive(true);
                InfoWindow.transform.Find("NoCash_Btn").gameObject.SetActive(false);
            }
            else
            {
                InfoWindow.transform.Find("NoCash_Btn").GetComponentInChildren<Text>().text = ((int)Mathf.Pow(myChar.ResurectionCost, myChar.NumberOfResurections) * 10).ToString("n0");
                InfoWindow.transform.Find("Cash_Btn").gameObject.SetActive(false);
                InfoWindow.transform.Find("NoCash_Btn").gameObject.SetActive(true);
            }
            

            if (InfoWindow.transform.Find("Second_BackGround").GetComponentInChildren<Text>().color.a == 1)
            {
                ResurTime = CurrentTime - Time.realtimeSinceStartup;
            }
            if (ResurTime > 0f)
            {
                InfoWindow.transform.Find("Second_BackGround").GetComponentInChildren<Text>().text = Mathf.Round(ResurTime).ToString("n0");
            }
            else if (ResurTime <= 0f)
            {
                InfoWindow.transform.Find("Second_BackGround").GetComponentInChildren<Text>().text = "0";
                InfoWindow.SetActive(false);
                Fail_ResultPanel.SetActive(true);

            }
        }
        else if (!InfoWindow.activeSelf)
        {
            CurrentTime = Time.realtimeSinceStartup + 10f;
        }

    }
    private void FixedUpdate()
    {

    }
    private void PauseBtnCheck()
    {
        if (myChar.Tutorial)
        {
            Pause_Btn.SetActive(false);
        }
        else
        {
            if (!Pause_Btn.activeSelf)
            {
                Pause_Btn.SetActive(true);
            }
        }
    }

    private void adOpenCheck()
    {
        if (myChar.AD_Enought_Check)
        {
            myChar.AD_Road_Time -= Time.unscaledDeltaTime;
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
                            Clear_AD_Btn.SetActive(false);
                            myChar.MultipleCheck = true;
                            myChar.Gain_Multiple = 2;
                            GameManager.Instance.ClearPlusEffect();
                            FirebaseManager.firebaseManager.ResultWatchAD("Clear");
                            break;
                        case 2:     //실패 배수보상
                            Fail_AD_Btn.SetActive(false);
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
                            Resurrection_AD_Btn.SetActive(false);
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

    private void TextCheck()
    {
        if (GiftBoxWindow.activeSelf)
        {
            if (myChar.Key >= 1)
            {
                GiftBoxWindow_Text.text = myChar.TextDataMgr.GetTemplate(206).Content[myChar.LanguageNum].Replace("\\n", "\n");
            }
            else
            {
                GiftBoxWindow_Text.text = myChar.TextDataMgr.GetTemplate(3000).Content[myChar.LanguageNum].Replace("\\n", "\n");
            }
            
        }
    }

    private void PlayerHPControl()
    {
        if (myChar.currentHp >= myChar.maxHp)
        {
            myChar.currentHp = myChar.maxHp;
        }
        //HP_iconChek();

        PlayerHP.transform.Find("HP_Text").GetComponent<Text>().text = myChar.currentHp.ToString("F0") + "/" + myChar.maxHp.ToString("F0");
        //if (myChar.currentHp > 5)
        //{
        //    PlayerHP.transform.Find("HP_Text").gameObject.SetActive(true);
        //    PlayerHP.transform.Find("HP_Text").GetComponent<Text>().text = " +" + myChar.currentHp.ToString("F0");

        //    for (int i = 0; i < 5; i++)
        //    {
        //        PlayerHP.transform.GetChild(i).gameObject.SetActive(true);
        //        PlayerHP.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
        //    }
        //}
        //else if (myChar.currentHp <= 5)
        //{

        //    PlayerHP.transform.Find("HP_Text").gameObject.SetActive(false);

        //    //if (myChar.maxHp <= 5)
        //    //{
        //    //    switch (myChar.maxHp)
        //    //    {
        //    //        case 1:
        //    //            HP_iconChek();
        //    //            break;
        //    //        case 2:
        //    //            HP_iconChek();
        //    //            break;
        //    //        case 3:
        //    //            HP_iconChek();
        //    //            break;
        //    //        case 4:
        //    //            HP_iconChek();
        //    //            break;
        //    //        case 5:
        //    //            HP_iconChek();
        //    //            break;
        //    //    }
        //    //}

        //    for (int i = 0; i < myChar.currentHp; i++)
        //    {
        //        PlayerHP.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
        //    }
        //    for (int i = myChar.currentHp; i < 5; i++)
        //    {
        //        PlayerHP.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        //    }
        //}
    }
    private void HP_iconChek()
    {
        if (myChar.maxHp <= 5)
        {
            for (int i = 0; i < myChar.maxHp; i++)
            {
                PlayerHP.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = myChar.maxHp; i < 5; i++)
            {
                PlayerHP.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if (myChar.maxHp > 5)
        {
            for (int i = 0; i <= 5; i++)
            {
                PlayerHP.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    private void CurrencyControl()
    {
        Coin.GetComponentInChildren<Text>().text = myChar.Coin.ToString("F0");
        //Gem.GetComponentInChildren<Text>().text = myChar.Gem.ToString("F0");
        Key.GetComponentInChildren<Text>().text = "x" + myChar.Key.ToString("F0");

    }

    private void PlayerShieldControl()
    {
        PlayerShield.transform.Find("Shield_Text").GetComponent<Text>().text = myChar.Shield.ToString("F0");

        //for (int i = 0; i < myChar.Shield; i++)
        //{
        //    PlayerShield.transform.GetChild(i).gameObject.SetActive(true);
        //}
        //for (int i = myChar.Shield; i < 5; i++)
        //{
        //    PlayerShield.transform.GetChild(i).gameObject.SetActive(false);
        //}
    }
    private void LobbiInfoCheck()
    {
        if (LobbiInfoPanel.activeSelf)
        {
            LobbiInfoPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(246).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }
    }
    private void BossUIControl()
    {
        if (!myChar.Tutorial)
        {
            if ((myChar.Chapter + 1) % 3 != 0)
            {
                if (myChar.Stage == 50)
                {
                    BossUI.SetActive(true);
                    if (BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount == 0)
                    {
                        if (myChar.BossClear)
                        {
                            StartCoroutine(BossKillTimeScale());
                        }
                    }
                }
                //if (myChar.Stage < 50)
                //{
                //    if (myChar.Stage % 10 != 0)
                //    {
                //        BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 0;
                //        BossUI.SetActive(false);
                //        myChar.BossHPAnim = false;
                //    }
                //    else if (myChar.Stage % 10 == 0)
                //    {
                //        BossUI.SetActive(true);
                //        if (BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount == 0)
                //        {
                //            if (myChar.BossClear)
                //            {
                //                StartCoroutine(BossKillTimeScale());
                //            }
                //        }
                //    }
                //}
                //else if (myChar.Stage == 50)
                //{
                //    BossUI.SetActive(true);
                //    if (BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount == 0)
                //    {
                //        if (myChar.BossClear)
                //        {
                //            StartCoroutine(BossKillTimeScale());
                //        }
                //    }
                //}
            }
            else if ((myChar.Chapter + 1) % 3 == 0)
            {
                if (myChar.Stage == 30)
                {
                    BossUI.SetActive(true);
                    if (BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount == 0)
                    {
                        if (myChar.BossClear)
                        {
                            StartCoroutine(BossKillTimeScale());
                        }
                    }
                }
                //if (myChar.Stage < 30)
                //{
                //    if (myChar.Stage % 10 != 0)
                //    {
                //        BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 0;
                //        BossUI.SetActive(false);
                //        myChar.BossHPAnim = false;
                //    }
                //    else if (myChar.Stage % 10 == 0)
                //    {
                //        BossUI.SetActive(true);
                //        if (BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount == 0)
                //        {
                //            if (myChar.BossClear)
                //            {
                //                StartCoroutine(BossKillTimeScale());
                //            }
                //        }
                //    }
                //}
                //else if (myChar.Stage == 30)
                //{
                //    BossUI.SetActive(true);
                //    if (BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount == 0)
                //    {
                //        if (myChar.BossClear)
                //        {
                //            StartCoroutine(BossKillTimeScale());
                //        }
                //    }
                //}
            }
            //if (myChar.Chapter < 3)
            //{
            //    if (myChar.Stage != (myChar.BasicStage + 3))
            //    {
            //        BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 0;
            //        BossUI.SetActive(false);
            //        myChar.BossHPAnim = false;
            //    }
            //    else if (myChar.Stage == (myChar.BasicStage + 3))
            //    {
            //        BossUI.SetActive(true);
            //        if (BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount == 0)
            //        {
            //            if (myChar.BossClear)
            //            {
            //                StartCoroutine(BossKillTimeScale());
            //            }
            //        }
            //    }
            //}
            //else if (myChar.Chapter >= 3)
            //{
            //    BossUI.SetActive(true);
            //    if (BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount == 0)
            //    {
            //        if (myChar.BossClear)
            //        {
            //            StartCoroutine(BossKillTimeScale());
            //        }
            //    }
            //}

            //보스 네임수정부분
            switch (myChar.Chapter)
            {
                case 0:
                    BossNameCheck(5019, 1);
                    //BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(257).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    //BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(257).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    //BossNamePanelImgCheck(0);
                    break;
                case 1:
                    BossNameCheck(5009, 3);
                    //BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(272).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    //BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(272).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    //BossNamePanelImgCheck(1);
                    break;
                case 2:
                    BossNameCheck(5031, 4);
                    //BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(267).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    //BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(267).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    //BossNamePanelImgCheck(2);
                    break;
                case 3:
                    BossNameCheck(5014, 2);
                    //BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(262).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    //BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(262).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    //BossNamePanelImgCheck(3);
                    break;
                case 4:
                    BossNameCheck(5004, 0);
                    break;
                case 5:
                    BossNameCheck(5042, 5);
                    break;
            }
        }
        else if (myChar.Tutorial)
        {
            if (myChar.TutorialNum == 3)
            {
                if (myChar.TutoThreeCheck)
                {
                    BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1;
                    BossUI.SetActive(true);
                }                
                BossUI.GetComponentInChildren<Text>().text = "";
                BossName_Text.GetComponent<Text>().text = "";
            }
            else if (myChar.TutorialNum == 5)
            {
                if (myChar.BossCheck)
                {
                    BossUI.SetActive(true);
                    BossNamePanelImgCheck(5);
                }
                BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(5020).Content[myChar.LanguageNum].Replace("\\n", "\n");
                BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(5020).Content[myChar.LanguageNum].Replace("\\n", "\n");
            }
            else if (myChar.TutorialNum != 5)
            {
                BossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 0;
                BossUI.SetActive(false);
                myChar.BossHPAnim = false;
            }
            
        }
    }
    private void BossNameCheck(int BossName, int BossImageNo)
    {
        string StageNo = (RoomManager.Instance.CurrentStage.transform.parent.name.Substring(RoomManager.Instance.CurrentStage.transform.parent.name.Length - 8, 1));
        //Debug.Log(RoomManager.Instance.CurrentStage.transform.parent.name.Substring(RoomManager.Instance.CurrentStage.transform.parent.name.Length - 8, 1));
        if ((myChar.Chapter + 1) % 3 != 0)
        {
            if (myChar.Stage == 50)
            {
                BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(BossName).Content[myChar.LanguageNum].Replace("\\n", "\n");
                BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(BossName).Content[myChar.LanguageNum].Replace("\\n", "\n");
                BossNamePanelImgCheck(BossImageNo);
            }
        }
        else if ((myChar.Chapter + 1) % 3 == 0)
        {
            if (myChar.Stage == 30)
            {
                BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(BossName).Content[myChar.LanguageNum].Replace("\\n", "\n");
                BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(BossName).Content[myChar.LanguageNum].Replace("\\n", "\n");
                BossNamePanelImgCheck(BossImageNo);
            }
        }

    }
    //보스 네임패널 띄우는거 변경 전
    //private void BossNameCheck(int BossName_0, int BossName_1, int BossName_2, int BossName_3, int BossName_4, int BossImageNo)
    //{
    //    string StageNo = (RoomManager.Instance.CurrentStage.transform.parent.name.Substring(RoomManager.Instance.CurrentStage.transform.parent.name.Length - 8, 1));
    //    //Debug.Log(RoomManager.Instance.CurrentStage.transform.parent.name.Substring(RoomManager.Instance.CurrentStage.transform.parent.name.Length - 8, 1));
    //    if ((myChar.Chapter + 1) % 3 != 0)
    //    {
    //        if (myChar.Stage < 50)
    //        {
    //            if (myChar.Stage % 10 == 0)
    //            {
    //                switch (int.Parse(StageNo))
    //                {
    //                    case 0:
    //                        BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_0).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                        BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_0).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                        break;
    //                    case 1:
    //                        BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_1).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                        BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_1).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                        break;
    //                    case 2:
    //                        BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_2).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                        BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_2).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                        break;
    //                    case 3:
    //                        BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_3).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                        BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_3).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                        break;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_4).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //            BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_4).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //            BossNamePanelImgCheck(BossImageNo);
    //        }
    //    }
    //    else if ((myChar.Chapter + 1) % 3 == 0)
    //    {
    //        if (myChar.Stage < 30)
    //        {
    //            if (myChar.Stage % 10 == 0)
    //            {
    //                switch (int.Parse(StageNo))
    //                {
    //                    case 0:
    //                        BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_0).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                        BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_0).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                        break;
    //                    case 1:
    //                        BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_1).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                        BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_1).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //                        break;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            BossUI.GetComponentInChildren<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_4).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //            BossName_Text.GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(BossName_4).Content[myChar.LanguageNum].Replace("\\n", "\n");
    //            BossNamePanelImgCheck(BossImageNo);
    //        }
    //    }

    //}

    private void ResultPanelControl()
    {
        if (Clear_ResultPanel.activeSelf == true)
        {
            Transform EquipmentUI_Pos = Clear_ResultPanel.transform.Find("Equipment_ScrollView").GetChild(0).GetChild(0);
            Clear_ResultPanel.transform.Find("Stage").GetComponentInChildren<Text>().text = myChar.Chapter.ToString() + " - " + myChar.Stage.ToString();

            int a_hour = (int)(myChar.PlayTime / 3600);
            int a_min = (int)(myChar.PlayTime % 3600 / 60);
            int a_sec = (int)(myChar.PlayTime % 3600 % 60);

            Clear_ResultPanel.transform.Find("Time").GetComponentInChildren<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", a_hour, a_min, a_sec);
            //Clear_ResultPanel.transform.Find("Soul_Spark").GetComponentInChildren<Text>().text = (myChar.GainSoulSpark * myChar.Gain_Multiple).ToString();
            //Clear_ResultPanel.transform.Find("Hero_Heart").GetComponentInChildren<Text>().text = (myChar.GainHeroHeart * myChar.Gain_Multiple).ToString();
            //Clear_ResultPanel.transform.Find("Gem").GetComponentInChildren<Text>().text = (myChar.GainGem * myChar.Gain_Multiple).ToString();
            if (myChar.MultipleCheck)
            {

            }
            else
            {
                Clear_ResultPanel.transform.Find("Time").GetComponentInChildren<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", a_hour, a_min, a_sec);
                Clear_ResultPanel.transform.Find("Soul_Spark").GetComponentInChildren<Text>().text = (myChar.GainSoulSpark * myChar.Gain_Multiple).ToString();
                Clear_ResultPanel.transform.Find("Hero_Heart").GetComponentInChildren<Text>().text = (myChar.GainHeroHeart * myChar.Gain_Multiple).ToString();
                Clear_ResultPanel.transform.Find("Gem").GetComponentInChildren<Text>().text = (myChar.GainGem * myChar.Gain_Multiple).ToString();
            }

            if (myChar.EquipmentUsed.Count > 0)
            {
                //Transform EquipmentUI_Pos = Clear_ResultPanel.transform.Find("Equipment_ScrollView").GetChild(0).GetChild(0);
                if (myChar.EquipmentUsed.Count != EquipmentUI_Pos.childCount)
                {
                    if (EquipmentUI_Pos.childCount > 0)
                    {
                        for (int i = 0; i < EquipmentUI_Pos.childCount; i++)
                        {
                            Destroy(EquipmentUI_Pos.GetChild(i).gameObject);
                        }
                    }
                    for (int i = 0; i < myChar.EquipmentUsed.Count; i++)
                    {
                        GameObject EquipmentUI = Instantiate(Result_Equipment, transform.position, Quaternion.identity);
                        EquipmentUI.transform.Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.EquipmentUsed[i].ToString());
                        EquipmentUI.transform.parent = EquipmentUI_Pos.transform;
                        EquipmentUI.transform.localScale = new Vector3(1, 1, 1);
                        EquipmentUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + myChar.itemDataMgr.GetTemplate(myChar.EquipmentUsed[i]).Grade);

                        switch (myChar.itemDataMgr.GetTemplate(myChar.EquipmentUsed[i]).Grade)
                        {
                            case 0:
                                EquipmentUI.transform.Find("LV_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[myChar.EquipmentUsed[i]] + 1) + "</color>";
                                break;
                            case 1:
                                EquipmentUI.transform.Find("LV_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[myChar.EquipmentUsed[i]] + 1) + "</color>";
                                break;
                            case 2:
                                EquipmentUI.transform.Find("LV_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[myChar.EquipmentUsed[i]] + 1) + "</color>";
                                break;
                        }
                        //if (myChar.EquipmentUsed[i] < 1000)
                        //{
                            
                        //}
                        //else if (myChar.EquipmentUsed[i] >= 1000 && myChar.EquipmentUsed[i] < 2000)
                        //{
                        //    switch (myChar.ElementStoneAll[myChar.EquipmentUsed[i] - 1000])
                        //    {
                        //        case 0:
                        //            EquipmentUI.transform.Find("Lv_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[myChar.EquipmentUsed[i]] + 1) + "</color>";
                        //            break;
                        //        case 1:
                        //            EquipmentUI.transform.Find("Lv_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[myChar.EquipmentUsed[i]] + 1) + "</color>";
                        //            break;
                        //        case 2:
                        //            EquipmentUI.transform.Find("Lv_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[myChar.EquipmentUsed[i]] + 1) + "</color>";
                        //            break;
                        //    }
                        //}
                        //else if (myChar.EquipmentUsed[i] >= 2000)
                        //{
                        //    switch (myChar.ActiveitemAll[myChar.EquipmentUsed[i]])
                        //    {
                        //        case 0:
                        //            EquipmentUI.transform.Find("Lv_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[myChar.EquipmentUsed[i]] + 1) + "</color>";
                        //            break;
                        //        case 1:
                        //            EquipmentUI.transform.Find("Lv_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[myChar.EquipmentUsed[i]] + 1) + "</color>";
                        //            break;
                        //        case 2:
                        //            EquipmentUI.transform.Find("Lv_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[myChar.EquipmentUsed[i]] + 1) + "</color>";
                        //            break;
                        //    }
                        //}
                    }
                }
            }
            else
            {
                if (BtnOnCheck)
                {
                    Clear_ResultPanel.transform.Find("Btn_Panel").gameObject.SetActive(true);
                }
            }

            if (BtnOnCheck)
            {
                if (!UI_ItemInfoTF)
                {
                    UI_ItemInfoTF = true;
                    if (EquipmentUI_Pos.gameObject.transform.childCount != 0)
                    {
                        StartCoroutine(UI_ItemInfo(EquipmentUI_Pos, Clear_ResultPanel));
                    }
                }
            }
        }

        if (Fail_ResultPanel.activeSelf == true)
        {
            Transform EquipmentUI_Pos = Fail_ResultPanel.transform.Find("Equipment_ScrollView").GetChild(0).GetChild(0);

            Fail_ResultPanel.transform.Find("Stage").GetComponentInChildren<Text>().text = myChar.Chapter.ToString() + " - " + myChar.Stage.ToString();

            int a_hour = (int)(myChar.PlayTime / 3600);
            int a_min = (int)(myChar.PlayTime % 3600 / 60);
            int a_sec = (int)(myChar.PlayTime % 3600 % 60);
            Fail_ResultPanel.transform.Find("Time").GetComponentInChildren<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", a_hour, a_min, a_sec);
            //Fail_ResultPanel.transform.Find("Soul_Spark").GetComponentInChildren<Text>().text = (myChar.GainSoulSpark * myChar.Gain_Multiple).ToString();
            //Fail_ResultPanel.transform.Find("Hero_Heart").GetComponentInChildren<Text>().text = (myChar.GainHeroHeart * myChar.Gain_Multiple).ToString();
            //Fail_ResultPanel.transform.Find("Gem").GetComponentInChildren<Text>().text = (myChar.GainGem * myChar.Gain_Multiple).ToString();
            if (myChar.MultipleCheck)
            {

            }
            else
            {
                Fail_ResultPanel.transform.Find("Soul_Spark").GetComponentInChildren<Text>().text = (myChar.GainSoulSpark * myChar.Gain_Multiple).ToString();
                Fail_ResultPanel.transform.Find("Hero_Heart").GetComponentInChildren<Text>().text = (myChar.GainHeroHeart * myChar.Gain_Multiple).ToString();
                Fail_ResultPanel.transform.Find("Gem").GetComponentInChildren<Text>().text = (myChar.GainGem * myChar.Gain_Multiple).ToString();
            }

            if (myChar.EquipmentUsed.Count > 0)
            {
                if (myChar.EquipmentUsed.Count != EquipmentUI_Pos.childCount)
                {
                    if (EquipmentUI_Pos.childCount > 0)
                    {
                        for (int i = 0; i < EquipmentUI_Pos.childCount; i++)
                        {
                            Destroy(EquipmentUI_Pos.GetChild(i).gameObject);
                        }
                    }
                    for (int i = 0; i < myChar.EquipmentUsed.Count; i++)
                    {
                        GameObject EquipmentUI = Instantiate(Result_Equipment, transform.position, Quaternion.identity);
                        EquipmentUI.transform.Find("Equipment").GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.EquipmentUsed[i].ToString());
                        EquipmentUI.transform.parent = EquipmentUI_Pos.transform;
                        EquipmentUI.transform.localScale = new Vector3(1, 1, 1);
                        EquipmentUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + myChar.itemDataMgr.GetTemplate(myChar.EquipmentUsed[i]).Grade);

                        switch (myChar.itemDataMgr.GetTemplate(myChar.EquipmentUsed[i]).Grade)
                        {
                            case 0:
                                EquipmentUI.transform.Find("LV_Text").GetComponent<Text>().text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[myChar.EquipmentUsed[i]] + 1) + "</color>";
                                break;
                            case 1:
                                EquipmentUI.transform.Find("LV_Text").GetComponent<Text>().text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[myChar.EquipmentUsed[i]] + 1) + "</color>";
                                break;
                            case 2:
                                EquipmentUI.transform.Find("LV_Text").GetComponent<Text>().text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[myChar.EquipmentUsed[i]] + 1) + "</color>";
                                break;
                        }
                    }
                }
            }
            else
            {
                if (BtnOnCheck)
                {
                    Fail_ResultPanel.transform.Find("Btn_Panel").gameObject.SetActive(true);
                }
            }

            if (BtnOnCheck)
            {
                if (!UI_ItemInfoTF)
                {
                    UI_ItemInfoTF = true;
                    if (EquipmentUI_Pos.gameObject.transform.childCount != 0)
                    {
                        StartCoroutine(UI_ItemInfo(EquipmentUI_Pos, Fail_ResultPanel));
                    }
                }
            }
        }
    }

    private void CharacterChoice()
    {
        Player = GameObject.Find("Player");

        for (int i = 0; i < myChar.HeroNum; i++)
        {
            if (Player.transform.GetChild(i).gameObject.activeSelf == true)
            {
                ClassName = Player.transform.GetChild(i).gameObject.name;
                SelectCharacter = Player.transform.GetChild(i).gameObject;
            }
        }

        if (!myChar.Finished)
        {
            if (ClassName == "SwordMan")
            {
                myChar.SwordMan = true;
                myChar.Warrior = false;
                myChar.Archer = false;
                myChar.Ninja = false;
                myChar.Wizard = false;
            }
            else if (ClassName == "Warrior")
            {
                myChar.SwordMan = false;
                myChar.Warrior = true;
                myChar.Archer = false;
                myChar.Ninja = false;
                myChar.Wizard = false;
            }
            else if (ClassName == "Archer")
            {
                myChar.SwordMan = false;
                myChar.Warrior = false;
                myChar.Archer = true;
                myChar.Ninja = false;
                myChar.Wizard = false;
            }
            else if (ClassName == "Ninja")
            {
                myChar.SwordMan = false;
                myChar.Warrior = false;
                myChar.Archer = false;
                myChar.Ninja = true;
                myChar.Wizard = false;
            }
            else if (ClassName == "Wizard")
            {
                myChar.SwordMan = false;
                myChar.Warrior = false;
                myChar.Archer = false;
                myChar.Ninja = false;
                myChar.Wizard = true;
            }
            ThroneOnCheck = false;
        }
        else if (myChar.Finished)
        {
            myChar.SwordMan = false;
            myChar.Warrior = false;
            myChar.Archer = false;
            myChar.Ninja = false;
            myChar.Wizard = false;

            //if (!ThroneOnCheck)
            //{
            //    Debug.Log(22);
            //    StartCoroutine(ThroneActive());
            //    ThroneOnCheck = true;
            //}
            
            myChar.SelectLocation.transform.parent.Find("Throne").GetChild(0).GetChild(0).gameObject.SetActive(false);
            myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).gameObject.SetActive(true);
            for (int i = 0; i < myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).childCount; i++)
            {
                if (i == myChar.SelectHero)
                {
                    myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).GetChild(i).gameObject.SetActive(true);
                }
                else if (i != myChar.SelectHero)
                {
                    myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).GetChild(i).gameObject.SetActive(false);
                }
            }

        }
    }

    public void ActiveItemDisplayCheck()
    {
        for (int i = 0; i < ActiveItemImg.transform.childCount; i++)
        {
            if (myChar.ActiveItem != i)
            {
                ActiveItemImg.transform.GetChild(i).gameObject.SetActive(false);
            }
            else if (myChar.ActiveItem == i)
            {
                ActiveItemImg.transform.GetChild(i).gameObject.SetActive(true);
            }

        }
    }
    private void ElementItemDisplayCheck()
    {
        ElementCountImg.GetComponentInChildren<Text>().text = myChar.ElementStone[myChar.SelectStoneNum].ToString("n0");
        for (int i = 0; i < ElementItemImg.transform.childCount; i++)
        {
            if (myChar.SelectStoneNum != i)
            {
                ElementItemImg.transform.GetChild(i).gameObject.SetActive(false);
            }
            else if (myChar.SelectStoneNum == i)
            {
                ElementItemImg.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        if (myChar.SelectStoneNum != 0)
        {
            if (myChar.ElementStone[myChar.SelectStoneNum] == 0)
            {
                myChar.SelectStoneNum = 0;
            }
        }
    }
    public void ElementStonUse()
    {
        if (myChar.Tutorial)
        {
            if (Time.timeScale >= 1)
            {
                if (myChar.SelectStoneNum == 0)
                {
                    for (int i = 1; i < myChar.ElementStone.Length; i++)
                    {
                        if (myChar.ElementStone[i] != 0)
                        {
                            myChar.SelectStoneNum = i;
                            break;
                        }
                    }

                }
                else if (myChar.SelectStoneNum == 1)
                {
                    int CheckCnt = 0;
                    for (int i = 2; i < myChar.ElementStone.Length; i++)
                    {
                        if (myChar.ElementStone[i] != 0)
                        {
                            myChar.SelectStoneNum = i;
                            CheckCnt++;
                            break;
                        }
                    }
                    if (CheckCnt == 0)
                    {
                        myChar.SelectStoneNum = 0;
                    }
                }
                else if (myChar.SelectStoneNum == 2)
                {
                    int CheckCnt = 0;
                    for (int i = 3; i < myChar.ElementStone.Length; i++)
                    {
                        if (myChar.ElementStone[i] != 0)
                        {
                            myChar.SelectStoneNum = i;
                            CheckCnt++;
                            break;
                        }
                    }
                    if (CheckCnt == 0)
                    {
                        myChar.SelectStoneNum = 0;
                    }
                }
                else if (myChar.SelectStoneNum == 3)
                {
                    int CheckCnt = 0;
                    for (int i = 4; i < myChar.ElementStone.Length; i++)
                    {
                        if (myChar.ElementStone[i] != 0)
                        {
                            myChar.SelectStoneNum = i;
                            CheckCnt++;
                            break;
                        }
                    }
                    if (CheckCnt == 0)
                    {
                        myChar.SelectStoneNum = 0;
                    }
                }

                else if (myChar.SelectStoneNum == 4)
                {
                    myChar.SelectStoneNum = 0;
                }

                if (myChar.TutorialNum == 3)
                {
                    myChar.TutoStoneUse = true;
                }
            }            
        }
        else if (!myChar.Tutorial)
        {
            if (myChar.SelectStoneNum == 0)
            {
                for (int i = 1; i < myChar.ElementStone.Length; i++)
                {
                    if (myChar.ElementStone[i] != 0)
                    {
                        myChar.SelectStoneNum = i;
                        break;
                    }
                }

            }
            else if (myChar.SelectStoneNum == 1)
            {
                int CheckCnt = 0;
                for (int i = 2; i < myChar.ElementStone.Length; i++)
                {
                    if (myChar.ElementStone[i] != 0)
                    {
                        myChar.SelectStoneNum = i;
                        CheckCnt++;
                        break;
                    }
                }
                if (CheckCnt == 0)
                {
                    myChar.SelectStoneNum = 0;
                }
            }
            else if (myChar.SelectStoneNum == 2)
            {
                int CheckCnt = 0;
                for (int i = 3; i < myChar.ElementStone.Length; i++)
                {
                    if (myChar.ElementStone[i] != 0)
                    {
                        myChar.SelectStoneNum = i;
                        CheckCnt++;
                        break;
                    }
                }
                if (CheckCnt == 0)
                {
                    myChar.SelectStoneNum = 0;
                }
            }
            else if (myChar.SelectStoneNum == 3)
            {
                int CheckCnt = 0;
                for (int i = 4; i < myChar.ElementStone.Length; i++)
                {
                    if (myChar.ElementStone[i] != 0)
                    {
                        myChar.SelectStoneNum = i;
                        CheckCnt++;
                        break;
                    }
                }
                if (CheckCnt == 0)
                {
                    myChar.SelectStoneNum = 0;
                }
            }

            else if (myChar.SelectStoneNum == 4)
            {
                myChar.SelectStoneNum = 0;
            }
        }
    }
    public void ActiveItemUse()
    {
        myChar.EquipmentAll[myChar.ItemCustomNum] = myChar.ItemCustomLv;
        myChar.EquipmentUsed.Add(myChar.ItemCustomNum);
        myChar.EquipmentUsedTrueCheck.Add(false);

        //추후 왕좌(착용아이템)확인을위해 / 현재 소유중인 장비에 추가하는함수
        //myChar.EnthroneEquipment.Add(myChar.StayEquipment.GetComponent<EquipmentScript>().EquipmentNum);
        myChar.PlayerEquipment.Add(myChar.ItemCustomNum);


        //switch (myChar.ActiveItme_Lv)
        //{
        //    case 0:
        //        ActiveItemLv = (int)myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveItem + 2000).Lv0;
        //        break;
        //    case 1:
        //        ActiveItemLv = (int)myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveItem + 2000).Lv1;
        //        break;
        //    case 2:
        //        ActiveItemLv = (int)myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveItem + 2000).Lv2;
        //        break;
        //    case 3:
        //        ActiveItemLv = (int)myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveItem + 2000).Lv3;
        //        break;
        //    case 4:
        //        ActiveItemLv = (int)myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveItem + 2000).Lv4;
        //        break;
        //    case 5:
        //        ActiveItemLv = (int)myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveItem + 2000).Lv5;
        //        break;
        //    case 6:
        //        ActiveItemLv = (int)myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveItem + 2000).Lv6;
        //        break;
        //}
        //if (Time.timeScale > 0)
        //{
        //    switch (myChar.ActiveItem)
        //    {
        //        case 1:
        //            switch (myChar.ActiveitemAll[0])
        //            {
        //                case 0:
        //                    StartCoroutine(SlowActive(myChar.ActiveitemDataMgr.GetTemplate(2000).Lv0));
        //                    break;
        //                case 1:
        //                    StartCoroutine(SlowActive(myChar.ActiveitemDataMgr.GetTemplate(2000).Lv1));
        //                    break;
        //                case 2:
        //                    StartCoroutine(SlowActive(myChar.ActiveitemDataMgr.GetTemplate(2000).Lv2));
        //                    break;
        //                case 3:
        //                    StartCoroutine(SlowActive(myChar.ActiveitemDataMgr.GetTemplate(2000).Lv3));
        //                    break;
        //                case 4:
        //                    StartCoroutine(SlowActive(myChar.ActiveitemDataMgr.GetTemplate(2000).Lv4));
        //                    break;
        //                case 5:
        //                    StartCoroutine(SlowActive(myChar.ActiveitemDataMgr.GetTemplate(2000).Lv5));
        //                    break;
        //                case 6:
        //                    StartCoroutine(SlowActive(myChar.ActiveitemDataMgr.GetTemplate(2000).Lv6));
        //                    break;
        //            }
        //            Flash();
        //            break;
        //        case 2:
        //            Flash();
        //            ShockWave();
        //            SoundManager.Instance.PlaySfx(24);
        //            switch (myChar.ActiveitemAll[1])
        //            {
        //                case 0:
        //                    MonsterDamage(myChar.ActiveitemDataMgr.GetTemplate(2001).Lv0);
        //                    break;
        //                case 1:
        //                    MonsterDamage(myChar.ActiveitemDataMgr.GetTemplate(2001).Lv1);
        //                    break;
        //                case 2:
        //                    MonsterDamage(myChar.ActiveitemDataMgr.GetTemplate(2001).Lv2);
        //                    break;
        //                case 3:
        //                    MonsterDamage(myChar.ActiveitemDataMgr.GetTemplate(2001).Lv3);
        //                    break;
        //                case 4:
        //                    MonsterDamage(myChar.ActiveitemDataMgr.GetTemplate(2001).Lv4);
        //                    break;
        //                case 5:
        //                    MonsterDamage(myChar.ActiveitemDataMgr.GetTemplate(2001).Lv5);
        //                    break;
        //                case 6:
        //                    MonsterDamage(myChar.ActiveitemDataMgr.GetTemplate(2001).Lv6);
        //                    break;
        //            }
        //            break;
        //        case 3:
        //            Flash();
        //            switch (myChar.ActiveitemAll[2])
        //            {
        //                case 0:
        //                    StartCoroutine(MonsterStun(myChar.ActiveitemDataMgr.GetTemplate(2002).Lv0));
        //                    break;
        //                case 1:
        //                    StartCoroutine(MonsterStun(myChar.ActiveitemDataMgr.GetTemplate(2002).Lv1));
        //                    break;
        //                case 2:
        //                    StartCoroutine(MonsterStun(myChar.ActiveitemDataMgr.GetTemplate(2002).Lv2));
        //                    break;
        //                case 3:
        //                    StartCoroutine(MonsterStun(myChar.ActiveitemDataMgr.GetTemplate(2002).Lv3));
        //                    break;
        //                case 4:
        //                    StartCoroutine(MonsterStun(myChar.ActiveitemDataMgr.GetTemplate(2002).Lv4));
        //                    break;
        //                case 5:
        //                    StartCoroutine(MonsterStun(myChar.ActiveitemDataMgr.GetTemplate(2002).Lv5));
        //                    break;
        //                case 6:
        //                    StartCoroutine(MonsterStun(myChar.ActiveitemDataMgr.GetTemplate(2002).Lv6));
        //                    break;
        //            }
        //            break;
        //        case 4:
        //            Flame_Boob();
        //            break;
        //        case 5:
        //            Frozen_Boob();
        //            break;
        //        case 6:
        //            myChar.SeasonalShield = true;
        //            SeasonalShieldReset();
        //            myChar.SeasonNumber = 1;
        //            break;
        //        case 7:
        //            myChar.SeasonalShield = true;
        //            SeasonalShieldReset();
        //            myChar.SeasonNumber = 2;
        //            break;
        //        case 8:
        //            myChar.SeasonalShield = true;
        //            SeasonalShieldReset();
        //            myChar.SeasonNumber = 3;
        //            break;
        //        case 9:
        //            myChar.SeasonalShield = true;
        //            SeasonalShieldReset();
        //            myChar.SeasonNumber = 4;
        //            myChar.HealingShieldCheck = 3;
        //            break;
        //        case 10:        //공속 포션
        //            switch (myChar.ActiveitemAll[9])
        //            {
        //                case 0:
        //                    StartCoroutine(ASPD_Potion(myChar.ActiveitemDataMgr.GetTemplate(2009).Lv0));
        //                    break;
        //                case 1:
        //                    StartCoroutine(ASPD_Potion(myChar.ActiveitemDataMgr.GetTemplate(2009).Lv1));
        //                    break;
        //                case 2:
        //                    StartCoroutine(ASPD_Potion(myChar.ActiveitemDataMgr.GetTemplate(2009).Lv2));
        //                    break;
        //                case 3:
        //                    StartCoroutine(ASPD_Potion(myChar.ActiveitemDataMgr.GetTemplate(2009).Lv3));
        //                    break;
        //                case 4:
        //                    StartCoroutine(ASPD_Potion(myChar.ActiveitemDataMgr.GetTemplate(2009).Lv4));
        //                    break;
        //                case 5:
        //                    StartCoroutine(ASPD_Potion(myChar.ActiveitemDataMgr.GetTemplate(2009).Lv5));
        //                    break;
        //                case 6:
        //                    StartCoroutine(ASPD_Potion(myChar.ActiveitemDataMgr.GetTemplate(2009).Lv6));
        //                    break;
        //            }
        //            break;
        //        case 11:        //파워 포션
        //            switch (myChar.ActiveitemAll[10])
        //            {
        //                case 0:
        //                    StartCoroutine(Power_Potion(myChar.ActiveitemDataMgr.GetTemplate(2010).Lv0));
        //                    break;
        //                case 1:
        //                    StartCoroutine(Power_Potion(myChar.ActiveitemDataMgr.GetTemplate(2010).Lv1));
        //                    break;
        //                case 2:
        //                    StartCoroutine(Power_Potion(myChar.ActiveitemDataMgr.GetTemplate(2010).Lv2));
        //                    break;
        //                case 3:
        //                    StartCoroutine(Power_Potion(myChar.ActiveitemDataMgr.GetTemplate(2010).Lv3));
        //                    break;
        //                case 4:
        //                    StartCoroutine(Power_Potion(myChar.ActiveitemDataMgr.GetTemplate(2010).Lv4));
        //                    break;
        //                case 5:
        //                    StartCoroutine(Power_Potion(myChar.ActiveitemDataMgr.GetTemplate(2010).Lv5));
        //                    break;
        //                case 6:
        //                    StartCoroutine(Power_Potion(myChar.ActiveitemDataMgr.GetTemplate(2010).Lv6));
        //                    break;
        //            }
        //            break;
        //        case 12:        //무적포션
        //            switch (myChar.ActiveitemAll[11])
        //            {
        //                case 0:
        //                    StartCoroutine(Invincibility_Potion(myChar.ActiveitemDataMgr.GetTemplate(2011).Lv0));
        //                    break;
        //                case 1:
        //                    StartCoroutine(Invincibility_Potion(myChar.ActiveitemDataMgr.GetTemplate(2011).Lv1));
        //                    break;
        //                case 2:
        //                    StartCoroutine(Invincibility_Potion(myChar.ActiveitemDataMgr.GetTemplate(2011).Lv2));
        //                    break;
        //                case 3:
        //                    StartCoroutine(Invincibility_Potion(myChar.ActiveitemDataMgr.GetTemplate(2011).Lv3));
        //                    break;
        //                case 4:
        //                    StartCoroutine(Invincibility_Potion(myChar.ActiveitemDataMgr.GetTemplate(2011).Lv4));
        //                    break;
        //                case 5:
        //                    StartCoroutine(Invincibility_Potion(myChar.ActiveitemDataMgr.GetTemplate(2011).Lv5));
        //                    break;
        //                case 6:
        //                    StartCoroutine(Invincibility_Potion(myChar.ActiveitemDataMgr.GetTemplate(2011).Lv6));
        //                    break;
        //            }
        //            break;
        //        case 13:
        //            Healing_Potion();
        //            break;
        //        case 14:
        //            switch (myChar.ActiveitemAll[13])
        //            {
        //                case 0:
        //                    myChar.Shield += (int)myChar.ActiveitemDataMgr.GetTemplate(2013).Lv0;
        //                    break;
        //                case 1:
        //                    myChar.Shield += (int)myChar.ActiveitemDataMgr.GetTemplate(2013).Lv1;
        //                    break;
        //                case 2:
        //                    myChar.Shield += (int)myChar.ActiveitemDataMgr.GetTemplate(2013).Lv2;
        //                    break;
        //                case 3:
        //                    myChar.Shield += (int)myChar.ActiveitemDataMgr.GetTemplate(2013).Lv3;
        //                    break;
        //                case 4:
        //                    myChar.Shield += (int)myChar.ActiveitemDataMgr.GetTemplate(2013).Lv4;
        //                    break;
        //                case 5:
        //                    myChar.Shield += (int)myChar.ActiveitemDataMgr.GetTemplate(2013).Lv5;
        //                    break;
        //                case 6:
        //                    myChar.Shield += (int)myChar.ActiveitemDataMgr.GetTemplate(2013).Lv6;
        //                    break;
        //            }
        //            ShieldEffect_On();
        //            //GameObject Shield_Effect = Instantiate(ShieldEffect, SelectCharacter.transform.position, Quaternion.identity);
        //            //Shield_Effect.transform.parent = PlayerObjectManager.transform;
        //            //Shield_Effect.transform.rotation = Quaternion.Euler(-90, 0, 0);
        //            //Destroy(Shield_Effect, 5f);
        //            break;
        //        case 15:
        //            switch (myChar.ActiveitemAll[14])
        //            {
        //                case 0:
        //                    StartCoroutine(Reflection_Potion(myChar.ActiveitemDataMgr.GetTemplate(2014).Lv0));
        //                    break;
        //                case 1:
        //                    StartCoroutine(Reflection_Potion(myChar.ActiveitemDataMgr.GetTemplate(2014).Lv1));
        //                    break;
        //                case 2:
        //                    StartCoroutine(Reflection_Potion(myChar.ActiveitemDataMgr.GetTemplate(2014).Lv2));
        //                    break;
        //                case 3:
        //                    StartCoroutine(Reflection_Potion(myChar.ActiveitemDataMgr.GetTemplate(2014).Lv3));
        //                    break;
        //                case 4:
        //                    StartCoroutine(Reflection_Potion(myChar.ActiveitemDataMgr.GetTemplate(2014).Lv4));
        //                    break;
        //                case 5:
        //                    StartCoroutine(Reflection_Potion(myChar.ActiveitemDataMgr.GetTemplate(2014).Lv5));
        //                    break;
        //                case 6:
        //                    StartCoroutine(Reflection_Potion(myChar.ActiveitemDataMgr.GetTemplate(2014).Lv6));
        //                    break;
        //            }
        //            break;
        //    }

            
        //    myChar.ActiveItem = 0;
        //    if (myChar.Tutorial)
        //    {
        //        if (myChar.TutorialNum == 5)
        //        {
        //            myChar.TutoActiveItemUse = true;
        //            myChar.TutoItemUse = true;
        //        }
        //    }

        //}
    }

    public void GamePlay()
    {
        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
            if (myChar.Chapter >= 3)
            {
                myChar.EnthroneCameraOn = true;
            }
        }
        if (Tutorial_Info.activeSelf)
        {
            //Tutorial_Info.SetActive(false);
        }
        if (myChar.Tutorial)
        {
            RoomManager.Instance.TutoNextMapStartCheck = true;
            if (myChar.TutorialNum == 5)
            {
                myChar.BossInfoCheck = false;
            }
        }
    }

    public void Flash()
    {
        if (RoomManagerObj.activeSelf == false)
        {
            GameObject Flasha = Instantiate(FlashEffect, TestMapPos.position, Quaternion.identity);
            Destroy(Flasha, 1.5f);
        }
        else
        {
            GameObject Flasha = Instantiate(FlashEffect, myChar.SelectLocation.transform.position, Quaternion.identity);
            Destroy(Flasha, 1.5f);
        }
    }

    public void ShockWave()
    {

        if (RoomManagerObj.activeSelf == false)
        {
            Instantiate(ShockWaveEffect, TestMapPos.position, Quaternion.identity);
        }
        else
        {
            Instantiate(ShockWaveEffect, myChar.SelectLocation.transform.position, Quaternion.identity);
        }

        if (MonsterProjectile.transform.childCount > 0)
        {
            for (int i = 0; i < MonsterProjectile.transform.childCount; i++)
            {
                Destroy(MonsterProjectile.transform.GetChild(i).gameObject);
            }
        }
    }
    public void StageClear()
    {
        if (MonsterProjectile.transform.childCount > 0)
        {
            for (int i = 0; i < MonsterProjectile.transform.childCount; i++)
            {
                Destroy(MonsterProjectile.transform.GetChild(i).gameObject);
            }
        }
    }
    private void SeasonalShieldReset()
    {
        for (int i = 0; i < 3; i++)
        {
            if (PlayerSeasonalShield.transform.GetChild(i).gameObject.activeSelf == false)
            {
                PlayerSeasonalShield.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    public void RightMoveBtn()
    {
        Right_Btn.GetComponent<Image>().sprite = RightBtnImg[1];
        SelectCharacter.GetComponent<PlayerController>().btnmoveSpeed = 0.75f;
    }
    public void nonClickRightMoveBtn()
    {
        Right_Btn.GetComponent<Image>().sprite = RightBtnImg[0];
        SelectCharacter.GetComponent<PlayerController>().btnmoveSpeed = 0;
    }
    public void LeftMoveBtn()
    {
        Left_Btn.GetComponent<Image>().sprite = LeftBtnImg[1];
        SelectCharacter.GetComponent<PlayerController>().btnmoveSpeed = -0.75f;
    }
    public void nonClickLeftMoveBtn()
    {
        Left_Btn.GetComponent<Image>().sprite = LeftBtnImg[0];
        SelectCharacter.GetComponent<PlayerController>().btnmoveSpeed = 0;
    }
    public void JumpBtn()
    {
        if (Time.timeScale > 0)
        {
            SelectCharacter.GetComponent<PlayerController>().CheckInputPhone();
        }

    }

    public void MonsterDamage(float Damage)     //몬스터 올 데미지
    {
        if (!myChar.Tutorial)
        {
            if (SelectCharacter.GetComponent<PlayerTargeting>().MonsterList.Count > 0)
            {
                for (int i = 0; i < SelectCharacter.GetComponent<PlayerTargeting>().MonsterList.Count; i++)
                {
                    //몬스터가 올킬이안됨 전체 대미지주는건 가능한데 
                    //PlayerTargeting.Instance.MonsterList[i].GetComponent<MonsterScript>().monsterHp -= Damage * ((myChar.ActiveItme_Lv * 0.5f) + 0.5f);
                    if (SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<MonsterScript>())
                    {
                        if (SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<MonsterScript>().monsterHp > 0)
                        {
                            SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<MonsterScript>().AttackHit(Damage);
                        }
                    }
                    else if (SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<AIDestinationSetter>())
                    {
                        if (SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<AIDestinationSetter>().monsterHp > 0)
                        {
                            SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponentInParent<AIDestinationSetter>().AttackHit(Damage);
                        }
                    }
                    else if (SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<BossPatternScript>())
                    {
                        if (SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<BossPatternScript>().monsterHp > 0)
                        {
                            SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<BossPatternScript>().AttackHit(Damage);
                        }
                    }

                }
            }
            //if (PlayerTargeting.Instance.MonsterList.Count > 0)
            //{
            //    Debugddddddddddd.Log(PlayerTargeting.Instance.MonsterList.Count);
            //    for (int i = 0; i < PlayerTargeting.Instance.MonsterList.Count; i++)
            //    {
            //        //몬스터가 올킬이안됨 전체 대미지주는건 가능한데 
            //        //PlayerTargeting.Instance.MonsterList[i].GetComponent<MonsterScript>().monsterHp -= Damage * ((myChar.ActiveItme_Lv * 0.5f) + 0.5f);
            //        if (PlayerTargeting.Instance.MonsterList[i].GetComponent<MonsterScript>() != null)
            //        {
            //            PlayerTargeting.Instance.MonsterList[i].GetComponent<MonsterScript>().AttackHit(Damage * ((myChar.ActiveItme_Lv * 0.5f) + 0.5f));
            //        }
            //        if (PlayerTargeting.Instance.MonsterList[i].GetComponent<AIDestinationSetter>() != null)
            //        {
            //            PlayerTargeting.Instance.MonsterList[i].GetComponent<AIDestinationSetter>().AttackHit(Damage * ((myChar.ActiveItme_Lv * 0.5f) + 0.5f));
            //        }
            //        if (PlayerTargeting.Instance.MonsterList[i].GetComponent<BossPatternScript>() != null)
            //        {
            //            PlayerTargeting.Instance.MonsterList[i].GetComponent<BossPatternScript>().AttackHit(Damage * ((myChar.ActiveItme_Lv * 0.5f) + 0.5f));
            //        }
            //    }
            //}
            else
            {
                return;
            }
        }
        else if (myChar.Tutorial)
        {
            for (int i = 0; i < PlayerTargeting.Instance.MonsterList.Count; i++)
            {
                if (PlayerTargeting.Instance.MonsterList[i].GetComponent<AIDestinationSetter>() != null)
                {
                    PlayerTargeting.Instance.MonsterList[i].GetComponent<AIDestinationSetter>().AttackHit(50);
                }
            }
        }

    }

    private void ActiveShield(int num)
    {
        GameObject ActiveItemCreate = Instantiate(ActiveItem[num], SelectCharacter.transform.position, Quaternion.identity);
        Debug.Log(11);
        //GameObject Shield_Effect = Instantiate(ShieldEffect, SelectCharacter.transform.position, Quaternion.identity);
        //Shield_Effect.transform.parent = PlayerObjectManager.transform;
    }
    private void Healing_Potion()
    {
        switch (myChar.ActiveitemAll[12])
        {
            case 0:
                myChar.currentHp += (int)myChar.ActiveitemDataMgr.GetTemplate(2012).Lv0;
                break;
            case 1:
                myChar.currentHp += (int)myChar.ActiveitemDataMgr.GetTemplate(2012).Lv1;
                break;
            case 2:
                myChar.currentHp += (int)myChar.ActiveitemDataMgr.GetTemplate(2012).Lv2;
                break;
            case 3:
                myChar.currentHp += (int)myChar.ActiveitemDataMgr.GetTemplate(2012).Lv3;
                break;
            case 4:
                myChar.currentHp += (int)myChar.ActiveitemDataMgr.GetTemplate(2012).Lv4;
                break;
            case 5:
                myChar.currentHp += (int)myChar.ActiveitemDataMgr.GetTemplate(2012).Lv5;
                break;
            case 6:
                myChar.currentHp += (int)myChar.ActiveitemDataMgr.GetTemplate(2012).Lv6;
                break;
        }
        HealingEffect_On();
        //GameObject Healing_Effect = Instantiate(HealEffect, SelectCharacter.transform.position, Quaternion.identity);
        //Healing_Effect.transform.parent = PlayerObjectManager.transform;
        //Destroy(Healing_Effect, 5f);
    }

    //public void FlameStone()
    //{
    //    float OriginDamage = myChar.CurrentDamage;
    //    if (myChar.SelectStoneNum == 1)
    //    {
    //        if (myChar.ElementStone_Lv == 1)
    //        {
    //            myChar.CurrentDamage = OriginDamage * 1.1f;
    //        }
    //        else if (myChar.ElementStone_Lv == 2)
    //        {
    //            myChar.CurrentDamage = OriginDamage * 1.25f;
    //        }
    //        else if (myChar.ElementStone_Lv == 3)
    //        {
    //            myChar.CurrentDamage = OriginDamage * 1.5f;
    //        }
    //    }
    //    else
    //    {
    //        myChar.CurrentDamage = OriginDamage;
    //    }
    //}


    private void Flame_Boob()
    {
        GameObject Flame_Boob = Instantiate(FlameBoob, SelectCharacter.transform.position, Quaternion.identity);
        Flame_Boob.transform.parent = PlayerObjectManager.transform;
        switch (myChar.ActiveitemAll[3])
        {
            case 0:
                Flame_Boob.GetComponent<BoobScript>().Damage = myChar.ActiveitemDataMgr.GetTemplate(2003).Lv0;
                break;
            case 1:
                Flame_Boob.GetComponent<BoobScript>().Damage = myChar.ActiveitemDataMgr.GetTemplate(2003).Lv1;
                break;
            case 2:
                Flame_Boob.GetComponent<BoobScript>().Damage = myChar.ActiveitemDataMgr.GetTemplate(2003).Lv2;
                break;
            case 3:
                Flame_Boob.GetComponent<BoobScript>().Damage = myChar.ActiveitemDataMgr.GetTemplate(2003).Lv3;
                break;
            case 4:
                Flame_Boob.GetComponent<BoobScript>().Damage = myChar.ActiveitemDataMgr.GetTemplate(2003).Lv4;
                break;
            case 5:
                Flame_Boob.GetComponent<BoobScript>().Damage = myChar.ActiveitemDataMgr.GetTemplate(2003).Lv5;
                break;
            case 6:
                Flame_Boob.GetComponent<BoobScript>().Damage = myChar.ActiveitemDataMgr.GetTemplate(2003).Lv6;
                break;
        }
        Destroy(Flame_Boob, 4);
    }
    private void Frozen_Boob()
    {
        GameObject Frozen_Boob = Instantiate(FrozenBoob, SelectCharacter.transform.position, Quaternion.identity);
        Frozen_Boob.transform.parent = PlayerObjectManager.transform;
        switch (myChar.ActiveitemAll[4])
        {
            case 0:
                Frozen_Boob.GetComponent<BoobScript>().SlowTime = myChar.ActiveitemDataMgr.GetTemplate(2004).Lv0;
                break;
            case 1:
                Frozen_Boob.GetComponent<BoobScript>().SlowTime = myChar.ActiveitemDataMgr.GetTemplate(2004).Lv1;
                break;
            case 2:
                Frozen_Boob.GetComponent<BoobScript>().SlowTime = myChar.ActiveitemDataMgr.GetTemplate(2004).Lv2;
                break;
            case 3:
                Frozen_Boob.GetComponent<BoobScript>().SlowTime = myChar.ActiveitemDataMgr.GetTemplate(2004).Lv3;
                break;
            case 4:
                Frozen_Boob.GetComponent<BoobScript>().SlowTime = myChar.ActiveitemDataMgr.GetTemplate(2004).Lv4;
                break;
            case 5:
                Frozen_Boob.GetComponent<BoobScript>().SlowTime = myChar.ActiveitemDataMgr.GetTemplate(2004).Lv5;
                break;
            case 6:
                Frozen_Boob.GetComponent<BoobScript>().SlowTime = myChar.ActiveitemDataMgr.GetTemplate(2004).Lv6;
                break;
        }
        Destroy(Frozen_Boob, 4);
    }
    //public void HealingEffect()
    //{
    //    GameObject Healing_Effect = Instantiate(HealEffect, SelectCharacter.transform.position, Quaternion.identity);
    //    Healing_Effect.transform.parent = PlayerObjectManager.transform;
    //    Destroy(Healing_Effect, 5f);
    //}
    private void SeasonalShieldCheck()
    {
        if (myChar.SeasonalShield)
        {
            PlayerSeasonalShield.SetActive(true);
        }
        else if (!myChar.SeasonalShield)
        {
            PlayerSeasonalShield.SetActive(false);
        }
    }

    private void GiftBoxWindowCheck()
    {
        if (GiftBoxWindow.activeSelf)
        {
            if (myChar.Key >= 1)
            {
                GiftBoxWindow.transform.GetChild(0).Find("GiftKey_Btn").gameObject.SetActive(true);
                GiftBoxWindow.transform.GetChild(0).Find("GiftGem_Btn").gameObject.SetActive(false);
                GiftBoxWindow.transform.GetChild(0).Find("GiftNotEnoughGem_Btn").gameObject.SetActive(false);
            }
            else if (myChar.Key <= 0)
            {
                GiftBoxWindow.transform.GetChild(0).Find("GiftKey_Btn").gameObject.SetActive(false);
                GiftBoxWindow.transform.GetChild(0).Find("GiftGem_Btn").gameObject.SetActive(true);
                if (myChar.Gem >= 20)
                {
                    GiftBoxWindow.transform.GetChild(0).Find("GiftGem_Btn").gameObject.SetActive(true);
                    GiftBoxWindow.transform.GetChild(0).Find("GiftNotEnoughGem_Btn").gameObject.SetActive(false);
                }
                else
                {
                    GiftBoxWindow.transform.GetChild(0).Find("GiftGem_Btn").gameObject.SetActive(false);
                    GiftBoxWindow.transform.GetChild(0).Find("GiftNotEnoughGem_Btn").gameObject.SetActive(true);
                }
            }

            if (myChar.Tutorial)
            {
                GiftBoxWindow.transform.GetChild(0).Find("AD_Btn").gameObject.SetActive(false);
            }
            else
            {
                GiftBoxWindow.transform.GetChild(0).Find("AD_Btn").gameObject.SetActive(true);
            }
        }
    }
    IEnumerator MonsterStun(float Time)         //몬스터 스턴
    {
        for (int i = 0; i < SelectCharacter.GetComponent<PlayerTargeting>().MonsterList.Count; i++)
        {
            //PlayerTargeting.Instance.MonsterList[i].GetComponent<MonsterScript>().isStun = true;
            if (SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<MonsterScript>() != null)
            {
                SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<MonsterScript>().preFireCount = PlayerTargeting.Instance.MonsterList[i].GetComponent<MonsterScript>().fireCountdown;
                SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<MonsterScript>().fireCountdown = 500f;
            }
            //if (PlayerTargeting.Instance.MonsterList[i].GetComponent<BossPatternScript>() != null)
            //{
            //    PlayerTargeting.Instance.MonsterList[i].GetComponent<BossPatternScript>().AttackHit(Damage * ((myChar.ActiveItme_Lv * 0.5f) + 0.5f));
            //}            
        }
        myChar.Stun = 0;

        yield return new WaitForSeconds(Time);

        for (int i = 0; i < SelectCharacter.GetComponent<PlayerTargeting>().MonsterList.Count; i++)
        {
            //PlayerTargeting.Instance.MonsterList[i].GetComponent<MonsterScript>().isStun = false;
            if (SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<MonsterScript>())
            {
                SelectCharacter.GetComponent<PlayerTargeting>().MonsterList[i].GetComponent<MonsterScript>().fireCountdown = PlayerTargeting.Instance.MonsterList[i].GetComponent<MonsterScript>().preFireCount;
            }

        }
        myChar.Stun = 1;
    }
    public void DestroyItem()
    {
        SoundManager.Instance.PlaySfx(3);
        if (myChar.StayEquipment.GetComponent<OrbScript>() != null)
        {
            if (myChar.StayEquipment.GetComponent<OrbScript>().Orb_Ability < 4)
            {
                if (myChar.Coin >= 10)
                {
                    myChar.ElementStone[(myChar.StayEquipment.GetComponent<OrbScript>().Orb_Ability + 1)] += 20;
                    for (int i = 0; i < myChar.PlayerEquipment.Count; i++)
                    {
                        if (myChar.PlayerEquipment[i] == (myChar.StayEquipment.GetComponent<OrbScript>().Orb_Ability + 1000))
                        {
                            myChar.StayEquipment.GetComponent<OrbScript>().ObtainItemCheck = true;
                        }
                    }
                    if (!myChar.StayEquipment.GetComponent<OrbScript>().ObtainItemCheck)
                    {
                        myChar.PlayerEquipment.Add(myChar.StayEquipment.GetComponent<OrbScript>().Orb_Ability + 1000);
                        myChar.EnthroneEquipment.Add(myChar.StayEquipment.GetComponent<OrbScript>().Orb_Ability + 1000);
                    }
                    //switch (myChar.ActiveitemAll[myChar.StayEquipment.GetComponent<OrbScript>().Orb_Ability])
                    //{
                    //    case 0:
                    //        myChar.ElementStone[(myChar.StayEquipment.GetComponent<OrbScript>().Orb_Ability + 1)] += 20;
                    //        break;
                    //    case 1:
                    //        myChar.ElementStone[(myChar.StayEquipment.GetComponent<OrbScript>().Orb_Ability + 1)] += 30;
                    //        break;
                    //    case 2:
                    //        myChar.ElementStone[(myChar.StayEquipment.GetComponent<OrbScript>().Orb_Ability + 1)] += 50;
                    //        break;
                    //}
                    if (myChar.Stage % 3 == 0 || myChar.Stage % 6 == 0)
                    {
                        RoomManager.Instance.ShopCostboard[myChar.StayEquipment.transform.parent.parent.GetSiblingIndex()] = false;
                    }
                    Destroy(myChar.StayEquipment);
                    myChar.Coin -= 10;
                }
            }
            //else if (myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum == 2015)
            //{
            //    if (myChar.Coin >= 50)
            //    {
            //        myChar.currentHp++;
            //        if (myChar.Stage == myChar.BasicStage + 2)
            //        {
            //            RoomManager.Instance.ShopCostboard[myChar.StayEquipment.transform.parent.parent.GetSiblingIndex()] = false;
            //        }
            //        Destroy(myChar.StayEquipment);
            //        myChar.Coin -= 50;
            //    }
            //}

        }
        if (myChar.StayEquipment.GetComponent<EquipmentScript>() != null)
        {
            if (myChar.Stage % 10 != 9)
            {
                if (myChar.StayEquipment.GetComponent<EquipmentScript>().EquipmentNum < 30)
                {
                    if (myChar.Coin >= 10)
                    {
                        ItemObtain();
                        myChar.Coin -= 10;
                    }
                }
                else if (myChar.StayEquipment.GetComponent<EquipmentScript>().EquipmentNum >= 30 && myChar.StayEquipment.GetComponent<EquipmentScript>().EquipmentNum <= 60)
                {
                    if (myChar.Coin >= 20)
                    {
                        ItemObtain();
                        myChar.Coin -= 20;
                    }
                }
                else if (myChar.StayEquipment.GetComponent<EquipmentScript>().EquipmentNum >= 61 && myChar.StayEquipment.GetComponent<EquipmentScript>().EquipmentNum <= 87)
                {
                    if (myChar.Coin >= 30)
                    {
                        ItemObtain();
                        myChar.Coin -= 30;
                    }
                }
            }
            else
            {
                ItemObtain();
                RoomManager.Instance.DontTouchBtn_Panel.SetActive(true);
            }
        }

        if (myChar.StayEquipment.GetComponent<ActiveItemScript>() != null)
        {
            if (myChar.Tutorial)
            {
                RoomManager.Instance.TutoCheck++;
            }
            if (myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum <= 2011)
            {
                AntiquityObtain();
                RoomManager.Instance.DontTouchBtn_Panel.SetActive(true);
            }
            else if (myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum == 2012)
            {
                if (myChar.Coin >= 10)
                {
                    if (myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum == 2012)
                    {
                        if (GameManager.Instance.PlayerSkill[6] >= 1)
                        {
                            myChar.currentHp += (int)Mathf.Ceil(myChar.maxHp * 0.55f);
                        }
                        else if (GameManager.Instance.PlayerSkill[6] == 0)
                        {
                            myChar.currentHp += (int)Mathf.Ceil(myChar.maxHp * 0.5f);
                        }
                        //switch (myChar.ActiveitemAll[myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum - 2000])
                        //{
                        //    case 0:
                        //        myChar.currentHp += (int)Mathf.Ceil(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum).Lv1 / 100));
                        //        break;
                        //    case 1:
                        //        myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum).Lv2 / 100));
                        //        break;
                        //    case 2:
                        //        myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum).Lv3 / 100));
                        //        break;
                        //    case 3:
                        //        myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum).Lv4 / 100));
                        //        break;
                        //    case 4:
                        //        myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum).Lv5 / 100));
                        //        break;
                        //    case 5:
                        //        myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum).Lv6 / 100));
                        //        break;
                        //    case 6:
                        //        myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum).Lv7 / 100));
                        //        break;
                        //    case 7:
                        //        myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum).Lv8 / 100));
                        //        break;
                        //    case 8:
                        //        myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum).Lv9 / 100));
                        //        break;
                        //    case 9:
                        //        myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum).Lv10 / 100));
                        //        break;
                        //    case 10:
                        //        myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(myChar.StayEquipment.GetComponent<ActiveItemScript>().ActiveitemNum).Lv11 / 100));
                        //        break;
                        //}
                        GameObject Healing_Effect = Instantiate(HealEffect, SelectCharacter.transform.position, Quaternion.identity);
                        Healing_Effect.transform.parent = PlayerObjectManager.transform;
                        Destroy(Healing_Effect, 5f);
                        myChar.Coin -= 10;
                    }
                    if (myChar.Stage % 10 == 3 || myChar.Stage % 10 == 6)
                    {
                        RoomManager.Instance.ShopCostboard[myChar.StayEquipment.transform.parent.parent.GetSiblingIndex()] = false;
                    }
                    Destroy(myChar.StayEquipment);
                }
            }
        }
    }

    private void ItemObtain()
    {
        myChar.EquipmentUsed.Add(myChar.StayEquipment.GetComponent<EquipmentScript>().EquipmentNum);
        myChar.EquipmentUsedTrueCheck.Add(false);

        //추후 왕좌(착용아이템)확인을위해 / 현재 소유중인 장비에 추가하는함수
        //myChar.EnthroneEquipment.Add(myChar.StayEquipment.GetComponent<EquipmentScript>().EquipmentNum);
        myChar.PlayerEquipment.Add(myChar.StayEquipment.GetComponent<EquipmentScript>().EquipmentNum);

        //RoomManager.Instance.DontTouchBtn_Panel.SetActive(true);

        GameObject Item = Instantiate(ObtainItem, SelectCharacter.transform.position, Quaternion.identity);
        SelectCharacter.GetComponent<PlayerController>()._anim.SetBool("Obtain", true);
        SoundManager.Instance.PlaySfx(52);
        Item.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + (myChar.StayEquipment.GetComponent<EquipmentScript>().EquipmentNum).ToString());
        Item.transform.parent = SelectCharacter.transform;
        Item.transform.localPosition = new Vector3(0, 0.2f, 0);
        Item.transform.localScale = new Vector3(1, 1, 1);
        Item.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        //아이템 샵에서 가격표 사라지게하는 부분
        if (RoomManager.Instance.EquipmentRoom)
        {
            RoomManager.Instance.ShopCostboard[myChar.StayEquipment.transform.parent.parent.GetSiblingIndex()] = false;
        }

        Destroy(myChar.StayEquipment);
    }

    private void AntiquityObtain()
    {

        //추후 왕좌(착용아이템)확인을위해 / 현재 소유중인 장비에 추가하는함수
        //myChar.EnthroneEquipment.Add(myChar.StayEquipment.GetComponent<EquipmentScript>().EquipmentNum);
        myChar.PlayerEquipment.Add(myChar.StayEquipment.GetComponent<ActiveItemScript>().ItemNum);

        //RoomManager.Instance.DontTouchBtn_Panel.SetActive(true);

        GameObject Item = Instantiate(ObtainItem, SelectCharacter.transform.position, Quaternion.identity);
        SelectCharacter.GetComponent<PlayerController>()._anim.SetBool("Obtain", true);
        SoundManager.Instance.PlaySfx(52);
        Item.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + (myChar.StayEquipment.GetComponent<ActiveItemScript>().ItemNum).ToString());
        Item.transform.parent = SelectCharacter.transform;
        Item.transform.localPosition = new Vector3(0, 0.2f, 0);
        Item.transform.localScale = new Vector3(1, 1, 1);
        Item.transform.GetChild(0).localScale = new Vector3(1, 1, 1);

        AntiquityUse(myChar.StayEquipment.GetComponent<ActiveItemScript>().ItemNum);

        Destroy(myChar.StayEquipment);


    }
    private void AntiquityUse(int ItemNum)
    {
        if (ItemNum == 2000 || ItemNum == 2004 || ItemNum == 2008)
        {
            switch (myChar.ActiveitemAll[ItemNum - 2000] )
            {
                case 0:
                    myChar.CurrentDamage += myChar.CurrentDamage * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1;
                    break;
                case 1:
                    myChar.CurrentDamage += myChar.CurrentDamage * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2;
                    break;
                case 2:
                    myChar.CurrentDamage += myChar.CurrentDamage * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3;
                    break;
                case 3:
                    myChar.CurrentDamage += myChar.CurrentDamage * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4;
                    break;
                case 4:
                    myChar.CurrentDamage += myChar.CurrentDamage * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5;
                    break;
                case 5:
                    myChar.CurrentDamage += myChar.CurrentDamage * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6;
                    break;
                case 6:
                    myChar.CurrentDamage += myChar.CurrentDamage * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7;
                    break;
                case 7:
                    myChar.CurrentDamage += myChar.CurrentDamage * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8;
                    break;
                case 8:
                    myChar.CurrentDamage += myChar.CurrentDamage * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9;
                    break;
                case 9:
                    myChar.CurrentDamage += myChar.CurrentDamage * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10;
                    break;
                case 10:
                    myChar.CurrentDamage += myChar.CurrentDamage * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11;
                    break;
            }
        }
        else if (ItemNum == 2001 || ItemNum == 2005 || ItemNum == 2009)
        {
            switch (myChar.ActiveitemAll[ItemNum - 2000])
            {
                case 0:
                    myChar.ASPD += myChar.ASPD * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1;
                    break;
                case 1:
                    myChar.ASPD += myChar.ASPD * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2;
                    break;
                case 2:
                    myChar.ASPD += myChar.ASPD * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3;
                    break;
                case 3:
                    myChar.ASPD += myChar.ASPD * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4;
                    break;
                case 4:
                    myChar.ASPD += myChar.ASPD * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5;
                    break;
                case 5:
                    myChar.ASPD += myChar.ASPD * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6;
                    break;
                case 6:
                    myChar.ASPD += myChar.ASPD * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7;
                    break;
                case 7:
                    myChar.ASPD += myChar.ASPD * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8;
                    break;
                case 8:
                    myChar.ASPD += myChar.ASPD * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9;
                    break;
                case 9:
                    myChar.ASPD += myChar.ASPD * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10;
                    break;
                case 10:
                    myChar.ASPD += myChar.ASPD * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11;
                    break;
            }
        }
        else if (ItemNum == 2002 || ItemNum == 2006 || ItemNum == 2010)
        {
            switch (myChar.ActiveitemAll[ItemNum - 2000])
            {
                case 0:
                    myChar.Range += myChar.Range * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1;
                    break;
                case 1:
                    myChar.Range += myChar.Range * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2;
                    break;
                case 2:
                    myChar.Range += myChar.Range * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3;
                    break;
                case 3:
                    myChar.Range += myChar.Range * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4;
                    break;
                case 4:
                    myChar.Range += myChar.Range * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5;
                    break;
                case 5:
                    myChar.Range += myChar.Range * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6;
                    break;
                case 6:
                    myChar.Range += myChar.Range * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7;
                    break;
                case 7:
                    myChar.Range += myChar.Range * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8;
                    break;
                case 8:
                    myChar.Range += myChar.Range * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9;
                    break;
                case 9:
                    myChar.Range += myChar.Range * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10;
                    break;
                case 10:
                    myChar.Range += myChar.Range * 0.01f * myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11;
                    break;
            }
        }
        else if (ItemNum == 2003 || ItemNum == 2007 || ItemNum == 2011)
        {
            switch (myChar.ActiveitemAll[ItemNum - 2000])
            {
                case 0:
                    myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1 / 100));
                    myChar.maxHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1 / 100));
                    break;
                case 1:
                    myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2 / 100));
                    myChar.maxHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2 / 100));
                    
                    break;
                case 2:
                    myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3 / 100));
                    myChar.maxHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3 / 100));
                    
                    break;
                case 3:
                    myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4 / 100));
                    myChar.maxHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4 / 100));                    
                    break;
                case 4:
                    myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5 / 100));
                    myChar.maxHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5 / 100));                    
                    break;
                case 5:
                    myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6 / 100));
                    myChar.maxHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6 / 100));                    
                    break;
                case 6:
                    myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7 / 100));
                    myChar.maxHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7 / 100));
                    break;
                case 7:
                    myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8 / 100));
                    myChar.maxHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8 / 100));
                    break;
                case 8:
                    myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9 / 100));
                    myChar.maxHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9 / 100));
                    break;
                case 9:
                    myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10 / 100));
                    myChar.maxHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10 / 100));
                    break;
                case 10:
                    myChar.currentHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11 / 100));
                    myChar.maxHp += (int)(myChar.maxHp * (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11 / 100));
                    break;
            }
        }
    }
    //public void BossNamePanelImgCheck()
    //{
    //    for (int i = 0; i < BossImgPanel.Count; i++)
    //    {
    //        if (i == myChar.SelectedStage)
    //        {
    //            BossImgPanel[i].SetActive(true);
    //        }
    //        else
    //        {
    //            BossImgPanel[i].SetActive(false);
    //        }
    //    }

    //}
    public void BossNamePanelImgCheck(int Num)
    {
        for (int i = 0; i < BossImgPanel.Count; i++)
        {
            if (i == Num)
            {
                BossImgPanel[i].SetActive(true);
            }
            else if (i != Num)
            {
                BossImgPanel[i].SetActive(false);
            }
        }
    }
    public void PanelOn()
    {
        if (!myChar.Tutorial)
        {
            if (myChar.Chapter + 1 % 10 != 3 && myChar.Chapter + 1 % 10 != 6)
            {
                if (myChar.Stage % 50 == 0)
                {
                    if (!myChar.BossNamePanel)
                    {
                        if (myChar.BossHPAnim)
                        {
                            BossNamePanel.SetActive(true);
                            myChar.BossNamePanel = true;
                            SoundManager.Instance.PlaySfx(0);
                        }
                    }
                }
                else
                {
                    myChar.BossNamePanel = false;
                }
            }
            else
            {
                if (myChar.Stage % 30 == 0)
                {
                    if (!myChar.BossNamePanel)
                    {
                        if (myChar.BossHPAnim)
                        {
                            BossNamePanel.SetActive(true);
                            myChar.BossNamePanel = true;
                            SoundManager.Instance.PlaySfx(0);
                        }
                    }
                }
                else
                {
                    myChar.BossNamePanel = false;
                }
            }
        }
        else
        {
            if (myChar.TutorialNum == 5)
            {
                if (!myChar.BossNamePanel)
                {
                    if (myChar.BossHPAnim)
                    {
                        BossNamePanel.SetActive(true);
                        myChar.BossNamePanel = true;
                        SoundManager.Instance.PlaySfx(0);
                    }
                }
            }            
        }
        
        //if (myChar.Chapter < 3)
        //{
        //    if (myChar.Stage == (myChar.BasicStage + 3))
        //    {
        //        if (!myChar.BossNamePanel)
        //        {
        //            if (myChar.BossHPAnim)
        //            {
        //                BossNamePanel.SetActive(true);
        //                myChar.BossNamePanel = true;
        //                SoundManager.Instance.PlaySfx(0);
        //            }
        //        }
        //    }
        //    else if (myChar.Stage == (myChar.BasicStage + 4))
        //    {
        //        myChar.BossNamePanel = false;
        //    }
        //}
        //else if (myChar.Chapter >= 3)
        //{
        //    if (!myChar.BossNamePanel)
        //    {
        //        if (myChar.BossHPAnim)
        //        {
        //            BossNamePanel.SetActive(true);
        //            myChar.BossNamePanel = true;
        //            SoundManager.Instance.PlaySfx(0);
        //        }
        //    }
        //}
    }

    void LoadingPanelCheck()
    {
        if (LoadingPanel.activeSelf == true)
        {
            Loding_Text.GetComponent<Text>().text = (GameObject.Find("Liding_img").GetComponent<Image>().fillAmount * 100f).ToString("F0") + "%";

            for (int i = 0; i < LodingDot.Length; i++)
            {
                if (i == myChar.Chapter)
                {
                    LodingDot[i].transform.GetChild(0).transform.gameObject.SetActive(false);
                    LodingDot[i].transform.GetChild(1).transform.gameObject.SetActive(true);
                    //LodingDot[i+1].transform.GetChild(0).transform.gameObject.SetActive(false);
                }
                else
                {
                    LodingDot[i].transform.GetChild(0).transform.gameObject.SetActive(true);
                    LodingDot[i].transform.GetChild(1).transform.gameObject.SetActive(false);
                    //LodingDot[i+1].transform.GetChild(0).transform.gameObject.SetActive(true);
                }
            }
            for (int i = 0; i < myChar.HeroNum; i++)
            {
                if (i == myChar.SelectHero)
                {
                    LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(i).gameObject.SetActive(false);
                }
            }
            switch (myChar.EquipmentCostume[myChar.SelectHero])
            {
                case 0:
                    for (int i = 0; i < 5; i++)
                    {
                        LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(myChar.SelectHero).GetComponent<Animator>().SetLayerWeight(i, 0);
                    }
                    break;
                case 1:
                    for (int i = 1; i <= 5; i++)
                    {
                        if (i == myChar.EquipmentCostume[myChar.SelectHero])
                        {
                            LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(myChar.SelectHero).GetComponent<Animator>().SetLayerWeight(i, 1);
                        }
                        else
                        {
                            LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(myChar.SelectHero).GetComponent<Animator>().SetLayerWeight(i, 0);
                        }
                    }
                    break;
                case 2:
                    for (int i = 1; i <= 5; i++)
                    {
                        if (i == myChar.EquipmentCostume[myChar.SelectHero])
                        {
                            LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(myChar.SelectHero).GetComponent<Animator>().SetLayerWeight(i, 1);
                        }
                        else
                        {
                            LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(myChar.SelectHero).GetComponent<Animator>().SetLayerWeight(i, 0);
                        }
                    }
                    break;
                case 3:
                    for (int i = 1; i <= 5; i++)
                    {
                        if (i == myChar.EquipmentCostume[myChar.SelectHero])
                        {
                            LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(myChar.SelectHero).GetComponent<Animator>().SetLayerWeight(i, 1);
                        }
                        else
                        {
                            LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(myChar.SelectHero).GetComponent<Animator>().SetLayerWeight(i, 0);
                        }
                    }
                    break;
                case 4:
                    for (int i = 1; i <= 5; i++)
                    {
                        if (i == myChar.EquipmentCostume[myChar.SelectHero])
                        {
                            LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(myChar.SelectHero).GetComponent<Animator>().SetLayerWeight(i, 1);
                        }
                        else
                        {
                            LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(myChar.SelectHero).GetComponent<Animator>().SetLayerWeight(i, 0);
                        }
                    }
                    break;
                case 5:
                    for (int i = 1; i <= 5; i++)
                    {
                        if (i == myChar.EquipmentCostume[myChar.SelectHero])
                        {
                            LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(myChar.SelectHero).GetComponent<Animator>().SetLayerWeight(i, 1);
                        }
                        else
                        {
                            LoadingPanel.transform.GetChild(0).Find("Anim_Character").GetChild(myChar.SelectHero).GetComponent<Animator>().SetLayerWeight(i, 0);
                        }
                    }
                    break;
            }
        }
    }
    private void Tutorial_InfoText()
    {
        Text TutoText = Tutorial_Info.transform.GetChild(1).GetComponentInChildren<Text>();
        switch (myChar.TutorialNum)
        {
            case 0:
                Tutorial_Info.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(1080f, 200f);
                Tutorial_Info.transform.GetChild(1).GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 860f, 0);
                if (Time.timeScale < 1)
                {
                    if (!myChar.TouchInfo)
                    {
                        TutoText.text = myChar.TextDataMgr.GetTemplate(249).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    }
                    else
                    {
                        Tutorial_Mask.SetActive(true);
                        TutoText.text = myChar.TextDataMgr.GetTemplate(250).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    }
                }
                else
                {
                    Tutorial_Mask.SetActive(false);
                    TutoText.text = myChar.TextDataMgr.GetTemplate(209).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }
                break;
            case 1:
                if (Time.timeScale < 1)
                {
                    //Tutorial_Anim.SetActive(true);
                    //Tutorial_Anim.transform.GetChild(0).gameObject.SetActive(true);
                    TutoText.text = myChar.TextDataMgr.GetTemplate(250).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }
                else
                {
                    TutoText.text = myChar.TextDataMgr.GetTemplate(210).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }
                break;
            case 2:
                if (Time.timeScale < 1)
                {
                    //Tutorial_Anim.SetActive(true);
                    //Tutorial_Anim.transform.GetChild(0).gameObject.SetActive(true);
                    TutoText.text = myChar.TextDataMgr.GetTemplate(250).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }
                else
                {
                    TutoText.text = myChar.TextDataMgr.GetTemplate(211).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }
                break;
            case 3:
                if (Time.timeScale < 1)
                {
                    TutoText.text = myChar.TextDataMgr.GetTemplate(250).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }
                else
                {
                    if (!StoneUsedCheck)
                    {
                        StartCoroutine(TutorialStoneUsedCheck());
                        StoneUsedCheck = true;
                    }
                    if (myChar.TutoStoneUse)
                    {
                        Tutorial_Mask.SetActive(false);
                    }
                    TutoText.text = myChar.TextDataMgr.GetTemplate(212).Content[myChar.LanguageNum].Replace("\\n", "\n");
                }
                break;
            //case 4:
            //    TutoText.text = myChar.TextDataMgr.GetTemplate(259).Content[myChar.LanguageNum].Replace("\\n", "\n");
            //    break;
            //case 5:
            //    if (Time.timeScale < 1)
            //    {
            //        TutoText.text = myChar.TextDataMgr.GetTemplate(260).Content[myChar.LanguageNum].Replace("\\n", "\n");
            //    }
            //    else
            //    {
            //        if (!ActiveItemUsedCheck)
            //        {
            //            StartCoroutine(TutorialActiveItemUsedCheck());
            //            ActiveItemUsedCheck = true;
            //        }
            //        if (myChar.TutoItemUse)
            //        {
            //            Tutorial_Mask.SetActive(false);
            //        }
            //    }
            //    break;
            case 4:
                TutoText.text = myChar.TextDataMgr.GetTemplate(215).Content[myChar.LanguageNum].Replace("\\n", "\n");
                break;
            case 5:
                if (myChar.BossInfoCheck)
                {
                    Tutorial_Anim.SetActive(true);
                    //Invoke("TutorialInfoOnDealay", 1f);
                    if (!BossNamePanel.activeSelf)
                    {
                        Tutorial_Info.SetActive(true);
                    }
                    Tutorial_Info.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(1080f, 300f);
                    Tutorial_Info.transform.GetChild(1).GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 300f, 0);
                    TutoText.text = myChar.TextDataMgr.GetTemplate(216).Content[myChar.LanguageNum].Replace("\\n", "\n");
                    Tutorial_Mask.SetActive(true);
                }
                else
                {
                    Tutorial_Anim.SetActive(false);
                    Tutorial_Info.SetActive(false);
                    Tutorial_Mask.SetActive(false);
                }                
                break;
        }
    }
    public void TutorialInfoOnDealay()
    {
        Tutorial_Info.SetActive(true);
    }
    public void StoreCostCheck()
    {
        if (RoomManager.Instance.EquipmentRoom)
        {
            for (int i = 0; i < 4; i++)
            {
                StoreUI.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = myChar.EquipmentCost[i].ToString();
            }
        }
    }
    //일시정지 화면체크
    private void PreferencesWindowCheck()
    {
        if (PreferencesWindow.activeSelf)
        {
            Time.timeScale = 0f;
            //PreferencesWindow.transform.Find("PreferencesWindow_Currency").Find("Gold_UI").GetComponentInChildren<Text>().text = myChar.Coin.ToString("F0");
            PreferencesWindow.transform.Find("PreferencesWindow_Currency").Find("Gem_UI").GetComponentInChildren<Text>().text = myChar.Gem.ToString("F0");
            PreferencesWindow.transform.Find("HeroCurrency").Find("Soul_UI").GetComponentInChildren<Text>().text = myChar.SoulSpark.ToString("F0");
            PreferencesWindow.transform.Find("HeroCurrency").Find("Heart_UI").GetComponentInChildren<Text>().text = myChar.HeroHeart.ToString("F0");

            PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Atk").Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n");
            PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("ASPD").Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n");
            PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("Range").Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n");
            PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("HP").Find("Status_Text").GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n");
            //Debug.Log(PreferencesWindow.transform.Find("Equipment_ScrollView").GetChild(0).GetChild(0).childCount);
            if (myChar.PlayerEquipment.Count <= 0)
            {
                PreferencesMorePanel.SetActive(false);
            }
            else
            {
                if (PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition.y == 0)
                {
                    PreferencesMorePanel.SetActive(true);
                }                
            }

            if (myChar.muteEffectSound)
            {
                PreferencesWindow.transform.Find("Btn_Panel").Find("Effect_On_Btn").gameObject.SetActive(false);
                PreferencesWindow.transform.Find("Btn_Panel").Find("Effect_Off_Btn").gameObject.SetActive(true);
            }
            else if (!myChar.muteEffectSound)
            {
                PreferencesWindow.transform.Find("Btn_Panel").Find("Effect_On_Btn").gameObject.SetActive(true);
                PreferencesWindow.transform.Find("Btn_Panel").Find("Effect_Off_Btn").gameObject.SetActive(false);
            }

            if (myChar.muteBGM)
            {
                PreferencesWindow.transform.Find("Btn_Panel").Find("BGM_On_Btn").gameObject.SetActive(false);
                PreferencesWindow.transform.Find("Btn_Panel").Find("BGM_Off_Btn").gameObject.SetActive(true);
            }
            else if (!myChar.muteBGM)
            {
                PreferencesWindow.transform.Find("Btn_Panel").Find("BGM_On_Btn").gameObject.SetActive(true);
                PreferencesWindow.transform.Find("Btn_Panel").Find("BGM_Off_Btn").gameObject.SetActive(false);
            }

            GameObject ItemCotent = PreferencesWindow.transform.Find("Equipment_ScrollView").GetChild(0).GetChild(0).gameObject;

            for (int i = 0; i < ItemCotent.transform.childCount; i++)
            {
                if (i < myChar.PlayerEquipment.Count)
                {
                    ItemCotent.transform.GetChild(i).gameObject.SetActive(true);
                    ItemCotent.transform.GetChild(i).GetComponent<SelectNumScript>().ItemIndex = myChar.PlayerEquipment[i];
                    //EquipmentUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + myChar.itemDataMgr.GetTemplate(myChar.EquipmentUsed[i]).Grade);
                    ItemCotent.transform.GetChild(i).GetComponent<SelectNumScript>().BtnNum = i;
                    if (2000 <= ItemCotent.transform.GetChild(i).GetComponent<SelectNumScript>().ItemIndex)
                    {
                        //ItemCotent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + myChar.ActiveitemDataMgr.GetTemplate(myChar.ActiveitemAll[]));
                    }
                    else if (1000 <= ItemCotent.transform.GetChild(i).GetComponent<SelectNumScript>().ItemIndex && ItemCotent.transform.GetChild(i).GetComponent<SelectNumScript>().ItemIndex < 2000)
                    {
                        ItemCotent.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + 0);
                        //ItemCotent.transform.GetChild(i).GetComponent<Button>().spriteState.pressedSprite = Resources.Load<Sprite>("03_UI/02_EquipmentWindow_Press/" + 0);
                    }

                }
                else if (i >= myChar.PlayerEquipment.Count)
                {
                    ItemCotent.transform.GetChild(i).gameObject.SetActive(false);
                }

            }
            GameObject ItemStats_Panel = PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
            Text Name_Text = PreferencesWindow.transform.Find("ItemInfo_Window").Find("Name_Text").GetComponent<Text>();
            Text Grade_Text = PreferencesWindow.transform.Find("ItemInfo_Window").Find("Grade_Text").GetComponent<Text>();
            Text Lv_Text = PreferencesWindow.transform.Find("ItemInfo_Window").Find("Lv_Text").GetComponent<Text>();

            if (myChar.PlayerEquipment.Count > 0)
            {
                if (myChar.PlayerEquipment[SelectItemNum] >= 2000)      //사용아이템 정보
                {
                    EquipmentInfoText(myChar.ActiveitemAll[myChar.PlayerEquipment[SelectItemNum] - 2000]);
                    PreferencesDontItemState();
                    //ItemStats_Panel.SetActive(false);
                    //액티브아이템 31번부터 시작 2000-1969
                    switch (myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Grade)
                    {
                        case 0:
                            Name_Text.text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Grade_Text.text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(195).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Lv_Text.text = "<color=#FFFFFF>Lv." + (myChar.ActiveitemAll[myChar.PlayerEquipment[SelectItemNum] - 2000] + 1) + "</color>";
                            break;
                        case 1:
                            Name_Text.text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Grade_Text.text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(196).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Lv_Text.text = "<color=#FDE43C>Lv." + (myChar.ActiveitemAll[myChar.PlayerEquipment[SelectItemNum] - 2000] + 1) + "</color>";
                            break;
                        case 2:
                            Name_Text.text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Grade_Text.text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(197).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Lv_Text.text = "<color=#FF85D0>Lv." + (myChar.ActiveitemAll[myChar.PlayerEquipment[SelectItemNum] - 2000] + 1) + "</color>";
                            break;
                    }
                    switch (myChar.ActiveitemAll[myChar.PlayerEquipment[SelectItemNum] - 2000])
                    {
                        case 0:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv1);
                            break;
                        case 1:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv2);
                            break;
                        case 2:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv3);
                            break;
                        case 3:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv4);
                            break;
                        case 4:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv5);
                            break;
                        case 5:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv6);
                            break;
                        case 6:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv7);
                            break;
                        case 7:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv8);
                            break;
                        case 8:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv9);
                            break;
                        case 9:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv10);
                            break;
                        case 10:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv11);
                            break;
                    }

                    if (myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).StatIndex != 0)
                    {
                        string ItemInfo = myChar.TextDataMgr.GetTemplate(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).InfoIndex).Content[myChar.LanguageNum].Replace("\\n", "\n");

                        PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text =
                            ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
                    }
                    else if (myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).StatIndex == 0)
                    {
                        PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text = "-\n";
                    }
                }
                else if (myChar.PlayerEquipment[SelectItemNum] >= 1000 && myChar.PlayerEquipment[SelectItemNum] < 2000)     //속성석 정보
                {
                    EquipmentInfoText(myChar.ElementStoneAll[myChar.PlayerEquipment[SelectItemNum] - 1000]);
                    PreferencesDontItemState();
                    //ItemStats_Panel.SetActive(false);
                    switch (myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Grade)
                    {
                        case 0:                            
                            Name_Text.text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Grade_Text.text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(195).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Lv_Text.text = "<color=#FFFFFF>Lv." + (myChar.ElementStoneAll[myChar.PlayerEquipment[SelectItemNum] - 1000] + 1) + "</color>";
                            break;
                        case 1:
                            Name_Text.text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Grade_Text.text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(196).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Lv_Text.text = "<color=#FDE43C>Lv." + (myChar.ElementStoneAll[myChar.PlayerEquipment[SelectItemNum] - 1000] + 1) + "</color>";
                            break;
                        case 2:
                            Name_Text.text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Grade_Text.text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(197).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Lv_Text.text = "<color=#FF85D0>Lv." + (myChar.ElementStoneAll[myChar.PlayerEquipment[SelectItemNum] - 1000] + 1) + "</color>";
                            break;
                    }

                    //아이템 설명 부분
                    switch (myChar.ElementStoneAll[myChar.PlayerEquipment[SelectItemNum] - 1000])
                    {
                        case 0:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv1);
                            break;
                        case 1:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv2);
                            break;
                        case 2:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv3);
                            break;
                        case 3:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv4);
                            break;
                        case 4:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv5);
                            break;
                        case 5:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv6);
                            break;
                        case 6:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv7);
                            break;
                        case 7:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv8);
                            break;
                        case 8:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv9);
                            break;
                        case 9:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv10);
                            break;
                        case 10:
                            EquipmentsInfoText(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv11);
                            break;
                    }
                    if (myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).StatIndex != 0)
                    {
                        string ItemInfo = myChar.TextDataMgr.GetTemplate(myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).InfoIndex).Content[myChar.LanguageNum].Replace("\\n", "\n");

                        PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text =
                            ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
                    }
                    else if (myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).StatIndex == 0)
                    {
                        PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text = "-\n";
                    }
                }
                if (myChar.PlayerEquipment[SelectItemNum] < 1000)   //장비 정보
                {
                    //EquipmentsInfoText();
                    PreferencesItemState(myChar.PlayerEquipment[SelectItemNum]);
                    //ItemStats_Panel.SetActive(true);
                    switch (myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Grade)
                    {
                        case 0:
                            Name_Text.text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum] + 97).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Grade_Text.text = "<color=#FFFFFF>" + myChar.TextDataMgr.GetTemplate(195).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Lv_Text.text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[myChar.PlayerEquipment[SelectItemNum]] + 1) + "</color>";
                            break;
                        case 1:
                            Name_Text.text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum] + 97).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Grade_Text.text = "<color=#FDE43C>" + myChar.TextDataMgr.GetTemplate(196).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Lv_Text.text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[myChar.PlayerEquipment[SelectItemNum]] + 1) + "</color>";
                            break;
                        case 2:
                            Name_Text.text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum] + 97).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Grade_Text.text = "<color=#FF85D0>" + myChar.TextDataMgr.GetTemplate(197).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                            Lv_Text.text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[myChar.PlayerEquipment[SelectItemNum]] + 1) + "</color>";
                            break;
                    }
                    Text Info_Text = PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();

                    switch (SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).StatIndex)
                    {
                        case 0:
                            AddText = null;
                            break;
                        case 1:     //공격력
                            if (myChar.PlayerEquipment[SelectItemNum] == 1002 || myChar.PlayerEquipment[SelectItemNum] == 5)
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString() + "%";
                            }
                            else
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString() + "%";
                            }
                            break;
                        case 2:     //사정거리
                            AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString() + "%";
                            break;
                        case 3:     //체력
                            AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString() + "%";
                            break;
                        case 4:     //속도
                            if (myChar.PlayerEquipment[SelectItemNum] < 1000)
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(4).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString() + "%";
                            }
                            else
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString() + "%";
                            }
                            break;
                        case 5:     //부활 횟수
                            AddText = myChar.TextDataMgr.GetTemplate(5).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString();
                            break;
                        case 6:     //하트 획득
                            if (myChar.PlayerEquipment[SelectItemNum] == 38)
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n") + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString() + "%";
                            }
                            else
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString();
                            }                            
                            break;
                        case 7:     //쉴드 획득
                            AddText = myChar.TextDataMgr.GetTemplate(7).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString();
                            break;
                        case 8:     //높이
                            AddText = myChar.TextDataMgr.GetTemplate(8).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString();
                            break;
                        case 9:     //점프 횟수
                            AddText = myChar.TextDataMgr.GetTemplate(9).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString();
                            break;
                        case 10:    //튕김
                            AddText = myChar.TextDataMgr.GetTemplate(10).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString();
                            break;
                        case 11:    //확률
                            if (myChar.PlayerEquipment[SelectItemNum] == 1003)
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString() + "%";
                            }
                            else
                            {
                                AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString() + "%";
                            }                            
                            break;
                        case 12:    //개수
                            AddText = myChar.TextDataMgr.GetTemplate(12).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString();
                            break;
                        case 13:    //지속 시간 (초)
                            AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).Value.ToString();
                            break;
                    }

                    if (myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1 != 0)
                    {
                        string ItemInfo = myChar.TextDataMgr.GetTemplate((SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).InfoIndex)).Content[myChar.LanguageNum].Replace("\\n", "\n");

                        PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text =
                            ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
                    }
                    else if (myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1 == 0)
                    {
                        PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).Find("Scroll View").GetChild(0).GetChild(0).Find("Info_Text").GetComponent<Text>().text = "-\n";
                    }
                }
            }
            else if (myChar.PlayerEquipment.Count == 0)
            {
                ItemStats_Panel.SetActive(false);
                Name_Text.text = "";
                Grade_Text.text = "";
            }
        }
    }
    //myChar.ActiveitemAll[myChar.PlayerEquipment[SelectItemNum] - 2000]

    //private void PreferencesGrade(int Equipment)
    //{
    //    //아이템 정보창 업그레이드 수치체크
    //    GameObject Grade = PreferencesWindow.transform.Find("ItemInfo_Window").Find("Grade").gameObject;

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
    //액티브 및 원소 소개텍스트
    private void EquipmentInfoText(int ActiveNum)
    {
        Text Info_Text = PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();

        if (ActiveNum == 0)
        {
            switch (myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).StatIndex)
            {
                case 0:
                    AddText = null;
                    break;
                case 1:
                    AddText = "공격력 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv0.ToString() + "%";
                    break;
                case 6:
                    AddText = "하트 획득 " + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv0.ToString() + "%";
                    break;
                case 7:
                    AddText = "쉴드 획득 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv0.ToString();
                    break;
                case 11:
                    AddText = "확률 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv0.ToString() + "%";
                    break;
                case 13:
                    AddText = "지속 시간(초) +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv0.ToString();
                    break;
            }
        }
        else if (ActiveNum == 1)
        {
            switch (myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).StatIndex)
            {
                case 0:
                    AddText = null;
                    break;
                case 1:
                    AddText = "공격력 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv1.ToString() + "%";
                    break;
                case 6:
                    AddText = "하트 획득 " + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv1.ToString() + "%";
                    break;
                case 7:
                    AddText = "쉴드 획득 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv1.ToString();
                    break;
                case 11:
                    AddText = "확률 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv1.ToString() + "%";
                    break;
                case 13:
                    AddText = "지속 시간(초) +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv1.ToString();
                    break;
            }
        }
        else if (ActiveNum == 2)
        {
            switch (myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).StatIndex)
            {
                case 0:
                    AddText = null;
                    break;
                case 1:
                    AddText = "공격력 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv2.ToString() + "%";
                    break;
                case 6:
                    AddText = "하트 획득 " + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv2.ToString() + "%";
                    break;
                case 7:
                    AddText = "쉴드 획득 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv2.ToString();
                    break;
                case 11:
                    AddText = "확률 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv1.ToString() + "%";
                    break;
                case 13:
                    AddText = "지속 시간(초) +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv2.ToString();
                    break;
            }
        }
        else if (ActiveNum == 3)
        {
            switch (myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).StatIndex)
            {
                case 0:
                    AddText = null;
                    break;
                case 1:
                    AddText = "공격력 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv3.ToString() + "%";
                    break;
                case 6:
                    AddText = "하트 획득 " + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv3.ToString() + "%";
                    break;
                case 7:
                    AddText = "쉴드 획득 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv3.ToString();
                    break;
                case 11:
                    AddText = "확률 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv1.ToString() + "%";
                    break;
                case 13:
                    AddText = "지속 시간(초) +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv3.ToString();
                    break;
            }
        }
        else if (ActiveNum == 4)
        {
            switch (myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).StatIndex)
            {
                case 0:
                    AddText = null;
                    break;
                case 1:
                    AddText = "공격력 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv4.ToString() + "%";
                    break;
                case 6:
                    AddText = "하트 획득 " + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv4.ToString() + "%";
                    break;
                case 7:
                    AddText = "쉴드 획득 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv4.ToString();
                    break;
                case 11:
                    AddText = "확률 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv1.ToString() + "%";
                    break;
                case 13:
                    AddText = "지속 시간(초) +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv4.ToString();
                    break;
            }
        }
        else if (ActiveNum == 5)
        {
            switch (myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).StatIndex)
            {
                case 0:
                    AddText = null;
                    break;
                case 1:
                    AddText = "공격력 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv5.ToString() + "%";
                    break;
                case 6:
                    AddText = "하트 획득 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv5.ToString();
                    break;
                case 7:
                    AddText = "쉴드 획득 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv5.ToString();
                    break;
                case 11:
                    AddText = "확률 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv1.ToString() + "%";
                    break;
                case 13:
                    AddText = "지속 시간(초) +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv5.ToString();
                    break;
            }
        }
        else if (ActiveNum == 6)
        {
            switch (myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).StatIndex)
            {
                case 0:
                    AddText = null;
                    break;
                case 1:
                    AddText = "공격력 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv6.ToString() + "%";
                    break;
                case 6:
                    AddText = "하트 획득 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv6.ToString();
                    break;
                case 7:
                    AddText = "쉴드 획득 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv6.ToString();
                    break;
                case 11:
                    AddText = "확률 +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv1.ToString() + "%";
                    break;
                case 13:
                    AddText = "지속 시간(초) +" + myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Lv6.ToString();
                    break;
            }
        }
        string ItemInfo = myChar.ActiveitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).KorText;

        Info_Text.text = "\n" + ItemInfo.Replace("\\n", "\n") + "\n" + "<color=#00FF00>" + AddText + "</color>";
    }
    //장비 소개텍스트 
    private void EquipmentsInfoText(float ItemLvInfo)
    {
        Text Info_Text = PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();

        switch (myChar.AntiqueitemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).StatIndex)
        {
            case 0:
                AddText = null;
                break;
            case 1:     //공격력
                if (myChar.PlayerEquipment[SelectItemNum] == 1002 || myChar.PlayerEquipment[SelectItemNum] == 5)
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
                if (myChar.PlayerEquipment[SelectItemNum] < 1000)
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
                if (myChar.PlayerEquipment[SelectItemNum] == 38)
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
                if (myChar.PlayerEquipment[SelectItemNum] == 1003)
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
        ////아이템 설명창은 추가 수정필요
        //if (myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1 != 0)
        //{
        //    string ItemInfo = myChar.TextDataMgr.GetTemplate((SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1).InfoIndex)).Content[myChar.LanguageNum].Replace("\\n", "\n");
        //    Info_Text.text = "\n" + ItemInfo.Replace("\\n", "\n") + "\n" + "<color=#00FF00>" + AddText + "</color>";
        //}
        //else if (myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1 == 0)
        //{
        //    Info_Text.text = "-";
        //}
        //SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(myChar.PlayerEquipment[SelectItemNum]).Option1);
    }
    private void PreferencesItemState(int EquipmentCnt)
    {
        GameObject StatePanel = PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        switch (myChar.EquipmentAll[EquipmentCnt])
        {
            case 0:
                Atk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk0;
                ASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD0;
                Range = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range0;
                HP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP0;
                break;
            case 1:
                Atk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk1;
                ASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD1;
                Range = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range1;
                HP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP1;
                break;
            case 2:
                Atk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk2;
                ASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD2;
                Range = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range2;
                HP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP2;
                break;
            case 3:
                Atk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk3;
                ASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD3;
                Range = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range3;
                HP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP3;
                break;
            case 4:
                Atk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk4;
                ASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD4;
                Range = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range4;
                HP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP4;
                break;
            case 5:
                Atk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk5;
                ASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD5;
                Range = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range5;
                HP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP5;
                break;
            case 6:
                Atk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk6;
                ASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD6;
                Range = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range6;
                HP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP6;
                break;
            case 7:
                Atk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk7;
                ASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD7;
                Range = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range7;
                HP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP7;
                break;
            case 8:
                Atk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk8;
                ASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD8;
                Range = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range8;
                HP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP8;
                break;
            case 9:
                Atk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk9;
                ASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD9;
                Range = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range9;
                HP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP9;
                break;
            case 10:
                Atk = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Atk10;
                ASPD = myChar.itemDataMgr.GetTemplate(EquipmentCnt).ASPD10;
                Range = myChar.itemDataMgr.GetTemplate(EquipmentCnt).Range10;
                HP = myChar.itemDataMgr.GetTemplate(EquipmentCnt).HP10;
                break;
        }

        StatePanel.transform.Find("Atk").GetComponentInChildren<Text>().text = Atk.ToString();
        StatePanel.transform.Find("ASPD").GetComponentInChildren<Text>().text = ASPD.ToString();
        StatePanel.transform.Find("Range").GetComponentInChildren<Text>().text = Range.ToString();
        StatePanel.transform.Find("HP").GetComponentInChildren<Text>().text = HP.ToString();

    }
    private void PreferencesDontItemState()
    {
        GameObject StatePanel = PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        Atk = 0;
        ASPD = 0;
        Range = 0;
        HP = 0;

        StatePanel.transform.Find("Atk").GetComponentInChildren<Text>().text = "";
        StatePanel.transform.Find("ASPD").GetComponentInChildren<Text>().text = "";
        StatePanel.transform.Find("Range").GetComponentInChildren<Text>().text = "";
        StatePanel.transform.Find("HP").GetComponentInChildren<Text>().text = "";

        //for (int i = 0; i < 10; i++)
        //{
        //    if (i < Atk)
        //    {
        //        AtkGage[i].transform.GetChild(0).gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        AtkGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    }

        //    if (i < ASPD)
        //    {
        //        ASPDGage[i].transform.GetChild(0).gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        ASPDGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    }
        //    if (i < Range)
        //    {
        //        RangeGage[i].transform.GetChild(0).gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        RangeGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    }
        //    if (i < HP)
        //    {
        //        HPGage[i].transform.GetChild(0).gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        HPGage[i].transform.GetChild(0).gameObject.SetActive(false);
        //    }
        //}
    }
    private void ItemUIManager()
    {
        if (myChar.PlayerEquipment.Count > 0)
        {
            if (myChar.PlayerEquipment[SelectItemNum] < 1000)
            {

            }
            else if(myChar.PlayerEquipment[SelectItemNum] >= 1000 && myChar.PlayerEquipment[SelectItemNum] < 2000)
            {

            }
            else if (myChar.PlayerEquipment[SelectItemNum] >= 2000)
            {

            }

        }
        else
        {
            ItemGage(0, 0, 0, 0);
        }
    }
    private void ItemGage(int HeroAtk, int HeroASPD, int HeroRange, int HeroHP)
    {
        GameObject PreferencesGage = PreferencesWindow.transform.Find("ItemInfo_Window").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;

        PreferencesGage.transform.Find("Atk").Find("Lv_Text").GetComponent<Text>().text = " LV." + HeroAtk.ToString("F0");
        PreferencesGage.transform.Find("ASPD").Find("Lv_Text").GetComponent<Text>().text = " LV." + HeroAtk.ToString("F0");
        PreferencesGage.transform.Find("Range").Find("Lv_Text").GetComponent<Text>().text = " LV." + HeroAtk.ToString("F0");
        PreferencesGage.transform.Find("HP").Find("Lv_Text").GetComponent<Text>().text = " LV." + HeroAtk.ToString("F0");

        myChar.CurrentDamage = HeroAtk;
        myChar.ASPD = HeroASPD;
        myChar.Range = HeroRange;
        myChar.maxHp = HeroHP;
        for (int i = 0; i < 10; i++)
        {
            if (i < HeroAtk)
            {
                AtkGage[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                AtkGage[i].transform.GetChild(0).gameObject.SetActive(false);
            }

            if (i < HeroASPD)
            {
                ASPDGage[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                ASPDGage[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            if (i < HeroRange)
            {
                RangeGage[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                RangeGage[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            if (i < HeroHP)
            {
                HPGage[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                HPGage[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }

    }
    private void GiftBoxUI_TextCheck()
    {
        if (GiftBox_InfoUI.activeSelf)
        {
            GiftBox_InfoUI.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.TextDataMgr.GetTemplate(258).Content[myChar.LanguageNum].Replace("\\n", "\n");
        }       
    }
    private void ItemInfoCheck()
    {
        if (myChar.EquipmentUsed.Count != 0)
        {
            if (myChar.EquipmentUsed.Count != iconCntCheck)
            {
                iconCntCheck = 0;
                for (int i = 0; i < ItemInfoPanel.transform.GetChild(0).childCount; i++)
                {
                    Destroy(ItemInfoPanel.transform.GetChild(0).GetChild(i).gameObject);
                }
                //ItemInfoPanel.transform.GetChild(0).
                for (int i = 0; i < myChar.EquipmentUsed.Count; i++)
                {
                    GameObject icon = Instantiate(ItemInfo_icon, ItemInfoPanel.transform.parent.position, Quaternion.identity);
                    icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.EquipmentUsed[i].ToString());
                    icon.transform.parent = ItemInfoPanel.transform.GetChild(0).transform;
                    icon.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    iconCntCheck++;
                }
            }            
        }
        else if (myChar.EquipmentUsed.Count == 0)
        {
            iconCntCheck = 0;
            for (int i = 0; i < ItemInfoPanel.transform.GetChild(0).childCount; i++)
            {
                Destroy(ItemInfoPanel.transform.GetChild(0).GetChild(i).gameObject);
            }
        }
    }
    private void ResetObject()
    {
        myChar.GainGem = 0;
        myChar.GainSoulSpark = 0;
        myChar.GainHeroHeart = 0;
        myChar.PlayTime = 0;
        myChar.Gain_Multiple = 1;
        myChar.Chapter1_Cnt = 0;
        myChar.Chapter2_Cnt = 0;
        myChar.Chapter3_Cnt = 0;
        myChar.ChapterCntCheck = false;
        for (int i = 0; i < myChar.ElementStone.Length; i++)
        {
            myChar.ElementStone[i] = 0;
        }
        myChar.EquipmentUsed.Clear();
        myChar.EquipmentUsedTrueCheck.Clear();
    }

    public void BtnSound(int Num)
    {
        SoundManager.Instance.PlaySfx(Num);
    }
    public void SFXSound(int Num)
    {
        SoundManager.Instance.PlaySfx(Num);
    }
    public void ElementalStonSound()
    {
        SoundManager.Instance.PlaySfx(22);
    }
    private void LodingInvoke()
    {
        LoadingPanel.SetActive(true);
    }
    public void ResetSkill()
    {
        for (int i = 0; i < PlayerSkill.Count; i++)
        {
            PlayerSkill[i] = 0;
        }
    }

    public void GiftBoxOpenCheck()
    {
        //기프트박스
        if (AdMerchin.GetComponent<BossGiftBoxScript>())
        {
            if (myChar.Key >= 1)
            {
                AdMerchin.GetComponent<BossGiftBoxScript>().OpenBox();
                AdMerchin.GetComponent<BossGiftBoxScript>().WindowCheck = true;
                if (!myChar.Tutorial)
                {
                    StartCoroutine(RoomManager.Instance.GiftBoxOpen_DoorInvisible());
                }                
                myChar.Key--;

                FirebaseManager.firebaseManager.IngameTreasureChestOpen("TreasureKey",0);
                myChar.SaveKey();
            }
            else
            {
                if (myChar.Gem >= 10)
                {
                    AdMerchin.GetComponent<BossGiftBoxScript>().OpenBox();
                    AdMerchin.GetComponent<BossGiftBoxScript>().WindowCheck = true;
                    if (!myChar.Tutorial)
                    {
                        StartCoroutine(RoomManager.Instance.GiftBoxOpen_DoorInvisible());
                    }
                    myChar.Gem -= 10;
                    FirebaseManager.firebaseManager.IngameTreasureChestOpen("Jewel", 10);

                    myChar.SaveGem();
                }
            }
        }
    }

    public void ADUse()
    {
        //광고패널
        if (AdMerchin.GetComponent<ADMarchentScript>())
        {
            AdMerchin.GetComponent<ADMarchentScript>().ADUse();
        }

        
    }
    //부활
    public void ResurrectionBtn(int num)
    {
        if (num == 0)
        {
            FirebaseManager.firebaseManager.IngameRetryPurchase("AD", 0);
            myChar.Resurrection = true;
            InfoWindow.SetActive(false);
            Fail_ResultPanel.SetActive(false);
            myChar.currentHp = myChar.maxHp;
            if (myChar.Wizard)
            {
                if (SelectCharacter.GetComponent<SpriteRenderer>().enabled == false)
                {
                    SelectCharacter.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
        else if (num == 1)
        {
            if (myChar.Gem >= ((int)Mathf.Pow(myChar.ResurectionCost, myChar.NumberOfResurections) * 10))
            {
                myChar.Gem -= (int)Mathf.Pow(myChar.ResurectionCost,myChar.NumberOfResurections) * 10;
                FirebaseManager.firebaseManager.IngameRetryPurchase("Jewel", (int)Mathf.Pow(myChar.ResurectionCost, myChar.NumberOfResurections) * 10);
                myChar.Resurrection = true;
                InfoWindow.SetActive(false);

                myChar.SaveGem();

                myChar.SaveGem();
                myChar.currentHp = myChar.maxHp;
                if (myChar.Wizard)
                {
                    if (SelectCharacter.GetComponent<SpriteRenderer>().enabled == false)
                    {
                        SelectCharacter.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }            
        }
        myChar.NumberOfResurections++;
    }

    IEnumerator SlowActive(float Time)              //슬로우아이템
    {//ActiveItem = 1
        myChar.SlowSpeed = 0.5f;
        myChar.Slow = true;
        yield return new WaitForSeconds(Time);
        myChar.Slow = false;
        myChar.SlowSpeed = 1f;
    }

    IEnumerator ASPD_Potion(float Time)             //공속포션
    {
        PlayerTargeting.Instance.ASPDActive = 2;
        myChar.ASPDPotion = true;
        myChar.EffectOnCheck = true;

        myChar.Reflection = false;
        myChar.PowerPotion = false;
        myChar.InvinciblePotion = false;
        yield return new WaitForSeconds(Time);
        PlayerTargeting.Instance.ASPDActive = 1;
        myChar.EffectOnCheck = false;
        myChar.ASPDPotion = false;
    }
    IEnumerator Power_Potion(float Time)            //파워포션
    {
        myChar.PowerPotion = true;
        myChar.EffectOnCheck = true;

        myChar.Reflection = false;
        myChar.ASPDPotion = false;
        myChar.InvinciblePotion = false;
        myChar.CurrentDamage *= 2;
        yield return new WaitForSeconds(Time);
        myChar.CurrentDamage /= 2;

        myChar.PowerPotion = false;
        myChar.EffectOnCheck = false;

    }
    IEnumerator Invincibility_Potion(float Time)    //무적 포션
    {
        myChar.InvinciblePotion = true;
        myChar.EffectOnCheck = true;
        myChar.Reflection = false;
        myChar.ASPDPotion = false;
        myChar.PowerPotion = false;
        PlayerController.Instance.HitAttack = false;
        yield return new WaitForSeconds(Time);
        myChar.InvinciblePotion = false;
        myChar.EffectOnCheck = false;
        PlayerController.Instance.HitAttack = true;
    }
    IEnumerator Reflection_Potion(float Time)    //반사 포션
    {
        myChar.Reflection = true;
        myChar.EffectOnCheck = true;
        myChar.ASPDPotion = false;
        myChar.PowerPotion = false;
        myChar.InvinciblePotion = false;
        yield return new WaitForSeconds(Time);
        myChar.EffectOnCheck = false;
        myChar.Reflection = false;
    }
    //IEnumerator StageGo(float Time)
    //{
    //    yield return new WaitForSeconds(Time);
    //    Destroy(RoomManager.Instance.Tutorial_Parent.transform.GetChild(0).gameObject);
    //    myChar.Chapter = 0;
    //    myChar.Stage = 0;
    //    myChar.SelectedStage = 0;
    //    myChar.Resurrection = false;
    //    myChar.Resetitem();
    //    ResetSkill();
    //    myChar.Finished = false;
    //    myChar.SelectHero = 0;
    //    myChar.SeasonalShield = false;

    //    RoomManager.Instance.NextStage();
    //    if (myChar.Tutorial)
    //    {
    //        myChar.Tutorial = false;
    //    }
    //}
    IEnumerator ClearResultPanel(float Time)
    {
        yield return new WaitForSeconds(Time);
        Clear_ResultPanel.SetActive(true);
    }
    IEnumerator EndPanel(float Time)
    {
        yield return new WaitForSeconds(Time);
        EndingPanel.SetActive(true);
    }
    IEnumerator LobbyGo(float Time)
    {
        if (myChar.ThroneBossKill)
        {
            if (myChar.Tutorial)
            {
                myChar.EnthroneHeroNum = myChar.SelectHero;
                myChar.ThroneHeroLv = myChar.HeroLv[myChar.SelectHero];
                myChar.ThroneCostumSkin = myChar.EquipmentCostume[myChar.SelectHero];
                myChar.ThroneWeaponSkin = myChar.EquipmentWeapon[myChar.SelectHero];
                switch (myChar.SelectHero)
                {
                    case 0:
                        myChar.ThroneDamage = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KAtk;
                        myChar.ThronemaxHp = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KHP;
                        myChar.ThroneRange = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KRange;
                        myChar.ThroneASPD = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KASPD;
                        break;
                    case 1:
                        myChar.ThroneDamage = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WAtk;
                        myChar.ThronemaxHp = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WHP;
                        myChar.ThroneRange = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KRange;
                        myChar.ThroneASPD = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KASPD;
                        break;
                    case 2:
                        myChar.ThroneDamage = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).AAtk;
                        myChar.ThronemaxHp = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).AHP;
                        myChar.ThroneRange = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KRange;
                        myChar.ThroneASPD = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KASPD;
                        break;
                    case 3:
                        myChar.ThroneDamage = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WiAtk;
                        myChar.ThronemaxHp = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WiHP;
                        myChar.ThroneRange = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KRange;
                        myChar.ThroneASPD = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KASPD;
                        break;
                    case 4:
                        myChar.ThroneDamage = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).NAtk;
                        myChar.ThronemaxHp = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).NHP;
                        myChar.ThroneRange = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KRange;
                        myChar.ThroneASPD = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KASPD;
                        break;
                }
                bool Itemcheck = false;
                if (!Itemcheck)
                {
                    myChar.ThroneItemList.Clear();
                    myChar.ThroneItemLvList.Clear();
                    for (int i = 0; i < myChar.EquipmentUsed.Count; i++)
                    {
                        myChar.ThroneItemList.Add(myChar.EquipmentUsed[i]);
                    }
                    for (int i = 0; i < myChar.ThroneItemList.Count; i++)
                    {
                        myChar.ThroneItemLvList.Add(myChar.EquipmentAll[myChar.ThroneItemList[i]]);
                    }
                    Itemcheck = true;
                }
            }
            else
            {
                myChar.EnthroneHeroNum = myChar.SelectHero;
                myChar.ThroneHeroLv = myChar.HeroLv[myChar.SelectHero];
                myChar.ThroneCostumSkin = myChar.EquipmentCostume[myChar.SelectHero];
                myChar.ThroneWeaponSkin = myChar.EquipmentWeapon[myChar.SelectHero];
                switch (myChar.SelectHero)
                {
                    case 0:
                        myChar.ThroneDamage = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KAtk;
                        myChar.ThronemaxHp = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KHP;
                        myChar.ThroneRange = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KRange;
                        myChar.ThroneASPD = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KASPD;
                        break;
                    case 1:
                        myChar.ThroneDamage = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WAtk;
                        myChar.ThronemaxHp = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WHP;
                        myChar.ThroneRange = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KRange;
                        myChar.ThroneASPD = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KASPD;
                        break;
                    case 2:
                        myChar.ThroneDamage = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).AAtk;
                        myChar.ThronemaxHp = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).AHP;
                        myChar.ThroneRange = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KRange;
                        myChar.ThroneASPD = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KASPD;
                        break;
                    case 3:
                        myChar.ThroneDamage = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WiAtk;
                        myChar.ThronemaxHp = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WiHP;
                        myChar.ThroneRange = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KRange;
                        myChar.ThroneASPD = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KASPD;
                        break;
                    case 4:
                        myChar.ThroneDamage = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).NAtk;
                        myChar.ThronemaxHp = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).NHP;
                        myChar.ThroneRange = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KRange;
                        myChar.ThroneASPD = myChar.HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KASPD;
                        break;
                }
                bool Itemcheck = false;
                if (!Itemcheck)
                {
                    myChar.ThroneItemList.Clear();
                    myChar.ThroneItemLvList.Clear();
                    for (int i = 0; i < myChar.EquipmentUsed.Count; i++)
                    {
                        myChar.ThroneItemList.Add(myChar.EquipmentUsed[i]);
                    }
                    for (int i = 0; i < myChar.ThroneItemList.Count; i++)
                    {
                        myChar.ThroneItemLvList.Add(myChar.EquipmentAll[myChar.ThroneItemList[i]]);
                    }
                    Itemcheck = true;
                }
            }
            myChar.SaveEnthroneHeroNum();
            myChar.SaveThroneHeroLv();
            myChar.SaveThroneCostumSkin();
            myChar.SaveThroneWeaponSkin();
            myChar.SaveThroneDamage();
            myChar.SaveThronemaxHp();
            myChar.SaveThroneRange();
            myChar.SaveThroneASPD();
            myChar.SaveThroneItemList();
            myChar.SaveThroneItemLvList();
        }
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene("LobbiScene");
        if (myChar.MultipleCheck)
        {
            myChar.Gem += myChar.GainGem;
            myChar.SoulSpark += myChar.GainSoulSpark;
            myChar.HeroHeart += myChar.GainHeroHeart;
            myChar.SaveGem();
            myChar.SaveSoulSpark();
            myChar.SaveHeroHeart();
        }

        if (!myChar.Tutorial)
        {
            if (Clear_ResultPanel.activeSelf)
            {
                if (myChar.StageClearCheck.Length > myChar.Chapter)
                {
                    myChar.StageClearCheck[myChar.Chapter + 1] = true;
                    myChar.SaveStageClearCheck();
                }
            }            
        }


        //myChar.Chapter = 0;
        myChar.Stage = 0;
        myChar.SelectedStage = 0;
        myChar.Resurrection = false;
        myChar.NumberOfResurections = 1;
        myChar.Resetitem();
        myChar.Finished = false;
        myChar.CrownGetBgm = false;
        myChar.SeasonalShield = false;
        BtnOnCheck = false;
        myChar.PlayerEquipment.Clear();
        myChar.EnthroneEquipment.Clear();
        myChar.MHpMultiIndex = 0;
             

        //포션류 체크
        myChar.Reflection = false;
        myChar.EffectOnCheck = false;
        myChar.ASPDPotion = false;
        myChar.PowerPotion = false;
        myChar.InvinciblePotion = false;

        ResetObject();
        ResetSkill();

        //if (myChar.EnthroneHeroNum != 0)
        //{
        //    myChar.SelectHero = 0;
        //}
        //else
        //{
        //    myChar.SelectHero = 1;
        //}

        if (myChar.Tutorial)
        {
            myChar.Tutorial = false;

            myChar.SaveTutorial();
        }
        
    }
    public void MoveLobby()
    {
        StartCoroutine(LobbyGo(0f));
    }
    public void MoveLobbyTime(float Time)
    {
        StartCoroutine(LobbyGo(Time));
    }
    public void OpenEndPanel(float Time)
    {
        StartCoroutine(EndPanel(Time));
    }
    public void WinPanelOpen(float Time)
    {
        StartCoroutine(ClearResultPanel(Time));
    }
    //public void LobbyGo()
    //{
    //    StartCoroutine(Lobby(0));
    //}
    void OnLoadHeroTemplateMgr()
    {
        string UnitTextResource = "01_Excel/Unit_Info";
        UnitDataMgr.OnDataLoad(UnitTextResource);
        string DropTextResource = "01_Excel/DropTable";
        DropDataMgr.OnDataLoad(DropTextResource);
        string DropMultiTextResource = "01_Excel/DropMulti";
        DropMultiDataMgr.OnDataLoad(DropMultiTextResource);
        string SkillTextResource = "01_Excel/SkillInfo";
        SkillDataMgr.OnDataLoad(SkillTextResource);
        
    }

    public void ShieldEffect_On()
    {
        GameObject Shield_Effect = Instantiate(ShieldEffect, SelectCharacter.transform.position, Quaternion.identity);
        Shield_Effect.transform.parent = PlayerObjectManager.transform;
        Shield_Effect.transform.rotation = Quaternion.Euler(-90, 0, 0);
        Destroy(Shield_Effect, 5f);
    }
    public void HealingEffect_On()
    {
        GameObject Healing_Effect = Instantiate(HealEffect, SelectCharacter.transform.position, Quaternion.identity);
        Healing_Effect.transform.parent = PlayerObjectManager.transform;
        Destroy(Healing_Effect, 5f);
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
    public void DoorOpen()
    {
        StartCoroutine(OpenDoor());
    }
    private void SlowSoundUp()
    {
        if (SoundManager.Instance.audioSource.volume != 0.5f)
        {
            SoundManager.Instance.audioSource.volume += 0.1f;
        }
    }
    public void TimeScaleBack()
    {
        Time.timeScale = 1f;
    }

    //광고 보고 증가되는 모션 보여주는곳
    public void ClearPlusEffect()
    {
        Debug.Log("성공");
        StartCoroutine(Count(Clear_ResultPanel.transform.Find("Soul_Spark").GetComponentInChildren<Text>(), true, myChar.GainSoulSpark * myChar.Gain_Multiple, myChar.GainSoulSpark, SoulSpark_UpText, Clear_ResultPanel.transform.Find("UpText").Find("SoulSpark_UpText").gameObject));
        StartCoroutine(Count(Clear_ResultPanel.transform.Find("Hero_Heart").GetComponentInChildren<Text>(), true, myChar.GainHeroHeart * myChar.Gain_Multiple, myChar.GainHeroHeart, HeroHeart_UpText, Clear_ResultPanel.transform.Find("UpText").Find("Hero_UpText").gameObject));
        StartCoroutine(Count(Clear_ResultPanel.transform.Find("Gem").GetComponentInChildren<Text>(), true, myChar.GainGem * myChar.Gain_Multiple, myChar.GainGem, Gem_UpText, Clear_ResultPanel.transform.Find("UpText").Find("Gem_UpText").gameObject));
    }
    public void FailPlusEffect()
    {
        Debug.Log("실패");
        StartCoroutine(Count(Fail_ResultPanel.transform.Find("Soul_Spark").GetComponentInChildren<Text>(), true, myChar.GainSoulSpark * myChar.Gain_Multiple, myChar.GainSoulSpark, SoulSpark_UpText, Fail_ResultPanel.transform.Find("UpText").Find("SoulSpark_UpText").gameObject));
        StartCoroutine(Count(Fail_ResultPanel.transform.Find("Hero_Heart").GetComponentInChildren<Text>(), true, myChar.GainHeroHeart * myChar.Gain_Multiple, myChar.GainHeroHeart, HeroHeart_UpText, Fail_ResultPanel.transform.Find("UpText").Find("Hero_UpText").gameObject));
        StartCoroutine(Count(Fail_ResultPanel.transform.Find("Gem").GetComponentInChildren<Text>(), true, myChar.GainGem * myChar.Gain_Multiple, myChar.GainGem, Gem_UpText, Fail_ResultPanel.transform.Find("UpText").Find("Gem_UpText").gameObject));
    }

    //public void Test()
    //{
    //    if (myChar.EquipmentWeapon[3] < 5)
    //    {
    //        myChar.EquipmentWeapon[3]++;
    //    }
    //    else
    //    {
    //        myChar.EquipmentWeapon[3] = 0;
    //    }
        
    //}
    IEnumerator BossKillTimeScale()
    {
        Time.timeScale = 0.3f;
        StartCoroutine(BossSoundOn(3f));
        SoundManager.Instance.audioSource.volume = 0f;
        while (Time.timeScale < 1)
        {
            myChar.BossClear = false;
            yield return new WaitForSeconds(0.3f);
            Time.timeScale += 0.1f;
        }
    }
    IEnumerator BossSoundOn(float TimeCnt)
    {
        yield return new WaitForSecondsRealtime(TimeCnt);
        for (int i = 1; i < 6; i++)
        {
            Invoke("SlowSoundUp", (0.1f * i));
        }
    }
    
    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(1f);
        myChar.RewardGetCheck = true;
    }
    IEnumerator UI_ItemInfo(Transform EquipmentUI_Pos, GameObject ResultPanel)
    {
        for (int i = 0; i < myChar.EquipmentUsed.Count; i++)
        {
            EquipmentUI_Pos.gameObject.transform.GetChild(i).gameObject.SetActive(true);
            if (BtnOnCheck)
            {
                if (i == myChar.EquipmentUsed.Count - 1)
                {
                    ResultPanel.transform.Find("Btn_Panel").gameObject.SetActive(true);
                }
            }
            //float _start = Time.realtimeSinceStartup;
            //Debug.Log(_start + " // " + (_start + 1f));
            //if (Time.realtimeSinceStartup >= _start + 0.2f)
            //{
            //    yield return null;
            //}            
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
    IEnumerator TutorialStoneUsedCheck()
    {
        yield return new WaitForSeconds(5f);
        if (!myChar.TutoStoneUse)
        {
            Tutorial_Mask.SetActive(true);
        }
    }
    IEnumerator TutorialActiveItemUsedCheck()
    {
        yield return new WaitForSeconds(5f);
        if (!myChar.TutoItemUse)
        {
            Tutorial_Mask.SetActive(true);
        }
    }

    IEnumerator Count(Text TextBox, bool _plus, float target, float current, GameObject UpText, GameObject UpParent)
    {
        float duration = 3000f; // 카운팅에 걸리는 시간 설정.(최소 1000은되야 1자리 숫자들도 올라가는게 보임 너무작으면 증가숫자가 큰수가보임) 
        float PlusText = target - current;
        float raiseTime = 3000f;
        if (_plus)
        {
            float offset = (target - current) / duration;

            while (current < target)
            {
                current += offset * Time.unscaledTime;
                TextBox.text = current.ToString("F0");
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
                TextBox.text = current.ToString("F0");
                yield return null;
            }
        }
        current = (int)target;
        TextBox.text = current.ToString("F0");

        GameObject up_Text = Instantiate(UpText, transform.position, Quaternion.identity);
        up_Text.GetComponent<Text>().text = "+" + PlusText.ToString("F0");
        up_Text.transform.SetParent(UpParent.transform);
        up_Text.transform.localScale = new Vector3(1, 1, 1);
        up_Text.transform.localPosition = new Vector3(0, 0, 0);
        while (raiseTime > 0)
        {
            raiseTime -= Time.unscaledTime;
            yield return null;
        }
        Destroy(up_Text);
    }
    //public IEnumerator ThroneActive()
    //{
    //    yield return new WaitForSeconds(4f);
    //    Debug.Log(33);
    //    myChar.SelectLocation.transform.parent.Find("Throne").GetChild(0).gameObject.SetActive(true);
    //}

    //IEnumerator UI_ItemInfo(Transform EquipmentUI_Pos, GameObject ResultPanel)
    //{
    //    for (int i = 0; i < myChar.EquipmentUsed.Count; i++)
    //    {
    //        EquipmentUI_Pos.gameObject.transform.GetChild(i).gameObject.SetActive(true);
    //        if (BtnOnCheck)
    //        {
    //            if (i == myChar.EquipmentUsed.Count - 1)
    //            {
    //                ResultPanel.transform.Find("Btn_Panel").gameObject.SetActive(true);
    //            }
    //        }
    //        yield return new WaitForSeconds(0.2f);
    //    }
    //}

    //private void Test(int Cnt)
    //{
    //    EquipmentUI_Pos.gameObject.transform.GetChild(i).gameObject.SetActive(true)
    //}

}
