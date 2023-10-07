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
        _fsm = new FiniteStateMachine();
    }

    
    void Update()
    {
        
    }
}

public enum HunterStates
{
    Idle,
    Move
}
