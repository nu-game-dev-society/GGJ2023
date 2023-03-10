using UnityEngine;

public abstract class PerkBase : MonoBehaviour, IPerk
{
    public bool IsActive 
    {
        get => this.isActive;
        set
        {
            bool oldValue = this.isActive;
            if (oldValue == value)
            {
                return;
            }
            this.isActive = value;
            this.OnIsActiveChanged(oldValue, this.isActive);
        }
    }
    private bool isActive;
    protected abstract void OnIsActiveChanged(bool oldValue, bool newValue);

    protected PlayerController PlayerController { get; private set; }

    public Color Color { get; set; }

    void Awake()
    {
        this.PlayerController = this.gameObject.GetComponent<PlayerController>();
    }

    void Start()
    {
        this.PlayerController.AddPerk(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isActive)
        {
            return;
        }

        this.InnerUpdate();
    }

    protected abstract void InnerUpdate();
}
