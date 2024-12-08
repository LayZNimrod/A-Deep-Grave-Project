using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCutscene : MonoBehaviour
{
    public AudioSource audioSource;

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
            CineMachineShake.Instance.ShakeCamera(5f, 15f);
        }
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
