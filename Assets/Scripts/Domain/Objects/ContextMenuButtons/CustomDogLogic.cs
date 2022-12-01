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
        lines.Enqueue(new Line("��������: ��� ������. ��� �� �����!"));
        lines.Enqueue(new Line("����: �, ������ ��� ������������! �� ���� ���� �����... ������ ��� ��� ����� ������� � ��������� � ������� �� ����� �������� ���."));
        lines.Enqueue(new Line("�: ���?!"));
        lines.Enqueue(new Line("�: � ��� 15 �����. ������� ������������!"));

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
