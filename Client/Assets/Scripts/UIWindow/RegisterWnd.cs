
using UnityEngine.UI;
using PEProtocol;

/// <summary>
/// 文件：RegisterWnd.cs
/// 功能：注册界面
/// </summary>
public class RegisterWnd : WindowRoot
{
    public InputField iptAcct;
    public InputField iptPass;
    public Button btnRegister;

    protected override void InitWnd()
    {
        base.InitWnd();
        iptAcct.text = "";
        iptPass.text = "";
        
    }


    public void ClickRegistBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        string _acct = iptAcct.text;
        string _pass = iptPass.text;
        if (_acct != "" && _pass != "")
        {
            //TODO 发送网络消息，请求注册

            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqRegister,
                reqRegister = new ReqRegister
                {
                    acct = _acct,
                    pass = _pass
                }
            };
            netSvc.SendMsg(msg);
        }
        else
        {
            GameRoot.AddTips("账号或密码不能为空");
        }

    }
}