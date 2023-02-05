using System.Globalization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static NumberFormatInfo CurrencyFormat { get; private set; }

    [field: SerializeField]
    public ControlsManager Controls { get; set; }
    [field: SerializeField]
    public PlayerController PlayerController { get; private set; }

    public int Points { set; get; }

    public int CurrentRound 
    {
        get => this.currentRound;
        private set
        {
            this.currentRound = value;
            this.RoundChanged?.Invoke();
        }
    }
    private int currentRound = 1;
    public delegate void RoundChangedEventHandler();
    public event RoundChangedEventHandler RoundChanged;


    public void IncrementRound()
    {
        ++this.CurrentRound;
    }

    public void ResetRounds()
    {
        this.CurrentRound = 1;
    }

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
        // TEMP - remove when we have spawning in place
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            this.IncrementRound();
        }
    }

    void Initialise()
    {
        Points = 1000;

        // Currency format
        CurrencyFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
        CurrencyFormat.CurrencySymbol = "";
        CurrencyFormat.CurrencyDecimalDigits = 0;

        // incase someone forgets to set it
        this.PlayerController ??= FindObjectOfType<PlayerController>();
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}