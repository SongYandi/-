using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using LitJson;

/// <summary>
/// 文件：ResSvc.cs
/// 功能：资源加载服务
/// </summary>
public class ResSvc : MonoBehaviour
{
    public static ResSvc Instance = null;

    public void InitSvc()
    {
        Instance = this;

        InitItemCfg();
        PECommon.Log("Init ResSvc...");
    }


    private Action prgCB = null;
    public void AsyncLoadScene(string sceneName, Action loaded)
    {
        GameRoot.Instance.loadingWnd.SetWndState();

        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName);
        prgCB = () =>
        {
            float val = sceneAsync.progress;
            GameRoot.Instance.loadingWnd.SetProgress(val);
            if (val == 1)
            {
                if (loaded != null)
                {
                    loaded();
                }
                prgCB = null;
                sceneAsync = null;
                GameRoot.Instance.loadingWnd.SetWndState(false);
            }
        };
    }

    private void Update()
    {
        if (prgCB != null)
        {
            prgCB();
        }
    }

    private Dictionary<string, AudioClip> adDic = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string path, bool cache = false)
    {
        AudioClip au = null;
        if (!adDic.TryGetValue(path, out au))
        {
            au = Resources.Load<AudioClip>(path);
            if (cache)
            {
                adDic.Add(path, au);
            }
        }
        return au;
    }

    private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();
    public GameObject LoadPrefab(string path, bool cache = false)
    {
        GameObject prefab = null;
        if (!goDic.TryGetValue(path, out prefab))
        {
            prefab = Resources.Load<GameObject>(path);
            if (cache)
            {
                goDic.Add(path, prefab);
            }
        }

        GameObject go = null;
        if (prefab != null)
        {
            go = Instantiate(prefab);
        }
        return go;
    }

    public List<Dictionary<string, object>> attDic = new List<Dictionary<string, object>>();
    public void InitPlayerCfg(string name)
    {
        List<Dictionary<string, object>> jsonData = JsonMapper.ToObject<List<Dictionary<string, object>>>(Resources.Load<TextAsset>("ResCfgs/人物基础属性").ToString());
        BattlePlayerData playerData = new BattlePlayerData();
        foreach (var item in jsonData)
        {
            if (item["heroName"].Equals(name))
            {
                playerData.level = int.Parse(item["level"].ToString());
                playerData.exp = int.Parse(item["exp"].ToString());
                playerData.coin = int.Parse(item["coin"].ToString());
                playerData.hp = int.Parse(item["hp"].ToString());
                playerData.hpGrowth = int.Parse(item["hpGrowth"].ToString());
                playerData.mp = int.Parse(item["mp"].ToString());
                playerData.mpGrowth = int.Parse(item["mpGrowth"].ToString());
                playerData.ad = int.Parse(item["ad"].ToString());
                playerData.adGrowth = int.Parse(item["adGrowth"].ToString());
                playerData.ap = int.Parse(item["ap"].ToString());
                playerData.critical = float.Parse(item["critical"].ToString());
                playerData.addef = int.Parse(item["addef"].ToString());
                playerData.addefGrowth = int.Parse(item["addefGrowth"].ToString());
                playerData.apdef = int.Parse(item["apdef"].ToString());
                playerData.apdefGrowth = int.Parse(item["apdefGrowth"].ToString());
                playerData.attackSpeed = float.Parse(item["attackSpeed"].ToString());
                playerData.moveSpeed = int.Parse(item["moveSpeed"].ToString());
            }
        }
        GameRoot.Instance.SetBattlePlayerData(playerData);
    }

    public Dictionary<string, List<Dictionary<string, object>>> skillDic = new Dictionary<string, List<Dictionary<string, object>>>();
    public List<Dictionary<string, object>> lstSkillData = new List<Dictionary<string, object>>();
    public void InitSkillCfg(string name)
    {
        Dictionary<string, List<Dictionary<string, object>>> jsonData = JsonMapper.ToObject<Dictionary<string, List<Dictionary<string, object>>>>(Resources.Load<TextAsset>("ResCfgs/技能配置").ToString());
        jsonData.TryGetValue(name, out lstSkillData);
        SkillCfg skillCfg = new SkillCfg();
        skillDic.Add(name, lstSkillData);
        foreach (var item in lstSkillData)
        {
            foreach (var it in item)
            {
                Debug.Log(it.Key + "..." + it.Value);
            }
        }
    }
    public SkillCfg GetSkillCfg(int skillID)
    {
        object id = 0;
        SkillCfg skillCfg = new SkillCfg();
        foreach (var item in lstSkillData)
        {
            item.TryGetValue("SkillID", out id);
            if (int.Parse(id.ToString()).Equals(skillID))
            {
                
                //获得技能伤害信息
                
                break;
            }
        }
        return skillCfg;
    }

    private Dictionary<int, Item> itemCfgDic = new Dictionary<int, Item>();
    private Dictionary<string, Item> itemCfgDicByName = new Dictionary<string, Item>();
    private void InitItemCfg()
    {
        List<Dictionary<string, object>> jsonData = JsonMapper.ToObject<List<Dictionary<string, object>>>(Resources.Load<TextAsset>("ResCfgs/装备配置").ToString());
        
        if (jsonData!=null)
        {
            foreach (var data in jsonData)
            {
                object id;
                data.TryGetValue("ID", out id);
                Item item = new Item()
                {
                    ID = (int)id
                };
                foreach (var key in data.Keys)
                {
                    object temp;
                    switch (key)
                    {
                        case "Name":
                            data.TryGetValue(key, out temp);
                            item.Name = temp.ToString();
                            break;
                        case "Description":
                            data.TryGetValue(key, out temp);
                            item.Intrduce = temp.ToString();
                            break;
                        case "BuyPrice":
                            data.TryGetValue(key, out temp);
                            item.BuyPrice = (int)temp;
                            break;
                        case "SellPrice":
                            data.TryGetValue(key, out temp);
                            item.SellPrice = (int)temp;
                            break;
                        case "Sprite":
                            data.TryGetValue(key, out temp);
                            item.Sprite = temp.ToString();
                            break;
                        case "Hp":
                            data.TryGetValue(key, out temp);
                            item.Hp = (int)temp;
                            break;
                        case "Mp":
                            data.TryGetValue(key, out temp);
                            item.Mp = (int)temp;
                            break;
                        case "Ad":
                            data.TryGetValue(key, out temp);
                            item.Ad = (int)temp;
                            break;
                        case "Ap":
                            data.TryGetValue(key, out temp);
                            item.Ap = (int)temp;
                            break;
                        case "Critical":
                            data.TryGetValue(key, out temp);
                            item.Critical = float.Parse(temp.ToString());
                            break;
                        case "Addef":
                            data.TryGetValue(key, out temp);
                            item.Addef = (int)temp;
                            break;
                        case "Apdef":
                            data.TryGetValue(key, out temp);
                            item.Apdef = (int)temp;
                            break;
                        case "AttackSpeed":
                            data.TryGetValue(key, out temp);
                            int b=1;
                            object a = b; 
                            item.AttackSpeed = float.Parse(temp.ToString());
                            break;
                        case "MoveSpeed":
                            data.TryGetValue(key, out temp);
                            item.MoveSpeed = (int)temp;
                            break;
                    }
                }
                itemCfgDic.Add((int)id, item);
                itemCfgDicByName.Add(item.Sprite, item);
            }
        } 
    }
    public Item GetItemCfg(int id)
    {
        Item item = new Item();
        itemCfgDic.TryGetValue(id, out item);
        return item;
    }
    public Item GetItemCfgByName(string name)
    {
        Item item = new Item();
        itemCfgDicByName.TryGetValue(name,out item);
        return item;
    }
}