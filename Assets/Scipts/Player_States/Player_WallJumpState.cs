using UnityEngine;

public class Player_WallJumpState : Player_AiredState
{
    public Player_WallJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.wallJumpForce.x * - player.facingDir, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();
        if (player.isGroundDetected)
            stateMachine.ChangeState(player.idleState);
        if (player.isWallDetected)
            stateMachine.ChangeState(player.wallSlideState);
    }
}
