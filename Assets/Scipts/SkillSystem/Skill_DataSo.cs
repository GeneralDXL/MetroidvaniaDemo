using UnityEngine;
using System;

[CreateAssetMenu(menuName = "RPG SetUp/Skill Data", fileName = "Skill data - ")]
public class Skill_DataSo : ScriptableObject
{
    [Header("Unlock & Upgrades")]
    public int cost;
    public SkillType skillType;
    public bool isUnlockedByDefault;
    public UpgradeData upgradeData;
    [Header("Skill description")]
    public Sprite icon;
    public string displayName;
    [TextArea]
    public string description;
}

[Serializable]
public class UpgradeData
{
    public float cooldown;
    public SkillUpgradeType upgradeType;
    public DamageScaleData damageScale;
}