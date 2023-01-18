using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern_WayCheck : MonoBehaviour
{
    public GameObject Boss;
    public GameObject NaturalWay;
    public bool NaturalCheck;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "WayPoint")
        {
            Boss.GetComponent<BossPatternScript>().movingCheck = true;

            if (NaturalCheck)
            {
                if (!NaturalWay.activeSelf)
                {
                    NaturalWay.SetActive(true);
                }
            }
        }
    }
}
