using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    AudioSource playerAS, environmentAS;

    [SerializeField]
    AudioClip[] clips;

    public static AudioController instance;

    bool playerSteps;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        PlayAudioRandom(10);
    }

    public void TriggerPlayerAS(bool activate)
    {
        if (activate)
        {
            if(!playerAS.isPlaying)
            playerAS.Play();
        }
        else
        {
            playerAS.Stop();
        }
    }

    void PlayAudioRandom(float delay)
    {
        StartCoroutine("PlayAudioRandom_CO", delay);
    }

    IEnumerator PlayAudioRandom_CO(float delay)
    {
        yield return new WaitForSeconds(delay);

        environmentAS.PlayOneShot(clips[0]);
        environmentAS.PlayOneShot(clips[1]);

        float nextCall = Random.Range(30, 90);

        PlayAudioRandom(nextCall);
    }
}
