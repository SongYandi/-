using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 文件：TimerSvc.cs
/// 功能：
/// </summary>
public class TimerSvc : MonoBehaviour
{
    public static TimerSvc Instance = null;
    public void InitSvc()
    {
        Instance = this;
        PECommon.Log("Init TimerSvc...");

    }

    private void Update()
    {
        
    }

   

}