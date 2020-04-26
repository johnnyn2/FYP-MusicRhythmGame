using System;
using UnityEngine;
using System.Numerics;
using System.Collections.Generic;
using DSPLib;
using System.IO;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

public class SoundManager : MonoBehaviour
{
    public Sound[] songs;
    private float timer;
    private float animationDuration = 2.5f;

    private SpectralFluxAnalyzer preProcessedSpectralFluxAnalyzer;
    public List<SpectralFluxInfo> peakOfPeakSamples;
    private float samplingRate;
    private int channels;
    private int numOfSamples;
    private float clipLength;
    private float[] samples;
    public StatusContainer statusContainer;
    private bool hasSetCoins = false;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in songs) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }   
    }
    
    void Start() {
        AudioListener.pause = false;
        timer = 0f;
        peakOfPeakSamples = new List<SpectralFluxInfo>();
        if (PlayerPrefs.GetString("mode") == "beat") {
            BpmAnalysis();
            InitializeMinions();
            Play(PlayerPrefs.GetString("selectedSong"));
        } else if (PlayerPrefs.GetString("mode") == "onset") {
            ReadOnsetJson();
            InitializeMinions();
            Play(PlayerPrefs.GetString("selectedSong"));
        }

        // This is to output the json recording of onset data. Do this when you added a new song.
        // SpectralFluxAnalysis();
    }
    void Update() {
        timer += Time.deltaTime;
        Sound s = Array.Find(songs, song => song.name == PlayerPrefs.GetString("selectedSong"));
        if (timer > (animationDuration + s.clip.length)) {
            statusContainer.ShowStatus();
        }
        if (timer > (animationDuration + s.clip.length + 5.0f)) {
            statusContainer.HideStatus();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().Dead();
            if (PlayerPrefs.HasKey("Coins") && !hasSetCoins) {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100);
                hasSetCoins = true;
            } else if (!hasSetCoins) {
                PlayerPrefs.SetInt("Coins",100);
                hasSetCoins = true;
            }
        }
    }

    private void BpmAnalysis() {
        Sound s = Array.Find(songs, song => song.name == PlayerPrefs.GetString("selectedSong"));
        int bpm = GetBpm(PlayerPrefs.GetString("selectedSong"));
        Debug.Log("BPM: " + bpm);
        float totalNumOfBeats = s.clip.length / 60 * bpm;
        Debug.Log("totalNumOfBeats: " + totalNumOfBeats);
        float interval = s.clip.length / totalNumOfBeats;
        Debug.Log("interval: " + interval);
        for(float i=1.0f;i<=totalNumOfBeats - 5.0f;i+=1.0f) {
            SpectralFluxInfo info = new SpectralFluxInfo();
            info.time = interval * i;
            Debug.Log("Time interval: " + info.time);
            peakOfPeakSamples.Add(info);
        }
    }

    private int GetBpm(String song) {
        switch(song) {
            case "faded":
                return 90;
            case "theFatRat":
                return 90;
            case "kimetsunoYaiba":
                return 135;
            case "nonono":
                return 106;
            default: return 90;
        }
    }

    private void SpectralFluxAnalysis() {
        Sound s = Array.Find(songs, song => song.name == PlayerPrefs.GetString("selectedSong"));
        preProcessedSpectralFluxAnalyzer = new SpectralFluxAnalyzer();
        samplingRate = s.source.clip.frequency;
        channels = s.source.clip.channels;
        numOfSamples = s.source.clip.samples;
        clipLength = s.source.clip.length;
        Debug.Log("clip length: " + clipLength);
        samples = new float[s.source.clip.samples * s.source.clip.channels];
        // the sample data is stored as [L, R, L, R, L, R, ...] in the array
        s.source.clip.GetData(samples, 0);
        GetSpectrumData(samples);

        Debug.Log("samples length: " + samples.Length);
        Debug.Log("flux samples length: " + preProcessedSpectralFluxAnalyzer.spectralFluxSamples.Count);
        List<SpectralFluxInfo> peakSamples =  preProcessedSpectralFluxAnalyzer.spectralFluxSamples.FindAll(IsPeakSample);
        Debug.Log("Peak Samples Length: " + peakSamples.Count);
        GetPeakOfPeakSamples(peakSamples);
        Debug.Log("Peak of peak samples length: " + peakOfPeakSamples.Count);
        OutputOnsetJson();
    } 

    private bool IsPeakSample(SpectralFluxInfo info) {
        if (info.isPeak) {
            return true;
        } else {
            return false;
        }
    }

    private void GetPeakOfPeakSamples(List<SpectralFluxInfo> peakSamples) {
        for(int i=0;i<peakSamples.Count;i++) {
            // Debug.Log(String.Format("Spectral flux: {0}, threshold: {1}, prunedSpectralFlux: {2}, time: {3}",
            //     peakSamples[i].spectralFlux,
            //     peakSamples[i].threshold,
            //     peakSamples[i].prunedSpectralFlux,
            //     peakSamples[i].time
            // ));
            // Find the main beat from a set of beats
            if (i==0) {
                if (peakSamples[0].prunedSpectralFlux > peakSamples[1].prunedSpectralFlux) {
                    peakOfPeakSamples.Add(peakSamples[i]);
                }
            } else if (i== peakSamples.Count-1) {
                if (peakSamples[i].prunedSpectralFlux > peakSamples[i-1].prunedSpectralFlux) {
                    peakOfPeakSamples.Add(peakSamples[i]);
                }
            } else {
                if (peakSamples[i-1].prunedSpectralFlux < peakSamples[i].prunedSpectralFlux && peakSamples[i+1].prunedSpectralFlux < peakSamples[i].prunedSpectralFlux) {
                    peakOfPeakSamples.Add(peakSamples[i]);
                }
            }
        }
    }

    private void InitializeMinions() {
        GameObject minionManager = GameObject.Find("MinionManager");
        for(int i=0;i<21;i++) {
             minionManager.GetComponent<MinionManager>().SpawnMinion(peakOfPeakSamples[i].time);
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(songs, song => song.name == name);
        if (s == null) {
            return;
        }
        s.source.PlayDelayed(animationDuration);
    }

    // return data that is of the same format as what’s returned in real-time by
    // AudioSource.GetOutputData, but it contains samples from the beginning of the track until
    // the end instead of just the samples for the currently playing audio.
    private float[] GetOutputData(float[] samples) {
        float[] preprocessedSamples = new float[numOfSamples];

        int numProcessed = 0;
        float combinedChannelAvg = 0f;
        for (int i=0; i<samples.Length; i++) {
            combinedChannelAvg += samples[i];

            // Each time we have processed all channels samples for a point in time, we will store the average of the channels combined
            if ((i + 1) % channels == 0 ) {
                preprocessedSamples[numProcessed] = combinedChannelAvg / channels;
                numProcessed++;
                combinedChannelAvg = 0f;
            }
        }

        return preprocessedSamples;
    }

    private void GetSpectrumData(float[] samples) {
        try{
            float[] preProcessedSamples = GetOutputData(samples);

            Debug.Log("Combine Channels done");
            Debug.Log(preProcessedSamples.Length);

            // Once we have our audio sample data prepared, we can execute an FFT to return the spectrum data over the time domain
            int spectrumSampleSize = 1024;
            int iterations = preProcessedSamples.Length / spectrumSampleSize;

            FFT fft = new FFT();
            fft.Initialize ((UInt32)spectrumSampleSize);
            
            Debug.Log (string.Format("Processing {0} time domain samples for FFT", iterations));
            double[] sampleChunk = new double[spectrumSampleSize];
            for(int i=0;i<iterations;i++) {
                // Grab the current 1024 chunk of audio sample data
                Array.Copy (preProcessedSamples, i * spectrumSampleSize, sampleChunk, 0, spectrumSampleSize);

                // Apply our chosen FFT Window
                double[] windowCoefs = DSP.Window.Coefficients (DSP.Window.Type.Hanning, (uint)spectrumSampleSize);
                double[] scaledSpectrumChunk = DSP.Math.Multiply (sampleChunk, windowCoefs);
                double scaleFactor = DSP.Window.ScaleFactor.Signal (windowCoefs);

                // Perform the FFT and convert output (complex numbers) to Magnitude
                Complex[] fftSpectrum = fft.Execute (scaledSpectrumChunk);
                double[] scaledFFTSpectrum = DSPLib.DSP.ConvertComplex.ToMagnitude (fftSpectrum);
                scaledFFTSpectrum = DSP.Math.Multiply (scaledFFTSpectrum, scaleFactor);

                // These 1024 magnitude values correspond (roughly) to a single point in the audio timeline
                float curSongTime = getTimeFromIndex(i) * spectrumSampleSize;

                // Send our magnitude data off to our Spectral Flux Analyzer to be analyzed for peaks
                preProcessedSpectralFluxAnalyzer.analyzeSpectrum (Array.ConvertAll (scaledFFTSpectrum, x => (float)x), curSongTime);

                Debug.Log ("Spectrum Analysis done");
                Debug.Log ("Background Thread Completed");
            }
        } catch (Exception e) {
            Debug.Log(e.ToString());
        }
    }

    public int getIndexFromTime(float curTime)
    {
        float lengthPerSample = this.clipLength / (float)numOfSamples;

        return Mathf.FloorToInt(curTime / lengthPerSample);
    }

    public float getTimeFromIndex(int index)
    {
        return ((1f / (float)samplingRate) * index);
    }

    private void OutputOnsetJson() {
        SpectralFluxInfo[] info = peakOfPeakSamples.ToArray();
        string peakOfPeakSamplesJson = JsonHelper.ToJson(info);
        string path = Application.dataPath + "/Resources/Json/" + PlayerPrefs.GetString("selectedSong") + ".json";
        File.WriteAllText(path, peakOfPeakSamplesJson);
        Debug.Log("Output is done!");
    }

    private void ReadOnsetJson() {
        // string path = Application.dataPath + "/Json/" + PlayerPrefs.GetString("selectedSong") + ".json";
        // string json = File.ReadAllText(path);
        string path = "Json/" + PlayerPrefs.GetString("selectedSong");
        string json = Resources.Load<TextAsset>(path).ToString();
        SpectralFluxInfo[] info = JsonHelper.FromJson<SpectralFluxInfo>(json);
        for(int i=0;i<info.Length;i++) {
            peakOfPeakSamples.Add(info[i]);
        }
    }
}
