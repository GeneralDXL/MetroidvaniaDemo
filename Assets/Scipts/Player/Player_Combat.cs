using UnityEngine;

public class Player_Combat : Entity_Combat
{
    [SerializeField] private float counterRecoveryDuration = .1f;

    public bool CounterAttackPerformed()
    {
        bool counteredSb = false;
        foreach(var collider in GetTargetColliders())
        {
            ICounterable counterable = collider.GetComponent<ICounterable>();
            if (counterable == null) continue; 
            if(counterable.CanBeCountered)
            {
                counteredSb = true;
                counterable.HandleCounter();
            }
        }
        return counteredSb;
    }

    public float GetCounterRecoveryDuration() => counterRecoveryDuration;
}
