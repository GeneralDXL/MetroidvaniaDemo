using UnityEngine;

public class Player_WallSlideState : PlayerState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Update()
    {
        base.Update();

        if (!player.isWallDetected)
        {
            if(rb.linearVelocity.y != 0)
                stateMachine.ChangeState(player.fallState);
        }
        else
        {
            if (player.moveInput.y >= 0)
                player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallSlideMutiplier);
            else
                player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
        }
        if(input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.wallJumpState);

        if (player.isGroundDetected)
        {
            stateMachine.ChangeState(player.idleState);
            if(player.facingDir != player.moveInput.x)
                player.Flip();
        }

    }

}
