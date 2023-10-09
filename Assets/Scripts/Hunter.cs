using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : SteeringAgents
{
    FiniteStateMachine _fsm;
    [SerializeField] SteeringAgents _target;
    //array waypoints;
    [SerializeField] protected float killDist;
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
        KillFlockers();
    }

    void KillFlockers()
    {
        if(Vector3.Distance(_target.transform.position, transform.position) <= killDist)
        {
            _target.RestartPosition();
            _currentEnergy = 0;
        }
            
        
    }
    //Preguntar donde irian mejor estos behaviours
    //Y si conviene poner dentro los Steering behaviours
    void IdleBehaviour(float _energyRecover)
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
    void PatrolBehaviour(float _energyLose)
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

    void ChaseBehaviour(float _energyLose)
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

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, killDist);
    }
}

public enum HunterStates
{
    Idle,
    Patrol,
    Chase
}
