using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public static AudioControl instance;

    public AudioSource[] music;
    public AudioSource[] sfx;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for (int i = 0; i < music.Length; i++)
        {
            music[i].volume = PlayerPrefs.GetFloat("music");
        }

        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].volume = PlayerPrefs.GetFloat("sfx");
        }
    }

    public void VolumeChange()
    {
        for (int i = 0; i < music.Length; i++)
        {
            music[i].volume = PlayerPrefs.GetFloat("music");
        }

        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].volume = PlayerPrefs.GetFloat("sfx");
        }
    }
}
