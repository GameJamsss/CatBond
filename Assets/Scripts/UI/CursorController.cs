using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private Sprite _idle;
    [SerializeField] private Sprite _click;
    [SerializeField] private float _clickTime = 0.6f;

    private Vector3 mousePosition;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Cursor.visible = false;
        _spriteRenderer.sprite = _idle;
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

        if (Input.GetMouseButton(0))
        {
            StartCoroutine(ClickCoroutine());
        }
    }

    IEnumerator ClickCoroutine()
    {
        _spriteRenderer.sprite = _click;
        yield return new WaitForSeconds(_clickTime);
        _spriteRenderer.sprite = _idle;
    }
}
