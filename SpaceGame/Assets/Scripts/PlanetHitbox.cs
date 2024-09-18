using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHitbox : MonoBehaviour
{
    public PlayerMovement pm;
    public Shooter sh;
    public Spawner sp;
    public LifeTally tally;
    public int health = 0;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        health -= 1;
        tally.SubtractToken();
        if (health <= 0)
        {
            pm.moveSpeed = 0;
            sh.stopped = true;
        }
    }
}
