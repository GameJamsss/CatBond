using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Items;
using CSharpFunctionalExtensions;
using UnityEngine;

namespace Assets.Scripts.Domain.State
{
    public class State : MonoBehaviour
    {

        [SerializeField] private int _id;
        [SerializeField] private int[] _contextMenuIds;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public List<IContextMenuButton> ContextMenu = new();

        void Start()
        {
            ContextMenu = Result
                .Try(() => GetComponents(typeof(IContextMenuButton)))
                .Map(contextMenu => contextMenu.ToList().FindAll(c => c is IContextMenuButton))
                .Map(lc => lc.Select(c => c as IContextMenuButton).ToList())
                .Map(lc => lc.FindAll(c => _contextMenuIds.Contains(c.GetId())))
                .Match(resultList => resultList, err =>
                {
                    Debug.Log("Some shit happened in the process of gathering context menu components: " + err);
                    return new List<IContextMenuButton>();
                });
        }

        public void ApplySprite()
        {
            _spriteRenderer.sprite = _sprite;
        }

        public int GetId()
        {
            return _id;
        }
    }
}
