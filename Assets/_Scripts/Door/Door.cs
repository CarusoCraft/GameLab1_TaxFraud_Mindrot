using UnityEngine;

public class Door : MonoBehaviour
{
    //Set't the active of the door to falls
    public void DoorOpen()
    {
        gameObject.SetActive(false);
        Debug.Log("Door opens");
    }


    //Set't the active of the door to true
    public void DoorClose()
    {
        gameObject.SetActive(true);
        Debug.Log("Door close's");
    }
}
