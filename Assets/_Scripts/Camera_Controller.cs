using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public GameObject _camera;
    public float[] positions = {1, 2, 3};
    private int x;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Active")
        {
            _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, positions[x]);
            x++;
        }
        
    }
}
