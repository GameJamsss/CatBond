using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class CheckListManager : MonoBehaviour
    {
        [SerializeField] private Toggle[] _toggles;

        public void MarkItem(int id)
        {
            _toggles[id].isOn = true;
        }
    }
}