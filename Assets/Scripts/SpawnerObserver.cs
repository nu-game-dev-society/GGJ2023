using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerObserver : MonoBehaviour
{
    private readonly List<Spawner> spawners = new();

    public void Register(Spawner spawner)
    {
        this.spawners.Add(spawner);
        spawner.ShouldSpawnChanged += Spawner_ShouldSpawnChanged;
    }

    private void Spawner_ShouldSpawnChanged()
    {
        if (this.spawners.All(spawner => !spawner.ShouldSpawn && spawner.SpawnedObjs.All(obj=>obj == null || obj.activeInHierarchy)))
        {
            GameManager.Instance.IncrementRound();
        }
    }
}
