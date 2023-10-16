using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject)
        {
            if(other.GetComponent<Boids>())
            {
                //Destroy(this);
                Debug.Log("me ttocaron");
            }
            
        }
        
    }
}
