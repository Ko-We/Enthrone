using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnthroneSitCheckScript : MonoBehaviour
{
    MyObject myChar;

    Rigidbody2D _rigdbody;

    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            if (col.gameObject.GetComponent<PlayerController>().isCrown)
            {
                if (myChar.Tutorial)
                {
                    myChar.Finished = true;
                    myChar.CrownGetBgm = true;
                    RoomManager.Instance.CongEffectStart();
                    //myChar.Tutorial = false;
                    GameManager.Instance.OpenEndPanel(10f);
                    GameManager.Instance.TutorialSkip_Btn.SetActive(true);
                }
                else if (!myChar.Tutorial)
                {
                    myChar.Finished = true;
                    myChar.CrownGetBgm = true;
                    RoomManager.Instance.CongEffectStart();
                    //myChar.Tutorial = false;
                    GameManager.Instance.WinPanelOpen(10f);
                    GameManager.Instance.ClearSkip_Btn.SetActive(true);
                }
            }
        }
    }
}
