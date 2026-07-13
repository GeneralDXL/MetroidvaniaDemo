using TMPro;
using UnityEngine;

public class UI_StatToolTip : UI_ToopTip
{
    [SerializeField] private TextMeshProUGUI statInfo;

    public void ShowToolTip(bool show, RectTransform targetRect, StatType type)
    {
        statInfo.text = GetTip(type);
        base.ShowToolTip(show, targetRect);
    }
    private string GetTip(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return "The total health you can have.";
            case StatType.HealthRegen: return "Amount of health per second restore";

            case StatType.Agility: return "Increase " + GetColoredText(UI.ColorsForStats[StatType.CritChance], "crit chance ") + "by 0.3%/point. \n" +
                    "Increase " + GetColoredText(UI.ColorsForStats[StatType.Evasion], "evasion ") + "by 0.5%/point.";
            case StatType.Intelligence: return "Increase " + GetColoredText(UI.ColorsForStats[StatType.ElementalDamage], "elemental damage bonus ") + "by 1/point. \n" +
                    "If your "+ GetColoredText(UI.ColorsForStats[StatType.ElementalDamage], "elemental damage ")+"is 0,then the bonus will not work.\n"+
                    "Increase " + GetColoredText(UI.ColorsForStats[StatType.Intelligence], "elemental resistance ") + "by 0.5%/point.";
            case StatType.Vitality: return "Increase " + GetColoredText(UI.ColorsForStats[StatType.Armor], "armor ") + "by 1/point. \n" +
                    "Increase " + GetColoredText(UI.ColorsForStats[StatType.MaxHealth], "max health ") + "by 5/point.";
            case StatType.Strength: return "Increase " + GetColoredText(UI.ColorsForStats[StatType.Damage],"physical damage ") + "by 1/point. \n" +
                    "Increase " + GetColoredText(UI.ColorsForStats[StatType.CritPower],"crit power " )+ "by 0.5%/point.";

            case StatType.Damage: return "Determines the "+ GetColoredText(UI.ColorsForStats[StatType.Damage], "physical damage ") + "of your attack.";
            case StatType.AttackSpeed: return "Determines the frequency of your attack.";
            case StatType.CritPower: return "Determines the power of your critical strike.";
            case StatType.CritChance: return "Determines the posibility you can perform critical strike.";
            case StatType.ArmorReduction: return "Percent of your attack can reduce the target's armor. ";

            case StatType.IceDamage: return "Determines the " + GetColoredText(UI.ColorsForStats[StatType.IceDamage], "ice damage ") + "of your attack.";
            case StatType.FireDamage: return "Determines the " + GetColoredText(UI.ColorsForStats[StatType.FireDamage], "fire damage ") + "of your attack.";
            case StatType.LightningDamage: return "Determines the " + GetColoredText(UI.ColorsForStats[StatType.LightningDamage], "lightning damage ") + "of your attack.";
            case StatType.ElementalDamage: return "Determines the " + GetColoredText(UI.ColorsForStats[StatType.ElementalDamage], "total elemental damage ") + "of your attack.";

            case StatType.Armor: return "Reduce the " + GetColoredText(UI.ColorsForStats[StatType.Damage], "physical damage ") + "that will recieve.\n " +
                    "The max damage reduction is 85%.";
            case StatType.Evasion: return "Chance to completely avoid the damage";

            case StatType.FireResistance: return "Reduce the " + GetColoredText(UI.ColorsForStats[StatType.FireResistance], "fire resistance ") + "that will recieve.\n " +
                    "The max damage reduction is 75%.";
            case StatType.LightningResistance: return "Reduce the " + GetColoredText(UI.ColorsForStats[StatType.LightningResistance], "lightning resistance ") + "that will recieve.\n " +
                    "The max damage reduction is 75%.";
            case StatType.IceResistance: return "Reduce the " + GetColoredText(UI.ColorsForStats[StatType.IceResistance], "ice resistance ") + "that will recieve.\n " +
                    "The max damage reduction is 75%.";
            default: return "";
        }
    }
}
