using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BoxController : MonoBehaviour
    {
        private BoxCollider2D _collider;
        private Animator _animator;

        [SerializeField] private BoxCollider2D _boxCollider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _boxCollider.enabled = false;
        }

        public void MoveBox()
        {
            _animator.SetTrigger("Move");
            _boxCollider.enabled = true;
            _collider.enabled = false;
        }
    }
}