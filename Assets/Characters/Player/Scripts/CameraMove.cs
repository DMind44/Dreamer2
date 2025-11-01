using System;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform target;
    [SerializeField] private Player playerData;
    
    private float _xRotation;


    private void OnEnable()
    {
        inputReader.lookEvent += Look;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.localPosition + transform.forward * -playerData.cameraOffset +  transform.up * playerData.cameraHeight;
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    }

    public void Look(Vector2 lookDirection)
    {
        _xRotation += lookDirection.y * playerData.sensitivity;
        _xRotation = Mathf.Clamp(_xRotation, playerData.clampLowerLimit, playerData.clampUpperLimit);
    }
}
