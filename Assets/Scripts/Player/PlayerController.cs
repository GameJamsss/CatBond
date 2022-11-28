using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _speed = 5.0f;

        [Header("Sounds")]
        [SerializeField] private AudioClip[] _walkClips;

        void Start()
        {
            Queue<Line> lines = new();
            lines.Enqueue(new Line("Босс: О, господин Котовски, в кой это световом году вовремя! Я даже немного волнуюсь, все хорошо?"));
            lines.Enqueue(new Line("Котовски: Да, просто попробовал встать вовремя и это сработало."));
            lines.Enqueue(new Line("Б: Неужеили? Ну хорошо, продолжаейте в том же духе. А сегодня у меня для вас необычное задание: нужно отыскать все предметы в этом списке. Помогать я вам, конечно же, не буду."));
            lines.Enqueue(new Line("К: Принял."));
            lines.Enqueue(new Line("Б: А еще ваш цветочек взбунтовался, разберитесь с этим.."));
            lines.Enqueue(new Line("К: Ну еще бы."));
            lines.Enqueue(new Line("Б: И последнее..."));
            lines.Enqueue(new Line("К: Что же?"));
            lines.Enqueue(new Line("Б: *Шепотом* Не доверяйте повару. "));
            FindObjectOfType<DialogManager>().StartDialog(new Dialog(lines));
        }

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