using UnityEngine;

public class KillingFloor : MonoBehaviour
{
    public GameObject DeathMenu;
    public GameObject Player_Active;


    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.name == "Player_Active")
        {
            PlayerGameOver();
            DeathMenu.SetActive(true);
            Player_Active.SetActive(false);
        }
    }

    void PlayerGameOver()
    {
        Debug.Log("Dead");

    }
}
