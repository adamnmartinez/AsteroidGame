using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public List<GameObject> spawners;

    public void Reset()
    {
        for (int i = 0; i < spawners.Count; i++)
        {
            spawners[i].GetComponent<Spawner>().Reset();
        }
    }
}
