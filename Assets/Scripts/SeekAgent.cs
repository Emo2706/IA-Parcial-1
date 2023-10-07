using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekAgent : SteeringAgents
{
    [SerializeField] Transform _target;
    void Update()
    {
        var obsAvoidanceForce = ObstacleAvoidance();
        if (obsAvoidanceForce == Vector3.zero)
            AddForce(Seek(_target.position));
        else AddForce(obsAvoidanceForce);

        Move();
    }
}
