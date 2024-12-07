using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is near!");
            NewFunction();
        }
    }

    private void NewFunction()
    {
        // Logic Stuff here
        Debug.Log("Function triggered!");
    }
}
