using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackOpenScript : MonoBehaviour
{
    public GameObject CardPanel;
    public bool CardPanelCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CardPanelOpen()
    {
        if (CardPanelCheck)
        {
            CardPanel.SetActive(true);
        }        
    }
}
