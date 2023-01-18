using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    MyObject myChar;
    public static string nextScene;

    [SerializeField]
    Image progressBar;
    public GameObject Loding_Text;
    public GameObject LoadingPanel;


    //private void Awake()
    //{
    //    Loding_Text = GameObject.Find("Loding_Text");
    //}
    private void Start()
    {
        StartCoroutine(LoadScene());
        myChar = MyObject.MyChar;
    }
    private void Update()
    {
        Loding_Text.GetComponent<Text>().text = (GameObject.Find("Liding_img").GetComponent<Image>().fillAmount * 100f).ToString("F0") + "%";

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

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;

        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        progressBar.fillAmount = 0f;
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);

        op.allowSceneActivation = false;

        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);

                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            { 
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}