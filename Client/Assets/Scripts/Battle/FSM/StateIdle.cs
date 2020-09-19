using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 文件：StateIdle.cs
/// 功能：待机状态
/// </summary>
public class StateIdle : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
        entity.currentAniState = AniState.Idle;
        entity.SetDir(Vector2.zero);
        PECommon.Log("Entity StateIdle");
    }

    public void Exit(EntityBase entity, params object[] args)
    {
        PECommon.Log("Exit StateIdle");
    }

    public void Process(EntityBase entity, params object[] args)
    {
        if (entity.entityType == EntityType.Player)
        {
            entity.canRlsSkill = true;
        }
        if (entity.GetDirInput()!=Vector2.zero)
        {
            entity.Move();
            entity.SetDir(entity.GetDirInput());
        }
        else
        {
            entity.SetBlend(Constants.BlendIdle);
        }
        PECommon.Log("Process StateIdle");
    }
}