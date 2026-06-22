using System.Collections;
using UnityEngine;

public class Skill_Shard : Skill_Base
{
    private SkillObject_Shard currentShard;
    private Entity_Health playerHealth;
    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonateTime = 2;
    [Header("Moving shard upgrade")]
    [SerializeField] private float shardSpeed = 7f;
    [Header("MutiCast shard upgrade")]
    [SerializeField] private int maxCharges = 3;
    private int currentCharges;
    private bool isCharging;
    [Header("Teleport shard upgrade")]
    [SerializeField] private float existingDuration = 10f;
    [Header("Health rewind shard upgrade")]
    private float savedPercentage;

    protected override void Awake()
    {
        base.Awake();
        currentCharges = maxCharges;
        playerHealth=GetComponentInParent<Entity_Health> ();
    }
    public void CreatShard()
    {
        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        currentShard = shard.GetComponent<SkillObject_Shard>();
        currentShard.SetupShard(this);
        if (isUnlockedUpgrade(SkillUpgradeType.Shard_Teleport) || isUnlockedUpgrade(SkillUpgradeType.Shard_TeleportAndHpRewind))
            currentShard.OnExlopde += ForceCooldown;
    }
    public void CreateRawShard()
    {
        bool canMove = isUnlockedUpgrade(SkillUpgradeType.Shard_MoveToEnemy) || isUnlockedUpgrade(SkillUpgradeType.Shard_MultiCast);
        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        shard.GetComponent<SkillObject_Shard>().SetupShard(this,detonateTime,canMove,shardSpeed);
    }
    private void ForceCooldown()
    {
        if (!IsOnCooldown())
        {
            SetSkillOnCooldown();
            currentShard.OnExlopde -= ForceCooldown;
        }
    }
    public float GetDetonateTime()
    {
        return (isUnlockedUpgrade(SkillUpgradeType.Shard_Teleport) || isUnlockedUpgrade(SkillUpgradeType.Shard_TeleportAndHpRewind)) ? existingDuration : detonateTime;
    }
    public override void TryUseSkill()
    {
        if (!CanUseSkill()) return;
        if (isUnlockedUpgrade(SkillUpgradeType.Shard)) HandleShardRegular();
        if (isUnlockedUpgrade(SkillUpgradeType.Shard_MoveToEnemy)) HandleShardMove();
        if (isUnlockedUpgrade(SkillUpgradeType.Shard_MultiCast)) HandleShardMutiCast();
        if (isUnlockedUpgrade(SkillUpgradeType.Shard_Teleport)) HandleShardTeleport();
        if(isUnlockedUpgrade(SkillUpgradeType.Shard_TeleportAndHpRewind)) HandleShardHealthRewind();
    }
    private void HandleShardHealthRewind()
    {
        if (currentShard == null)
        {
            CreatShard();
            savedPercentage=playerHealth.GetHealthPercent();
        }
        else
        {
            SwapPlayerAndShard();
            playerHealth.SetHealthToPercentage(savedPercentage);
            currentShard.Explode();
            SetSkillOnCooldown();
        }
    }
    private void HandleShardTeleport()
    {
        if (currentShard == null)
        {
            CreatShard();
        }
        else
        {
            SwapPlayerAndShard();
            currentShard.Explode();
            SetSkillOnCooldown();
        }
    }
    private void SwapPlayerAndShard()
    {
        Vector3 shardPosition = currentShard.transform.position;
        Vector3 playerPosition = player.transform.position;
        currentShard.transform.position = playerPosition;
        player.Teleport(shardPosition);
    }
    private void HandleShardMutiCast()
    {
        if (currentCharges <= 0) return;
        CreatShard();
        currentCharges--;
        currentShard.MoveTowardsClosestTarget(shardSpeed);
        if (!isCharging)
            StartCoroutine(ShardChargeCo());
    }
    private IEnumerator ShardChargeCo()
    {
        isCharging = true;
        while (currentCharges < maxCharges)
        {
            currentCharges++;
            yield return new WaitForSeconds(cooldown);
        }
        isCharging = false;
    }
    private void HandleShardRegular()
    {
        CreatShard();
        SetSkillOnCooldown();
    }
    private void HandleShardMove()
    {
        HandleShardRegular();
        currentShard.MoveTowardsClosestTarget(shardSpeed);
    }
}
