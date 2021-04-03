using UnityEngine;

public class CarController : MonoBehaviour
{
    internal enum DriveType
    {
        FRONT_WHEELS,
        BACK_WHEELS,
        ALL_WHEELS
    }
    
    private InputManager inputManager;
    private Rigidbody _rigidbody;
    
    private float currentSteerAngle;
    private float currentBrakeForce;

    [Header("Force")] [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] public float speedInKmH;
    [SerializeField] private float radius = 6f;
    [SerializeField] private DriveType driveType;

    [Header("Wheel Collider")] [SerializeField]
    private WheelCollider frontLeftWheelCollider;

    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;

    [Header("Wheel Transforms")] [SerializeField]
    private Transform frontLeftWheelTransform;

    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;

    private void Start()
    {
        GetObjects();
    }

    private void FixedUpdate()
    {
        InitValues();
        HandleMotor();
        HandleSteering();
        HandleDrifting();
        UpdateWheels();
    }

    private void GetObjects()
    {
        inputManager = FindObjectOfType<InputManager>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void InitValues()
    {
        speedInKmH = _rigidbody.velocity.magnitude * 3.6f;
    }

    private void HandleMotor()
    {
        if (driveType == DriveType.ALL_WHEELS)
        {
            frontLeftWheelCollider.motorTorque = inputManager.verticalInput * (motorForce / 4);
            frontRightWheelCollider.motorTorque = inputManager.verticalInput * (motorForce / 4);
            backLeftWheelCollider.motorTorque = inputManager.verticalInput * (motorForce / 4);
            backRightWheelCollider.motorTorque = inputManager.verticalInput * (motorForce / 4);
        }
        else if (driveType == DriveType.FRONT_WHEELS)
        {
            frontLeftWheelCollider.motorTorque = inputManager.verticalInput * (motorForce / 2);
            frontRightWheelCollider.motorTorque = inputManager.verticalInput * (motorForce / 2);
        }
        else
        {
            backLeftWheelCollider.motorTorque = inputManager.verticalInput * (motorForce / 2);
            backRightWheelCollider.motorTorque = inputManager.verticalInput * (motorForce / 2);
        }
        
        currentBrakeForce = inputManager.isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        backLeftWheelCollider.brakeTorque = currentBrakeForce;
        backRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        
        if (inputManager.horizontalInput > 0)
        {
            frontLeftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * inputManager.horizontalInput;
            frontRightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * inputManager.horizontalInput;
        }
        else if (inputManager.horizontalInput < 0)
        {
            frontLeftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * inputManager.horizontalInput;
            frontRightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * inputManager.horizontalInput;
        }
        else
        {
            frontLeftWheelCollider.steerAngle = 0;
            frontRightWheelCollider.steerAngle = 0;
        }
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        wheelCollider.GetWorldPose(out var position, out var rotation);
        wheelTransform.rotation = rotation * new Quaternion(0, 0, 180, 0);
        wheelTransform.position = position;
    }

    private void HandleDrifting()
    {
    }
}