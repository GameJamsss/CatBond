using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Items;
using CSharpFunctionalExtensions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static System.Int32;

namespace Assets.Scripts.Domain.State
{
    public class State : MonoBehaviour
    {

        [SerializeField] private int _id;
        [SerializeField] private int[] _contextMenuIds;
        [SerializeField] private string[] _transitions;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _onTransitionAudioClip;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _onTransitionAnimationName;
        private List<Tuple<int, int>> _parsedTransactions;
        public List<IContextMenuButton> ContextMenu = new();

        void Start()
        {
            ContextMenu = Result
                .Try(() => GetComponents(typeof(IContextMenuButton)))
                .Map(contextMenu => contextMenu.ToList().FindAll(c => c is IContextMenuButton))
                .Map(lc => lc.Select(c => c as IContextMenuButton).ToList())
                .Map(lc => lc.FindAll(c => _contextMenuIds.Contains(c.GetId())))
                .Match(resultList => resultList, err =>
                {
                    Debug.LogError("Some shit happened in the process of gathering context menu components: " + gameObject.name + ", " + err);
                    return new List<IContextMenuButton>();
                });
            _parsedTransactions = _parseTransactions(_transitions).Match(some => some, () =>
            {
                Debug.LogError("can not parse all _transactions" + gameObject.name);
                return new List<Tuple<int, int>>();
            });
        }

        public Maybe<int> Transition(int itemId)
        {
            Tuple<int, int> t = _parsedTransactions.Find(i => i.Item1 == itemId);
            return t != null ? Maybe<int>.From(t.Item2) : Maybe<int>.None;
        }

        public void ApplySprite()
        {
            if (_sprite != null && _spriteRenderer != null) _spriteRenderer.sprite = _sprite;
        }

        public void ApplySound()
        {
            if (_audioSource != null && _onTransitionAudioClip != null)
            {
                _audioSource.Stop();
                _audioSource.loop = false;
                _audioSource.clip = _onTransitionAudioClip;
                _audioSource.Play();
            }
        }

        public void ApplyAnimation()
        {
            if (_animator != null)
            {
                Result
                    .Try(() => _animator.Play(_onTransitionAnimationName))
                    .TapError(Debug.LogError);
            }
        }

        public void ApplyState()
        {
            ApplySprite();
            ApplySound();
            ApplyAnimation();
        }

        public int GetId()
        {
            return _id;
        }

        private Maybe<List<Tuple<int, int>>> _parseTransactions(string[] transitions)
        {
            return Result
                .Try(() => transitions.ToList().Select(str =>
                    {
                        string[] m = str.Split(":");
                        return new Tuple<int, int>(Parse(m[0]), Parse(m[1]));
                    }).ToList())
                .Match(
                    Maybe.From,
                    err => { Debug.Log(err); return Maybe.None; }
                    );
        }

    }
}
