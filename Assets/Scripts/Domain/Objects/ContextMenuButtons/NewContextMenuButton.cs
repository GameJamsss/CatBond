using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Items;
using Assets.Scripts.Domain.State;
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
        private DialogManager _dialogManager;
        private AudioSource _audioSource;
        private Maybe<AudioClip> _wrongClip = Maybe.None;

        void Start()
        {
            MaybeRich.NullSafe(FindObjectOfType<ItemInventoryManager>())
             .Match(suc => iim = suc,
             () => Debug.Log("No inventory manager has been found " + gameObject.name)
             );

            MaybeRich.NullSafe(FindObjectOfType<DialogManager>())
              .Match(suc => _dialogManager = suc,
               () => Debug.Log("No dialog manager has been found " + gameObject.name)
               );

            _wrongClip = MaybeRich.NullSafe(Resources.Load<AudioClip>("WrongInteraction"));

            MaybeRich.NullSafe(GetComponent<AudioSource>())
            .ToResult("No audio source found in: " + gameObject.name)
            .Match(
             suc => _audioSource = suc,
             Debug.Log
            );
        }

        public int GetId()
        {
            return _id;
        }

        public void SpawnButton(GameObject parent, StateManager sm, float x, float y)
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
                                        .Select<KeyValuePair<int, Sprite>, IContextMenuButton>(kv =>
                                           new UseItemContextMenuButton(_dialogManager, _audioSource, _wrongClip, new Random().Next(),
                                               kv.Key,
                                               _staticContextMenuBackground,
                                               _hoveredContextMenuBackground,
                                               _clickedContextMenuBackground,
                                               kv.Value)
                                                )
                                        .ToList()
                                    );
                            })
                            .TapError(Debug.Log);
                    },
              _staticButtonSprite,
    _hoveredButtonSprite,
    _clickedButtonSprite);
        }
    }
}
