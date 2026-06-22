using UnityEngine;
using System;
[Serializable]
public class DamageScaleData 
{
    [Header("Damage")]
    public float physical = 1f;
    public float elemental = 1f;

    [Header("Burn")]
    public float burnDuration = 3f;
    public float burnDamageScale = 1.5f;

    [Header("Chill")]
    public float chillDuration = 3f;
    public float chillSlowMultiplier = 0.3f;

    [Header("Eletrify")]
    public float electrifyDuration = 3f;
    public float electrifyDamageScale = 1.5f;
    public float electrifyCharge = 0.4f;
}
