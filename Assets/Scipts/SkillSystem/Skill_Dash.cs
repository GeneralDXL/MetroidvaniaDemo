using System.Net.NetworkInformation;
using UnityEngine;

public class Skill_Dash : Skill_Base
{

    public override void OnStartEffect()
    {
        base.OnStartEffect();
        if (isUnlockedUpgrade(SkillUpgradeType.Dash_CloneOnStart) || isUnlockedUpgrade(SkillUpgradeType.Dash_CloneOnStartAndArrival))
            CreateClone();
        if (isUnlockedUpgrade(SkillUpgradeType.Dash_ShardOnStart) || isUnlockedUpgrade(SkillUpgradeType.Dash_ShardOnStartAndArrival))
            CreateShard();
    }

    public override void OnEndEffect()
    {
        base.OnEndEffect();
        if(isUnlockedUpgrade(SkillUpgradeType.Dash_CloneOnStartAndArrival))
            CreateClone();
        if (isUnlockedUpgrade(SkillUpgradeType.Dash_ShardOnStartAndArrival))
            CreateShard();
    }
    private void CreateClone()
    {
        Debug.Log("create time echo.");
    }
    private void CreateShard()
    {
        skillManager.shard.CreateRawShard();
    }
}
