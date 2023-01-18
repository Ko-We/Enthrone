using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinInfoScript : MonoBehaviour
{
    MyObject myChar;
    public int BtnNum;
    public int SkinIndex;

    public bool SkinSelectCheck = false;
    public GameObject SelectPanel;
    public GameObject RockPanel;
    public GameObject Equipment;
    public bool Weapon, Costume;
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
    }

    // Update is called once per frame
    void Update()
    {
        //무기 착용중인지 체크해주는스크립
        if (Weapon)
        {
            if (myChar.EquipmentWeapon[myChar.SelectHero] == BtnNum)
            {
                Equipment.SetActive(true);
            }
            else
            {
                Equipment.SetActive(false);
            }
        }
        //코스튬 착용중인지 체크해주는스크립
        if (Costume)
        {
            if (myChar.EquipmentCostume[myChar.SelectHero] == BtnNum)
            {
                Equipment.SetActive(true);
            }
            else
            {
                Equipment.SetActive(false);
            }
        }
        
        BtnNum = transform.GetSiblingIndex();
        RockCheck();

        if (Weapon)
        {
            if (BtnNum == myChar.SelectWeapon[myChar.SelectHero])
            {
                SelectPanel.SetActive(true);
            }
            else
            {
                SelectPanel.SetActive(false);
            }
        }

        if (Costume)
        {
            if (BtnNum == myChar.SelectCostume[myChar.SelectHero])
            {
                SelectPanel.SetActive(true);
            }
            else
            {
                SelectPanel.SetActive(false);
            }
        }
    }
    public void OnClickNumCheck()
    {
        LobbiManager.instance.ClickSound();
        if (Weapon)
        {
            if (myChar.SelectWeapon[myChar.SelectHero] != BtnNum)
            {
                if (myChar.SelectCostume[myChar.SelectHero] != -1)
                {
                    myChar.SelectCostume[myChar.SelectHero] = -1;
                }
                myChar.SelectWeapon[myChar.SelectHero] = BtnNum;
            }
            else
            {
                myChar.SelectWeapon[myChar.SelectHero] = -1;
            }
        }
        if (Costume)
        {
            if (myChar.SelectCostume[myChar.SelectHero] != BtnNum)
            {
                if (myChar.SelectWeapon[myChar.SelectHero] != -1)
                {
                    myChar.SelectWeapon[myChar.SelectHero] = -1;
                }
                myChar.SelectCostume[myChar.SelectHero] = BtnNum;
            }
            else
            {
                myChar.SelectCostume[myChar.SelectHero] = -1;
            }
        }

    }
    public void RockCheck()
    {
        if (myChar.SelectHero == 0)
        {
            if (Weapon)
            {
                if (myChar.KnightWeaponSkinPurchase[BtnNum] == -1)
                {
                    RockPanel.SetActive(true);
                }
                else
                {
                    RockPanel.SetActive(false);
                }
            }

            if (Costume)
            {
                if (myChar.KnightCostumeSkinPurchase[BtnNum] == -1)
                {
                    RockPanel.SetActive(true);
                }
                else
                {
                    RockPanel.SetActive(false);
                }
            }
        }
        else if (myChar.SelectHero == 1)
        {
            if (Weapon)
            {
                if (myChar.WarriorWeaponSkinPurchase[BtnNum] == -1)
                {
                    RockPanel.SetActive(true);
                }
                else
                {
                    RockPanel.SetActive(false);
                }
            }

            if (Costume)
            {
                if (myChar.WarriorCostumeSkinPurchase[BtnNum] == -1)
                {
                    RockPanel.SetActive(true);
                }
                else
                {
                    RockPanel.SetActive(false);
                }
            }
        }
        else if (myChar.SelectHero == 2)
        {
            if (Weapon)
            {
                if (myChar.ArcherWeaponSkinPurchase[BtnNum] == -1)
                {
                    RockPanel.SetActive(true);
                }
                else
                {
                    RockPanel.SetActive(false);
                }
            }

            if (Costume)
            {
                if (myChar.ArcherCostumeSkinPurchase[BtnNum] == -1)
                {
                    RockPanel.SetActive(true);
                }
                else
                {
                    RockPanel.SetActive(false);
                }
            }
        }
        else if (myChar.SelectHero == 3)
        {
            if (Weapon)
            {
                if (myChar.WizardWeaponSkinPurchase[BtnNum] == -1)
                {
                    RockPanel.SetActive(true);
                }
                else
                {
                    RockPanel.SetActive(false);
                }
            }

            if (Costume)
            {
                if (myChar.WizardCostumeSkinPurchase[BtnNum] == -1)
                {
                    RockPanel.SetActive(true);
                }
                else
                {
                    RockPanel.SetActive(false);
                }
            }
        }
        else if (myChar.SelectHero == 4)
        {
            if (Weapon)
            {
                if (myChar.NinjaWeaponSkinPurchase[BtnNum] == -1)
                {
                    RockPanel.SetActive(true);
                }
                else
                {
                    RockPanel.SetActive(false);
                }
            }

            if (Costume)
            {
                if (myChar.NinjaCostumeSkinPurchase[BtnNum] == -1)
                {
                    RockPanel.SetActive(true);
                }
                else
                {
                    RockPanel.SetActive(false);
                }
            }
        }
    }
}
