using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 文件：Item.cs
/// 功能：物品信息
/// </summary>
public class Item 
{
    public int ID { get; set; }
    public string Name { get; set; }
    /// <summary>
    /// 物品描述
    /// </summary>
    public string Intrduce { get; set; }
    /// <summary>
    /// 购买价格
    /// </summary>
    public int BuyPrice { get; set; }
    /// <summary>
    /// 出售价格
    /// </summary>
    public int SellPrice { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    public string Sprite { get; set; }

    public int Coin { get; set; }
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int Ad { get; set; }
    public int Ap { get; set; }
    public float Critical { get; set; }
    public int Addef { get; set; }
    public int Apdef { get; set; }
    public float AttackSpeed { get; set; }
    public int MoveSpeed { get; set; }
    public Item()
    {
        ID = -1;
    }

    public Item(int id, string name, string intrduce, int capacity, int buyPrice, int sellPrice, string sprite,
        int coin,int hp,int mp,int ad,int ap,float critical,int addef,int apdef,float attackSpeed,int moveSpeed)
    {
        this.ID = id;
        this.Name = name;
        this.Intrduce = intrduce;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
        this.Sprite = sprite;
        this.Coin = coin;
        this.Hp = hp;
        this.Mp = mp;
        this.Ad = hp;
        this.Ap = hp;
        this.Critical = hp;
        this.Addef = addef;
        this.Apdef = apdef;
        this.AttackSpeed = attackSpeed;
        this.MoveSpeed = moveSpeed;
    }
    

}
