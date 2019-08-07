using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVisualization : MonoBehaviour {

    private const int SAMPLE_SIZE = 1024;

    public GameObject prefab;

    public float rmsValue; // average power output of the sound
    public float dbValue; // how loud it is
    public float pitchValue; // how treble or bass it is

    public float maxVisualScale = 25.0f;
    public float visualModifier = 50.0f;
    public float smoothSpeed = 10.0f;
    public float keepPercentage = 0.5f;

    private AudioSource source;
    private float[] samples;
    private float[] spectrum;
    private float sampleRate;

    private Transform[] visualList;
    private float[] visualScale;
    private int amnVisual = 64;
    // Start is called before the first frame update

    void Start(){

        source = GetComponent<AudioSource>();
        samples = new float[1024];
        spectrum = new float[1024];
        sampleRate = AudioSettings.outputSampleRate;

        SpawnLine();

    }

    private void SpawnLine() {

        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];

        for (int i = 0; i < amnVisual; i++) {

            GameObject go = Instantiate(prefab);
            visualList[i] = go.transform;
            visualList[i].position = Vector3.right * i;
        }
    }

    // Update is called once per frame
    private void Update() {
        AnaylzeSound();
        UpdateVisual();
    }

    private void UpdateVisual() {

        int visualIndex = 0;
        int spectrumIndex = 0;
        int averageSize = (int)((SAMPLE_SIZE * keepPercentage) / amnVisual);

        while (visualIndex < amnVisual) {

            int j = 0;
            float sum = 0;

            while (j < averageSize) {
                sum += spectrum[spectrumIndex];
                spectrumIndex++;
                j++;
            }

            float scaleY = sum / averageSize * visualModifier;
            visualScale[visualIndex] -= Time.deltaTime * smoothSpeed;

            if (visualScale[visualIndex] < scaleY)
                visualScale[visualIndex] = scaleY;

            if (visualScale[visualIndex] > maxVisualScale)
                visualScale[visualIndex] = maxVisualScale;

            visualList[visualIndex].localScale = Vector3.one + Vector3.up * visualScale[visualIndex];
            visualIndex++;

        }
    }
    private void AnaylzeSound() {

        source.GetOutputData(samples, 0); //sample is being passed as an array. It's going to be modified while in this function

        //Get the RMS

        int i = 0;
        float sum = 0;
        for (; i < SAMPLE_SIZE; i++) {
            sum = samples[i] * samples[i]; //getting the square of sampels
        }

        rmsValue = Mathf.Sqrt(sum / SAMPLE_SIZE);

        //Get the DB Value

        dbValue = 20 * Mathf.Log10(rmsValue / 0.1f); // formula to get db value

        //Get Sound Spectrum
        source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        //Find Pitch Value

        float maxV = 0;
        var maxN = 0;
        for (i = 0; i < SAMPLE_SIZE; i++) {
            if (!(spectrum[i] > maxV) || !(spectrum[i] > 0.0f)) {
                continue;
            }
            maxV = spectrum[i];
            maxN = i;
        }

        float freqN = maxN;
        if (maxN > 0 && maxN < SAMPLE_SIZE - 1) {
            var dL = spectrum[maxN - 1] / spectrum[maxN];
            var dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        pitchValue = freqN * (sampleRate / 2) / SAMPLE_SIZE;
    }
}
