using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private AudioClip _mainMenuAudio;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Time.timeScale = 1f;
        _mainMenuPanel.SetActive(true);
        _settingsPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        _mainMenuPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }

    public void OpenMainMenu()
    {
        _mainMenuPanel.SetActive(true);
        _settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
