
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 文件：EntityBase.cs
/// 功能：逻辑实体基类
/// </summary>
public class EntityBase
{
    public AniState currentAniState = AniState.None;

    public BattleMgr battleMgr = null;
    public StateMgr stateMgr = null;
    public SkillMgr skillMgr = null;
    public Controller controller = null;

    public bool canControl = true;
    public bool canRlsSkill = true;


    public EntityType entityType = EntityType.None;

    public void Move()
    {
        stateMgr.ChangeStates(this, AniState.Move);
    }
    public void Idle()
    {
        stateMgr.ChangeStates(this, AniState.Idle);
    }
    public void Attack(int skillID)
    {
        stateMgr.ChangeStates(this, AniState.Attack, skillID);
    }
    public virtual void SetBlend(float blend)
    {
        if (controller!=null)
        {
            controller.SetBlend(blend);
        }
    }
    public virtual void SetAction(int act)
    {
        if (controller != null)
        {
            controller.SetAction(act);
        }
    }
    
    public void SkillAttack(int skillID)
    {
        skillMgr.SkillAttack(this, skillID);
    }

    public void ExitCurtSkill()
    {
        canControl = true;
        SetAction(Constants.ActionDefault);
    }
    public virtual void SetDir(Vector2 dir)
    {
        if (controller != null)
        {
            controller.Dir = dir;
        }
    }
    public virtual Vector2 GetDirInput()
    {
        return Vector2.zero;
    }
}
