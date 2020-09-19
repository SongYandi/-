using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 文件：PlayerEntity.cs
/// 功能：玩家逻辑实体类
/// </summary>
public class EntityPlayer : EntityBase
{
    public EntityPlayer()
    {
        entityType = EntityType.Player;
    }

    public override Vector2 GetDirInput()
    {
        return battleMgr.GetDirInput();
    }
}