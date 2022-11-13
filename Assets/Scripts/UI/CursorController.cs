using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain;
using UnityEngine;
using Assets.Scripts.Domain.Objects;
using Assets.Scripts.Managers;
using CSharpFunctionalExtensions;

namespace Assets.Scripts.UI
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private int _clickableObjectsLayerIndex = 1;
        [SerializeField] private float _moveSpeed = 100f;
        [SerializeField] private Sprite _idle;
        [SerializeField] private Sprite _click;
        [SerializeField] private Sprite _hovered;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private float _checkCircleRadius = 1f;

        private bool clicked;
        private Vector3 mousePosition;
        private void Start()
        {
            Cursor.visible = false;
            _spriteRenderer.sprite = _idle;
        }

        void Update()
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, _moveSpeed);

            if (clicked)
            {
                _spriteRenderer.sprite = _hovered;
            }
            else
            {
                _spriteRenderer.sprite = _idle;
            }

            if (!clicked && Input.GetMouseButtonDown(0))
            {
                clicked = true;
                ClickItems();
            }

            if (Input.GetMouseButtonUp(0))
            {
                clicked = false;
            }
        }

        private void ClickItems()
        {
            List<ClickableObject> cos =
                FindObjectsOfType<ClickableObject>().ToList();

            List<Collider2D> cols = 
                Physics2D
                    .OverlapCircleAll(transform.position, _checkCircleRadius)
                    .ToList()
                    .FindAll(col => col.tag == ConfigClass.ClickableItemTag);

            foreach (var col in cols)
            {
                CollectableItem mbCollectableItem = col.GetComponent<CollectableItem>();
                if (mbCollectableItem)
                {
                    mbCollectableItem.Take();
                }

                InGameButton mbInGameButton = col.GetComponent<InGameButton>();
                if (mbInGameButton)
                {
                    mbInGameButton.Click();
                }

                ClickableObject mbClickableItem = col.GetComponent<ClickableObject>();
                if (mbClickableItem)
                {
                    cos
                        .FindAll(co => co != mbClickableItem)
                        .ForEach(co => co.CloseContextMenu());
                    mbClickableItem.Click();
                }
            }
        }

        private void ClickOnItem()
        {

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _checkCircleRadius);
        }
    }
}
