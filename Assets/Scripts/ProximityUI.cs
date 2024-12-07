using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProximityUI : MonoBehaviour
{
    public Image proximityIcon; //Assign UI icon in the Inspector
    public float fadeSpeed = 2.0f;    // Speed of the fade in/out

    private int nearbyObjectsCount = 0; // Tracks how many objects are within range
    private Color iconColor;           // Stores the initial color of the icon

    private void Start()
    {
        if (proximityIcon != null)
        {
            iconColor = proximityIcon.color; // Save the initial icon color
            SetIconAlpha(0); // Ensure the icon is invisible at the start
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Environment") || other.CompareTag("LivingCreature"))
        {
            // Increment nearby object count
            nearbyObjectsCount++;

            // Start fading in the icon
            if (nearbyObjectsCount > 0)
            {
                StartCoroutine(FadeIcon(1)); // Fade to full visibility
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Environment") || other.CompareTag("LivingCreature"))
        {
            // Decrement nearby object count
            nearbyObjectsCount--;

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
