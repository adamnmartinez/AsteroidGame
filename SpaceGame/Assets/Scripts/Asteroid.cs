using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Spawner spawner;

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
