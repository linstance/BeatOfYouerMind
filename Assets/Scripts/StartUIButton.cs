using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUIButton : MonoBehaviour
{
    private void LodeScene()
    {
        SceneManager.LoadScene("SongSelectScene");
    }
    
    private void QuitGame()
    {
        Application.Quit();
    }

    public void QuitBtn()
    {
        Invoke("QuitGame", 0.9f);
    }

    public void StartBtn()
    {
        Invoke("LodeScene", 0.9f);
    }

}
