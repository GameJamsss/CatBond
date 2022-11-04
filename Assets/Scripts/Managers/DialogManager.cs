using Assets.Scripts.Domain;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Managers
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private Button _nextButton;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private TMP_Text _dialogText;
        [SerializeField] private float _sleep;
        [SerializeField] private string buttonNextText;
        [SerializeField] private string buttonEndText;
        private Dialog _currentDialog;
        private Coroutine _coroutine;

        public void StartDialog(Dialog dialog)
        {
            Debug.Log("started");
            canvas.SetActive(true);
            _nextButton.onClick.RemoveListener(CloseDialog);
            _nextButton.onClick.AddListener(NextLine);
            _buttonText.text = buttonNextText;
            _currentDialog = dialog;
            NextLine();
        }
        private void NextLine()
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            Line line = _currentDialog.Next();
            if (line is EndingLine)
            {
                _buttonText.text = buttonEndText;
                _nextButton.onClick.RemoveListener(NextLine);
                _nextButton.onClick.AddListener(CloseDialog);
            }
            _coroutine = StartCoroutine(StartSequence(line.GetLine()));
        }
        private void CloseDialog()
        {
            canvas.SetActive(false);
        }
        IEnumerator StartSequence(string sentence)
        {
            _dialogText.text = "";
            foreach (var c in sentence)
            {
                _dialogText.text += c;
                yield return new WaitForSeconds(_sleep); ;
            }
            
        }



    }

}
