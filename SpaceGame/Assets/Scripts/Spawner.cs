using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Asteroid Spawner Script
    public GameObject spawnObject;
    public GameObject scoreTextMesh;
    public GameObject difficultyTextMesh;

    public PlayerUpgradeController puc;

    // Spawner Stats and Score Tracker Variables
    public float xRange = 5f;
    public float yRange = 5f;
    public float cooldownTime, startingCooldown = 1.2f;
    public int points;
    public int difficultyScore = 0;
    public bool stopped = false;

    private List<GameObject> ActiveAsteroids;
    private bool onCooldown = false;

    void Awake()
    {
        ActiveAsteroids = new List<GameObject>();
    }

    void Update()
    {
        // Update UI with points and difficulty rating.
        scoreTextMesh.GetComponent<TMP_Text>().text = points.ToString();
        difficultyTextMesh.GetComponent<TMP_Text>().text = difficultyScore.ToString();

        // Spawn an asteroid if spawner isn't on cooldown and isn't disabled.
        if (!onCooldown && !stopped)
        {
            Spawn();
            StartCoroutine(SpawnCooldown());
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

    public void AddPoints(int val)
    {
        points += val;
        switch(points)
        {
            // Difficulty increase by player score
            case 10:
            case 25:
            case 50:
            case 100:
            case 150:
            case 200:
            case 250:
            case 500:
            case 750:
            case 1000:
                // Increase Visual Difficulty Score and Increase Asteroid Spawn Rate
                difficultyScore += 1;
                cooldownTime -= 0.1f;
                // Initiate Player Upgrade
                puc.InitiateUpgrade();
                break;
            default:
                break;
        }
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
        asteroidObject.GetComponent<Asteroid>().spawner = gameObject.GetComponent<Spawner>();
        asteroidObject.GetComponent<Rigidbody2D>().rotation = 3f;

        // Set asteroid tradjectory
        Rigidbody2D arb = asteroidObject.GetComponent<Rigidbody2D>();
        if (asteroidObject.transform.localPosition.x > 0)
        {
            arb.velocity = new Vector2(Random.Range(-0.7f, 0f), -1);
        } 
        else 
        {
            arb.velocity = new Vector2(Random.Range(0f, 0.7f), -1);
        }
        
    }
}
