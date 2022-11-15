using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Plant : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        private bool _isItFull = false;

        private Animator _animator;
        private AudioSource _audioSource;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void GetAngry()
        {
            if (_clip != null)
            {
                _audioSource.PlayOneShot(_clip);
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
    }
}
