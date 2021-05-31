using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{

    public GameObject KilledUI;
    public GameObject TimeOutUI;
    private bool isCouroutineExecuting = false;

    void Update()
    {
        if(GameObject.Find("Player").GetComponent<PlayerMovement>().energy<=0)
        {
            StartCoroutine(WaitCoroutine(1));
        }
        else if(GameObject.Find("Player").GetComponent<Animator>().GetBool("WithinRange"))
        {
            StartCoroutine(WaitCoroutine(2));
        }
    }


    IEnumerator WaitCoroutine(int option)
    {
        if(isCouroutineExecuting)
            yield break;
        isCouroutineExecuting = true;
        yield return new WaitForSeconds(2);

        if(option==1)
        {
            TimeOutUI.SetActive(true);
            Time.timeScale = 0f;
        }

        else if(option==2)
        {
            KilledUI.SetActive(true);
            Time.timeScale = 0f;
        }

        isCouroutineExecuting = false;
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void QuitGame()
    {
        Debug.Log("QUITTING!");
        Application.Quit();
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HighLevel");
    }
}