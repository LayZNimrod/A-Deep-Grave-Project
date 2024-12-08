using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineMachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera CNvirtualCamera;
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    private void Awake()
    {
        CNvirtualCamera = GetComponent<CinemachineVirtualCamera>();
        perlinNoise = CNvirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ResetIntensity();
        ResetFrequency();
    }
    public void ShakeCamera(float initialIntensity, float shakeDuration, float initialFrequency, float finalFrequency)
    {
        StopAllCoroutines(); // Stop any ongoing shake to avoid conflicts
        StartCoroutine(ShakeCoroutine(initialIntensity, shakeDuration, initialFrequency, finalFrequency));
    }

    private IEnumerator ShakeCoroutine(float initialIntensity, float shakeDuration, float initialFrequency, float finalFrequency)
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float remainingTime = shakeDuration - elapsed;
            float normalizedTime = remainingTime / shakeDuration;

            // Linearly reduce the amplitude based on time
            perlinNoise.m_AmplitudeGain = Mathf.Lerp(0f, initialIntensity, remainingTime / shakeDuration);

            perlinNoise.m_FrequencyGain = Mathf.Lerp(finalFrequency, initialFrequency, normalizedTime);

            elapsed += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Resets after shake ends
        ResetIntensity();
        ResetFrequency();
    }


    void ResetIntensity()
    {
        perlinNoise.m_AmplitudeGain = 0f;
    }

    void ResetFrequency()
    {
        perlinNoise.m_FrequencyGain = 0f;
    }
}
