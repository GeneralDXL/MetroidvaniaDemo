using UnityEngine;

public class Player_DashState : EntityState
{
    private int dashDir;
    private float gravityScale;
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        dashDir = player.moveInput.x!=0? ((int)player.moveInput.x): player.facingDir;
        gravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        stateTimer = player.dashDuration;
    }
    public override void Update()
    {
        base.Update();
        player.SetVelocity(dashDir * player.dashSpeed, 0);
        CancleDashIfNeeded();
        if(stateTimer<0)
        {
            if (player.isGroundDetected)
                stateMachine.ChangeState(player.idleState);
            else if (player.isWallDetected)
                stateMachine.ChangeState(player.wallSlideState);
            else
                stateMachine.ChangeState(player.fallState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = gravityScale;
        player.SetVelocity(0, 0);
    }

    private void CancleDashIfNeeded()
    {
        if (player.isWallDetected)
        {
            if (player.isGroundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
