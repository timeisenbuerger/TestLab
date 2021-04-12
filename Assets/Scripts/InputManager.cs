using UnityEngine;

public class InputManager : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string BREAKING = "Jump";
    
    public float horizontalInput;
    public float verticalInput;
    public bool isBraking;
    public bool isDrifting;
    
    
    void FixedUpdate()
    {
        GetInput();
    }
    
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBraking = Input.GetKey(KeyCode.LeftShift);
        isDrifting = Input.GetKey(KeyCode.Space);
    }
    
    
}
