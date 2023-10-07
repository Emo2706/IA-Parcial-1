using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    FiniteStateMachine _fsm;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxEnergy;
    float _currentEnergy;
    float energyLoseRate;
    
    void Start()
    {
        _currentEnergy = maxEnergy;
        _fsm = new FiniteStateMachine();

        _fsm.AddState(HunterStates.Idle, new IdleState(this));
        _fsm.AddState(HunterStates.Patrol, new PatrolState(this));
        _fsm.AddState(HunterStates.Chase, new ChaseState(this));
    } 
    void Update()
    {
        _fsm.OnUpdate();
    }

    void OnIdle()
    {

    }
    void OnPatrol(float _energyLose)
    {
        energyLoseRate += Time.deltaTime;

        if(energyLoseRate >= 1 && _currentEnergy > 0)
        {
            _currentEnergy -= _energyLose;
            energyLoseRate = 0;
        }
        else
        return;
    }

    void OnChase(float _energyLose)
    {
        energyLoseRate += Time.deltaTime;

        if(energyLoseRate >= 1 && _currentEnergy > 0)
        {
            _currentEnergy -= _energyLose;
            energyLoseRate = 0;
        }
        else
        return;
    }
}

public enum HunterStates
{
    Idle,
    Patrol,
    Chase
}
