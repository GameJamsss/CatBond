using UnityEngine;

namespace Assets.Scripts
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private GameObject _cam;
        [SerializeField] private float _parallaxEffect;

        private float _length, _startPos;

        private void Start()
        {
            _cam = FindObjectOfType<Camera>().gameObject;

            _startPos = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void FixedUpdate()
        {
            float dist = (_cam.transform.position.x * _parallaxEffect);
            transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);
        }
    }
}
