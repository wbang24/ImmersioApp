using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneSoundVisualizer : MonoBehaviour
{

    public AudioClip audioClip;
    bool useMicrophone;
    // Start is called before the first frame update
    void Start()
    {

        if (useMicrophone) {
            if (Microphone.devices.Length > 0) {

            }
            else {
                useMicrophone = false;
            }
        }
        if (!useMicrophone) {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
