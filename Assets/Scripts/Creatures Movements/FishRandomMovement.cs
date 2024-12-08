using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRandomMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3.0f;             // Speed of the fish movement
    [Range(1, 100)]
    public float randomness = 50f;         // Randomness of movement (higher values = more erratic movement)
    public float decelerationTime = 1.0f;  // Time it takes for the fish to slow down to a stop

    [Header("Area Settings")]
    public Collider2D movementArea;       // The collider defining the movement area

    private Vector2 direction;             // Direction the fish is currently moving in
    private float timeToChangeDirection;   // Timer to change direction
    private float currentSpeed;            // Current speed of the fish (which will be reduced over time)

    private void Start()
    {
        // Initialize random direction and set a random time to change direction
        SetRandomDirection();
        SetRandomTimeToChangeDirection();
        currentSpeed = speed;  // Start with full speed
    }

    private void Update()
    {
        // Move the fish in the current direction
        MoveFish();

        // If the timer runs out, change direction
        timeToChangeDirection -= Time.deltaTime;
        if (timeToChangeDirection <= 0f)
        {
            // Gradually decelerate to a stop
            Decelerate();

            // When the fish has stopped, set a new random direction
            if (currentSpeed <= 0.1f)
            {
                SetRandomDirection();  // Change direction randomly
                SetRandomTimeToChangeDirection();  // Reset the timer
                currentSpeed = speed;  // Reset speed back to original
            }
        }

        // Constrain the fish's position within the movement area
        ConstrainToArea();
    }

    private void SetRandomDirection()
    {
        // Randomize direction based on randomness value
        float randomAngle = Random.Range(0f, 360f);  // Random angle between 0 and 360 degrees
        direction = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
    }

    private void SetRandomTimeToChangeDirection()
    {
        // Set a random time interval before changing direction (higher randomness = more frequent changes)
        timeToChangeDirection = Random.Range(1f, 5f / (randomness / 100f));  // Random time based on randomness value
    }

    private void MoveFish()
    {
        // Move the fish in the current direction, based on current speed and time
        transform.Translate(direction * currentSpeed * Time.deltaTime);
    }

    private void Decelerate()
    {
        // Decelerate the fish over time based on decelerationTime
        currentSpeed = Mathf.Max(0f, currentSpeed - (speed / decelerationTime) * Time.deltaTime);
    }

    private void ConstrainToArea()
    {
        // If the movement area is defined, constrain the fish's position within the bounds
        if (movementArea != null)
        {
            Bounds bounds = movementArea.bounds;

            // Clamp the fish's position within the boundaries
            float clampedX = Mathf.Clamp(transform.position.x, bounds.min.x, bounds.max.x);
            float clampedY = Mathf.Clamp(transform.position.y, bounds.min.y, bounds.max.y);

            // Apply the clamped position
            transform.position = new Vector2(clampedX, clampedY);
        }
    }
}
