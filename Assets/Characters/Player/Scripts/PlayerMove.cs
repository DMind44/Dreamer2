using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Player playerData;
    private Collider _collider;
    private Rigidbody _rigidbody;
    private Vector3 _move;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        inputReader.moveEvent += Move;
        inputReader.lookEvent += Rotate;
    }

    private void OnDisable()
    {
        inputReader.moveEvent -= Move;
        inputReader.lookEvent -= Rotate;
    }

    private void FixedUpdate()
    {
        _rigidbody.AddRelativeForce(_move, ForceMode.Acceleration);
        var flatVelocity = _rigidbody.linearVelocity;
        flatVelocity.y = 0;
        if (flatVelocity.magnitude > playerData.moveSpeed)
        {
            var velocity = flatVelocity.normalized *  playerData.moveSpeed;
            velocity.y = _rigidbody.linearVelocity.y;
            _rigidbody.linearVelocity = velocity;
        }
    }

    public void Move(Vector2 direction)
    {
        _move = new Vector3(direction.x, 0, direction.y);
        _move *= playerData.acceleration;
    }

    public void Rotate(Vector2 direction)
    {
        var y = direction.x * playerData.sensitivity;
        transform.Rotate(new Vector3(0, y, 0));
    }

    public void Jump()
    {
        var castDist = 0.1f; 
        var halfExtents = new Vector3(_collider.bounds.extents.x/2, castDist, _collider.bounds.extents.z/2);
        var castOrigin = transform.position - new Vector3(0, _collider.bounds.extents.y/2 + castDist, 0);
        if (Physics.BoxCast(castOrigin, halfExtents, transform.up * -1, out RaycastHit hit, transform.localRotation, castDist))
        {
            var jumpForce = transform.up;
            jumpForce *= playerData.jumpForce;
            _rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        }
    }
}
