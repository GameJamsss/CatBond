using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Domain.Objects;

namespace Assets.Scripts.Managers
{
    public class ItemInventoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _items; //prefs
        private GameObject[] _itemInInventory;

        private void Start()
        {
            _itemInInventory = new GameObject[_items.Length];
        }

        public void AddItem(int id)
        {
            if (id < _items.Length)
            {
                _itemInInventory[id] = Instantiate(_items[id]);
                _itemInInventory[id].transform.parent = transform;
                //_itemInInventory[id].transform.x = transform;

            }
        }

        public void Zac(ClickableObject _object)
        {
            for (int i = 0; i < _itemInInventory.Length; i++)
            {
                _object.Apply(i);
            }
        }

        public void RemoveItem(int id)
        {
            if (id < _items.Length)
            {
                Destroy(_itemInInventory[id]);              
            }
        }
    }
}
