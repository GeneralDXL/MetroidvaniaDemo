using UnityEngine;

public class Enemy_AnimationTirggers : Entity_AnimationTriggers
{
    Enemy enemy;
    Enemy_VFX vfx;
    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponentInParent<Enemy>();
        vfx=GetComponentInParent<Enemy_VFX>();
    }
    private void EnableCounterWindow()
    {
        enemy.EnableCounterWindow(true);
        vfx.EnableAlert();
    }
    private void DisableCounterWindow()
    {
        enemy.EnableCounterWindow(false);
        vfx.DisableAlert();
    }
}
