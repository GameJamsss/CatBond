using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Items;
using Assets.Scripts.Managers;
using CSharpFunctionalExtensions;
using UnityEngine;

using UnityEngine.UI;
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
                                List<float> pozs = iim.GetItems().Count % 2 != 0 ? co.CalcOddPoz(iim.GetItems().ToList()) : co.ClacEvenPoz(iim.GetItems().ToList());

                                for (int i = 0; i < iim.AllItems.Count; i++)
                                {
                                    GameObject go = iim.AllItems[i];
                                    Sprite sprite = go.GetComponent<Image>().sprite;
                                    int itemId = iim.AllItems.Keys.ToArray()[i];

                                    new ItemContextMenuButton(new Random().Next(),
                                            itemId,
                                            sprite,
                                            sprite,
                                            sprite)
                                        .SpawnButton(parent, pozs[i], co._buttonBottomOffset);
                                }
                            })
                            .TapError(Debug.Log);
                    },
              _staticButtonSprite,
    _hoveredButtonSprite,
    _clickedButtonSprite);
        }
    }
}
