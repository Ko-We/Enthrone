using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CashShopTemplate{

    public int Index;
    public string Kor;
    public int Cost;
    public float CoolTime;
    public int Dia;
    public int Soul;
    public int HeroToken;
    public int SlotCoin;
    public int Key;

    public CashShopTemplate() { }

    public CashShopTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        Kor = listValue[wCount++];
        Cost = Convert.ToInt32(listValue[wCount++]);
        CoolTime = Convert.ToSingle(listValue[wCount++]);
        Dia = Convert.ToInt32(listValue[wCount++]);
        Soul = Convert.ToInt32(listValue[wCount++]);
        HeroToken = Convert.ToInt32(listValue[wCount++]);
        SlotCoin = Convert.ToInt32(listValue[wCount++]);
        Key = Convert.ToInt32(listValue[wCount++]);
    }
}
