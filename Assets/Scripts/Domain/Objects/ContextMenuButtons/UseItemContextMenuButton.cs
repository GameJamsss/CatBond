﻿using Assets.Scripts.Domain.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using UnityEngine;
using Assets.Scripts.Domain.State;
using Assets.Scripts.Managers;
using Assets.Scripts.UI;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Domain.Objects.ContextMenuButtons
{
    public class UseItemContextMenuButton : MonoBehaviour, IContextMenuButton
    {
        private int _id;
        public int _itemId;
        private Sprite _staticButtonSprite;
        private Sprite _hoveredButtonSprite;
        private Sprite _clickedButtonSprite;
        private Sprite _frontImage;

        public UseItemContextMenuButton(int id, int itemId, Sprite staticButtonSprite, Sprite hoveredButtonSprite, Sprite clickedButtonSprite, Sprite frontImage)
        {
            _id = id;
            _itemId = itemId;
            _staticButtonSprite = staticButtonSprite;
            _hoveredButtonSprite = hoveredButtonSprite;
            _clickedButtonSprite = clickedButtonSprite;
            _frontImage = frontImage;
        }

        public int GetId()
        {
            return _id;
        }

        public void SpawnButton(GameObject parent, float x, float y)
        {
            InGameButton.Create(
                parent,
                x,
                y,
                () =>
                {
                    MaybeRich
                        .NullSafe(parent.GetComponent<StateManager>())
                        .ToResult("no state manager")
                        .Bind(sm =>
                            MaybeRich.NullSafe(FindObjectOfType<ItemInventoryManager>())
                                .ToResult("No inventory manager found")
                                .Map(iim => (iim, sm)))
                        .Tap(valueTuple =>
                        {
                            if (valueTuple.sm.TransitState(_itemId)) valueTuple.iim.RemoveItem(_itemId);
                        })
                        .Bind(_ => InGameButtonUtils.GetClickableObject(parent, _id))
                        .Tap(co => co.CloseContextMenu())
                        .TapError(Debug.LogError);
                },
                _staticButtonSprite,
                _hoveredButtonSprite,
                _clickedButtonSprite,
                _frontImage
            );
        }
    }
}
