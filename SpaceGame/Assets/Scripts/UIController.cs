using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject gameOverText;
    public PlanetHitbox planet;

    private bool gameOverDisplayed = false;

    // Update is called once per frame
    void Update()
    {
        if (planet.health <= 0 && !gameOverDisplayed) 
        {
            Instantiate(gameOverText, gameObject.transform.localPosition, Quaternion.identity, gameObject.transform);
            gameOverDisplayed = true;
        }
    }
}
