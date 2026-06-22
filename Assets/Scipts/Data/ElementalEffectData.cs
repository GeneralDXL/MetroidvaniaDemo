
public class ElementalEffectData
{
    public float burnDuration;
    public float burnDamage;

    public float chillDuration;
    public float chillSlowMultiplier;

    public float electrifyDamage;
    public float electrifyDuration;
    public float electrifyCharge;

    public ElementalEffectData(Entity_Stats stats,DamageScaleData scaleData)
    {
        burnDuration =scaleData.burnDuration;
        burnDamage = stats.offense.fireDamage.GetBaseValue() * scaleData.burnDamageScale;

        chillDuration =scaleData.chillDuration;
        chillSlowMultiplier =scaleData.chillSlowMultiplier;

        electrifyCharge =scaleData.electrifyCharge;
        electrifyDuration =scaleData.electrifyDuration;
        electrifyDamage=stats.offense.lightningDamage.GetBaseValue()*scaleData.electrifyDamageScale;
    }
}
