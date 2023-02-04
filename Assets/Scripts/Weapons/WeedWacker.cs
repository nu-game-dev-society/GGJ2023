using UnityEngine;
using UnityEngine.InputSystem;

public class WeedWacker : Weapon
{
    [SerializeField]
    bool firing;
    [SerializeField]
    private Vector3 attackPos, startPos;
    [SerializeField]
    private Transform weaponModel;

    private float nextFireTime;
    private void Start()
    {
        currentFuel = TotalFuel;
        GameManager.Instance.Controls.controls.Gameplay.Fire.performed += Fire;
        GameManager.Instance.Controls.controls.Gameplay.Fire.canceled += Fire;
        nextFireTime = 0;
    }
    public void Fire(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            Fire();
        }
        if (ctx.phase == InputActionPhase.Canceled)
        {
            EndFire();
        }
    }
    public override void Fire()
    {
        firing = true;
    }
    private void EndFire()
    {
        firing = false;
    }
    public override void Reload()
    {
        currentFuel = TotalFuel;
    }
    private bool Firing()
    {
        return firing;
    }
    private void Update()
    {
        if (Firing())
        {
            weaponModel.transform.localPosition = Vector3.Lerp(weaponModel.transform.localPosition, attackPos, Time.deltaTime * 6.0f);
            currentFuel -= Time.deltaTime * 5;

            if (Time.time >= nextFireTime)
            {
                if (Physics.Raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), out RaycastHit hit, Range)
                    && hit.transform.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(Damage);
                    nextFireTime = Time.time + FireRate;
                }
            }
        }
        else
        {
            weaponModel.transform.localPosition = Vector3.Lerp(weaponModel.transform.localPosition, startPos, Time.deltaTime * 4.0f);
            currentFuel -= Time.deltaTime;
        }
    }
}
