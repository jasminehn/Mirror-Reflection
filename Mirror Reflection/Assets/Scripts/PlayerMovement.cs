﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject doorb;
    public GameObject doort;
    Door doorbScript;
    Door doortScript;
    public float speed;             //this is the speed our player will move
    private Rigidbody2D rb;         //variable to store Rigidbody2D component
    private float moveInput;        //this store the input value

    public float jumpForce;         //force with which player jump
    private bool isGrounded;        //this variable will tell if our player is grounded or not
    public Transform feetPos;       //this variable will store reference to transform from where we will create a circle
    public float circleRadius;      //radius of circle
    public LayerMask whatIsGround;  //layer our ground will have.

    public float jumpTime;          //time till which we will apply jump force
    private float jumpTimeCounter;  //time to count how long player has pressed jump key
    private bool isJumping;         //bool to tell if player is jumping or not

    public int shards = 0;          //initiate the collectible shards
    public List<GameObject> currentCollisions = new List<GameObject>();

    public bool waterExists;
    public bool waterI = false;
    Collider2D water;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //get reference to 	Rigidbody2D component
        doorbScript = doorb.GetComponent<Door>();
        doortScript = doort.GetComponent<Door>();
        if (waterExists)
        {
            water = GameObject.FindGameObjectWithTag("Water").GetComponent<Collider2D>();
            Debug.Log("Water exists");
        }
    }

    //as we are going to use physics for our player , we must use FixedUpdate for it
    void FixedUpdate()
    {
        //the moveInput will be 1 when we press right key and -1 for left key
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput > 0)                                  //moving towards right side
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (moveInput < 0)                             //moving towards left side
            transform.eulerAngles = new Vector3(0, 180, 0);
        //here we set our player x velocity and y will not ne changed
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        //here we set the isGrounded
        isGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);
        //we check if isGrounded is true and we pressed Space button
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;                           //we set isJumping to true
            jumpTimeCounter = jumpTime;                 //set jumpTimeCounter
            rb.velocity = Vector2.up * jumpForce;       //add velocity to player
        }

        //if Space key is pressed and isJumping is true
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)                    //we check if jumpTimeCounter is more than 0
            {
                rb.velocity = Vector2.up * jumpForce;   //add velocity
                jumpTimeCounter -= Time.deltaTime;      //reduce jumpTimeCounter by Time.deltaTime
            }
            else                                        //if jumpTimeCounter is less than 0
            {
                isJumping = false;                      //set isJumping to false
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))              //if we unpress the Space key
        {
            isJumping = false;                          //set isJumping to false
        }

        if (isGrounded == true && gameObject.layer != 10)
            gameObject.layer = 10;

        if (isGrounded == true && Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.layer = 9;
        }

    }

    //Use this for collectibles and powerups
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "DoorB")
        {
            doorbScript.isReached = true;
        }
        else if (col.tag == "DoorT")
        {
            doortScript.isReached = true;
        }
        else
        {
            if (col.tag == "Collectible")
            {
                Destroy(col.gameObject);
                shards += 1;
                Debug.Log("collected a glass shard! Shards = " + shards);
            }
            else if (col.tag == "WaterItem")
            {
                col.gameObject.SetActive(false);
                waterI = true;
                Debug.Log("water item collected");
            }
            else if (col.tag == "JumpPU")
            {
                Destroy(col.gameObject);
                jumpForce = 15;
                Debug.Log("collected a powerup! Ability: Jump");
            }
        }
        if(doorbScript.isReached && doortScript.isReached)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        currentCollisions.Add(col.gameObject);

        foreach (GameObject gObject in currentCollisions)
        {
            print(gObject.name);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        doorbScript.isReached = false;
        doortScript.isReached = false;

        // Remove the GameObject collided with from the list.
        currentCollisions.Remove(col.gameObject);

        // Print the entire list to the console.
        foreach (GameObject gObject in currentCollisions)
        {
            print(gObject.name);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Moving"))
            this.transform.parent = col.transform;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Moving"))
            this.transform.parent = null;
    }

}
