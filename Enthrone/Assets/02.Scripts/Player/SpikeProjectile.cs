using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeProjectile : MonoBehaviour
{
    MyObject myChar;
    GameManager instance;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;

    public Transform targetPos;
    public Vector3 distance;
    public Vector3 NewDir;
    public Vector3 AttackPoint;
    public Vector2 Boltdir;

    public GameObject HitEffect;
    public GameObject HitMonster;

    public float BoltSpeed = 5f;
    public float Damage;


    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * BoltSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monster" || col.gameObject.tag == "Ground")
        {
            if (col.gameObject.tag == "Monster")
            {
                if (col.gameObject != HitMonster)
                {
                    col.SendMessage("AttackHit", Damage);
                    //GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                    //Destroy(Effect, 2f);
                    Destroy(gameObject);
                }                
            }
            else if (col.gameObject.tag == "Ground")
            {
                //GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                //Destroy(Effect, 2f);
                Destroy(gameObject);
            }
        }
    }
}
