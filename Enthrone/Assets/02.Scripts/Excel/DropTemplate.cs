using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DropTemplate {
    public int Index;
    public string Kor;
    public int BasicMin, BasicMax;
    public float BasicPer;
    public int EliteMin, EliteMax;
    public float ElitePer;
    public int BossMin, BossMax;
    public float BossPer;
    public int HeroMin, HeroMax;
    public float HeroPer;
    public int GiftMin, GiftMax;
    public float GiftPer;

    public DropTemplate() { }

    public DropTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        Kor = listValue[wCount++];
        BasicMin = Convert.ToInt16(listValue[wCount++]);
        BasicMax = Convert.ToInt16(listValue[wCount++]);
        BasicPer = Convert.ToSingle(listValue[wCount++]);
        EliteMin = Convert.ToInt16(listValue[wCount++]);
        EliteMax = Convert.ToInt16(listValue[wCount++]);
        ElitePer = Convert.ToSingle(listValue[wCount++]);
        BossMin = Convert.ToInt16(listValue[wCount++]);
        BossMax = Convert.ToInt16(listValue[wCount++]);
        BossPer = Convert.ToSingle(listValue[wCount++]);
        HeroMin = Convert.ToInt16(listValue[wCount++]);
        HeroMax = Convert.ToInt16(listValue[wCount++]);
        HeroPer = Convert.ToSingle(listValue[wCount++]);
        GiftMin = Convert.ToInt16(listValue[wCount++]);
        GiftMax = Convert.ToInt16(listValue[wCount++]);
        GiftPer = Convert.ToSingle(listValue[wCount++]);
    }
}
