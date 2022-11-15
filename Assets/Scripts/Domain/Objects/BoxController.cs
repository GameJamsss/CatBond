using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Domain.Objects;

namespace Assets.Scripts
{
    public class BoxController : MonoBehaviour
    {
        private BoxCollider2D _collider;
        private Animator _animator;
        private Interactable _interactable;

        [SerializeField] private BoxCollider2D _boxCollider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
            _interactable = GetComponent<Interactable>();
        }

        private void Start()
        {
            _boxCollider.enabled = false;
            _interactable.OnInteract += MoveBox;
        }

        private void MoveBox()
        {
            _animator.SetTrigger("Move");
            _boxCollider.enabled = true;
            _collider.enabled = false;
        }

        private void OnDisable()
        {
            _interactable.OnInteract -= MoveBox;
        }
    }
}