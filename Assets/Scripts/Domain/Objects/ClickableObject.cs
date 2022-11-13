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
        public readonly float _buttonBottomOffset;
        public readonly float _buttonSideOffset;
        private Collider2D selfCollider;
        private StateManager _stateManager;
        void Start()
        {
            
            Result.Try(() => (GetComponent<StateManager>(), GetComponent<Collider2D>()))
                .Ensure(
                    tup => tup.Item1 != null && tup.Item2 != null, "no state manager or collider2d in object: " + gameObject.name
                    )
                .Match(
                    tup =>
                    {
                        selfCollider = tup.Item2;
                        selfCollider.tag = ConfigClass.ClickableItemTag;
                        _stateManager = tup.Item1;                      
                    },
                    Debug.LogError);
        }

        public void CloseContextMenu()
        {
            selfCollider.enabled = true;
            InGameButton[] gos = FindObjectsOfType<InGameButton>();
            gos.ToList().ForEach(p => Destroy(p.gameObject));
        }

        public void Click()
        {
            _stateManager
                .GetStateContextMenu()
                .Match(cmb =>
                {
                    SpawnButtons(cmb);
                    selfCollider.enabled = false;
                }, Debug.LogError);
        }

        public void ChangeContextMenu(List<IContextMenuButton> cmb)
        {
            CloseContextMenu();
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
            return _stateManager.TransitState(objId);
        }
    }
}
