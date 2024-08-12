using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointHidder : MonoBehaviour
{
    void Start()
    {
        try {
            var wp = GameObject.FindGameObjectsWithTag("Checkpoint");
            if (wp.Length > 0) {
                foreach (var waypoints in wp)
                {
                    waypoints.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        } catch(Exception ex) {
            Debug.Log("[WaypointHidder] - " + ex.Message);
        }
    }
}
