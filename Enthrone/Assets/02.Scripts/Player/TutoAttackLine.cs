using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoAttackLine : MonoBehaviour
{
    MyObject myChar;
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;

        transform.localScale = new Vector3(0.9f + (myChar.Range * 0.05f), 0.9f + (myChar.Range * 0.05f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(GameManager.Instance.SelectCharacter.transform.position.x, GameManager.Instance.SelectCharacter.transform.position.y - 0.2f);
    }
}
