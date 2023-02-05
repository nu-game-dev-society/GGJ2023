using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioPool : MonoBehaviour
{
    public static AudioPool Instance;
    [SerializeField] private AudioSource AudioSourcePrefab;

    private List<AudioSource> audioSources = new List<AudioSource>();
    private List<AudioSource> active = new List<AudioSource>();
    private void Awake()
    {
        Instance= this;
    }
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            audioSources.Add(Instantiate(AudioSourcePrefab, transform));
        }
    }

    public void PlayClip(AudioClip clip, Transform t, float pitch = 1.0f, float volume = 1.0f)
    {
        StartCoroutine(Play(clip, t, pitch, volume));
    }
    private IEnumerator Play(AudioClip clip, Transform t, float pitch, float volume)
    {
        if (audioSources.Count == 0)
        {
            audioSources.Add(Instantiate(AudioSourcePrefab, transform));
        }

        var a = audioSources[0];
        active.Add(a);
        audioSources.RemoveAt(0);
        a.clip = clip;
        a.pitch = pitch;
        a.volume = volume;
        a.transform.parent = t;
        a.transform.localPosition = Vector3.zero;
        a.Play();

        yield return new WaitForSeconds(clip.length);

        audioSources.Add(a);
        a.transform.parent = transform;
        active.Remove(a);

    }
}
