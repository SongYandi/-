using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 文件：BattleWnd.cs
/// 功能：战斗UI
/// </summary>
public class BattleWnd : WindowRoot 
{
    public Image imgDirBg;
    public Image imgDirPoint;

    private float pointDis;
    private Vector2 startPos;

    public Vector2 currentDir;
    public Transform expPrgTrans;
        
    protected override void InitWnd()
    {
        base.InitWnd();
        pointDis = Screen.height * 1.0f / Constants.ScreenStandardHeight * Constants.ScreenOPDis;
        startPos = imgDirPoint.transform.position;
        
        RegisterTouchEvts();

        PECommon.Log("BattleWnd InitWnd.");
    }

    private void RefreshUI()
    {
        GridLayoutGroup grid = expPrgTrans.GetComponent<GridLayoutGroup>();
        float globalRate = 1.0F * Constants.ScreenStandardHeight / Screen.height;
        float screenWidth = Screen.width * globalRate;
        float width = (screenWidth - 234) / 10;
        grid.cellSize = new Vector2(width, 10);
        
        BattlePlayerData data = GameRoot.Instance.BattlePlayerData;
        int num = (int)(data.exp * 1.0f / GetExpUpValByLv(data.level) * 100 / 10);
        for (int i = 0; i < expPrgTrans.childCount; i++)
        {
            Image img = expPrgTrans.GetChild(i).GetComponent<Image>();
            if (i<num)
            {
                img.fillAmount = 1;
            }
            else if (i>num)
            {
                img.fillAmount = 0;
            }
            else
            {
                img.fillAmount = (data.exp - GetExpUpValByLv(data.level) * 1.0f / 10 * num) / (GetExpUpValByLv(data.level) * 1.0f / 10 * num);
                img.fillAmount = data.exp / (GetExpUpValByLv(data.level) * 1.0f / 10 * num) - 1;
            }
        }
    }
    private int GetExpUpValByLv(int lv)
    {
        return 100 * lv * lv;

    }
    private void Update()
    {
        if (ChosePlayerWnd.isChose)
        {
            RefreshUI();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ClickNomalAtkBtn();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ClickSkill1Btn();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ClickSkill2Btn();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ClickSkill3Btn();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClickSkill4Btn();
        }
    }

    public void RegisterTouchEvts()
    {
        imgDirBg.gameObject.AddComponent<PEListener>();
        OnClickDown(imgDirBg.gameObject, (PointerEventData evt) =>
         {
             imgDirPoint.transform.position = evt.position;
         });
        OnClickUp(imgDirBg.gameObject, (PointerEventData evt) =>
         {
             imgDirPoint.transform.localPosition = Vector2.zero;
             currentDir = Vector2.zero;
             //TODO将方向信息传递出去
             BattleSys.Instance.SetMoveDir(currentDir);

         });
        OnDrag(imgDirBg.gameObject, (PointerEventData evt) =>
        {
            Vector2 dir = evt.position - startPos;
            float len = dir.magnitude;
            if (len > pointDis)
            {
                Vector2 clampDir = Vector2.ClampMagnitude(dir, pointDis);
                imgDirPoint.transform.position = startPos + clampDir;
            }
            else
            {
                imgDirPoint.transform.position = evt.position;
            }
            currentDir = dir.normalized;
            //TODO将方向信息传递出去
             BattleSys.Instance.SetMoveDir(currentDir);

        });
    }

    public void ClickInfoBtn()
    {
        BattleSys.Instance.infoWnd.SetWndState();
        BattleSys.Instance.infoWnd.ClickInfoBtn();
    }
    public void ClickNomalAtkBtn()
    {
        BattleSys.Instance.battleMgr.ReleaseNomalAtk();
    }
    public void ClickSkill1Btn()
    {
        BattleSys.Instance.battleMgr.ReleaseSkill1();
    }
    public void ClickSkill2Btn()
    {
        BattleSys.Instance.battleMgr.ReleaseSkill2();
    }
    public void ClickSkill3Btn()
    {
        BattleSys.Instance.battleMgr.ReleaseSkill3();
    }
    public void ClickSkill4Btn()
    {
        BattleSys.Instance.battleMgr.ReleaseSkill4();
    }
    public void ClickStoreBtn()
    {
        StoreSys.Instance.storeWnd.SetWndState();
    }
}
