using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boids : SteeringAgents
{
    GameManager gm;

    [Range(0f, 1.5f)] public float cohesionWeight = 1;
    [Range(0f, 1.5f)] public float separationWeight = 1;
    [Range(0f, 1.5f)] public float alignmentWeight = 1;
    [SerializeField] float eatDist;
    [SerializeField] SteeringAgents hunter;
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
        AllBehavoiurs();
        gm.ShiftPositionOnBounds(transform);
    }

    void AllBehavoiurs()
    {
        Move();
        if(Vector3.Distance(transform.position, hunter.transform.position) > viewRadius)
        {
            if(Vector3.Distance(transform.position, gm.foodPrefab.transform.position) <= viewRadius)
            {
                Debug.Log("Detecto comida");
                AddForce(Arrive(gm.foodPrefab.transform.position));
                if(Vector3.Distance(transform.position, gm.foodPrefab.transform.position) <= eatDist)
                {
                    gm.ChangeFoodPosition();
                }
                //EatFood();
            }
            else
            {
                Flocking();
                Debug.Log("Flockeando");
            }

        }
        else
        {
            AddForce(Evade(hunter));
            Debug.Log("Peligro hay un hunter");
        }
        
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

    /*void EatFood()
    {
        if(Vector3.Distance(transform.position, gm.food.transform.position) <= eatDist)
        {
            Debug.Log("Morfo");
            Destroy(gm.food);
        }
        
    }*/
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        Gizmos.DrawWireSphere(transform.position, _separationRadius);
        Gizmos.DrawWireSphere(transform.position, eatDist);
    }
}
