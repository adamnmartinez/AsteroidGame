using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHitbox : MonoBehaviour
{
    // Controls player health and restarting game scene.
    public PlayerMovement pm;
    public Shooter sh;
    public SpawnerController spawners;
    public Points p;
    public LifeTally tally;
    public PlayerUpgradeController puc;
    public int startHP, health = 3;
   
    public bool isDead = false;

    void Update()
    {
        // Reset Code
        if (Input.GetKey(KeyCode.R) && isDead)
        {
            isDead = false;

            // Reset Difficult and Points
            p.points = 0;
            p.difficultyScore = 0;

            // Reset Spawners
            spawners.Reset();

            // Reset Shooter
            sh.stopped = false;

            // Reset Planet HP
            health = startHP;
            tally.ResetTally();

            // Reset Upgrades
            puc.ResetUpgrades();
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
