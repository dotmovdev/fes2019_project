using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoopMusicPlayer : MonoBehaviour
{
    [SerializeField] 
    private AudioClip audioClip;
    [SerializeField]
    private bool playOnAwake = false;
    [SerializeField]
    private float volume;
    [SerializeField]
    private float loopStartSec;
    [SerializeField]
    private float loopEndSec;
    [SerializeField]
    private float fadeSec;

    private int playingIndex = 0;
    private bool isFading = false;

    AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        loopStartSec = Mathf.Clamp(loopStartSec, 0.0f, audioClip.length);
        loopEndSec = Mathf.Clamp(loopEndSec, 0.0f, audioClip.length);

        if (loopEndSec < loopStartSec)
        {
            loopEndSec = loopStartSec;
        }
        if (fadeSec > loopEndSec - loopStartSec)
        {
            fadeSec = loopEndSec - loopStartSec;
        }


        audioSources = new AudioSource[2];
        for (int i = 0; i < 2; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();

            audioSources[i].clip = audioClip;
            audioSources[i].loop = false;
            audioSources[i].playOnAwake = false;
            audioSources[i].spatialBlend = 0.95f;
            audioSources[i].maxDistance = 1.01f;
        }
        
        if (playOnAwake)
        {
            Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSources[playingIndex].time >= loopEndSec - fadeSec && !isFading)
        {
            int nextIndex = (playingIndex + 1) % audioSources.Length;
            audioSources[playingIndex].DOFade(0.0f, fadeSec)
                .OnComplete(() =>
                {
                    audioSources[playingIndex].Stop();
                    isFading = false;
                    playingIndex = nextIndex;
                });
            audioSources[nextIndex].volume = 0.0f;
            audioSources[nextIndex].time = loopStartSec;
            audioSources[nextIndex].Play();
            audioSources[nextIndex].DOFade(volume, fadeSec);
            isFading = true;
        }
    }

    public void Play()
    {
        Stop();

        int index = 0;
        foreach (var audioSource in audioSources)
        {
            if (audioSource == null) continue;
            if (audioSource.isPlaying) continue;

            audioSource.volume = volume;
            audioSource.Play();

            playingIndex = index;

            index++;

            break;
        }

    }

    public void FadeIn()
    {
        Stop();

        int index = 0;
        foreach (var audioSource in audioSources)
        {
            if (audioSource == null) continue;

            isFading = true;

            audioSource.volume = 0.0f;
            audioSource.Play();

            audioSource.DOFade(volume, fadeSec)
                .OnComplete(() =>
                {
                    isFading = false;
                });

            playingIndex = index;

            index++;

            break;
        }
    }

    public void Stop()
    {
        foreach(var audioSource in audioSources)
        {
            if (audioSource == null) continue;
            audioSource.DOKill();

            audioSource.Stop();
        }
    }

    public void FadeOut()
    {
        foreach (var audioSource in audioSources)
        {
            if (audioSource == null) continue;
            audioSource.DOKill();
            isFading = true;
            audioSource.DOFade(0.0f, fadeSec)
                .OnComplete(() =>
                {
                    audioSource.Stop();
                    isFading = false;
                });
        }
    }
}
