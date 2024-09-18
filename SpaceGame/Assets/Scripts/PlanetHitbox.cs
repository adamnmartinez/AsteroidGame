using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHitbox : MonoBehaviour
{
    // Controls player health and restarting game scene.
    public PlayerMovement pm;
    public Shooter sh;
    public Spawner sp;
    public LifeTally tally;
    public int startHP, health = 3;
   
    public bool isDead = false;

    void Update()
    {
        // Reset Code
        if (Input.GetKey(KeyCode.R) && isDead)
        {
            isDead = false;

            // Reset Spawner and Points
            sp.points = 0;
            sp.difficultyScore = 0;
            sp.DestroyActiveAsteroids();
            sp.SetSpeed(sp.startingCooldown);

            // Reset Shooter
            sh.stopped = false;

            // Reset Planet HP
            health = startHP;
            tally.ResetTally();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        health -= 1;
        tally.SubtractToken();
        if (health <= 0)
        {
            sh.stopped = true;
            isDead = true;
        }
    }
}
