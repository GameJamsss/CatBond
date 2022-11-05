using UnityEngine;

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
