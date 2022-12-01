using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Domain.Objects.ContextMenuButtons.CustomLogic;
using Assets.Scripts.Managers;
using Assets.Scripts.Domain;
using Assets.Scripts.Utils;
using CSharpFunctionalExtensions;

public class CustomDogLogic : MonoBehaviour, ICustomLogic
{
    [SerializeField] private int _id;
    DialogManager _dialogManager;

    public void Apply()
    {
        Queue<Line> lines = new();
        lines.Enqueue(new Line("Котовски: Вот список. Все на месте!"));
        lines.Enqueue(new Line("Босс: О, список это замечательно! Но есть один нюанс... Теперь все это нужно собрать и упаковать в коробки до конца рабочего дня."));
        lines.Enqueue(new Line("К: ЧТО?!"));
        lines.Enqueue(new Line("Б: У вас 15 минут. Советую поторопиться!"));

        MaybeRich.NullSafe(FindObjectOfType<DialogManager>()).Match(dem => dem.StartDialog(new Dialog(lines, TheEndOfEverything)), () => Debug.Log("DialogManager doesn't found")); 
    }

    private void TheEndOfEverything()
    {
        //The end of game
        FindObjectOfType<TheEnd>().StartTheEndOfEverything();
    }

    public int GetId()
    {
         return _id;
    }
}
