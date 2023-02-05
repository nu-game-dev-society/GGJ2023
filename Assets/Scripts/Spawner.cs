using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [field: SerializeField]
    private Vector3 spawnLocation;
    [SerializeField]
    private GameObject prefabToSpawn;

    private bool shouldSpawn = true;
    private float spawnRateInSeconds = 1f;
    private readonly List<GameObject> spawnedObjs = new();

    IEnumerator Spawn()
    {
        if (this.shouldSpawn)
        {
            GameObject spawnedObj = Instantiate(prefabToSpawn, this.GetSpawnLocationInWorldSpace(), Quaternion.identity);
            this.spawnedObjs.Add(spawnedObj);
        }

        yield return new WaitForSeconds(this.spawnRateInSeconds);
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
