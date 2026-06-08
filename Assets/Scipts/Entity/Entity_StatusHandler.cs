using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private ElementType currenttype = ElementType.None;
    private Entity entity;
    private Entity_VFX vfx;
    private Entity_Stats stats;
    private Entity_Health health;

    [Header("Electrify effect details")]
    [SerializeField] private GameObject lightningStrikeVfx;
    [SerializeField] private float currentCharge;
    [SerializeField] private float maxCharge = 1f;
    private Coroutine electrifyCo;
    
    private void Awake()
    {
        entity = GetComponent<Entity>();
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
        health = GetComponent<Entity_Health>();
    }
    public void ApplyElectrifyEffect(float duration,float damage,float charge)
    {
        float lightningRes = stats.GetElementResistance(ElementType.Lightning);
        float finalCharge=charge*(1-lightningRes);
        currentCharge += finalCharge;
        if(currentCharge > maxCharge)
        {
            DoLightningStrike(damage);
            StopElectrifyEffect();
            return;
        }
        if(electrifyCo!=null)
            StopCoroutine(electrifyCo);
        electrifyCo = StartCoroutine(ElectrifyEffectCo(duration));

    }

    private void StopElectrifyEffect()
    {
        currentCharge = 0;
        currenttype = ElementType.None;
        vfx.StopAllVfx();
    }
    private void DoLightningStrike(float damage)
    {
        Instantiate(lightningStrikeVfx, transform.position, Quaternion.identity);
        health.ReduceHealth(damage);
    }

    public void ApplyChillEffect(float duration,float slowMultiplier)
    {
        float iceRes = stats.GetElementResistance(ElementType.Ice);
        float finalDuration=duration*(1-iceRes);
        StartCoroutine(ChillEffectCo(finalDuration, slowMultiplier));
    }

    public void ApplyBurnEffect(float duration,float fireDamage)
    {
        float fireRes=stats.GetElementResistance(ElementType.Fire);
        float fianlDamage = fireDamage * (1 - fireRes);
        StartCoroutine(BurnEffectCo(duration, fianlDamage));

    }

    private IEnumerator ElectrifyEffectCo(float duration)
    {
        currenttype = ElementType.Lightning;
        vfx.PlayElementVfx(duration,ElementType.Lightning);
        yield return new WaitForSeconds(duration);
        StopElectrifyEffect();
    }
    private IEnumerator BurnEffectCo(float duration,float totalDamage)
    {
        currenttype = ElementType.Fire;
        vfx.PlayElementVfx(duration, ElementType.Fire);


        int tickPerSecond = 2;
        int tickCount = Mathf.RoundToInt(duration * tickPerSecond);

        float damagePerTick=totalDamage/tickCount;
        float tickInterval = 1f / tickPerSecond;
        
        for(int i=0;i<tickCount; ++i)
        {
            health.ReduceHealth(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }

        currenttype = ElementType.None;
    }
    private IEnumerator ChillEffectCo(float duration,float slowMultiplier)
    {
        entity.SlowdonwnEntity(duration, slowMultiplier);
        currenttype = ElementType.Ice;
        vfx.PlayElementVfx(duration, ElementType.Ice);

        yield return new WaitForSeconds(duration);

        currenttype = ElementType.None;
    }
    public bool CanBeApplied(ElementType type)
    {
        if(currenttype == ElementType.Lightning && type == ElementType.Lightning)
            return true;
        return currenttype == ElementType.None;
    }
}
