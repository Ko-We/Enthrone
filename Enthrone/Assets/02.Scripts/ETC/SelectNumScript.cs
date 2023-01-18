using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectNumScript : MonoBehaviour
{
    MyObject myChar;
    public int BtnNum;
    public int ItemIndex;
    public GameObject SelectPanel;

    private GameObject Equipment;
    private Text Lv_Text;

    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        Equipment = transform.Find("Equipment").gameObject;
        Lv_Text = transform.Find("LV_Text").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        //ItemIndex = myChar.PlayerEquiement[ItemIndex];
       
        if (BtnNum == GameManager.Instance.SelectItemNum)
        {
            SelectPanel.SetActive(true);
        }
        else
        {
            SelectPanel.SetActive(false);
        }
        ItemIconCheck();
        LvCheck();
        startGradeCheck();
        //ItemGradeCheck();
    }
    public void OnClickNumCheck()
    {
        GameManager.Instance.SelectItemNum = BtnNum;
    }

    private void ItemIconCheck()
    {
        if (ItemIndex < 1000)
        {
            Equipment.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + ItemIndex.ToString());
        }
        else
        {
            Equipment.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + ItemIndex.ToString());
        }
    }
    private void LvCheck()
    {
        if (ItemIndex < 1000)
        {
            switch (myChar.itemDataMgr.GetTemplate(ItemIndex).Grade)
            {
                case 0:
                    Lv_Text.text = "<color=#FFFFFF>Lv." + (myChar.EquipmentAll[ItemIndex] + 1) + "</color>";

                    break;
                case 1:
                    Lv_Text.text = "<color=#FDE43C>Lv." + (myChar.EquipmentAll[ItemIndex] + 1) + "</color>";
                    break;
                case 2:
                    Lv_Text.text = "<color=#FF85D0>Lv." + (myChar.EquipmentAll[ItemIndex] + 1) + "</color>";
                    break;
            }
        }
        else
        {
            if (ItemIndex >= 1000 && ItemIndex < 2000)
            {
                switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade)
                {
                    case 0:
                        Lv_Text.text = "<color=#FFFFFF>Lv." + (myChar.ElementStoneAll[ItemIndex - 1000] + 1) + "</color>";
                        break;
                    case 1:
                        Lv_Text.text = "<color=#FDE43C>Lv." + (myChar.ElementStoneAll[ItemIndex - 1000] + 1) + "</color>";
                        break;
                    case 2:
                        Lv_Text.text = "<color=#FF85D0>Lv." + (myChar.ElementStoneAll[ItemIndex - 1000] + 1) + "</color>";
                        break;
                }
            }
            else if (ItemIndex >= 2000)
            {
                switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade)
                {
                    case 0:
                        Lv_Text.text = "<color=#FFFFFF>Lv." + (myChar.ActiveitemAll[ItemIndex - 2000] + 1) + "</color>";
                        break;
                    case 1:
                        Lv_Text.text = "<color=#FDE43C>Lv." + (myChar.ActiveitemAll[ItemIndex - 2000] + 1) + "</color>";
                        break;
                    case 2:
                        Lv_Text.text = "<color=#FF85D0>Lv." + (myChar.ActiveitemAll[ItemIndex - 2000] + 1) + "</color>";
                        break;
                }
            }
        }
    }
    private void startGradeCheck()
    {
        if (ItemIndex < 1000)
        {
            transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + myChar.itemDataMgr.GetTemplate(ItemIndex).Grade);
        }
        else
        {
            transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/01_EquipmentWindow/" + myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade);
        }
    }

}
