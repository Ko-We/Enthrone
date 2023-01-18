using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThroneStatTemplate {

    public int Index;

    public int ThroneRank;
    public int ThroneEquipEXPRank;

    public int ThroneSkill;

    public float ThroneASPD;
    public float ThroneRange;
    public int ThroneHP;
    public float ThroneSpeed;
    public int ThroneDamage;

    public int ThroneStageATK;
    public int ThroneStageHP;

    public int ThroneDropMin;
    public int ThroneDropMax;

    public ThroneStatTemplate() { }

    public ThroneStatTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);

        ThroneRank = Convert.ToInt16(listValue[wCount++]);
        ThroneEquipEXPRank = Convert.ToInt16(listValue[wCount++]);

        ThroneSkill = Convert.ToInt16(listValue[wCount++]);

        ThroneASPD = Convert.ToSingle(listValue[wCount++]);
        ThroneRange = Convert.ToSingle(listValue[wCount++]);
        ThroneHP = Convert.ToInt16(listValue[wCount++]);
        ThroneSpeed = Convert.ToSingle(listValue[wCount++]);
        ThroneDamage = Convert.ToInt16(listValue[wCount++]);

        ThroneStageATK = Convert.ToInt16(listValue[wCount++]);
        ThroneStageHP = Convert.ToInt16(listValue[wCount++]);

        ThroneDropMin = Convert.ToInt16(listValue[wCount++]);
        ThroneDropMax = Convert.ToInt16(listValue[wCount++]);
    }
}
