using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Active")
        {
            SceneManager.LoadScene(2);
        }
    }
}
