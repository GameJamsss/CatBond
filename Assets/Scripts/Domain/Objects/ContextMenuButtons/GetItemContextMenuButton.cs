using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Domain.Items;
using Assets.Scripts.Managers;
using CSharpFunctionalExtensions;
using UnityEngine;

namespace Assets.Scripts.Domain.Objects.ContextMenuButtons
{
    public class GetItemContextMenuButton : MonoBehaviour, IContextMenuButton
    {
        [SerializeField] private int _id;
        [SerializeField] private int _itemId;
        [SerializeField] private Sprite _staticButtonSprite;
        [SerializeField] private Sprite _hoveredButtonSprite;
        [SerializeField] private Sprite _clickedButtonSprite;

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
                    Result
                        .Try(FindObjectOfType<ItemInventoryManager>)
                        .Match(
                            ItemInventoryManager => ItemInventoryManager.AddItem(_itemId),
                            error => Debug.Log("We are stupid fucks. No inventory manager is around: " + error)
                        );
                },
                _staticButtonSprite,
                _hoveredButtonSprite,
                _clickedButtonSprite
            );
        }
    }
}
