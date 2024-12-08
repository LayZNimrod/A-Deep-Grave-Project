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
    }

    public void ShakeCamera(float intensity, float shakeTime)
    {
        perlinNoise.m_AmplitudeGain = intensity;
        StartCoroutine(WaitTime(shakeTime));
    }

    IEnumerator WaitTime(float shakeTime)
    {
        yield return new WaitForSeconds(shakeTime);
        ResetIntensity();
    }

    void ResetIntensity()
    {
        perlinNoise.m_AmplitudeGain = 0f;
    }
}
