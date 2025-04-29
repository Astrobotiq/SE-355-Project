using System;
using UnityEngine;

public class MusicBeatManager : MonoBehaviour
{
    public static event Action OnBeatDetected;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public int bassBand = 1;
    public float threshold = 1f;
    public float cooldown = 0.3f;

    private float[] spectrum = new float[64];
    private float lastBeatTime;

    void Update()
    {
        if (!audioSource || !audioSource.isPlaying) return;

        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        float bassValue = spectrum[bassBand];
        

        if (bassValue > threshold && Time.time - lastBeatTime >= cooldown)
        {
            OnBeatDetected?.Invoke(); // 🎯 Beat event
            lastBeatTime = Time.time;
        }
    }
}