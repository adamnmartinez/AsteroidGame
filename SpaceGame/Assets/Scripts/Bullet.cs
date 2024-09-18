using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float duration = 3f;
    
    public Spawner spawner;
    public GameObject playerObject;

    public IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(BulletLife());
    }
}
