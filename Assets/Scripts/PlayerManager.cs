using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject car;
    public CarController carController;
    
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player");
        carController = car.GetComponentInChildren<CarController>();
    }
}
