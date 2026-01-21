using System.Xml.Schema;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisplayTester : MonoBehaviour
{
    [SerializeField] private Presenter[] healthPresenters;
    [SerializeField] private Presenter[] manaPresenters;
    [SerializeField] private Presenter[] xpPresenters;

    [SerializeField] private float hpMin;
    [SerializeField] private float hpMax;
    [SerializeField] private float hp;

    [SerializeField] private float manaMin;
    [SerializeField] private float manaMax;
    [SerializeField] private float mana;

    [SerializeField] private float xpMin;
    [SerializeField] private float xpMax;
    [SerializeField] private float xp;

    [SerializeField] private PlayerInput input;
    private void Start()
    {
        foreach (Presenter presenter in healthPresenters)
        {
            if (presenter != null) presenter.SetFillAmount(hpMin, hpMax, hp);
        }

        foreach (Presenter presenter in manaPresenters)
        {
            if (presenter != null) presenter.SetFillAmount(manaMin, manaMax, mana);
        }

        foreach (Presenter presenter in xpPresenters)
        {
            if (presenter != null) presenter.SetFillAmount(xpMin, xpMax, xp);
        }
    }

    public void OnJump(InputValue value)
    {
        hp = Mathf.Clamp(hp + 10, hpMin, hpMax);
        mana = Mathf.Clamp(mana + 10, manaMin, manaMax);
        xp = Mathf.Clamp(xp + 10, xpMin, xpMax);

        foreach (Presenter presenter in healthPresenters)
        {
            if (presenter != null) presenter.SetFillAmount(hpMin, hpMax, hp);
        }

        foreach (Presenter presenter in manaPresenters)
        {
            if (presenter != null) presenter.SetFillAmount(manaMin, manaMax, mana);
        }

        foreach (Presenter presenter in xpPresenters)
        {
            if (presenter != null) presenter.SetFillAmount(xpMin, xpMax, xp);
        }
    }
    public void OnAttack(InputValue value)
    {
        hp = Mathf.Clamp(hp - 10, hpMin, hpMax);
        mana = Mathf.Clamp(mana - 10, manaMin, manaMax);
        xp = Mathf.Clamp(xp - 10, xpMin, xpMax);

        foreach (Presenter presenter in healthPresenters)
        {
            if (presenter != null) presenter.SetFillAmount(hpMin, hpMax, hp);
        }

        foreach (Presenter presenter in manaPresenters)
        {
            if (presenter != null) presenter.SetFillAmount(manaMin, manaMax, mana);
        }

        foreach (Presenter presenter in xpPresenters)
        {
            if (presenter != null) presenter.SetFillAmount(xpMin, xpMax, xp);
        }
    }
}