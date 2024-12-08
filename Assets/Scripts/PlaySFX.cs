using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("CameraShake Settings")]
    [SerializeField] private CineMachineShake cameraShake;
    [SerializeField] private float shakeIntensity = 1f;
    [SerializeField] private float shakeTimer = 1f;
    [SerializeField] private float StartFrequency = 1f;
    [SerializeField] private float EndFrequency = 3f;

    public float speed = 1;

    [SerializeField] private CinemachineVirtualCamera cam;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            if (audioSource != null)
            {
                audioSource.Play();
                StartCoroutine(ZoomOut());
                cameraShake.ShakeCamera(shakeIntensity, shakeTimer, StartFrequency, EndFrequency);
            }
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator ZoomOut()
    {
        for (int i = 0; i <= 12; i++)
            if (cam.m_Lens.OrthographicSize <= 9)
            {
                cam.m_Lens.OrthographicSize += 0.5f;
                yield return new WaitForSeconds(0.2f);
            }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
