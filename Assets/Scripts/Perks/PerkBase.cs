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

    protected CharacterController CharacterController { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        this.CharacterController = this.gameObject.GetComponent<CharacterController>();
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
