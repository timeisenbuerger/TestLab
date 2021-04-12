using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowFromVideo : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject cameraConstraint;
    [SerializeField] private GameObject cameraLookAt;
    [SerializeField] private float speed = 20;
    [SerializeField] private float defaultFOV = 0;
    [SerializeField] private float desiredFOV = 0;
    [SerializeField] [Range(0,3)] private float smoothTime = 0;

    void Awake()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        _playerManager = FindObjectOfType<PlayerManager>();
        inputManager = FindObjectOfType<InputManager>();
        cameraConstraint = player.transform.Find("CameraConstraint").gameObject;
        cameraLookAt = player.transform.Find("CameraLookAt").gameObject;
        defaultFOV = mainCamera.fieldOfView;
    }

    void FixedUpdate()
    {
        Follow();
        // BoostFOV();
    }

    private void Follow()
    {
        speed = Mathf.Lerp(speed, _playerManager.CarController.speedInKmH / 2, Time.deltaTime);
        
        gameObject.transform.position = Vector3.Lerp(transform.position, cameraConstraint.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(cameraLookAt.gameObject.transform.position);
    }

    private void BoostFOV()
    {
        if (inputManager.isDrifting)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, desiredFOV, Time.deltaTime * smoothTime);
        }
        else
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, defaultFOV, Time.deltaTime * smoothTime);;
        }
    }
}
