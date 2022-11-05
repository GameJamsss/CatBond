using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeItem : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;

    private void Start()
    {
        _canvas.SetActive(false);
    }

    public void Interact()
    {
        _canvas.SetActive(true);
        Debug.Log("Info...");
    }

}
