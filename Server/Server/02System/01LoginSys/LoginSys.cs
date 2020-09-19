
using PEProtocol;
/// <summary>
/// 文件：LoginSys.cs
/// 功能：登录业务系统
/// </summary>
public class LoginSys 
{
    private static LoginSys instance = null;
    public static LoginSys Instance 
    {
        get 
        {
            if (instance == null) 
            {
                instance = new LoginSys();
            }
            return instance;
        }
    }
    private CacheSvc cacheSvc = null;

    public void Init() 
    {
        cacheSvc = CacheSvc.Instance;
        PECommon.Log("LoginSys Init Done.");
    }

    public void ReqLogin(MsgPack pack)
    {
        ReqLogin data = pack.msg.reqLogin;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspLogin
        };
        //判断账号是否存在
        if (!cacheSvc.IsAcctExist(data.acct))
        {
            msg.err = (int)ErrorCode.WrongPass;
        }
        //判断账号是否已经在线
        else if (cacheSvc.IsAcctOnLine(data.acct))
        {
            msg.err = (int)ErrorCode.AcctIsOnline;
        }
        else
        {
            //未上线：
            //判断密码是否正确 
            PlayerData pd = cacheSvc.GetPlayerData(data.acct, data.pass);
            //密码错误
            if (pd == null) 
            {
                msg.err = (int)ErrorCode.WrongPass;
            }
            //密码正确
            else 
            {
                msg.rspLogin = new RspLogin
                {
                    playerData = pd
                };
                //缓存账号数据
                cacheSvc.AcctOnline(data.acct, pack.session, pd);
            }
        }
        //回应客户端
        pack.session.SendMsg(msg);
    }

    public void ClearOfflineData(ServerSession session)
    {
        PlayerData pd = cacheSvc.GetPlayerDataBySession(session);
        if (pd != null)
        {
            cacheSvc.AcctOffLine(session);
        }
    }
}
