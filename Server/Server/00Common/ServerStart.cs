
/// <summary>
/// 文件：ServerStart.cs
/// 功能：服务器入口
/// </summary>
class ServerStart {
    static void Main(string[] args) {
        ServerRoot.Instance.Init();

        while (true) {
            ServerRoot.Instance.Update();
        }
    }
}
