using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Domain.Objects;
using Assets.Scripts.UI;
using CSharpFunctionalExtensions;

namespace Assets.Scripts.Managers
{
    public class ItemInventoryManager : MonoBehaviour
    {
        private readonly Dictionary<int, GameObject> _itemInInventory = new();

        public void AddItem(int id)
        {
            Result
                .Try(() => ItemsId.AllItems[id])
                .Map(spritePath =>
                {
                    Debug.Log("here");
                    Sprite sprite = Resources.Load<Sprite>(spritePath);
                    GameObject go = new GameObject();
                    Image i = go.AddComponent<Image>();
                    i.sprite = sprite;
                    go.transform.SetParent(transform, false);
                    return go;

                })
                .Tap(go => _itemInInventory.Add(id, go))
                .TapError(Debug.Log);
        }

        public void RemoveItem(int id)
        {
            Result
                .Try(() => _itemInInventory[id])
                .Tap(_ => _itemInInventory.Remove(id))
                .Tap(Destroy)
                .TapError(Debug.Log);
        }
    }
}
