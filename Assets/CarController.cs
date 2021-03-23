using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [System.Serializable]
    public class AxialInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;

        public bool motor;
        public bool steering;
    }

    public List<AxialInfo> axialInfos = new List<AxialInfo>();
    public float maxMotorTorque;
    public float maxSteeringAngle;
    
    void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxialInfo axialInfo in axialInfos)
        {
            if (axialInfo.steering)
            {
                axialInfo.leftWheel.steerAngle = steering;
                axialInfo.rightWheel.steerAngle = steering;
            }

            if (axialInfo.motor)
            {
                axialInfo.leftWheel.motorTorque = motor;
                axialInfo.rightWheel.motorTorque = motor;
            }
            
            applyLocalPositionToVisual(axialInfo.leftWheel);
            applyLocalPositionToVisual(axialInfo.rightWheel);
        }
    }

    public void applyLocalPositionToVisual(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);
        Vector3 position;
        Quaternion rotation;
        
        collider.GetWorldPose(out position, out rotation);

        rotation = rotation * Quaternion.Euler(new Vector3(0, 90, 0));
        
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}
