using UnityEngine;

public class DoorButton : MonoBehaviour
{
    //References the Door GameObject
    public Door door;

    public bool activeButton = false;


    //Opens the door when the player collides with the button
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Player enter the button");
        door.DoorOpen();
        activeButton = true;
    }


    //Closes the door when the player leaves the button collider
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player exit the button");
        if(activeButton == true)
        {
            door.DoorClose();
        }
        
    }


}
