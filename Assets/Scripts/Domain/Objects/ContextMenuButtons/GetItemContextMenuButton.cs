using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Domain.Items;
using Assets.Scripts.Domain.State;
using Assets.Scripts.Managers;
using Assets.Scripts.Utils;
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
        [SerializeField] private Sprite _staticBackgroundSprite;
        [SerializeField] private Sprite _hoveredBackgroundSprite;
        [SerializeField] private Sprite _clickedBackgroundSprite;

        private ItemInventoryManager _iim;
        void Start()
        {
            MaybeRich
                .NullSafe(FindObjectOfType<ItemInventoryManager>())
                .Match(
                    suc => _iim = suc,
                    () => Debug.Log("No Item Inventory Manager has been found: " + gameObject.name)
                );
        }
        public int GetId()
        {
            return _id;
        }

        public void SpawnButton(GameObject parent, StateManager sm, float x, float y)
        {

            _iim.GetCollectedItemSprite(_itemId).Match(
                    suc =>
                    {
                        InGameButton.Create(
                            parent,
                            x,
                            y,
                            () =>
                            {

                                _iim.AddItem(_itemId);
                                _iim.GetCollectedItemSprite(_itemId);
                                if (_nextState != -1) sm.ApplyState(_nextState);
                                InGameButtonUtils.GetClickableObject(parent, _id)
                                    .Match(co => co.CloseContextMenu(), Debug.Log);
                            },
                            _staticBackgroundSprite,
                            _hoveredBackgroundSprite,
                            _clickedBackgroundSprite,
                            suc
                        );
                    },
                    () => {
                        Debug.Log("Can not find corresponding sprite: " + gameObject.name + "on parent " + parent.name);
                    }
                );
            
        }
    }
}
