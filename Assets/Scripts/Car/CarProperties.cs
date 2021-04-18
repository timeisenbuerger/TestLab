using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarProperties : MonoBehaviour
{
    public enum DriveType
    {
        FRONT_WHEELS,
        BACK_WHEELS,
        ALL_WHEELS
    }
    
    [Header("Force")] 
    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float downForce = 50f;

    [Header("Steering and Drifting")]
    [SerializeField] private float steeringRadius = 6f;
    [SerializeField] private float driftStrength = 2f;
    
    [Header("Speed")]
    [SerializeField] private float currentSpeedInKmH = 0f;
    [SerializeField] private float maxSpeedInKmH = 150f;
    
    [Header("Drive Type")]
    [SerializeField] private DriveType driveType;
    

    public float MotorForce => motorForce;

    public float BrakeForce => brakeForce;

    public float DownForce => downForce;

    public float SteeringRadius
    {
        get => steeringRadius;
        set => steeringRadius = value;
    }

    public float DriftStrength => driftStrength;

    public float CurrentSpeedInKmH
    {
        get => currentSpeedInKmH;
        set => currentSpeedInKmH = value;
    }

    public float MaxSpeedInKmH => maxSpeedInKmH;

    public DriveType CarDriveType => driveType;
}
