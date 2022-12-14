using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Items;
using Assets.Scripts.Domain.State;
using Assets.Scripts.Managers;
using Assets.Scripts.Utils;
using CSharpFunctionalExtensions;
using UnityEngine;

namespace Assets.Scripts.Domain.Objects.ContextMenuButtons
{
    class InfoContextMenuButton : MonoBehaviour, IContextMenuButton, IInformationable
    {
        [SerializeField] private int _id;
        [SerializeField] private string[] _info;
        [SerializeField] private Sprite _staticButtonSprite;
        [SerializeField] private Sprite _hoveredButtonSprite;
        [SerializeField] private Sprite _clickedButtonSprite;
        public Dialog GetDialog()
        {
            return new Dialog(new Queue<Line>(_info.Select(str => new Line(str))));
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
                        .NullSafe(FindObjectOfType<DialogManager>())
                        .ToResult("Dialog manager does not exist")
                        .Match(
                            dialogManager => dialogManager.StartDialog(GetDialog()),
                            error => Debug.Log("We are stupid fucks. The error is: " + error)
                         );
                    InGameButtonUtils
                        .GetClickableObject(parent, _id)
                        .Match(co => co.CloseContextMenu(), Debug.Log);
                },
                _staticButtonSprite,
                _hoveredButtonSprite,
                _clickedButtonSprite
                );
        }
    }
}
