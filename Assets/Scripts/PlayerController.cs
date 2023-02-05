using System.Collections.Generic;
using System.Linq;
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

    private Camera playerCamera;

    [SerializeField]
    private AudioSource asFootsteps;
    [SerializeField]
    private AudioSource asPain;
    [SerializeField] private float stepSpeed = 0.3f;
    private float nextStepTime = 0f;
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
        playerCamera = Camera.main;

        controller = GetComponent<CharacterController>();
        controls = GameManager.Instance.Controls;

        Cursor.lockState = CursorLockMode.Locked;
        xRotation = playerCamera.transform.localRotation.x;

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
        asPain.Play();
        if (currentHealth <= 0)
        {
            if (this.perks.OfType<GreenPerk>().Any())
            {
                this.perks.Clear();
                this.PerksChanged?.Invoke(new PerksChangedEventArgs(null));
                this.currentHealth = this.maxHealth;
                return;
            }
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

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
        
        Vector3 moveDirection = (transform.right * move.x) + (transform.forward * move.y) + Physics.gravity;

        controller.Move(moveDirection * moveSpeed * speedMultiplier * Time.deltaTime);

        if (currentHealth < maxHealth)
        {
            currentHealth += healthRegen * Time.deltaTime;
        }
        
        bool moving = controller.velocity.magnitude > 0.5f;
        if (moving && Time.time >= nextStepTime)
        {
            asFootsteps.pitch = Random.Range(2f, 2.5f);
            asFootsteps.Play();
            nextStepTime= Time.time + (stepSpeed / speedMultiplier);
        }

    }

    public void AddPerk(IPerk perk)
    {
        this.perks.Add(perk);
        this.PerksChanged?.Invoke(new PerksChangedEventArgs(perk));
    }
}
