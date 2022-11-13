using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Domain.Items;
using Assets.Scripts.Domain.State;
using Assets.Scripts.Managers;
using CSharpFunctionalExtensions;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Domain.Objects.ContextMenuButtons
{
    public class GetItemContextMenuButton : MonoBehaviour, IContextMenuButton
    {
        [SerializeField] private int _id;
        [SerializeField] private int _itemId;
        [SerializeField] private int _nextState = -1;
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
                        .Bind(iim => Result.Try(() => new Tuple<StateManager, ItemInventoryManager>(GetComponent<StateManager>(), iim)))
                        .Match(
                            tup =>
                            {
                                tup.Item2.AddItem(_itemId);
                                if (_nextState != -1) tup.Item1.ApplyState(_nextState);
                            },
                            error => Debug.LogError("We are stupid fucks. No inventory manager is around: " + error)
                        );
                },
                _staticButtonSprite,
                _hoveredButtonSprite,
                _clickedButtonSprite
            );
        }
    }
}
