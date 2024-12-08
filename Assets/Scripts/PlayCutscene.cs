using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCutscene : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Movement Settings")]
    [SerializeField] private CineMachineShake cameraShake;
    [SerializeField] private float shakeIntensity = 1f;
    [SerializeField] private float shakeTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (audioSource != null)
        {
            audioSource.Play();
            cameraShake.ShakeCamera(shakeIntensity, shakeTimer);
        }
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
