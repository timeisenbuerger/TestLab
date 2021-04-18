using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Collider")] [SerializeField]
    private WheelCollider[] wheelColliders;

    [Header("Wheel Transforms")] [SerializeField]
    private Transform[] wheelTransforms;
    
    [Header("Others")]
    [SerializeField] private GameObject centerOfMass;

    private InputManager inputManager;
    private CarProperties carProperties;
    private Rigidbody _rigidbody;

    private float currentSteerAngle;
    private float currentBrakeForce;
    private float[] slip = new float[4];
    
    private WheelFrictionCurve forwardFriction;
    private WheelFrictionCurve sidewaysFriction;

    private float driftFactor;
    private float tempo;

    private void Awake()
    {
        StartCoroutine(timedLoop());
    }

    private void Start()
    {
        GetObjects();
    }

    private void FixedUpdate()
    {
        UpdateValues();
        ApplyDownForce();
        HandleMotor();
        HandleSteering();
        HandleFriction();
        HandleDrifting();
        UpdateWheels();
    }

    private void GetObjects()
    {
        inputManager = FindObjectOfType<InputManager>();
        carProperties = GetComponent<CarProperties>();
        _rigidbody = GetComponent<Rigidbody>();
        centerOfMass = GameObject.Find("CenterOfMass");
        _rigidbody.centerOfMass = centerOfMass.transform.localPosition;
    }

    private void UpdateValues()
    {
        carProperties.CurrentSpeedInKmH = _rigidbody.velocity.magnitude * 3.6f;
    }

    private void ApplyDownForce()
    {
        _rigidbody.AddForce(-transform.up * (carProperties.DownForce * _rigidbody.velocity.magnitude));
    }

    private void HandleMotor()
    {
        float motorForceToAdd = 0f;
        if (carProperties.CurrentSpeedInKmH < carProperties.MaxSpeedInKmH)
        {
            motorForceToAdd = (carProperties.CarDriveType == CarProperties.DriveType.ALL_WHEELS)
                ? inputManager.verticalInput * (carProperties.MotorForce / 4)
                : inputManager.verticalInput * (carProperties.MotorForce / 2);
        }

        switch (carProperties.CarDriveType)
        {
            case CarProperties.DriveType.ALL_WHEELS:
                foreach (var wheelCollider in wheelColliders)
                {
                    wheelCollider.motorTorque = motorForceToAdd;
                }

                break;
            case CarProperties.DriveType.FRONT_WHEELS:
                foreach (var wheelCollider in wheelColliders)
                {
                    wheelCollider.motorTorque = motorForceToAdd;
                }

                break;
            case CarProperties.DriveType.BACK_WHEELS:
                foreach (var wheelCollider in wheelColliders)
                {
                    wheelCollider.motorTorque = motorForceToAdd;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        currentBrakeForce = inputManager.isBraking ? carProperties.BrakeForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        foreach (var wheelCollider in wheelColliders)
        {
            wheelCollider.brakeTorque = currentBrakeForce;
        }
    }

    private void HandleSteering()
    {
        if (inputManager.horizontalInput > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                wheelColliders[i].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (carProperties.SteeringRadius + (1.5f / 2))) *
                                               inputManager.horizontalInput;
            }
        }
        else if (inputManager.horizontalInput < 0)
        {
            for (int i = 0; i < 2; i++)
            {
                wheelColliders[i].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (carProperties.SteeringRadius - (1.5f / 2))) *
                                               inputManager.horizontalInput;
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                wheelColliders[i].steerAngle = 0;
            }
        }
    }

    private void HandleFriction()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            WheelHit wheelHit;
            wheelColliders[i].GetGroundHit(out wheelHit);

            slip[i] = wheelHit.sidewaysSlip;
        }
    }

    private void UpdateWheels()
    {
        for (int i = 0; i < 4; i++)
        {
            wheelColliders[i].GetWorldPose(out var position, out var rotation);
            wheelTransforms[i].rotation = rotation;
            wheelTransforms[i].position = position;
        }
    }

    private void HandleDrifting()
    {
        //tine it takes to go from normal drive to drift 
        float driftSmoothFactor = .7f * Time.deltaTime;

        if (inputManager.isDrifting)
        {
            sidewaysFriction = wheelColliders[0].sidewaysFriction;
            forwardFriction = wheelColliders[0].forwardFriction;

            float velocity = 0;
            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = forwardFriction.extremumValue =
                forwardFriction.asymptoteValue =
                    Mathf.SmoothDamp(forwardFriction.asymptoteValue, driftFactor * carProperties.DriftStrength,
                        ref velocity, driftSmoothFactor);

            for (var i = 0; i < 4; i++)
            {
                wheelColliders[i].sidewaysFriction = sidewaysFriction;
                wheelColliders[i].forwardFriction = forwardFriction;
            }

            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue =
                forwardFriction.extremumValue = forwardFriction.asymptoteValue = 1.1f;

            //extra grip for the front wheels
            for (var i = 0; i < 2; i++)
            {
                wheelColliders[i].sidewaysFriction = sidewaysFriction;
                wheelColliders[i].forwardFriction = forwardFriction;
            }

            _rigidbody.AddForce(transform.forward * ((carProperties.CurrentSpeedInKmH / 400) * 10000));
        }
        //executed when drifting is being held
        else
        {
            forwardFriction = wheelColliders[0].forwardFriction;
            sidewaysFriction = wheelColliders[0].sidewaysFriction;

            forwardFriction.extremumValue = forwardFriction.asymptoteValue = sidewaysFriction.extremumValue =
                sidewaysFriction.asymptoteValue =
                    ((carProperties.CurrentSpeedInKmH * carProperties.DriftStrength) / 300) + 1;

            for (var i = 0; i < 4; i++)
            {
                wheelColliders[i].forwardFriction = forwardFriction;
                wheelColliders[i].sidewaysFriction = sidewaysFriction;
            }
        }

        //checks the amount of slip to control the drift
        for (var i = 2; i < 4; i++)
        {
            WheelHit wheelHit;

            wheelColliders[i].GetGroundHit(out wheelHit);

            if (wheelHit.sidewaysSlip < 0)
                driftFactor = (1 + -inputManager.horizontalInput) * Mathf.Abs(wheelHit.sidewaysSlip);

            if (wheelHit.sidewaysSlip > 0)
                driftFactor = (1 + inputManager.horizontalInput) * Mathf.Abs(wheelHit.sidewaysSlip);
        }
    }

    private IEnumerator timedLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(.7f);
            carProperties.SteeringRadius = 6 + carProperties.CurrentSpeedInKmH / 20;
        }
    }

    public CarProperties CarProperties => carProperties;
}