using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAudio : MonoBehaviour
{
    public AudioSource stepSource;
    public AudioSource citySource;
    public AudioSource effectSource;
    public AudioClip gearClip;
    public AudioClip noMoneyClip;
    public AudioClip ideaClip;
    
    public void PlayCityClip()
    {
        citySource.Play();
    }

    public void StopCityClip()
    {
        citySource.Stop();
    }

    public void PlayStepClip()
    {
        stepSource.Play();
    }

    public void StopStepClip()
    {
        stepSource.Stop();
    }

    public void PlayGearSound()
    {
        effectSource.PlayOneShot(gearClip);
    }

    public void PlayNoMoney()
    {
        effectSource.PlayOneShot(noMoneyClip);
    }

    public void PlayIdeaClip()
    {
        effectSource.PlayOneShot(ideaClip);
    }

    public void StopEffectClip()
    {
        effectSource.Stop();
    }
}
