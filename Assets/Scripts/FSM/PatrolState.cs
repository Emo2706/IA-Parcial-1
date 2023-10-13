using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PatrolState : State
{
    Material mat;
    Transform transform;
    Hunter _hunter;

    public PatrolState(Hunter npc)
    {
        transform = npc.transform;
        _hunter = npc;
        mat = npc.GetComponent<Renderer>().material;
    }
    public override void OnEnter() 
    { 
        mat.color = Color.yellow;
        Debug.Log("Arranco a patrullar");
    }

    public override void OnUpdate()
    {
        Debug.Log("Patrullando");
        
        if(_hunter._currentEnergy > 0)
        {
            if(Vector3.Distance(transform.position, _hunter.target.transform.position) > _hunter.viewRadius)
            _hunter.PatrolBehaviour();
            else
            fsm.ChangeState(HunterStates.Chase);
        }
        
        else
        fsm.ChangeState(HunterStates.Idle);
    }

    public override void OnExit()
    {
        Debug.Log("Dejo de patrullar");
    }

}
