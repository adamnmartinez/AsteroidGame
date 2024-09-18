using System.Collections;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject scoreTextMesh;
    public GameObject difficultyTextMesh;

    public float xRange = 5f;
    public float yRange = 5f;
    public float cooldownTime = 1.5f;
    public int points;
    public int difficultyScore = 0;


    private bool onCooldown = false;

    private IEnumerator SpawnCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
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
            case 200:
            case 300:
            case 400:
            case 500:
            case 1000:
            case 1500:
                difficultyScore += 1;
                cooldownTime -= 0.1f;
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

        // Link spawner to asteroid for points counter and acceleration behavior
        asteroidObject.GetComponent<Asteroid>().spawner = gameObject.GetComponent<Spawner>();
        asteroidObject.GetComponent<Rigidbody2D>().rotation = 3f;

        // Set asteroid tradjectory
        Rigidbody2D arb = asteroidObject.GetComponent<Rigidbody2D>();
        arb.velocity = new Vector2(Random.Range(-0.7f, 0.7f), -1);
    }

    void Update()
    {
        // Update UI with points and difficulty rating.
        scoreTextMesh.GetComponent<TMP_Text>().text = points.ToString();
        difficultyTextMesh.GetComponent<TMP_Text>().text = difficultyScore.ToString();

        if (!onCooldown)
        {
            Spawn();
            StartCoroutine(SpawnCooldown());
        }
    }
}
