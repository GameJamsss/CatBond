using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellRign : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)]);
    }
}
