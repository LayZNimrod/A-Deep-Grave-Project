using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    public float chaseSpeed = 3.0f;        // Speed at which the shark chases the player or target
    public float detectionRange = 5.0f;    // Range at which the shark detects the target
    public float stoppingDistance = 1.0f;  // Distance at which the shark stops chasing
    public Collider2D movementBoundary;    // Collider2D that defines the movement boundary
    public Transform target;               // The target the shark will chase

    private bool isChasing = false;        // Whether the shark is currently chasing the target

    private void Start()
    {
        // If no target is assigned, try to automatically assign the player as the default target
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogWarning("No target assigned to SharkEnemy and no player found in the scene.");
            }
        }
    }

    private void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            // Start chasing the target if within detection range
            if (distanceToTarget <= detectionRange)
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }

            // If the shark is chasing, move towards the target
            if (isChasing && distanceToTarget > stoppingDistance)
            {
                ChaseTarget();
            }
        }
    }

    private void ChaseTarget()
    {
        // Calculate direction to the target
        Vector2 direction = (target.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * chaseSpeed * Time.deltaTime;

        // Constrain the target position within the boundary collider
        targetPosition = ConstrainToBoundary(targetPosition);

        // Move the shark towards the clamped target position
        transform.position = targetPosition;
    }

    private Vector2 ConstrainToBoundary(Vector2 targetPosition)
    {
        // Ensure the target position stays within the collider's bounds
        if (movementBoundary != null)
        {
            // Get the boundary's bounds
            Bounds boundaryBounds = movementBoundary.bounds;

            // Clamp the target position within the boundary's X and Y limits
            targetPosition.x = Mathf.Clamp(targetPosition.x, boundaryBounds.min.x, boundaryBounds.max.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, boundaryBounds.min.y, boundaryBounds.max.y);
        }

        return targetPosition;
    }
}
