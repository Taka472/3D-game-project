using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneSoundControl : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    public void PlayNoticeSound()
    {
        audioSource.PlayOneShot(clip);
    }
}
