using PENet;
/// <summary>
/// 文件：PECommon.cs
/// 功能：客户端服务端共用工具类
/// </summary>
public class PECommon
{
    public static void Log(string msg = "", LogType tp = LogType.Log)
    {
        LogLevel lv = (LogLevel)tp;
        PETool.LogMsg(msg, lv);
    }
}

public enum LogType {
    Log = 0,
    Warn = 1,
    Error = 2,
    Info = 3
}
