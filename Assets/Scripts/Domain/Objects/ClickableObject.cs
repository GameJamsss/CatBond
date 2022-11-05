using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Items;
using Assets.Scripts.Domain.State;
using CSharpFunctionalExtensions;
using UnityEngine;

namespace Assets.Scripts.Domain.Objects
{
    public class ClickableObject : MonoBehaviour, IClickable
    {
        [SerializeField] private float _buttonBottomOffset;
        [SerializeField] private float _buttonSideOffset;
        private Maybe<StateManager> _stateManager = Maybe<StateManager>.None;
        void Start()
        {
            
            _stateManager = Result
                .Try(GetComponent<StateManager>)
                .Match(
                    Maybe<StateManager>.From,
                    err =>
                    {
                        Debug.Log(
                            "You forgot to add StateManager to object, I WON'T TELL YOU WHICH ONE, FIND OUT YOURSELF: " +
                            err);
                        return Maybe<StateManager>.None;
                    });
        }

        void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                Click();
            }
        }

        public void DestructContextMenu()
        {
            foreach (Transform child in transform)
            {
               Destroy(child.gameObject);
            }
        }

        public void Click()
        {
            _stateManager.Match(
                sm => 
                    sm
                        .GetStateContextMenu()
                        .Match(lcmb =>
                                {
                                    List<float> pozs = lcmb.Select((_, i) => (i - 1) * _buttonSideOffset - lcmb.Count * _buttonSideOffset).ToList();
                                    foreach (var (contextMenuButton, i1) in lcmb.Select((cmb, i) => (cmb, i)))
                                    {
                                        contextMenuButton.SpawnButton(gameObject, pozs[i1], -1 * _buttonBottomOffset);
                                    }
                                }, err =>
                                {
                                    Debug.Log(err);
                                }),
                () =>
                {
                    Debug.Log("State manager has not found. Add one.");
                }
            );
        }

    }
}
