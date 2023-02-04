using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField]
    public ControlsManager Controls { get; set; }
    public int Points { set; get; }

    void Awake()
    {
        InitialiseSingleton();
        Initialise();
    }

    void InitialiseSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        Debug.Log("InitialiseInstance");
    }

    private void Update()
    {
        
    }

    void Initialise()
    {
        
    }
}