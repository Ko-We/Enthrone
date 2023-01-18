using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterYetiKingWall : MonoBehaviour
{
    private Animator _anim;
    public float ThawTime;
    private IEnumerator ThawCoroutine;
    public bool PosCheck = true;
    private float PosX;

    // Start is called before the first frame update
    void Start()
    {
        ThawCoroutine = ThawWall(ThawTime);
        _anim = GetComponent<Animator>();
        StartCoroutine(ThawCoroutine);
        PosX = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (PosCheck)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.2f);
            PosCheck = !PosCheck;
        }
        
    }
    private void DestroyObj()
    {
        Destroy(gameObject);
    }
    public void ThawWallRefrash()
    {
        StopCoroutine(ThawCoroutine);
        ThawCoroutine = ThawWall(5f);
        StartCoroutine(ThawCoroutine);
    }
    IEnumerator ThawWall(float Time)
    {
        yield return new WaitForSeconds(Time);
        _anim.SetBool("Thaw", true);
    }
}
