using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _smooth = 5.0f;
    private Vector3 _offset = new Vector3(0, 2, -5);

    private void Start()
    {
        if (_target == null)
            _target = GameObject.FindGameObjectWithTag("PLayer");
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.transform.position + _offset, Time.deltaTime * _smooth);
    }
}
