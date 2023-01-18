using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AntiqueItemTemplate {
    public int Index;
    public string Kor;
    public int Grade;
    public int StatIndex;
    public int InfoIndex;
    public float Lv1, Lv2, Lv3, Lv4, Lv5, Lv6, Lv7, Lv8, Lv9, Lv10, Lv11;

    public AntiqueItemTemplate() { }

    public AntiqueItemTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        Kor = listValue[wCount++];
        Grade = Convert.ToInt16(listValue[wCount++]);
        StatIndex = Convert.ToInt16(listValue[wCount++]);
        InfoIndex = Convert.ToInt32(listValue[wCount++]);
        Lv1 = Convert.ToSingle(listValue[wCount++]);
        Lv2 = Convert.ToSingle(listValue[wCount++]);
        Lv3 = Convert.ToSingle(listValue[wCount++]);
        Lv4 = Convert.ToSingle(listValue[wCount++]);
        Lv5 = Convert.ToSingle(listValue[wCount++]);
        Lv6 = Convert.ToSingle(listValue[wCount++]);
        Lv7 = Convert.ToSingle(listValue[wCount++]);
        Lv8 = Convert.ToSingle(listValue[wCount++]);
        Lv9 = Convert.ToSingle(listValue[wCount++]);
        Lv10 = Convert.ToSingle(listValue[wCount++]);
        Lv11 = Convert.ToSingle(listValue[wCount++]);
    }
}
