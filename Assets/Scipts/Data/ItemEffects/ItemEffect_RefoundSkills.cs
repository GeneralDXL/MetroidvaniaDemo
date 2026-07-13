using UnityEngine;
[CreateAssetMenu(menuName = "RPG SetUp/Item Data/Item Effect/Refound Skills", fileName = "Item effect data - Refound skills")]
public class ItemEffect_RefoundSkills : ItemEffectDataSO
{
    public override void Execute()
    {
        UI_SkillTree skillTree=FindFirstObjectByType<UI>().skillTree;
        if (skillTree == null) return;
        skillTree.RefoundAllSkills();
    }
}
