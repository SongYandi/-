using PEProtocol;

/// <summary>
/// 文件：RegisterSys.cs
/// 功能：注册业务系统
/// </summary>
public class RegisterSys 
{
    private static RegisterSys instance = null;
    public static RegisterSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new RegisterSys();
            }
            return instance;
        }
    }
    private CacheSvc cacheSvc = null;

    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        PECommon.Log("RegisterSys Init Done.");
    }

    public void ReqRegister(MsgPack pack)
    {
        ReqRegister data = pack.msg.reqRegister;
        
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspRegister
        };
        //判断账号是否存在
        if (cacheSvc.IsAcctExist(data.acct))
        {
            //当前账号已存在
            msg.err = (int)ErrorCode.AcctIsExist;
        }
        else
        {
            //账号可以注册
            PlayerData pd = cacheSvc.dBMgr.CreateNewAcctData(data.acct, data.pass);
            msg.rspRegister = new RspRegister
            {
                playerData = pd
            };
            //缓存账号数据
            cacheSvc.AcctOnline(data.acct, pack.session, pd);
        }
        //回应客户端
        pack.session.SendMsg(msg);
    }

    public void ReqRename(MsgPack pack)
    {
        ReqRename data = pack.msg.reqRename;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspRename
        };
        if (cacheSvc.IsNameExist(data.name))
        {
            msg.err = (int)ErrorCode.NameIsExist;
        }
        else
        {
            //更新缓存、数据库、传回客户端
            PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
            playerData.name = data.name;

            if (!cacheSvc.UpdatePlayerData(playerData.id,playerData))
            {
                msg.err = (int)ErrorCode.UpdateDBError;
            }
            else
            {
                msg.rspRename = new RspRename
                {
                    name = data.name
                };
            }
        }
        pack.session.SendMsg(msg);
    }

}