
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 文件：Istate.cs
/// 功能：状态接口
/// </summary>
public interface IState
{
    void Enter(EntityBase entity, params object[] args);//可变参数

    void Process(EntityBase entity, params object[] args);

    void Exit(EntityBase entity, params object[] args);
}

public enum AniState
{
    None,
    Idle,
    Move,
    Attack,

}
   