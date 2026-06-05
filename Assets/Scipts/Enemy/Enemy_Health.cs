using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();
    public override void TakeDamage(float damage,Transform dealer)
    {
        base.TakeDamage(damage,dealer);
        if(dealer.GetComponent<Player>()!=null)
           enemy.TryEnterBattleState(dealer);

    }

}
