using UnityEngine;

public enum SkillUpgradeType 
{
    None,
    // -------dash tree --------
    Dash,
    Dash_CloneOnStart,
    Dash_CloneOnStartAndArrival,
    Dash_ShardOnStart,
    Dash_ShardOnStartAndArrival,
    //---------shard tree ---------
    Shard,
    Shard_MoveToEnemy,
    Shard_MultiCast,
    Shard_Teleport,
    Shard_TeleportAndHpRewind 
}
