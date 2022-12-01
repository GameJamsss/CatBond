using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : MonoBehaviour
{
    [SerializeField] private GameObject _theEndPanel;

    private void Start() => _theEndPanel.SetActive(false);

    public void StartTheEndOfEverything()
    {
        _theEndPanel.SetActive(true);
    }
}
