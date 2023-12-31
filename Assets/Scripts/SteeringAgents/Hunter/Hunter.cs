using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : SteeringAgents
{
    FiniteStateMachine _fsm;
    //[SerializeField] SteeringAgents _target;
    //array waypoints;
    public SteeringAgents actualTarget;
    [SerializeField] protected float killDist;

    [SerializeField] protected float energyLosePatrol;
    [SerializeField] protected float energyLoseChase;
    [SerializeField] protected float energyRecover;
    //[SerializeField] protected float maxEnergy;
    public float maxEnergy;
    public float _currentEnergy;
    [SerializeField] protected float energyRate;
    float counter;
    public Transform[] waypoints;
    int _currentWaypoint = 0;
    GameManager gm;
    void Start()
    {
        gm = GameManager.instance;
        //_currentEnergy = maxEnergy;
        _fsm = new FiniteStateMachine();

        _fsm.AddState(HunterStates.Idle, new IdleState(this));
        _fsm.AddState(HunterStates.Patrol, new PatrolState(this));
        _fsm.AddState(HunterStates.Chase, new ChaseState(this));

        _fsm.ChangeState(HunterStates.Idle);
    } 
    void Update()
    {
        _fsm.OnUpdate();
        CheckCloserBoid(gm.allBoids);
    }

    void CheckCloserBoid(List<SteeringAgents> agents)
    {
        foreach(var boid in agents)
        {
            if(Vector3.Distance(actualTarget.transform.position, transform.position) > 
               Vector3.Distance(boid.transform.position, transform.position))
               {
                    actualTarget = boid;
               }
        }
    }

    void KillFlockers()
    {
        if(Vector3.Distance(actualTarget.transform.position, transform.position) <= killDist)
        {
            actualTarget.RestartPosition();
            _currentEnergy = 0;
        }           
    }

    Vector3 Waypoints()
    {
        if (waypoints.Length == 0)
        {
            return default;
        }

        Transform targetWaypoint = waypoints[_currentWaypoint];
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            _currentWaypoint = (_currentWaypoint + 1) % waypoints.Length;
        }
        return targetWaypoint.position;
    }
    public void IdleBehaviour()
    {
        RegainEnergy(energyRecover);
        AddForce(Vector3.zero);
    }
    public void PatrolBehaviour()
    {
        LoseEnergy(energyLosePatrol);
        Move();
        AddForce(Seek(Waypoints()));
    }

    public void ChaseBehaviour()
    {
        LoseEnergy(energyLoseChase);

        Move();

        AddForce(Pursuit(actualTarget));
 
        KillFlockers();
    }

    void LoseEnergy(float energyManage)
    {
        counter += Time.deltaTime;

        if(counter >= energyRate && _currentEnergy > 0)
        {
            _currentEnergy -= energyManage;
            counter = 0;

            if(_currentEnergy < 0)
            _currentEnergy = 0;
        }
        
    }
    void RegainEnergy(float energyManage)
    {
        counter += Time.deltaTime;
        
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
