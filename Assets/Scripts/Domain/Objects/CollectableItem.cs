using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private int _id = 0;
    private ItemInventoryManager _inventoryManager;
    private bool _isUsed = false;
    private Animator _animator;

    private void Awake()
    {
      _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _inventoryManager = FindObjectOfType<ItemInventoryManager>();
    }

    public void Take()
    {
        if (!_isUsed)
        {
            _animator.SetTrigger("Fade");
            _inventoryManager.AddItem(_id);
            _isUsed = true;
            Destroy(gameObject);
        }       
    }
}
