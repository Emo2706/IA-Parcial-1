using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints
{
    public Transform[] waypoints;
    int currentWaypoint = 0;

    void SeekWaypoints()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypoint];

    }
}
