using UnityEngine;

public class Death_menu : MonoBehaviour
{
    public Transform Player_Active;

    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Kill");
    }
}
