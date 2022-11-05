using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _speed = 5.0f;

        [Header("Sounds")]
        [SerializeField] private AudioClip[] _walkClips;

        private CharState State
        {
            get { return (CharState)_animator.GetInteger("State"); }
            set { _animator.SetInteger("State", (int)value); }
        }

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private AudioSource _audioSource;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (Input.GetButton("Horizontal")) Run();
            else
            {
                _animator.SetBool("IsWalking", false);
                State = CharState.Idle;
            }
        }

        private void Run()
        {
            _animator.SetBool("IsWalking", true);

            Vector3 direction = transform.right * Input.GetAxis("Horizontal");
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);

            Vector3 characterScale = transform.localScale;
            if (Input.GetAxis("Horizontal") < 0)
            {
                characterScale.x = -1;

            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                characterScale.x = 1;
            }

            transform.localScale = characterScale;
        }

        //called from animation trigger
        private void PlayWalkSound()
        {
            _audioSource.PlayOneShot(_walkClips[Random.Range(0, _walkClips.Length)]);
        }
    }

    public enum CharState
    {
        Idle,
        Run,
        Jump
    }
}