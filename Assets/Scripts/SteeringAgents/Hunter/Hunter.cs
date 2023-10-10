using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : SteeringAgents
{
    FiniteStateMachine _fsm;
    //[SerializeField] SteeringAgents _target;
    //array waypoints;
    public SteeringAgents _target;
    [SerializeField] protected float killDist;

    [SerializeField] protected float energyLosePatrol;
    [SerializeField] protected float energyLoseChase;
    [SerializeField] protected float energyRecover;
    //[SerializeField] protected float maxEnergy;
    public float maxEnergy;
    public float _currentEnergy;
    [SerializeField] protected float energyRate;
    float counter;
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

    void KillFlockers()
    {
        if(Vector3.Distance(_target.transform.position, transform.position) <= killDist)
        {
            _target.RestartPosition();
            _currentEnergy = 0;
        }           
    }

    public void IdleBehaviour()
    {
        HunterEnergy(energyRecover);
        AddForce(Vector3.zero);
    }
    public void PatrolBehaviour()
    {
        HunterEnergy(energyLosePatrol);
        Move();
        //Seek(waypointsArray);
    }

    public void ChaseBehaviour()
    {
        HunterEnergy(energyLoseChase);
        Move();
        Pursuit(_target);
        KillFlockers();
    }

    void HunterEnergy(float energyManage)
    {
        counter += Time.deltaTime;

        if(counter >= energyRate && _currentEnergy > 0)
        {
            _currentEnergy -= energyManage;
            counter = 0;

            if(_currentEnergy < 0)
            _currentEnergy = 0;
        }
        if(counter >= energyRate && _currentEnergy < maxEnergy)
        {
            _currentEnergy += energyManage;
            counter = 0;

            if(_currentEnergy > maxEnergy)
            _currentEnergy = maxEnergy;
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
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
