using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DropMultiTemplate{
    public int Index;
    public int MultiDia, MultiSoul, MultiHero;

    public DropMultiTemplate() { }

    public DropMultiTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        MultiDia = Convert.ToInt16(listValue[wCount++]);
        MultiSoul = Convert.ToInt16(listValue[wCount++]);
        MultiHero = Convert.ToInt16(listValue[wCount++]);
    }
}
