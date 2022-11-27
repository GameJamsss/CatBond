using System;
using System.Collections.Generic;
using UnityEngine;
using CSharpFunctionalExtensions;
using System.Linq;
using Assets.Scripts.Domain;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class ItemInventoryManager : MonoBehaviour
    {
        [SerializeField] private Sprite _backgroundSprite;
        private readonly Dictionary<int, Sprite> _allSprites = new();
        private readonly Dictionary<int, GameObject> _itemsInInventory = new();

        private void Start()
        {
            Result.Try(() =>
                    Resources
                    .LoadAll<Sprite>(ConfigClass.InventoryItemsPath)
                    .ToList()
                    .ForEach(sprite => _allSprites.Add(int.Parse(sprite.name), sprite))
                ).TapError(err => Debug.LogError("Can not parse items from resource folder: " + ConfigClass.InventoryItemsPath + ": error - " + err));

            AddItem(5);
            AddItem(2);
        }

        public void AddItem(int id)
        {
            Result
                .Try(() => (id, _allSprites[id]))
                .Ensure(_ => !_itemsInInventory.ContainsKey(id), "Player already have this item: " + id)
                .Tap(tup => _itemsInInventory.Add(id, AddElementToUi(tup.Item2)))
                .TapError(Debug.Log);
        }

        public Maybe<Sprite> GetCollectedItemSprite(int id)
        {
            return _allSprites.TryFind(id);
        }

        public Dictionary<int, Sprite> GetItems()
        {
            return _allSprites
                .ToList()
                .FindAll(tup => _itemsInInventory.Keys.Contains(tup.Key))
                .ToDictionary(tup => tup.Key, tup => tup.Value);
        }

        public void RemoveItem(int id)
        {
            Result
                .Try(() => _itemsInInventory[id])
                .Tap(_ => _itemsInInventory.Remove(id))
                .Tap(Destroy)
                .TapError(Debug.LogError);
        }
        
        private GameObject AddElementToUi(Sprite itemSprite)
        {
            GameObject go = new GameObject("item: " + itemSprite.name, typeof(RectTransform));
            go.layer = LayerMask.NameToLayer("UI");
            GameObject backgroundGameObject = new GameObject();
            backgroundGameObject.layer = LayerMask.NameToLayer("UI");
            GameObject spriteGameObject = new GameObject();
            spriteGameObject.layer = LayerMask.NameToLayer("UI");
            backgroundGameObject.AddComponent<CanvasRenderer>();
            spriteGameObject.AddComponent<CanvasRenderer>();
            go.transform.SetParent(gameObject.transform, false);
            backgroundGameObject.transform.SetParent(go.transform, false);
            spriteGameObject.transform.SetParent(go.transform, false);
            Image bgsr = backgroundGameObject.AddComponent<Image>();
            Image msr = spriteGameObject.AddComponent<Image>();
            spriteGameObject.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
            bgsr.sprite = _backgroundSprite;
            msr.sprite = itemSprite;
            return go;
        }
    }
}
