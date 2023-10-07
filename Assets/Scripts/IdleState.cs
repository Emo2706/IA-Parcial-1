using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    Material mat;
    Transform transform;
    Hunter _hunter;

    public IdleState(Hunter npc)
    {
        transform = npc.transform;
        _hunter = npc;
        mat = npc.GetComponent<Renderer>().material;
    }
    public override void OnEnter() 
    { 
        Debug.Log("Estoy cansado");
        mat.color = Color.green;
    }

    public override void OnUpdate()
    {
        Debug.Log("Recargando energia");
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            fsm.ChangeState(HunterStates.Patrol);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Energia a Full");
    }
}
