using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Items;
using CSharpFunctionalExtensions;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Domain.State
{
    public class StateManager : MonoBehaviour
    {
        private List<State> _states = new();
        [SerializeField] private int _currentStateId;

        void Start()
        {
            _states = Result
                .Try(() => GetComponents(typeof(State)))
                .Map(lc => lc.Select(c => c as State).ToList())
                .Match(states => states, err =>
                {
                    Debug.Log("Some shit happened in the process of gathering states: " + err);
                    return new List<State>();
                });
        }

        public void ApplyState(int _stateId)
        {
            Result.Try(() => _states.Find(state => state.GetId() == _stateId))
                .Ensure(state => state != null, "no such state")
                .Match(state =>
                    {
                        state.ApplySprite();
                        _currentStateId = state.GetId();
                    },
                    err =>
                    {
                        Debug.Log(err);
                    });
        }

        public Result<List<IContextMenuButton>> GetStateContextMenu()
        {
            _states.ForEach(s => Debug.Log("state ids" + s.GetId()));
            return Result
                .Try(() => _states.Find(state => state.GetId() == _currentStateId))
                .Ensure(state => state != null, "no such state found")
                .Map(state =>
                {
                    Debug.Log(state);
                    return state.ContextMenu;
                });
        }
    }
}
