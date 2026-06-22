using UnityEngine;
using System;
[Serializable]
public class AttackData
{
    public float physicalDamage;
    public float elementalDamage;
    public bool isCrit;
    public ElementType type;

    public ElementalEffectData effectData;

    public AttackData(Entity_Stats stats,DamageScaleData scaleData)
    {
        physicalDamage = stats.GetPhysicalDamage(out bool isCrit, scaleData.physical);
        elementalDamage =stats.GetElementalDamage(out ElementType type, scaleData.elemental);

        effectData=new ElementalEffectData(stats, scaleData);
        this.isCrit = isCrit;
        this.type = type;
    }
}
