using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GachaTemplate{

    public int Index;
    public int GachaNum;
    public int ItemNum;

    public GachaTemplate() { }

    public GachaTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        GachaNum = Convert.ToInt32(listValue[wCount++]);
        ItemNum = Convert.ToInt32(listValue[wCount++]);
    }
}
