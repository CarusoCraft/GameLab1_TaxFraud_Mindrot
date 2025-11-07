using UnityEngine;
using UnityEngine.SceneManagement;

public class level_1_Loader : MonoBehaviour
{

    public void GoToNewScene()
    {
        SceneManager.LoadScene("Level1");
    }

}