using System;
using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : Entity
{
    public static event Action PlayerOnDeath;
    public Player_SkillManager skillManager;
    public Player_VFX vfx;
    private UI ui;

    #region States
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }
    public Player_DeadState deadState { get; private set; }
    public Player_CounterAttackState counterAttackState { get; private set; }
    #endregion
    public PlayerInputSet input { get; private set; }
    public Vector2 moveInput { get; private set; }

    [Header("Movement details")]
    public float moveSpeed = 8;
    public float jumpForce = 12;
    public Vector2 wallJumpForce;
    [Range(0, 1)]
    public float airMoveMutiplier = 0.8f;
    [Range(0, 1)]
    public float wallSlideMutiplier = 0.3f;
    public float dashSpeed = 20;
    public float dashDuration = 0.25f;

    [Header("Attack details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = 0.1f;
    public float comboResetTime = 1;
    private Coroutine queueAttackCo;

    protected override void Awake()
    {
        base.Awake();
        input = new PlayerInputSet();
        skillManager = GetComponent<Player_SkillManager>();
        #region Initialize States
        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");
        dashState = new Player_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
        deadState = new Player_DeadState(this, stateMachine, "dead");
        counterAttackState = new Player_CounterAttackState(this, stateMachine, "counter");
        #endregion
        ui = FindAnyObjectByType<UI>();
        vfx = GetComponent<Player_VFX>();
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    public void Teleport(Vector3 newPosition) => transform.position = newPosition;
    protected override IEnumerator SlowEntityCo(float duration, float slowMultiplier)
    {
        float originalMoveSpeed = moveSpeed;
        float originalJumpForce=jumpForce;
        float originalAnimSpeed = anim.speed;
        Vector2 originalWallJump = wallJumpForce;
        Vector2 originalJumpAttack=jumpAttackVelocity;
        Vector2[] originalAttackVelocity = attackVelocity;

        float speedmultiplier = 1 - slowMultiplier;

        moveSpeed*=speedmultiplier;
        jumpForce*=speedmultiplier;
        anim.speed *= speedmultiplier;
        wallJumpForce*=speedmultiplier;
        jumpAttackVelocity*=speedmultiplier;
        for(int i=0;i<attackVelocity.Length;i++)
            attackVelocity[i] *= speedmultiplier;

        yield return new WaitForSeconds(duration);

        moveSpeed= originalMoveSpeed;
        jumpForce= originalJumpForce;
        anim.speed = originalAnimSpeed;
        wallJumpForce = originalWallJump;
        jumpAttackVelocity = originalJumpAttack;
        for (int i = 0; i < attackVelocity.Length; i++)
            attackVelocity[i]= originalAttackVelocity[i];

    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
        PlayerOnDeath.Invoke();
    }
    public void EnterAttackStateWithDelay()
    {
        if (queueAttackCo != null)
            StopCoroutine(queueAttackCo);
        queueAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }
    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);

    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += xtx => moveInput = new Vector2(0, 0);
        input.Player.ToggleSkillTreeUI.performed += ctx => ui.ToggleSkillTree();
        input.Player.Spell.performed += ctx => skillManager.shard.TryUseSkill();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
