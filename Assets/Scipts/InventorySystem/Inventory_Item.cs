using System;
using UnityEditor;
using UnityEngine;
[Serializable]
public class Inventory_Item 
{
    public ItemDataSO itemData;
    public int stackSize = 1;
    private string itemID;
    public EffectModifier[] modifiers { get;private set; }
    public ItemEffectDataSO[] itemEffects {  get; private set; }
    public EffectDataSO[] effects { get; private set; }
    public Inventory_Item(ItemDataSO itemData)
    {
        this.itemData = itemData;
        itemID = itemData.itemName + Guid.NewGuid();
        EquipmentDataSO equipmentData = IsEquipment();
        modifiers = equipmentData?.modifiers;
        ConsumableItemDataSO consumable = IsConsumable();
        itemEffects = consumable?.itemEffects;
        effects = consumable?.effects;
    }


    public void AddModifiers(Entity_Stats playerStats)
    {
        foreach(var mod in modifiers)
        {
            Stat statToModify = playerStats.GetStatByType(mod.statType);
            statToModify.AddModifier(mod.value, itemID);
        }
    }

    public void RemoveModifiers(Entity_Stats playerStats)
    {
        foreach (var mod in modifiers)
        {
            Stat statToModify=playerStats.GetStatByType(mod.statType);
            statToModify.RemoveModifier(itemID);
        }
    }

    public ConsumableItemDataSO IsConsumable()
    {
        if(itemData is  ConsumableItemDataSO)
            return (ConsumableItemDataSO)itemData;
        return null;
    }
    public EquipmentDataSO IsEquipment()
    {
        if(itemData is EquipmentDataSO equipment)
            return equipment;
        return null;
    }
    public bool CanStack() => stackSize < itemData.maxStackSize;
    public void AddStack()=> stackSize++;
    public void RemoveStack()=> stackSize--;
}
