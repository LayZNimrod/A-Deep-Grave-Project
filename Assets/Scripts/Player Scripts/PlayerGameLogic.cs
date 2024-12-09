using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerGameLogic : MonoBehaviour
{
    [Header("Submarine Settings")]
    public int maxLives = 4;                  // Total lives the submarine starts with
    public float speedBoostMultiplier = 1.5f; // Speed boost multiplier after a hit
    public float boostDuration = 2f;          // Duration of the speed boost
    public float invulnerabilityTime = 2f;    // Duration of invulnerability after being hit
    public Collider2D submarineCollider;

    [Header("Light Flicker Settings")]
    public Light2D lightObject;                // Reference to the light object
    public float flickerInterval = 0.1f;       // Interval for the light flicker

    private int currentLives;                 // Current lives of the submarine
    private bool isInvulnerable = false;      // Whether the submarine is currently invulnerable
    private PlayerMovement playerMovement;

    private void Start()
    {
        currentLives = maxLives; // Initialize lives
        playerMovement = GetComponent<PlayerMovement>();

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script is not attached to the submarine.");
        }

        // Ensure the primary collider is assigned
        if (submarineCollider == null)
        {
            submarineCollider = GetComponent<Collider2D>();
            if (submarineCollider == null)
            {
                Debug.LogError("Submarine Collider is not assigned or found. Please assign it.");
            }
        }

        // Ensure the light is assigned
        if (lightObject == null)
        {
            lightObject = GetComponentInChildren<Light2D>();
            if (lightObject == null)
            {
                Debug.LogWarning("Light object not assigned or found.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with an object tagged "Environment" or "Living Creatures"
        if (isInvulnerable)
            return;
        if (collision.CompareTag("LivingCreature"))
        {
            HandleCollision();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision with solid "Environment" objects
        if (isInvulnerable) return;

        if (collision.collider.CompareTag("Environment"))
        {
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        if (isInvulnerable) return; // Ignore damage if invulnerable

        currentLives--; // Reduce lives
        Debug.Log($"Submarine hit! Remaining lives: {currentLives}");

        // Trigger speed boost and invulnerability
        StartCoroutine(SpeedBoostCoroutine());
        StartCoroutine(InvulnerabilityCoroutine());

        // Handle death logic if lives are exhausted
        if (currentLives <= 0)
        {
            HandleDeath();
        }
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        if (playerMovement != null)
        {
            playerMovement.speed *= speedBoostMultiplier; // Apply speed boost
            yield return new WaitForSeconds(boostDuration);
            playerMovement.speed /= speedBoostMultiplier; // Reset speed
        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        if (lightObject != null)
        {
            StartCoroutine(FlickerLightCoroutine());
        }

        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    private IEnumerator FlickerLightCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < invulnerabilityTime)
        {
            lightObject.enabled = !lightObject.enabled; // Toggle light
            yield return new WaitForSeconds(flickerInterval);
            elapsedTime += flickerInterval;
        }

        lightObject.enabled = true; // Ensure light is on after invulnerability ends
    }

    private void HandleDeath()
    {
        Debug.Log("Submarine destroyed!");
        // Add your death handling logic here
        Destroy(gameObject); // Temporarily disable the submarine
    }
}