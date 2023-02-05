using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [field: SerializeField]
    private Vector3 spawnLocation;
    [SerializeField]
    private GameObject prefabToSpawn;

    public event ShouldSpawnChangedEventHandler ShouldSpawnChanged;
    public delegate void ShouldSpawnChangedEventHandler();
    public bool ShouldSpawn { get; private set; } = true;
    private float spawnRateInSeconds = 1f;
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
        return this.gameObject.transform.position + this.spawnLocation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.GetSpawnLocationInWorldSpace(), 0.1f);
    }
}
