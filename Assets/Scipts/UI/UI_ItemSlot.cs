using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour,IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Slot Setup")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemStackSize;
    [SerializeField] private Sprite defaultIcon;
    public Inventory_Item itemInSlot { get; private set; }
    protected Inventory_Player inventory;
    private UI ui;
    private RectTransform rect;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(itemInSlot == null) return;
        
        if (itemInSlot.itemData is EquipmentDataSO)
            inventory.TryEquipItem(itemInSlot);
        else if (itemInSlot.itemData.itemType == ItemType.Comsumable)
            inventory.TryConsumeItem(itemInSlot);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(itemInSlot==null) return;
        ui.itemToolTip.ShowToolTip(true, rect,itemInSlot.itemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.itemToolTip.ShowToolTip(false, rect);
    }

    protected void Awake()
    {
        inventory = FindAnyObjectByType<Inventory_Player>();
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
    }
    public void UpdateSlot(Inventory_Item item)
    {
        itemInSlot = item;
        Color color = Color.white; color.a = 0.9f;
        itemIcon.color = color;

        if(itemInSlot == null )
        {
            itemIcon.sprite = defaultIcon;
            itemStackSize.text = " ";
            return;
        }
        itemIcon.sprite = item.itemData.itemIcon;
        //itemStackSize.text 
        itemStackSize.text = item.stackSize + "/" + item.itemData.maxStackSize;
    }

   
}
