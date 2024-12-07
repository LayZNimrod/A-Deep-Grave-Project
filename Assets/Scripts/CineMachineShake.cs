using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineMachineShake : MonoBehaviour
{
    public static CineMachineShake Instance { get; private set; }
    private CinemachineVirtualCamera CNvirtualCamera;
    private float shakeTimer;

    private void Awake()
    {
        /*
         * Can be accessed from other scripts by using this
         * CineMachineShake.Instance.ShakeCamera(intensity, time);
        */
        Instance = this;
        CNvirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin CNvirtualCameraMultiChannelPerlin = CNvirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        CNvirtualCameraMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer < 0f) {
                CinemachineBasicMultiChannelPerlin CNvirtualCameraMultiChannelPerlin = CNvirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                CNvirtualCameraMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
