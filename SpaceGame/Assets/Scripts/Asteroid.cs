using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float duration = 12f;

    public Spawner spawner;

    private IEnumerator AsteroidLife()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    void Awake()
    {
        StartCoroutine(AsteroidLife());
    }

    void Update()
    {
        // Make asteroids spin!
        gameObject.transform.localEulerAngles += new Vector3(0, 0, 0.3f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            spawner.AddPoints(1);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
