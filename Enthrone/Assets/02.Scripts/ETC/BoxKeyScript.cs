using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxKeyScript : MonoBehaviour
{
    MyObject myChar;

    Rigidbody2D _rigdbody;
    BoxCollider2D _collider;
    SpriteRenderer _sprite;


    public GameObject GiftBoxUI;

    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        _collider = GetComponent<BoxCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            Destroy(gameObject, 10f);
            _collider.enabled = false;
            _sprite.enabled = false;
            myChar.Key++;

            if (myChar.Tutorial)
            {
                GameManager.Instance.GiftBox_InfoUI.SetActive(true);
            }
            else
            {
                RoomManager.Instance.BossGiftBoxDrop();
            }

            //GameManager.Instance.GiftBox_InfoUI.SetActive(true);
            //튜토리얼제외 상자안먹어도 키만먹으면 왕좌활성화하게만드는방법
            //if (myChar.Tutorial)
            //{
            //    GameManager.Instance.GiftBox_InfoUI.SetActive(true);
            //}
            //else
            //{
            //    RoomManager.Instance.BossGiftBoxDrop();
            //    StartCoroutine(ThroneActive());
            //}
        }
    }

    IEnumerator ThroneActive()
    {
        yield return new WaitForSeconds(4f);
        myChar.SelectLocation.transform.parent.Find("Throne").GetChild(0).gameObject.SetActive(true);
    }
}
