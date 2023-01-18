using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBossHeadReturnScript : MonoBehaviour
{
    public GameObject Head;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Head.GetComponent<BossPatternScript>().Crush)
        {
            transform.position = Head.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "CrushHead")
        {
            col.transform.parent.gameObject.GetComponent<BossPatternScript>().Crush = false;
            col.transform.parent.gameObject.GetComponent<BossPatternScript>().HeadCrushCheck = false;
            col.transform.parent.transform.parent.GetComponent<Animator>().enabled = true;
        }
    }
}
