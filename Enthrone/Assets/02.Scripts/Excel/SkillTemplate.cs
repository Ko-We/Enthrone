using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillTemplate {
    public int Index;
    public int CodeIndex;
    public int Lv;

    public SkillTemplate() { }

    public SkillTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        CodeIndex = Convert.ToInt32(listValue[wCount++]);
        Lv = Convert.ToInt32(listValue[wCount++]);
    }
}
