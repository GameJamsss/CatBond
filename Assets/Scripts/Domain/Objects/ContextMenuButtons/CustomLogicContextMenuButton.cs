using System.Linq;
using Assets.Scripts.Domain.Items;
using Assets.Scripts.Domain.Objects.ContextMenuButtons.CustomLogic;
using UnityEngine;

namespace Assets.Scripts.Domain.Objects.ContextMenuButtons
{
    public class CustomLogicContextMenuButton : MonoBehaviour, IContextMenuButton
    {

        [SerializeField] private int _id;
        [SerializeField] private int _customLogicComponentId;
        [SerializeField] private GameObject _customLogicHolder;

        [SerializeField] private Sprite _staticButtonSprite;
        [SerializeField] private Sprite _hoveredButtonSprite;
        [SerializeField] private Sprite _clickedButtonSprite;

        private ICustomLogic _customLogicComponent;

        void Start()
        {
            _customLogicComponent = _customLogicHolder
                .GetComponents<ICustomLogic>()
                .ToList()
                .Find(icl => icl.GetId() == _customLogicComponentId);
        }

        public int GetId()
        {
            return _id;
        }

        public void SpawnButton(GameObject parent, float x, float y)
        {
            InGameButton.Create(parent
                , x
                , y
                , () =>
                {
                    if (_customLogicComponent != null)
                        _customLogicComponent.Apply();
                    else 
                        Debug.LogError("Custom Logic Component not found in: " + gameObject.name);
                }
                , _staticButtonSprite
                , _hoveredButtonSprite
                , _clickedButtonSprite);
        }
    }
}
