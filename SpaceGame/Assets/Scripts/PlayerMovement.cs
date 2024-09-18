using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float boostSpeed = 9f;
    Rigidbody2D rb;
    Animator anim;


    [SerializeField] private bool _isFacingRight = false;
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        set
        {
            _isFacingRight = value;
            gameObject.transform.localEulerAngles = new Vector3(0, value ? 0 : 180, 0);
        }
    }

    private bool _isBoosting = false;
    public bool IsBoosting
    {
        get
        {
            return _isBoosting;
        }
        set
        {
            if (value != _isBoosting)
            {
                _isBoosting = value;
                anim.SetBool("isBoosting", value);
            }
            
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        IsBoosting = Input.GetKey(KeyCode.LeftShift);
        IsFacingRight = Input.GetAxisRaw("Horizontal") > 0;
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * (IsBoosting ? boostSpeed : moveSpeed), 0);
    }
}
