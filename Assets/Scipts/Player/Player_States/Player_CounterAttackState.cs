using UnityEngine;

public class Player_CounterAttackState : PlayerState
{
    private Player_Combat combat;
    private bool counteredSb;
    public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        combat = player.GetComponent<Player_Combat>();
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = combat.GetCounterRecoveryDuration();
        counteredSb = combat.CounterAttackPerformed();
        anim.SetBool("counterPerformed", counteredSb);
    }
    public override void Update()
    {
        base.Update();
        
        if (animationTriggered)
            stateMachine.ChangeState(player.idleState);
        if (stateTimer < 0 && !counteredSb)
            stateMachine.ChangeState(player.idleState);
    }
}
