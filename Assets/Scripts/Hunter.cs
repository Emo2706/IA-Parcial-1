using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    FiniteStateMachine _fsm;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxEnergy;
    float _currentEnergy;
    
    void Start()
    {
        _currentEnergy = maxEnergy;
        _fsm = new FiniteStateMachine();

        _fsm.AddState(HunterStates.Idle, new IdleState());
        _fsm.AddState(HunterStates.Patrol, new PatrolState(this));
        _fsm.AddState(HunterStates.Chase, new ChaseState(this));
    }

    
    void Update()
    {
        _fsm.OnUpdate();
    }
}

public enum HunterStates
{
    Idle,
    Patrol,
    Chase
}
