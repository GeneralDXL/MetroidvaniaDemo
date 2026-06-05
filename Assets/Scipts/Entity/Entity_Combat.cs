using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;

    [Header("Target detection")]
    [SerializeField] private float detectRadius = 1;
    [SerializeField] private Transform targetCheck;
    [SerializeField] private LayerMask whatIsTarget;

    [Header("Combat details")]
    public float damage = 10;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
    }
    public void PerformAttack()
    {
        foreach (var collider in GetTargetColliders())
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            if (damagable == null) continue; 
            damagable.TakeDamage(damage,transform);
            vfx.CreateOnHitVFX(collider.transform);
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
