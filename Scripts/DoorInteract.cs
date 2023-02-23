using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    private Animator animator;
    public Transform player;
    public AudioClip open;
    public AudioClip close;
    private AudioSource audioSource;

    private bool isOpen = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenClose()
    {
        if (!isOpen)
        {
            audioSource.clip = open;
            audioSource.volume = 1;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = close;
            audioSource.volume = 0.5f;
            audioSource.Play();
        }
        animator.SetBool("isOpen", !isOpen);
        isOpen = !isOpen;
    }
}
