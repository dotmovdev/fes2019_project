using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundEffectsControl : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip spawnSound;
    public AudioClip SpawnSound
    {
        get
        {
            return spawnSound;
        }
    }
    [SerializeField]
    private AudioClip spawnBurstSound;
    public AudioClip SpawnBurstSound
    {
        get
        {
            return spawnBurstSound;
        }
    }
    [SerializeField]
    private AudioClip starBurstSound;
    public AudioClip StarBurstSound
    {
        get
        {
            return starBurstSound;
        }
    }
    [SerializeField]
    private AudioClip starBurstImpactSound;
    public AudioClip StarBurstImpactSound
    {
        get
        {
            return starBurstImpactSound;
        }
    }

    [Header("Parameters")]
    [SerializeField]
    private float volume = 0.65f;

    public void PlayOneShot(AudioClip audioClip, float fadeOutDuration)
    {
        GameObject audioSourceObject = new GameObject("AudioSource");
        audioSourceObject.transform.parent = this.transform;
        AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();

        //音量
        audioSource.volume = volume;
        audioSource.PlayOneShot(audioClip);

        //Tweenで音量調整
        var audioTween = DOTween.To(
            () => volume,
            (v) => audioSource.volume = v,
            0.0f,
            fadeOutDuration);

        audioTween.onComplete = () =>
        {
            StartCoroutine(removeAudioSource(audioSourceObject, () => { }));
        };
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        GameObject audioSourceObject = new GameObject("AudioSource");
        audioSourceObject.transform.parent = this.transform;
        AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();

        //音量
        audioSource.volume = volume;
        audioSource.PlayOneShot(audioClip);

        StartCoroutine(removeAudioSource(audioSourceObject, () => { }));
    }

    private IEnumerator removeAudioSource(GameObject gameObject, System.Action onComplete)
    {
        var audioSourceRef = gameObject.GetComponent<AudioSource>();
        if (!audioSourceRef.isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        
    }
}
