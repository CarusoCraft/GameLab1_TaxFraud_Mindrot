
using System.Linq;
using UnityEngine;

public class Button_Reader : MonoBehaviour
{
    [Tooltip("Assign exactly 3 button GameObjects (they must have ButtonPress component). If left empty, the first 3 ButtonPress in scene will be used.")]
    [SerializeField] private Button_Press[] buttons = new Button_Press[0];

    [Tooltip("UI panel to show when all 3 buttons are pressed")]
    [SerializeField] private GameObject panelToOpen;

    private bool panelOpen;

    [Header("Audio")]
    [SerializeField] private GameObject playerSounds;
    [SerializeField] private AudioSource tubeOpenSound;
    [SerializeField] private AudioSource monsterGrowl;


    [SerializeField] private float timer = 5;
    [SerializeField] private bool opening = false;


    void Awake()
    {
        // If user didn't assign buttons, try to auto-find up to 3 in scene.
        if (buttons == null || buttons.Length == 0)
        {
            var found = FindObjectsOfType<Button_Press>().Take(3).ToArray();
            buttons = new Button_Press[found.Length];
            found.CopyTo(buttons, 0);
        }

        // Subscribe to button events
        foreach (var b in buttons)
        {
            if (b != null) b.PressedChanged += OnButtonChanged;
        }


    }

    private void OnDestroy()
    {
        foreach (var b in buttons)
        {
            if (b != null) b.PressedChanged -= OnButtonChanged;
        }
    }

    private void OnButtonChanged(Button_Press button, bool pressed)
    {
        CheckButtons();
    }

    private void CheckButtons()
    {
        // require exactly 3 buttons assigned
        if (buttons == null || buttons.Length < 3)
        {
            // not ready yet
            return;
        }

        bool allPressed = buttons.All(b => b != null && b.IsPressed);
        Debug.Log("All buttons pressed: " + allPressed);

        if (allPressed && !panelOpen)
        {
            opening = true;
            tubeOpenSound.Play();
        }
        else if (!allPressed && panelOpen) ClosePanelAndRestoreInputs();
    }

    private void Update()
    {
        if (opening)
        {
            timer -= Time.deltaTime;
            
        }
        if (timer <= 0)
        {
            OpenPanelAndDisableInputs();
            opening = false;
        }
    }



    private void OpenPanelAndDisableInputs()
    {
        panelOpen = true;
        if (panelToOpen != null) panelToOpen.SetActive(true);

        //mute button sounds
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button_Press>().pressExitButtonSound.mute = true;
        }

        // Disable player movement, swap ability and exit menu manager in scene
        foreach (var m in FindObjectsOfType<PlayerMovement>()) m.enabled = false;
        foreach (var s in FindObjectsOfType<Script_SwapAbility>()) s.enabled = false;
        foreach (var e in FindObjectsOfType<ExitMenuManager>()) e.enabled = false;
        playerSounds.SetActive(false);

        

    }

    private void ClosePanelAndRestoreInputs()
    {
        panelOpen = false;
        if (panelToOpen != null) panelToOpen.SetActive(false);

        foreach (var m in FindObjectsOfType<PlayerMovement>()) m.enabled = true;
        foreach (var s in FindObjectsOfType<Script_SwapAbility>()) s.enabled = true;
        foreach (var e in FindObjectsOfType<ExitMenuManager>()) e.enabled = true;
    }

    // Optional: editor/debug helper
    private void OnValidate()
    {
        if (buttons != null && buttons.Length > 3)
        {
            var arr = new Button_Press[3];
            for (int i = 0; i < 3; i++) arr[i] = buttons[i];
            buttons = arr;
        }
    }
}
