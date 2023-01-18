using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgeItemNumScript : MonoBehaviour
{
    MyObject myChar;
    public int BtnNum;
    public int ItemIndex;
    public GameObject SelectPanel;
    public GameObject Caution;
    private GameObject Equipment;
    //private GameObject Silver;
    //private GameObject Gold;
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        Equipment = transform.Find("Equipment").gameObject;
        //Silver.SetActive(false);
        //Gold.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        BtnNum = transform.GetSiblingIndex();

        if (LobbiManager.instance.BlackSmithPanel.activeSelf)
        {
            if (BtnNum == LobbiManager.instance.SelectForgeNum)
            {
                SelectPanel.SetActive(true);
            }
            else
            {
                SelectPanel.SetActive(false);
            }
            UpgradeInfo();
        }
        else if (LobbiManager.instance.InventoryPanel.activeSelf)
        {
            if (BtnNum == LobbiManager.instance.SelectInventoryNum)
            {
                SelectPanel.SetActive(true);
            }
            else
            {
                SelectPanel.SetActive(false);
            }
        }
        
        ItemIconCheck();
        //ItemGradeCheck();
       
    }
    public void OnClickNumCheck()
    {
        if (LobbiManager.instance.BlackSmithPanel.activeSelf)
        {
            if (LobbiManager.instance.SelectForgeNum != BtnNum)
            {
                LobbiManager.instance.SelectForgeNum = BtnNum;
                LobbiManager.instance.SelectForgeIndex = ItemIndex;
            }
            else
            {
                LobbiManager.instance.SelectForgeNum = -1;
                LobbiManager.instance.SelectForgeIndex = -1;
            }
        }
        if (LobbiManager.instance.InventoryPanel.activeSelf)
        {
            if (LobbiManager.instance.SelectInventoryNum != BtnNum)
            {
                LobbiManager.instance.SelectInventoryNum = BtnNum;
                LobbiManager.instance.SelectInventoryIndex = ItemIndex;
            }
            else
            {
                LobbiManager.instance.SelectInventoryNum = -1;
                LobbiManager.instance.SelectInventoryIndex = -1;
            }
        }
        LobbiManager.instance.PopUpSound();
    }
    private void UpgradeInfo()
    {
        if (ItemIndex < 1000)
        {
            if ((myChar.EquipmentAll[ItemIndex] + 1) < LobbiManager.instance.EquipmentMaxLv && myChar.EquipmentAll[ItemIndex] > -1)
            {
                switch (myChar.itemDataMgr.GetTemplate(ItemIndex).Grade)
                {
                    case 0:
                        if (myChar.HeroHeart >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[ItemIndex] + 1).Nhero && myChar.EquipmentQuantity[ItemIndex] >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[ItemIndex] + 1).Nenchan)
                        {
                            Caution.SetActive(true);
                        }
                        else
                        {
                            Caution.SetActive(false);
                        }
                        break;
                    case 1:
                        if (myChar.HeroHeart >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[ItemIndex] + 1).Rhero && myChar.EquipmentQuantity[ItemIndex] >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[ItemIndex] + 1).Renchan)
                        {
                            Caution.SetActive(true);
                        }
                        else
                        {
                            Caution.SetActive(false);
                        }
                        break;
                    case 2:
                        if (myChar.HeroHeart >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[ItemIndex] + 1).Uhero && myChar.EquipmentQuantity[ItemIndex] >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.EquipmentAll[ItemIndex] + 1).Uenchan)
                        {
                            Caution.SetActive(true);
                        }
                        else
                        {
                            Caution.SetActive(false);
                        }
                        break;
                }
            }
            else
            {
                Caution.SetActive(false);
            }
        }
        else if (ItemIndex >= 1000 && ItemIndex < 2000)
        {
            if ((myChar.ElementStoneAll[ItemIndex - 1000] + 1) < LobbiManager.instance.EquipmentMaxLv && myChar.ElementStoneAll[ItemIndex - 1000] > -1)
            {
                switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade)
                {
                    case 0:
                        if (myChar.HeroHeart >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[ItemIndex - 1000] + 202).Nhero && myChar.EquipmentQuantity[ItemIndex - 1000] >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[ItemIndex - 1000] + 202).Nenchan)
                        {
                            Caution.SetActive(true);
                        }
                        else
                        {
                            Caution.SetActive(false);
                        }
                        break;
                    case 1:
                        if (myChar.HeroHeart >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[ItemIndex - 1000] + 202).Rhero && myChar.EquipmentQuantity[ItemIndex - 1000] >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[ItemIndex - 1000] + 202).Renchan)
                        {
                            Caution.SetActive(true);
                        }
                        else
                        {
                            Caution.SetActive(false);
                        }
                        break;
                    case 2:
                        if (myChar.HeroHeart >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[ItemIndex - 1000] + 202).Uhero && myChar.EquipmentQuantity[ItemIndex - 1000] >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.ElementStoneAll[ItemIndex - 1000] + 202).Uenchan)
                        {
                            Caution.SetActive(true);
                        }
                        else
                        {
                            Caution.SetActive(false);
                        }
                        break;
                }
            }
            else
            {
                Caution.SetActive(false);
            }
        }
        else if (ItemIndex > 2000)
        {
            if ((myChar.ActiveitemAll[ItemIndex - 2000] + 1) < LobbiManager.instance.EquipmentMaxLv && myChar.ActiveitemAll[ItemIndex - 2000] > -1)
            {
                switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade)
                {
                    case 0:
                        if (myChar.HeroHeart >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[ItemIndex - 2000] + 102).Nhero && myChar.EquipmentQuantity[ItemIndex - 2000] >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[ItemIndex - 2000] + 102).Nenchan)
                        {
                            Caution.SetActive(true);
                        }
                        else
                        {
                            Caution.SetActive(false);
                        }
                        break;
                    case 1:
                        if (myChar.HeroHeart >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[ItemIndex - 2000] + 102).Rhero && myChar.EquipmentQuantity[ItemIndex - 2000] >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[ItemIndex - 2000] + 102).Renchan)
                        {
                            Caution.SetActive(true);
                        }
                        else
                        {
                            Caution.SetActive(false);
                        }
                        break;
                    case 2:
                        if (myChar.HeroHeart >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[ItemIndex - 2000] + 102).Uhero && myChar.EquipmentQuantity[ItemIndex - 2000] >= LobbiManager.instance.BlackSmithDataMgr.GetTemplate(myChar.ActiveitemAll[ItemIndex - 2000] + 102).Uenchan)
                        {
                            Caution.SetActive(true);
                        }
                        else
                        {
                            Caution.SetActive(false);
                        }
                        break;
                }
            }
            else
            {
                Caution.SetActive(false);
            }
        }
    }

    //private void ItemGradeCheck()
    //{
    //    if (ItemIndex < 1000)
    //    {
    //        GradeTrueFalseCheck(myChar.EquipmentAll[ItemIndex]);
    //    }
    //    else if (ItemIndex >= 1000 && ItemIndex < 2000)
    //    {
    //        GradeTrueFalseCheck(myChar.ElementStoneAll[ItemIndex - 1000]);
    //    }
    //    else if (ItemIndex >= 2000)
    //    {
    //        GradeTrueFalseCheck(myChar.ActiveitemAll[ItemIndex - 2000]);
    //    }
    //}
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

    //private void GradeTrueFalseCheck(int ItemNum)
    //{
    //    if (ItemNum <= 3)
    //    {
    //        Silver.SetActive(true);
    //        Gold.SetActive(false);
    //    }
    //    else if (ItemNum >= 4)
    //    {
    //        Silver.SetActive(true);
    //        Gold.SetActive(true);
    //    }
    //    switch (ItemNum)
    //    {
    //        case -1:
    //            Silver.transform.GetChild(0).gameObject.SetActive(false);
    //            Silver.transform.GetChild(1).gameObject.SetActive(false);
    //            Silver.transform.GetChild(2).gameObject.SetActive(false);
    //            break;
    //        case 0:
    //            Silver.transform.GetChild(0).gameObject.SetActive(false);
    //            Silver.transform.GetChild(1).gameObject.SetActive(false);
    //            Silver.transform.GetChild(2).gameObject.SetActive(false);
    //            break;
    //        case 1:
    //            Silver.transform.GetChild(0).gameObject.SetActive(true);
    //            Silver.transform.GetChild(1).gameObject.SetActive(false);
    //            Silver.transform.GetChild(2).gameObject.SetActive(false);
    //            break;
    //        case 2:
    //            Silver.transform.GetChild(0).gameObject.SetActive(true);
    //            Silver.transform.GetChild(1).gameObject.SetActive(true);
    //            Silver.transform.GetChild(2).gameObject.SetActive(false);
    //            break;
    //        case 3:
    //            Silver.transform.GetChild(0).gameObject.SetActive(true);
    //            Silver.transform.GetChild(1).gameObject.SetActive(true);
    //            Silver.transform.GetChild(2).gameObject.SetActive(true);
    //            break;
    //        case 4:
    //            Gold.transform.GetChild(0).gameObject.SetActive(true);
    //            Gold.transform.GetChild(1).gameObject.SetActive(false);
    //            Gold.transform.GetChild(2).gameObject.SetActive(false);
    //            break;
    //        case 5:
    //            Gold.transform.GetChild(0).gameObject.SetActive(true);
    //            Gold.transform.GetChild(1).gameObject.SetActive(true);
    //            Gold.transform.GetChild(2).gameObject.SetActive(false);
    //            break;
    //        case 6:
    //            Gold.transform.GetChild(0).gameObject.SetActive(true);
    //            Gold.transform.GetChild(1).gameObject.SetActive(true);
    //            Gold.transform.GetChild(2).gameObject.SetActive(true);
    //            break;
    //    }
    //}
}
