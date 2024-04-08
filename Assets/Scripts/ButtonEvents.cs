using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    public void StartButton()
    {
        LevelManager.levelCount++;

        SceneManager.LoadScene(LevelManager.levelCount);
    }
    public void NextLevelButton()
    {
        LevelManager.levelCount++;
        Debug.Log("Loading next level: " + LevelManager.levelCount);
        SceneManager.LoadScene(LevelManager.levelCount);
    }
    public void RestartButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
