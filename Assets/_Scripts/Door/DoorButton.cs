using UnityEngine;

public class DoorButton : MonoBehaviour
{
    //References the Door GameObject
    public Door door;


    //Opens the door when the player collides with the button
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player enter the button");
        door.DoorOpen();

    }


    //Closes the door when the player leaves the button collider
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player exit the button");
        door.DoorClose();
    }


}
