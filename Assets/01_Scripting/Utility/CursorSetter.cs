using UnityEngine;
public class CursorSetter : MonoBehaviour
{
    public void LockCursor() { Cursor.lockState = CursorLockMode.Locked; }
    public void UnlockCursor() { Cursor.lockState = CursorLockMode.None; }
}