using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    FiniteStateMachine _fsm;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxEnergy;
    float _currentEnergy;
    float energyRate;
    
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

    void OnIdle(float _energyRecover)
    {
        energyRate += Time.deltaTime;

        if(energyRate >= 1 && _currentEnergy < maxEnergy)
        {
            _currentEnergy += _energyRecover;
            energyRate = 0;
        }
        else
        return;
    }
    void OnPatrol(float _energyLose)
    {
        energyRate += Time.deltaTime;

        if(energyRate >= 1 && _currentEnergy > 0)
        {
            _currentEnergy -= _energyLose;
            energyRate = 0;
        }
        else
        return;
    }

    void OnChase(float _energyLose)
    {
        energyRate += Time.deltaTime;

        if(energyRate >= 1 && _currentEnergy > 0)
        {
            _currentEnergy -= _energyLose;
            energyRate = 0;
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
