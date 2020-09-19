using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 文件：PlayerInfoWnd.cs
/// 功能：玩家信息面板
/// </summary>
public class PlayerInfoWnd : WindowRoot
{
    public Text level;
    public Text hp;
    public Text mp;
    public Text ad;
    public Text ap;
    public Text critical;
    public Text addef;
    public Text apdef;
    public Text attackSpeed;
    public Text moveSpeed;

    public Button btnClose;
    protected override void InitWnd()
    {
        base.InitWnd();
    }

    public void ClickInfoBtn()
    {
        BattlePlayerData data = GameRoot.Instance.BattlePlayerData;
        level.text = data.level.ToString();
        hp.text = data.hp.ToString();
        mp.text = data.mp.ToString();
        ad.text = data.ad.ToString();
        ap.text = data.ap.ToString();
        critical.text = data.critical.ToString();
        addef.text = data.addef.ToString();
        apdef.text = data.apdef.ToString();
        attackSpeed.text = data.attackSpeed.ToString();
        moveSpeed.text = data.moveSpeed.ToString();
    }
    public void ClickCloceBtn()
    {
        this.SetWndState(false);
    }
}