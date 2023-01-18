using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemTemplate{

    public int Index;
    public string Kor;
    public int Grade;
    public int Option1, Option2, Option3;
    public int Atk0, Atk1, Atk2, Atk3, Atk4, Atk5, Atk6, Atk7, Atk8, Atk9, Atk10;
    public float ASPD0, ASPD1, ASPD2, ASPD3, ASPD4, ASPD5, ASPD6, ASPD7, ASPD8, ASPD9, ASPD10;
    public float Range0, Range1, Range2, Range3, Range4, Range5, Range6, Range7, Range8, Range9, Range10;
    public int HP0, HP1, HP2, HP3, HP4, HP5, HP6, HP7, HP8, HP9, HP10;

    public ItemTemplate() { }

    public ItemTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        Kor = listValue[wCount++];
        Grade = Convert.ToInt16(listValue[wCount++]);
        Option1 = Convert.ToInt16(listValue[wCount++]);
        Option2 = Convert.ToInt16(listValue[wCount++]);
        Option3 = Convert.ToInt16(listValue[wCount++]);
        Atk0 = Convert.ToInt16(listValue[wCount++]);
        Atk1 = Convert.ToInt16(listValue[wCount++]);
        Atk2 = Convert.ToInt16(listValue[wCount++]);
        Atk3 = Convert.ToInt16(listValue[wCount++]);
        Atk4 = Convert.ToInt16(listValue[wCount++]);
        Atk5 = Convert.ToInt16(listValue[wCount++]);
        Atk6 = Convert.ToInt16(listValue[wCount++]);
        Atk7 = Convert.ToInt16(listValue[wCount++]);
        Atk8 = Convert.ToInt16(listValue[wCount++]);
        Atk9 = Convert.ToInt16(listValue[wCount++]);
        Atk10 = Convert.ToInt16(listValue[wCount++]);
        ASPD0 = Convert.ToSingle(listValue[wCount++]);
        ASPD1 = Convert.ToSingle(listValue[wCount++]);
        ASPD2 = Convert.ToSingle(listValue[wCount++]);
        ASPD3 = Convert.ToSingle(listValue[wCount++]);
        ASPD4 = Convert.ToSingle(listValue[wCount++]);
        ASPD5 = Convert.ToSingle(listValue[wCount++]);
        ASPD6 = Convert.ToSingle(listValue[wCount++]);
        ASPD7 = Convert.ToSingle(listValue[wCount++]);
        ASPD8 = Convert.ToSingle(listValue[wCount++]);
        ASPD9 = Convert.ToSingle(listValue[wCount++]);
        ASPD10 = Convert.ToSingle(listValue[wCount++]);
        Range0 = Convert.ToSingle(listValue[wCount++]);
        Range1 = Convert.ToSingle(listValue[wCount++]);
        Range2 = Convert.ToSingle(listValue[wCount++]);
        Range3 = Convert.ToSingle(listValue[wCount++]);
        Range4 = Convert.ToSingle(listValue[wCount++]);
        Range5 = Convert.ToSingle(listValue[wCount++]);
        Range6 = Convert.ToSingle(listValue[wCount++]);
        Range7 = Convert.ToSingle(listValue[wCount++]);
        Range8 = Convert.ToSingle(listValue[wCount++]);
        Range9 = Convert.ToSingle(listValue[wCount++]);
        Range10 = Convert.ToSingle(listValue[wCount++]);
        HP0 = Convert.ToInt16(listValue[wCount++]);
        HP1 = Convert.ToInt16(listValue[wCount++]);
        HP2 = Convert.ToInt16(listValue[wCount++]);
        HP3 = Convert.ToInt16(listValue[wCount++]);
        HP4 = Convert.ToInt16(listValue[wCount++]);
        HP5 = Convert.ToInt16(listValue[wCount++]);
        HP6 = Convert.ToInt16(listValue[wCount++]);
        HP7 = Convert.ToInt16(listValue[wCount++]);
        HP8 = Convert.ToInt16(listValue[wCount++]);
        HP9 = Convert.ToInt16(listValue[wCount++]);
        HP10 = Convert.ToInt16(listValue[wCount++]);
    }
}
