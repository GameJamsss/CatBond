using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Domain.Objects;

namespace Assets.Scripts.UI
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 100f;
        [SerializeField] private Sprite _idle;
        [SerializeField] private Sprite _click;
        [SerializeField] private Sprite _hovered;
        [SerializeField] private float _clickTime = 0.6f;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private float _checkCircleRadius = 1f;

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

            if (CheckHovered())
            {
                _spriteRenderer.sprite = _hovered;
            }
            else
            {
                _spriteRenderer.sprite = _idle;
            }


            if (Input.GetMouseButton(0))
            {
                CheckItems();
                StartCoroutine(ClickCoroutine());
            }
        }

        IEnumerator ClickCoroutine()
        {
            _spriteRenderer.sprite = _click;
            yield return new WaitForSeconds(_clickTime);
            _spriteRenderer.sprite = _idle;
        }

        private bool CheckHovered()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _checkCircleRadius);

            foreach (var collider in colliders)
                if (collider.CompareTag("Item"))
                    return true;

            return false;
        }

        private void CheckItems()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _checkCircleRadius);

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Item"))
                {
                    Debug.Log("I click on item"); //calls few times in a row i dunno why

                    //if (collider.GetComponent<DecorativeItem>() != null)
                    //    collider.GetComponent<DecorativeItem>().Click();

                    //if (collider.GetComponent<Item>() != null)
                    //    collider.GetComponent<Item>().Click();

                    if (collider.GetComponent<Plant>() != null)
                        collider.GetComponent<Plant>().Interract();

                    if (collider.GetComponent<BoxController>() != null)
                        collider.GetComponent<BoxController>().MoveBox();

                    if (collider.GetComponent<MouseToy>() != null)
                        collider.GetComponent<MouseToy>().Run();

                    if (collider.GetComponent<CollecteableItem>() != null)
                        collider.GetComponent<CollecteableItem>().Take();

                    if (collider.GetComponent<ClickableObject>() != null)
                        collider.GetComponent<ClickableObject>().Click();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _checkCircleRadius);
        }
    }
}
