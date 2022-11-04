using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private bool _isOnPause = false;

    void Start()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
    }

    void Update()
    {
        Pause();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isOnPause)
        {
            _isOnPause = true;
            _pauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isOnPause)
        {
            Continue();
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        _isOnPause = false;
        _pauseMenu.SetActive(false);
    }
}
