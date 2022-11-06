using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Domain.State;

namespace Assets.Scripts.Managers
{
    public class CheckListManager : MonoBehaviour
    {
        [SerializeField] private Toggle[] _toggles;
        [SerializeField] private GameObject _boss;

        private int _marktedItemsCount = 0;

        public void MarkItem(int id)
        {
            _toggles[id].isOn = true;
            _marktedItemsCount++;

            if (_marktedItemsCount == _toggles.Length)
            {
                _boss.GetComponent<StateManager>()._currentStateId = 1;
            }
        }
    }
}