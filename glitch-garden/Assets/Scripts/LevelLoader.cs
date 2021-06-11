using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float splashSeconds = 3f;
    int currentSceneIndex;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            AutoLoadFromSplashScreen();
        }
    }

    IEnumerator WaitOnSplashScreenThenLoad()
    {
        yield return new WaitForSeconds(splashSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void LoadYouLoseScene()
    {
        SceneManager.LoadScene("Lose Screen");
    }
    public void LoadNextScene()
    {              
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Screen");
    }
    public void LoadOptionsScreen()
    {
        
        SceneManager.LoadScene("Options Screen");
    }
    public void AutoLoadFromSplashScreen()
    {
        StartCoroutine(WaitOnSplashScreenThenLoad());
    }
}
