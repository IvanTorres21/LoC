using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public bool newGame = true;

    public static MainMenuController instance;

    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void StartNewGame()
    {
        newGame = true;
        SceneManager.LoadScene(1);
    }

    public void LoadOldGame()
    {
        newGame = false;
        SceneManager.LoadScene(1);
    }
}
