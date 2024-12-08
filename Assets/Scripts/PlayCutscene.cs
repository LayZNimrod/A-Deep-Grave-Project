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

    public GameObject ToAnimate;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ToAnimate.GetComponent<Animator>().Play("EyeClosed");
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (audioSource != null)
        {
            audioSource.Play();
            cameraShake.ShakeCamera(shakeIntensity, shakeTimer);
            AnimateEye();
        }
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator AnimateEye()
    {
        ToAnimate.GetComponent<Animator>().Play("EyeAnim");
        yield return new WaitForSeconds(15);
        ToAnimate.GetComponent<Animator>().Play("EyeOpen ");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
