using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 文件：ChosePlayerWnd.cs
/// 功能：角色选择UI
/// </summary>
public class ChosePlayerWnd : WindowRoot 
{
    public Button btnPlayer;

    public static bool isChose = false;
    private string playerName;
    protected override void InitWnd()
    {
        base.InitWnd();

    }

    public void ClickPlayerUI()
    {
        playerName = btnPlayer.GetComponent<Image>().sprite.name;
        BattleSys.Instance.LoadPlayer(playerName);
        StoreSys.Instance.StartStore();
        isChose = true;
        this.SetWndState(false);
    }
    
}
