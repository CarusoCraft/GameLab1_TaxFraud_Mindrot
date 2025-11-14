using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Button_Press : MonoBehaviour
{
    public event Action<Button_Press, bool> PressedChanged;

    // Count of colliders currently pressing the button (supports multiple colliders)
    private int presserCount;

    public bool IsPressed { get; private set; }

    private void Reset()
    {
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Accept any object (any collider) as a presser
        presserCount++;
        if (presserCount == 1) SetPressed(true);
    }

    private void OnTriggerExit(Collider other)
    {
        presserCount = Math.Max(0, presserCount - 1);
        if (presserCount == 0) SetPressed(false);
    }

    private void SetPressed(bool value)
    {
        if (IsPressed == value) return;
        IsPressed = value;
        PressedChanged?.Invoke(this, value);
    }
}