using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private UI ui;
    private RectTransform rect;
    private Entity_Stats playerStats;

    [SerializeField]private StatType statType;
    [SerializeField] private TextMeshProUGUI statName;
    [SerializeField] private TextMeshProUGUI statValue;

    private void OnValidate()
    {
        gameObject.name = "UI_Stat - "+Entity_Stats.GetStatNameByType(statType);
    }
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        ui=GetComponentInParent<UI>();
        playerStats = FindFirstObjectByType<Player>().GetComponent<Entity_Stats>();
    }

    private void Start()
    {
        statName.text = UI_ToopTip.GetColoredText(UI.ColorsForStats[statType], Entity_Stats.GetStatNameByType(statType));
        UpdateStatSlotValue();
        
    }

    public void UpdateStatSlotValue()
    {
        Stat statToUpdate=playerStats.GetStatByType(statType);
        float value = 0f;
        switch(statType)
        {
            //pyhsical 
            case StatType.Damage:
                value = playerStats.GetBasicPhysicalDamage();
                break;
            case StatType.CritChance:
                value = playerStats.GetCritChance();
                break;
            case StatType.CritPower:
                value = playerStats.GetCritPower();
                break;
            case StatType.Armor:
                value = playerStats.GetArmor();
                break;
            case StatType.MaxHealth:
                value = playerStats.GetMaxHealth();
                break;
            case StatType.Evasion:
                value = playerStats.GetEvasion();
                break;
            case StatType.AttackSpeed:
                value = statToUpdate.GetBaseValue() * 100;
                break;
            case StatType.ArmorReduction:
                value = playerStats.GetArmorReduction() * 100;
                break;
            //elemental
            case StatType.ElementalDamage:
                value =playerStats.GetElementalDamage(out ElementType type);
                break;
            case StatType.IceResistance:
                value = playerStats.GetElementResistance(ElementType.Ice) * 100;
                break;
            case StatType.FireResistance:
                value = playerStats.GetElementResistance(ElementType.Fire) * 100;
                break;
            case StatType.LightningResistance:
                value = playerStats.GetElementResistance(ElementType.Lightning) * 100;
                break;
            default:
                value = statToUpdate.GetBaseValue();
                break;
        }
        bool isPercentage = Entity_Stats.IsPercentageStat(statType);
        string formattedValue = isPercentage
            ? value.ToString("F1")
            : value.ToString("F0");
        string newText = formattedValue + (isPercentage ? "%" : "");
        statValue.text = UI_ToopTip.GetColoredText(UI.ColorsForStats[statType],newText);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.statTooltip.ShowToolTip(true, rect, statType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statTooltip.ShowToolTip(false, rect);
    }
}
