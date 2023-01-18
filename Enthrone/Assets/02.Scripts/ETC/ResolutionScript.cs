using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionScript : MonoBehaviour
{
    public Camera camera;
    public Rect rect;
    void Awake()
    {
        camera = GetComponent<Camera>();
        rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16);
        float scalewdith = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2;
        }
        else
        {
            rect.width = scalewdith;
            rect.x = (1f - scalewdith) / 2;
        }
        camera.rect = rect;
    }
}
