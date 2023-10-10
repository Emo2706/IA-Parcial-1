using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    State _currentState;

    Dictionary<HunterStates, State> _allStates = new Dictionary<HunterStates, State>();

    public void ChangeState(HunterStates name)
    {
        if (!_allStates.ContainsKey(name)) return;
        _currentState?.OnExit();
        _currentState = _allStates[name];
        _currentState.OnEnter();
    }

    public void AddState(HunterStates name, State state)
    {
        if (!_allStates.ContainsKey(name))
            _allStates.Add(name, state);
        else
            _allStates[name] = state;

        state.fsm = this;
    }

    public void OnUpdate()
    {
        //_currentState? es lo mismo que poner
        //if(_currentState != null)
        _currentState?.OnUpdate();
    }
}
