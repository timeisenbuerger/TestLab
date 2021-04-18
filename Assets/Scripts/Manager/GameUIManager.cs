using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject singlePlayerModesScreen;
    [SerializeField] private GameObject singlePlayerDifficultiesScreen;
    
    [SerializeField] private Button[] titleScreenOptions;
    [SerializeField] private Button[] singlePlayerModes;
    [SerializeField] private Button[] singlePlayerDifficulties;
    
    private void Start()
    {
        titleScreenOptions = titleScreen.GetComponentsInChildren<Button>();
        singlePlayerModes = singlePlayerModesScreen.GetComponentsInChildren<Button>();
        singlePlayerDifficulties = singlePlayerDifficultiesScreen.GetComponentsInChildren<Button>();

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

    public void QuitGame()
    {
        Application.Quit();
    }
}
