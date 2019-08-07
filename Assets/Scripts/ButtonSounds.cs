using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour {
    private AudioSource buttonAudio;
    public AudioSource micAudio;
    private TextMeshProUGUI text;
    //for the switch
    public int speechConvo = 5;

    // Start is called before the first frame update
    void Start() {
        buttonAudio = GetComponent<AudioSource>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {

        //the following are for the UI Hint Button
        if (text.text == "Say:\"<b><i>Salve</b></i>\"") {
            buttonAudio.clip = Resources.Load<AudioClip>("NewAudio/Salue");
        }

        if (text.text == "Say:\"<b><i>Valeo et tu</b></i>\"") {
            buttonAudio.clip = Resources.Load<AudioClip>("NewAudio/ValuoEtTu");

        }

        if (text.text == "Say:\"<b><i>Mihi nomen est__</b></i>\"") {
            buttonAudio.clip = Resources.Load<AudioClip>("NewAudio/MihiNomenEst");

        }

        if (text.text == "Say:\"<b><i>Habito in __</i></b>\"") {
            buttonAudio.clip = Resources.Load<AudioClip>("NewAudio/HabitoIn");

        }

        if (text.text == "Say:\"<b><i>Salve, quid agis Iulia</b></i>\"") {
            buttonAudio.clip = Resources.Load<AudioClip>("NewAudio/SalueQuidAgis");

        }
         
        if (text.text == "Say:\"<b><i>Valeo</b></i>\"") {
            buttonAudio.clip = Resources.Load<AudioClip>("NewAudio/Value");
        }

        if (text.text == "Say:\"<b><i>Bene et tu</b></i>\"") {
            buttonAudio.clip = Resources.Load<AudioClip>("NewAudio/BeneEtTu");
        }

        if (text.text == null) {
            buttonAudio.clip = null;
        }
    }

}
