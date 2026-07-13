using UnityEngine;

public class Object_ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemDataSO itemData;
    private SpriteRenderer sr;
    private Inventory_Item item;
    private Inventory_Base inventory;

    private void Awake()
    {
        item=new Inventory_Item(itemData);
    }
    private void OnValidate()
    {
        if (itemData == null) return;
        sr=GetComponent<SpriteRenderer>();
        sr.sprite = itemData.itemIcon;
        gameObject.name = "Object_ItemPickup - " + itemData.itemName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inventory=collision.GetComponent<Inventory_Base>();
        if (inventory !=null && (inventory.CanAddItem()||inventory.CanAddStack(item)))
        {
            inventory.AddItem(item);
            Destroy(gameObject);
        }
    }
}
