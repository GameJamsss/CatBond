using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip _clickButtonClip;
    [SerializeField] private AudioClip _selectButtonClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        if (_clickButtonClip != null)
            _audioSource.PlayOneShot(_clickButtonClip);
    }

    public void PlaySelectSound()
    {
        if (_selectButtonClip != null)
            _audioSource.PlayOneShot(_selectButtonClip);
    }
}
