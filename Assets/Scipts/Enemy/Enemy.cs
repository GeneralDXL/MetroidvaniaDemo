using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;
    public Enemy_DeadState deadState;
    public Enemy_StunnedState stunnedState;

    public Entity_Stats stats { get; private set; }

    protected override void Awake()
    {
        stats = GetComponent<Entity_Stats>();
    }

    [Header("Movement details")]
    public float moveSpeed = 1.4f;
    public float moveAnimSpeedMultiplier = 1;

    [Header("Player detection")]
    [SerializeField]private float playerCheckDistance = 20;
    [SerializeField]private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;

    [Header("Battle details")]
    public float attackDistance = 2f;
    public float chaseSpeed = 3;
    public float battleDuration = 5;
    public float retreatDistance = 1;
    public Vector2 retreatVelocity;
    public Transform player { get; private set; }

    [Header("Counter attack details")]
    public Vector2 counterVelocity = new Vector2(7, 7);
    public float stunnedDuration = 2f;
    protected bool canBeStunned;

    public void EnableCounterWindow(bool canBeStunned)
    {
        this.canBeStunned = canBeStunned;
    }

    protected override IEnumerator SlowEntityCo(float duration, float slowMultiplier)
    {
        float originalMoveSpeed = moveSpeed;
        float originalChaseSpeed = chaseSpeed;
        float originalAnimSpeed = anim.speed;

        float speedMultiplier = 1 - slowMultiplier;

        moveSpeed*=speedMultiplier;
        chaseSpeed*=speedMultiplier;
        anim.speed*=speedMultiplier;

        yield return new WaitForSeconds(duration);
        moveSpeed =originalMoveSpeed;
        chaseSpeed =originalChaseSpeed;
        anim.speed = originalAnimSpeed;

    }
    public void TryEnterBattleState(Transform player)
    {
        this.player = player;
        if(stateMachine.currentState != battleState && stateMachine.currentState != attackState)
            stateMachine.ChangeState(battleState);
    }
    public Transform GetPlayerReferrence()
    {
        if (player == null)
            player = PlayerDetect().transform;
        return player;
    }

    public RaycastHit2D PlayerDetect()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround);
        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;

        return hit;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + new Vector3(playerCheckDistance * facingDir, 0));
    }

    private void HandlePlayerDeath()
    {
        stateMachine.ChangeState(idleState);
    }
    private void OnEnable()
    {
        Player.PlayerOnDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        Player.PlayerOnDeath-= HandlePlayerDeath;
    }
}
