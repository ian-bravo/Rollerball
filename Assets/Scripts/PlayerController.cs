using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
     // Speed at which the player moves.
    public float speed = 0;
    public TextMeshProUGUI countText;
     // Rigidbody of the player.
    private Rigidbody rb;
    private int count;
     // Movement along X and Y axes.
    private float movementX;
    private float movementY;
    public GameObject winTextObject;
    public float jumpAmount = 80;
    public float gravityScale = 10;
    // public float fallingGravityScale = 40;

    //boosting:
    private float boostTimer;
    private bool boosting;

    private float slowTimer;
    private bool slowDown;


    // Start is called before the first frame update
    void Start()
    {
        count = 0;
         // Get and store the Rigidbody component attached to the player.
        rb = GetComponent <Rigidbody>();
         // Update the count display.
        SetCountText();
         // Initially set the win text to be inactive.
        winTextObject.SetActive(false);
        
        //boosting
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }

        if(slowDown)
        {
            slowTimer += Time.deltaTime;
            if(slowTimer >= 2)
            {
                speed = 11;
                slowTimer = 0;
                slowDown = false;
                Debug.Log("back to normal speed from slow");
            }
        }


        //boosting
        if(boosting)
        {
            boostTimer += Time.deltaTime;
            if(boostTimer >= 0.5)
            {
                speed = 12;
                boostTimer = 0;
                boosting = false;
                Debug.Log("back to normal speed from boost");
            }
        }
        
        // if(rb.velocity.y >= 0)
        // {
        //     rb.gravityScale = gravityScale;
        // }
        // else if (rb.velocity.y < 0)
        // {
        //     rb.gravityScale = fallingGravityScale;
        // }
        
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
         // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
         // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
        
    }

    void OnTriggerEnter (Collider other)
    {   
         // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("SlowField"))
        {
            Debug.Log("hit slow field");
            slowDown = true;

            speed = -10;
            Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
            rb.AddForce(movement * speed * 0.1f);

            
            // other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            Debug.Log("slow field destroyed");

        }
         
        if (other.gameObject.CompareTag("PickUp"))
        {
            //boosting
            Debug.Log("boost on collision");
            boosting = true;
            speed = 10000;

            Destroy(other.gameObject);

             // Deactivate the collided object (making it disappear).
            // other.gameObject.SetActive(false);~`~~~~~~~~~~~~~~~~~~
             // Increment the count of "PickUp" objects collected.
            count = count + 1;
             // Update the count display.
            SetCountText();

            
            // float speed = 100f;
            // // makes ball jump when touching pickup
            // Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
            // // rb.AddForce(movement * 10000);
            // rb.AddForce(movement * speed);
            // transform.position += Vector3.forward * (speed * Time.deltaTime);
        }
    }

 // This function is called when a move input is detected.
    void OnMove (InputValue movementValue)
    {
         // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();
 // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

 // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
         // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();
         // Check if the count has reached or exceeded the win condition.
        if (count >=12)
        {
             // Display the win text.
            winTextObject.SetActive(true);
        }
    }
}
    // void IsGrounded()
    // {
    //     canJump = Physics3D.BoxCast(boxCol3D.bounds.center, boxCol3D.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    // }

    // void HandleJumping()
    // {
    //     if (Input.GetBUttonDown(TagManager.JUMP_BUTTON))
    //     {
    //         if (canJump)
    //         {
    //             doubleJump = true;
    //             myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
    //         }
    //         else if (doubleJump)
    //         {
    //             doubleJump = flase;
    //             myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
    //         }
    //     }
    // }

