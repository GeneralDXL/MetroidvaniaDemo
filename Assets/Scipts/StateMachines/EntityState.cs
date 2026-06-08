using UnityEngine;

public abstract class EntityState 
{
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected float stateTimer;
    protected bool animationTriggered;
    protected Entity_Stats stats;

    public EntityState(StateMachine stateMachine,string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        anim.SetBool(animBoolName, true);
        animationTriggered = false;
    }
    public virtual void Exit()
    {
        anim.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();
        
    }
    public virtual void UpdateAnimationParameters()
    {

    }

    public void SyncAttackSpeed()
    {
        float attackSpeed=stats.offense.attackSpeed.GetBaseValue();
        anim.SetFloat("attackSpeedMultiplier",attackSpeed);
    }
    public void AnimationTriggered()
    {
        animationTriggered = true;
    }
}
