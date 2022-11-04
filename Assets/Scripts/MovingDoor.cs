using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Transform toPos;
    [SerializeField] private Transform fromPos;

    [SerializeField] private float speed = 1f;

    private bool isDoorMoving = false;
    private bool isDoorActive = false;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        fromPos.position = door.transform.position;
    }

    private void Update()
    {
        if (isDoorActive && isDoorMoving)
            MoveDoor();
        else if (isDoorMoving && !isDoorActive)
            MoveDoorToStartPos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grabbable"))
        {
            anim.SetTrigger("Active");
            isDoorMoving = true;
            isDoorActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grabbable"))
        {
            anim.SetTrigger("Idle");
            isDoorMoving = true;
            isDoorActive = false;
        }
    }

    private void MoveDoor()
    {
        door.transform.position = Vector2.Lerp(door.transform.position, toPos.position, speed * Time.deltaTime);

        if (door.transform.position == toPos.position)
            isDoorMoving = false;
    }

    private void MoveDoorToStartPos()
    {
        door.transform.position = Vector2.Lerp(door.transform.position, fromPos.position, speed * Time.deltaTime);

        if (door.transform.position == fromPos.position)
            isDoorMoving = false;
    }
}
