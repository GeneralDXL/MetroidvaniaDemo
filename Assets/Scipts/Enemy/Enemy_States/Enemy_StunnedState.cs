using UnityEngine;

public class Enemy_StunnedState : EnemyState
{
    private Enemy_VFX vfx;
    public Enemy_StunnedState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        vfx=enemy.GetComponent<Enemy_VFX>();
    }
    public override void Enter()
    {
        base.Enter();
        vfx.DisableAlert();
        enemy.EnableCounterWindow(false);
        stateTimer = enemy.stunnedDuration;
        rb.linearVelocity = new Vector2(enemy.counterVelocity.x * -enemy.facingDir, enemy.counterVelocity.y);

    }
    public override void Update()
    {
        base.Update();
        if (stateTimer<0)
            stateMachine.ChangeState(enemy.idleState);
    }
   
}
