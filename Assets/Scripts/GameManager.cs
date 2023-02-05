using System.Collections;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static NumberFormatInfo CurrencyFormat { get; private set; }

    [SerializeField]
    private Room[] rooms;

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
    private int currentRound = 0;
    public delegate void RoundChangedEventHandler();
    public event RoundChangedEventHandler RoundChanged;
    private bool changingRounds;


    public void IncrementRound()
    {
        ++this.CurrentRound;
        StartCoroutine(EnableAllUnlockedSpawners());
    }
    private IEnumerator EnableAllUnlockedSpawners()
    {
        changingRounds = true;
        yield return new WaitForSeconds(5f);
        foreach (Room room in this.rooms.Where(IsRoomUnlocked))
        {
            this.EnableSpawners(room);
        }
        changingRounds = false;
    }

    public void EnableSpawners(Room room)
    {
        foreach (Spawner spawner in room.Spawners)
        {
            spawner.SetShouldSpawn(true);
        }
    }

    public void ResetRounds()
    {
        this.CurrentRound = 0;
        this.IncrementRound();
    }

    void Awake()
    {
        InitialiseSingleton();
        Initialise();
        ResetRounds();
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
        if (changingRounds == false && this.rooms.Where(IsRoomUnlocked).All(IsRoomComplete))
        {
            this.IncrementRound();
        }
    }

    private bool IsRoomUnlocked(Room room)
    {
        return !room.Doors.Any() || room.Doors.Any(door => door.IsOpen);
    }
    private bool IsRoomComplete(Room room)
    {
        // if they all should NOT spawn, room is complete
        return room.Spawners.All(spawner => !spawner.ShouldSpawn);
    }

    void Initialise()
    {
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