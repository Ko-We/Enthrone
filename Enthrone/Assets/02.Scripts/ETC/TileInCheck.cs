using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInCheck : MonoBehaviour
{
    public Transform wall_Right_Check;
    public Transform wall_Left_Check;
    public float wallCheckDistance;
    public bool isRightWall;
    public bool isLeftWall;
    public LayerMask _layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
    }
    private void WallCheck()
    {
        isRightWall = Physics2D.OverlapBox(wall_Right_Check.position, new Vector3(0.2f, wallCheckDistance, wall_Right_Check.position.z), 0, _layerMask);
        isLeftWall = Physics2D.OverlapBox(wall_Left_Check.position, new Vector3(-0.2f, wallCheckDistance, wall_Left_Check.position.z), 0, _layerMask);
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (isRightWall)
            {
                col.gameObject.transform.Translate(-0.1f, 0.1f, 0);
            }
            else if (isLeftWall)
            {
                col.gameObject.transform.Translate(0.1f, 0.1f, 0);
            }
            else
            {
                col.gameObject.transform.Translate(-0.1f, 0.1f, 0);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(wall_Right_Check.position, new Vector3(0.2f, wallCheckDistance, wall_Right_Check.position.z));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(wall_Left_Check.position, new Vector3(-0.2f, wallCheckDistance, wall_Left_Check.position.z));
    }
}
