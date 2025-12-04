using UnityEngine;

public class KillingFloor : MonoBehaviour
{
    public GameObject DeathMenu;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Active"))
        {
            PlayerGameOver();
            DeathMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void PlayerGameOver()
    {
        Debug.Log("Dead");

    }
}
