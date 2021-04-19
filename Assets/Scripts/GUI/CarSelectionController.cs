using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectionController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject carPlatform;
    [SerializeField] private GameObject[] cars;
    [SerializeField] private GameObject flatArea;
    [SerializeField] private StatBar[] statBars = new StatBar[3];

    private GameObject currentCar;
    [SerializeField] private CarProperties currentCarProperties;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
    }

    void Update()
    {
        flatArea.transform.Rotate(0, 0.1f, 0);
        currentCar.transform.Rotate(0, 0.1f, 0);
    }

    public void ChangeCurrentCar(int index)
    {
        if (!carPlatform.activeSelf)
        {
            carPlatform.SetActive(true);
        }
        
        Destroy(currentCar);

        currentCar = Instantiate(cars[index], carPlatform.transform);
        currentCarProperties = currentCar.GetComponent<CarProperties>();
        
        InitStatBars();
        
        gameManager.SelectedProperties.SelectedCarIndex = index;
    }

    public void DeactivateAll()
    {
        carPlatform.SetActive(false);
        Destroy(currentCar);
        currentCarProperties = null;
    }

    private void InitStatBars()
    {
        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    statBars[i].Init(0, 3000);
                    statBars[i].SetValue(currentCarProperties.MotorForce);
                    break;
                case 1:
                    statBars[i].Init(0, 1200);
                    statBars[i].SetValue(1000);
                    break;
                case 2:
                    statBars[i].Init(0, 5);
                    statBars[i].SetValue(currentCarProperties.DriftStrength);
                    break;    
                default:
                    break;
            }
        }
    }
}
