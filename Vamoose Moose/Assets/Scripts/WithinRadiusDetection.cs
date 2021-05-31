using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithinRadiusDetection : MonoBehaviour
{
    private bool hasPlayerCollided = false;

    public Animator animator;

    private Animator playerAnimator;

    public float leftPatrol, rightPatrol;

    public float moveSpeed = 2f;
    private bool dirRight = true;
    private bool isCouroutineExecuting = false;
    private bool reachedEnd = false;
    private bool hasFlipped = false;

    private AudioSource audioSource;
    private bool audioShouldBeExecuted = false;
    public AudioClip deathClip;
    

    // Start is called at the beginning
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {       
        while(audioShouldBeExecuted && !audioSource.isPlaying)
            {
                audioSource.clip = deathClip;
                audioSource.volume = 0.8f;
                audioSource.Play();
                audioShouldBeExecuted = false;
            }
        
        if(hasPlayerCollided)
        {
            //Debug.Log("collision");
            return;
        }

        if(dirRight)
        {
            if(!reachedEnd)
            {   
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                isCouroutineExecuting = false;
                animator.SetInteger("MoveDirection", 1);
            }
            else
            {
                transform.Translate(Vector2.zero);
                //animator.SetInteger("MoveDirection", 0);
            }
        }

        else
        {
            if(!reachedEnd)
            {
                transform.Translate(-Vector2.right * moveSpeed * Time.deltaTime);
                isCouroutineExecuting = false;
                animator.SetInteger("MoveDirection", -1);
            }
            else
            {
                transform.Translate(Vector2.zero);
                //animator.SetInteger("MoveDirection", 0);
            }
        }

        if(transform.position.x > leftPatrol && transform.position.x < rightPatrol)
        {
            hasFlipped = false;
        }

        if(transform.position.x > rightPatrol && !reachedEnd && !hasFlipped)
        {
            dirRight = false;
            reachedEnd = true;
            StartCoroutine(WaitCoroutine(true, 0.2f));
        }

        if(transform.position.x < leftPatrol && !reachedEnd && !hasFlipped)
        {   
            dirRight = true;
            reachedEnd = true;
            StartCoroutine(WaitCoroutine(true, 0.2f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            hasPlayerCollided = true;
            Debug.Log("Player has Collided");

            animator.SetBool("ShouldAttack",true);
            playerAnimator = collider.gameObject.GetComponent<Animator>();

            StartCoroutine(WaitCoroutine(false, 1f));
            audioShouldBeExecuted = true;
            
        }
    }

    private void Flip()
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        float temp = gameObject.GetComponent<CircleCollider2D>().offset.x;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(temp*-1,0);
        hasFlipped = true;
    }

    IEnumerator WaitCoroutine(bool toFlip, float secs)
    {
        if(isCouroutineExecuting)
            yield break;
        isCouroutineExecuting = true;
        yield return new WaitForSeconds(secs);
        if(toFlip)
        {
            Flip();
            reachedEnd = false;
        }
        else
        {
            playerAnimator.SetBool("WithinRange", true);
        }
        isCouroutineExecuting = false;
    }
}