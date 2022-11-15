using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

public class Checkeble : MonoBehaviour
{
    [SerializeField] private int _id = 0;
    [SerializeField] private AudioClip _clip;

    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Mark()
    {
        _source.PlayOneShot(_clip);
        FindObjectOfType<CheckListManager>().MarkItem(_id);
    }
}
