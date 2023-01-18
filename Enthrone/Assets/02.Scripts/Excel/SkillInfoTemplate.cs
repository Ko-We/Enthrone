using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillInfoTemplate {
    public int Index;
    public string Kor;
    public int StatIndex;
    public int InfoIndex;
    public float Value;
    public string KorText;


    public SkillInfoTemplate() { }

    public SkillInfoTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        Kor = listValue[wCount++];
        StatIndex = Convert.ToInt16(listValue[wCount++]);
        InfoIndex = Convert.ToInt16(listValue[wCount++]);
        Value = Convert.ToSingle(listValue[wCount++]);
        KorText = listValue[wCount++];
    }
}
