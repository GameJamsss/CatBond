using Assets.Scripts.Domain.Items;
using Assets.Scripts.Domain.State;
using Assets.Scripts.Managers;
using Assets.Scripts.Utils;
using CSharpFunctionalExtensions;
using UnityEngine;

namespace Assets.Scripts.Domain.Objects.ContextMenuButtons
{
    public class CheckItemContextMenuButton : MonoBehaviour, IContextMenuButton
    {
        [SerializeField] private int _id;
        [SerializeField] private int _itemId;
        [SerializeField] private int _transitToState = -1;

        [SerializeField] private Sprite _staticButtonSprite;
        [SerializeField] private Sprite _hoveredButtonSprite;
        [SerializeField] private Sprite _clickedButtonSprite;
        private CheckListManager _clm;

        void Start()
        {
            MaybeRich
                .NullSafe(FindObjectOfType<CheckListManager>())
                .Match(clm => _clm = clm,
                    () => Debug.LogError("No CheckListManager found for object: " + gameObject.name)
                    );
        }

        public int GetId()
        {
           return _id;
        }

        public void SpawnButton(GameObject parent, StateManager sm, float x, float y)
        {
            InGameButton.Create(parent
                , x
                , y
                , () =>
                {
                    _clm.MarkItem(_itemId);
                    if (_transitToState != -1) sm.ApplyState(_transitToState);
                    InGameButtonUtils
                        .GetClickableObject(parent, _id)
                        .Match(co => co.CloseContextMenu(), Debug.LogError);

                }
                , _staticButtonSprite
                , _hoveredButtonSprite
                , _clickedButtonSprite);
        }
    }
}
