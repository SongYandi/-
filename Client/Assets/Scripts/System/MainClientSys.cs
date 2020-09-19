using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 文件：MainClientSys.cs
/// 功能：主客户端服务系统
/// </summary>
public class MainClientSys : SystemRoot
{
    public static MainClientSys Instance = null;

    public MainClientWnd mainClinetWnd;
    public BattleWnd battleWnd;
    public ChosePlayerWnd chosePlayerWnd;

    public override void InitSys()
    {
        Instance = this;

        base.InitSys();
    }

    public void EnterBattleScene()
    {
        resSvc.AsyncLoadScene(Constants.SceneBattle, () => 
        {
            PECommon.Log("Enter BattleScene.");

            //打开战斗场景UI，关闭客户端窗口
            battleWnd.SetWndState();
            mainClinetWnd.SetWndState(false);
            //加载选择角色UI界面
            chosePlayerWnd.SetWndState();
            //播放背景音乐
            audioSvc.PlayBGMusic(Constants.BGBattle);
            //进入场景
            BattleSys.Instance.StartBattle();
        });

    }
}