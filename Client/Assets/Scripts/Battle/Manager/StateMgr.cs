using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 文件：StateMgr.cs
/// 功能：状态管理器
/// </summary>
public class StateMgr : MonoBehaviour
{
    private Dictionary<AniState, IState> fsm = new Dictionary<AniState, IState>();
    public void Init()
    {
        fsm.Add(AniState.Idle, new StateIdle());
        fsm.Add(AniState.Move, new StateMove());
        fsm.Add(AniState.Attack, new StateAttack());
    }

    public void ChangeStates(EntityBase entity,AniState targetState,params object[] args)
    {
        if (entity.currentAniState == targetState)
        {
            return;
        }
        if (fsm.ContainsKey(targetState))
        {
            if (entity.currentAniState!=AniState.None)
            {
                fsm[entity.currentAniState].Exit(entity,args);
            }
            fsm[targetState].Enter(entity, args);
            fsm[targetState].Process(entity, args);
        }
    }
}