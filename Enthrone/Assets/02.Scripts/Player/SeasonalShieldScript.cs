using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonalShieldScript : MonoBehaviour
{
    MyObject myChar;
    GameManager instance;
    private SpriteRenderer _sprite;
    private GameObject PlayerObjectManager;


    public Sprite[] Shield;
    public GameObject[] HitEffect;
    public GameObject ProjectileObject;
    public GameObject HealingEffect;
    public Collider2D _collider;

    private float Damage;

    void Start()
    {
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
        _sprite = GetComponent<SpriteRenderer>();
        PlayerObjectManager = GameObject.Find("PlayerObjectManager");
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Damage = myChar.Damage;
        _sprite.sprite = Shield[myChar.SeasonNumber];

        if (instance.SelectCharacter.GetComponent<PlayerTargeting>().MonsterList.Count <= 0)
        {
            _collider.enabled = false;
        }
        else
        {
            _collider.enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monster" || col.gameObject.tag == "MonsterProjectile")
        {
            if (col.gameObject.tag == "MonsterProjectile")
            {
                Destroy(col.gameObject);
            }
            else if (col.gameObject.tag == "Monster")
            {
                if (myChar.ActiveItme_Lv == 3)
                {
                    Damage = myChar.Damage;
                }
                else if (myChar.ActiveItme_Lv == 2)
                {
                    Damage = myChar.Damage * 0.7f;
                }
                else if (myChar.ActiveItme_Lv == 1)
                {
                    Damage = myChar.Damage * 0.5f;
                }

                col.SendMessage("AttackHit", Damage);
            }
            ShieldEffct();
            gameObject.SetActive(false);
        }        
    }

    private void ShieldEffct()
    {
        switch (myChar.SeasonNumber)
        {
            case 1:
                FlameShieldEffect();
                break;
            case 2:
                IceShieldEffect();
                break;
            case 3:
                EarthShieldEffect();
                break;
            case 4:
                NatureEffect();
                break;
            default:
                break;
        }
    }
    private void FlameShieldEffect()
    {
        GameObject ShieldEffect = Instantiate(HitEffect[myChar.SeasonNumber], transform.position, Quaternion.identity);
        //if (myChar.ActiveItme_Lv == 3)
        //{
        //    Damage = myChar.Damage;
        //}
        //else if (myChar.ActiveItme_Lv == 2)
        //{
        //    Damage = myChar.Damage * 0.7f;
        //}
        //else if (myChar.ActiveItme_Lv == 1)
        //{
        //    Damage = myChar.Damage * 0.5f;
        //}
        ShieldEffect.GetComponent<ExplosionDamage>().Damage = Damage;
        ShieldEffect.transform.parent = PlayerObjectManager.transform;
        Destroy(ShieldEffect, 1f);
        //폭발이펙트에서 Destroy해주기때문에 따로 Destroy안해도됨
    }
    private void IceShieldEffect()
    {
        GameObject ShieldEffect = Instantiate(HitEffect[myChar.SeasonNumber], transform.position, Quaternion.identity);
        ShieldEffect.transform.parent = PlayerObjectManager.transform;
    }
    private void EarthShieldEffect()
    {
        GameObject ShieldEffect = Instantiate(HitEffect[myChar.SeasonNumber], transform.position, Quaternion.identity);
        ShieldEffect.transform.parent = PlayerObjectManager.transform;

        for (int i = 0; i < 360; i += 45)
        {
            GameObject temp = Instantiate(ProjectileObject, transform.position, Quaternion.identity);
            temp.transform.parent = PlayerObjectManager.transform;
            temp.GetComponent<PlayerSkillProjectile>().BoltSpeed = 3.5f;

            Destroy(temp, 2f);

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }
    private void NatureEffect()
    {
        myChar.HealingShieldCheck--;
        GameObject ShieldEffect = Instantiate(HitEffect[myChar.SeasonNumber], transform.position, Quaternion.identity);
        ShieldEffect.transform.parent = PlayerObjectManager.transform;

        if (myChar.HealingShieldCheck == 0)
        {
            Healing();
        }
    }
    private void Healing()
    {
        GameObject Healing_Effect = Instantiate(HealingEffect, instance.SelectCharacter.transform.position, Quaternion.identity);
        Healing_Effect.transform.parent = PlayerObjectManager.transform;
        myChar.currentHp++;
    }
}
