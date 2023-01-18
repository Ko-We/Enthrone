using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBounceProjectile : MonoBehaviour
{
    MyObject myChar;
    //[SerializeField][Range(0, 50f)] float speed = 10f;
    public float speed;

    private Rigidbody2D _rigidbody;
    private GameObject MonsterProjectile;
    [SerializeField]
    private float randomX, randomY;
    public float Damage;
    public GameObject HitEffect;
    public GameObject Projectile;
    public Transform AttackPos;

    public Transform target;
    
    public GameObject PlayerList;

    private Vector3 startPos;
    private Vector3 crushPos;
    private int bounceCnt;
    private int[] randomPos = {-1, 1};
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        bounceCnt = 6;
        _rigidbody = GetComponent<Rigidbody2D>();
        MonsterProjectile = GameObject.Find("MonsterProjectileManager");

        //int CheckNum = Random.Range(0, 2);
        //randomX = randomPos[CheckNum];
        //int CheckNum2 = Random.Range(0, 2);
        //randomY = randomPos[CheckNum2];

        //Vector2 dir = new Vector2(randomX, randomY).normalized;
        TargetAutoCheck();
        //transform.Translate(Vector2.up * speed * myChar.SlowSpeed * Time.deltaTime);
        Vector3 distance = PlayerList.transform.position;

        Vector3 dir = distance - AttackPos.position;
        //dir = dir.normalized;         //테스트용

        _rigidbody.AddForce(dir * speed * Time.fixedDeltaTime);
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {        
        if (bounceCnt <= 0)
        {
            Destroy(gameObject);
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        bounceCnt--;
        ContactPoint2D contact = col.contacts[0];
        
        crushPos = contact.point;

        if (col.gameObject.tag == "Ground")
        {
            GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
            Destroy(Effect, 2f);
            SoundManager.Instance.PlaySfx(63);
            if (MyObject.MyChar.Chapter >= 2)
            {
                FireProjectile();
            }            
            startPos = crushPos;
            //Destroy(gameObject);
        }
        //if (col.gameObject.tag == "Player" || col.gameObject.tag == "Ground")
        //{
        //    if (col.gameObject.tag == "Player")
        //    {
        //        //col.transform.GetComponent<PlayerController>().PlayerHit(1);
        //        //col.SendMessage("PlayerHit", 1f);
        //        col.transform.SendMessage("PlayerHit", Damage);
        //        GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
        //    }
        //    else if (col.gameObject.tag == "Ground")
        //    {
        //        GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
        //        Destroy(Effect, 2f);
        //        FireProjectile();
        //        startPos = crushPos;
        //        //Destroy(gameObject);
        //    }
        //}
    }
    private void FireProjectile()
    {
        Vector3 dir = crushPos - startPos;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject projectile = Instantiate(Projectile, AttackPos.position, Quaternion.identity);
        projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle + 90), Vector3.forward);
        projectile.transform.parent = MonsterProjectile.transform;
        projectile.GetComponent<MonsterProjectile>().Damage = Damage;
        GameObject projectileR = Instantiate(Projectile, AttackPos.position, Quaternion.identity);
        projectileR.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle + 90) + 45, Vector3.forward);
        projectileR.transform.parent = MonsterProjectile.transform;
        projectileR.GetComponent<MonsterProjectile>().Damage = Damage;
        GameObject projectileL = Instantiate(Projectile, AttackPos.position, Quaternion.identity);
        projectileL.GetComponent<Transform>().rotation = Quaternion.AngleAxis((angle + 90) - 45, Vector3.forward);
        projectileL.transform.parent = MonsterProjectile.transform;
        projectileL.GetComponent<MonsterProjectile>().Damage = Damage;
    }
}
