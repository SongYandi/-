using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 文件：MainWnd.cs
/// 功能：主客户端界面
/// </summary>
public class MainClientWnd : WindowRoot
{
    public Image imgHand;
    public Text txtName;
    public Text txtLv;
    public Text txtCoin;
    public Text txtDiamond;
    public Button btnRank;
    public Button btnStand;
    protected override void InitWnd()
    {
        base.InitWnd();
        txtName.text = GameRoot.Instance.PlayerData.name;
        txtLv.text = "Lv:" +GameRoot.Instance.PlayerData.lv.ToString();
        txtCoin.text = GameRoot.Instance.PlayerData.coin.ToString();
        txtDiamond.text = GameRoot.Instance.PlayerData.diamond.ToString();

    }
    public void ClickRankBtn()
    {
        PECommon.Log("进入天梯");
    }
    public void ClickStandBtn()
    {
        MainClientSys.Instance.EnterBattleScene();
        PECommon.Log("进入单机");
    }
}
