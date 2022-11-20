using System.Collections;
using UnityEngine;
using Assets.Scripts.Domain.State;

namespace Assets.Scripts
{
    public class MovablePlatform : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private int startPoint;
        [SerializeField] private Transform[] points;

        [SerializeField] private float _waitTime = 2f;

        [SerializeField] private GameObject _fullSprite;
        [SerializeField] private GameObject _emptySprite;

        private bool _isTankFull = false;
        private bool _canMove = false;

        [Header("Audio")]
        [SerializeField] private AudioClip _working;
        [SerializeField] private AudioClip _stops;
        [SerializeField] private AudioClip _error;
        [SerializeField] private AudioClip _toGo;

        private StateManager _stateManager;

        private int i;
        private AudioSource _audioSource;

        private void Awake()
        {
            _stateManager = GetComponent<StateManager>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audioSource.Play();
            _fullSprite.SetActive(true);
            _emptySprite.SetActive(false);
            transform.position = points[startPoint].position;
        }

        private void Update()
        {
            if (_stateManager._currentStateId == 1)
            {
                FillTank();
            }

            if (!_isTankFull || !_canMove)
            {
                _audioSource.Stop();
                return;
            }
          
            if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
            {
                i++;
                if (i == points.Length)
                {
                    i = 0;
                }                  
            }

            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

            if (transform.position == points[i].position)
                StartCoroutine(WaitTime());
        }

        IEnumerator WaitTime()
        {
            _canMove = false;
            yield return new WaitForSeconds(_waitTime);
            _audioSource.Play();
            _canMove = true;
        }

        public void FillTank()
        {
            _isTankFull = true;
            _canMove = true;

            _fullSprite.SetActive(false);
            _emptySprite.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.transform.parent = transform;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.transform.parent = null;
            }
        }
    }
}
