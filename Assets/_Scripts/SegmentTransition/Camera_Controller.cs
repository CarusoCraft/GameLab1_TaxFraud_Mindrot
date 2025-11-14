using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public GameObject _camera;
    public Camera _camera2;
    [SerializeField]
    private int x = 0;
    [SerializeField]
    private int y = 0;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Active")
        {
            _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z + x);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            _camera2.orthographicSize = y;
        }
        
    }

}
