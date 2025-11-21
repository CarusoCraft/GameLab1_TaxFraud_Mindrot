using UnityEngine;

public class EliasDoor : MonoBehaviour
{
    //Speed that the door opens/closes at
    private float speed = 1f;

    

    //Where the door will move open and close / down and up
    [SerializeField] private Transform targetDown;
    [SerializeField] private Transform targetUp;
    
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
