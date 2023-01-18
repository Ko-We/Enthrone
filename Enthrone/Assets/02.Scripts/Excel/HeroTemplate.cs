using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeroTemplate{

    public int Index;
    public string Name;
    public int Cost;
    public int KHP, WHP, AHP, WiHP, NHP;
    public float KAtk, KASPD, KRange;
    public float WAtk, WASPD, WRange;
    public float AAtk, AASPD, ARange;
    public float WiAtk, WiASPD, WiRange;
    public float NAtk, NASPD, NRange;

    public HeroTemplate() { }

    public HeroTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        Cost = Convert.ToInt32(listValue[wCount++]);
        KAtk = Convert.ToSingle(listValue[wCount++]);
        KASPD = Convert.ToSingle(listValue[wCount++]);
        KRange = Convert.ToSingle(listValue[wCount++]);
        KHP = Convert.ToInt16(listValue[wCount++]);
        WAtk = Convert.ToSingle(listValue[wCount++]);
        WASPD = Convert.ToSingle(listValue[wCount++]);
        WRange = Convert.ToSingle(listValue[wCount++]);
        WHP = Convert.ToInt16(listValue[wCount++]);
        AAtk = Convert.ToSingle(listValue[wCount++]);
        AASPD = Convert.ToSingle(listValue[wCount++]);
        ARange = Convert.ToSingle(listValue[wCount++]);
        AHP = Convert.ToInt16(listValue[wCount++]);
        WiAtk = Convert.ToSingle(listValue[wCount++]);
        WiASPD = Convert.ToSingle(listValue[wCount++]);
        WiRange = Convert.ToSingle(listValue[wCount++]);
        WiHP = Convert.ToInt16(listValue[wCount++]);
        NAtk = Convert.ToSingle(listValue[wCount++]);
        NASPD = Convert.ToSingle(listValue[wCount++]);
        NRange = Convert.ToSingle(listValue[wCount++]);
        NHP = Convert.ToInt16(listValue[wCount++]);
    }
}
