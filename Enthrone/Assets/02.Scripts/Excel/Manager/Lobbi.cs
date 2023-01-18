using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobbi : MonoBehaviour
{
    private static TitleManager s_MyObject = null;
    public static TitleManager instance
    {
        get
        {
            if (s_MyObject == null)
            {
                s_MyObject = FindObjectOfType(typeof(TitleManager)) as TitleManager;
                if (s_MyObject == null)
                {
                    GameObject obj = new GameObject("MyChar");
                    s_MyObject = obj.AddComponent(typeof(TitleManager)) as TitleManager;
                }
            }
            return s_MyObject;
        }
    }
    /*************************************************************************************/
    MyObject myChar;
    public HeroTemplate HeroData;
    public HeroTemplateMgr HeroDataMgr;

    private GameObject CharacterBackground;
    private GameObject Characterimg;
    private GameObject HeroNameWindow;
    private GameObject HeroStats_Panel;
    private GameObject HeroViewport;
    private Text KeyText;
    private Text SlotCoinText;
    private List<GameObject> ClothingCostum;
    private List<GameObject> WeaponeCostum;
    private GameObject CoinBtn;
    private GameObject CashBtn;
    public GameObject Coin_Img;

    public List<GameObject> HeroBackPanel = new List<GameObject>();
    public List<GameObject> AtkGage, ASPDGage, RangeGage, HPGage = new List<GameObject>();
    public List<GameObject> KnightGrade, WarriorGrade, ArcherGrade, WizardGrade, NinjaGrade = new List<GameObject>();

    public List<Sprite> HeroBackPanelActive = new List<Sprite>();
    public List<Sprite> HeroBackPanelNoneActive = new List<Sprite>();

    private Text[] StatsText;

    public GameObject SlotPanel;
    public GameObject AttendanceWindow;
    public GameObject PreferencesWindow;


    private void Awake()
    {
        myChar = MyObject.MyChar;
        HeroDataMgr = new HeroTemplateMgr();

        CharacterBackground = GameObject.Find("Character_BackGround");
        Characterimg = GameObject.Find("Character_Img");
        HeroNameWindow = GameObject.Find("HeroName_Text");
        HeroStats_Panel = GameObject.Find("HeroStats_Panel");
        HeroViewport = GameObject.Find("HeroViewport");
        KeyText = GameObject.Find("Key_Text").GetComponent<Text>();
        SlotCoinText = GameObject.Find("SlotCoin_Text").GetComponent<Text>();
        CoinBtn = GameObject.Find("Slot_Coin_Btn");
        CashBtn = GameObject.Find("Slot_Cash_Btn");
        Coin_Img = GameObject.Find("Coin_Img");

        SlotPanel = GameObject.Find("Slot_Panel");
        AttendanceWindow = GameObject.Find("Attendance_Panel");
        PreferencesWindow = GameObject.Find("Preferences_Panel");

        StatsText = new Text[HeroStats_Panel.transform.childCount];

        for (int i = 0; i < 10; i++)
        {
            AtkGage.Add(GameObject.Find("AtkGage").transform.GetChild(i).gameObject);
            ASPDGage.Add(GameObject.Find("ASPDGage").transform.GetChild(i).gameObject);
            RangeGage.Add(GameObject.Find("RangeGage").transform.GetChild(i).gameObject);
            HPGage.Add(GameObject.Find("HPGage").transform.GetChild(i).gameObject);
            AtkGage[i].transform.GetChild(0).gameObject.SetActive(false);
            ASPDGage[i].transform.GetChild(0).gameObject.SetActive(false);
            RangeGage[i].transform.GetChild(0).gameObject.SetActive(false);
            HPGage[i].transform.GetChild(0).gameObject.SetActive(false);

        }
        for (int i = 0; i < HeroViewport.transform.childCount; i++)
        {
            HeroBackPanel.Add(HeroViewport.transform.GetChild(i).GetChild(0).gameObject);
            KnightGrade.Add(HeroViewport.transform.GetChild(0).GetChild(0).Find("Grade_Panel").GetChild(i).gameObject);
            WarriorGrade.Add(HeroViewport.transform.GetChild(1).GetChild(0).Find("Grade_Panel").GetChild(i).gameObject);
            ArcherGrade.Add(HeroViewport.transform.GetChild(2).GetChild(0).Find("Grade_Panel").GetChild(i).gameObject);
            WizardGrade.Add(HeroViewport.transform.GetChild(3).GetChild(0).Find("Grade_Panel").GetChild(i).gameObject);
            NinjaGrade.Add(HeroViewport.transform.GetChild(4).GetChild(0).Find("Grade_Panel").GetChild(i).gameObject);
        }
        for (int i = 0; i < HeroStats_Panel.transform.childCount; i++)
        {
            StatsText[i] = HeroStats_Panel.transform.GetChild(i).GetComponentInChildren<Text>();
        }
        SlotPanel.SetActive(false);
        AttendanceWindow.SetActive(false);
        PreferencesWindow.SetActive(false);
    }
    private void Start()
    {
        OnLoadHeroTemplateMgr();
    }

    void Update()
    {
        //DayCheck();
        HeroUIManager();
        KeyText.text = myChar.Key.ToString("F0");
        SlotCoinText.text = myChar.SlotCoin.ToString("F0");

        //SlotPanelControl();
        HeroBackPanelCheck();
        PreferencesWindowCheck();

        //if (AttendanceWindow.activeSelf == false)
        //{
        //    myChar.AttendanceWindowOpenCheck = false;
        //}
    }
    public void ClothingCostumeItemCheck(int num)
    {
        myChar.SelectClotihingCostume = num;
    }
    public void WeaponCostumeItemCheck(int num)
    {
        myChar.SelectWeaponCostume = num;
    }

    private void SlotPanelControl()
    {
        if (SlotPanel.activeSelf == true)
        {
            if (myChar.SlotCoin > 0)
            {
                CoinBtn.SetActive(true);
                CashBtn.SetActive(false);
                //CoinBtn.transform.GetChild(1).GetComponent<Text>().text = myChar.SlotCoin.ToString("F0");
            }
            else
            {
                CoinBtn.SetActive(false);
                CashBtn.SetActive(true);
                CashBtn.transform.GetChild(1).GetComponent<Text>().text = myChar.Gem.ToString("F0");
            }
        }
    }
    private void HeroGage(float HeroAtk, float HeroASPD, float HeroRange, int HeroHP)
    {
        StatsText[0].text = " LV." + HeroAtk.ToString("F0");
        StatsText[1].text = " LV." + HeroASPD.ToString("F0");
        StatsText[2].text = " LV." + HeroRange.ToString("F0");
        StatsText[3].text = " LV." + HeroHP.ToString("F0");
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
    private void HeroGrade(List<GameObject> Hero, int HeroNum)
    {
        for (int i = 0; i < Hero.Count; i++)
        {
            if (i < myChar.HeroLv[HeroNum])
            {
                Hero[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                Hero[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }

    }
    private void HeroUIManager()
    {
        HeroGrade(KnightGrade, 0);
        HeroGrade(WarriorGrade, 1);
        HeroGrade(ArcherGrade, 2);
        HeroGrade(WizardGrade, 3);
        HeroGrade(NinjaGrade, 4);

        if (myChar.SelectHero == 0)
        {
            HeroNameWindow.GetComponent<Text>().text = "Knight";

            //HeroGrade(KnightGrade);

            HeroGage(HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KAtk,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KASPD,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KRange,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KHP);
            myChar.CurrentDamage = HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).KAtk;
        }
        if (myChar.SelectHero == 1)
        {
            HeroNameWindow.GetComponent<Text>().text = "Warrior";

            //HeroGrade(WarriorGrade);

            HeroGage(HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WAtk,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WASPD,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WRange,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WHP);
        }
        if (myChar.SelectHero == 2)
        {
            HeroNameWindow.GetComponent<Text>().text = "Archer";

            //HeroGrade(ArcherGrade);

            HeroGage(HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).AAtk,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).AASPD,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).ARange,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).AHP);
        }
        if (myChar.SelectHero == 3)
        {
            HeroNameWindow.GetComponent<Text>().text = "Wizzard";

            //HeroGrade(WizardGrade);

            HeroGage(HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WiAtk,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WiASPD,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WiRange,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).WiHP);
        }
        if (myChar.SelectHero == 4)
        {
            HeroNameWindow.GetComponent<Text>().text = "Ninja";

            //HeroGrade(NinjaGrade);

            HeroGage(HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).NAtk,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).NASPD,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).NRange,
                     HeroDataMgr.GetTemplate(myChar.HeroLv[myChar.SelectHero]).NHP);
        }
    }

    public void PlusHeroBtn()
    {
        if (myChar.SelectHero != 4)
        {
            myChar.SelectHero++;
        }
        else if (myChar.SelectHero == 4)
        {
            myChar.SelectHero = 0;
        }
    }
    public void MinusHeroBtn()
    {
        if (myChar.SelectHero != 0)
        {
            myChar.SelectHero--;
        }
        else if (myChar.SelectHero == 0)
        {
            myChar.SelectHero = 4;
        }
    }
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

    public void StagerInput()
    {
        SceneManager.LoadScene("StageScene");
        myChar.InGameStart = true;
    }

    void OnLoadHeroTemplateMgr()
    {
        string TextResource = "01_Excel/Hero_info";
        HeroDataMgr.OnDataLoad(TextResource);
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
        }
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

    public void EffectSoundMute()
    {
        myChar.muteEffectSound = true;
    }
    public void EffectSoundPlay()
    {
        myChar.muteEffectSound = false;
    }
    public void BGMSoundMute()
    {
        myChar.muteBGM = true;
    }
    public void BGMSoundPlay()
    {
        myChar.muteBGM = false;
    }

    public void Test()
    {
        myChar.EquipmentAll[myChar.itemNum] = myChar.ItemLv;
        myChar.EquipmentUsed.Add(myChar.itemNum);
        myChar.EquipmentUsedTrueCheck.Add(false);
    }

    //public void DayCheck()
    //{
    //    if (AttendanceWindow.activeSelf == true)
    //    {
    //        if (!myChar.AttendanceWindowOpenCheck)
    //        {
    //            Debug.Log(11);
    //            for (int i = 0; i < AttendanceWindow.transform.GetChild(0).GetChild(0).childCount; i++)
    //            {
    //                AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(false);
    //            }
    //            for (int i = 0; i < AttendanceWindow.transform.GetChild(0).GetChild(1).childCount; i++)
    //            {
    //                AttendanceWindow.transform.GetChild(0).GetChild(1).GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
    //            }
    //            myChar.AttendanceWindowOpenCheck = true;
    //        }
    //    }
    //    if (myChar.NowDate == 0)
    //    {
    //        //화살표
    //        for (int i = 0; i < 2; i++)
    //        {
    //            AttendanceWindow.transform.GetChild(0).GetChild(1).GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
    //        }

    //        if (myChar.Attendancereward)
    //        {
    //            AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
    //        }
    //        else if (!myChar.Attendancereward)
    //        {
    //            AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
    //        }
    //        for (int i = 1; i <= 2; i++)
    //        {
    //            AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(false);
    //        }
    //    }
    //    else if (myChar.NowDate == 1)
    //    {
    //        //화살표
    //        AttendanceWindow.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
    //        AttendanceWindow.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
    //        if (myChar.Attendancereward)
    //        {
    //            AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(true);
    //            AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
    //        }
    //        if (!myChar.Attendancereward)
    //        {
    //            AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(true);
    //            AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
    //        }

    //        AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
    //        AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(false);
    //        AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(false);
    //        AttendanceWindow.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);

    //    }
    //    else if (myChar.NowDate >= 2)
    //    {
    //        //화살표
    //        for (int i = 0; i < 2; i++)
    //        {
    //            AttendanceWindow.transform.GetChild(0).GetChild(1).GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(true);
    //        }

    //        if (myChar.Attendancereward)
    //        {
    //            AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(true);
    //            AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(true);

    //        }
    //        if (!myChar.Attendancereward)
    //        {
    //            AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(true);
    //            AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(false);
    //        }

    //        AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(true);
    //        AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
    //        AttendanceWindow.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);

    //        AttendanceWindow.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
    //    }
    //}
    //private void Reward()
    //{

    //}

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
}
