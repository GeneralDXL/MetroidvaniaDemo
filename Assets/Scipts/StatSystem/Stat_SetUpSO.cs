using UnityEngine;


[CreateAssetMenu(menuName ="RPG SetUp/Default Stat Setup",fileName ="Default Stat Setup")]
public class Stat_SetUpSO : ScriptableObject
{
    [Header("Resources")]
    public float maxHealth = 100;
    public float healthRegen = 0;

    [Header("Major")]
    public float strength;
    public float agility;
    public float vitality;
    public float intelligence;

    [Header("Offense - Pyhsical Damage")]
    public float attackSpeed = 1;
    public float damage = 10;
    public float critChance;
    public float critPower = 150;
    public float armorReduction;

    [Header("Offense - Elemental Damage")]
    public float fireDamage;
    public float iceDamage;
    public float lightningDamage;

    [Header("Defense - Physical Damage")]
    public float armor;
    public float evasion;

    [Header("Defense Elemental Damage")]
    public float fireResistance;
    public float iceResistance;
    public float lightningResistance;
    
}
