using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialListiner : MonoBehaviour
{
    [SerializeField] SerialController se;
    [SerializeField] LightNumCalc lightCalc;
    public int sensor_val = 0;
    public int buttonR = 0;
    public int buttonL = 0;


    // Start is called before the first frame update

    private void Start()
    {
        
    }
    void OnMessageArrived(string msg)
    {
        string[] msgArray = msg.Split(',');
        sensor_val = int.Parse(msgArray[0]);
        buttonR = int.Parse(msgArray[1]);
        buttonL = int.Parse(msgArray[2]);

        
        serialOutput();
    }

    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
    public void serialOutput()
    {
        se.SendSerialMessage(lightCalc.grabLightNum().ToString()+"\n");
    }
}
