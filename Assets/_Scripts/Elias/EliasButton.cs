using UnityEngine;
using UnityEngine.EventSystems;

public class EliasButton : MonoBehaviour
{
    // Checking if there is a player on the button
    public bool playerOnButton = false;

    public EliasDoor door;

   
    //Private to say a object has enter the button
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player enter the button");
        door.DoorOpen();
        
    }


    //Private to say a object has exit the button
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player exit the button");
        door.DoorClose();
    }


}
