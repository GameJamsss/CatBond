using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

public class CupItem : MonoBehaviour
{
    [SerializeField] private int _id = 0;
    private ItemInventoryManager _inventoryManager;
    private bool _isUsed = false;

    private void Start()
    {
        _inventoryManager = FindObjectOfType<ItemInventoryManager>();
    }

    public void Take()
    {
        if (!_isUsed)
        {
            _inventoryManager.AddItem(_id);
            _isUsed = true;
            GetComponent<SpriteRenderer>().enabled  = false;
        }       
    }
}
