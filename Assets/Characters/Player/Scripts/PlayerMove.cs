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
        inputReader.jumpEvent += Jump;
    }

    private void OnDisable()
    {
        inputReader.moveEvent -= Move;
        inputReader.lookEvent -= Rotate;
        inputReader.jumpEvent -= Jump;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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
        var playerHeight = _collider.bounds.size.y/2 +0.1f; 
        if (Physics.BoxCast(transform.position, _collider.bounds.extents, transform.up * -1, out RaycastHit hit, Quaternion.identity, playerHeight))
        {
            var jumpForce = transform.up;
            jumpForce *= playerData.jumpForce;
            _rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        _collider = GetComponent<Collider>();
        var playerHeight = _collider.bounds.size.y/2 +0.1f; 
        Gizmos.color = Color.red;
        Physics.BoxCast(transform.position, _collider.bounds.extents*0.5f, transform.up * -1, out RaycastHit hit,
            Quaternion.identity, playerHeight);
        
        Gizmos.DrawRay(transform.position, transform.up * -playerHeight);
        var bounds = _collider.bounds.extents;
        Gizmos.DrawWireCube(transform.position, bounds);
    }
}
