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
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
