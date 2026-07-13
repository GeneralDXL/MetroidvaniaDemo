using UnityEngine;
[CreateAssetMenu(menuName = "RPG SetUp/Item Data/Material item", fileName = "Material data - ")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public int maxStackSize = 1;
    [TextArea]
    public string itemInfo;
}
