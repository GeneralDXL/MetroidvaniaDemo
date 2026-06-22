using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour , IDamagable
{
    private Entity_VFX entityVfx;
    private Entity entity;
    [SerializeField] private Slider healthbar;
    [SerializeField] protected Entity_Stats stats;
    [SerializeField] protected float currentHealth ;
    [SerializeField] protected bool isDead;

    [Header("Knockback details")]
    [SerializeField] private float knockbackDuration = .2f;
    [SerializeField] private Vector2 knockbackForce = new Vector2(1.5f, 2.5f);
    [SerializeField] private float heavyKnockThreshold = .3f;
    [SerializeField] private float heavyKnockDuration = .5f;
    [SerializeField] private Vector2 heavyKnockForce = new Vector2(7, 7);

    [Header("Health regen")]
    [SerializeField] private float regenInterval = 1;
    [SerializeField] private bool canRegenerateHealth = true;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
        stats=GetComponent<Entity_Stats>();
        currentHealth=stats.GetMaxHealth();
        
        UpdateHealthbar();

        InvokeRepeating(nameof(RegenateHealth), 0, regenInterval);
    }
    public virtual bool TakeDamage(float damage,float elementalDamage,ElementType type, Transform dealer)
    {
        if (isDead) return false;
        if(isEvaded()) return false;
        float finalDamage =damage+elementalDamage;
        Entity_Stats dealStats=dealer.GetComponent<Entity_Stats>();
        if (dealStats != null)
        {
            float physicalDamageTaken = damage * (1 - stats.GetArmorMitigation(dealStats.offense.armorReduction.GetBaseValue()));
            float elementalDamageTaken = elementalDamage * (1 - stats.GetElementResistance(type));
            finalDamage = physicalDamageTaken + elementalDamage;
        }
        entity?.ReceiveKnockback(GetKnockForce(finalDamage, dealer), GetDuration(finalDamage));
        ReduceHealth(finalDamage);
        return true;
    }

    private void RegenateHealth()
    {
        if (!canRegenerateHealth)
            return;
        float healAmount = stats.resouces.healthRegen.GetBaseValue();
        IncreaseHealth(healAmount);
    }
    public void IncreaseHealth(float healAmount)
    {
        if(isDead) return;
        float newHealth = currentHealth + healAmount;
        float maxHealth=stats.GetMaxHealth();
        currentHealth = Mathf.Min(newHealth, maxHealth);
        UpdateHealthbar();
    }
    private Vector2 GetKnockForce(float damage,Transform dealer)
    {
        Vector2 result = IsHeavyKnock(damage) ? heavyKnockForce : knockbackForce;
        result.x *= dealer.position.x < transform.position.x ? 1 : -1;
        return result;
    }

    private bool isEvaded()
    {
        return Random.Range(0, 100) < stats.GetEvasion();
    }

    public float GetHealthPercent() => currentHealth / stats.GetMaxHealth();

    public void SetHealthToPercentage(float percentage)
    {
        currentHealth = stats.GetMaxHealth() * Mathf.Clamp01(percentage);
        UpdateHealthbar();
    }
    private void UpdateHealthbar()
    {
        healthbar.value = currentHealth / stats.GetMaxHealth();
    }
    private float GetDuration(float damage)
    {
        return IsHeavyKnock(damage) ? heavyKnockDuration : knockbackDuration;
    }
    private bool IsHeavyKnock(float damage)
    {
        return damage / stats.GetMaxHealth() > heavyKnockThreshold;
    }
    public void ReduceHealth(float damage)
    {
        entityVfx?.PlayOnDamageVfx();
        currentHealth-= damage;
        if (currentHealth <= 0)
            Die();
        UpdateHealthbar();
    }

    private void Die()
    {
        isDead = true;
        entity.Die();
    }
}
