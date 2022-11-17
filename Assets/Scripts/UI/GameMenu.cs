using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [Space(5f), Header("Check List")]
    [SerializeField] private GameObject _checkList;
    [SerializeField] private GameObject _checkListRing;

    private bool _isOnPause = false;

    void Start()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _checkList.SetActive(false);
        _checkListRing.SetActive(false);
    }

    void Update()
    {
        Pause();
        CheckList();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetGameOnPause();
        }
    }

    public void SetGameOnPause()
    {
        if (!_isOnPause)
        {
            _isOnPause = true;
            _pauseMenu.SetActive(true);
        }
        else
        {
            Continue();
        }
    }

    private void CheckList()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            _checkList.SetActive(true);
            _checkListRing.SetActive(true);
        }
        else
        {
            _checkList.SetActive(false);
            _checkListRing.SetActive(false);
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
