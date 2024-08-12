using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofShower : MonoBehaviour
{
    [SerializeField] private bool isDev;
    void Start()
    {
        if (!isDev) {
            try {
                var roof = GameObject.FindGameObjectsWithTag("Roof");
                if (roof.Length > 0) {
                    foreach (var waypoints in roof)
                    {
                        waypoints.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            } catch(Exception ex) {
                Debug.Log("[RoofShower] - " + ex.Message);
            }
        }
    }
}
