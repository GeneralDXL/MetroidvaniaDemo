using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
    public Player player;
    public List<Inventory_EquipmentSlot> equipmentList;
    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }
    public void TryConsumeItem(Inventory_Item item)
    {
        if (!(item.itemData.itemType == ItemType.Comsumable)) return;
        for (int i = 0; i < item.itemEffects.Length; i++)
            item.itemEffects[i].Execute();
        for (int i = 0; i < item.effects.Length; i++)
            player.effectManager.AddEffect(new Effect(item.effects[i]));
        if (item.stackSize > 1)
        {
            item.RemoveStack();
            InvokeInventoryChangeEvent();
        }
        else RemoveItem(item);
        
    }

    public void TryEquipItem(Inventory_Item item)
    {
        if (!(item.itemData is EquipmentDataSO))
            return;
        
        List<Inventory_EquipmentSlot> matchSlots = equipmentList.FindAll(slot => slot.slotType == item.itemData.itemType);
        foreach (Inventory_EquipmentSlot slot in matchSlots)
        {
            if (!slot.HasItemEquipped())
            {
                EquipItem(item, slot);
                return;
            }
        }
        if (CanAddItem())
        {
            Inventory_EquipmentSlot slotToReplace = matchSlots[0];
            UnequipItem(slotToReplace.equippedItem);
            EquipItem(item, slotToReplace);
        }
        
    }

    private void EquipItem(Inventory_Item item, Inventory_EquipmentSlot slot)
    {
        slot.equippedItem = item;
        float savedHealthPercent = player.health.GetHealthPercent();
        slot.equippedItem.AddModifiers(player.stats);
        player.health.SetHealthToPercentage(savedHealthPercent);
        RemoveItem(item);
    }
    public void UnequipItem(Inventory_Item item)
    {
        
        if (!CanAddItem())
        {
            return;
        }
        float savedHealthPercent = player.health.GetHealthPercent();
        foreach (var equip in equipmentList)
        {
            if (equip.equippedItem == item)
            {
                item.RemoveModifiers(player.stats);
                equip.equippedItem = null;
                break;
            }
        }
        player.health.SetHealthToPercentage(savedHealthPercent);
        AddItem(item);
    }

}
