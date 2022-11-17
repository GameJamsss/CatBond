using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Octopus : MonoBehaviour
    {
        [SerializeField] private GameObject _kettle;

        private void Start()
        {
            _kettle.SetActive(false);
        }

        private void ShowKettle()
        {
            _kettle.SetActive(true);
        }
    }
}
