using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmbienceMixer : MonoBehaviour
{
    public AudioClip[] clips;

    [Header("Audio Source Pool")]
    public int poolSize = 5;

    [Header("Volume")]
    public float minVolume = 0.6f;
    public float maxVolume = 1f;

    [Header("Pitch")]
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    [Header("Delay Between Sounds")]
    public float minDelay = 2f;
    public float maxDelay = 6f;

    private List<AudioSource> audioSources = new List<AudioSource>();

    void Awake()
    {
        // Create pool
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;
            audioSources.Add(source);
        }
    }

    void Start()
    {
        StartCoroutine(PlayRandomAmbience());
    }

    IEnumerator PlayRandomAmbience()
    {
        while (true)
        {
            AudioSource source = GetFreeAudioSource();
            if (source != null && clips.Length > 0)
            {
                AudioClip clip = clips[Random.Range(0, clips.Length)];

                source.clip = clip;
                source.volume = Random.Range(minVolume, maxVolume);
                source.pitch = Random.Range(minPitch, maxPitch);
                source.Play();
            }

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }

    AudioSource GetFreeAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
                return source;
        }

        // All sources busy — optional: return null or steal one
        return null;
    }
}
