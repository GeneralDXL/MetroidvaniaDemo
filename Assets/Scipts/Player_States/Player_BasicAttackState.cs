using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float velocityDuration;
    private float lastAttackTime;
    private int comboIndex = 1;
    private int startComboIndex = 1;
    private int limitComboIndex = 3;
    private bool comboAttackQueued;
    private int attackDir;
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if(player.attackVelocity.Length!=limitComboIndex)
        {
            limitComboIndex=player.attackVelocity.Length;
            Debug.LogWarning("reset the limitComboIndex to adjust the length of attackVelocity");
        }
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;
        ResetComboIfNeeded();
        anim.SetInteger("comboIndex",comboIndex);
        ApplyVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleVelocity();
        if (input.Player.Attack.WasPerformedThisFrame())
            QueueNextAttack();
        if (animationTriggered)
            HandleStateExit();
        


    }

    private void QueueNextAttack()
    {
        if (comboIndex < limitComboIndex)
            comboAttackQueued = true;
    }

    private void HandleStateExit()
    {
        if (!comboAttackQueued)
            stateMachine.ChangeState(player.idleState);
        else
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
    }

    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
    }

    private void HandleVelocity()
    {
        velocityDuration-=Time.deltaTime;
        if(velocityDuration < 0 )
        {
            player.SetVelocity(0, rb.linearVelocity.y);
        }

    }
    private void ResetComboIfNeeded()
    {
        if (Time.time > lastAttackTime + player.comboResetTime)
            comboIndex = startComboIndex;
        if (comboIndex > limitComboIndex)
            comboIndex = startComboIndex;
    }
    private void ApplyVelocity()
    {
        velocityDuration = player.attackVelocityDuration;
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
        comboIndex++;
    }
}
