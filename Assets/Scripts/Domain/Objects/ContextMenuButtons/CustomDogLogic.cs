using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Domain.Objects.ContextMenuButtons.CustomLogic;
using Assets.Scripts.Managers;
using Assets.Scripts.Domain;

public class CustomDogLogic : MonoBehaviour, ICustomLogic
{
    [SerializeField] private int _id;
    DialogManager _dialogManager;

    private void Start()
    {
        _dialogManager = FindObjectOfType<DialogManager>();
    }

    public void Apply()
    {
        Queue<Line> lines = new();
        lines.Enqueue(new Line("Котовски: Вот список. Все на месте!"));
        lines.Enqueue(new Line("Босс: О, список это замечательно! Но есть один нюанс... Теперь все это нужно собрать и упаковать в коробки до конца рабочего дня."));
        lines.Enqueue(new Line("К: ЧТО?!"));
        lines.Enqueue(new Line("Б: У вас 15 минут. Советую поторопиться!"));
        FindObjectOfType<DialogManager>().StartDialog(new Dialog(lines));

        //Заершение игры
    }

    public int GetId()
    {
         return _id;
    }
}
