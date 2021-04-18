using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffectsController : MonoBehaviour
{
    private InputManager _inputManager;
    public TrailRenderer[] tireMarks;
    private bool tireMarksFlag;
    
    void Start()
    {
        _inputManager = FindObjectOfType<InputManager>();
    }

    void Update()
    {
        CheckDrifting();
    }

    private void CheckDrifting()
    {
        if (_inputManager.isDrifting)
        {
            StartTrailEmitter();
        }
        else
        {
            StopTrailEmitter();
        }
    }

    private void StartTrailEmitter()
    {
        if (tireMarksFlag)
        {
            return;
        }

        foreach (var trailRenderer in tireMarks)
        {
            trailRenderer.emitting = true;
        }

        tireMarksFlag = true;
    }
    
    private void StopTrailEmitter()
    {
        if (!tireMarksFlag)
        {
            return;
        }

        foreach (var trailRenderer in tireMarks)
        {
            trailRenderer.emitting = false;
        }

        tireMarksFlag = false;
    }
}
