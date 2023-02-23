using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPeople : MonoBehaviour
{
    public GameObject customers;
    public int CustAmount = 5;
    public int CustExist = 0;
    public GameObject citizen;
    public int amount = 70;
    public List<Transform> location = new();

    private void Awake()
    {
        GameObject[] waypointsFind = GameObject.FindGameObjectsWithTag("waypoint");
        GameObject[] waypoints2Find = GameObject.FindGameObjectsWithTag("waypoint2");
        foreach (GameObject g in waypointsFind)
        {
            g.GetComponent<Renderer>().enabled = false;
            location.Add(g.transform);
        }
        foreach (GameObject g in waypoints2Find)
        {
            g.GetComponent<Renderer>().enabled = false;
            location.Add(g.transform);
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject citizens = Instantiate(citizen, transform);
            int locate = Random.Range(0, location.Count);
            citizens.GetComponent<Test_script>().locate = locate;
            Transform spawnLocation = location[locate];
            citizens.GetComponent<NavMeshAgent>().Warp(spawnLocation.position);
        }
    }

    public void SpawnCustomer()
    {
        if (CustExist < CustAmount)
        {
            GameObject customer = Instantiate(customers, transform);
            customer.GetComponent<NavMeshAgent>().Warp(location[Random.Range(0, location.Count)].position);
            CustExist++;
        }
    }
}
