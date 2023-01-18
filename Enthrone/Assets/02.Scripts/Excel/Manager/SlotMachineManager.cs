using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineManager : MonoBehaviour
{
    /********************************** 싱 글 톤 *******************************************/
    private static SlotMachineManager slot_Manager = null;
    public static SlotMachineManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(SlotMachineManager)) as SlotMachineManager;
                if (Instance == null)
                {
                    GameObject obj = new GameObject("SlotMachineManager");
                    instance = obj.AddComponent(typeof(SlotMachineManager)) as SlotMachineManager;
                }
            }
            return instance;
        }
    }
    private static SlotMachineManager instance;

    /*************************************************************************************/

    MyObject myChar;
    public Animator _anim;

    public GameObject NotTouchPanel;
    public GameObject Slot_Img;
    [SerializeField]
    bool OnItemCheck = false;
    public List<bool> RouletteOn = new List<bool>();
    public GameObject[] SlotItemObject;
    public GameObject[] Slot;
    public GameObject[] SlotClosePanel = new GameObject[2];
    public GameObject[] ItemClosePanel = new GameObject[2];
    public GameObject Coin_Anim_Img;

    public Sprite[] ItemSprite;
    [System.Serializable]
    public class DisplayItemSlot
    {
        public List<Image> SlotSprite = new List<Image>();
    }
    public DisplayItemSlot[] DisplayItemSlots;

    public Image DisplayResultImage;

    public List<int> StartList = new List<int>();
    public List<int> ResultIndexList = new List<int>();
    public List<Image> OnItem = new List<Image>();
    public List<GameObject> GetItemEffect = new List<GameObject>();
    public List<Sprite> Tests = new List<Sprite>();
    int ItemCnt = 3;

    public bool WindowOpenCheck;
    // Start is called before the first frame update
    void Start()
    {
        
        _anim = GetComponent<Animator>();
        Coin_Anim_Img = GameObject.Find("Coin_Img");
        Coin_Anim_Img.GetComponent<Animator>().enabled = false;
    }
    private void Awake()
    {
        myChar = MyObject.MyChar;
    }
    // Update is called once per frame
    void Update()
    {

        if (myChar.EquipmentActive.Count != 0)
        {

        }
        if (!RouletteOn[0] && !RouletteOn[1] && !RouletteOn[2])
        {
            NotTouchPanel.SetActive(false);
            for (int i = 0; i < myChar.MachinSlotCnt; i++)
            {
                if (OnItemCheck)
                {
                    ResultIndexList[i] = ((int)SlotItemObject[i].transform.localPosition.y / 240 + 2);
                    //myChar.LobbyMachineEquipmentNum[i] = SlotItemObject[0].transform.GetChild(ResultIndexList[0] - 1).GetComponent<SlotItemCheckScript>().ItemNum;
                    OnItem[i].sprite = DisplayItemSlots[i].SlotSprite[ResultIndexList[i] - 1].sprite;
                    GetItemEffect[i].GetComponent<Animator>().enabled = true;

                    if (myChar.MachinSlotCnt >= 1)
                    {
                        if (i == 0)
                        {
                            myChar.LobbyMachineEquipmentNum[i] = ResultIndexList[i] - 2;        //위아래로 1개씩 추가해서 -2넣어야함
                            LobbiManager.instance.SlotItem[i].enabled = true;
                        }
                    }
                    if (myChar.MachinSlotCnt >= 2)
                    {
                        if (i == 1)
                        {
                            myChar.LobbyMachineEquipmentNum[i] = ResultIndexList[i] - 1;        //Activeitem은 0번이 NoItem이라서 -1만줌
                            LobbiManager.instance.SlotItem[i].enabled = true;
                        }
                    }
                    if (myChar.MachinSlotCnt >= 3)
                    {
                        if (i == 2)
                        {
                            myChar.LobbyMachineEquipmentNum[i] = ResultIndexList[i] - 1;        //elementalstone은 0번이 기본공격이라서 -1만줌
                            LobbiManager.instance.SlotItem[i].enabled = true;
                        }
                    }
                    myChar.SaveLobbyMachineEquipmentNum();
                }
            }
            OnItemCheck = false;
        }
        else
        {
            NotTouchPanel.SetActive(true);
        }
        ClosePanelManger();


    }
    public void StartAD()
    {
    }
    public void StartSlot(int Check)
    {
        if (Check == 1 && myChar.SlotCoin > 0)
        {
            FirebaseManager.firebaseManager.LobbyRoulettePay("RoulletteCoin", 1);
            Coin_Anim_Img.GetComponent<Animator>().enabled = true;
            myChar.SlotActiveCheck = true;

            ResultIndexList.Clear();
            if (!RouletteOn[0] && !RouletteOn[1] && !RouletteOn[2])
            {
                //Debug.Log(Slot.Length + " / " + myChar.MachinSlotCnt);
                for (int i = 0; i < myChar.MachinSlotCnt * Slot.Length; i++)
                {
                    StartList.Add(i);
                }
                for (int i = 0; i < Slot.Length; i++)
                {
                    for (int j = 0; j < myChar.MachinSlotCnt; j++)
                    {
                        int randomIndex = Random.Range(0, StartList.Count);
                        if (i == 0 && j == 1 || i == 1 && j == 0 || i == 2 && j == 2)
                        {
                            //당첨된 스킬 인덱스                            
                            ResultIndexList.Add(StartList[randomIndex]);
                        }
                        StartList.RemoveAt(randomIndex);
                    }
                }
            }
            for (int i = 0; i < myChar.MachinSlotCnt; i++)
            {
                StartCoroutine(StartSlot1(i));
            }
            if (Check == 1)
            {
                myChar.SlotCoin--;

                myChar.SaveSlotCoin();
            }
            if (Check == 2)
            {
                myChar.Gem--;

                myChar.SaveGem();
            }
        }
        else if(Check == 0)
        {
            FirebaseManager.firebaseManager.LobbyRoulettePay("AD", 0);
            Coin_Anim_Img.GetComponent<Animator>().enabled = true;
            myChar.SlotActiveCheck = true;

            ResultIndexList.Clear();
            if (!RouletteOn[0] && !RouletteOn[1] && !RouletteOn[2])
            {
                //Debug.Log(Slot.Length + " / " + myChar.MachinSlotCnt);
                for (int i = 0; i < myChar.MachinSlotCnt * Slot.Length; i++)
                {
                    StartList.Add(i);
                }
                for (int i = 0; i < Slot.Length; i++)
                {
                    for (int j = 0; j < myChar.MachinSlotCnt; j++)
                    {
                        int randomIndex = Random.Range(0, StartList.Count);
                        if (i == 0 && j == 1 || i == 1 && j == 0 || i == 2 && j == 2)
                        {
                            //당첨된 스킬 인덱스                            
                            ResultIndexList.Add(StartList[randomIndex]);
                        }
                        StartList.RemoveAt(randomIndex);
                    }
                }
            }
            for (int i = 0; i < myChar.MachinSlotCnt; i++)
            {
                StartCoroutine(StartSlot1(i));
            }
            myChar.ADSlotStartTime = UnbiasedTime.Instance.Now().AddSeconds(300);
            myChar.ADSlotCheck = true;

            myChar.SaveADSlotStartTime();
            myChar.SaveADSlotCheck();
        }
        myChar.SaveSlotActiveCheck();
    }
    private void ClosePanelManger()
    {
        if (myChar.MachinSlotCnt == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                //ItemClosePanel[i].SetActive(true);
                SlotClosePanel[i].SetActive(true);
            }
        }
        else if (myChar.MachinSlotCnt == 2)
        {
            //ItemClosePanel[0].SetActive(false);
            SlotClosePanel[0].SetActive(false);

            //ItemClosePanel[1].SetActive(true);
            SlotClosePanel[1].SetActive(true);
        }
        else if (myChar.MachinSlotCnt == 3)
        {
            for (int i = 0; i < 2; i++)
            {
                //ItemClosePanel[i].SetActive(false);
                SlotClosePanel[i].SetActive(false);
            }
        }
    }

    IEnumerator StartSlot1(int SlotIndex)
    {
        OnItemCheck = true;
        //for (int i = 0; i < SlotIndex; i++)
        //{
        //    RouletteOn[i] = true;
        //}
        switch (SlotIndex)
        {
            case 0:
                RouletteOn[0] = true;
                break;
            case 1:
                RouletteOn[1] = true;
                break;
            case 2:
                RouletteOn[2] = true;
                break;
        }
        //RouletteOn[0] = true;
        //RouletteOn[1] = true;
        //RouletteOn[2] = true;

        int RotationAdd = Random.Range(1, 5);       //슬롯머신 돌아가는 회전량
        int Check = 17 * (RotationAdd) * 2;
        for (int i = 0; i < Check; i++)
        {
            int SaveCheck = Check;
            SlotItemObject[SlotIndex].transform.localPosition -= new Vector3(0, 125f, 0);
            if (SlotItemObject[SlotIndex].transform.localPosition.y < 0f)
            {
                SlotItemObject[SlotIndex].transform.localPosition += new Vector3(0, ((DisplayItemSlots[SlotIndex].SlotSprite.Count - 3) * 250f), 0);
            }
            if (i == SaveCheck - 1)
            {
                //RouletteOn[SlotIndex] = false;
                //if (SlotIndex == 0)
                //{
                //    RouletteOn[1] = false;
                //    RouletteOn[2] = false;
                //}
                //else if (SlotIndex == 1)
                //{
                //    RouletteOn[2] = false;
                //}
                switch (SlotIndex)
                {
                    case 0:
                        RouletteOn[0] = false;
                        break;
                    case 1:
                        RouletteOn[1] = false;
                        break;
                    case 2:
                        RouletteOn[2] = false;
                        break;
                }
            }
            yield return new WaitForSeconds(0.02f);
        }
        //for (int i = 0; i < ItemCnt; i++)
        //{
        //    Slot[i].interactable = true;
        //}
    }
    public void SlotOpenCheck()
    {
        WindowOpenCheck = true;
        if (WindowOpenCheck)
        {
            if (myChar == null)
            {
                myChar = MyObject.MyChar;
            }


            if (myChar.MachinSlotCnt >= 3)
            {
                //1번슬롯 보유한아이템을 보여주는 코드                        
                if (myChar.ElementStoneAll.Length != SlotItemObject[2].transform.childCount)
                {
                    //ResultIndexList.Clear();
                    DisplayItemSlots[2].SlotSprite.Clear();
                    if (SlotItemObject[2].transform.childCount > 0)
                    {
                        for (int i = 0; i < SlotItemObject[2].transform.childCount; i++)
                        {
                            Destroy(SlotItemObject[2].transform.GetChild(i).gameObject);
                        }

                    }
                    for (int i = 0; i < myChar.ElementStoneActive.Count; i++)
                    {
                        //if문이 있는 이유는 맨처음아이템 앞이랑 맨마지막 아이템 뒤에 시작과 끝아이템을 노출해서 보여주기위함
                        if (i == 0)
                        {
                            GameObject EquipmentSlotFirst = Instantiate(Slot_Img, SlotItemObject[2].transform.position, Quaternion.identity);
                            EquipmentSlotFirst.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + myChar.ElementStoneActive[myChar.ElementStoneActive.Count - 1].ToString());
                            EquipmentSlotFirst.GetComponent<SlotItemCheckScript>().ItemNum = myChar.ElementStoneActive[i];
                            EquipmentSlotFirst.transform.parent = SlotItemObject[2].transform;
                            EquipmentSlotFirst.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            DisplayItemSlots[2].SlotSprite.Add(EquipmentSlotFirst.GetComponent<Image>());
                        }

                        GameObject EquipmentSlot = Instantiate(Slot_Img, SlotItemObject[2].transform.position, Quaternion.identity);
                        EquipmentSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + myChar.ElementStoneActive[i].ToString());
                        EquipmentSlot.GetComponent<SlotItemCheckScript>().ItemNum = myChar.ElementStoneActive[i];
                        EquipmentSlot.transform.parent = SlotItemObject[2].transform;
                        EquipmentSlot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        DisplayItemSlots[2].SlotSprite.Add(EquipmentSlot.GetComponent<Image>());

                        if (i == (myChar.ElementStoneActive.Count - 1))
                        {
                            GameObject EquipmentSlotFirst = Instantiate(Slot_Img, SlotItemObject[2].transform.position, Quaternion.identity);
                            EquipmentSlotFirst.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + myChar.ElementStoneActive[0].ToString());
                            EquipmentSlotFirst.GetComponent<SlotItemCheckScript>().ItemNum = myChar.ElementStoneActive[i];
                            EquipmentSlotFirst.transform.parent = SlotItemObject[2].transform;
                            EquipmentSlotFirst.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            DisplayItemSlots[2].SlotSprite.Add(EquipmentSlotFirst.GetComponent<Image>());
                        }
                    }
                }
            }
            if (myChar.MachinSlotCnt >= 2)
            {
                //1번슬롯 보유한아이템을 보여주는 코드                        
                if (myChar.ActiveitemActive.Count != SlotItemObject[1].transform.childCount)
                {
                    //ResultIndexList.Clear();
                    DisplayItemSlots[1].SlotSprite.Clear();
                    if (SlotItemObject[1].transform.childCount > 0)
                    {
                        for (int i = 0; i < SlotItemObject[1].transform.childCount; i++)
                        {
                            Destroy(SlotItemObject[1].transform.GetChild(i).gameObject);
                        }

                    }
                    for (int i = 0; i < myChar.ActiveitemActive.Count; i++)
                    {
                        //if문이 있는 이유는 맨처음아이템 앞이랑 맨마지막 아이템 뒤에 시작과 끝아이템을 노출해서 보여주기위함
                        if (i == 0)
                        {
                            GameObject EquipmentSlotFirst = Instantiate(Slot_Img, SlotItemObject[1].transform.position, Quaternion.identity);
                            EquipmentSlotFirst.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + myChar.ActiveitemActive[myChar.ActiveitemActive.Count - 1].ToString());
                            EquipmentSlotFirst.GetComponent<SlotItemCheckScript>().ItemNum = myChar.ActiveitemActive[i];
                            EquipmentSlotFirst.transform.parent = SlotItemObject[1].transform;
                            EquipmentSlotFirst.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            DisplayItemSlots[1].SlotSprite.Add(EquipmentSlotFirst.GetComponent<Image>());
                        }

                        GameObject EquipmentSlot = Instantiate(Slot_Img, SlotItemObject[1].transform.position, Quaternion.identity);
                        EquipmentSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + myChar.ActiveitemActive[i].ToString());
                        EquipmentSlot.GetComponent<SlotItemCheckScript>().ItemNum = myChar.ActiveitemActive[i];
                        EquipmentSlot.transform.parent = SlotItemObject[1].transform;
                        EquipmentSlot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        DisplayItemSlots[1].SlotSprite.Add(EquipmentSlot.GetComponent<Image>());

                        if (i == (myChar.ActiveitemActive.Count - 1))
                        {
                            GameObject EquipmentSlotFirst = Instantiate(Slot_Img, SlotItemObject[1].transform.position, Quaternion.identity);
                            EquipmentSlotFirst.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + myChar.ActiveitemActive[0].ToString());
                            EquipmentSlotFirst.GetComponent<SlotItemCheckScript>().ItemNum = myChar.ActiveitemActive[i];
                            EquipmentSlotFirst.transform.parent = SlotItemObject[1].transform;
                            EquipmentSlotFirst.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            DisplayItemSlots[1].SlotSprite.Add(EquipmentSlotFirst.GetComponent<Image>());
                        }
                    }
                }
            }
            if (myChar.MachinSlotCnt >= 1)
            {
                //1번슬롯 보유한아이템을 보여주는 코드                        
                if (myChar.EquipmentActive.Count != SlotItemObject[0].transform.childCount)
                {
                    //ResultIndexList.Clear();
                    DisplayItemSlots[0].SlotSprite.Clear();
                    if (SlotItemObject[0].transform.childCount > 0)
                    {
                        for (int i = 0; i < SlotItemObject[0].transform.childCount; i++)
                        {
                            Destroy(SlotItemObject[0].transform.GetChild(i).gameObject);
                        }

                    }
                    for (int i = 0; i < myChar.EquipmentActive.Count; i++)
                    {
                        //if문이 있는 이유는 맨처음아이템 앞이랑 맨마지막 아이템 뒤에 시작과 끝아이템을 노출해서 보여주기위함
                        if (i == 0)
                        {
                            GameObject EquipmentSlotFirst = Instantiate(Slot_Img, SlotItemObject[0].transform.position, Quaternion.identity);
                            EquipmentSlotFirst.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.EquipmentActive[myChar.EquipmentActive.Count - 1].ToString());
                            EquipmentSlotFirst.GetComponent<SlotItemCheckScript>().ItemNum = myChar.EquipmentActive[i];
                            EquipmentSlotFirst.transform.parent = SlotItemObject[0].transform;
                            EquipmentSlotFirst.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            DisplayItemSlots[0].SlotSprite.Add(EquipmentSlotFirst.GetComponent<Image>());
                        }

                        GameObject EquipmentSlot = Instantiate(Slot_Img, SlotItemObject[0].transform.position, Quaternion.identity);
                        EquipmentSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.EquipmentActive[i].ToString());
                        EquipmentSlot.GetComponent<SlotItemCheckScript>().ItemNum = myChar.EquipmentActive[i];
                        EquipmentSlot.transform.parent = SlotItemObject[0].transform;
                        EquipmentSlot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        DisplayItemSlots[0].SlotSprite.Add(EquipmentSlot.GetComponent<Image>());

                        if (i == (myChar.EquipmentActive.Count - 1))
                        {
                            GameObject EquipmentSlotFirst = Instantiate(Slot_Img, SlotItemObject[0].transform.position, Quaternion.identity);
                            EquipmentSlotFirst.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + myChar.EquipmentActive[0].ToString());
                            EquipmentSlotFirst.GetComponent<SlotItemCheckScript>().ItemNum = myChar.EquipmentActive[i];
                            EquipmentSlotFirst.transform.parent = SlotItemObject[0].transform;
                            EquipmentSlotFirst.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            DisplayItemSlots[0].SlotSprite.Add(EquipmentSlotFirst.GetComponent<Image>());
                        }
                    }
                }
            }
        }
    }
    public void SlotCloseCheck()
    {
        WindowOpenCheck = false;
    }

}
