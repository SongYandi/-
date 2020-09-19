
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 文件：BattleMgr.cs
/// 功能：战斗管理器
/// </summary>
public class BattleMgr : MonoBehaviour 
{
    ResSvc resSvc = null;
    private PlayerController playerCtrl;

    private StateMgr stateMgr;
    private SkillMgr skillMgr;

    private EntityPlayer entityPlayer;
    public void Init()
    {
        resSvc = ResSvc.Instance;

        stateMgr = gameObject.AddComponent<StateMgr>();
        stateMgr.Init();
        skillMgr = gameObject.AddComponent<SkillMgr>();
        skillMgr.Init();
    }
    public void LoadPlayer(string playerName)
    {
        GameObject player = resSvc.LoadPrefab("PrefabPlayer/" + playerName, true);
        player.transform.position = new Vector3(-210, 28, 190);

        Camera.main.transform.position = new Vector3(-150, 115, 180);
        Camera.main.transform.eulerAngles = new Vector3(60, -90, 0);

        entityPlayer = new EntityPlayer
        {
            battleMgr = this,
            stateMgr = stateMgr,
            skillMgr = skillMgr
        };
        
        //加载玩家初始数据
        resSvc.InitPlayerCfg(playerName);
        resSvc.InitSkillCfg(playerName);
        playerCtrl = player.GetComponent<PlayerController>();
        playerCtrl.Init();
        entityPlayer.controller = playerCtrl;

    }
    public void SetMoveDir(Vector2 dir)
    {
        if (entityPlayer.canControl==true)
        {
            if (dir == Vector2.zero)
            {
                entityPlayer.Idle();
            }
            else
            {
                entityPlayer.Move();
            }
            playerCtrl.Dir = dir;
        }
        
    }
    public void ReleaseNomalAtk()
    {
        entityPlayer.Attack(1);
    }
    public void ReleaseSkill1()
    {
        //PECommon.Log("Click Skill1");
        entityPlayer.Attack(11);
    }
    public void ReleaseSkill2()
    {
        //PECommon.Log("Click Skill2");
        entityPlayer.Attack(12);
    }
    public void ReleaseSkill3()
    {
        //PECommon.Log("Click Skill3");
        entityPlayer.Attack(13);
    }
    public void ReleaseSkill4()
    {
        //PECommon.Log("Click Skill3");
        entityPlayer.Attack(14);
    }
    public Vector2 GetDirInput()
    {
        return BattleSys.Instance.GetDirInput();
    }
}
