using UnityEngine;

public class EliasDoor : MonoBehaviour
{
    [SerializeField] private AudioSource openSound;
    [SerializeField] private AudioSource closeSound;





    //When the door is open
    public void DoorOpen()
    {
        gameObject.SetActive(false);
        Debug.Log("Door opens");
        openSound.Play();
    }


    //When the door is closed
    public void DoorClose()
    {
        gameObject.SetActive(true);
        Debug.Log("Door close's");
        closeSound.Play();
    }
}
