using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowTargeting : MonoBehaviour
{
    Animator _anim;
    public GameObject Bow;
    public GameObject Character;

    private Vector3 PlayerPos;
    private Transform target;
    private GameObject PlayerList;
    private bool FlipCheck;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetAutoCheck();
        Vector3 distance = PlayerList.transform.position;

        Vector3 dir = distance - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            AttackPosCheck();
            Bow.transform.rotation = Quaternion.AngleAxis((angle), Vector3.forward);
            if (transform.parent.localScale.x > 0)
            {
                Bow.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                Bow.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            Bow.transform.rotation = Quaternion.Euler(0, 0, 0);
            Bow.transform.localScale = new Vector3(1, 1, 1);
            if (FlipCheck)
            {
                FlipCheck = !FlipCheck;

                Character.transform.localScale *= new Vector2(-1,1);
            }
        }

    }

    public void TargetAutoCheck()
    {
        GameObject Player = GameObject.Find("Player");

        for (int i = 0; i < MyObject.MyChar.HeroNum; i++)
        {
            if (Player.transform.GetChild(i).gameObject.activeSelf == true)
            {
                if (Player.transform.GetChild(i).name != "SeasnalShield")
                {
                    target = Player.transform.GetChild(i);
                    PlayerList = target.gameObject;
                }
            }
        }
    }

    public void AttackPosCheck()
    {
        float TargetPosCheck = PlayerList.transform.position.x - transform.position.x;

        if (TargetPosCheck > 0)
        {
            if (transform.parent.transform.localScale.x != 6)
            {                
                if (!FlipCheck)
                {
                    FlipCheck = !FlipCheck;

                    Character.transform.localScale *= new Vector2(-1, 1);
                }
                
            }
        }
        else if (TargetPosCheck < 0)
        {
            
            if (transform.parent.transform.localScale.x != -6)
            {
                if (!FlipCheck)
                {
                    FlipCheck = !FlipCheck;

                    Character.transform.localScale *= new Vector2(-1, 1);
                }
            }
        }
    }
}
