using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GiftBoxCreateScript : MonoBehaviour
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
        if (!myChar.Tutorial)
        {
            Vector3 DropPos = myChar.SelectLocation.transform.position + new Vector3(0, -2, 0);
            GameObject GiftBox = Instantiate(BossGfitBox, DropPos, Quaternion.identity);
            GiftBox.transform.parent = myChar.SelectLocation.transform.GetChild(0).transform;
            GiftBox.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            Vector3 DropPos = myChar.SelectLocation.transform.position;
            GameObject GiftBox = Instantiate(BossGfitBox, DropPos, Quaternion.identity);
            GiftBox.transform.parent = myChar.SelectLocation.transform.GetChild(0).transform;
            GiftBox.transform.localScale = new Vector2(1, 1);
        }
    }

    private void OffPanel()
    {
        if (SceneManager.GetActiveScene().name == "StageScene")
        {
            transform.gameObject.SetActive(false);
            RoomManager.Instance.OffTouchPanel();
        }
        if (myChar.Tutorial)
        {
            if (myChar.TutorialNum == 1)
            {
                GameManager.Instance.Player.transform.Find("Tutorial_AttackLine").gameObject.SetActive(true);
            }
            else if (myChar.TutorialNum == 3)
            {
                myChar.TutoThreeCheck = true;
            }
            else if (myChar.TutorialNum == 5)
            {
                myChar.BossCheck = true;
            }
        }
    }
    private void AstarReset()
    {
        if (!RoomManager.Instance.EquipmentRoom && !RoomManager.Instance.AntiqueRoom)
        {
            var graphtoscan = AstarPath.active.data.gridGraph;
            AstarPath.active.Scan(graphtoscan);
        }
    }

    private void NextMap()
    {
        if (!myChar.Tutorial)
        {
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

            if (myChar.TutorialNum < 4)
            {
                for (int i = 0; i < GameManager.Instance.Tutorial_Anim.transform.childCount; i++)
                {
                    GameManager.Instance.Tutorial_Anim.transform.GetChild(i).gameObject.SetActive(false);
                }
                if (Time.timeScale < 1)
                {
                    GameManager.Instance.Tutorial_Anim.SetActive(true);
                    GameManager.Instance.Tutorial_Anim.transform.GetChild(0).gameObject.SetActive(true);
                }
                
            }
            else
            {
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
    public void BtnSound(int Num)
    {
        SoundManager.Instance.PlaySfx(Num);
    }

    private void DoorSound()
    {
        SoundManager.Instance.PlaySfx(13);
    }
}
