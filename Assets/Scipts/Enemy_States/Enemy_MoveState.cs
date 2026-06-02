using UnityEngine;

public class Enemy_MoveState : Enemy_GroundedState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        anim.SetFloat("moveAnimSpeedMutiplier", enemy.moveAnimSpeedMultiplier);
        if (!enemy.isGroundDetected || enemy.isWallDetected)
            enemy.Flip();
    }
    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.facingDir * enemy.moveSpeed, rb.linearVelocity.y);
        if (!enemy.isGroundDetected || enemy.isWallDetected)
            stateMachine.ChangeState(enemy.idleState);
    }
}
