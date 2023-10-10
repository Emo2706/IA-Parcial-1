using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitAgent : SteeringAgents
{
    [SerializeField] SteeringAgents _target;
    [SerializeField] protected float killDist;
    void Update()
    {
        var obsAvoidanceForce = ObstacleAvoidance();
        if (obsAvoidanceForce == Vector3.zero)
            AddForce(Pursuit(_target));
        else AddForce(obsAvoidanceForce);

        Move();
        KillFlockers();
    }

    void KillFlockers()
    {
        if(Vector3.Distance(_target.transform.position, transform.position) <= killDist)
            _target.RestartPosition();
        
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(CalculateFuturePos(_target), 0.3f);
    }
}
