using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentScript : MonoBehaviour
{
    public MyObject myChar;
    public GameManager instance;
    public Animator _anim;
    public Rigidbody2D _rigdbody;

    public int EquipmentNum;
    public int EquipmentLv = 1;
    public string EquipmentName;
    public int UniqueNum;

    public int[] Special_Ability = new int[3];

    public int Orb_Ability;

    public int[] Status_Str = new int[3];
    public int[] Status_ASPD = new int[3];
    public int[] Status_Range = new int[3];
    public int[] Status_Heart = new int[3];

    private string AddText;
    private bool SoundCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
        _anim = GetComponent<Animator>();
        if (_anim.enabled == true)
        {
            StartCoroutine(AnimStart());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            instance.Select_Btn.SetActive(true);
            instance.Jump_Btn.SetActive(false);
            instance.EquipmentWindow.SetActive(true);
            instance.GiftBox_Btn.SetActive(false);
            instance.AD_Btn.SetActive(false);

            ItemInfoWindow();
            instance.UniqueNum = UniqueNum;
            if (!SoundCheck)
            {
                SoundManager.Instance.PlaySfx(18);
                SoundCheck = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            instance.Select_Btn.SetActive(false);
            instance.Jump_Btn.SetActive(true);
            instance.EquipmentWindow.SetActive(false);
            instance.GiftBox_Btn.SetActive(false);
            instance.AD_Btn.SetActive(false);
            SoundCheck = false;
        }
    }
    private void ItemInfoWindow()
    {
        GameObject InfoWindow = instance.EquipmentWindow;
        InfoWindow.transform.Find("Status_Image").gameObject.SetActive(true);
        //아이템 종류별로 아이템명 색상값주기
        //(myChar.EquipmentAll[EquipmentNum] + 1) +1해주는이유는lv0이아닌 lv1부터시작하기위함
        if (myChar.itemDataMgr.GetTemplate(EquipmentNum).Grade == 0)
        {
            InfoWindow.transform.Find("ItemName_text").GetComponent<Text>().text = "<color=#e4e4e4>" + myChar.TextDataMgr.GetTemplate(EquipmentNum + 97).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
            InfoWindow.transform.Find("ItemLv_text").GetComponent<Text>().text = "<color=#e4e4e4>Lv" + (myChar.EquipmentAll[EquipmentNum] + 1) + "</color>";
        }
        else if (myChar.itemDataMgr.GetTemplate(EquipmentNum).Grade == 1)
        {
            InfoWindow.transform.Find("ItemName_text").GetComponent<Text>().text = "<color=#fff200>" + myChar.TextDataMgr.GetTemplate(EquipmentNum + 97).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
            InfoWindow.transform.Find("ItemLv_text").GetComponent<Text>().text = "<color=#fff200>Lv" + (myChar.EquipmentAll[EquipmentNum] + 1) + "</color>";
        }
        else if (myChar.itemDataMgr.GetTemplate(EquipmentNum).Grade == 2)
        {
            InfoWindow.transform.Find("ItemName_text").GetComponent<Text>().text = "<color=#ff00fe>" + myChar.TextDataMgr.GetTemplate(EquipmentNum + 97).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
            InfoWindow.transform.Find("ItemLv_text").GetComponent<Text>().text = "<color=#ff00fe>Lv" + (myChar.EquipmentAll[EquipmentNum] + 1) + "</color>";
        }

        //업그레이드된 아이템 종류별로 성표시 띄워주기 및 능력치 보여주기
        EquipmentStateText();
        //EquipmentLvStar();
        EquipmentInfoText();


    }

    private void EquipmentInfoText()
    {
        switch (instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).StatIndex)
        {
            case 0:
                AddText = null;
                break;
            case 1:     //공격력
                if (EquipmentNum == 1002 || EquipmentNum == 5)
                {
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString() + "%";
                }
                else
                {
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString() + "%";
                }
                break;
            case 2:     //사정거리
                AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString() + "%";
                break;
            case 3:     //체력
                AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString() + "%";
                break;
            case 4:     //속도
                if (EquipmentNum < 1000)
                {
                    AddText = myChar.TextDataMgr.GetTemplate(4).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString() + "%";
                }
                else
                {
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString() + "%";
                }
                break;
            case 5:     //부활 횟수
                AddText = myChar.TextDataMgr.GetTemplate(5).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString();
                break;
            case 6:     //하트 획득
                if (EquipmentNum == 38)
                {
                    AddText = myChar.TextDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n") + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString() + "%";
                }
                else
                {
                    AddText = myChar.TextDataMgr.GetTemplate(6).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString();
                }
                
                break;
            case 7:     //쉴드 획득
                AddText = myChar.TextDataMgr.GetTemplate(7).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString();
                break;
            case 8:     //높이
                AddText = myChar.TextDataMgr.GetTemplate(8).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString();
                break;
            case 9:     //점프 횟수
                AddText = myChar.TextDataMgr.GetTemplate(9).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString();
                break;
            case 10:    //튕김
                AddText = myChar.TextDataMgr.GetTemplate(10).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString();
                break;
            case 11:    //확률
                if (EquipmentNum == 1003)
                {
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " " + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString() + "%";
                }
                else
                {
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString() + "%";
                }
                
                break;
            case 12:    //개수
                AddText = myChar.TextDataMgr.GetTemplate(12).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString();
                break;
            case 13:    //지속 시간 (초)
                AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).Value.ToString();
                break;
        }
        if (myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1 != 0)
        {
            string ItemInfo = myChar.TextDataMgr.GetTemplate((instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).InfoIndex)).Content[myChar.LanguageNum].Replace("\\n", "\n");

            instance.EquipmentWindow.transform.Find("Info_Text").GetComponent<Text>().text =
                ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>" + "\n";
        }
        else if (myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1 == 0)
        {
            instance.EquipmentWindow.transform.Find("Info_Text").GetComponent<Text>().text = "-";
        }
        //if (myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1 != 0)
        //{
        //    string ItemInfo = instance.SkillDataMgr.GetTemplate(myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1).KorText;

        //    instance.EquipmentWindow.transform.Find("Info_Text").GetComponent<Text>().text =
        //        ItemInfo.Replace("\\n", "\n") + "\n" + "<color=#00FF00>" + AddText + "</color>";
        //}
        //else if (myChar.itemDataMgr.GetTemplate(EquipmentNum).Option1 == 0)
        //{
        //    instance.EquipmentWindow.transform.Find("Info_Text").GetComponent<Text>().text = "-";
        //}

    }
    private void EquipmentStateText()
    {
        switch (myChar.EquipmentAll[EquipmentNum])
        {
            case 0:
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Atk0.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(1).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).ASPD0.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(2).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Range0.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(3).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).HP0.ToString();
                break;
            case 1:
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Atk1.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(1).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).ASPD1.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(2).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Range1.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(3).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).HP1.ToString();
                break;
            case 2:
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Atk2.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(1).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).ASPD2.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(2).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Range2.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(3).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).HP2.ToString();
                break;
            case 3:
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Atk3.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(1).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).ASPD3.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(2).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Range3.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(3).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).HP3.ToString();
                break;
            case 4:
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Atk4.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(1).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).ASPD4.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(2).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Range4.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(3).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).HP4.ToString();
                break;
            case 5:
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Atk5.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(1).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).ASPD5.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(2).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Range5.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(3).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).HP5.ToString();
                break;
            case 6:
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Atk6.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(1).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).ASPD6.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(2).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Range6.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(3).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).HP6.ToString();
                break;
            case 7:
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Atk7.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(1).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).ASPD7.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(2).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Range7.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(3).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).HP7.ToString();
                break;
            case 8:
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Atk8.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(1).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).ASPD8.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(2).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Range8.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(3).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).HP8.ToString();
                break;
            case 9:
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Atk8.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(1).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).ASPD8.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(2).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Range8.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(3).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).HP8.ToString();
                break;
            case 10:
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Atk9.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(1).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).ASPD9.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(2).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Range9.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(3).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).HP9.ToString();
                break;
            case 11:
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(0).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Atk10.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(1).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).ASPD10.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(2).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).Range10.ToString();
                instance.EquipmentWindow.transform.Find("Status_Image").GetChild(3).GetChild(0).GetComponent<Text>().text = myChar.itemDataMgr.GetTemplate(EquipmentNum).HP10.ToString();
                break;
        }
    }
    private void EquipmentLvStar()
    {
        if (myChar.EquipmentAll[EquipmentNum] <= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(1).gameObject.SetActive(false);
            }

            switch (myChar.EquipmentAll[EquipmentNum])
            {
                case 0:
                    for (int i = 0; i < 3; i++)
                    {
                        instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(0).gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    for (int i = 0; i < 1; i++)
                    {
                        instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(0).gameObject.SetActive(true);
                    }
                    for (int i = 1; i < 3; i++)
                    {
                        instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(0).gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    for (int i = 0; i < 2; i++)
                    {
                        instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(0).gameObject.SetActive(true);
                    }
                    for (int i = 2; i < 3; i++)
                    {
                        instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(0).gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(0).gameObject.SetActive(true);
                    }
                    break;
            }
        }
        else if (myChar.EquipmentAll[EquipmentNum] > 3)
        {
            for (int i = 0; i < 3; i++)
            {
                instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
            switch (myChar.EquipmentAll[EquipmentNum])
            {
                case 4:
                    for (int i = 0; i < 1; i++)
                    {
                        instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(1).gameObject.SetActive(true);
                    }
                    for (int i = 1; i < 3; i++)
                    {
                        instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(1).gameObject.SetActive(false);
                    }
                    break;
                case 5:
                    for (int i = 0; i < 2; i++)
                    {
                        instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(1).gameObject.SetActive(true);
                    }
                    for (int i = 2; i < 3; i++)
                    {
                        instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(1).gameObject.SetActive(false);
                    }
                    break;
                case 6:
                    for (int i = 0; i < 3; i++)
                    {
                        instance.EquipmentWindow.transform.Find("ItemLevel_img").GetChild(i).GetChild(1).gameObject.SetActive(true);
                    }
                    break;
            }
        }
    }

    //아이템 없어지면서 보스맵 
    private void OnDestroy()    
    {
        instance.DoorOpen();
        Destroy(gameObject);
    }

    IEnumerator AnimStart()
    {
        yield return new WaitForSeconds(0.5f);
        _anim.SetBool("Obtain", true);
    }


    //if (EquipmentLv == 1)
    //    {
    //        if (Special_Ability[0] != 99)
    //        {
    //            if (instance.PlayerSkill[Special_Ability[0]] <= 0)
    //            {
    //                instance.PlayerSkill[Special_Ability[0]] = 1;
    //            }

    //        }
    //    }
}
