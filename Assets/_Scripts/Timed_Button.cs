using UnityEngine;

public class Timed_Button : MonoBehaviour
{
    [SerializeField]
    private EliasDoor door;
    private float buttonTimer = 0;
    [SerializeField] [Range(5, 25)]
    private float timeBeforeClose;

    private bool isPressed = false;

    private void Update()
    {
        if (isPressed)
        {
            buttonTimer += Time.deltaTime;
            float timer = buttonTimer;
            if (timer > timeBeforeClose)
            {
                isPressed = false;
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
