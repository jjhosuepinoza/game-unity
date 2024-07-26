using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BulletScript controls the behavior of the bullet
public class BulletScript : MonoBehaviour
{
    // Public variable for setting the bullet speed
    public float Speed;

    // Private variables for handling physics and direction
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;

    // Start is called before the first frame update
    // Initializes the Rigidbody2D component
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    private void FixedUpdate()
    {
        // Set the velocity of the bullet based on its direction and speed
        Rigidbody2D.velocity = Direction * Speed;
    }

    // SetDirection sets the direction of the bullet
    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }
}
