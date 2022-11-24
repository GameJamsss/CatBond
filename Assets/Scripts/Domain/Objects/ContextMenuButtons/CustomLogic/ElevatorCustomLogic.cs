using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

namespace Assets.Scripts.Domain.Objects.ContextMenuButtons.CustomLogic
{
    public class ElevatorCustomLogic : MonoBehaviour, ICustomLogic
    {
        [SerializeField] private int _id;
        [SerializeField] private float _speed;

        [Header("Positions")]
        [SerializeField] private Transform _from;
        [SerializeField] private Transform _to;
        private Transform _goTo;

        [Header("Audio")]
        [SerializeField] private AudioClip _moving;
        [SerializeField] private AudioClip _startMoving;
        [SerializeField] private AudioClip _stopped;
        [SerializeField] private AudioClip _error;

        private AudioSource _audioSource;
        private Coroutine _coroutine;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            transform.position = _from.position;
            _goTo = _to;
        }

        public int GetId()
        {
           return _id;
        }

        public void Apply()
        {
            if (_moving)
            {
                _audioSource.loop = true;
                _audioSource.clip = _moving;
                _audioSource.Play();
            }
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(StartSequence());
        }

        IEnumerator StartSequence()
        {
            while (Vector2.Distance(transform.position, _goTo.position) > 0.02f)
            {
                transform.position = Vector2.MoveTowards(transform.position, _goTo.position, _speed * Time.deltaTime);
                yield return null;
            }

            _goTo = _goTo != _from ? _from : _to;

            if (_stopped)
            {
                _audioSource.loop = false;
                _audioSource.clip = _stopped;
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }          
        }
    }
}