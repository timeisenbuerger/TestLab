using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject car;
    private CarController carController;
    
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player");
        carController = car.GetComponentInChildren<CarController>();
    }

    public GameObject Car => car;

    public CarController CarController => carController;
}
