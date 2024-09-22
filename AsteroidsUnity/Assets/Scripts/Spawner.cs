using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Asteroid Spawner Script
    
    public GameObject spawnObject;

    public Points p;
    public PlayerUpgradeController puc;

    // Spawner Stats and Variables

    // Spawn Range
    public float xRange = 5f;
    public float yRange = 5f;

    // Spawned Asteroid Fall Speed
    public float yVelocity = 1f;

    // Spawner will activate after threshold is met.
    public int pointThreshold = 0;

    // Spanwer Speed (spawn cooldown)
    public float cooldownTime, startingCooldown = 1.2f;
    public float minCooldown = 0.2f;

    // The rate at which the spawn rate will increase as difficulty increases
    public float cooldownScale = 0.1f;

    // List of all active asteroids (for deletion purposes)
    private List<GameObject> ActiveAsteroids;

    // Spawner Cooldown Check
    public bool onCooldown = false;

    // Spawner can be disabled with this boolean
    public bool stopped = false;

    // Latest difficulty reading
    private int currentDifficulty = 0;

    void Awake()
    {
        ActiveAsteroids = new List<GameObject>();
    }

    void Update()
    {
        // Check if point threshold met
        if (p.points >= pointThreshold)
        {
            // Set cooldownTime to account for new difficulty value after point threshold met
            if (currentDifficulty != p.difficultyScore){
                if (cooldownTime >= minCooldown)
                {
                    SetSpeed(cooldownTime -= cooldownScale);
                }
                currentDifficulty = p.difficultyScore;
            }
            
            // Spawn an asteroid if spawner isn't on cooldown and isn't disabled after point threshold met
            if (!onCooldown && !stopped)
            {
                Spawn();
                StartCoroutine(SpawnCooldown());
            }
        }
    }

    private IEnumerator SpawnCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }

    public void DestroyActiveAsteroids()
    {
        for (int i = 0; i < ActiveAsteroids.Count; i++)
        {
            if(ActiveAsteroids[i]) Destroy(ActiveAsteroids[i]);
        }
    }

    public void SetSpeed(float newTime)
    {
        cooldownTime = newTime;
    }

    public void Reset()
    {
        SetSpeed(startingCooldown);
        currentDifficulty = 0;
        DestroyActiveAsteroids();
    }

    private void Spawn()
    {
        // Create asteroid
        Vector3 pos = gameObject.transform.localPosition;
        GameObject asteroidObject = Instantiate(spawnObject, new Vector3(
            Random.Range(pos.x - xRange, pos.x + xRange),
            Random.Range(pos.y - yRange, pos.y + yRange),
            0
        ), Quaternion.identity, gameObject.transform);

        // Add Asteroid to active asteroids
        ActiveAsteroids.Add(asteroidObject);

        // Link spawner to asteroid for points counter and acceleration behavior
        asteroidObject.GetComponent<Asteroid>().p = p;
        asteroidObject.GetComponent<Rigidbody2D>().rotation = 3f;

        // Set asteroid tradjectory
        Rigidbody2D arb = asteroidObject.GetComponent<Rigidbody2D>();
        if (asteroidObject.transform.localPosition.x > 0)
        {
            arb.velocity = new Vector2(Random.Range(-0.5f, 0f), -yVelocity);
        } 
        else 
        {
            arb.velocity = new Vector2(Random.Range(0f, 0.5f), -yVelocity);
        }
        
    }
}
