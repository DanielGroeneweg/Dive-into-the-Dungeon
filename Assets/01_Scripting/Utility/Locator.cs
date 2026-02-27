using UnityEngine;
public class Locator : MonoBehaviour
{
    public static Locator instance;
    [SerializeField] private Transform player;
    [SerializeField] private Presenter potionPresenter;
    [SerializeField] private Inventory inventory;
    public Transform Player => player;
    public Presenter PotionPresenter => potionPresenter;
    public Inventory Inventory => inventory;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

}
