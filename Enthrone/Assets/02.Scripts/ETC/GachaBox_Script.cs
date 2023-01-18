using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaBox_Script : MonoBehaviour
{
    public GameObject CardPack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CardOpen()
    {
        CardPack.SetActive(true);
    }
    private void DrawBox()
    {
        SoundManager.Instance.PlaySfx(45);
    }
}
