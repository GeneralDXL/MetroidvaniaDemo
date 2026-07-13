using UnityEngine;
using System;
[CreateAssetMenu(menuName = "RPG SetUp/Item Data/Equipment item", fileName = "Equipment data - ")]
public class EquipmentDataSO : ItemDataSO
{
    [Header("Stat Modifiers")]
    public EffectModifier[] modifiers;
}

[Serializable]
public class EffectModifier
{
    public StatType statType;
    public float value;
}
