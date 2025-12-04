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

    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3f;
    private float _velocity;
    private void Awake()
    {
       _characterController = GetComponent<CharacterController>();
    }


    


    void Update()
    {
        ApplyRotation();
        ApplyMovement();
        ApplyGravity();
    }

    private void ApplyGravity() 
    { 
    _velocity += _gravity * gravityMultiplier * Time.deltaTime;
    _direction.y = _velocity;
    }


    private void ApplyRotation()
        {
            if (_input.sqrMagnitude == 0) return;
            var targetAngle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }

    private void ApplyMovement()
    {
        _characterController.Move(_direction * speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
     {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0, _input.y);
    }
}