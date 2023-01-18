using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotate : MonoBehaviour
{
    public float Damage;
    public bool BossProjectileCheck;
    public bool NinjaSkillCheck;
    public GameObject HitEffect;
    public GameObject NinjaParent;

    // Start is called before the first frame update
    void Start()
    {
        if (BossProjectileCheck)
        {
            Damage = transform.parent.GetComponent<MonsterBounceProjectile>().Damage;
        }
        else
        {
            if (!NinjaParent)
            {
                //Damage = transform.parent.GetComponent<MonsterBounceProjectile>().Damage;
                Damage = transform.parent.GetComponent<MonsterProjectile>().Damage;
            }
        }
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 360f) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (BossProjectileCheck)
            {
                col.transform.SendMessage("PlayerHit", Damage);
            }
            else if (NinjaSkillCheck)
            {
                col.transform.SendMessage("PlayerHit", Damage);
                Destroy(NinjaParent);
                GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                Destroy(Effect, 2f);
            }
            else
            {
                col.transform.SendMessage("PlayerHit", Damage);
                GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                Destroy(Effect, 2f);
                Destroy(transform.parent.gameObject);
            }

        }
    }
}
