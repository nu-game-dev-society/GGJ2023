using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    public float moveSpeed = 10f;
    public float mouseSensitivity = 100f;
    public float speedMultiplier = 1f;
    public float sprintMultiplier = JOG_SPEED_MULTIPLIER;
    public const float JOG_SPEED_MULTIPLIER = 1.1f;
    float xRotation = 0f;
    public float healthRegen = 10;

    CharacterController controller;
    ControlsManager controls;

    public Transform camera;

    [SerializeField]
    private Vector2 move;
    [SerializeField]
    private Vector2 look;
    [SerializeField]
    private float currentHealth = 100;
    public float maxHealth = 100;

    private readonly HashSet<IPerk> perks = new(new PerkTypeEqualityComparer());
    public struct PerksChangedEventArgs
    {
        public IPerk NewPerk { get; }
        public PerksChangedEventArgs(IPerk newPerk)
        {
            this.NewPerk = newPerk;
        }
    }
    public delegate void PerksChangedEventHandler(PerksChangedEventArgs args);
    public PerksChangedEventHandler PerksChanged;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        controls = GameManager.Instance.Controls;

        Cursor.lockState = CursorLockMode.Locked;
        xRotation = camera.localRotation.x;

        GameManager.Instance.Controls.controls.Gameplay.Sprint.performed += Sprint_performed;
        GameManager.Instance.Controls.controls.Gameplay.Sprint.canceled += Sprint_canceled;

        ResetHealth();
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void ResetHealth()
    {
        currentHealth = 100;
        maxHealth = 100;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
        if (currentHealth <= 0)
        {
            GameManager.Instance.QuitGame();
        }
    }

    private void Sprint_performed(InputAction.CallbackContext obj)
    {
        this.speedMultiplier = sprintMultiplier;
    }    
    private void Sprint_canceled(InputAction.CallbackContext obj)
    {
        this.speedMultiplier = 1f;
    }

    void GetInputs()
    {
        move = controls.GetMovement();
        look = controls.GetLook();

    }

    void Update()
    {
        GetInputs();

        float mouseX = look.x * mouseSensitivity;
        float mouseY = look.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

        Vector3 moveDirection = (transform.right * move.x) + (transform.forward * move.y) + Physics.gravity;
        controller.Move(moveDirection * moveSpeed * speedMultiplier * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(25);
        }

        if (currentHealth < maxHealth)
        {
            currentHealth += healthRegen * Time.deltaTime;
        }
    }

    public void AddPerk(IPerk perk)
    {
        this.perks.Add(perk);
        this.PerksChanged?.Invoke(new PerksChangedEventArgs(perk));
    }
}
