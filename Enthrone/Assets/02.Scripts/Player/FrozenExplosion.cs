using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenExplosion : MonoBehaviour
{
    public float SlowTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 0.4f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            col.SendMessage("IndividualSlowCheck", SlowTime);
            //if (col.gameObject.GetComponent<MonsterScript>())
            //{
            //    //col.gameObject.GetComponent<MonsterScript>().Individual_Slow = true;
                
            //    //col.gameObject.GetComponent<MonsterScript>().IndividualSlowCheck(SlowTime);
            //}
            //else
            //{
            //    Debug.Log("SlowNull");
            //}
            
        }
    }
    
}
