using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioPool : MonoBehaviour
{
    public static AudioPool Instance;
    [SerializeField] private AudioSource AudioSourcePrefab;

    private Dictionary<AudioSource, bool> audioSources = new();
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            audioSources.Add(Instantiate(AudioSourcePrefab, transform), false);
        }
    }

    public void PlayClip(AudioClip clip, Transform t, float pitch = 1.0f, float volume = 1.0f)
    {
        StartCoroutine(Play(clip, t, pitch, volume));
    }
    private IEnumerator Play(AudioClip clip, Transform t, float pitch, float volume)
    {
        var found = audioSources.FirstOrDefault(v => v.Value == false);
        var a = found.Key;
        if (a == default)
        {
            a = Instantiate(AudioSourcePrefab, transform);
            audioSources.Add(a, true);
        }
        else
            audioSources[a] = true;

        a.clip = clip;
        a.pitch = pitch;
        a.volume = volume;
        a.transform.position = t.position;
        a.Play();

        yield return new WaitForSeconds(clip.length);

        audioSources[a] = false;

    }

}
