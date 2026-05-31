using UnityEngine;

public class Player_JumpAttackState : EntityState
{
    private bool isGrounded;
    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isGrounded = false;
        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDir, player.jumpAttackVelocity.y);
    }
    public override void Update()
    {
        base.Update();
        if(player.isGroundDetected&&!isGrounded)
        {
            isGrounded = true;
            player.SetVelocity(0, rb.linearVelocity.y);
            anim.SetTrigger("jumpAttackTrigger");
        }
        if(animationTriggered && player.isGroundDetected)
            stateMachine.ChangeState(player.idleState);
    }
}
