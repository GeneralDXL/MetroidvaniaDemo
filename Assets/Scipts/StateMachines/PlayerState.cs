using UnityEngine;

public class PlayerState :EntityState
{
    protected Player player;
    protected PlayerInputSet input;
    public PlayerState(Player player,StateMachine stateMachine, string animBoolName):base(stateMachine, animBoolName)
    {
        this.player = player;
        anim = player.anim;
        rb=player.rb;
        input=player.input;
        stats = player.stats;
    }


    public override void Update()
    {
        base.Update();
        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
            stateMachine.ChangeState(player.dashState);

    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
    private bool CanDash()
    {
        if(stateMachine.currentState == player.dashState)
            return false;
        if(player.isWallDetected)
            return false;
        return true;
    }

   
}
