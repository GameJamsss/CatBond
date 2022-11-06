using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Items;
using Assets.Scripts.Domain.State;
using CSharpFunctionalExtensions;
using UnityEngine;

namespace Assets.Scripts.Domain.Objects
{
    public class ClickableObject : MonoBehaviour, IClickable, IApplicable
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
                            "You forgot to add StateManager to object, I WON'T TELL YOU WHICH ONE BECAUSE I DON'T KNOW, FIND OUT YOURSELF: " +
                            err);
                        return Maybe<StateManager>.None;
                    });
        }

        //void DestroyContextMenu()
        //{
        //    StartCoroutine(DestructContextMenu);
        //} 

        public void DestructContextMenu()
        {
            InGameButton[] gos = FindObjectsOfType<InGameButton>();
            gos.ToList().ForEach(p => DestroyImmediate(p.gameObject));
        }

        public void Click()
        {
            _stateManager.Match(
                sm => 
                    sm
                        .GetStateContextMenu()
                        .Match(lcmb =>
                                {
                                    List<float> pozs = lcmb.Count % 2 != 0 ? CalcOddPoz(lcmb) : ClacEvenPoz(lcmb);
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

        private List<float> CalcOddPoz(List<IContextMenuButton> list)
        {
            float offsetBack = list.Count / 2f * _buttonSideOffset;
            return list.Select((_, i) => i * _buttonSideOffset - offsetBack).ToList();
        }

        private List<float> ClacEvenPoz(List<IContextMenuButton> list)
        {
            float offsetBack = list.Count * _buttonBottomOffset / 2;
            return list.Select((_, i) => i * _buttonSideOffset - offsetBack).ToList();
        }

        public bool Apply(int objId)
        {
            return _stateManager.Match(
                sm => sm.TransitState(objId),
                () =>
                {
                    Debug.Log("State manager has not found. Add one.");
                    return false;
                }
            );
        }
    }
}
