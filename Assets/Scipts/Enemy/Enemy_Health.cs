using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();
    public override bool TakeDamage(float damage,float elementalDamage,ElementType type, Transform dealer)
    {
        if(! base.TakeDamage(damage,elementalDamage,type,dealer))
            return false;
        if (dealer.GetComponent<Player>() != null)
            enemy.TryEnterBattleState(dealer);
        return true;
    }

}
