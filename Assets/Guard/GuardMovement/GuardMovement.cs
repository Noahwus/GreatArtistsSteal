﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour {

    //look distance of guard to determine if chasing or not
    /*public float lookDistance = 30f;
    public LayerMask hitting;//determines what to hit with the raycast
    public Transform originPoint;
    private Vector2 direction = new Vector2(-1, 0);*/


    //determine sprite direction
    bool faceRight = true;

    //left and right wall check
    bool wallCheckL = false;
    bool wallCheckR = false;
    public LayerMask whatIsGuardWall;//determines what is considered a wall for the guard
    float wallRadius = .2f;
    public Transform wallNearL;
    public Transform wallNearR;
    bool movingRight = true;

    bool Patrolling = true;
    bool playerCaught = false;

    //animator
    Animator anim;

    //guard move speed
    public float walkSpeed = 3f;

    private void Awake()
    {
        
    }

    public void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool( "Patrolling",Patrolling);
    }

    private void FixedUpdate()
    {
        wallCheckL = Physics2D.OverlapCircle(wallNearR.position, wallRadius, whatIsGuardWall);
        wallCheckR = Physics2D.OverlapCircle(wallNearL.position, wallRadius, whatIsGuardWall);

        if(Patrolling == true)
        {
            switch (movingRight)
            {
                case true:
                    moveRight();
                    if (wallCheckR)
                    {
                        Flip();
                        movingRight = false;
                        faceRight = false;
                    }
                    break;

                case false:
                    moveLeft();
                    if (wallCheckL)
                    {
                        Flip();
                        movingRight = true;
                        faceRight = true;
                    }
                    break;
            }
        }
        
    }

    private void Update()
    {
        /*Debug.DrawRay(originPoint.position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, direction, lookDistance);

        if(hit == true)
        {
            if (hit.collider.tag == ("Player"))
            {
                Debug.Log("Player Hit");
            }
            if (hit.collider.tag == ("Wall"))
            {
                direction *= -1;
            }
        }

        anim.SetBool("Caught", playerCaught);*/
    }

    //movement of the guard position
    public void moveLeft()
    {
        transform.position += transform.right * walkSpeed * Time.deltaTime;
        
        Patrolling = true;
    }
    public void moveRight()
    {
        transform.position += -transform.right * walkSpeed * Time.deltaTime;
        Patrolling = true;
    }

    //flips the direction of the sprite depending on which way the player is facing/moving
    void Flip()
    {
        SpriteRenderer guardSprite = gameObject.GetComponent<SpriteRenderer>();
        
        if (guardSprite.flipX == true)
        {
            guardSprite.flipX = false;
        }
        else
        {
            guardSprite.flipX = true;
        }

    }

}
