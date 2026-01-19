using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
public class PlayerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private Camera thirdPersonCamera;
    [SerializeField] private Image UI;

    [Header("Camera")]
    [SerializeField] private UnityEvent SetFirstPerson;
    [SerializeField] private UnityEvent SetThirdPerson;

    private bool lockCamera = false;
    public void OnOpenInventory(InputValue value)
    {
        UI.gameObject.SetActive(!UI.gameObject.activeSelf);
    }
    public void ChangeCameraMode(Int32 input)
    {
        // 0 is third person
        // 1 is first person
        if (input == 0) SetThirdPerson?.Invoke();
        if (input == 1) SetFirstPerson?.Invoke();
    }
    public void ChangeCameraLock()
    {
        lockCamera = !lockCamera;
    }
}