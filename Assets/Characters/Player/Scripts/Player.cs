using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player")]
public class Player : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;
    public float jumpForce;
    
    [Header("Camera")]
    public float sensitivity;
    public float clampLowerLimit;
    public float clampUpperLimit;
    public float cameraOffset;
    public float cameraHeight;
}
