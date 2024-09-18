using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float boostSpeed = 9f;
    Rigidbody2D rb;

    private bool _isBoosting = false;
    public bool IsBoosting
    {
        get
        {
            return _isBoosting;
        }
        set
        {
            _isBoosting = value;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        IsBoosting = Input.GetKey(KeyCode.LeftShift);
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * (IsBoosting ? boostSpeed : moveSpeed), 0);
    }
}
