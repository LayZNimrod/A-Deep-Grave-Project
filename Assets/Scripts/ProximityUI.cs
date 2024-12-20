using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProximityUI : MonoBehaviour
{
    public Image proximityIcon; //Assign UI icon in the Inspector
    public AnimationClip proximityIconGlitch; //Assign UI icon in the Inspector
    public float fadeSpeed = 2.0f;    // Speed of the fade in/out
    public int amountEnemy = 0;
    public int amountFish = 0;
    public int amountWall = 0;

    private int nearbyObjectsCount = 0; // Tracks how many objects are within range
    private Color iconColor;           // Stores the initial color of the icon

    public Transform submarine;
    private Vector3 offset;

    private void Start()
    {
        if (proximityIcon != null)
        {
            iconColor = proximityIcon.color; // Save the initial icon color
            SetIconAlpha(0); // Ensure the icon is invisible at the start
        }

        if (submarine == null)
        {
            Debug.LogWarning("Submarine reference is missing in ProximityUI.");
        }
        else
        {
            offset = transform.position - submarine.position;
        }
    }

    private void FixedUpdate()
    {
        if (submarine != null)
        {
            // Update position to follow the submarine with the offset
            transform.position = submarine.position + offset;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LivingCreature") || other.CompareTag("iddy bitty fish"))
        {
            // Increment nearby object count
            nearbyObjectsCount++;
            if (other.CompareTag("LivingCreature"))
            {
                amountEnemy++;
            }
            if (other.CompareTag("iddy bitty fish"))
            {
                amountFish++;
            }
            // Start fading in the icon
            if (nearbyObjectsCount > 0)
            {
                StartCoroutine(FadeIcon(1)); // Fade to full visibility
            }
        }
        // This part will detect environment objects using isTrigger
        else if (other.CompareTag("Environment"))
        {
            // For trigger colliders set as non-trigger (solid objects), detect them as well
            nearbyObjectsCount++;
            amountWall++;

            // Start fading in the icon
            StartCoroutine(FadeIcon(1)); // Fade to full visibility
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Environment"))
        {
            nearbyObjectsCount++;
            amountWall++;

            // Start fading in the icon
            StartCoroutine(FadeIcon(1)); // Fade to full visibility
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("LivingCreature") || other.CompareTag("iddy bitty fish"))
        {
            // Decrement nearby object count
            nearbyObjectsCount--;
            if (other.CompareTag("LivingCreature"))
            {
                amountEnemy--;
            }
            if (other.CompareTag("iddy bitty fish"))
            {
                amountFish--;
            }
            // Start fading out the icon if no objects are nearby
            if (nearbyObjectsCount <= 0)
            {
                StartCoroutine(FadeIcon(0)); // Fade to invisible
            }
        }
        else if (other.CompareTag("Environment"))
        {
            // Decrement nearby object count when exiting the environment
            nearbyObjectsCount--;
            amountWall--;

            // Start fading out the icon if no objects are nearby
            if (nearbyObjectsCount <= 0)
            {
                StartCoroutine(FadeIcon(0)); // Fade to invisible
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Environment"))
        {
            nearbyObjectsCount--;
            amountWall--;

            // Start fading out the icon if no objects are nearby
            if (nearbyObjectsCount <= 0)
            {
                StartCoroutine(FadeIcon(0)); // Fade to invisible
            }
        }
    }

    private System.Collections.IEnumerator FadeIcon(float targetAlpha)
    {
        if (proximityIcon == null) yield break;

        float startAlpha = proximityIcon.color.a;
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * fadeSpeed;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time);

            // Update the icon's color alpha
            SetIconAlpha(newAlpha);

            yield return null;
        }

        // Ensure the alpha is set to the target value at the end
        SetIconAlpha(targetAlpha);
    }

    private void SetIconAlpha(float alpha)
    {
        if (proximityIcon != null)
        {
            Color color = proximityIcon.color;
            color.a = alpha;
            proximityIcon.color = color;
        }
    }
}