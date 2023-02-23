using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatControl : MonoBehaviour
{
    public static List<GameObject> Available = new();
    public static List<GameObject> Unavailable = new();

    private void Awake()
    {
        Available.Clear();
        Unavailable.Clear();
        GameObject[] SittingpointsFind = GameObject.FindGameObjectsWithTag("sittingpoint");
        foreach (GameObject g in SittingpointsFind)
        {
            g.GetComponent<Renderer>().enabled = false;
            Available.Add(g);
        }
    }
}
