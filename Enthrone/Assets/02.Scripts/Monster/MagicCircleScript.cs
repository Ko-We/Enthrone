using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleScript : MonoBehaviour
{
    public GameObject BossObject;
    public Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();

        _anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = BossObject.transform.position;
    }

    private void BossPaidIn()
    {
        BossObject.GetComponent<BossPatternScript>().FadeInColor();
    }
    private void FadeInShot()
    {
        BossObject.GetComponent<MonsterTargeting>().FadeShot();
    }
    private void AnimEnabled()
    {
        _anim.enabled = false;
        BossObject.GetComponent<BossPatternScript>().FadeCheck = false;
        BossObject.GetComponent<BossPatternScript>().Teleport = false;
        BossObject.GetComponent<BossPatternScript>().ColliderOnCheck();
    }
}
