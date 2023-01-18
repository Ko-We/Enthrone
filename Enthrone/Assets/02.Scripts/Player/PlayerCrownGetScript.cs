using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrownGetScript : MonoBehaviour
{
    MyObject myChar;

    Rigidbody2D _rigdbody;

    public GameObject InfoArrow;
    public GameObject Effect;
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        StartCoroutine(InfoArrowStart());
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().isCrown = true;
            //myChar.SelectLocation.transform.parent.Find("Throne").GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject);


            //if (myChar.Tutorial)
            //{
            //    Destroy(gameObject);
            //    myChar.Finished = true;
            //    RoomManager.Instance.CongEffectStart();
            //    //myChar.Tutorial = false;
            //    GameManager.Instance.MoveLobbyTime(10f);
            //}
            //else if (!myChar.Tutorial)
            //{
            //    Destroy(gameObject);
            //    myChar.Finished = true;
            //    RoomManager.Instance.CongEffectStart();
            //    //myChar.Tutorial = false;
            //    GameManager.Instance.WinPanelOpen(10f);
            //}

        }
        if (col.transform.CompareTag("Ground"))
        {
            Effect.SetActive(true);
        }
    }
    IEnumerator InfoArrowStart()
    {
        yield return new WaitForSeconds(3f);
        InfoArrow.SetActive(true);
    }
}
