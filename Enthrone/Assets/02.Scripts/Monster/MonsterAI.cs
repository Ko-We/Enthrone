using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterAI : MonoBehaviour
{
    public Transform target;
    public Transform monster;
    public Transform wallCheck;

    public Vector2 currentPosition;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float followCheckRadius;
    public float[] moveRange = { -0.6f, -0.5f, -0.4f, -0.3f, 0.3f, 0.4f, 0.5f, 0.6f };

    public float monsterHp = 2;
    Path _path;
    Seeker _seeker;
    private Rigidbody2D _rigdbody;
    private Animator _animator;
    public LayerMask _layerMask;

    int currentWaypoint = 0;

    private bool reachedEndOfPath = false;
    private bool isTarget;
    [SerializeField]
    private bool isWallCheck;
    private bool TargetRealCheck = true;
    private bool moving = true;


    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            TargetAutoCheck();
        }
        _seeker = GetComponent<Seeker>();
        _rigdbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        _seeker.StartPath(_rigdbody.position, target.position, OnPathComplete);
    }

    void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rigdbody.position, target.position, OnPathComplete);
        }
    } 
    private void Update()
    {
        CheckSurroundings();

        if (isTarget)
        {
            TargetTraceCheck(1);
        }
        else if (!isTarget)
        {
            TargetTraceCheck(0.5f);
        }
    }

    private void CheckSurroundings()
    {
        isTarget = Physics2D.OverlapCircle(transform.position, followCheckRadius, _layerMask);
        isWallCheck = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.6f, 1f), LayerMask.NameToLayer("Ground"));
    }

    void TargetTraceCheck(float checkSpeed)
    {
        TargetRealCheck = true;
        if (_path == null) return;

        if (currentWaypoint >= _path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)_path.vectorPath[currentWaypoint] - _rigdbody.position).normalized;
        Vector2 force = direction.normalized * checkSpeed * speed * Time.deltaTime;
        Debug.Log(force);

        //_rigdbody.AddForce(force);
        _rigdbody.velocity = force;

        float distance = Vector2.Distance(_rigdbody.position, _path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (_rigdbody.velocity.x >= 0.01f)
        {
            monster.localScale = new Vector3(-3f, 3f, 3f);
        }
        else if (_rigdbody.velocity.x <= -0.01f)
        {
            monster.localScale = new Vector3(3f, 3f, 3f);
        }
    }

    private void TargetLose()
    {
        //if (TargetRealCheck)
        //{
        //    //_rigdbody.velocity = Vector2.zero;
        //    currentPosition = transform.position;
        //    TargetRealCheck = !TargetRealCheck;
        //}
        //Debug.Log(randomPos);
        if (moving)
        {
            int randomX = Random.Range(0, 7);
            int randomY = Random.Range(0, 7);
            StartCoroutine(MoveTo(moveRange[randomX], moveRange[randomY]));
        }        
        //_rigdbody.position = Vector2.MoveTowards(transform.position, force, speed);
        //_rigdbody.MovePosition(force); 순간이동

        //Debug.Log(transform.position.x +" / "+ randomPos);

    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            currentWaypoint = 0;
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
                }
                
            }
        }
    }
    public void AttackHit(float Damage)
    {
        monsterHp -= Damage;
        if (monsterHp <= 0)
        {
            transform.Find("Monster(Skull)").gameObject.GetComponent<Animator>().SetBool("Death", true);
            _rigdbody.velocity = Vector2.zero;
        }
    }

    IEnumerator MoveTo(float X, float Y)
    {
        moving = false;
        _rigdbody.velocity = new Vector2(X, Y);

        yield return new WaitForSeconds(3f);
        moving = true;
    }

    //IEnumerator SlowActive(float Time)              //슬로우아이템
    //{
    //    SlowNum = 0.5f;
    //    yield return new WaitForSeconds(Time);
    //    SlowNum = 1f;
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followCheckRadius);

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(wallCheck.position, new Vector3(0.6f, 1f, 0f));
    }
}
