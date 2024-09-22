using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float duration = 12f;
    public int health = 1;

    public Points p;

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

    void Hurt(int val)
    {
        health -= val;
        if(health <= 0)
        {
            // If damage is lethal, destroy object and gain a point.
            p.AddPoints(1);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            // Get projectile bullet component
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            // Take damage
            Hurt(bullet.damage);
        }
    }
}
