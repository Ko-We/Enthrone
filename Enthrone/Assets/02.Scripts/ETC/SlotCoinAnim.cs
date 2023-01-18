using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCoinAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SlotCoinImgAnimCheck()
    {
        LobbiManager.instance.Coin_Img.GetComponent<Animator>().enabled = false;
    }
}
