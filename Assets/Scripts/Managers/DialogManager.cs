﻿using Assets.Scripts.Domain;
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

        [Space(10f), Header("Äudio")]
        [SerializeField] private AudioClip _writtingClip;
        [SerializeField] private AudioClip _closePopUp;
        [SerializeField] private AudioClip _popUpClip;
        [SerializeField] private AudioSource _dialogCanvasAudioS;

        private Dialog _currentDialog;
        private Coroutine _coroutine;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void StartDialog(Dialog dialog)
        {          
            canvas.SetActive(true);
            _dialogCanvasAudioS.PlayOneShot(_popUpClip);
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
            _audioSource.Stop();
            canvas.SetActive(false);
        }

        IEnumerator StartSequence(string sentence)
        {
            _audioSource.Play();
            _dialogText.text = "";
            foreach (var c in sentence)
            {
                _dialogText.text += c;
                yield return new WaitForSeconds(_sleep); ;
            }
            _audioSource.Stop();
        }
    }
}
