using UnityEngine;
public class EnemyHealthPresenter : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Presenter presenter;
    public void Present(float min, float max, float current)
    {
        presenter.SetValue(min, max, current);
    }
    private void OnEnable()
    {
        health.healthChanged += Present;
    }
    private void OnDisable()
    {
        health.healthChanged -= Present;
    }
    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}