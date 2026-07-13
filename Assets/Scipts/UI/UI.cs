using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillToolTip skillToolTip;
    public UI_SkillTree skillTree;
    public UI_ItemToolTip itemToolTip;
    public UI_StatToolTip statTooltip;
    public UI_Inventory inventory;
    [Header("Colors for Stats")]
    public Color ChoosePool;
    public static Dictionary<StatType,string> ColorsForStats=new Dictionary<StatType,string>();

    private bool skillTreeEnabled;
    private bool inventoryEnabled;
    private void Awake()
    {
        skillToolTip=GetComponentInChildren<UI_SkillToolTip>();
        skillTree = GetComponentInChildren<UI_SkillTree>(true);
        inventory = GetComponentInChildren<UI_Inventory>(true);
        itemToolTip=GetComponentInChildren<UI_ItemToolTip>();
        statTooltip=GetComponentInChildren<UI_StatToolTip>();

        skillTreeEnabled = skillTree.gameObject.activeSelf;
        inventoryEnabled = inventory.gameObject.activeSelf;

        InitializeColorsForStats();
    }
    private static void InitializeColorsForStats()
    {
        ColorsForStats.Add(StatType.MaxHealth, "#7BE610");
        ColorsForStats.Add(StatType.HealthRegen, "#7BE610");

        ColorsForStats.Add(StatType.Agility, "#FFDE5D");
        ColorsForStats.Add(StatType.Intelligence, "#2E6FAC");
        ColorsForStats.Add(StatType.Strength, "#A52723");
        ColorsForStats.Add(StatType.Vitality, "#7BE610");

        ColorsForStats.Add(StatType.Damage, "#818280");
        ColorsForStats.Add(StatType.CritChance, "#FF0800");
        ColorsForStats.Add(StatType.CritPower, "#FF0800");
        ColorsForStats.Add(StatType.ArmorReduction, "#793304");
        ColorsForStats.Add(StatType.AttackSpeed, "#FFDE5D");

        ColorsForStats.Add(StatType.IceDamage, "#59A9F4");
        ColorsForStats.Add(StatType.FireDamage, "#FB8D0B");
        ColorsForStats.Add(StatType.LightningDamage, "#FFE200");
        ColorsForStats.Add(StatType.ElementalDamage, "#2E6FAC");

        ColorsForStats.Add(StatType.IceResistance, "#3A84CB");
        ColorsForStats.Add(StatType.FireResistance, "#C9821D");
        ColorsForStats.Add(StatType.LightningResistance, "#C7B100");

        ColorsForStats.Add(StatType.Armor, "#A67A23");
        ColorsForStats.Add(StatType.Evasion, "#818280");
        
    }
    
    public void ToggleSkillTree()
    {
        skillTreeEnabled = !skillTreeEnabled;
        skillTree.gameObject.SetActive(skillTreeEnabled);
        skillToolTip.ShowToolTip(false, null);
    }
    public void ToggleInventory()
    {
        inventoryEnabled = !inventoryEnabled;
        inventory.gameObject.SetActive(inventoryEnabled);
        itemToolTip.ShowToolTip(false,null);
        statTooltip.ShowToolTip(false,null);
    }
}
