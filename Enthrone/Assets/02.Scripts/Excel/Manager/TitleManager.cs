using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TitleManager : MonoBehaviour
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
    MyObject myChar;
    public Sprite[] TitleImg;
    private GameObject lobbyBackGround;
    public HeroTemplate HeroData;
    public HeroTemplateMgr HeroDataMgr;
    public Text Ver_Text;

    public GameObject VideoPanel;
    public GameObject Video;
    public VideoPlayer videoClip;
    public double time;
    public double currentTime;
    public GameObject Star_Img;

    public bool BgmCheck = false;
    private Text StartText;
    private Text WarrningText;
    // Start is called before the first frame update
    private void Awake()
    {
        myChar = MyObject.MyChar;
        HeroDataMgr = new HeroTemplateMgr();

        lobbyBackGround = GameObject.Find("Lobby_BackGround");
        time = videoClip.GetComponent<VideoPlayer>().length - 0.1f;
        VideoPanel = GameObject.Find("VideoPanel");
        StartText = GameObject.Find("Start_Text").GetComponent<Text>();
        WarrningText = GameObject.Find("Warrning_Text").GetComponent<Text>();
    }
    private void Start()
    {
        OnLoadHeroTemplateMgr();
    }
    // Update is called once per frame
    void Update()
    {
        Ver_Text.text = myChar.Throne_Version;
        if (VideoPanel.activeSelf)
        {
            Intro_Check();
        }
        lobbyBackGround.GetComponent<Image>().sprite = TitleImg[myChar.EnthroneHeroNum + 1];
        if (myChar.Tutorial)
        {
            myChar.SelectHero = 1;
            myChar.EnthroneHeroNum = 1;
        }

        StartText.text = myChar.TextDataMgr.GetTemplate(218).Content[myChar.LanguageNum].Replace("\\n", "\n");
        WarrningText.text = myChar.TextDataMgr.GetTemplate(219).Content[myChar.LanguageNum].Replace("\\n", "\n");
    }
    public void StagerInput()
    {
        if (!myChar.Tutorial)
        {
            SceneManager.LoadScene("LobbiScene");
        }
        else if (myChar.Tutorial)
        {
            myChar.InGameStart = true;
            SceneManager.LoadScene("StageScene");
        }


    }
    public void Intro_Check()
    {
        currentTime = videoClip.GetComponent<VideoPlayer>().time;
        if (currentTime >= time)
        {
            StartCoroutine(Panel_False(1f));
            Star_Img.SetActive(true);
        }
    }
    void OnLoadHeroTemplateMgr()
    {
        string TextResource = "01_Excel/Hero_info";
        HeroDataMgr.OnDataLoad(TextResource);
    }

    public void Skip_btn()
    {
        StartCoroutine(Panel_False(0f));
        Star_Img.SetActive(true);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    IEnumerator Panel_False(float Cnt)
    {
        yield return new WaitForSeconds(Cnt);
        VideoPanel.SetActive(false);
    }
}
