using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDamageSprite : MonoBehaviour
{
    public Asteroid aster;

    public int damagedThresh = 0;
    public int decimatedThresh = 0;

    public Sprite damaged;
    public Sprite decimated;

    public SpriteRenderer sp;

    // Start is called before the first frame update
    void Awake()
    {
        aster = GetComponent<Asteroid>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aster.health <= damagedThresh)
        {
            sp.sprite = damaged;
        } 
        
        if (aster.health <= decimatedThresh)
        {
            sp.sprite = decimated;
        }
    }
}
