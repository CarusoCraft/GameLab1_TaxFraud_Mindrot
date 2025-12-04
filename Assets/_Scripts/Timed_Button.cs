using UnityEngine;

public class Timed_Button : MonoBehaviour
{
    [SerializeField] private EliasDoor door;
    [SerializeField] private float buttonTimer = 0;
    [SerializeField][Range(5, 25)] private float timeBeforeClose;

    [SerializeField] private bool isPressed = false;
    private bool doorOpen = false;



    [Header("Audio")]
    [SerializeField] private bool playedSlowSound = false;
    [SerializeField] private bool playedFastSound = false;
    [SerializeField] private AudioSource tickingFast;
    [SerializeField] private AudioSource tickingSlow;


    private void Update()
    {

        if (isPressed == true)
        {
            buttonTimer += Time.deltaTime;

            if (playedSlowSound == true)
            {
                Debug.Log(buttonTimer - timeBeforeClose);
            }

            if (playedSlowSound == false)
            {
                tickingSlow.Play();
                playedSlowSound = true;
            }
            else if (playedFastSound == false && timeBeforeClose - buttonTimer <= 3f )
            {
                tickingFast.Play();
                tickingSlow.mute = true;

                playedFastSound = true;
            }

            if (buttonTimer >= timeBeforeClose)
            {
                isPressed = false;
                playedSlowSound = false;
                tickingSlow.mute = false;
                playedFastSound = false;
                doorOpen = false;
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
        if (doorOpen == false)
        {
            door.DoorOpen();
            doorOpen = true;
        }
    }
}
