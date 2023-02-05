using UnityEngine;
using UnityEngine.InputSystem;

public class WeedWacker : Weapon
{
    [SerializeField]
    bool firing;
    [SerializeField]
    private Vector3 attackPos, startPos;

    private Camera mainCamera;
    private WeedwackerAnim anim;

    private float nextFireTime;
    private void Start()
    {
        currentFuel = TotalFuel;
        GameManager.Instance.Controls.controls.Gameplay.Fire.performed += Fire;
        GameManager.Instance.Controls.controls.Gameplay.Fire.canceled += Fire;
        nextFireTime = 0;
        anim = GetComponent<WeedwackerAnim>();
        mainCamera = Camera.main;
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
            currentFuel -= Time.deltaTime * 5;
            RaycastHit[] hits = Physics.CapsuleCastAll(mainCamera.transform.position, mainCamera.transform.TransformPoint(mainCamera.transform.forward * Range), 0.1f, mainCamera.transform.forward);
            for (int i =0; i < hits.Length; ++i)
            {
                if (hits[i].transform.TryGetComponent(out IDamageable damageable))
                {
                    if (Time.time >= nextFireTime)
                    {
                        damageable.TakeDamage(Damage);
                        nextFireTime = Time.time + FireRate;
                    }

                    if (i == 0)
                    {
                        anim.Animate(Firing(), hits[i].distance / Range, true);
                    }
                }
            }

            if (hits.Length == 0)
            {
                anim.Animate(Firing(), 1.0f);
            }
        }
        else
        {
            currentFuel -= Time.deltaTime;
            anim.Animate(Firing(), 0.0f);

        }
    }
}
