using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;
    private SpriteRenderer spriteRenderer;  // To handle sprite flipping

    private Vector2 movementDirection;
    private bool movingForward;
    [SerializeField] SerialListiner seruak;

    // Start is called before the first frame update
    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        movingForward = true;
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = seruak.buttonR-seruak.buttonL;
        float yInput = seruak.sensor_val;

        // Determine the movement direction
        movementDirection = new Vector2(xInput, yInput);

        if (Mathf.Abs(xInput) > 0)
        {
            body.velocity = new Vector2(xInput * speed, body.velocity.y);

            movingForward = xInput > 0;
        }

        if (Mathf.Abs(yInput) > 400)
        {
            body.velocity = new Vector2(body.velocity.x, -speed);
        }
        if (Mathf.Abs(yInput) < 250)
        {
            body.velocity = new Vector2(body.velocity.x, speed);
        }

        FlipBasedOnDirection(movingForward);
    }

    private void FlipBasedOnDirection(bool movingFroward)
    {

        if (spriteRenderer == null)
            return;

        // Flip the sprite horizontally based on movement direction
        spriteRenderer.flipX = !movingForward;
    }
}

