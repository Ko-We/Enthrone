using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkinDataTemplate{

    public int Index;
    public string[] Content;
    public int Grade;
    public int Resource;
    public int Cost;

    public SkinDataTemplate() { }

    public SkinDataTemplate(string[] listValue)
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
        Grade = Convert.ToInt16(listValue[wCount++]);
        Resource = Convert.ToInt16(listValue[wCount++]);
        Cost = Convert.ToInt16(listValue[wCount++]);
    }
}
