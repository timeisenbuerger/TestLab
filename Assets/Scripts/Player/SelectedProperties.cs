using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedProperties : MonoBehaviour
{
    [SerializeField] private int difficulty;
    [SerializeField] private int selectedCarIndex;
    [SerializeField] private int selectedCharacterIndex;
    [SerializeField] private int selectedCupIndex;
    [SerializeField] private int selectedTrackIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Difficulty
    {
        get => difficulty;
        set => difficulty = value;
    }

    public int SelectedCarIndex
    {
        get => selectedCarIndex;
        set => selectedCarIndex = value;
    }

    public int SelectedCharacterIndex
    {
        get => selectedCharacterIndex;
        set => selectedCharacterIndex = value;
    }

    public int SelectedCupIndex
    {
        get => selectedCupIndex;
        set => selectedCupIndex = value;
    }

    public int SelectedTrackIndex
    {
        get => selectedTrackIndex;
        set => selectedTrackIndex = value;
    }
}
