using UnityEngine;
using System;
[CreateAssetMenu(menuName = "RPG SetUp/Item Data/Consumable Item", fileName = "Consumable item data - ")]
public class ConsumableItemDataSO : ItemDataSO
{
    public ItemEffectDataSO[] itemEffects;
    public EffectDataSO[] effects;
}
