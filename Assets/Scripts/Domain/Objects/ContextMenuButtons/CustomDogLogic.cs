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
        lines.Enqueue(new Line("��������: ��� ������. ��� �� �����!"));
        lines.Enqueue(new Line("����: �, ������ ��� ������������! �� ���� ���� �����... ������ ��� ��� ����� ������� � ��������� � ������� �� ����� �������� ���."));
        lines.Enqueue(new Line("�: ���?!"));
        lines.Enqueue(new Line("�: � ��� 15 �����. ������� ������������!"));
        FindObjectOfType<DialogManager>().StartDialog(new Dialog(lines));

        //��������� ����
    }

    public int GetId()
    {
         return _id;
    }
}
