using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosionDamage : MonoBehaviour
{
    public float Damage;
    public GameObject Circle;
    public SpriteRenderer Faid;
    public bool Player, Monster;
    public GameObject Glow;

    public float FadeTime = 0.4f; // Fade효과 재생시간
    private float time;
    private bool isPlaying = false;
    // Start is called before the first frame update
    private void Awake()
    {
        Faid = Circle.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        Faid.DOFade(0, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(Glow, FadeTime);
        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (Monster)
        {
            if (col.gameObject.tag == "Player")
            {
                col.SendMessage("PlayerHit", Damage);
            }
        }

        if (Player)
        {
            if (col.gameObject.tag == "Monster")
            {
                col.SendMessage("AttackHit", Damage);
            }
        }
    }


}
