using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Domain;
using UnityEngine;
using Assets.Scripts.Managers;
using CSharpFunctionalExtensions;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private int _id = 0;
    private Collider2D selfCollider;
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
        Result
            .Try(GetComponent<Collider2D>)
            .Ensure(col => col != null, "no collider2d in the object: " + gameObject.name)
            .Match(
                suc => {
                    selfCollider = suc;
                    selfCollider.tag = ConfigClass.ClickableItemTag;
                },
                Debug.LogError);
    }

    public void Take()
    {
        if (!_isUsed)
        {
            _animator.SetTrigger("Fade");
            _inventoryManager.AddItem(_id);
            _isUsed = true;
        }       
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
