using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class SpellComponentButton : MonoBehaviour
{
    [SerializeField] private SpellComponent component;
    [SerializeField] private UnityEvent<SpellComponent> selectComponent;
    [SerializeField] private UnityEvent<SpellComponent> hoverComponent;
    [SerializeField] private Image image;
    public SpellComponent Component { get { return component; } }
    public void Select()
    {
        selectComponent?.Invoke(component);
    }
    public void Hover()
    {
        hoverComponent?.Invoke(component);
    }
    private void OnEnable()
    {
        image.sprite = component.Icon;
    }
}