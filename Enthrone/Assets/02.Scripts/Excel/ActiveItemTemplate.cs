using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActiveItemTemplate {
    public int Index;
    public string Kor;
    public int Grade;
    public int StatIndex;
    public float Lv0, Lv1, Lv2, Lv3, Lv4, Lv5, Lv6;
    public string KorText;

    public ActiveItemTemplate() { }

    public ActiveItemTemplate(string[] listValue)
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
        Lv0 = Convert.ToSingle(listValue[wCount++]);
        Lv1 = Convert.ToSingle(listValue[wCount++]);
        Lv2 = Convert.ToSingle(listValue[wCount++]);
        Lv3 = Convert.ToSingle(listValue[wCount++]);
        Lv4 = Convert.ToSingle(listValue[wCount++]);
        Lv5 = Convert.ToSingle(listValue[wCount++]);
        Lv6 = Convert.ToSingle(listValue[wCount++]);
        KorText = listValue[wCount++];
    }
}
