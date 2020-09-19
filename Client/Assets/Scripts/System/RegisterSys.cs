using UnityEngine;
using System;
using System.Collections.Generic;
using PEProtocol;

/// <summary>
/// 文件：RegisterSys.cs
/// 功能：注册账号业务系统
/// </summary>
public class RegisterSys : SystemRoot 
{
    public static RegisterSys Instance = null;

    public RegisterWnd registerWnd;
    public LoginWnd loginWnd;
    public CreateWnd createWnd;
    public MainClientWnd mainClientWnd;

    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        PECommon.Log("Init RegisterSys...");
    }
    

    public void RspRegister(GameMsg msg)
    {
        GameRoot.AddTips("注册成功");
        GameRoot.Instance.SetPlayerData(msg.rspRegister);

        registerWnd.SetWndState(false);
        createWnd.SetWndState();
    }
    public void RspRename(GameMsg msg)
    {
        GameRoot.AddTips("登陆成功");
        GameRoot.Instance.SetPlayerData(msg.rspRename);
        //打开主界面
        mainClientWnd.SetWndState();
        createWnd.SetWndState(false);
    }
}