
using PEProtocol;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 文件：CreateWnd.cs
/// 功能：角色创建界面
/// </summary>
public class CreateWnd : WindowRoot
{
    public InputField iptName;
    public Button btnSure;

    protected override void InitWnd()
    {
        base.InitWnd();

        iptName.text = "";

    }


    public void ClickSureBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);

        if (iptName.text!="")
        {
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqRename,
                reqRename = new ReqRename
                {
                    name = iptName.text
                }
            };
            netSvc.SendMsg(msg);
        }
        else
        {
            GameRoot.AddTips("当前姓名不能为空");
        }
    }
}