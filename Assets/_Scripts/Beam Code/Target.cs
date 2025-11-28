using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{    
    [Header("UI References")]
    public GameObject DeathMenu;
    public Button closeButton;    // Assign the Close Button here
    private bool isPaused = false;
    void Start()
{
    DeathMenu.SetActive(false);
    closeButton.onClick.AddListener(CloseMenu);
    }

    public void Hit()
    {
        DeathMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }
    void CloseMenu()
    {
        DeathMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }
}
