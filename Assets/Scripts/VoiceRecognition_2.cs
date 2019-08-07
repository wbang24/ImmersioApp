using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using UnityEngine.UI;
using TMPro;


public class VoiceRecognition_2 : MonoBehaviour {
    private KeywordRecognizer keyword;
    private Dictionary<string, Action> listWords = new Dictionary<string, Action>();

    [SerializeField]
    public TextMeshProUGUI textPromt = null;

    public GameObject convoButton;

    public Image progressFill;

    public TextMeshProUGUI progress = null;
    private int state = 6;

    //Audio

    private AudioSource narroratorAudio;

    public AudioSource randomErrorAudio;

    public AudioClip[] audioSources;

    public GameObject button1;
    public GameObject button2;

    // private bool[] flags = new bool[5];

    //rough case with flags test
    


    
    //Change CondifenceLevel.Low to Confidence.Medium to test the method
    ConfidenceLevel ConfidenceLevel = ConfidenceLevel.Low;


    private void Start() {


        listWords.Add("Salue", intro1);
        listWords.Add("Bene et tu", intro2);
        listWords.Add("Miji nomen est", intro3);
        listWords.Add("Jabito in", intro4);
        listWords.Add("Salue, cuid aguis Iulia", intro5);
        listWords.Add("Ualeo", intro6);

        //error non inteligo 


        narroratorAudio = GetComponent<AudioSource>();
        convoButton.GetComponent<AudioSource>();

        /**
         *  Latin using Mexican-Spanish
         * 1. Sal Uai
         * 2. Cuid Agis
         * 3. 
         * 4.
         * 
         */

        keyword = new KeywordRecognizer(listWords.Keys.ToArray());

        keyword.OnPhraseRecognized += RecognizedSpeech;


        narroratorAudio.clip = Resources.Load<AudioClip>("NewAudio/2-01-salue");
        narroratorAudio.Play();
        textPromt.text = "Say:\"<b><i>Salve</b></i>\"";

        keyword.Start();


        //"when it gets triggered, this is what it does"

    }

    public void Update() {

    }

    ////Setting on and off the speech recognition
    public void ButtonClicked() {
        keyword.Start();
        Debug.Log(keyword);
    }
    public void ButtonUnclicked(){
        keyword.Stop();
        Debug.Log(keyword);
    }
    


    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {

        Debug.Log(speech.text);
        Debug.Log(speech.confidence);
        listWords[speech.text].Invoke();

        //Default is medium, if low then we randomly output error messages
        //if (speech.confidence == ConfidenceLevel.Medium) {
        //    narroratorAudio.clip = audioSources[UnityEngine.Random.Range(0, audioSources.Length)];
        //    narroratorAudio.Play();
        //}


        //FOUND LIMITATIONS
        //Unity Speech has limted options for capturing error. Confidence level is sorted to 3 enums, 3 options. Low, Medium, and High.
        //To Test this function, change Confidence level to Medium;
        //Look at line 37 for more on how
        if ((speech.text == "Valeo et tu") && (speech.confidence == ConfidenceLevel.Low)) {
            StartCoroutine(Audio1());
        }
        if (speech.text == "Miji nomen est" && speech.confidence == ConfidenceLevel.Low) {
            StartCoroutine("Audio2");
        }
        if (speech.text == "Miji habitou" && speech.confidence == ConfidenceLevel.Low) {
            StartCoroutine("Audio3");
        }
        if (speech.text == "Salue, cuid agis Marcus" && speech.confidence == ConfidenceLevel.Low) {
            StartCoroutine("Audio4");
        }
        if (speech.text == "Valeo" && speech.confidence == ConfidenceLevel.Low) {
            StartCoroutine("Audio5");
        }

    }

    private void intro1() {


        //play intro1 sounds
        narroratorAudio.clip = Resources.Load<AudioClip>("NewAudio/2-02-quid-aguis");
        narroratorAudio.Play();
        textPromt.text = "Say:\"<b><i>Bene et tu</b></i>\"";
        progress.text = "20%";
        progressFill.fillAmount = 0.2f;
        listWords.Remove("Salue");
    }

    private void intro2() {

        StartCoroutine("Narrarator3");
        textPromt.text = "Say:\"<b><i>Mihi nomen est__</b></i>\"";
        progress.text = "40%";
        progressFill.fillAmount = 0.4f;
        listWords.Remove("Ualeo et tu");
    }

    private void intro3() {

        //play intro3 sounds
        narroratorAudio.clip = Resources.Load<AudioClip>("NewAudio/2-05-habito");
        narroratorAudio.Play();
        textPromt.text = "Say:\"<b><i>Habito in __</i></b>\"";
        progress.text = "60%";
        progressFill.fillAmount = 0.6f;
        listWords.Remove("Miji nomen est");
    }

    private void intro4() {

        //play intro4 sounds
        StartCoroutine("Narrarator4");
        textPromt.text = "Say:\"<b><i>Salve, quid agis Iulia</b></i>\"";
        progress.text = "80%";
        progressFill.fillAmount = 0.8f;
        listWords.Remove("Miji habitou");
    }
    private void intro5() {
        //play intro5 sounds
        narroratorAudio.clip = Resources.Load<AudioClip>("NewAudio/2-06-iuliaa");
        narroratorAudio.Play();
        textPromt.text = "Say:\"<b><i>Valeo</b></i>\"";
        progress.text = "100%";
        progressFill.fillAmount = 1.0f;
    }
    private void intro6() {
        //play intro6 sounds
        narroratorAudio.clip = Resources.Load<AudioClip>("NewAudio/2-07-optime");
        narroratorAudio.Play();
        textPromt.text = null;
        button1.SetActive(true);
        button2.SetActive(true);
    }



    //just need time between actions

    IEnumerator Audio1() {
        intro1();
        narroratorAudio.clip = audioSources[UnityEngine.Random.Range(0, audioSources.Length)];
        narroratorAudio.Play();
        yield return new WaitForSeconds(narroratorAudio.clip.length);
        narroratorAudio.clip = Resources.Load<AudioClip>("Audio/2_Response_Valeo");
        narroratorAudio.Play();
        yield return new WaitForSeconds(narroratorAudio.clip.length);
        intro1();
    }


    IEnumerator Narrarator3() {
        yield return new WaitForSeconds(1);
        narroratorAudio.clip = Resources.Load<AudioClip>("NewAudio/2-03-bene");
        narroratorAudio.Play();
        yield return new WaitForSeconds(narroratorAudio.clip.length);
        narroratorAudio.clip = Resources.Load<AudioClip>("NewAudio/2-04-nomen-tibi");
        narroratorAudio.Play();
    }

    IEnumerator Narrarator4() {
        yield return new WaitForSeconds(1);
        narroratorAudio.clip = Resources.Load<AudioClip>("NewAudio/2-06-iualia");
        narroratorAudio.Play();
        yield return new WaitForSeconds(narroratorAudio.clip.length);
    }

}
