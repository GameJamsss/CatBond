using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Domain.Objects;
using Assets.Scripts.UI;
using CSharpFunctionalExtensions;
using System;

namespace Assets.Scripts.Managers
{
    public class ItemInventoryManager : MonoBehaviour
    {
        private readonly Dictionary<int, GameObject> _itemInInventory = new();

        [SerializeField] private GameObject _cup;
        [SerializeField] private GameObject _coffeeBeans;
        [SerializeField] private GameObject _coin;
        [SerializeField] private GameObject _plasticBottle;
        [SerializeField] private GameObject _coffeeCup;

        [HideInInspector] public Tuple<int, GameObject> Cup;
        [HideInInspector] public Tuple<int, GameObject> CoffeeBeans;
        [HideInInspector] public Tuple<int, GameObject> Coin;
        [HideInInspector] public Tuple<int, GameObject> PlasticBottle;
        [HideInInspector] public Tuple<int, GameObject> CoffeeCup;

        [HideInInspector] public Dictionary<int, GameObject> AllItems = new();

        private void Start()
        {
            Cup = new(1, _cup);
            CoffeeBeans = new(2, _coffeeBeans);
            Coin = new(3, _coin);
            PlasticBottle = new(4, _plasticBottle);
            CoffeeCup = new(5, _coffeeCup);

            AllItems.Add(Cup.Item1, Cup.Item2);
            AllItems.Add(CoffeeBeans.Item1, CoffeeBeans.Item2);
            AllItems.Add(Coin.Item1, Coin.Item2);
            AllItems.Add(PlasticBottle.Item1, PlasticBottle.Item2);
            AllItems.Add(CoffeeCup.Item1, CoffeeCup.Item2);
        }

        public void AddItem(int id)
        {
            Result
                .Try(() => AllItems[id])
                .Map(gameObject =>
                {
                    GameObject go = Instantiate(gameObject);
                    go.transform.SetParent(transform, false);
                    return go;

                })
                .Tap(go => _itemInInventory.Add(id, go))
                .TapError(Debug.Log);
        }

        public Maybe<GameObject> GetItem(int id)
        {
            return Result.Try(() => _itemInInventory[id])
                .Match(
                    Maybe<GameObject>.From,
                    err =>
                        {
                            Debug.Log(err);
                            return Maybe<GameObject>.None;
                        }
                    );
        }

        public Dictionary<int, GameObject> GetItems()
        {
            return _itemInInventory;
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
