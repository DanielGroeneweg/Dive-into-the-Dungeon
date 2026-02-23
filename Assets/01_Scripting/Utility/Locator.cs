using UnityEngine;
public class Locator : MonoBehaviour
{
    public static Locator instance;
    [SerializeField] private Transform player;
    public Transform Player => player;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

}
