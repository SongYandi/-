
using PENet;
using PEProtocol;
using System.Collections.Generic;

/// <summary>
/// 文件：NetSvc.cs
/// 功能：网络服务
/// </summary>
public class NetSvc {
    private static NetSvc instance = null;
    public static NetSvc Instance {
        get {
            if (instance == null) {
                instance = new NetSvc();
            }
            return instance;
        }
    }
    public static readonly string obj = "lock";
    private Queue<MsgPack> msgPackQue = new Queue<MsgPack>();

    public void Init() {
        PESocket<ServerSession, GameMsg> server = new PESocket<ServerSession, GameMsg>();
        server.StartAsServer(SrvCfg.srvIP, SrvCfg.srvPort);

        PECommon.Log("NetSvc Init Done.");
        
    }

    public void AddMsgQue(ServerSession session, GameMsg msg) {
        lock (obj) {
            msgPackQue.Enqueue(new MsgPack(session, msg));
        }
    }

    public void Update() 
    {
        if (msgPackQue.Count > 0) 
        {
            PECommon.Log("QueCount:" + msgPackQue.Count);
            lock (obj) 
            {
                MsgPack pack = msgPackQue.Dequeue();
                HandOutMsg(pack);
            }
        }
    }

    private void HandOutMsg(MsgPack pack)
    {
        switch ((CMD)pack.msg.cmd)
        {
            case CMD.ReqLogin:
                LoginSys.Instance.ReqLogin(pack);
                break;
            case CMD.ReqRegister:
                RegisterSys.Instance.ReqRegister(pack);
                break;
            case CMD.ReqRename:
                RegisterSys.Instance.ReqRename(pack);
                break;
        }
    }
}

public class MsgPack
{
    public ServerSession session;
    public GameMsg msg;
    public MsgPack(ServerSession session, GameMsg msg)
    {
        this.session = session;
        this.msg = msg;
    }
}
