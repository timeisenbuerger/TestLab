using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TachometerController : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private TMPro.TextMeshProUGUI _textMeshPro;

    void Awake()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        double speed = Math.Round(_playerManager.CarController.speedInKmH, 0);
        _textMeshPro.text = $"{speed} km/h";
    }
}
