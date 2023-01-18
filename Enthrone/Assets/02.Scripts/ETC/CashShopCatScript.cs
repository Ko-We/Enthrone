using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashShopCatScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CatAnim()
    {
        int Stretch = Random.Range(0, 5);

        if (Stretch == 0)
        {
           LobbiManager.instance.CatStretchCheck = true;
        }
    }
    public void CatAnimEnd()
    {
        LobbiManager.instance.CatStretchCheck = false;
    }

}
