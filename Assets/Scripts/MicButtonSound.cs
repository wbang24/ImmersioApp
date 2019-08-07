using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicButtonSound : MonoBehaviour
{
    private AudioSource micAudio;
    private void Start() {
        micAudio = GetComponent<AudioSource>();
    }
    public void MicOnButtonSound() {
        micAudio.clip = Resources.Load<AudioClip>("Audio/MicOnAudio");
        micAudio.Play();
    }
    public void MicOffButtonSound() {
        micAudio.clip = Resources.Load<AudioClip>("Audio/MicOffAudio");
        micAudio.Play();
    }
}
