using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_Guardian_bodyScrpt : MonoBehaviour
{
    public GameObject MainObject;
    private Rigidbody2D _rigdbody;
    private Collider2D _collider;

    private void Awake()
    {
        //MainObject = transform.parent.parent.gameObject;
        _rigdbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MainObject.GetComponent<BossPatternScript>().monsterHp <= 0)
        {
            _rigdbody.gravityScale = 0.8f;
            _collider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
