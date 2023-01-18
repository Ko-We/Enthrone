using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerLine : MonoBehaviour
{
    public GameObject Projectile;
    LineRenderer _lineRender;

    public Vector3 EndPosition;
    // Start is called before the first frame update
    void Start()
    {
        _lineRender = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        _lineRender.SetPosition(0, transform.position);
        if (Projectile.transform.rotation.z > 0)
        {
            _lineRender.SetPosition(1, transform.position + new Vector3(9, 0, 0));
        }
        else
        {
            _lineRender.SetPosition(1, transform.position + new Vector3(-9, 0, 0));
        }
        
    }
}
