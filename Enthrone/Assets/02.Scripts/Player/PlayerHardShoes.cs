using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHardShoes : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.PlayerSkill[16] >= 1)
        //{
        //    _boxCollider.enabled = true;
        //}
        //else if (GameManager.Instance.PlayerSkill[16] == 0)
        //{
        //    _boxCollider.enabled = false;
        //}
    }
    

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            Debug.Log(11);
            transform.parent.GetComponent<PlayerController>().amountOfJumpsLeft = transform.parent.GetComponent<PlayerController>().amountOfJumps;
        }
    }
}
