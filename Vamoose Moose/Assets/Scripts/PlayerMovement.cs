using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    public Animator animator;
    private Text txt;

    public float energy = 100f;
    private bool isCouroutineExecuting = false;
    private bool GameIsPaused = true;

    private AudioSource audioSource;
    //private bool audioplaying = false;
    public AudioClip deathClip;

    // Start is called at the beginning
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        GameIsPaused = !GameIsPaused;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!GameIsPaused)
        {
            if(animator.GetBool("WithinRange"))
            {
                //rb.bodyType = Static;
                rb.simulated = false;
            }
            
            // Input 
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.magnitude);

            //Decrease Energy
            if(movement.x != 0 || movement.y != 0 && !isCouroutineExecuting)
                StartCoroutine(WaitCoroutine(true));
            if(movement.x == 0 && movement.y == 0 && !isCouroutineExecuting)
                StartCoroutine(WaitCoroutine(false));
            //Debug.Log(energy);
        }
    }

    void FixedUpdate()
    {        
        if(!audioSource.isPlaying && animator.GetFloat("Speed")>0.01f)
            audioSource.Play();
        if(animator.GetFloat("Speed")<0.01f || animator.GetBool("WithinRange") || animator.GetBool("DidWin"))
            audioSource.Stop();

        //Check if timer has run out
        if(energy<=0)
            animator.SetBool("WithinRange",true);
        
        // Movement
        if(!animator.GetBool("WithinRange") && !animator.GetBool("DidWin"))        
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        UpdateEnergyText();
    }

    IEnumerator WaitCoroutine(bool isMoving)
    {
        if(isCouroutineExecuting)
            yield break;
        isCouroutineExecuting = true;
        yield return new WaitForSeconds(1);
        if(energy<=0)
            energy=0;
        else if(isMoving)
            energy -= 5f;
        else
            energy -= 1f;
        isCouroutineExecuting = false;
    }

    public void UpdateEnergyText()
    {
        txt = GameObject.Find("EnergyStat").GetComponent<Text>();
        txt.text = "Energy = " + energy.ToString();
    }

    public void AddEnergy()
    {
        energy = energy + 50f > 100f ? 100f : energy + 50f;
    }
}