using Assets.Scripts.Domain.Items;
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
        private DialogManager _dialogManager;
        private AudioSource _audioSource;
        private Maybe<AudioClip> _wrongClip = Maybe.None;

        public UseItemContextMenuButton(DialogManager dialogManager, AudioSource  audioSource, Maybe<AudioClip>  wrongClip, int id, int itemId, Sprite staticButtonSprite, Sprite hoveredButtonSprite, Sprite clickedButtonSprite, Sprite frontImage)
        {
            _dialogManager = dialogManager;
            _audioSource = audioSource;
            _wrongClip = wrongClip;
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

        public void SpawnButton(GameObject parent, StateManager sm, float x, float y)
        {
            InGameButton.Create(
                parent,
                x,
                y,
                () =>
                {
                    MaybeRich
                        .NullSafe(FindObjectOfType<ItemInventoryManager>())
                        .ToResult("No inventory manager found")
                        .Tap(iim =>
                        {
                            if (sm.TransitState(_itemId))
                                iim.RemoveItem(_itemId);
                            else
                            {
                                _dialogManager.StartDialog(Dialog.build("Хмм, не могу себе даже представить, как это может тут помочь?!"));
                                _wrongClip.Tap(_audioSource.PlayOneShot);                                   
                            }

                        })
                        .Bind(_ => InGameButtonUtils.GetClickableObject(parent, _id))
                        .Tap(co => co.CloseContextMenu())
                        .TapError(Debug.Log);
                },
                _staticButtonSprite,
                _hoveredButtonSprite,
                _clickedButtonSprite,
                _frontImage
            );
        }
    }
}
