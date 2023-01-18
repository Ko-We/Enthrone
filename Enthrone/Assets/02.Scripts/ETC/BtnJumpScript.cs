using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnJumpScript : MonoBehaviour
{
    private Animator _anim;
    //private bool ClickCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AnimEndCheck()
    {
        _anim.enabled = false;
    }
}
