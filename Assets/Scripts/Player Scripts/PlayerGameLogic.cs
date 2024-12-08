using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameLogic : MonoBehaviour
{
    [Header("Submarine Settings")]
    public int maxLives = 4;                  // Total lives the submarine starts with
    public float bumpDistance = 2.0f;         // Distance the submarine is bumped away upon collision
    public float invulnerabilityTime = 2.0f;  // Duration of invulnerability after being hit
    public float bumpForce = 5.0f;            // Force applied to bump the submarine

    private int currentLives;                 // Current lives of the submarine
    private bool isInvulnerable = false;      // Whether the submarine is currently invulnerable
    private Rigidbody2D rb;                  // Rigidbody2D component of the submarine

    private void Start()
    {
        currentLives = maxLives; // Initialize lives
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing on the Submarine.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an "Environment" object and handle accordingly
        if (!isInvulnerable && collision.gameObject.CompareTag("Environment"))
        {
            HandleCollision(collision.contacts[0].point, collision.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with an object tagged "Environment" or "Living Creatures"
        if (!isInvulnerable && other.CompareTag("LivingCreature"))
        {
            HandleCollision(transform.position, other.transform.position);
        }
    }

    private void HandleCollision(Vector2 impactPoint, Vector2 sourcePosition)
    {
        currentLives--; // Reduce lives
        Debug.Log($"Submarine hit! Remaining lives: {currentLives}");

        // Handle bumping away from the collision object
        Vector3 bumpDirection = ((Vector2)transform.position - sourcePosition).normalized;
        rb.AddForce(bumpDirection * bumpForce, ForceMode2D.Impulse);

        // Trigger invulnerability
        StartCoroutine(InvulnerabilityCoroutine());

        // Handle death logic if lives are exhausted
        if (currentLives <= 0)
        {
            HandleDeath();
        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        Debug.Log("Submarine is invulnerable.");
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
        Debug.Log("Submarine is no longer invulnerable.");
    }

    private void HandleDeath()
    {
        Debug.Log("Submarine destroyed!");
        // Add your death handling logic here (e.g., game over, respawn, etc.)
        gameObject.SetActive(false); // Temporarily disable the submarine
    }
}