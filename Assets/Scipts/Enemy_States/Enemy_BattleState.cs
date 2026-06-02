using Unity.Mathematics;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    private float lastTimeAttack;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player == null)
            player = enemy.PlayerDetect().transform;
        if(ShouldRetreat())
        {
            rb.linearVelocity = new Vector2(-ChaseDir() * enemy.retreatVelocity.x, enemy.retreatVelocity.y);
            enemy.HandleFlip(ChaseDir());
        }
    }
    public override void Update()
    {
        base.Update();
        if(enemy.PlayerDetect())
            lastTimeAttack = Time.time;
        if (enemy.battleDuration+lastTimeAttack<Time.time )
            stateMachine.ChangeState(enemy.idleState);
        if (WithinAttackDistance()&&enemy.PlayerDetect())
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            enemy.SetVelocity(ChaseDir() * enemy.chaseSpeed, rb.linearVelocity.y);
        }
    }

    private bool ShouldRetreat()
    {
        return DistanceToPlayer()<enemy.retreatDistance&&enemy.PlayerDetect();
    }
    private bool WithinAttackDistance()
    {
        return DistanceToPlayer() < enemy.attackDistance;
    }

    private float DistanceToPlayer()
    {
        if(player!=null)
            return math.abs(player.position.x - enemy.transform.position.x);
        return float.MaxValue;
    }

    private int ChaseDir()
    {
        if(player==null)
            return 0;
        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }

}
