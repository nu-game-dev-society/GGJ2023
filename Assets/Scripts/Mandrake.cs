using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mandrake : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float attackRate;
    [SerializeField]
    private float attackAmount;
    [SerializeField]
    private float attackDistance;

    private float nextAttackTime;
    private NavMeshAgent agent;

    [SerializeField]
    private ParticleSystem damageParticles;
    [SerializeField]
    private AudioClip deathClip;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        nextAttackTime = 0;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackRate;
            if (Vector3.Distance(transform.position, GameManager.Instance.PlayerController.transform.position) <= attackDistance)
            {
                GameManager.Instance.PlayerController.TakeDamage(attackAmount);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If we can move this out into a less frequent func that would be better
        agent.SetDestination(GameManager.Instance.PlayerController.transform.position);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        damageParticles.Play();
        if (health <= 0)
        {
            AudioPool.Instance?.PlayClip(deathClip, transform, volume: 1.5f);
            Destroy(gameObject);
        }
    }
}
