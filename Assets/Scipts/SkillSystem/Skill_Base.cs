using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    public Player player {  get; private set; }
    public DamageScaleData damageScaleData {  get; private set; }
    public Player_SkillManager skillManager { get; private set; }
    [Header("General details")]
    [SerializeField] protected float cooldown;
    [SerializeField] protected SkillType skillType;
    [SerializeField] protected SkillUpgradeType upgradeType;
    private float lasttimeUsed;
   

    //    public void ResetCooldown() => lasttimeUsed = Time.time;
    protected virtual void Awake()
    {
        lasttimeUsed -= cooldown;
        player = GetComponentInParent<Player>();
        skillManager= GetComponentInParent<Player_SkillManager>();
    }
   
    public void SetSkillUpgrade(UpgradeData upgradeData)
    {
        upgradeType = upgradeData.upgradeType;
        cooldown = upgradeData.cooldown;
        damageScaleData = upgradeData.damageScale;
    }
    public void RefreshCooldown(float cooldown) => lasttimeUsed += cooldown;
    public bool IsOnCooldown() => Time.time < lasttimeUsed + cooldown;
    public void SetSkillOnCooldown() => lasttimeUsed = Time.time;

    protected bool isUnlockedUpgrade(SkillUpgradeType type) => type == upgradeType;
    public virtual bool CanUseSkill()
    {
        if (upgradeType == SkillUpgradeType.None)
            return false;

        if (IsOnCooldown()) return false;
        return true;
    }
    public virtual void TryUseSkill()
    {

    }

    public virtual void OnStartEffect()
    {

    }
    public virtual void OnEndEffect()
    {

    }

}
