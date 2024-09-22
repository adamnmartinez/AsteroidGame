using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 8f;
    public float cooldownTime = 0.3f;
    public float shooterYOffset = 0.2f;

    private bool onCooldown = false;
    public bool stopped = false;

    // Upgrades
    public bool dualUpgrade = false;
    public bool tripleUpgrade = false;
    public bool runGun = false;

    public IEnumerator StartCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }

    public void Fire()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, new Vector3(
            gameObject.transform.localPosition.x,
            gameObject.transform.localPosition.y + shooterYOffset,
            gameObject.transform.localPosition.z
        ), Quaternion.identity);
        bulletObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
        bulletObject.transform.localEulerAngles = new Vector3(0, 0, 90);
        StartCoroutine(StartCooldown());
    }

    public void DualFire()
    {
        GameObject bulletObject1 = Instantiate(bulletPrefab, new Vector3(
            gameObject.transform.localPosition.x - 0.3f,
            gameObject.transform.localPosition.y + shooterYOffset,
            gameObject.transform.localPosition.z
        ), Quaternion.identity);
        bulletObject1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
        bulletObject1.transform.localEulerAngles = new Vector3(0, 0, 90);

        GameObject bulletObject2 = Instantiate(bulletPrefab, new Vector3(
            gameObject.transform.localPosition.x + 0.3f,
            gameObject.transform.localPosition.y + shooterYOffset,
            gameObject.transform.localPosition.z
        ), Quaternion.identity);
        bulletObject2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
        bulletObject2.transform.localEulerAngles = new Vector3(0, 0, 90);

        StartCoroutine(StartCooldown());
    }

    public void TripleFire()
    {
        GameObject bulletObject1 = Instantiate(bulletPrefab, new Vector3(
            gameObject.transform.localPosition.x - 0.5f,
            gameObject.transform.localPosition.y + shooterYOffset,
            gameObject.transform.localPosition.z
        ), Quaternion.identity);
        bulletObject1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
        bulletObject1.transform.localEulerAngles = new Vector3(0, 0, 90);

        GameObject bulletObject2 = Instantiate(bulletPrefab, new Vector3(
            gameObject.transform.localPosition.x + 0.5f,
            gameObject.transform.localPosition.y + shooterYOffset,
            gameObject.transform.localPosition.z
        ), Quaternion.identity);
        bulletObject2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
        bulletObject2.transform.localEulerAngles = new Vector3(0, 0, 90);

        GameObject bulletObject3 = Instantiate(bulletPrefab, new Vector3(
            gameObject.transform.localPosition.x,
            gameObject.transform.localPosition.y + shooterYOffset,
            gameObject.transform.localPosition.z
        ), Quaternion.identity);
        bulletObject3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
        bulletObject3.transform.localEulerAngles = new Vector3(0, 0, 90);

        StartCoroutine(StartCooldown());
    }

    void Update()
    {
        if (!onCooldown && !stopped)
        {
            if(!gameObject.GetComponent<PlayerMovement>().IsBoosting)
            {
                if (tripleUpgrade)
                {
                    TripleFire();
                }
                else if (dualUpgrade)
                {
                    DualFire();
                } 
                else
                {
                    Fire();
                }
            } else {
                if (runGun && tripleUpgrade)
                {
                    // If Run and Gun and Triple Blaster are unlocked, the Run and Gun will use the Dual blaster instead of the standard.
                    DualFire();
                }
                else if (runGun) {
                    Fire();
                }
            }
            
        }
        
    }
}
