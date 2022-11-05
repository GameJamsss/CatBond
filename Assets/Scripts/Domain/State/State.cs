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
        [SerializeField] private AudioClip _onTransitionClip;
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
                    Debug.Log("Some shit happened in the process of gathering context menu components: " + err);
                    return new List<IContextMenuButton>();
                });
            _parseTransactions(_transitions).Match(some => some, () =>
            {
                Debug.Log("can not parse all _transactions");
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
            if (_sprite != null) _spriteRenderer.sprite = _sprite;
        }

        public void ApplySound()
        {
            if (_audioSource != null && _onTransitionClip != null)
            {
                _audioSource.Stop();
                _audioSource.loop = false;
                _audioSource.clip = _onTransitionClip;
                _audioSource.Play();
            }
        }

        public void ApplyState()
        {
            ApplySprite();
            ApplySound();
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
