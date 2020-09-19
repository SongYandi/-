
using MySql.Data.MySqlClient;
using PEProtocol;
using System;
/// <summary>
/// 文件：DBMgr.cs
/// 功能：数据库管理层
/// </summary>
public class DBMgr 
{
    private static DBMgr instance = null;
    public static DBMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DBMgr();
            }
            return instance;
        }
    }
    private MySqlConnection conn;
    public void Init()
    {
        conn = new MySqlConnection("server=localhost;User Id=root;password=1999;Database=xuanjishengcun;Charset=utf8");
        conn.Open();

        PECommon.Log("DBMgr Init Done.");
    }

    public PlayerData QueryPlayerData(string acct,string pass)
    {
        PlayerData playerData = null;
        MySqlDataReader reader = null;
        try
        {
            MySqlCommand cmd = new MySqlCommand("select * from account where acct=@acct", conn);
            cmd.Parameters.AddWithValue("acct", acct);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string _pass = reader.GetString("pass");
                if (_pass.Equals(pass))
                {
                    //密码正确
                    playerData = new PlayerData
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        lv = reader.GetInt32("level"),
                        exp = reader.GetInt32("exp"),
                        coin = reader.GetInt32("coin"),
                        diamond = reader.GetInt32("diamond")
                    };
                }
            }
        }
        catch (Exception e)
        {
            PECommon.Log("查询角色账号密码时错误:" + e, LogType.Error);
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
        }
        return playerData;
    }
    public PlayerData CreateNewAcctData(string acct,string pass)
    {
        PlayerData playerData = new PlayerData
        {
            id=-1,
            name="",
            lv = 1,
            exp = 0,
            coin = 0,
            diamond = 0
        };
        playerData.id = InsertNewAcctData(acct, pass, playerData);
        return playerData;
    }
    private int InsertNewAcctData(string acct,string pass,PlayerData pd)
    {
        int id = -1;
        try
        {
            MySqlCommand cmd = new MySqlCommand("insert into account set acct = @acct,pass=@pass,name=@name,level=@level,exp=@exp,coin=@coin,diamond=@diamond", conn);
            cmd.Parameters.AddWithValue("acct", acct);
            cmd.Parameters.AddWithValue("pass", pass);
            cmd.Parameters.AddWithValue("name", pd.name);
            cmd.Parameters.AddWithValue("level", pd.lv);
            cmd.Parameters.AddWithValue("exp", pd.exp);
            cmd.Parameters.AddWithValue("coin", pd.coin);
            cmd.Parameters.AddWithValue("diamond", pd.diamond);
            cmd.ExecuteNonQuery();

            id = (int)cmd.LastInsertedId;
        }
        catch (Exception e)
        {
            PECommon.Log("新建账号数据有误：" + e, LogType.Error);
        }
        return id;
    }

    public bool QueryAcctData(string acct)
    {
        bool exist = false;
        MySqlDataReader reader = null;
        try
        {
            MySqlCommand cmd = new MySqlCommand("select * from account where acct = @acct", conn);
            cmd.Parameters.AddWithValue("acct", acct);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                exist = true;
            }
        }
        catch (Exception e)
        {
            PECommon.Log("查询账号是否 存在错误：" + e, LogType.Error);
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
        }
        return exist;
    }

    public bool QueryNameData(string name)
    {
        bool exist = false;
        MySqlDataReader reader = null;
        try
        {
            MySqlCommand cmd = new MySqlCommand("select * from account where name=@name",conn);
            cmd.Parameters.AddWithValue("name", name);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                exist = true;
            }
        }
        catch (Exception e)
        {
            PECommon.Log("查询名字是否存在 出错：" + e, LogType.Error);
        }
        finally
        {
            if (reader!=null)
            {
                reader.Close();
            }
        }
        return exist;
    }

    public bool UpdatePlayerData(int id,PlayerData pd)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand("update account set name=@name,level=@level,exp=@exp,coin=@coin,diamond=@diamond where id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("name", pd.name);
            cmd.Parameters.AddWithValue("level", pd.lv);
            cmd.Parameters.AddWithValue("exp", pd.exp);
            cmd.Parameters.AddWithValue("coin", pd.coin);
            cmd.Parameters.AddWithValue("diamond", pd.diamond);

            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            PECommon.Log("更新玩家信息出错：" + e, LogType.Error);
            return false;
        }
        return true;
    }
}