using System.Globalization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static NumberFormatInfo CurrencyFormat { get; private set; }

    [field: SerializeField]
    public ControlsManager Controls { get; set; }
    [SerializeField]
    public PlayerController PlayerController { get; private set; }

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