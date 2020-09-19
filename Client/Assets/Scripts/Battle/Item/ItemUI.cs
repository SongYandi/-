
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 文件：ItemUI.cs
/// 功能：物品UI及信息
/// </summary>
public class ItemUI : WindowRoot 
{
    public int id;
    protected override void InitWnd()
    {
        base.InitWnd();
        Item item = resSvc.GetItemCfg(id);
    }
}
