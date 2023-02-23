using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomationDoor : MonoBehaviour
{
    public bool playerTrigger = false;
    public bool customerTrigger = false;
    private bool citySoundIsPlaying = false;

    private AudioSource audioSource;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        tag = "AutoDoor";
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.volume = 0;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerTrigger || customerTrigger)
        {
            animator.SetBool("isOpen", true);
            if (playerTrigger)
            {
                if (!citySoundIsPlaying)
                {
                    audioSource.volume = PlayerPrefs.GetFloat("music");
                    citySoundIsPlaying = true;
                }
            }
        }
        else
        {
            animator.SetBool("isOpen", false);
            citySoundIsPlaying = false;
            audioSource.volume = 0;
        }
    }
}
