using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbScript : MonoBehaviour
{   
    public MyObject myChar;
    public GameManager instance;

    public Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private GameObject PlayerList;
    public Transform target;

    public int Orb_Ability;
    public int ItemNum;

    private string AddText;
    public float MagneticRadius;
    public float WallcheckRadius;
    public float DropPosX, DropPosY;

    [SerializeField]
    private bool PlayerCheck;
    public bool isWallCheck;
    public bool isDropCheck;

    public bool ObtainItemCheck = false;        //이미 획득한 템인지 확인후 보유중인 장비에 중복적용막기위함
    private bool SoundCheck = false;

    // Start is called before the first frame update
    private void Awake()
    {
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        isDropCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        Magnetic();

        SurroundCheckPlayer();
        TargetAutoCheck();

        if (myChar.Stage % 10 != 3 && myChar.Stage % 10 != 6)
        {
            if (GameManager.Instance.PlayerSkill[9] < 1)
            {
                MagneticRadius = 1f;
            }
            else if (GameManager.Instance.PlayerSkill[9] >= 1)
            {
                MagneticRadius = 2f;
            }
        }
        else
        {
            MagneticRadius = 1f;
        }       
    }
    //private void SurroundCheckPlayer()
    //{
    //    PlayerCheck = Physics2D.OverlapCircle(transform.position, MagneticRadius, 1 << LayerMask.NameToLayer("Player"));
    //}
    private void SurroundCheckPlayer()
    {
        PlayerCheck = Physics2D.OverlapCircle(transform.position, MagneticRadius, 1 << LayerMask.NameToLayer("Player"));
        isWallCheck = Physics2D.OverlapCircle(new Vector2(transform.position.x + DropPosX, transform.position.y + DropPosY), WallcheckRadius,LayerMask.GetMask("Ground", "PassGround"));
    }
    public void TargetAutoCheck()
    {
        GameObject Player = GameObject.Find("Player");

        for (int i = 0; i < myChar.HeroNum; i++)
        {
            if (Player.transform.GetChild(i).gameObject.activeSelf == true)
            {
                if (Player.transform.GetChild(i).name != "SeasnalShield")
                {
                    target = Player.transform.GetChild(i);
                    PlayerList = target.gameObject;
                }

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (instance.RoomManagerObj.activeSelf)
            {
                if (!RoomManager.Instance.EquipmentRoom)
                {
                    SoundManager.Instance.PlaySfx(3);
                    for (int i = 0; i < myChar.PlayerEquipment.Count; i++)
                    {
                        if (myChar.PlayerEquipment[i] == (Orb_Ability + 1000))
                        {
                            ObtainItemCheck = true;
                        }
                    }
                    if (!ObtainItemCheck)
                    {
                        myChar.PlayerEquipment.Add(Orb_Ability + 1000);
                        myChar.EnthroneEquipment.Add(Orb_Ability + 1000);
                    }

                    myChar.ElementStone[(Orb_Ability + 1)] += 20;
                    //switch (myChar.ActiveitemAll[Orb_Ability])
                    //{
                    //    case 0:
                    //        myChar.ElementStone[(Orb_Ability + 1)] += 20;
                    //        break;
                    //    case 1:
                    //        myChar.ElementStone[(Orb_Ability + 1)] += 30;
                    //        break;
                    //    case 2:
                    //        myChar.ElementStone[(Orb_Ability + 1)] += 50;
                    //        break;
                    //}

                    Destroy(gameObject);
                    if (myChar.Tutorial)
                    {
                        RoomManager.Instance.TutoCheck++;
                    }
                }
            }
            else
            {
                myChar.ElementStone[(Orb_Ability + 1)] += 20;
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (instance.RoomManagerObj.activeSelf)
            {
                if (!RoomManager.Instance.EquipmentRoom)
                {
                    SoundManager.Instance.PlaySfx(3);
                    for (int i = 0; i < myChar.PlayerEquipment.Count; i++)
                    {
                        if (myChar.PlayerEquipment[i] == (Orb_Ability + 1000))
                        {
                            ObtainItemCheck = true;
                        }
                    }
                    if (!ObtainItemCheck)
                    {
                        myChar.PlayerEquipment.Add(Orb_Ability + 1000);
                        myChar.EnthroneEquipment.Add(Orb_Ability + 1000);
                    }

                    myChar.ElementStone[(Orb_Ability + 1)] += 20;
                    //switch (myChar.ActiveitemAll[Orb_Ability])
                    //{
                    //    case 0:
                    //        myChar.ElementStone[(Orb_Ability + 1)] += 20;
                    //        break;
                    //    case 1:
                    //        myChar.ElementStone[(Orb_Ability + 1)] += 30;
                    //        break;
                    //    case 2:
                    //        myChar.ElementStone[(Orb_Ability + 1)] += 50;
                    //        break;
                    //}

                    Destroy(gameObject);
                    if (myChar.Tutorial)
                    {
                        RoomManager.Instance.TutoCheck++;
                    }
                }
            }
            else
            {
                myChar.ElementStone[(Orb_Ability + 1)] += 20;
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (instance.RoomManagerObj.activeSelf)
            {
                if (RoomManager.Instance.EquipmentRoom)
                {
                    instance.Select_Btn.SetActive(true);
                    instance.Jump_Btn.SetActive(false);
                    instance.EquipmentWindow.SetActive(true);
                    instance.GiftBox_Btn.SetActive(false);
                    instance.AD_Btn.SetActive(false);

                    OrbInfo();
                    if (!SoundCheck)
                    {
                        SoundManager.Instance.PlaySfx(18);
                        SoundCheck = true;
                    }
                }
            }
        }


    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (instance.RoomManagerObj.activeSelf)
        {
            if (RoomManager.Instance.EquipmentRoom)
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
    }

    private void OrbInfo()
    {
        GameObject InfoWindow = instance.EquipmentWindow;
        InfoWindow.transform.Find("Status_Image").gameObject.SetActive(false);

        //(myChar.ActiveitemAll[ItemNum - 2000] + 1) +1해주는이유는lv0이아닌 Lv4부터시작하기위함
        InfoWindow.transform.Find("ItemLv_text").GetComponent<Text>().text = "<color=#e4e4e4>Lv" + (myChar.ElementStoneAll[Orb_Ability] + 1) + "</color>";

        OrbInfoText();

        switch (ItemNum)
        {
            case 1000:
                InfoWindow.transform.Find("ItemName_text").GetComponent<Text>().text = "<color=#FF4E45>" + myChar.TextDataMgr.GetTemplate(ItemNum).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                break;
            case 1001:
                InfoWindow.transform.Find("ItemName_text").GetComponent<Text>().text = "<color=#3089E0>" + myChar.TextDataMgr.GetTemplate(ItemNum).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                break;
            case 1002:
                InfoWindow.transform.Find("ItemName_text").GetComponent<Text>().text = "<color=#E1C78B>" + myChar.TextDataMgr.GetTemplate(ItemNum).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                break;
            case 1003:
                InfoWindow.transform.Find("ItemName_text").GetComponent<Text>().text = "<color=#00C050>" + myChar.TextDataMgr.GetTemplate(ItemNum).Content[myChar.LanguageNum].Replace("\\n", "\n") + "</color>";
                break;
        }
    }
    private void OrbInfoText()
    {
        if (myChar.ElementStoneAll[ItemNum - 1000] == 0)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).StatIndex)
            {
                case 27:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1.ToString() + "%";
                    break;
                case 28:
                    AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1.ToString() ;
                    break;
                case 29:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1.ToString() + "%";
                    break;
                case 30:
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv1.ToString() + "%";
                    break;
            }
        }
        if (myChar.ElementStoneAll[ItemNum - 1000] == 1)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).StatIndex)
            {
                case 27:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2.ToString() + "%";
                    break;
                case 28:
                    AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2.ToString() + "%";
                    break;
                case 29:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2.ToString() + "%";
                    break;
                case 30:
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv2.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ElementStoneAll[ItemNum - 1000] == 2)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).StatIndex)
            {
                case 27:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3.ToString() + "%";
                    break;
                case 28:
                    AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3.ToString() + "%";
                    break;
                case 29:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3.ToString() + "%";
                    break;
                case 30:
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv3.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ElementStoneAll[ItemNum - 1000] == 3)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).StatIndex)
            {
                case 27:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4.ToString() + "%";
                    break;
                case 28:
                    AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4.ToString() + "%";
                    break;
                case 29:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4.ToString() + "%";
                    break;
                case 30:
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv4.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ElementStoneAll[ItemNum - 1000] == 4)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).StatIndex)
            {
                case 27:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5.ToString() + "%";
                    break;
                case 28:
                    AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5.ToString() + "%";
                    break;
                case 29:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5.ToString() + "%";
                    break;
                case 30:
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv5.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ElementStoneAll[ItemNum - 1000] == 5)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).StatIndex)
            {
                case 27:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6.ToString() + "%";
                    break;
                case 28:
                    AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6.ToString() + "%";
                    break;
                case 29:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6.ToString() + "%";
                    break;
                case 30:
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv6.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ElementStoneAll[ItemNum - 1000] == 6)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).StatIndex)
            {
                case 27:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7.ToString() + "%";
                    break;
                case 28:
                    AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7.ToString() + "%";
                    break;
                case 29:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7.ToString() + "%";
                    break;
                case 30:
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv7.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ElementStoneAll[ItemNum - 1000] == 7)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).StatIndex)
            {
                case 27:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8.ToString() + "%";
                    break;
                case 28:
                    AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8.ToString() + "%";
                    break;
                case 29:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8.ToString() + "%";
                    break;
                case 30:
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv8.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ElementStoneAll[ItemNum - 1000] == 8)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).StatIndex)
            {
                case 27:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9.ToString() + "%";
                    break;
                case 28:
                    AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9.ToString() + "%";
                    break;
                case 29:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9.ToString() + "%";
                    break;
                case 30:
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv9.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ElementStoneAll[ItemNum - 1000] == 9)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).StatIndex)
            {
                case 27:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10.ToString() + "%";
                    break;
                case 28:
                    AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10.ToString() + "%";
                    break;
                case 29:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10.ToString() + "%";
                    break;
                case 30:
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv10.ToString() + "%";
                    break;
            }
        }
        else if (myChar.ElementStoneAll[ItemNum - 1000] == 10)
        {
            switch (myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).StatIndex)
            {
                case 27:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11.ToString() + "%";
                    break;
                case 28:
                    AddText = myChar.TextDataMgr.GetTemplate(13).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11.ToString() + "%";
                    break;
                case 29:
                    AddText = myChar.TextDataMgr.GetTemplate(0).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11.ToString() + "%";
                    break;
                case 30:
                    AddText = myChar.TextDataMgr.GetTemplate(11).Content[myChar.LanguageNum].Replace("\\n", "\n") + " +" + myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).Lv11.ToString() + "%";
                    break;
            }
        }
        string ItemInfo = myChar.TextDataMgr.GetTemplate(myChar.AntiqueitemDataMgr.GetTemplate(ItemNum).StatIndex).Content[myChar.LanguageNum].Replace("\\n", "\n");

        instance.EquipmentWindow.transform.Find("Info_Text").GetComponent<Text>().text =
            ItemInfo + "\n" + "<color=#00FF00>" + AddText + "</color>";

    }
    //private void Magnetic()
    //{
    //    if (PlayerCheck)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, instance.SelectCharacter.transform.position, Time.deltaTime * 10f);
    //    }
    //}
    private void Magnetic()
    {
        if (isWallCheck)
        {
            isDropCheck = true;
        }
        if (PlayerCheck)
        {
            if (isDropCheck)
            {
                _collider.isTrigger = true;
                _rigidbody.gravityScale = 0f;
                Vector2 dir = PlayerList.transform.position - transform.position;

                //_rigidbody.MovePosition((PlayerList.transform.position) * 0.5f * Time.deltaTime);
                transform.position = Vector2.MoveTowards(transform.position, PlayerList.transform.position, Time.deltaTime * 10f);
            }
        }
        else if (!PlayerCheck)
        {
            if (!myChar.Tutorial)
            {
                _collider.isTrigger = false;
                _rigidbody.gravityScale = 0.5f;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, MagneticRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + DropPosX, transform.position.y + DropPosY), WallcheckRadius);
    }


}
