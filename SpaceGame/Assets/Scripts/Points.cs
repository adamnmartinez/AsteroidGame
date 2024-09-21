using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int points;
    public int difficultyScore = 0;

    public GameObject scoreTextMesh;
    public GameObject difficultyTextMesh;

    public PlayerUpgradeController puc;

    // Keeps track of difficulty rating 
    public int difficultyThreshold = 25;
    // private int difficultyThresholdOffset = 25;

    public void AddPoints(int val)
    {
        points += val;
        CheckForDifficulty(points);
    }

    private void CheckForDifficulty(int val)
    {
        if (val >= difficultyThreshold)
        {
            // Increase Visual Difficulty Score and Increase Asteroid Spawn Rate
            difficultyScore += 1;

            // Initiate Player Upgrade
            puc.InitiateUpgrade();

            // Once met, Create new difficulty threshold, make next threshold further.
            difficultyThreshold = difficultyThreshold * 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update UI with points and difficulty rating.
        scoreTextMesh.GetComponent<TMP_Text>().text = points.ToString();
        difficultyTextMesh.GetComponent<TMP_Text>().text = difficultyScore.ToString();
    }
}
