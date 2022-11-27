using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Managers;
using Assets.Scripts.Utils;
using CSharpFunctionalExtensions;
using UnityEngine;

namespace Assets.Scripts.Domain.Objects.ContextMenuButtons.CustomLogic
{
    public class PlantCustomLogic : MonoBehaviour, ICustomLogic
    {
        [SerializeField] private int _id;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _clip;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationName;
        private DialogManager dm;

        void Start()
        {
            MaybeRich
                .NullSafe(FindObjectOfType<DialogManager>())
                .Match(suc => dm = suc, () => Debug.Log("Can't find Dialog manager: " + gameObject.name));
        }
        public int GetId()
        {
            return _id;
        }

        public void Apply()
        {
            MaybeRich
                .NullSafe(_audioSource)
                .Bind(source => MaybeRich.NullSafe(_clip).Map(clip => (source, clip)))
                .Tap(tap => tap.source.PlayOneShot(tap.clip));
            MaybeRich
                .NullSafe(_animator)
                .Tap(animator => animator.Play(_animationName));

            dm.StartDialog(Dialog.build("Или моя лапка или эта кружка. К сожалению мне нужны они оба"));
        }
    }
}
