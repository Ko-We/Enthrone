using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeScript : MonoBehaviour
{
    public int MonsterIndex;
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
        if (col.gameObject.tag == "Player")
        {
            col.SendMessage("PlayerHit", GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * MyObject.MyChar.Damage);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.collider.SendMessage("PlayerHit", GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * MyObject.MyChar.Damage);
        }
    }
    private void setFalseCheck()
    {
        gameObject.SetActive(false);
    }
}
