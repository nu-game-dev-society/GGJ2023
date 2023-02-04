using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrunkEnemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float Health = 1000.0f;

    public UnityEvent Died = new UnityEvent();
    public void TakeDamage(float damage)
    {
        Health -= damage;
        Screenshaker.Instance.ShakeOnce();
        if(Health <= 0)
        {
            Died.Invoke();
        }
    }
}
