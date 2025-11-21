using UnityEngine;

public class Timed_Button : MonoBehaviour
{
    [SerializeField]
    private EliasDoor door;
    [SerializeField]
    private float buttonTimer = 0;
    [SerializeField] [Range(10, 20)]
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
