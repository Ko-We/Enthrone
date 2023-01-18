using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSubScript : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = gameObject.transform.parent.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            gameObject.transform.parent.GetComponent<ItemScript>().isWallCheck = true;
        }
        else
        {
            gameObject.transform.parent.GetComponent<ItemScript>().isWallCheck = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            gameObject.transform.parent.GetComponent<ItemScript>().isWallCheck = true;
        }
        else
        {
            gameObject.transform.parent.GetComponent<ItemScript>().isWallCheck = false;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            gameObject.transform.parent.GetComponent<ItemScript>().isWallCheck = true;
        }
        else
        {
            gameObject.transform.parent.GetComponent<ItemScript>().isWallCheck = false;
        }
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            gameObject.transform.parent.GetComponent<ItemScript>().isWallCheck = true;
        }
        else
        {
            gameObject.transform.parent.GetComponent<ItemScript>().isWallCheck = false;
        }
    }
}
