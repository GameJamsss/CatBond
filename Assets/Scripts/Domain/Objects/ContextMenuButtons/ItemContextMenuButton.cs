using Assets.Scripts.Domain.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using UnityEngine;
using Assets.Scripts.Domain.State;
using Assets.Scripts.UI;

namespace Assets.Scripts.Domain.Objects.ContextMenuButtons
{
    public class ItemContextMenuButton : MonoBehaviour, IContextMenuButton
    {
        private int _id;
        public int _itemId;
        private Sprite _staticButtonSprite;
        private Sprite _hoveredButtonSprite;
        private Sprite _clickedButtonSprite;

        public ItemContextMenuButton(int id, int itemId, Sprite staticButtonSprite, Sprite hoveredButtonSprite, Sprite clickedButtonSprite)
        {
            _id = id;
            _itemId = itemId;
            _staticButtonSprite = staticButtonSprite;
            _hoveredButtonSprite = hoveredButtonSprite;
            _clickedButtonSprite = clickedButtonSprite;
        }

        public int GetId()
        {
            return _id;
        }

        public void SpawnButton(GameObject parent, float x, float y)
        {
            Debug.Log("creating buttons");
            InGameButton.Create(
                parent,
                x,
                y,
                () =>
                {
                    Result
                        .Try(parent.GetComponent<StateManager>)
                        .Ensure(sm => sm != null, "no state manager")
                        .Tap(sm => sm.TransitState(_itemId))
                        .Map(_ => parent.GetComponent<ClickableObject>())
                        .Ensure(co => co != null, "no clickable object")
                        .Tap(co => co.CloseContextMenu())
                        .Map(_ => FindObjectOfType<CursorController>())
                        .TapError(Debug.Log);
                },
                _staticButtonSprite,
                _hoveredButtonSprite,
                _clickedButtonSprite
            );
        }
    }
}
