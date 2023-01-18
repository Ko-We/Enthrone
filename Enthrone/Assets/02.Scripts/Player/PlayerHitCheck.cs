using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCheck : MonoBehaviour
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
        if (GameManager.Instance.PlayerSkill[16] >= 1)
        {
            _boxCollider.offset = new Vector2(0, 0.02f);
            _boxCollider.size = new Vector2(0.15f, 0.13f);
        }
        else if (GameManager.Instance.PlayerSkill[16] == 0)
        {
            _boxCollider.offset = new Vector2(0, 0);
            _boxCollider.size = new Vector2(0.15f, 0.17f);
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            transform.parent.GetComponent<PlayerController>().PlayerHit(1);
            //col.collider.GetComponent<PlayerController>().PlayerHit(1);
        }
    }
}
