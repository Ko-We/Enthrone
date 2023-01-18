using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEndCheckScript : MonoBehaviour
{
    public Animator _Anim;
    // Start is called before the first frame update
    void Start()
    {
        _Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EndAnim()
    {
        _Anim.enabled = false;
    }
}
