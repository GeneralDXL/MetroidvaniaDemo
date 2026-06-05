using UnityEngine;

public class StateMachine
{
    public EntityState currentState { get; private set; }
    private bool canChangeState = true;
    public void Initialize(EntityState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(EntityState state)
    {
        if (!canChangeState)
            return;
        currentState.Exit();
        currentState=state;
        currentState.Enter();
    }

    public void UpdateActiveState()
    {
        currentState.Update();
    }

    public void SwitchOffStateMachine()=>canChangeState = false;
}
