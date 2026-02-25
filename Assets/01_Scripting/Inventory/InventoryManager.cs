using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    private void OnEnable()
    {
        EventBusManager.Instance.GetItemEvent.Register(AddItem);
    }
    private void OnDisable()
    {
        EventBusManager.Instance.GetItemEvent.Unregister(AddItem);
    }
    private void AddItem(GetItemEventData data)
    {
        if (data.Item is Potion potion) inventory.AddPotion(potion);
        else inventory.AddItem(data.Item);
    }
    private void Start()
    {
        Locator.instance.PotionPresenter.SetValue(0, float.PositiveInfinity, inventory.equippedItems.hpPotions.Count);
    }
}