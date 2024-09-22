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

    private List<Upgrade> AvailableUpgrades;
    private List<Upgrade> InstalledUpgrades;

    public delegate void UpgradeCallback(string id, bool val);
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
                callback(ID, true);
                Installed = true;
            }
        }

        public void Uninstall()
        {
            if (Installed)
            {
                callback(ID, false);
                Installed = false;
            }
        }
    }

    // On Awake, initalize standard upgrades.
    void Awake()
    {
        upRef = installCallback;
        InstalledUpgrades = new List<Upgrade>();
        AvailableUpgrades = new List<Upgrade> {
            new Upgrade("Lightning Reload", "fireRate1", upRef),
            new Upgrade("Twin Cannons", "dualBlaster", upRef),
            new Upgrade("Quantum Fuel", "speedBoost", upRef),
            new Upgrade("Run and Gun Protocol", "runGun", upRef),
        };
    }
    
    public void installCallback(string id, bool installing)
    {
        // Matches Upgrade ID to effect. Can be set to install or uninstall provided upgrade.

        // Arbitrary variables to be assigned and used when changing behaviors.
        // Variables must be set before they are used in upgrade case.
        float factor;
        float factor2;

        switch(id)
        {
            case "fireRate1":
                factor = 0.3f;
                sh.cooldownTime -= installing ? factor : -factor;
                break;

            case "fireRate2":
                factor = 0.2f;
                sh.cooldownTime -= installing ? factor : -factor;
                break;

            case "dualBlaster":
                sh.dualUpgrade = installing;
                break;

            case "speedBoost":
                factor = 3f;
                factor2 = 3f;
                pm.moveSpeed += installing ? factor : -factor;
                pm.boostSpeed += installing ? factor2 : -factor2;
                break;

            case "runGun":
                factor = 0.2f;
                sh.runGun = installing;
                break;

            case "tripleBlaster":
                sh.tripleUpgrade = installing;
                break;

            case "null":
                break;

            default:
                break;
        }
    }

    public void ResetUpgrades()
    {
        for (int i = 0; i < InstalledUpgrades.Count; i++)
        {
            // Lock upgrade paths
            UnlockUpgrades(InstalledUpgrades[i].ID);
            // Uninstall upgrade
            InstalledUpgrades[i].Uninstall();
        }

        InstalledUpgrades.Clear();
        // Reset Available Upgrade List
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

            // Deactivate second button.
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
        // Remove installed upgrades from list, add installed upgrades to installed list, and unlock any advanced upgrades.
        if (!up1.Installed) {AvailableUpgrades.Add(up1);} else {UnlockUpgrades(up1.ID); InstalledUpgrades.Add(up1);}
        if (!up2.Installed) {AvailableUpgrades.Add(up2);} else {UnlockUpgrades(up2.ID); InstalledUpgrades.Add(up2);}

        // Remove old listeners
        choice1.onClick.RemoveAllListeners();
        choice2.onClick.RemoveAllListeners();

        // Reactivate second button if disabled.
        choice2.gameObject.SetActive(true);

        // Resume Game
        isUpgrading = false;
        UnPause();
    }
}
