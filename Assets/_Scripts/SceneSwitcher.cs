using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private int sceneNumber;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Active")
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
