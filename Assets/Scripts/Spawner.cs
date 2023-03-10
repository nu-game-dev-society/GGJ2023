using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private TrunkEnemy[] trunks;
    [field: SerializeField]
    private Vector3 spawnLocation;
    [SerializeField]
    private GameObject prefabToSpawn;

    public event ShouldSpawnChangedEventHandler ShouldSpawnChanged;
    public delegate void ShouldSpawnChangedEventHandler();
    [field: SerializeField]
    public bool ShouldSpawn { get; private set; } = false;
    private float spawnRateInSeconds = 10f;
    private readonly List<GameObject> spawnedObjs = new();
    public ReadOnlyCollection<GameObject> SpawnedObjs => new(this.spawnedObjs);

    IEnumerator Spawn()
    {
        while (true)
        {
            if (this.ShouldSpawn)
            {
                GameObject spawnedObj = Instantiate(prefabToSpawn, this.GetSpawnLocationInWorldSpace(), Quaternion.identity);
                this.spawnedObjs.Add(spawnedObj);
            }

            yield return new WaitForSeconds(this.spawnRateInSeconds);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(this.Spawn());
    }

    private Vector3 GetSpawnLocationInWorldSpace()
    {
        return transform.TransformPoint(this.spawnLocation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.GetSpawnLocationInWorldSpace(), 0.1f);
    } 
    public void CheckShouldSpawn()
    {
        bool newShouldSpawn = trunks.Any(trunk => trunk.IsAlive);
        SetShouldSpawn(newShouldSpawn);
    }

    public void SetShouldSpawn(bool newShouldSpawn)
    {
        if (newShouldSpawn == this.ShouldSpawn)
        {
            return;
        }
        ShouldSpawn = newShouldSpawn;
        ShouldSpawnChanged?.Invoke();
        if (this.ShouldSpawn)
        {
            foreach (var trunk in trunks)
            {
                trunk.ResetEnemy();
            }
        }
    }
}
