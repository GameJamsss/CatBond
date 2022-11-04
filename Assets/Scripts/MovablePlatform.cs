using UnityEngine;

namespace Assets.Scripts
{
    public class MovablePlatform : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private int startPoint;
        [SerializeField] private Transform[] points;
        [SerializeField] private CapsuleCollider2D _capsuleCollider2D;

        private int i;

        private void Start()
        {
            transform.position = points[startPoint].position;
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
            {
                i++;
                if (i == points.Length)
                    i = 0;
            }

            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.transform.parent = transform;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.transform.parent = null;
            }
        }
    }
}