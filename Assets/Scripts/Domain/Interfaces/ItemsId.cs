using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public static class ItemsId
    {
        public static Tuple<int, string> Cup = new(1, "");
        public static Tuple<int, string> CoffeeBeans = new(2, "");
        public static Tuple<int, string> Coin = new(3, "");
        public static Tuple<int, string> PlasticBottle = new(4, "");
        public static Tuple<int, string> CoffeeCup = new(5, "");

        public static Dictionary<int, string> AllItems = new()
        {
            [Cup.Item1] = Cup.Item2,
            [CoffeeBeans.Item1] = CoffeeBeans.Item2,
            [Coin.Item1] = Coin.Item2,
            [PlasticBottle.Item1] = PlasticBottle.Item2,
            [CoffeeCup.Item1] = CoffeeCup.Item2,
        };
    }
}
