using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EyeGlowTransition : MonoBehaviour
{
    public float fadeSpeed = 2.0f;    // Speed of the fade in/out
    public Light2D lightObj;

    // Start is called before the first frame update
    void Start()
    {
        if (lightObj != null)
        {
            SetlightObjAlpha(0); // Ensure the icon is invisible at the start
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(FadelightObj(1));
    }
    private System.Collections.IEnumerator FadelightObj(float targetAlpha)
    {
        if (lightObj == null) yield break;

        float startAlpha = lightObj.color.a;
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * fadeSpeed;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time);

            // Update the icon's color alpha
            SetlightObjAlpha(newAlpha);

            yield return null;
        }

        // Ensure the alpha is set to the target value at the end
        SetlightObjAlpha(targetAlpha);
    }
    private void SetlightObjAlpha(float alpha)
    {
        if (lightObj != null)
        {
            Color color = lightObj.color;
            color.a = alpha;
            lightObj.color = color;
        }
    }
}
