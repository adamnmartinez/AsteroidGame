using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 8f;
    public float cooldownTime = 0.3f;
    private bool onCooldown = false;
    public bool stopped = false;

    public IEnumerator StartCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }

    void Update()
    {
        if (!onCooldown && !gameObject.GetComponent<PlayerMovement>().IsBoosting && !stopped)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, gameObject.transform.localPosition, Quaternion.identity, gameObject.transform);
            bulletObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
            bulletObject.transform.localEulerAngles = new Vector3(0, 0, 90);
            StartCoroutine(StartCooldown());
        }        
    }
}
