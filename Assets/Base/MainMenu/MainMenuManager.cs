using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string _tutorSceneName;

    public void Play()
    {
        SceneManager.LoadScene(_tutorSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
