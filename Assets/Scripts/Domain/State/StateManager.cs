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
        [SerializeField] public int _currentStateId;

        void Start()
        {
            _states = Result
                .Try(() => GetComponents(typeof(State)))
                .Map(lc => lc.Select(c => c as State).ToList())
                .Match(states => states, err =>
                {
                    Debug.LogError("Some shit happened in the process of gathering states: " + err);
                    return new List<State>();
                });
        }

        public void ApplyState(int _stateId)
        {
            Result.Try(() => _states.Find(state => state.GetId() == _stateId))
                .Ensure(state => state != null, "no such state")
                .Match(state =>
                    {
                        state.ApplyState();
                        _currentStateId = state.GetId();
                    },
                    Debug.LogError);
        }

        public bool TransitState(int itemId)
        {
            return getState(itemId)
                .Map(state => state.Transition(itemId))
                .Bind(mbId => mbId.Map(getState).Match(some => some, () => Result.Failure<State>("no such state")))
                .Tap(state => ApplyState(state.GetId()))
                .TapError(err => Debug.Log(err))
                .IsSuccess;
        }

        public Result<List<IContextMenuButton>> GetStateContextMenu()
        {
            return getState(_currentStateId)
                .Map(state =>
                {
                    return state.ContextMenu;
                });
        }

        private Result<State> getState(int stateId)
        {
            return Result
                .Try(() => _states.Find(state => state.GetId() == stateId))
                .Ensure(state => state != null, gameObject.name + " no such state found: " + stateId);

        }
    }
}
