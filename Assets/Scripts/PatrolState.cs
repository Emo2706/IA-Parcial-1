using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }

}
