using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    Material mat;
    public override void OnEnter() 
    { 
        Debug.Log("Entre a Idle");
        mat.color = Color.green;
    }

    public override void OnUpdate()
    {
        Debug.Log("Estoy en Idle");

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            fsm.ChangeState(HunterStates.Patrol);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Sali de Idle");
    }
}
