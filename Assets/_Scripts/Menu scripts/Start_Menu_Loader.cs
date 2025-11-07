using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu_Loader : MonoBehaviour
{

    public void GoToNewScene()
    {
        SceneManager.LoadScene("Start Menu");
    }

}