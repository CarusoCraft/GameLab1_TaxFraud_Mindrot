using UnityEngine;
using UnityEngine.SceneManagement;

public class End_level_Loader : MonoBehaviour
{
    public void GoToNewScene()
    {
        SceneManager.LoadScene("_EndLevel");
    }


}
