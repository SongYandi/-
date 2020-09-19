using PEProtocol;
using UnityEngine;

/// <summary>
/// 文件：GameRoot.cs
/// 功能：游戏启动入口
/// </summary>
public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance = null;

    public LoadingWnd loadingWnd;
    public DynamicWnd dynamicWnd;

    private void Start()
    {
        Instance = this;

        DontDestroyOnLoad(this);
        PECommon.Log("Game Start...");

        ClearUIRoot();

        Init();
    }
    private void Init()
    {
        //服务模块初始化
        NetSvc net = GetComponent<NetSvc>();
        net.InitSvc();
        ResSvc res = GetComponent<ResSvc>();
        res.InitSvc();
        AudioSvc audio = GetComponent<AudioSvc>();
        audio.InitSvc();


        //业务系统初始化
        LoginSys login = GetComponent<LoginSys>();
        login.InitSys();
        RegisterSys register = GetComponent<RegisterSys>();
        register.InitSys();

        MainClientSys mainClient = GetComponent<MainClientSys>();
        mainClient.InitSys();

        //战斗系统初始化
        BattleSys battle = GetComponent<BattleSys>();
        battle.InitSys();
        //商店系统初始化
        StoreSys store = GetComponent<StoreSys>();
        store.InitSys();

        //进入登录场景并加载相应UI
        login.EnterLogin();
    }

    private void ClearUIRoot()
    {
        Transform canvas = transform.Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }

        dynamicWnd.SetWndState();
    }
    
    public static void AddTips(string tips)
    {
        Instance.dynamicWnd.AddTips(tips);
    }


    private PlayerData playerData = null;
    public PlayerData PlayerData
    {
        get
        {
            return playerData;
        }
    }
    public void SetPlayerData(RspLogin data)
    {
        playerData = data.playerData;
    }
    public void SetPlayerData(RspRegister data)
    {
        playerData = data.playerData;
    }
    public void SetPlayerData(RspRename data)
    {
        playerData.name = data.name;
    }

    private BattlePlayerData battlePlayerData = null;
    public BattlePlayerData BattlePlayerData
    {
        get
        {
            return battlePlayerData;
        }
    }
    public void SetBattlePlayerData(BattlePlayerData data)
    {
        battlePlayerData = data;
    }
}