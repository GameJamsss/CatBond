using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Transform _moveToPos;

    private CameraController _camera;

    private void Start()
    {
        _camera = FindObjectOfType<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _camera.SetNewPos(_moveToPos);
        }
    }
}
