using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    public Slider hpSlider;
    public Transform Monster;
    public float maxHp = 1000f;
    public float currentHp = 1000f;
    public float ScaleX, ScaleY;
    public bool FllowMonster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Monster.position;
        hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 5f);
        //hpSlider.value = currentHp / maxHp;
        if (!FllowMonster)
        {
            if (Monster.localScale.x > 0)
            {
                transform.localScale = new Vector2(ScaleX, ScaleY);
            }
            else
            {
                transform.localScale = new Vector2(-ScaleX, ScaleY);
            }
        }
        else
        {
            transform.localScale = new Vector2(ScaleX, ScaleY);
        }
    }

}
