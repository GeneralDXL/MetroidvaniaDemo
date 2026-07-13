using System;
using System.Collections.Generic;
using UnityEngine;
public class Inventory_Base : MonoBehaviour
{
    public List<Inventory_Item> itemList = new List<Inventory_Item>();
    public int maxInventorySize = 10;
    public event Action OnInventoryChanged;

    protected virtual void Awake()
    {
        
    }
    public bool CanAddItem() => itemList.Count < maxInventorySize;
    public bool CanAddStack(Inventory_Item item)
    {
        var items=itemList.FindAll(target => target.itemData==item.itemData);
        foreach(var itemInList in items)
        {
            if(itemInList.CanStack()) return true;
        }
        return false;
    }

    protected void InvokeInventoryChangeEvent() => OnInventoryChanged?.Invoke();
    public void AddItem(Inventory_Item item)
    {
        Inventory_Item itemInList = itemList.Find(itemFound => itemFound.itemData == item.itemData && itemFound.CanStack());
        if (itemInList == null)
        {
            itemList.Add(item);
        }
        else
        {
            itemInList.AddStack();
        }
        InvokeInventoryChangeEvent();
    }
    public void RemoveItem(Inventory_Item item)
    {
        // 直接移除传入的实例，避免多个相同 itemData 时误删
        if (itemList.Contains(item))
        {
            itemList.Remove(item);
            InvokeInventoryChangeEvent();
        }
    }
    public Inventory_Item FindItemInList(ItemDataSO itemData)
    {
        return itemList.Find(item =>  item.itemData == itemData);
    }
    
}
