using UnityEngine;

public class UI_PlayerStats : MonoBehaviour
{
    private UI_StatSlot[] statSlots;
    private Inventory_Player inventory;
    private Player player;

    private void Awake()
    {
        statSlots=GetComponentsInChildren<UI_StatSlot>();
        player = FindAnyObjectByType<Player>();
        inventory = player.GetComponent<Inventory_Player>();

        player.effectManager.OnEffectsChanged += UpdateStatsUI;
        inventory.OnInventoryChanged += UpdateStatsUI;
    }


    private void UpdateStatsUI()
    {
        foreach (var slot in statSlots) 
            slot.UpdateStatSlotValue();
    }
}
