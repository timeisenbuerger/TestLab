using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    private Transform target;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        target = playerManager.car.transform;
    }

    void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

    private void HandleTranslation()
    {
        translateSpeed = (playerManager.carController.speedInKmH >= 50) ? 20 : playerManager.carController.speedInKmH / 4;
        
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
