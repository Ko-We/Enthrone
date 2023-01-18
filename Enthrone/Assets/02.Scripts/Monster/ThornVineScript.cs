using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornVineScript : MonoBehaviour
{
    public int MonsterIndex;
    public bool lastCheck;
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
            col.SendMessage("PlayerHit", GameManager.Instance.UnitDataMgr
                );
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.collider.SendMessage("PlayerHit", GameManager.Instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * MyObject.MyChar.MultiDamage);
        }
    }
    private void setFalseCheck()
    {
        gameObject.SetActive(false);
    }
    private void LastCheck()
    {
        if (lastCheck)
        {
            transform.parent.parent.parent.Find("Natural_GreenEyes(Boss)").GetComponent<BossPatternScript>().VineStartCheck = false;
        }
    }
    public void SFXVineSound()
    {
        SoundManager.Instance.PlaySfx(61);
    }
}
