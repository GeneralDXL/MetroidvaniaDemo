using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
public class Entity : MonoBehaviour
{
    public event Action OnFlipped;
    protected StateMachine stateMachine;
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Entity_Stats stats { get; private set; }

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

    
    private bool isKnockback;
    private Coroutine knockBackCo;
    private Coroutine slowdownCo;

    public void ReceiveKnockback(Vector2 knockForce,float duration)
    {
        if(knockBackCo != null)
            StopCoroutine(knockBackCo);
        knockBackCo = StartCoroutine(KnockBackCo(knockForce,duration));
    }
    private IEnumerator KnockBackCo(Vector2 knockForce,float duration)
    {
        rb.linearVelocity = knockForce;
        isKnockback=true;
        yield return new WaitForSeconds(duration);
        rb.linearVelocity=Vector2.zero;
        isKnockback = false;
    }


    
    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
        stats=GetComponent<Entity_Stats>();

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
        if (isKnockback)
            return;
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    public virtual void SlowdonwnEntity(float duration,float slowMultiplier)
    {
        if(slowdownCo != null)
            StopCoroutine(slowdownCo);
        slowdownCo=StartCoroutine(SlowEntityCo(duration, slowMultiplier));
    }

    protected virtual IEnumerator SlowEntityCo(float duration,float slowMultiplier)
    {
        yield return null;
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
        OnFlipped.Invoke();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(grounCheck.transform.position,grounCheck.transform.position + new Vector3(0, -groundCheckDistance, 0));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0, 0));
        if(secondaryWallCheck!=null)
            Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0, 0));
    }

    public void StateAnimationTriggered()
    {
        stateMachine.currentState.AnimationTriggered();
    }

    public void DeathTriggered()
    {
        anim.enabled = false;
    }
    public virtual void Die()
    {
        
    }
}
