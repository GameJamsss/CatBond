using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Domain.Objects;

public class Checkeble : MonoBehaviour
{
    [SerializeField] private int _id = 0;
    [SerializeField] private AudioClip _clip;

    private AudioSource _source;
    private Interactable _interactable;

    private bool _isMarked;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _interactable = GetComponent<Interactable>();
    }

    private void Start()
    {
        _interactable.OnInteract += Mark;
    }

    private void Mark()
    {
        if (_source != null && _clip != null)
            _source.PlayOneShot(_clip);

        if (!_isMarked)
        {
            FindObjectOfType<CheckListManager>().MarkItem(_id);
            _isMarked = true;
        }
    }

    private void OnDisable()
    {
        _interactable.OnInteract -= Mark;
    }
}
