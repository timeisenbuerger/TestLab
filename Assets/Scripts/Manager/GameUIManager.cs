using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CarSelectionController carSelectionController;
    
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject singlePlayerModesScreen;
    [SerializeField] private GameObject singlePlayerDifficultiesScreen;
    [SerializeField] private GameObject carSelectScreen;
    
    [SerializeField] private Button[] titleScreenOptions;
    [SerializeField] private Button[] singlePlayerModes;
    [SerializeField] private Button[] singlePlayerDifficulties;
    [SerializeField] private Button[] carOptions;
    
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    
    private void Start()
    {
        titleScreenOptions = titleScreen.GetComponentsInChildren<Button>();
        singlePlayerModes = singlePlayerModesScreen.GetComponentsInChildren<Button>();
        singlePlayerDifficulties = singlePlayerDifficultiesScreen.GetComponentsInChildren<Button>();
        carOptions = carSelectScreen.GetComponentsInChildren<Button>();

        InitUI();
    }

    private void InitUI()
    {
        titleScreen.SetActive(true);
        titleScreenOptions[0].Select();
    }

    public void GoToSinglePlayerModes()
    {
        titleScreen.SetActive(false);
        singlePlayerModesScreen.SetActive(true);
        
        singlePlayerModes[0].Select();
    }
    
    public void GoToSinglePlayerDifficulties()
    {
        singlePlayerModesScreen.SetActive(false);
        singlePlayerDifficultiesScreen.SetActive(true);
        
        singlePlayerDifficulties[0].Select();
    }
    
    public void GoToCarSelection(int difficulty)
    {
        gameManager.SelectedProperties.Difficulty = difficulty;
        
        singlePlayerDifficultiesScreen.SetActive(false);
        carSelectScreen.SetActive(true);

        carOptions[0].Select();
    }

    public void GoBackToTitleScreen()
    {
        singlePlayerModesScreen.SetActive(false);
        titleScreen.SetActive(true);
        
        titleScreenOptions[0].Select();
    }
    
    public void GoBackToSinglePlayerModes()
    {
        singlePlayerDifficultiesScreen.SetActive(false);
        singlePlayerModesScreen.SetActive(true);

        singlePlayerModes[0].Select();
    }
    
    public void GoBackToSinglePlayerDifficulties()
    {
        carSelectScreen.SetActive(false);
        carSelectionController.DeactivateAll();
        
        singlePlayerDifficultiesScreen.SetActive(true);
        singlePlayerDifficulties[0].Select();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
