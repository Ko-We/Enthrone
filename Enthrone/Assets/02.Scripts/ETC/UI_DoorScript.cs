using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DoorScript : MonoBehaviour
{

    MyObject myChar;
    public GameObject BossGfitBox;
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GiftBoxCreate()
    {
        GameObject GiftBox = Instantiate(BossGfitBox, myChar.SelectLocation.transform.position, Quaternion.identity);
        GiftBox.transform.parent = myChar.SelectLocation.transform.GetChild(0).transform;
        GiftBox.transform.localScale = new Vector2(1, 1);
    }

    private void OffPanel()
    {
        transform.gameObject.SetActive(false);
        RoomManager.Instance.OffTouchPanel();
        if (myChar.TutorialNum == 1)
        {
            GameManager.Instance.Player.transform.Find("Tutorial_AttackLine").gameObject.SetActive(true);
        }
        else if (myChar.TutorialNum == 5)
        {
            myChar.BossCheck = true;
        }
    }

    private void NextMap()
    {
        if (!myChar.Tutorial)
        {
            Debug.Log(11);
            RoomManager.Instance.NextStage();
            Time.timeScale = 0.0f;
        }
        else if (myChar.Tutorial)
        {
            RoomManager.Instance.Tutorial_Stage();
        }
    }
    private void Tutorial_Info()
    {
        if (myChar.Tutorial)
        {
            GameManager.Instance.Tutorial_Info.SetActive(true);
            GameManager.Instance.Tutorial_Anim.SetActive(true);
            for (int i = 0; i < GameManager.Instance.Tutorial_Anim.transform.childCount; i++)
            {
                if (i != myChar.TutorialNum)
                {
                    GameManager.Instance.Tutorial_Anim.transform.GetChild(i).gameObject.SetActive(false);
                }
                else if (i == myChar.TutorialNum)
                {
                    GameManager.Instance.Tutorial_Anim.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }

}
