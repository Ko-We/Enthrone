using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThroneSkillTemplate
{

    public int Index;
    public float K_Speed;
    public float K_ATK;
    public int K_ProjectileNo;
    public float K_ProjectileSpeed;
    public float K_Channeling;
    public float K_SkillSpeed;
    public float K_CoolTime;
    public float W_Speed;
    public float W_ATK;
    public int W_ProjectileNo;
    public float W_ProjectileSpeed;
    public float W_Channeling;
    public float W_SkillSpeed;
    public float W_CoolTime;
    public float A_Speed;
    public float A_ATK;
    public int A_ProjectileNo;
    public float A_ProjectileSpeed;
    public float A_Channeling;
    public float A_SkillSpeed;
    public float A_CoolTime;
    public float Wi_Speed;
    public float Wi_ATK;
    public int Wi_ProjectileNo;
    public float Wi_ProjectileSpeed;
    public float Wi_Channeling;
    public float Wi_SkillSpeed;
    public float Wi_CoolTime;
    public float N_Speed;
    public float N_ATK;
    public int N_ProjectileNo;
    public float N_ProjectileSpeed;
    public float N_Channeling;
    public float N_SkillSpeed;
    public float N_CoolTime;

    //public float HeroSpeed;
    //public float ProjectileSpeed;
    //public float CoolTime;

    public ThroneSkillTemplate() { }

    public ThroneSkillTemplate(string[] listValue)
    {
        SetUp(listValue);
    }

    public void SetUp(string[] listValue)
    {
        ushort wCount = 0;
        Index = Convert.ToInt32(listValue[wCount++]);

        K_Speed = Convert.ToSingle(listValue[wCount++]);
        K_ATK = Convert.ToSingle(listValue[wCount++]);
        K_ProjectileNo = Convert.ToInt32(listValue[wCount++]);
        K_ProjectileSpeed = Convert.ToSingle(listValue[wCount++]);
        K_Channeling = Convert.ToSingle(listValue[wCount++]);
        K_SkillSpeed = Convert.ToSingle(listValue[wCount++]);
        K_CoolTime = Convert.ToSingle(listValue[wCount++]);
        W_Speed = Convert.ToSingle(listValue[wCount++]);
        W_ATK = Convert.ToSingle(listValue[wCount++]);
        W_ProjectileNo = Convert.ToInt32(listValue[wCount++]);
        W_ProjectileSpeed = Convert.ToSingle(listValue[wCount++]);
        W_Channeling = Convert.ToSingle(listValue[wCount++]);
        W_SkillSpeed = Convert.ToSingle(listValue[wCount++]);
        W_CoolTime = Convert.ToSingle(listValue[wCount++]);
        A_Speed = Convert.ToSingle(listValue[wCount++]);
        A_ATK = Convert.ToSingle(listValue[wCount++]);
        A_ProjectileNo = Convert.ToInt32(listValue[wCount++]);
        A_ProjectileSpeed = Convert.ToSingle(listValue[wCount++]);
        A_Channeling = Convert.ToSingle(listValue[wCount++]);
        A_SkillSpeed = Convert.ToSingle(listValue[wCount++]);
        A_CoolTime = Convert.ToSingle(listValue[wCount++]);
        Wi_Speed = Convert.ToSingle(listValue[wCount++]);
        Wi_ATK = Convert.ToSingle(listValue[wCount++]);
        Wi_ProjectileNo = Convert.ToInt32(listValue[wCount++]);
        Wi_ProjectileSpeed = Convert.ToSingle(listValue[wCount++]);
        Wi_Channeling = Convert.ToSingle(listValue[wCount++]);
        Wi_SkillSpeed = Convert.ToSingle(listValue[wCount++]);
        Wi_CoolTime = Convert.ToSingle(listValue[wCount++]);
        N_Speed = Convert.ToSingle(listValue[wCount++]);
        N_ATK = Convert.ToSingle(listValue[wCount++]);
        N_ProjectileNo = Convert.ToInt32(listValue[wCount++]);
        N_ProjectileSpeed = Convert.ToSingle(listValue[wCount++]);
        N_Channeling = Convert.ToSingle(listValue[wCount++]);
        N_SkillSpeed = Convert.ToSingle(listValue[wCount++]);
        N_CoolTime = Convert.ToSingle(listValue[wCount++]);

        //HeroSpeed = Convert.ToSingle(listValue[wCount++]);
        //ProjectileSpeed = Convert.ToSingle(listValue[wCount++]);
        //CoolTime = Convert.ToSingle(listValue[wCount++]);
    }
}

