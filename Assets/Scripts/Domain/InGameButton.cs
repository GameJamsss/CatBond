using System;
using Assets.Scripts.Domain.Objects;
using UnityEngine;
using Assets.Scripts.UI;
using System.Collections;
using System.Threading;
using Assets.Scripts.Domain.Items;
using CSharpFunctionalExtensions;
using Random = System.Random;

namespace Assets.Scripts.Domain
{
    public class InGameButton : MonoBehaviour, IClickable
    {
        private Maybe<Sprite> _spriteImage;
        private Sprite _spriteStatic;
        private Sprite _spriteHover;
        private Sprite _spriteClick;
        private SpriteRenderer _spriteRenderer;
        private Action _action;

        public static GameObject Create(
            GameObject parent,
            float x,
            float y,
            Action action,
            Sprite staticSprite,
            Sprite hoverSprite,
            Sprite clickSprite,
            Sprite frontImage)
        {
            GameObject go = Create(parent, x, y, action, staticSprite, hoverSprite, clickSprite);
            GameObject frontImageGo = new GameObject("front sprite");
            frontImageGo.transform.SetParent(go.transform, false);
            frontImageGo.tag = ConfigClass.ClickableItemTag;
            SpriteRenderer sr = frontImageGo.AddComponent<SpriteRenderer>();
            sr.sprite = frontImage;
            sr.sortingOrder = 51;
            return go;
        }

        public static GameObject Create(
            GameObject parent,
            float x,
            float y,
            Action action,
            Sprite staticSprite,
            Sprite hoverSprite,
            Sprite clickSprite)
        {
            GameObject go = new GameObject("InGameButton" + new Random().Next(1, 100));
            go.tag = ConfigClass.ClickableItemTag;
            go.transform.SetParent(parent.transform, false);
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
            return go;
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

        void OnMouseUp()
        {
            _spriteRenderer.sprite = _spriteStatic;
        }

        public void Click()
        {
            _spriteRenderer.sprite = _spriteClick;
            _action();
        //    transform.parent.GetComponent<ClickableObject>().CloseContextMenu();
        }
    }
}
