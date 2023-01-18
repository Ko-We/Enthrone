using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTile : MonoBehaviour
{
    private Animator _anim;
    public bool destroy = false;
    private float DelayTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TileMotion();
    }

    private void destroyTile()
    {
        destroy = !destroy;
        DelayTime = 2f;
    }

    private void TileMotion()
    {
        if (destroy)
        {
            if (DelayTime <=0)
            {
                _anim.SetBool("Destroy", true);
            }
            else
            {
                DelayTime -= Time.deltaTime;
            }            
        }
        else
        {
            if (DelayTime <= 0)
            {
                _anim.SetBool("Destroy", false);
                
            }
            else
            {
                DelayTime -= Time.deltaTime;
            }
            
        }
    }
}
