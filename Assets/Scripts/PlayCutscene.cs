using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCutscene : MonoBehaviour
{
    public AudioSource audioSource;

    //Copy this around
    [Header("CameraShake Settings")]
    [SerializeField] private CineMachineShake cameraShake;
    [SerializeField] private float shakeIntensity = 1f;
    [SerializeField] private float shakeTimer = 1f;
    [SerializeField] private float StartFrequency = 1f;
    [SerializeField] private float EndFrequency = 3f;

    public float speed = 1;

    private bool triggerZoom;

    [SerializeField] private CinemachineVirtualCamera cam;
    

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        triggerZoom = false;
        audioSource = GetComponent<AudioSource>();
        animator.SetBool("Closed", true);
        animator.SetBool("Anim", false);
        animator.SetBool("Open", false);
    }

    IEnumerator AnimateEye()
    {
        Debug.Log("REACHED BOOL");
        triggerZoom = true;
        animator.SetBool("Anim", true);
        animator.SetBool("Open", false);
        animator.SetBool("Closed", false);
        yield return new WaitForSeconds(4);
        animator.SetBool("Open", true);
        animator.SetBool("Closed", false);
        animator.SetBool("Anim", false);
        Debug.Log("REACHED END");
    }

    IEnumerator ZoomOut()
    {
        if (cam.m_Lens.OrthographicSize <= 13)
        {
            cam.m_Lens.OrthographicSize += 1;
            yield return new WaitForSeconds(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (audioSource != null)
        {
            audioSource.Play();
            //Copy this around
            Debug.Log("REACHED EYE");
            StartCoroutine(AnimateEye());
            cameraShake.ShakeCamera(shakeIntensity, shakeTimer, StartFrequency, EndFrequency);
        }
        this.GetComponent<BoxCollider2D>().enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (triggerZoom)
        {
            StartCoroutine(ZoomOut());
        }
    }
}
