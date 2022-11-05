using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToy : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Run");
        }
    }
}
