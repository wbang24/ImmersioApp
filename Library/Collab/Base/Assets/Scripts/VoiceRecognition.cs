using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using UnityEngine.UI;
using TMPro;


public class VoiceRecognition : MonoBehaviour {
    private KeywordRecognizer keyword;
    private Dictionary<string, Action> listWords = new Dictionary<string, Action>();

    [SerializeField]
    public TextMeshProUGUI textPromt = null;

    public GameObject convoButton;


    //Audio

    private AudioSource narroratorAudio;

    public AudioSource randomErrorAudio;

    public AudioClip[] audioSources;

    private void Start() {


        listWords.Add("Salue", intro1);
        listWords.Add("Valeo et tu", intro2);
        listWords.Add("Miji nomen est", intro3);
        listWords.Add("Miji habito", intro4);
        listWords.Add("Salue, cuid agis Marcus", intro5);
        listWords.Add("Valeo", intro6);

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


        narroratorAudio.clip = Resources.Load<AudioClip>("Audio/1_Convo_Slave");
        narroratorAudio.Play();
        textPromt.text = "Say:\"Salve\"";

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
        if (speech.confidence == ConfidenceLevel.Medium) {
            narroratorAudio.clip = audioSources[UnityEngine.Random.Range(0, audioSources.Length)];
            narroratorAudio.Play();
        }
    }

    private void intro1() {

        //play intro1 sounds
        StartCoroutine(momentOfSilence());
        narroratorAudio.clip = Resources.Load<AudioClip>("Audio/2_Convo_QuidAgis");
        narroratorAudio.Play();
        StartCoroutine(momentOfSilence());
        textPromt.text = "Say:\"Valeo et tu\"";

    }

    private void intro2() {

        //play intro2 sounds
        StartCoroutine(momentOfSilence());
        narroratorAudio.clip = Resources.Load<AudioClip>("Audio/4_Convo_ValeoGratias");
        narroratorAudio.Play();
        StartCoroutine(momentOfSilence());
        textPromt.text = "Say:\"Mihi nomen est __\"";
    }

    private void intro3() {

        //play intro3 sounds
        StartCoroutine(momentOfSilence());
        narroratorAudio.clip = Resources.Load<AudioClip>("Audio/5_Convo_Habito");
        narroratorAudio.Play();
        StartCoroutine(momentOfSilence());
        textPromt.text = "Say:\"Mihi habito __\"";

    }

    private void intro4() {

        //play intro4 sounds
        StartCoroutine(momentOfSilence());
        narroratorAudio.clip = Resources.Load<AudioClip>("Audio/6_Convo_Valim");
        narroratorAudio.Play();
        StartCoroutine(momentOfSilence());
        textPromt.text = "Say:\"Salve, quid agis, Marcus\"";
    }
    private void intro5() {
        //play intro4 sounds
        StartCoroutine(momentOfSilence());
        narroratorAudio.clip = Resources.Load<AudioClip>("Audio/3_Convo_Valeo");
        narroratorAudio.Play();
        StartCoroutine(momentOfSilence());
        textPromt.text = "Say:\"Valeo\"";

    }
    private void intro6() {
        //play intro4 sounds
        StartCoroutine(momentOfSilence());
        narroratorAudio.clip = Resources.Load<AudioClip>("Audio/7_Convo_Mastered");
        narroratorAudio.Play();
        StartCoroutine(momentOfSilence());
        textPromt.text = null;

    }

    private void error1()
    {
        //play intro4 sounds
        StartCoroutine(momentOfSilence());
        narroratorAudio.clip = Resources.Load<AudioClip>("Audio/7_Convo_Mastered");
        narroratorAudio.Play();
        StartCoroutine(momentOfSilence());
        textPromt.text = null;

    }

    private void error2()
    {
        //play intro4 sounds
        StartCoroutine(momentOfSilence());
        narroratorAudio.clip = Resources.Load<AudioClip>("Audio/7_Convo_Mastered");
        narroratorAudio.Play();
        StartCoroutine(momentOfSilence());
        textPromt.text = null;

    }



    //just need time between actions

    IEnumerator momentOfSilence() {
        print(Time.time);
        yield return new WaitForSeconds(3);
        print(Time.time);
    }
}
