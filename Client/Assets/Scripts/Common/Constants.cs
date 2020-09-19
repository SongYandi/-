using UnityEngine;

/// <summary>
/// 文件：Constants.cs
/// 功能：常量配置
/// </summary>
public class Constants
{
    //场景名称
    public const string SceneLogin = "SceneLogin";
    public const string SceneBattle = "SceneBattle";

    //音效名称
    public const string BGLogin = "bgLogin";
    public const string BGBattle = "bgBattle";

    //登录按钮音效
    public const string UILoginBtn = "uiLoginBtn";

    //常规UI点击音效
    public const string UIClickBtn = "uiClickBtn";
    
    public const int ScreenStandardWidth = 1334;
    public const int ScreenStandardHeight = 750;
    public const int ScreenOPDis = 90;

    public const int PlayerMoveSpeed = 100;
    public const int MonsterMoveSpeed = 7;

    //运动平滑加速度
    public const float AccelerSpeed = 5;

    //混合参数
    public const int BlendIdle = 0;
    public const int BlendMove = 1;


    public const int ActionDefault = -1;

    
}

public enum EntityType
{
    None,
    Player,
    Monster
}