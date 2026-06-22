using UnityEngine;

public class SkillObject_Base : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected Transform targetCheck;
    [SerializeField] protected float checkRadius = 1f;
    [SerializeField] protected float searchRadius = 10f;
    protected Entity_Stats stats;
    protected DamageScaleData scaleData;
    protected ElementType type;
    protected virtual void DamageEnemiesInRadius(Transform t, float radius)
    {
        foreach(var target in GetEnemiesAround(t,radius))
        {
            IDamagable damagable = target.GetComponent<IDamagable>();
            if(damagable == null) continue;

            AttackData attackData = stats.GetAttackData(scaleData);
            this.type = attackData.type;
            damagable.TakeDamage(attackData.physicalDamage,attackData.elementalDamage,type,transform);

            if (type != ElementType.None)
            {
                target.GetComponent<Entity_StatusHandler>().ApplyStatusEffect(type, attackData.effectData);
            }
        }
    }
    protected Collider2D[] GetEnemiesAround(Transform t,float radius)
    {
        return Physics2D.OverlapCircleAll(t.position,radius,whatIsEnemy);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, checkRadius);
    }

    protected Transform GetClosestTarget()
    {
        Transform target = null;
        float closestDistance = Mathf.Infinity;
       
        foreach(var enemy in GetEnemiesAround(transform,searchRadius))
        {
            float distance= Vector2.Distance(transform.position,enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target=enemy.transform;
            }
        }

        return target;
    }
}
