using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Data;


public class CharacterMovement1 : MonoBehaviour
{

    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    [SerializeField] private float smoothTime = 0.5f;
    private float _currentVelocity;

    [SerializeField] private float speed;

    private void Awake()
    {
       _characterController = GetComponent<CharacterController>();
    }


    public void Move(InputAction.CallbackContext context)
     {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0, _input.y);
    }


    void Update()
    {
        if (_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        _characterController.Move(_direction * speed * Time.deltaTime);
    }
}