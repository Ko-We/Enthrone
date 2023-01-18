using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemScript : MonoBehaviour
{
    MyObject myChar;
    GameManager instance;

    public Rigidbody2D _rigdbody;

    public int EquipmentLv = 1;
    public string EquipmentName;
    public int UniqueNum;

    public int Orb_Ability;
    public int ActiveitemNum;
    public int ItemNum;
    [SerializeField]
    private int ArrayCnt;

    public int[] Status_Str = new int[3];
    public int[] Status_ASPD = new int[3];
    public int[] Status_Range = new int[3];
    public int[] Status_Heart = new int[3];

    private string AddText;
    public float MagneticRadius;

    [SerializeField]
    private bool PlayerCheck;
    private bool SoundCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //if (ActiveitemNum >=2000)
        //{
        //    ArrayCnt = ActiveitemNum - 2000;
        //}
        //else if (ActiveitemNum >= 1000)
        //{
        //    ArrayCnt = ActiveitemNum - 1000;
        //}
        //if (instance.PlayerSkill[9] < 1)
        //{
        //    MagneticRadius = 1f;
        //}
        //else if (instance.PlayerSkill[9] >= 1)
        //{
        //    MagneticRadius = 2f;
        //}
        //if (myChar.Stage != 7)
        //{
        //    SurroundCheckPlayer();
        //    Magnetic();
        //}        
    }
    private void SurroundCheckPlayer()
    {
        PlayerCheck = Physics2D.OverlapCircle(transform.position, MagneticRadius, 1 << LayerMask.NameToLayer("Player"));
    }
    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == "Player")
    //    {
    //        if (!RoomManager.Instance.EquipmentRoom)
    //        {
    //            SoundManager.Instance.PlaySfx(3);
    //            switch (myChar.ActiveitemAll[Orb_Ability])
    //            {
    //                case 0:
    //                    myChar.ElementStone[(Orb_Ability + 1)] += 20;
    //                    break;
    //                case 1:
    //                    myChar.ElementStone[(Orb_Ability + 1)] += 30;
    //                    break;
    //                case 2:
    //                    myChar.ElementStone[(Orb_Ability + 1)] += 50;
    //                    break;
    //            }
    //            Destroy(gameObject);
    //            if (myChar.Tutorial)
    //            {
    //                RoomManager.Instance.TutoCheck++;
    //            }
    //        }
    //    }
    //}
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        { 
            if (RoomManager.Instance.EquipmentRoom || RoomManager.Instance.AntiqueRoom)
            {
                instance.Select_Btn.SetActive(true);
                instance.Jump_Btn.SetActive(false);
                instance.EquipmentWindow.SetActive(true);
                instance.GiftBox_Btn.SetActive(false);
                instance.AD_Btn.SetActive(false);

                AntiqueItemInfoWindow();
                if (!SoundCheck)
                {
                    SoundManager.Instance.PlaySfx(18);
                    SoundCheck = true;
                }
            }

            if (myChar.Tutorial)
            {
                instance.Select_Btn.SetActive(true);
                instance.Jump_Btn.SetActive(false);
                instance.EquipmentWindow.SetActive(true);
                instance.GiftBox_Btn.SetActive(false);
                instance.AD_Btn.SetActive(false);

                AntiqueItemInfoWindow();
                if (!SoundCheck)
                {
                    SoundManager.Instance.PlaySfx(18);
                    SoundCheck = true;
                }
            }
        }


    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (RoomManager.Instance.EquipmentRoom || RoomManager.Instance.AntiqueRoom)
        {
            instance.Select_Btn.SetActive(false);
            instance.GiftBox_Btn.SetActive(false);
            instance.Jump_Btn.SetActive(true);
            instance.EquipmentWindow.SetActive(false);
            instance.AD_Btn.SetActive(false);
            SoundCheck = false;

            for (int i = 0; i < 4; i++)
            {
                instance.StoreUI.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }

            myChar.OnElementStoneCnt = 0;
        }

        if (myChar.Tutorial)
        {
            instance.Select_Btn.SetActive(false);
            instance.GiftBox_Btn.SetActive(false);
            instance.Jump_Btn.SetActive(true);
            instance.EquipmentWindow.SetActive(false);
            instance.AD_Btn.SetActive(false);
            SoundCheck = false;

            for (int i = 0; i < 4; i++)
            {
                instance.StoreUI.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }

            myChar.OnElementStoneCnt = 0;
        }


    }
    private void AntiqueItemInfoWindow()
    {
        GameObject InfoWindow = instance.EquipmentWindow;
        InfoWindow.transform.Find("Status_Image").gameObject.SetActive(false);

        EquipmentInfoText();
        
        //(myChar.ActiveitemAll[ItemNum - 2000] + 1) +1해주는이유는lv0이아닌 lv1부터시작하기위함
        switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Grade)
        {
            case 0:
                InfoWindow.transform.Find("ItemName_text").GetComponent<Text>().text = "<color=#e4e4e4>" + myChar.TextDataMgr.GetTemplate(ItemNum).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                InfoWindow.transform.Find("ItemLv_text").GetComponent<Text>().text = "<color=#e4e4e4>Lv" + (myChar.ActiveitemAll[ItemNum - 2000] + 1) + "</color>";
                break;
            case 1:
                InfoWindow.transform.Find("ItemName_text").GetComponent<Text>().text = "<color=#fff200>" + myChar.TextDataMgr.GetTemplate(ItemNum).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                InfoWindow.transform.Find("ItemLv_text").GetComponent<Text>().text = "<color=#fff200>Lv" + (myChar.ActiveitemAll[ItemNum - 2000] + 1) + "</color>";
                break;
            case 2:
                InfoWindow.transform.Find("ItemName_text").GetComponent<Text>().text = "<color=#ff00fe>" + myChar.TextDataMgr.GetTemplate(ItemNum).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                InfoWindow.transform.Find("ItemLv_text").GetComponent<Text>().text = "<color=#ff00fe>Lv" + (myChar.ActiveitemAll[ItemNum - 2000] + 1) + "</color>";
                break;
        }
    }

    private void EquipmentInfoText()
    {
        if (myChar.ActiveitemAll[ItemNum - 2000] == 0)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).InfoIndex)
            {
                case 10002:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1.ToString() + "%";
                    break;
                case 10003:
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1.ToString() + "%";
                    break;
                case 10004:
                    AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1.ToString() + "%";
                    break;
                case 10005:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1.ToString() + "%";
                    break;
                case 248:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ActiveitemAll[ItemNum - 2000] == 1)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).InfoIndex)
            {
                case 10002:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2.ToString() + "%";
                    break;
                case 10003:
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2.ToString() + "%";
                    break;
                case 10004:
                    AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2.ToString() + "%";
                    break;
                case 10005:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2.ToString() + "%";
                    break;
                case 248:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ActiveitemAll[ItemNum - 2000] == 2)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).InfoIndex)
            {
                case 10002:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3.ToString() + "%";
                    break;
                case 10003:
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3.ToString() + "%";
                    break;
                case 10004:
                    AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3.ToString() + "%";
                    break;
                case 10005:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3.ToString() + "%";
                    break;
                case 248:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ActiveitemAll[ItemNum - 2000] == 3)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).InfoIndex)
            {
                case 10002:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4.ToString() + "%";
                    break;
                case 10003:
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4.ToString() + "%";
                    break;
                case 10004:
                    AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4.ToString() + "%";
                    break;
                case 10005:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4.ToString() + "%";
                    break;
                case 248:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ActiveitemAll[ItemNum - 2000] == 4)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).InfoIndex)
            {
                case 10002:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5.ToString() + "%";
                    break;
                case 10003:
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5.ToString() + "%";
                    break;
                case 10004:
                    AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5.ToString() + "%";
                    break;
                case 10005:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5.ToString() + "%";
                    break;
                case 248:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ActiveitemAll[ItemNum - 2000] == 5)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).InfoIndex)
            {
                case 10002:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6.ToString() + "%";
                    break;
                case 10003:
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6.ToString() + "%";
                    break;
                case 10004:
                    AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6.ToString() + "%";
                    break;
                case 10005:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6.ToString() + "%";
                    break;
                case 248:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ActiveitemAll[ItemNum - 2000] == 6)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).InfoIndex)
            {
                case 10002:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7.ToString() + "%";
                    break;
                case 10003:
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7.ToString() + "%";
                    break;
                case 10004:
                    AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7.ToString() + "%";
                    break;
                case 10005:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7.ToString() + "%";
                    break;
                case 248:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ActiveitemAll[ItemNum - 2000] == 7)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).InfoIndex)
            {
                case 10002:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8.ToString() + "%";
                    break;
                case 10003:
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8.ToString() + "%";
                    break;
                case 10004:
                    AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8.ToString() + "%";
                    break;
                case 10005:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8.ToString() + "%";
                    break;
                case 248:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ActiveitemAll[ItemNum - 2000] == 8)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).InfoIndex)
            {
                case 10002:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9.ToString() + "%";
                    break;
                case 10003:
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9.ToString() + "%";
                    break;
                case 10004:
                    AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9.ToString() + "%";
                    break;
                case 10005:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9.ToString() + "%";
                    break;
                case 248:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ActiveitemAll[ItemNum - 2000] == 9)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).InfoIndex)
            {
                case 10002:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10.ToString() + "%";
                    break;
                case 10003:
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10.ToString() + "%";
                    break;
                case 10004:
                    AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10.ToString() + "%";
                    break;
                case 10005:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10.ToString() + "%";
                    break;
                case 248:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ActiveitemAll[ItemNum - 2000] == 10)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).InfoIndex)
            {
                case 10002:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11.ToString() + "%";
                    break;
                case 10003:
                    AddText = myChar.TextDataMgr.GetTemplate(1).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11.ToString() + "%";
                    break;
                case 10004:
                    AddText = myChar.TextDataMgr.GetTemplate(2).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11.ToString() + "%";
                    break;
                case 10005:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11.ToString() + "%";
                    break;
                case 248:
                    AddText = myChar.TextDataMgr.GetTemplate(3).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11.ToString() + "%";
                    break;
            }
        }
        string ItemInfo = myChar.TextDataMgr.GetTemplate(myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).InfoIndex).Content[myChar.LanguageNum].Replace("\\n", "\n");

        instance.EquipmentWindow.transform.Find("Info_Text").GetComponent<Text>().text =
            ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>";
    }
    private void Magnetic()
    {
        if (PlayerCheck)
        {
            transform.position = Vector2.MoveTowards(transform.position, instance.SelectCharacter.transform.position, Time.deltaTime * 10f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, MagneticRadius);
    }
}
