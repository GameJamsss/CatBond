using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

public class Checkeble : MonoBehaviour
{
    [SerializeField] private int _id = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            FindObjectOfType<CheckListManager>().MarkItem(_id);
    }
}
