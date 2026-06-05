using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour , IDamagable
{
    private Entity_VFX entityVfx;
    private Entity entity;
    [SerializeField] private Slider healthbar;
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected float curHp ;
    [SerializeField] protected bool isDead;

    [Header("Knockback details")]
    [SerializeField] private float knockbackDuration = .2f;
    [SerializeField] private Vector2 knockbackForce = new Vector2(1.5f, 2.5f);
    [SerializeField] private float heavyKnockThreshold = .3f;
    [SerializeField] private float heavyKnockDuration = .5f;
    [SerializeField] private Vector2 heavyKnockForce = new Vector2(7, 7);

    private void Awake()
    {
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
        curHp=maxHp;
        UpdateHealthbar();
    }
    public virtual void TakeDamage(float damage,Transform dealer)
    {
        if (isDead) return;
        ReduceHp(damage);
        entityVfx?.PlayOnDamageVfx();
        entity?.ReceiveKnockback(GetKnockForce(damage, dealer), GetDuration(damage));
    }

    private Vector2 GetKnockForce(float damage,Transform dealer)
    {
        Vector2 result = IsHeavyKnock(damage) ? heavyKnockForce : knockbackForce;
        result.x *= dealer.position.x < transform.position.x ? 1 : -1;
        return result;
    }

    private void UpdateHealthbar()
    {
        healthbar.value = curHp / maxHp;
    }
    private float GetDuration(float damage)
    {
        return IsHeavyKnock(damage) ? heavyKnockDuration : knockbackDuration;
    }
    private bool IsHeavyKnock(float damage)
    {
        return damage / maxHp > heavyKnockThreshold;
    }
    protected void ReduceHp(float damage)
    {
        if (curHp <= 0)
            Die();
        else
            curHp-= damage;
        UpdateHealthbar();
    }

    private void Die()
    {
        isDead = true;
        entity.Die();
    }
}
