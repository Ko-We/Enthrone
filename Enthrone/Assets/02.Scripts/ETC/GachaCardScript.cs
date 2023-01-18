using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaCardScript : MonoBehaviour
{
    MyObject myChar;
    private Animator _Anim;
    [SerializeField]
    private Image _Img;
    public int CardNum;         //몇번째 카드인지 알려주는 숫자
    public int ItemIndex;
    public bool CardOpenAnim = false;       //애니메이션 여는거
    public bool CardOpenCheck = false;      //카드 완전히 뒤집히고 오픈됨 체크해주는거
    public Button Btn;
    public bool ItemCheck = false;
    private int SoundNum;
    public bool OpenCheck = false;      //스킨은 Grade값이 없어서 처음보일때 무조건 흰색으로 보여주는걸 막아주는함수

    public GameObject Card;
    public GameObject icon;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        _Anim = GetComponent<Animator>();
        _Img = GetComponent<Image>();
        Btn = GetComponent<Button>();
        //Btn.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        CardOpen();

        if (!ItemCheck)
        {
            if (CardOpenCheck)
            {
                LobbiManager.instance.CardOpenCheckList[CardNum] = true;
            }
        }

        Btn.onClick.AddListener(CardOpenAnimCheck);
    }
    private void CardOpen()
    {
        if (CardOpenAnim)
        {
            _Anim.SetBool("Open", true);
        }
        else
        {
            _Anim.SetBool("Open", false);
        }
    }
    private void Draw()
    {
        if (ItemIndex >= 0 && ItemIndex < 1000)
        {
            GradeCheck(myChar.itemDataMgr.GetTemplate(ItemIndex).Grade);
        }
        else if (ItemIndex >= 1000 && ItemIndex < 2000)
        {
            GradeCheck(myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade);
        }
        else if (ItemIndex >= 2000 && ItemIndex < 3000)
        {
            GradeCheck(myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade);
        }
        SoundManager.Instance.PlaySfx(SoundNum);
    }
    public void Open()
    {
        if (!OpenCheck)
        {
            if (ItemIndex >= 0 && ItemIndex < 1000)
            {
                icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + ItemIndex);
                CardGradeCheck(myChar.itemDataMgr.GetTemplate(ItemIndex).Grade);
            }
            else if (ItemIndex >= 1000 && ItemIndex < 2000)
            {
                icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + ItemIndex);
                CardGradeCheck(myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade);
            }
            else if (ItemIndex >= 2000 && ItemIndex < 3000)
            {
                icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_Equipment/Item/" + ItemIndex);
                CardGradeCheck(myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade);
            }
        }
        
    }

    private void CardGradeCheck(int GradeNum)
    {
        if (GradeNum == 0)
        {
            Card.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/05_CardBoard/" + 0);
        }
        else if (GradeNum == 1)
        {
            Card.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/05_CardBoard/" + 1);
        }
        else if (GradeNum == 2)
        {
            Card.GetComponent<Image>().sprite = Resources.Load<Sprite>("03_UI/05_CardBoard/" + 2);
        }
    }
    public void CardOpenAnimCheck()
    {
        CardOpenAnim = true;
    }
    public void CardBtn_Open()
    {
        CardOpenCheck = true;
        //transform.GetComponent<Button>().interactable = false;
    }

    public void OpenCardChange()
    {
        if (ItemIndex >= 0 && ItemIndex < 1000)
        {
            GradeCheck(myChar.itemDataMgr.GetTemplate(ItemIndex).Grade);
        }
        else if (ItemIndex >= 1000 && ItemIndex < 2000)
        {
            GradeCheck(myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade);
        }
        else if (ItemIndex >= 2000 && ItemIndex < 3000)
        {
            GradeCheck(myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade);
        }
    }

    public void OpenCardSoundChange()
    {
        if (ItemIndex >= 0 && ItemIndex < 1000)
        {
            SoundGradeCheck(myChar.itemDataMgr.GetTemplate(ItemIndex).Grade);
        }
        else if (ItemIndex >= 1000 && ItemIndex < 2000)
        {
            SoundGradeCheck(myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade);
        }
        else if (ItemIndex >= 2000 && ItemIndex < 3000)
        {
            SoundGradeCheck(myChar.AntiqueitemDataMgr.GetTemplate(ItemIndex).Grade);
        }
        SoundManager.Instance.PlaySfx(SoundNum);
    }


    private void GradeCheck(int GradeNum)
    {
        if (!ItemCheck)
        {
            if (GradeNum == 0)
            {
                _Img.sprite = Resources.Load<Sprite>("03_UI/05_CardBoard/" + 3);
            }
            else if (GradeNum == 1)
            {
                _Img.sprite = Resources.Load<Sprite>("03_UI/05_CardBoard/" + 4);
            }
            else if (GradeNum == 2)
            {
                _Img.sprite = Resources.Load<Sprite>("03_UI/05_CardBoard/" + 5);
            }
        }
        else
        {
            _Img.sprite = Resources.Load<Sprite>("03_UI/05_CardBoard/" + 5);
        }
    }
    private void SoundGradeCheck(int GradeNum)
    {
        if (!ItemCheck)
        {
            if (GradeNum == 0)
            {
                SoundNum = 46;
            }
            else if (GradeNum == 1)
            {
                SoundNum = 47;
            }
            else if (GradeNum == 2)
            {
                SoundNum = 48;
            }
        }
        else
        {
            SoundNum = 48;
        }
    }
}
