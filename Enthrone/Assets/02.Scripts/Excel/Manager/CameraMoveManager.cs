using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveManager : MonoBehaviour
{
    GameManager GM;
    MyObject myChar;
    [SerializeField]
    Transform tr;
    Vector3 offset;
    float smoothSpeed = 0.125f;

    public float limitMinX;
    public float limitMaxX;
    public float AddminX;
    public float AddmaxX;

    public float limitMinY;
    public float limitMaxY;
    public float AddminY;
    public float AddmaxY;

    public int MapCheckNum;

    bool DataCheck = false;
    private void Awake()
    {
        myChar = MyObject.MyChar;
        //GM = GameManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance)
        {
            if (GameManager.Instance.RoomManagerObj.activeSelf)
            {
                if (!myChar.Tutorial)
                {
                    if (RoomManager.Instance.EquipmentRoom || RoomManager.Instance.AntiqueRoom)
                    {
                        AddminX = 0;
                        AddmaxX = 0;

                        AddminY = -1.075f;
                        AddmaxY = -1.075f;
                    }
                    else if (RoomManager.Instance.LargeRoom)
                    {
                        AddminX = -2.65f;
                        AddmaxX = 3.05f;

                        AddminY = -2.65f;
                        AddmaxY = 0.4f;
                    }
                    else if (RoomManager.Instance.MidiumRoom)
                    {
                        AddminX = -2.01f;
                        AddmaxX = 2.01f;

                        AddminY = -2f;
                        AddmaxY = -0.05f;
                        //if (!myChar.EnthroneCameraOn)
                        //{
                        //    AddminX = 0;
                        //    AddmaxX = 0;

                        //    AddminY = -2.575f;
                        //    AddmaxY = -1.075f;
                        //}
                        //else if (myChar.EnthroneCameraOn)
                        //{
                        //    AddminX = -2.01f;
                        //    AddmaxX = 2.01f;

                        //    AddminY = -2f;
                        //    AddmaxY = -0.05f;
                        //}
                    }
                    else
                    {
                        AddminX = -1.05f;
                        AddmaxX = 1.05f;

                        AddminY = -1.7f;
                        AddmaxY = -0.5f;
                    }
                }
                else
                {
                    AddminX = 0;
                    AddmaxX = 0;

                    AddminY = -1.075f;
                    AddmaxY = -1.075f;
                }
            }
            else
            {
                if (MapCheckNum == 3)
                {
                    if (!myChar.EnthroneCameraOn)
                    {
                        AddminX = 0;
                        AddmaxX = 0;

                        AddminY = -2.575f;
                        AddmaxY = -1.075f;
                    }
                    else if (myChar.EnthroneCameraOn)
                    {
                        AddminX = -5.325f;
                        AddmaxX = 5.325f;

                        AddminY = -2.6f;
                        AddmaxY = -0.6f;
                    }
                }
                else if (MapCheckNum == 2)  //large
                {
                    AddminX = -2.65f;
                    AddmaxX = 3.05f;

                    AddminY = -2.65f;
                    AddmaxY = 0.4f;
                }
                else if (MapCheckNum == 1)  //midium
                {
                    AddminX = -2.01f;
                    AddmaxX = 2.01f;

                    AddminY = -2f;
                    AddmaxY = -0.05f;
                }
                else if (MapCheckNum == 0)  //small
                {
                    AddminX = -1.05f;
                    AddmaxX = 1.05f;

                    AddminY = -1.7f;
                    AddmaxY = -0.5f;
                }
                else    //basic
                {
                    AddminX = 0;
                    AddmaxX = 0;

                    AddminY = -1.075f;
                    AddmaxY = -1.075f;
                }
                
            }
            if (GameManager.Instance.SelectCharacter)
            {
                tr = GameManager.Instance.SelectCharacter.transform;
                limitMinX = GameManager.Instance.Player.transform.position.x + AddminX;
                limitMaxX = GameManager.Instance.Player.transform.position.x + AddmaxX;

                limitMinY = GameManager.Instance.Player.transform.position.y + AddminY;
                limitMaxY = GameManager.Instance.Player.transform.position.y + AddmaxY;
            }
            
        }

        //float x = Mathf.Clamp(tr.position.x, limitMinX, limitMaxX);
        //float y = Mathf.Clamp(tr.position.y, limitMinY, limitMaxY);

        //transform.position = new Vector3(x, y, -10);
        float x = Mathf.Clamp(tr.position.x, limitMinX, limitMaxX);
        float y = Mathf.Clamp(tr.position.y, limitMinY, limitMaxY);
        offset.Set(Mathf.Clamp(Mathf.Lerp(transform.position.x, tr.position.x, smoothSpeed), limitMinX, limitMaxX),
            Mathf.Clamp(Mathf.Lerp(transform.position.y, tr.position.y, smoothSpeed), limitMinY, limitMaxY), transform.position.z);

        //Vector3 desiredPostion = tr.position + offset;

        //Vector3 SmoothdPosition = Vector3.Lerp(tr.position, desiredPostion, smoothSpeed);
        //transform.position = SmoothdPosition;

        if (Time.timeScale > 0)
        {
            transform.position = offset;
        }
        else
        {
            transform.position = new Vector3(x, y, -10);
        }
    }


}
