using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject gameOverText;
    public GameObject upgradeUI;

    public PlayerUpgradeController puc;
    public PlanetHitbox planet;

    void Update()
    {
        gameOverText.SetActive(planet.isDead);
        upgradeUI.SetActive(puc.isUpgrading);
    }


}
