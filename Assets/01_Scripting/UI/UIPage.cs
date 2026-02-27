using UnityEngine;
public class UIPage : MonoBehaviour
{
    [SerializeField] private string pageName;
    public string Name => pageName;
    private void Awake()
    {
        if (string.IsNullOrEmpty(pageName)) pageName = gameObject.name;
    }
}