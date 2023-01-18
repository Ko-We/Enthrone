using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseBGScroller : MonoBehaviour
{
    public Material _Render;

    private float scroll_x, scroll_y;

    public float speed;

    void Start()
    {
    }

    void Update()
    {
        scroll_x += Time.unscaledDeltaTime * speed;
        scroll_y += Time.unscaledDeltaTime * speed;
        _Render.mainTextureOffset = new Vector2(-scroll_x, -scroll_y);
    }
}
