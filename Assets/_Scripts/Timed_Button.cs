using UnityEngine;

public class Timed_Button : MonoBehaviour
{
    [SerializeField] private EliasDoor door;
    [SerializeField] private float buttonTimer = 0;
    [SerializeField][Range(5, 25)] private float timeBeforeClose;

    [SerializeField] private bool isPressed = false;


    [Header("Audio")]
    private bool playedSlowSound = false;
    private bool playedFastSound = false;
    [SerializeField] private AudioSource tickingFast;
    [SerializeField] private AudioSource tickingSlow;


    private void Update()
    {
        if (isPressed == true)
        {
            buttonTimer += Time.deltaTime;

            if (playedSlowSound == false)
            {
                tickingSlow.Play();
                playedSlowSound = true;
            }
            else if (playedFastSound == false && buttonTimer >= 3)
            {
                tickingFast.Play();
                tickingSlow.mute = true;

                playedFastSound = true;
            }

            if (buttonTimer >= timeBeforeClose)
            {
                isPressed = false;
                playedSlowSound = false;
                playedFastSound = false;
                tickingFast.Stop();
                tickingSlow.Stop();
                door.DoorClose();
                buttonTimer = 0;
            }
           

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        isPressed = true;
        door.DoorOpen();
    }
}
