using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Plant : MonoBehaviour
    {
        [SerializeField] private AudioClip _eatClip;
        [SerializeField] private GameObject _cup;

        private bool _isItFull = false;

        private Animator _animator;
        private AudioSource _audioSource;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _cup.SetActive(true);
        }

        private void GetAngry()
        {
            if (_eatClip != null)
            {
                _audioSource.PlayOneShot(_eatClip);
            }

            _animator.SetTrigger("Angry");
        }

        public void FeedPlant()
        {
            _isItFull = true;
        }

        public void Interract()
        {
            if (_isItFull) return;

            GetAngry();
        }

        private void PlayEatSound()
        {
            _audioSource.PlayOneShot(_eatClip);
        }

        private void RemoveCup()
        {
            _cup.SetActive(false);
        }
    }
}
