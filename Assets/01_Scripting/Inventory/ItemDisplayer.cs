using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ItemDisplayer : MonoBehaviour
{
    public Item item;
    [SerializeField] private Image icon;
    public UnityEvent<Item> equip;
    public void EquipItem()
    {
        equip?.Invoke(item);
    }
    private void Start()
    {
        icon.sprite = item.Icon;
    }
}