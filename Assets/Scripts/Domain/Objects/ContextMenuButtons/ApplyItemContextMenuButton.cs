using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Items;
using Assets.Scripts.Managers;
using CSharpFunctionalExtensions;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

namespace Assets.Scripts.Domain.Objects.ContextMenuButtons
{
    public class ApplyItemContextMenuButton : MonoBehaviour, IContextMenuButton
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
                        Result
                            .Try(parent.GetComponent<ClickableObject>)
                            .Tap(co => co.DestructContextMenu())
                            .Tap(co =>
                            {
                                Dictionary<int, GameObject> items = iim.GetItems();
                                List<float> pozs = items.Count % 2 != 0 ? co.CalcOddPoz(items.ToList()) : co.ClacEvenPoz(items.ToList());

                                int i = 0;
                                List<IContextMenuButton> cmb = new();
                                foreach (var keyValuePair in items)
                                {
                                    GameObject go = keyValuePair.Value;
                                    Sprite sprite = go.GetComponent<Image>().sprite;
                                    int itemId = keyValuePair.Key;

                                    cmb.Add(new ItemContextMenuButton(new Random().Next(),
                                            itemId,
                                            sprite,
                                            sprite,
                                            sprite)
                                            );
                                    i++;
                                }
                                co.ChangeContextMenu(cmb);
                            })
                            .TapError(Debug.Log);
                    },
              _staticButtonSprite,
    _hoveredButtonSprite,
    _clickedButtonSprite);
        }
    }
}
