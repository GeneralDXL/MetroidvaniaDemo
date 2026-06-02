 using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;
    public EnemyState(Enemy enemy,StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;
        anim=enemy.anim;
        rb=enemy.rb;
    }
   

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        float battleAnimSpeedMultiplier = enemy.chaseSpeed / enemy.moveSpeed * enemy.moveAnimSpeedMultiplier;
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("moveAnimSpeedMutiplier", enemy.moveAnimSpeedMultiplier);
        anim.SetFloat("battleAnimSpeedMutiplier", battleAnimSpeedMultiplier);
    }
}
