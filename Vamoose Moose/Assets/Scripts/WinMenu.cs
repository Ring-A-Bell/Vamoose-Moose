using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{

    public GameObject successMenuUI;
    private bool isCouroutineExecuting = false;

    void Update()
    {
        if(GameObject.Find("Player").GetComponent<Animator>().GetBool("DidWin"))
        {
            StartCoroutine(WaitCoroutine());
        }
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

    IEnumerator WaitCoroutine()
    {
        if(isCouroutineExecuting)
            yield break;
        isCouroutineExecuting = true;
        yield return new WaitForSeconds(1);

        successMenuUI.SetActive(true);
        Time.timeScale = 0f;

        isCouroutineExecuting = false;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HighLevel");
    }
}
