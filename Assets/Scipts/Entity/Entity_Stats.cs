using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat_SetUpSO defaultStatSetup;
    public Stat_ResourceGroup resouces;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;
    public Stat_MajorGroup major;

    public AttackData GetAttackData(DamageScaleData scaleData)
    {
        return new AttackData(this, scaleData);
    }
    public float GetMaxHealth()
    {
        float baseHp = resouces.maxHealth.GetBaseValue();
        float bonusHp = major.vitality.GetBaseValue() * 5;
        float finalHp = baseHp + bonusHp;
        return finalHp;
    }
    public float GetEvasion()
    {
        float baseEvasion = defense.evasion.GetBaseValue();
        float bonusEvasion = major.agility.GetBaseValue() * 0.5f;
        float totalEvasion = baseEvasion + bonusEvasion;
        float maxEvasion = 85;
        float fianalEvasion = Mathf.Clamp(totalEvasion, 0, maxEvasion);
        return fianalEvasion;
    }

    public float GetArmorMitigation(float armorReduction)
    {
        float baseArmor = defense.armor.GetBaseValue();
        float bonusArmor = major.vitality.GetBaseValue();
        float totalArmor = baseArmor + bonusArmor;

        float armormultiplier = Mathf.Clamp01(1 - armorReduction);
        float finalArmor = totalArmor * armormultiplier;

        float mitigaiton = finalArmor / (finalArmor + 100);
        float maxMitigation = 0.85f;

        float finalMitigation = Mathf.Clamp(mitigaiton, 0, maxMitigation);
        return finalMitigation;
    }

    public float GetElementResistance(ElementType type)
    {
        float resistrance = 0;
        switch (type)
        {
            case ElementType.Fire:
                resistrance = defense.fireRes.GetBaseValue(); break;
            case ElementType.Ice:
                resistrance = defense.iceRes.GetBaseValue(); break;
            case ElementType.Lightning:
                resistrance = defense.lightningRes.GetBaseValue(); break;
            default:
                resistrance = 0;
                break;
        }
        float maxRes = 75;
        float finalRes = Mathf.Clamp(resistrance, 0, maxRes);
        return finalRes / 100;
    }
    public float GetElementalDamage(out ElementType type, float scale = 1f)
    {
        float fire = offense.fireDamage.GetBaseValue();
        float ice = offense.iceDamage.GetBaseValue();
        float lightning = offense.lightningDamage.GetBaseValue();
        float bonusElementalDamage = major.intelligence.GetBaseValue();

        float hightestDamage = fire;
        type = ElementType.Fire;

        if (hightestDamage < ice)
        {
            type = ElementType.Ice;
            hightestDamage = ice;
        }
        if (hightestDamage < lightning)
        {
            type = ElementType.Lightning;
            hightestDamage = lightning;
        }

        float weakerElementDamage = 0;
        if (hightestDamage != fire)
            weakerElementDamage += fire * 0.5f;
        if (hightestDamage != ice)
            weakerElementDamage += ice * 0.5f;
        if (hightestDamage != lightning)
            weakerElementDamage += lightning * 0.5f;

        if (hightestDamage == 0)
        {
            type = ElementType.None;
            return 0;
        }
        float finalDamage = hightestDamage + bonusElementalDamage;
        return finalDamage * scale;
    }
    public float GetArmorReduction() => offense.armorReduction.GetBaseValue() / 100;
    public float GetPhysicalDamage(out bool isCrit, float scale = 1f)
    {
        float baseDamage = offense.damage.GetBaseValue();
        float bonusDamage = major.strength.GetBaseValue();
        float finalDamage = baseDamage + bonusDamage;

        isCrit = Random.Range(0, 100) < offense.cirtChance.GetBaseValue();
        float baseCritPower = offense.critPower.GetBaseValue();
        float bonusCritPower = major.strength.GetBaseValue() * 0.5f;
        float finalCritpower = (baseCritPower + bonusCritPower) / 100;

        return (isCrit ? finalCritpower * finalDamage : finalDamage) * scale;
    }

    public Stat GetStatByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth:return resouces.maxHealth;
            case StatType.HealthRegen:return resouces.healthRegen;

            case StatType.Agility:return major.agility;
            case StatType.Intelligence:return major.intelligence;
            case StatType.Vitality:return major.vitality;
            case StatType.Strength:return major.strength;

            case StatType.Damage:return offense.damage;
            case StatType.AttackSpeed:return offense.attackSpeed;
            case StatType.CritPower:return offense.critPower;
            case StatType.CritChance:return offense.cirtChance;
            case StatType.ArmorReduction:return offense.armorReduction;

            case StatType.IceDamage:return offense.iceDamage;
            case StatType.FireDamage:return offense.fireDamage;
            case StatType.LightningDamage:return offense.lightningDamage;

            case StatType.Armor:return defense.armor;
            case StatType.Evasion:return defense.evasion;

            case StatType.FireResistance:return defense.fireRes;
            case StatType.LightningResistance:return defense.lightningRes;
            case StatType.IceResistance:return defense.iceRes;
            default: return null;
        }
    }

    [ContextMenu("Update Default Stat Setup")]
    public void ApplyDefaultStatSetup()
    {
        if (defaultStatSetup == null) return;
        resouces.maxHealth.SetBaseValue(defaultStatSetup.maxHealth);
        resouces.healthRegen.SetBaseValue(defaultStatSetup.healthRegen);

        major.strength.SetBaseValue(defaultStatSetup.strength);
        major.intelligence.SetBaseValue(defaultStatSetup.intelligence);
        major.vitality.SetBaseValue(defaultStatSetup.vitality);
        major.agility.SetBaseValue(defaultStatSetup.agility);

        offense.damage.SetBaseValue(defaultStatSetup.damage);
        offense.attackSpeed.SetBaseValue(defaultStatSetup.attackSpeed);
        offense.cirtChance.SetBaseValue(defaultStatSetup.critChance);
        offense.critPower.SetBaseValue(defaultStatSetup.critPower);
        offense.armorReduction.SetBaseValue(defaultStatSetup.armorReduction);

        offense.fireDamage.SetBaseValue(defaultStatSetup.fireDamage);
        offense.iceDamage.SetBaseValue(defaultStatSetup.iceDamage);
        offense.lightningDamage.SetBaseValue(defaultStatSetup.lightningDamage);

        defense.armor.SetBaseValue(defaultStatSetup.armor);
        defense.evasion.SetBaseValue(defaultStatSetup.evasion);

        defense.fireRes.SetBaseValue(defaultStatSetup.fireResistance);
        defense.iceRes.SetBaseValue(defaultStatSetup.iceResistance);
        defense.lightningRes.SetBaseValue(defaultStatSetup.lightningResistance);

    }
}
