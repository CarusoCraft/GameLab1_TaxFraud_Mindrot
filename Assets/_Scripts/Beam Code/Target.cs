using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{    
    [Header("UI References")]
    public GameObject DeathMenu;


    void Start()
    {
        DeathMenu.SetActive(false);
        Time.timeScale = 1.0f; // StartGame
    }

    public void Hit()
    {
        DeathMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }
}
