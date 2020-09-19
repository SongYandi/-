using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// 文件：SkillMgr.cs
/// 功能：技能管理器
/// </summary>
public class SkillMgr : MonoBehaviour
{
    private ResSvc resSvc;
    public void Init()
    {
        resSvc = ResSvc.Instance;
    }

    public void SkillAttack(EntityBase entity,int skillID)
    {
        AttackDamage(entity, skillID);
        AttackEffect(entity, skillID);
    }

    public void AttackDamage(EntityBase entity, int skillID)
    {
        
    }

    /// <summary>
    /// 攻击特效和动画
    /// </summary>
    public void AttackEffect(EntityBase entity,int skillID)
    {
        entity.canControl = false;
        entity.SetDir(Vector2.zero);
        entity.SetAction(skillID);
        StartCoroutine(Delay(entity));

    }
    IEnumerator Delay(EntityBase entity)
    {
        yield return new WaitForSeconds(1f);
        entity.Idle();
    }
}