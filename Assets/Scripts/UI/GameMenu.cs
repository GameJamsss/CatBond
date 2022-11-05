using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _checkList;

    private bool _isOnPause = false;

    void Start()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _checkList.SetActive(false);
    }

    void Update()
    {
        Pause();
        CheckList();
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

    private void CheckList()
    {
        if (Input.GetKey(KeyCode.Tab))
            _checkList.SetActive(true);
        else
            _checkList.SetActive(false);
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
