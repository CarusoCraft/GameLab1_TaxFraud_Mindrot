using UnityEngine;
using UnityEngine.EventSystems;

public class EliasButton : MonoBehaviour
{
    // Checking if there is a player on the button
    [SerializeField] private bool playerOnButton = false;
    [SerializeField] private bool doorOpen = false;
    private float closingTimer = 0.5f;

    public EliasDoor door;


    //Private to say a object has enter the button
    private void OnTriggerStay(Collider other)
    {
        if (doorOpen == false)
        {
            door.DoorOpen();
            doorOpen = true;
            playerOnButton = true;
        }
        else
        {
            closingTimer = 0.5f;
            playerOnButton = true;
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        playerOnButton = false;

    }

    private void Update()
    {
        if (playerOnButton == false && doorOpen == true)
        {
            closingTimer -= Time.deltaTime;

            if (closingTimer <= 0f)
            {
                door.DoorClose();
                doorOpen = false;
                closingTimer = 0.5f;
            }

        }
    }




}
