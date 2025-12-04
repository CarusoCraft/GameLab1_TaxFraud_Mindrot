using UnityEngine;

public class KillingFloor : MonoBehaviour
{
    public GameObject DeathMenu;
    public GameObject Player_Active;


    private void OnTriggerEnter(Collider other)
    {
            PlayerGameOver();
            Player_Active.SetActive(false);
            Debug.Log("Player died in a hole");
    }

    public void PlayerGameOver()
    {
        DeathMenu.SetActive(true);
    }
}
