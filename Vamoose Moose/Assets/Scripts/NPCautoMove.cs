using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCautoMove : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;

    private bool isWalking;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;
    public Animator animator;

    private AudioSource audioSource;

    private int walkDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if(isWalking)
        {
            walkCounter -= Time.deltaTime;

            if(walkCounter<0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }

            switch (walkDirection)
            {
                case 0: {
                    rb.velocity = new Vector2(0, moveSpeed);
                    //movement.y = 1;
                    animator.SetFloat("Vertical", 1f);
                    animator.SetFloat("Horizontal", 0f);
                    animator.SetFloat("Speed", 1f);
                }
                break;

                case 1: {
                    rb.velocity = new Vector2(moveSpeed, 0);
                    //movement.x = 1;
                    animator.SetFloat("Horizontal", 1f);
                    animator.SetFloat("Vertical", 0f);
                    animator.SetFloat("Speed", 1f);
                }
                break;

                case 2: {
                    rb.velocity = new Vector2(0, -moveSpeed);
                    //movement.y = -1;
                    animator.SetFloat("Vertical", -1f);
                    animator.SetFloat("Horizontal", 0f);
                    animator.SetFloat("Speed", 1f);
                }
                break;

                case 3: {
                    rb.velocity = new Vector2(-moveSpeed, 0);
                    //movement.x = -1;
                    animator.SetFloat("Horizontal", -1f);
                    animator.SetFloat("Vertical", 0f);
                    animator.SetFloat("Speed", 1f);
                }
                break;
            }
        }

        else
        {
            if(!audioSource.isPlaying)
            {
                float dist = Vector3.Distance(rb.position, GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().position);
                if(GameObject.FindWithTag("Player").GetComponent<Animator>().GetBool("WithinRange"))
                {
                    audioSource.Stop();
                }
                else if(dist<20f)
                {
                    audioSource.volume = dist*0.01f;
                    audioSource.Play();
                }
                else
                    audioSource.Stop();
            }
            
            waitCounter -= Time.deltaTime;

            rb.velocity = Vector2.zero;

            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Speed", 0f);

            if(waitCounter<0)
            {
                ChangeDirection();
            }
        }
    }

    void ChangeDirection()
    {
        isWalking = true;
        walkDirection = Random.Range(0,4);
        walkCounter = walkTime;
    }
}
