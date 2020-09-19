
/// <summary>
/// 文件：StoreWnd.cs
/// 功能：商店界面
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreWnd : WindowRoot 
{
    public GameObject ADItems;
    public GameObject APItems;
    public GameObject DefItems;
    public GameObject SpeedItems;

    public Transform MyItems;

    public Text itemName;
    public Text itemPorp;
    public Text itemIntrduce;
    public Text itemPrice;
    public Text myCoin;

    public Button btnAd;
    public Button btnAp;
    public Button btnDef;
    public Button btnSpeed;
    public Button btnBuy;
    public Button btnSell;

    private GameObject nowOpenItems;
    private GameObject nowChoseItem;
    
    protected override void InitWnd()
    {
        base.InitWnd();
        nowOpenItems = ADItems;
        
    }

    public void ClickItemsTypeBtn(GameObject items)
    {
        nowOpenItems.SetActive(false);
        items.SetActive(true);
        nowOpenItems = items;
        ClearItemInfo();
    }
    public void ClickStoreItemBtn(int id)
    {
        btnBuy.gameObject.SetActive(true);
        btnSell.gameObject.SetActive(false);
        Item item = resSvc.GetItemCfg(id);
        Transform storeTrans = GameObject.FindWithTag("Store").GetComponent<Transform>();
        for (int i = 0; i < storeTrans.GetComponentsInChildren<Transform>().Length; i++)
        {
            if (storeTrans.GetComponentsInChildren<Transform>()[i].name.Equals(item.Sprite))
            {
                nowChoseItem = storeTrans.GetComponentsInChildren<Transform>()[i].gameObject;
            }
        }
        itemName.text = item.Name;
        itemIntrduce.text = item.Intrduce;
        itemPrice.text = item.BuyPrice.ToString();
    }
    public void ClickMyItemBtn(int index)
    {
        btnBuy.gameObject.SetActive(false);
        btnSell.gameObject.SetActive(true);

        nowChoseItem = MyItems.GetChild(index).gameObject;
        string name = nowChoseItem.GetComponent<Image>().sprite.name;
        Item item = resSvc.GetItemCfgByName(name);
        itemName.text = item.Name;
        itemIntrduce.text = item.Intrduce;
        itemPrice.text = item.SellPrice.ToString();
    }
    public void ClickBuyBtn()
    {
        Transform myItems = GameObject.FindWithTag("MyItems").GetComponent<Transform>();
        if (int.Parse(myCoin.text) <= int.Parse(itemPrice.text))
        {
            GameRoot.AddTips("金币不够");
            return;
        }
        if (nowChoseItem != null)
        {
            for (int i = 0; i < myItems.childCount; i++)
            {
                Sprite itemSprite = nowChoseItem.GetComponent<Image>().sprite;
                if (myItems.GetChild(i).GetComponent<Image>().sprite==null)
                {
                    myItems.GetChild(i).GetComponent<Image>().sprite = itemSprite;
                    Color color = new Color
                    {
                        a = 1,
                        r = 1,
                        b = 1,
                        g = 1
                    };
                    myItems.GetChild(i).GetComponent<Image>().color = color;
                    break;
                }
            }
            //扣除对应钱
            myCoin.text = (int.Parse(myCoin.text) - int.Parse(itemPrice.text)).ToString();
            //给角色加对应的装备属性
        }
    }
    public void ClickSellBtn()
    {
        if (nowChoseItem!=null)
        {
            Color color = new Color
            {
                a = 0,
                r=1,
                b=1,
                g=1
            };
            nowChoseItem.GetComponent<Image>().color = color;
            nowChoseItem.GetComponent<Image>().sprite = null;
            ClearItemInfo();
            //增加对应钱
            myCoin.text = (int.Parse(myCoin.text) + int.Parse(itemPrice.text)).ToString();
            //减少对应属性
        }
    }
    private void ClearItemInfo()
    {
        itemName.text = "";
        itemPrice.text = "";
        itemPorp.text = "";
        itemIntrduce.text = "";
    }
    public void ClickCloseBtn()
    {
        this.SetWndState(false);
    }

}
