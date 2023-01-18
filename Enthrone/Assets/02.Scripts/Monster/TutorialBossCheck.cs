using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBossCheck : MonoBehaviour
{
    MyObject myChar;
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AppearBoss()
    {
        myChar.SelectLocation.transform.parent.Find("Monster").gameObject.SetActive(true);
        RoomManager.Instance.DontTouchBtn_Panel.SetActive(false);
        myChar.BossInfoCheck = true;
        transform.gameObject.SetActive(false);
    }
}
