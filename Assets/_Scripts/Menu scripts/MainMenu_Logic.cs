using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Logic : MonoBehaviour
{
    public GameObject ButtonLevel1;
    public GameObject ButtonLevel2;
    public GameObject ButtonLevel3;


    //Activates the level selector
    public void SelectLevelMeny()
    {
        ButtonLevel1.SetActive(true);
        ButtonLevel2.SetActive(true);
        ButtonLevel3.SetActive(true);
    }

    //Closes the game
    public void QuitGame()
    {
        Application.Quit();
    }



}
