using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] songs;
    private float startTime;
    private float animationDuration = 2.0f;
    private bool isPlaying = false;
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
        // startTime = Time.time;
        Sound s = Array.Find(songs, song => song.name == "faded");
        float[] samples = new float[s.clip.samples * s.clip.channels];
        s.clip.GetData(samples, 0);
        Debug.Log("samples length: " + samples.Length);
        Play("faded");
    }
    void Update() {
    }
    public void Play(string name) {
        Sound s = Array.Find(songs, song => song.name == name);
        if (s == null) {
            return;
        }
        s.source.PlayDelayed(animationDuration);
    }

    public bool IsMusicEnded() {
        Sound s = Array.Find(songs, song => song.name == "faded");
        if (s == null || s.source == null) {
            return false;
        } else if (s.source.isPlaying) {
            return false;
        } else {
            return true;
        }
    }
}
