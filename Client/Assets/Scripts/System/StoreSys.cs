using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// 文件：StoreSys.cs
/// 功能：商店系统
/// </summary>
public class StoreSys : SystemRoot
{
    public static StoreSys Instance = null;
    public StoreWnd storeWnd;
    
    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
    }
    
    /// <summary>
    /// 开启协程，每两秒增加一金币
    /// </summary>
    public void StartStore()
    {
        StartCoroutine(AddCoinBuySecond());
    }
    private IEnumerator AddCoinBuySecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            int mycoin = int.Parse(storeWnd.myCoin.text) + 1;
            storeWnd.myCoin.text = mycoin.ToString();
            Debug.Log(storeWnd.myCoin.text);
        }
    }
}