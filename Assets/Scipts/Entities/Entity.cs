using UnityEngine;

public class Entity : MonoBehaviour
{
    protected StateMachine stateMachine;
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public int facingDir { get; private set; } = 1;

   

    [Header("Collision detecte details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private Transform grounCheck;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool isGroundDetected { get; private set; }
    public bool isWallDetected { get; private set; }


    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();

    }

    protected virtual void Start()
    {
        
    }

   

    protected virtual void Update()
    {
        stateMachine.UpdateActiveState();
        HandleCollisionDetected();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    
    private void HandleCollisionDetected()
    {
        isGroundDetected = Physics2D.Raycast(grounCheck.transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        if (secondaryWallCheck != null)
            isWallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround)
                      && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
        else
            isWallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }
    public void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && facingDir<0)
            Flip();
        else if (xVelocity < 0 && facingDir>0)
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingDir = facingDir * -1;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(grounCheck.transform.position,grounCheck.transform.position + new Vector3(0, -groundCheckDistance, 0));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0, 0));
        if(secondaryWallCheck!=null)
            Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0, 0));
    }

    public void AnimationTriggered()
    {
        stateMachine.currentState.AnimationTriggered();
    }
}
