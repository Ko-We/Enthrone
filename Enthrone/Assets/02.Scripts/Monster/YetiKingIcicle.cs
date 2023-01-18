using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiKingIcicle : MonoBehaviour
{
    MyObject myChar;
    GameManager instance;

    public int MonsterIndex;
    public Animator _anim;
    public LineRenderer _lineRender;
    private GameObject MonsterProjectile;

    public GameObject MonsterBolt;
    public Transform AttackPoint;

    public Transform target;

    public float BoltSpeed;

    public GameObject PlayerList;

    [Header("ShotType")]
    public bool ForwardShot;
    public bool LayerPoint;


    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        MonsterProjectile = GameObject.Find("MonsterProjectileManager");
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
        _lineRender = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetAutoCheck();
        if (myChar.YetiKingIcicleCheck)
        {
            _anim.SetBool("Attack", true);
        }
    }

    public void TargetAutoCheck()
    {
        GameObject Player = GameObject.Find("Player");

        for (int i = 0; i < myChar.HeroNum; i++)
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
    //YetiKing Icicle 전용 AngleAxis가 90도면 생성될때 가로로나와서 모서리에서 발사할경우 콜라이더에 닿아서 오브젝트가 파괴됨
    public void YetiKing_IcicleAttack()
    {
        GameObject projectile = Instantiate(MonsterBolt, AttackPoint.position, Quaternion.identity);
        //미사일 생성을 ProjectileManager자식으로 생성하기위함
        projectile.transform.parent = MonsterProjectile.transform;
        projectile.GetComponent<MonsterProjectile>().BoltSpeed = BoltSpeed;
        projectile.GetComponent<MonsterProjectile>().Damage = instance.UnitDataMgr.GetTemplate(MonsterIndex).Damage * myChar.MultiDamage;
        if (!LayerPoint && ForwardShot)
        {
            if (transform.localScale.x > 0)
            {
                projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(-180, Vector3.forward);
            }
            else if (transform.localScale.x < 0)
            {
                projectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(180, Vector3.forward);
            }

        }
    }

    public void MonsterDestroy()
    {
        Destroy(gameObject);
    }
}
