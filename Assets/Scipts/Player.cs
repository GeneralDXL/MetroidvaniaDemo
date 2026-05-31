using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;
    public Player_IdleState idleState {  get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }
    public  PlayerInputSet input { get; private set; }
    public Vector2 moveInput { get; private set; }
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }

    [Header("Movement details")]
    public float moveSpeed = 8;
    public float jumpForce = 12;
    public Vector2 wallJumpForce;
    private bool facingRight = true;
    public int facingDir { get; private set; } = 1;
    [Range(0,1)]
    public float airMoveMutiplier = 0.8f;
    [Range(0,1)]
    public float wallSlideMutiplier = 0.3f;
    public float dashSpeed = 20;
    public float dashDuration = 0.25f;

    [Header("Collision detecte details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool isGroundDetected { get; private set; }
    public bool isWallDetected { get; private set; }

    [Header("Attack details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = 0.1f;
    public float comboResetTime = 1;
    private Coroutine queueAttackCo;

    private void Awake()
    {
        anim=GetComponentInChildren<Animator>();
        rb=GetComponent<Rigidbody2D>();
        input = new PlayerInputSet();
        stateMachine = new StateMachine();


        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");
        dashState = new Player_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += xtx => moveInput = new Vector2(0, 0);
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        stateMachine.UpdateActiveState();
        HandleCollisionDetected();
    }

    public void SetVelocity(float xVelocity,float yVelocity)
    {
        rb.linearVelocity=new Vector2(xVelocity,yVelocity);
        HandleFlip(xVelocity);
    }

    public void EnterAttackStateWithDelay()
    {
        if(queueAttackCo!=null)
            StopCoroutine(queueAttackCo);
        queueAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }
    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);

    }
    private void HandleCollisionDetected()
    {
        isGroundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(primaryWallCheck.position,Vector2.right* facingDir,wallCheckDistance,whatIsGround)
                      && Physics2D.Raycast(secondaryWallCheck.position,Vector2.right* facingDir,wallCheckDistance,whatIsGround);
    }
    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !facingRight)
            Flip();
        else if(xVelocity < 0 && facingRight)
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance, 0));
        Gizmos.DrawLine(primaryWallCheck.position,primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0, 0));
        Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir,0, 0));
    }

    public void AnimationTriggered()
    {
        stateMachine.currentState.AnimationTriggered();
    }
}
