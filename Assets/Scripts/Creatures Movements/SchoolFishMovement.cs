using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolFishMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;              // Speed at which the fish move forward
    public float travelDistance = 10f;    // Distance the fish travel before turning around
    public float turnAroundTime = 1f;     // Time it takes for the fish to turn around

    private Vector2 startPoint;           // Starting point of the fish
    private Vector2 targetPoint;          // Target point the fish swim toward
    private bool movingForward = true;    // Indicates whether the fish are moving forward
    private float turnAroundTimer = 0f;   // Timer for the turning delay

    private void Start()
    {
        // Set the initial position as the starting point
        startPoint = transform.position;

        // Calculate the target point based on the travel distance
        targetPoint = startPoint + Vector2.right * travelDistance;
    }

    private void Update()
    {
        if (turnAroundTimer > 0f)
        {
            // Wait for the turning delay to finish
            turnAroundTimer -= Time.deltaTime;
            return;
        }

        // Move the fish in the current direction
        MoveFish();

        // Check if the fish have reached the target or start point
        if (movingForward && Vector2.Distance(transform.position, targetPoint) < 0.1f)
        {
            StartTurnAround(false); // Start turning back to the start point
        }
        else if (!movingForward && Vector2.Distance(transform.position, startPoint) < 0.1f)
        {
            StartTurnAround(true); // Start turning back to the target point
        }
    }

    private void MoveFish()
    {
        // Determine the direction based on whether the fish are moving forward or backward
        Vector2 direction = movingForward ? (targetPoint - (Vector2)transform.position).normalized
                                          : (startPoint - (Vector2)transform.position).normalized;

        // Move the fish in the calculated direction
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void StartTurnAround(bool toForward)
    {
        // Stop movement briefly for turning
        turnAroundTimer = turnAroundTime;

        // Set the new movement direction
        movingForward = toForward;

        // Flip the fish horizontally
        Vector3 scale = transform.localScale;
        scale.x = movingForward ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
