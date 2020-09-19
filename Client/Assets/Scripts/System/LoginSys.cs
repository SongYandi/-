
using PEProtocol;
using UnityEngine;

/// <summary>
/// 文件：LoginSys.cs
/// 功能：登录业务系统
/// </summary>
public class LoginSys : SystemRoot
{
    public static LoginSys Instance = null;

    public LoginWnd loginWnd;
    public RegisterWnd registerWnd;
    public CreateWnd createWnd;
    public MainClientWnd mainClientWnd;

    public override void InitSys()
    {
        base.InitSys();

        Instance = this;
        PECommon.Log("Init LoginSys...");
    }

    /// <summary>
    /// 进入登录场景
    /// </summary>
    public void EnterLogin()
    {
        //异步的加载登录场景
        //并显示加载的进度
        resSvc.AsyncLoadScene(Constants.SceneLogin, () => 
        {
            //加载完成以后再打开注册登录界面
            loginWnd.SetWndState();
            audioSvc.PlayBGMusic(Constants.BGLogin);
        });
    }

    public void RspLogin(GameMsg msg)
    {
        GameRoot.AddTips("登录成功");
        GameRoot.Instance.SetPlayerData(msg.rspLogin);

        if (msg.rspLogin.playerData.name.Equals(""))
        {
            createWnd.SetWndState();
        }
        else
        {
            //打开主客户端
            mainClientWnd.SetWndState();
        }

        //关闭登录界面
        loginWnd.SetWndState(false);
    }
}