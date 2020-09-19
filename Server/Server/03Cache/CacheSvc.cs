using System.Collections.Generic;
using PEProtocol;

/// <summary>
/// 文件：CacheSvc.cs
/// 功能：缓存层
/// </summary>
public class CacheSvc 
{
    private static CacheSvc instance = null;
    public static CacheSvc Instance 
    {
        get {
            if (instance == null) 
            {
                instance = new CacheSvc();
            }
            return instance;
        }
    }
    public DBMgr dBMgr = null;

    private Dictionary<string, ServerSession> onLineAcctDic = new Dictionary<string, ServerSession>();
    //缓存账号相关的数据，当玩家数据存在字典里，下一次后就不会再从数据库中读取数据了
    private Dictionary<ServerSession, PlayerData> onLineSessionDic = new Dictionary<ServerSession, PlayerData>();

    public void Init() 
    {
        dBMgr = DBMgr.Instance;
        PECommon.Log("CacheSvc Init Done.");
    }

    public bool IsAcctOnLine(string acct)
    {
        return onLineAcctDic.ContainsKey(acct);
    }
    public bool IsAcctExist(string acct)
    {
        //判断账号是否存在
        return dBMgr.QueryAcctData(acct);
    }
    public bool IsNameExist(string name)
    {
        //判断姓名是否存在
        return dBMgr.QueryNameData(name);
    }

    /// <summary>
    /// 根据账号密码返回对应账号数据，  如果密码错误则返回null
    /// </summary>
    public PlayerData GetPlayerData(string acct, string pass) 
    {
        //TODO
        //从数据库中查找账号数据
        return dBMgr.QueryPlayerData(acct,pass);
    }

    /// <summary>
    /// 账号上线，缓存数据
    /// </summary>
    public void AcctOnline(string acct, ServerSession session, PlayerData playerData) 
    {
        onLineAcctDic.Add(acct, session);
        onLineSessionDic.Add(session, playerData);
    }
    
    public PlayerData GetPlayerDataBySession(ServerSession session)
    {
        if (onLineSessionDic.TryGetValue(session,out PlayerData playerData))
        {
            return playerData;
        }
        else
        {
            return null;
        }
    }
    public bool UpdatePlayerData(int id,PlayerData playerData)
    {
        return dBMgr.UpdatePlayerData(id, playerData); ;
    }
    public void AcctOffLine(ServerSession session)
    {
        foreach (var item in onLineAcctDic)
        {
            if (item.Value == session)
            {
                onLineAcctDic.Remove(item.Key);
                break;
            }
        }

        bool succ = onLineSessionDic.Remove(session);
        PECommon.Log("Offline Result:" + succ);
    }
}
