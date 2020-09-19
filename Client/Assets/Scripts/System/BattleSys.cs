using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 文件：BattleSys.cs
/// 功能：战斗系统
/// </summary>
public class BattleSys : SystemRoot 
{
    public static BattleSys Instance = null;

    public BattleWnd battleWnd;
    public MainClientWnd mainClinetWnd;
    public PlayerInfoWnd infoWnd;

    public BattleMgr battleMgr;
    

    public override void InitSys()
    {
        base.InitSys();

        Instance = this;
        PECommon.Log("Init BattleSys...");
    }

    public void StartBattle()
    {
        GameObject go = new GameObject
        {
            name = "BattleRoot"
        };
        go.transform.SetParent(GameRoot.Instance.transform);
        battleMgr = go.AddComponent<BattleMgr>();
        battleMgr.Init();
    }

    public void LoadPlayer(string playerName)
    {
        battleMgr.LoadPlayer(playerName);
    }

    public void SetMoveDir(Vector2 dir)
    {
        battleMgr.SetMoveDir(dir);
    }
    public Vector2 GetDirInput()
    {
        return battleWnd.currentDir;
    }
    
}
