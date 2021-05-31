using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    public Animator animator;

    private AudioSource audioSource;
    public AudioClip deathClip;

    private bool audioShouldBeExecuted = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        audioSource = GetComponent<AudioSource>();
        if(collider.gameObject.CompareTag("Player"))
        {
            animator.SetBool("ShouldAttack", true);
            audioSource.Play();
            collider.gameObject.GetComponent<Animator>().SetBool("WithinRange", true);
            audioShouldBeExecuted = true;
        }
    }

    void Update()
    {
        while(audioShouldBeExecuted && !audioSource.isPlaying)
            {
                audioSource.clip = deathClip;
                audioSource.volume = 0.8f;
                audioSource.Play();
                audioShouldBeExecuted = false;
            }
    }
}
