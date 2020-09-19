
using PENet;
using PEProtocol;
/// <summary>
/// 文件：ServerSession.cs
/// 功能：网络会话连接
/// </summary>
public class ServerSession : PESession<GameMsg> 
{
    protected override void OnConnected() 
    {
        PECommon.Log("Client Connect");
    }

    protected override void OnReciveMsg(GameMsg msg) 
    {
        PECommon.Log("RcvPack CMD:" + ((CMD)msg.cmd).ToString());
        NetSvc.Instance.AddMsgQue(this, msg);
    }

    protected override void OnDisConnected() 
    {
        LoginSys.Instance.ClearOfflineData(this);
        PECommon.Log("Client DisConnect");
    }
}
