using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 文件：BaseData.cs
/// 功能：配置数据类
/// </summary>
public class BaseData<T>
{
    public int ID;
}

public class BattleProps
{
    public int hp;
    public int ad;
    public int ap;
    public int addef;
    public int apdef;
    public int dodge;
    public int pierce;
    public int critical;
}

public class SkillCfg : BaseData<SkillCfg>
{
    public string release;
    public int adDamage;
    public int adDamageGrowth;
    public int apDamage;
    public int apDamageGrowth;
    public int consumeMP;
    public int consumeMPGrowth;
    public string skillName;
    public int cd;
}