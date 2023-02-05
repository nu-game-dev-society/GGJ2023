using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    [field: SerializeField]
    public UnlockableDoor[] Doors { get; private set; }

    [field: SerializeField]
    public Spawner[] Spawners { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
