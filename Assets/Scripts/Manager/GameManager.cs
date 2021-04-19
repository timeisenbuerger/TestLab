using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SelectedProperties selectedProperties;
    
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start()
    {
        selectedProperties = gameObject.AddComponent<SelectedProperties>();
    }

    void Update()
    {
        
    }

    public SelectedProperties SelectedProperties => selectedProperties;
}
