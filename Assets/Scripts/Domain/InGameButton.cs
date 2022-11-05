using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

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
            _action();
        }

        void OnMouseUp()
        {
            _spriteRenderer.sprite = _spriteStatic;
        }
    }
}
