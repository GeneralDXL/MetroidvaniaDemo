using TMPro;
using UnityEngine;

public class UI_ItemToolTip : UI_ToopTip
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemInfo;
    [SerializeField] private TextMeshProUGUI itemModifiers;

    public override void ShowToolTip(bool show, RectTransform targetRect)
    {
        base.ShowToolTip(show, targetRect);
    }
    public void ShowToolTip(bool show, RectTransform targetRect, ItemDataSO itemData)
    {
        itemName.text = itemData.itemName;
        itemType.text = itemData.itemType.ToString();
        itemInfo.text = itemData.itemInfo;
        if (itemData is EquipmentDataSO )
        {
            EquipmentDataSO equipmentData = (EquipmentDataSO)itemData;
            itemModifiers.text = "";
            for (int i = 0; i < equipmentData.modifiers.Length; i++)
            {
                EffectModifier mod = equipmentData.modifiers[i];
                string textToAdd =Entity_Stats. GetStatNameByType(mod.statType) + " : + " + Entity_Stats.GetStatValueForUI(mod).ToString() +( Entity_Stats.IsPercentageStat(mod.statType) ? "%" : "");
                itemModifiers.text += GetColoredText(UI.ColorsForStats[mod.statType], textToAdd) + "\n";
            }
        }
        else if(itemData is ConsumableItemDataSO)
        {
            ConsumableItemDataSO consumable = (ConsumableItemDataSO)itemData;
            itemModifiers.text = "";
            for (int i = 0; i < consumable.effects.Length; i++)
            {
                for (int j = 0; j < consumable.effects[i].modifiers.Length; j++)
                {
                    EffectModifier mod = consumable.effects[i].modifiers[j];
                    string textToAdd = Entity_Stats.GetStatNameByType(mod.statType) + " : + " +Entity_Stats.GetStatValueForUI(mod).ToString() + (Entity_Stats.IsPercentageStat(mod.statType) ? "%" : "");
                    itemModifiers.text += GetColoredText(UI.ColorsForStats[mod.statType], textToAdd) + "\n";
                }
            }
        }
        else
        {
            itemModifiers.text = "";
        }

        ShowToolTip(show, targetRect);
    }

    

}
