using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private UI_ItemSlot[] uiItemSlots;
    private UI_EquipSlot[] uiEquipSlots;
    private Inventory_Player inventory;

    [SerializeField] private Transform itemSlotParent;
    [SerializeField] private Transform equipSlotParent;
    private void Awake()
    {
        uiItemSlots = itemSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        uiEquipSlots = equipSlotParent.GetComponentsInChildren<UI_EquipSlot>();
        inventory = FindFirstObjectByType<Inventory_Player>();
        inventory.OnInventoryChanged += UpdateSlots;
        UpdateSlots();
    }


    private void UpdateSlots()
    {
        UpdateEquipSlots();
        UpdateInventorySlots();
    }
    private void UpdateEquipSlots()
    {
        Dictionary<ItemType, Queue<UI_EquipSlot>> queues = new Dictionary<ItemType, Queue<UI_EquipSlot>>();
        for (int i = 0; i < uiEquipSlots.Length; i++)
        {
            if (!queues.ContainsKey(uiEquipSlots[i].slotType))
                queues.Add(uiEquipSlots[i].slotType, new Queue<UI_EquipSlot>());
            queues.TryGetValue(uiEquipSlots[i].slotType, out Queue<UI_EquipSlot> q);
            q.Enqueue(uiEquipSlots[i]);
        }
        var equipList = inventory.equipmentList;
        foreach(var equip in equipList)
        {
            var type = equip.slotType;
            queues.TryGetValue(type, out Queue<UI_EquipSlot> q);
            var uiSlot=q.Dequeue();
            if(equip.HasItemEquipped())
            {
                uiSlot.UpdateSlot(equip.equippedItem);
            }
            else
            {
                uiSlot.UpdateSlot(null);
            }
            q.Enqueue(uiSlot);
        }
    }
    private void UpdateInventorySlots()
    {
        List<Inventory_Item> itemList = inventory.itemList;
        for (int i = 0; i < uiItemSlots.Length; i++)
        {
            if (i < itemList.Count)
            {

                uiItemSlots[i].UpdateSlot(itemList[i]);
            }
            else
            {
                uiItemSlots[i].UpdateSlot(null);
            }
        }
    }
}
