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
                        InGameButtonUtils.GetClickableObject(parent, _id)
                            .Tap(co =>
                            {
                                Dictionary<int, Sprite> items = iim.GetItems();
                                Debug.Log(items.Count);
                                int i = 0;
                                List<IContextMenuButton> cmb = new();
                                foreach (var keyValuePair in items)
                                {
                                    Sprite sprite = keyValuePair.Value; ;
                                    int itemId = keyValuePair.Key;

                                    cmb.Add(new UseItemContextMenuButton(new Random().Next(),
                                            itemId,
                                            sprite,
                                            sprite,
                                            sprite)
                                            );
                                    i++;
                                }

                                co.ChangeContextMenu(cmb);
                            })
                            .TapError(Debug.LogError);
                    },
              _staticButtonSprite,
    _hoveredButtonSprite,
    _clickedButtonSprite);
        }
    }
}
