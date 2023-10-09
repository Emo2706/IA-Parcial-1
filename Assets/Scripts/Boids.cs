using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : SteeringAgents
{
    GameManager gm;

    [Range(0f, 1.5f)] public float cohesionWeight = 1;
    [Range(0f, 1.5f)] public float separationWeight = 1;
    [Range(0f, 1.5f)] public float alignmentWeight = 1;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        gm.allBoids.Add(this);

        var randomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        AddForce(randomDir.normalized * _maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Flocking();
        Move();
        gm.ShiftPositionOnBounds(transform);
    }

    void Flocking()
    {                                    //Weighted behaviors
        //AddForce(Cohesion(gm.allBoids) * cohesionWeight);
        //AddForce(Separation(gm.allBoids) * separationWeight);
        //AddForce(Alignment(gm.allBoids) * alignmentWeight);

        //Combined Behaviors
        AddForce(Cohesion(gm.allBoids) * cohesionWeight + 
                Separation(gm.allBoids) * separationWeight + 
                Alignment(gm.allBoids) * alignmentWeight);
    }


    protected override void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }
}
