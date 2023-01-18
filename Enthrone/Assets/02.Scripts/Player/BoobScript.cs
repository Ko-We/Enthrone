using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobScript : MonoBehaviour
{
    public enum BoobType
    {
        None,
        FlameType,
        FrozenType
    }

    MyObject myChar;

    public BoobType boobType;
    public int AnimLoopNum;
    public GameObject ExplosionEffect;

    public float Damage;
    public float SlowTime;
    private Animator _anim;

    // Start is called before the first frame update
    private void Awake()
    {
        myChar = MyObject.MyChar;
        AnimLoopNum = 0;
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AnimLoopNum >= 4)
        {
            _anim.SetBool("Boob", true);
        }
    }
    private void animLoopCheck()
    {
        AnimLoopNum++;
    }

    private void BoobEffect()
    {
        GameObject Effect = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Effect.GetComponent<ExplosionDamage>().Damage = Damage;
        Destroy(Effect, 2f);
    }
    private void FrozenEffect()
    {
        GameObject Effect = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Effect.GetComponent<FrozenExplosion>().SlowTime = SlowTime;
        Destroy(Effect, 2f);
    }

}
