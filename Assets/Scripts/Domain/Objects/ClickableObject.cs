using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Scripts.Domain.Items;
using Assets.Scripts.Domain.State;
using CSharpFunctionalExtensions;
using UnityEngine;
using UnityEngine.U2D.IK;

namespace Assets.Scripts.Domain.Objects
{
    public class ClickableObject : MonoBehaviour, IClickable, IApplicable
    {
        [SerializeField] public float _buttonBottomOffset;
        [SerializeField] public float _buttonSideOffset;
        private Maybe<StateManager> _stateManager = Maybe<StateManager>.None;
        public bool InContextMenu = false;
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

        public void DestructContextMenu()
        {
            InGameButton[] gos = FindObjectsOfType<InGameButton>();
            gos.ToList().ForEach(p => Destroy(p.gameObject));
        }

        public void ResetObject()
        {
            InContextMenu = false;
            DestructContextMenu();
        }

        public void Click()
        {
            
            if (!InContextMenu) {
                Debug.Log("FICASDFASDFOIJSDGO:FIj");
                _stateManager.Match(
                sm => 
                    sm
                        .GetStateContextMenu()
                        .Match(SpawnButtons, err =>
                                {
                                    Debug.Log(err);
                                }),
                () =>
                {
                    Debug.Log("State manager has not found. Add one.");
                }
                );
                InContextMenu = true;
            }
        }

        public void ChangeContextMenu(List<IContextMenuButton> cmb)
        {
            DestructContextMenu();
            SpawnButtons(cmb);
        }

        private void SpawnButtons(List<IContextMenuButton> cmb)
        {
            List<float> pozs = cmb.Count % 2 != 0 ? CalcOddPoz(cmb) : ClacEvenPoz(cmb);
            foreach (var (contextMenuButton, i1) in cmb.Select((cmb, i) => (cmb, i)))
            {
                contextMenuButton.SpawnButton(gameObject, pozs[i1], -1 * _buttonBottomOffset);
            }
        }

        public List<float> CalcOddPoz<T>(List<T> list)
        {
            float offsetBack = list.Count / 2f * _buttonSideOffset;
            return list.Select((_, i) => i * _buttonSideOffset - offsetBack).ToList();
        }

        public List<float> ClacEvenPoz<T>(List<T> list)
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
