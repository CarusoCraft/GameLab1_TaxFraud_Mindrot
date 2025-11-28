using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public GameObject _camera;
    public Camera _camera2;
    [SerializeField]
    private float x = 0;
    [SerializeField]
    private float y = 0;
    [SerializeField]
    private float z = 0;
    [SerializeField]
    private float orthographicSize = 0;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Active")
        {
            _camera.transform.position = new Vector3(_camera.transform.position.x + x, _camera.transform.position.y + y, _camera.transform.position.z + z);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            _camera2.orthographicSize = orthographicSize;
        }
        
    }

}
