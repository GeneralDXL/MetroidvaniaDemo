using UnityEngine;
[CreateAssetMenu(menuName = "RPG SetUp/Item Data/Item Effect/Heal Effect", fileName = "Item effect data - Heal")]
public class ItemEffect_Heal : ItemEffectDataSO
{
    [SerializeField] private float healPercent = 0.1f;

    public override void Execute()
    {
        Player player = FindFirstObjectByType<Player>();
        float healAmount = player.stats.GetMaxHealth() * healPercent;
        player.health.IncreaseHealth(healAmount);
    }
}
