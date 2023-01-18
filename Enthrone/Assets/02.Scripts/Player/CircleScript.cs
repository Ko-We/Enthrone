using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    private GameObject PlayerList;

    public Transform target;
    public GameObject Planet;
    public float speed;
    public bool Center;

    private void Update()
    {
        OrbitAround();
        TargetAutoCheck();
        if (Center)
        {
            transform.position = new Vector2(GameManager.Instance.SelectCharacter.transform.position.x, GameManager.Instance.SelectCharacter.transform.position.y - 0.2f);
            //transform.position = Planet.transform.position;
        }
    }

    void OrbitAround()
    {
        // transform.RotateAround(Planet.transform.position, Vector2.zero, speed * Time.deltaTime);
        if (!Center)
        {
            transform.up = Vector2.up;
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
                    Planet = target.gameObject;
                }

            }
        }
    }
}
