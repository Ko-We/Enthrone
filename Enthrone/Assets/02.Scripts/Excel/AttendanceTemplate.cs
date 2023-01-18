using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttendanceTemplate {
    public int Index;
    public string Day;
    public int Dia;

    public AttendanceTemplate() { }

    public AttendanceTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        Day = listValue[wCount++];
        Dia = Convert.ToInt16(listValue[wCount++]);
    }
}
