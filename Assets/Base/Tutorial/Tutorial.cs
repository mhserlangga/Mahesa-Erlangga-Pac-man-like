using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private string _mainMenuSceneName;
    [SerializeField]
    private string _gameSceneName;

    public void MainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_gameSceneName);
    }
}
