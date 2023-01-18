using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class UnitTemplate {
    public int Index;
    public string Kor;
    public int Map;
    public int Class;
    public float ASPD;
    public int Range, HP;
    public float Speed;
    public int Jump;
    public float Damage;
    public float P2, P3;
    public float BoltSpeed;

    public UnitTemplate() { }

    public UnitTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);
        Kor = listValue[wCount++];
        Map = Convert.ToInt16(listValue[wCount++]);
        Class = Convert.ToInt16(listValue[wCount++]);
        //Atk = Convert.ToInt16(listValue[wCount++]);
        ASPD = Convert.ToSingle(listValue[wCount++]);
        Range = Convert.ToInt16(listValue[wCount++]);
        HP = Convert.ToInt16(listValue[wCount++]);
        Speed = Convert.ToSingle(listValue[wCount++]);
        Jump = Convert.ToInt16(listValue[wCount++]);
        Damage = Convert.ToSingle(listValue[wCount++]);
        P2 = Convert.ToSingle(listValue[wCount++]);
        P3 = Convert.ToSingle(listValue[wCount++]);
        BoltSpeed = Convert.ToSingle(listValue[wCount++]);
    }
}
