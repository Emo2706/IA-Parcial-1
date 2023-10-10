using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    Material mat;
    Transform transform;
    Hunter _hunter;

    public ChaseState(Hunter npc)
    {
        transform = npc.transform;
        _hunter = npc;
        mat = npc.GetComponent<Renderer>().material;
    }
    public override void OnEnter() 
    { 
        mat.color = Color.red;
        Debug.Log("Boid detectado");
    }

    public override void OnUpdate()
    {
        Debug.Log("Estoy cazando");
        
        if(_hunter._currentEnergy > 0)
        _hunter.ChaseBehaviour();
        else
        fsm.ChangeState(HunterStates.Idle);
    }

    public override void OnExit()
    {
        Debug.Log("Dejamos de cazar");
    }
}
