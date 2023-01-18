using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LanguageScript : MonoBehaviour
{
    MyObject myChar;
    public Dropdown _dropdown;

    public GameObject startButtonObject;
    private Button startButton;
    private Image startButtonImage;
    private void Awake()
    {
        myChar = MyObject.MyChar;
        _dropdown = GetComponent<Dropdown>();
        
    }
    private void Start()
    {
        _dropdown.value = myChar.LanguageNum;
    }

    // Update is called once per frame
    void Update()
    {
        myChar.LanguageNum = _dropdown.value;
        myChar.SaveLanguageNum();
    }
}
