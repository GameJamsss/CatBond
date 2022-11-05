using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BoxController : MonoBehaviour
    {
        private BoxCollider2D _collider;
        private Animator _animator;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
        }

        public void MoveBox()
        {
            _animator.SetTrigger("Move");
            _collider.enabled = false;
        }
    }
}