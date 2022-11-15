using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Domain.Objects;

public class BellRign : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;

    private AudioSource _audioSource;
    private Interactable _interactable;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _interactable = GetComponent<Interactable>();
    }

    private void Start()
    {
        _interactable.OnInteract += PlaySound;
    }

    private void PlaySound()
    {
        _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)]);
    }

    private void OnDisable()
    {
        _interactable.OnInteract -= PlaySound;
    }
}
