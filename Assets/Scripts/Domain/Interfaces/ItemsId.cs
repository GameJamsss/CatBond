using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ItemsId : MonoBehaviour
    {
        [SerializeField] private Sprite _cup;
        [SerializeField] private Sprite _coffeeBeans;
        [SerializeField] private Sprite _coin;
        [SerializeField] private Sprite _plasticBottle;
        [SerializeField] private Sprite _coffeeCup;

        [HideInInspector] public Tuple<int, Sprite> Cup;
        [HideInInspector] public Tuple<int, Sprite> CoffeeBeans;
        [HideInInspector] public Tuple<int, Sprite> Coin;
        [HideInInspector] public Tuple<int, Sprite> PlasticBottle;
        [HideInInspector] public Tuple<int, Sprite> CoffeeCup;

        [HideInInspector] public Dictionary<int, Sprite> AllItems = new();

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
    }
}
