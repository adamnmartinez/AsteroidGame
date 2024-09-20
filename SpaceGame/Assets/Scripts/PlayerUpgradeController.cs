using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgradeController : MonoBehaviour
{
    public Spawner sp;
    public Shooter sh;
    public PlayerMovement pm;

    public bool isUpgrading = false;
    public Button choice1, choice2;
    private int upIdx1, upIdx2;
    private Upgrade up1, up2;
    public List<Upgrade> AvailableUpgrades;
    public delegate void UpgradeCallback(string id);
    public UpgradeCallback upRef;

    public class Upgrade {
        public string Name { get; }
        public string ID { get; }
        public bool Installed { get; set; }
        public UpgradeCallback callback { get; }

        public Upgrade(string name, string id, UpgradeCallback reference)
        {
            Name = name;
            ID = id;
            Installed = false;
            callback = reference;
        }

        public void Install()
        {
            if (!Installed)
            {
                Debug.Log("PUC: Installed " + Name);
                callback(ID);
                Installed = true;
            }
        }
    }
    
    public void installCallback(string id)
    {
        switch(id)
        {
            case "fireRate":
                sh.cooldownTime -= 0.3f;
                break;
            case "fireRate2":
                sh.cooldownTime -= 0.4f;
                break;
            case "dualBlaster":
                sh.dualUpgrade = true;
                break;
            case "speedBoost":
                pm.moveSpeed += 3f;
                pm.boostSpeed += 3f;
                break;
            case "runGun":
                sh.runGun = true;
                sh.cooldownTime -= 0.2f;
                break;
            case "tripleBlaster":
                sh.tripleUpgrade = true;
                break;
            case "null":
                break;
            default:
                break;
        }
    }

    // On Awake, initalize standard upgrades.
    void Awake()
    {
        upRef = installCallback;
        AvailableUpgrades = new List<Upgrade> {
            new Upgrade("Lightning Reload", "fireRate1", upRef),
            new Upgrade("Twin Cannons", "dualBlaster", upRef),
            new Upgrade("Quantum Fuel", "speedBoost", upRef),
            new Upgrade("Run and Gun Protocol", "runGun", upRef),
        };
    }

    // UnlockUpgrades will unlock some new, advanced abilites once other upgrade prerequisites are met.
    public void UnlockUpgrades(string id)
    {
        switch(id){
            case "fireRate1":
                AvailableUpgrades.Add(new Upgrade("Atomic Reload", "fireRate2", upRef));
                break;
            case "dualBlaster":
                AvailableUpgrades.Add(new Upgrade("Tri-Blaster", "tripleBlaster", upRef));
                break;
            default:
                break;
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }

    public void InitiateUpgrade()
    {
        // Pause Game
        Pause();
        isUpgrading = true;

        if (AvailableUpgrades.Count == 0) 
        {
            // Do nothing.
            CloseUpgrade();
        } 
        else if (AvailableUpgrades.Count == 1) 
        {
            // Get Last Upgrade.
            upIdx1 = 0;
            up1 = AvailableUpgrades[upIdx1];
            
            choice1.GetComponentInChildren<TMP_Text>().text = up1.Name;
            choice1.onClick.AddListener(up1.Install);
            choice1.onClick.AddListener(CloseUpgrade);

            AvailableUpgrades.Remove(up1);

            // Make second button null.
            choice2.gameObject.SetActive(false);
        } 
        else 
        {
            // Create a button linked to the upgrade instance, then remove it from the list of available upgrades.

            // Get Random Upgrade 1
            upIdx1 = Random.Range(0, AvailableUpgrades.Count);
            up1 = AvailableUpgrades[upIdx1];
            
            choice1.GetComponentInChildren<TMP_Text>().text = up1.Name;
            choice1.onClick.AddListener(up1.Install);
            choice1.onClick.AddListener(CloseUpgrade);

            AvailableUpgrades.Remove(up1);

            // Get Random Upgrade 2
            upIdx2 = Random.Range(0, AvailableUpgrades.Count);
            up2 = AvailableUpgrades[upIdx2];

            choice2.GetComponentInChildren<TMP_Text>().text = up2.Name;
            choice2.onClick.AddListener(up2.Install);
            choice2.onClick.AddListener(CloseUpgrade);

            AvailableUpgrades.Remove(up2);
        }
    }

    public void CloseUpgrade()
    {
        // Remove installed upgrades from list.
        if (!up1.Installed) {AvailableUpgrades.Add(up1);} else {UnlockUpgrades(up1.ID);}
        if (!up2.Installed) {AvailableUpgrades.Add(up2);} else {UnlockUpgrades(up2.ID);}

        Debug.Log("Remaining Upgrades: " + AvailableUpgrades.Count.ToString());
        
        // Remove old listeners
        choice1.onClick.RemoveAllListeners();
        choice2.onClick.RemoveAllListeners();

        choice2.gameObject.SetActive(true);

        // Resume Game
        isUpgrading = false;
        UnPause();
    }
}
