using System;
using PENet;

/// <summary>
/// 功能：网络通信协议（客户端服务端共用）
/// </summary>
namespace PEProtocol {
    [Serializable]
    public class GameMsg : PEMsg 
    {
        public ReqLogin reqLogin;
        public RspLogin rspLogin;
        public ReqRegister reqRegister;
        public RspRegister rspRegister;
        public ReqRename reqRename;
        public RspRename rspRename;
    }

    [Serializable]
    public class ReqLogin 
    {
        public string acct;
        public string pass;
    }
    [Serializable]
    public class RspLogin
    {
        public PlayerData playerData;
    }

    [Serializable]
    public class ReqRegister
    {
        public string acct;
        public string pass;
    }
    [Serializable]
    public class RspRegister
    {
        public PlayerData playerData;
    }
    [Serializable]
    public class ReqRename
    {
        public string name;
    }
    [Serializable]
    public class RspRename
    {
        public string name;
    }


    [Serializable]
    public class PlayerData 
    {
        public int id;
        public string name;
        public int lv;
        public int exp;
        public int coin;
        public int diamond;
        //TOADD
    }

    public enum ErrorCode
    {
        None = 0,//没有错误

        AcctIsOnline,//账号已经上线
        AcctIsExist,//账号已经存在
        NameIsExist,//名字已经存在
        WrongPass,//密码错误

        UpdateDBError,//更新数据库出错
    }

    public enum CMD 
    {
        None = 0,
        //登录相关 100
        ReqLogin = 101,
        RspLogin = 102,

        ReqRegister = 103,
        RspRegister = 104,

        ReqRename = 105,
        RspRename = 106,
    }

    public class SrvCfg 
    {
        public const string srvIP = "192.168.137.1";
        public const int srvPort = 17666;
    }
}
