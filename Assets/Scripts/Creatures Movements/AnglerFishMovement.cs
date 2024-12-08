using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerFishMovement : MonoBehaviour
{
    [Header("Passive Movement Settings")]
    public float passiveSpeed = 3.0f;             // Speed of passive movement
    public float decelerationTime = 1.0f;         // Time it takes to slow down
    [Range(1, 100)]
    public float randomness = 50f;               // Randomness of movement (1-100)
    public Collider2D movementArea;              // Area for passive movement

    [Header("Chase Settings")]
    public Transform target;                     // Target to chase
    public float detectionRange = 5f;            // Range to detect and chase target
    public float chaseSpeed = 6.0f;              // Speed when chasing
    public float loseTargetTime = 3.0f;          // Time to return to passive after losing target

    private Vector2 passiveDirection;            // Direction of passive movement
    private float passiveTimer;                  // Timer to change direction
    private float currentSpeed;                  // Current speed for passive movement
    private bool isChasing = false;              // Whether currently chasing
    private float noTargetTimer = 0f;            // Timer after losing the target

    private SpriteRenderer spriteRenderer;
    private Transform lightObject;
    private Vector3 initialLightPosition;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lightObject = transform.Find("Light 2D");
        if (lightObject != null)
        initialLightPosition = lightObject.localPosition;

        SetRandomDirection();
        SetRandomTimeToChangeDirection();
        currentSpeed = passiveSpeed;
    }

    private void Update()
    {
        if (isChasing)
        {
            HandleChaseBehavior();
        }
        else
        {
            HandlePassiveMovement();
            CheckForTarget();
        }
    }

    private void HandlePassiveMovement()
    {
        if (passiveTimer > 0)
        {
            passiveTimer -= Time.deltaTime;
        }
        else
        {
            Decelerate();

            if (currentSpeed <= 0.1f)
            {
                SetRandomDirection();
                SetRandomTimeToChangeDirection();
                currentSpeed = passiveSpeed;
            }
        }

        Move(passiveDirection, currentSpeed);
        ConstrainToArea();
        FlipBasedOnDirection(passiveDirection);
    }

    private void CheckForTarget()
    {
        if (target == null) return;

        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget <= detectionRange)
        {
            isChasing = true;
        }
    }

    private void HandleChaseBehavior()
    {
        if (target == null)
        {
            isChasing = false;
            return;
        }

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= detectionRange)
        {
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            Move(directionToTarget, chaseSpeed);
            FlipBasedOnDirection(directionToTarget);
            noTargetTimer = 0f; // Reset timer while chasing
        }
        else
        {
            noTargetTimer += Time.deltaTime;
            if (noTargetTimer >= loseTargetTime)
            {
                isChasing = false;
                SetRandomDirection();
                SetRandomTimeToChangeDirection();
            }
        }
    }

    private void SetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        passiveDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
    }

    private void SetRandomTimeToChangeDirection()
    {
        passiveTimer = Random.Range(1f, 5f / (randomness / 100f));
    }

    private void Move(Vector2 direction, float speed)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void Decelerate()
    {
        currentSpeed = Mathf.Max(0f, currentSpeed - (passiveSpeed / decelerationTime) * Time.deltaTime);
    }

    private void ConstrainToArea()
    {
        if (movementArea == null) return;

        Bounds bounds = movementArea.bounds;

        float clampedX = Mathf.Clamp(transform.position.x, bounds.min.x, bounds.max.x);
        float clampedY = Mathf.Clamp(transform.position.y, bounds.min.y, bounds.max.y);

        transform.position = new Vector2(clampedX, clampedY);
    }

    private void FlipBasedOnDirection(Vector2 direction)
    {
        bool movingForward = direction.x > 0;

        if (spriteRenderer == null)
            return;
        //The Sprite itself
        spriteRenderer.flipX = movingForward;
        Vector3 lightPosition = initialLightPosition;
        //The Light Position
        lightPosition.x = movingForward ? -initialLightPosition.x : initialLightPosition.x;
        lightObject.localPosition = lightPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
