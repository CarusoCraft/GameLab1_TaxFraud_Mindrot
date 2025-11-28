using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Button_Press : MonoBehaviour
{
    public event Action<Button_Press, bool> PressedChanged;

    // Count of colliders currently pressing the button (supports multiple colliders)
    private int presserCount;

    public bool IsPressed { get; private set; }

    [Header("Audio")]
    public AudioSource pressExitButtonSound;
    private float soundtimer = 0.3f;
    [SerializeField] private bool otherBody = false;
    private bool isplayed = true;


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
        
        if (other.CompareTag("Active"))
        {
            pressExitButtonSound.mute = false;
            pressExitButtonSound?.Play();

        }
        if (other.CompareTag("UsedPlayer"))
        {
            otherBody = true;

            pressExitButtonSound.mute = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        presserCount = Math.Max(0, presserCount - 1);
        if (presserCount == 0) SetPressed(false);

        if (other.CompareTag("Active"))
        {
            isplayed = false;
        }

    }

    private void SetPressed(bool value)
    {
        if (IsPressed == value) return;
        IsPressed = value;
        PressedChanged?.Invoke(this, value);
    }

    private void Update()
    {
        if (otherBody == false && isplayed == false)
        {
            soundtimer -= Time.deltaTime;
            if (soundtimer <= 0)
            {
                pressExitButtonSound?.Play();
                Debug.Log("Play Sound");
                isplayed = true;

                soundtimer = 0.3f;
            }
        }

    }


}