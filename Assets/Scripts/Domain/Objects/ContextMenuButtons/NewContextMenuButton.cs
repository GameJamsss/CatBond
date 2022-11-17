using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Items;
using Assets.Scripts.Managers;
using Assets.Scripts.Utils;
using CSharpFunctionalExtensions;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Domain.Objects.ContextMenuButtons
{
    public class NewContextMenuButton : MonoBehaviour, IContextMenuButton
    {

        [SerializeField] private int _id;
        [SerializeField] private Sprite _staticButtonSprite;
        [SerializeField] private Sprite _hoveredButtonSprite;
        [SerializeField] private Sprite _clickedButtonSprite;
        [SerializeField] private Sprite _staticContextMenuBackground;
        [SerializeField] private Sprite _hoveredContextMenuBackground;
        [SerializeField] private Sprite _clickedContextMenuBackground;
        private ItemInventoryManager iim;

        void Start()
        {
            iim = FindObjectOfType<ItemInventoryManager>();
        }
        public int GetId()
        {
            return _id;
        }

        public void SpawnButton(GameObject parent, float x, float y)
        {
            InGameButton
                .Create(parent,
                    x,
                    y,
                    () =>
                    {
                        InGameButtonUtils
                            .GetClickableObject(parent, _id)
                            .Tap(co =>
                            {
                                co.ChangeContextMenu(
                                    iim
                                        .GetItems()
                                        .Select<KeyValuePair<int, Sprite>, IContextMenuButton> (kv =>
                                            new UseItemContextMenuButton(new Random().Next(),
                                                kv.Key,
                                                _staticContextMenuBackground,
                                                _hoveredContextMenuBackground,
                                                _clickedContextMenuBackground,
                                                kv.Value)
                                                )
                                        .ToList()
                                    );
                            })
                            .TapError(Debug.LogError);
                    },
              _staticButtonSprite,
    _hoveredButtonSprite,
    _clickedButtonSprite);
        }
    }
}
