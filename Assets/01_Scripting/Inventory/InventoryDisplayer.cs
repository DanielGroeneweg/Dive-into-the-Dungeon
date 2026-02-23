using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class InventoryDisplayer : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private ItemDisplayer displayerPrefab;
    [SerializeField] private GridLayoutGroup content;
    [SerializeField] private InventoryManager inventoryManager;
    private List<ItemDisplayer> items = new();
    private void OnEnable()
    {
        Display();
    }
    public void Display()
    {
        // Clear List
        for (int i = items.Count - 1; i >= 0; i--)
        {
            ItemDisplayer item = items[i];
            items.Remove(item);
            Destroy(item.gameObject);
        }

        // Create List
        foreach (Item item in inventory.ItemsInInventory)
        {
            ItemDisplayer displayer = Instantiate(displayerPrefab, transform);
            displayer.item = item;
            displayer.equip.AddListener(inventoryManager.EquipItem);
            items.Add(displayer);
        }

        RectTransform rect = content.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, (content.cellSize.y + content.spacing.y) * Mathf.Ceil(items.Count / content.constraintCount));
    }
}