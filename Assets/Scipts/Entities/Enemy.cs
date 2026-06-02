using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;

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
    

  

    public RaycastHit2D PlayerDetect()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround);
        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + new Vector3(playerCheckDistance * facingDir, 0));
    }
}
