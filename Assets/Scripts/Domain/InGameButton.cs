using System;
using Assets.Scripts.Domain.Objects;
using UnityEngine;
using Assets.Scripts.UI;
using System.Collections;
using System.Threading;

namespace Assets.Scripts.Domain
{
    public class InGameButton : MonoBehaviour
    {
        private Sprite _spriteStatic;
        private Sprite _spriteHover;
        private Sprite _spriteClick;
        private SpriteRenderer _spriteRenderer;
        private Action _action;

        public static void Create(
            GameObject parent,
            float x,
            float y,
            Action action,
            Sprite staticSprite,
            Sprite hoverSprite,
            Sprite clickSprite)
        {
            GameObject go = new GameObject { transform = { parent = parent.transform } };
            go.transform.localPosition = new Vector2(x, y);
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sortingOrder = 50;
            sr.sprite = staticSprite;
            CircleCollider2D c = go.AddComponent<CircleCollider2D>();
            c.isTrigger = true;
            InGameButton script = go.AddComponent<InGameButton>();
            script._spriteRenderer = sr;
            script._action = action;
            script._spriteStatic = staticSprite;
            script._spriteHover = hoverSprite;
            script._spriteClick = clickSprite;
        }

        void Start()
        {
            _spriteRenderer.sprite = _spriteStatic;
        }

        void OnMouseEnter()
        {
            _spriteRenderer.sprite = _spriteHover;
        }

        void OnMouseExit()
        {
            _spriteRenderer.sprite = _spriteStatic;
        }

        void OnMouseDown()
        {
            _spriteRenderer.sprite = _spriteClick;
            foreach (ClickableObject o in FindObjectsOfType(typeof(ClickableObject)))
            {
                o.DestructContextMenu();
            }
            _action();

            new Thread(_ => DestroyCoroutine());
        }

        void DestroyCoroutine()
        {
            // yield return new WaitForSeconds(2f);
            Thread.Sleep(1000);
            CursorController cursorC = FindObjectOfType<CursorController>();
            cursorC._tmp = null;
        }

        void OnMouseUp()
        {
            _spriteRenderer.sprite = _spriteStatic;
        }
    }
}
