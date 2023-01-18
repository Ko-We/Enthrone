using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlackSmithTemplate {
    public int Index;
    public int ADSkip;
    public int CHSkip;
    public int CHCost;
    public int Nper, Ntime, Nsoul, Nhero, Nenchan;
    public int Rper, Rtime, Rsoul, Rhero, Renchan;
    public int Uper, Utime, Usoul, Uhero, Uenchan;

    public BlackSmithTemplate() { }

    public BlackSmithTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        ADSkip = Convert.ToInt16(listValue[wCount++]);
        CHSkip = Convert.ToInt16(listValue[wCount++]);
        CHCost = Convert.ToInt16(listValue[wCount++]);
        Nper = Convert.ToInt16(listValue[wCount++]);
        Ntime = Convert.ToInt16(listValue[wCount++]);
        Nsoul = Convert.ToInt16(listValue[wCount++]);
        Nhero = Convert.ToInt16(listValue[wCount++]);
        Nenchan = Convert.ToInt16(listValue[wCount++]);
        Rper = Convert.ToInt16(listValue[wCount++]);
        Rtime = Convert.ToInt16(listValue[wCount++]);
        Rsoul = Convert.ToInt16(listValue[wCount++]);
        Rhero = Convert.ToInt16(listValue[wCount++]);
        Renchan = Convert.ToInt16(listValue[wCount++]);
        Uper = Convert.ToInt16(listValue[wCount++]);
        Utime = Convert.ToInt16(listValue[wCount++]);
        Usoul = Convert.ToInt16(listValue[wCount++]);
        Uhero = Convert.ToInt16(listValue[wCount++]);
        Uenchan = Convert.ToInt16(listValue[wCount++]);
    }
}
