using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _smooth = 5.0f;
    private Vector3 _offset = new Vector3(0, 0, -5);
    private Vector3 _targetPos;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        if (Camera.main.aspect >= 1.7)
            _camera.orthographicSize = 5f;
        else
            _camera.orthographicSize = 5.5f;
    }

    private void Start()
    {
        _targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPos + _offset, Time.deltaTime * _smooth);
    }

    public void SetNewPos(Transform newPos)
    {
        _targetPos = newPos.position;
    }
}
