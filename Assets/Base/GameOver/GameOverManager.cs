using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private string _gameSceneName;
    [SerializeField]
    private string _mainMenuSceneName;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Restart()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }
}
