using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;

    [Header("Target detection")]
    [SerializeField] private float detectRadius = 1;
    [SerializeField] private Transform targetCheck;
    [SerializeField] private LayerMask whatIsTarget;

    [Header("Combat details")]
    public DamageScaleData basicAttackScaleData;
    public Entity_Stats stats;

    
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

            AttackData attackData = stats.GetAttackData(basicAttackScaleData);

            if (attackData.type != ElementType.None)
            {
                Entity_StatusHandler handler = target.GetComponent<Entity_StatusHandler>();
                handler?.ApplyStatusEffect(attackData.type, attackData.effectData);
            }
            if (damagable.TakeDamage(attackData.physicalDamage,attackData.elementalDamage, attackData.type, transform))            
                vfx.CreateOnHitVFX(target.transform,attackData.isCrit,attackData.type);
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
