using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mandrake : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float health;

    private NavMeshAgent agent;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If we can move this out into a less frequent func that would be better
        agent.SetDestination(player.transform.position);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
