using UnityEngine;
using UnityEngine.UI;

public class ExitMenuManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseMenu;  // Assign the PauseMenu panel here
    public Button closeButton;    // Assign the Close Button here
    public Button extraCloseButton; // Assign the Extra Close Button here

    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
        closeButton.onClick.AddListener(CloseMenu);
        extraCloseButton.onClick.AddListener(CloseMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                CloseMenu();
            else
                OpenMenu();
        }
    }

    void OpenMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    void CloseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }
}