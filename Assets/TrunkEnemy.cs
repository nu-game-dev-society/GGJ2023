using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrunkEnemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float Health = 100.0f;
    public bool IsAlive => this.Health > 0;
    public UnityEvent Died = new UnityEvent();
    public void TakeDamage(float damage)
    {
        Health -= damage;
        Screenshaker.Instance.ShakeOnce();
        if(Health <= 0)
        {
            GameManager.Instance.Points += 50;
            Died.Invoke();
        }
    }
    public void ResetEnemy()
    {
        Health = 100.0f;
        gameObject.SetActive(true);
    }
}
