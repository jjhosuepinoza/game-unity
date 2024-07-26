using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// JohnMovement handles the movement and actions of the player character
public class JohnMovement : MonoBehaviour
{
    // Public variables for assigning the bullet prefab and movement parameters
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;

    // Private variables for handling physics, animations, input, and shooting mechanics
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;

    // Start is called before the first frame update
    // Initializes the Rigidbody2D and Animator components
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    // Handles input and updates the character's state
    void Update()
    {
        // Get horizontal input from player (left/right movement)
        Horizontal = Input.GetAxisRaw("Horizontal");

        // Update the character's facing direction based on input
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Set the running animation based on horizontal input
        Animator.SetBool("running", Horizontal != 0.0f);

        // Draw a debug ray to visualize ground check
        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);

        // Check if the character is grounded using a raycast
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.2f))
        {
            Grounded = true;
        }
        else Grounded = false;

        // Check for jump input and if the character is grounded
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        // Check for shooting input and rate limiting
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    // Handles the jump action by applying an upward force
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    // Handles the shoot action by instantiating a bullet and setting its direction
    private void Shoot()
    {
        // Determine the bullet's direction based on the character's facing direction
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        // Instantiate the bullet at the character's position, slightly offset in the shooting direction
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    private void FixedUpdate()
    {
        // Update the character's velocity based on horizontal input
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }
}
