using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Domain.Objects;

public class MouseToy : MonoBehaviour
{
    private Animator _animator;
    private Interactable _interactable;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _interactable = GetComponent<Interactable>();
    }

    private void Start()
    {
        _interactable.OnInteract += Run;
    }

    private void Run()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Run");
        }
    }

    private void OnDisable()
    {
        _interactable.OnInteract -= Run;
    }
}
