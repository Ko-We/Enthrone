using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBounceProjectile2 : MonoBehaviour
{
    MyObject myChar;
    GameManager instance;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    [SerializeField]
    private Collider2D _WallonlyCollider2D;
    private GameObject MonsterProjectileManager;
    private GameObject TargetPos;
    private GameObject Player;

    public bool SlowShot;
    public bool BounceShot;
    private bool CoroutineCheck = false;

    public Transform targetPos;
    public Vector3 distance;
    public Vector3 NewDir;
    public Vector3 AttackPoint;
    public Vector2 Boltdir;
    public Vector3 lastVelocity;

    public GameObject HitEffect;

    public float BoltSpeed = 5f;
    public float Damage;

    public int WallbounceCnt;

    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        MonsterProjectileManager = GameObject.Find("MonsterProjectileManager");

        _rigidbody.velocity = transform.up * BoltSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = _rigidbody.velocity;

        Player = GameObject.Find("Player");
        for (int i = 0; i < myChar.HeroNum; i++)
        {
            if (Player.transform.GetChild(i).gameObject.activeSelf == true)
            {
                TargetPos = Player.transform.GetChild(i).gameObject;
            }
        }
        if (WallbounceCnt <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (myChar.Reflection)
            {
                Quaternion originalRot = transform.rotation;

                Vector3 originalRotVec3 = originalRot.eulerAngles;
                Vector3 newRotation = originalRotVec3 + new Vector3(0, 0, 180);

                transform.rotation = Quaternion.Euler(newRotation);
            }
            //col.transform.GetComponent<PlayerController>().PlayerHit(1);
            if (!myChar.Reflection)
            {
                col.SendMessage("PlayerHit", Damage);
                if (SlowShot)
                {
                    col.gameObject.GetComponent<PlayerController>().SlowUse(3f, 0.5f);
                    //if (myChar.Chapter < 2)
                    //{
                    //    col.gameObject.GetComponent<PlayerController>().SlowUse(2f, 0.5f);
                    //}
                    //else if (myChar.Chapter >= 2)
                    //{
                    //    col.gameObject.GetComponent<PlayerController>().SlowUse(2f, 0.25f);
                    //}
                }
                //col.gameObject.GetComponent<PlayerController>().StartCoroutine(SlowEffect(1f));

                Destroy(gameObject);
            }
            GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
            Effect.transform.parent = MonsterProjectileManager.transform;
            if (Effect.GetComponent<ExplosionDamage>())
            {
                Effect.GetComponent<ExplosionDamage>().Damage = Damage;
            }
            Destroy(Effect, 2f);

        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (BounceShot)
            {
                var speed = lastVelocity.magnitude;
                if (WallbounceCnt >= 0)
                {
                    WallbounceCnt--;
                    var direction = Vector2.Reflect(lastVelocity.normalized, col.contacts[0].normal);
                    _rigidbody.velocity = direction * Mathf.Max(speed, 0f);
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                    //NewDir = Vector2.Reflect(NewDir, col.contacts[0].normal);

                    //_rigidbody.velocity = NewDir * 20f;
                    return;
                }
                else
                {
                    GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                    Effect.transform.parent = MonsterProjectileManager.transform;
                    Destroy(Effect, 2f);
                }
            }
        }
    }
}
