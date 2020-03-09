using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class FPSPlayerMovement : MonoBehaviour
{
    [SerializeField] private float _defaultMovementSpeed = 5f;
    [SerializeField] private float _shiftMovementSpeed = 10f;
    [SerializeField] private float _jumpForce = 5f;

    private Rigidbody _body;
    private Collider _collider;
    private float _currentMovementSpeed;
    private float _verticalSpeed;
    private float _horizontalSpeed;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _currentMovementSpeed = _defaultMovementSpeed;
    }

    private void Update()
    {
        Walk();
        Jump();
    }

    private void Walk()
    {
        _verticalSpeed = Input.GetAxis("Vertical") * _currentMovementSpeed * Time.deltaTime;
        _horizontalSpeed = Input.GetAxis("Horizontal") * _currentMovementSpeed * Time.deltaTime;
        transform.Translate(_horizontalSpeed, 0, _verticalSpeed);

        _currentMovementSpeed = Input.GetKey(KeyCode.LeftShift) ? _shiftMovementSpeed : _defaultMovementSpeed;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnGround())
        {
            _body.AddForce(0, _jumpForce, 0, ForceMode.Impulse);
        }
    }

    private bool OnGround()
    {
        return Physics.Raycast(transform.position + Vector3.down * (_collider.bounds.size.y / 2 - 0.01f), Vector3.down, 0.2f);
    }
}
