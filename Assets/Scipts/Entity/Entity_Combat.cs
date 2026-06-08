using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;

    [Header("Target detection")]
    [SerializeField] private float detectRadius = 1;
    [SerializeField] private Transform targetCheck;
    [SerializeField] private LayerMask whatIsTarget;

    [Header("Combat details")]
    public Entity_Stats stats;

    [Header("Status Effect details")]
    [SerializeField] private float defaultDuration = 3;
    [SerializeField] private float chillSlowMultipiler = 0.4f;
    [SerializeField] private float electrifyChargeBuildUp = 0.4f;
    [Space]
    [SerializeField] private float fireScale = .8f;
    [SerializeField] private float lightningScale = 2.5f;
    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }
    public void PerformAttack()
    {
        foreach (var target in GetTargetColliders())
        {
            IDamagable damagable = target.GetComponent<IDamagable>();
            if (damagable == null) continue;
            float damage = stats.GetPhysicalDamage(out bool isCrit);
            float elementalDamage = stats.GetElementalDamage(out ElementType type,0.6f);
            if (type != ElementType.None)
                ApplyElementEffect(target.transform, type);
            if (damagable.TakeDamage(damage, elementalDamage, type, transform))
            {
                vfx.UpdateOnHitColor(type);
                vfx.CreateOnHitVFX(target.transform, isCrit);
            }
        }
    }

    public void ApplyElementEffect(Transform target,ElementType type,float scale=1f)
    {
        Entity_StatusHandler handler=target.GetComponent<Entity_StatusHandler>();
        if (handler == null) return;
        if(type==ElementType.Ice&&handler.CanBeApplied(ElementType.Ice))
        {
            handler.ApplyChillEffect(defaultDuration,chillSlowMultipiler*scale);
        }
        if(type==ElementType.Fire&&handler.CanBeApplied(ElementType.Fire))
        {
            scale = fireScale;
            float fireDamage=stats.offense.fireDamage.GetBaseValue()*scale;
            handler.ApplyBurnEffect(defaultDuration,fireDamage);
        }
        if(type==ElementType.Lightning&&handler.CanBeApplied(ElementType.Lightning))
        {
            scale=lightningScale;
            float lightningDamage=stats.offense.lightningDamage.GetBaseValue()*scale;
            handler.ApplyElectrifyEffect(defaultDuration, lightningDamage,electrifyChargeBuildUp);
        }
    }
    protected Collider2D[] GetTargetColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, detectRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, detectRadius);
    }
}
