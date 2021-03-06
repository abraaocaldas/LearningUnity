﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    public float moveSpeed;
    private float moveVelocity;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    private bool doubleJumped = false;

    private Animator animator;

    public Transform firePoint;
    public GameObject ninjaStar;

    public float shotDelay;
    private float shotDelayCounter;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().freezeRotation = true;
        animator = GetComponent<Animator>();
	}
	
    //Happens in 15 seconds interval
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

	// Update is called once per frame
	void Update () {

        animator.SetBool("Grounded", grounded);

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            Jump();
            doubleJumped = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !grounded && doubleJumped)
        {
            Jump();            
            doubleJumped = false;
        }

        moveVelocity = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            moveVelocity = moveSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            moveVelocity = -moveSpeed;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
            shotDelayCounter = shotDelay;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            shotDelayCounter -= Time.deltaTime;

            if(shotDelayCounter <= 0)
            {
                Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
                shotDelayCounter = shotDelay;
            }
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

        if(GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f,1f);
        }

        
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }
}
