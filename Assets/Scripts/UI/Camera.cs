using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float _smooth = 5.0f;
    private Vector3 _offset = new Vector3(0, 0, -5);
    private Vector3 _targetPos;

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
