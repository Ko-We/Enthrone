using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MonsterHPMultiTemplate
{
    public int Index;
    public string Chapter;
    public float MultiDamage;
    public float MultiHp;
    public int Respond;

    public MonsterHPMultiTemplate() { }

    public MonsterHPMultiTemplate(string[] listValue)
    {
        SetUp(listValue);
    }
    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        Chapter = listValue[wCount++];
        MultiDamage = Convert.ToSingle(listValue[wCount++]);
        MultiHp = Convert.ToSingle(listValue[wCount++]);
        Respond = Convert.ToInt32(listValue[wCount++]);
    }
}