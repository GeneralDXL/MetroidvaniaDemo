using UnityEngine;
using System;
[Serializable]
public class Inventory_EquipmentSlot
{
    public ItemType slotType;
    public Inventory_Item equippedItem;

    public bool HasItemEquipped() => equippedItem != null && equippedItem.itemData != null;
}
