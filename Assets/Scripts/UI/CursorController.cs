using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain;
using UnityEngine;
using Assets.Scripts.Domain.Objects;
using Assets.Scripts.Utils;
using CSharpFunctionalExtensions;
using Assets.Scripts.Managers;

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
        private DialogManager dm;

        private bool clicked;
        private Vector3 mousePosition;

        private void Start()
        {
            MaybeRich.NullSafe(FindObjectOfType<DialogManager>())
                .Match(
                suc => dm = suc,
                () => Debug.LogError("No dialog manager found: " + gameObject.name));
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

            if (cols.Count != 0 && !dm.InDialog)
            {

                cols.ForEach(col =>
                {
                    MaybeRich
                        .NullSafe(col.GetComponent<CollectableItem>())
                        .Tap(ci =>
                        {
                            ci.Take();
                            cos.ForEach(co => co.CloseContextMenu());
                        });
                    MaybeRich
                        .NullSafe(col.GetComponent<InGameButton>())
                        .Tap(igb =>
                        {
                            igb.Click();
                        });
                    MaybeRich
                        .NullSafe(col.GetComponent<ClickableObject>())
                        .Tap(clickable =>
                        {
                            cos
                                .FindAll(co => co != clickable)
                                .ForEach(co => co.CloseContextMenu());
                            clickable.Click();
                        });
                    MaybeRich
                        .NullSafe(col.GetComponent<Interactable>())
                        .Tap(i =>
                        {
                            i.Interact();
                        });

                });
            }
            else
            {
                cos.ForEach(co => co.CloseContextMenu());
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _checkCircleRadius);
        }
    }
}
