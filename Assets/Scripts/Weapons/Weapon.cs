using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField]
    public float Damage { get; set; }  
    [field: SerializeField]
    public float FireRate { get; set; }
    [field: SerializeField]
    public float Range { get; set; }
    [field: SerializeField]
    public float ReloadTime { get; set; }
    [field: SerializeField]
    public float TotalFuel { get; set; }

    [Header("Debug")]

    [field: SerializeField]
    protected float currentFuel;
    public abstract void Fire();

    public abstract void Reload();
}
