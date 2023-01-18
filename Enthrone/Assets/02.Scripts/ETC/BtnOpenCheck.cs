using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOpenCheck : MonoBehaviour
{
    GameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void BtnOn()
    {
        instance.BtnOnCheck = true;
    }
}
