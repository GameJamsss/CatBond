using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Assets.Scripts.Domain.Objects
{
    public class Interactable : MonoBehaviour
    {
        [HideInInspector] public Action OnInteract;

        public void Interact()
        {
            OnInteract?.Invoke();
        }
    }
}