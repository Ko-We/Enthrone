using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopTemplate {

    public int Index;
    public string[] Content;
    //public string Kor;
    //public string Eng;

    public ShopTemplate() { }

    public ShopTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        Content = new string[4];

        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        for (int nCnt = 0; nCnt < Content.Length; ++nCnt)
        {
            Content[nCnt] = listValue[wCount++];
        }
        //Kor = listValue[wCount++];
        //Eng = listValue[wCount++];
    }
}
