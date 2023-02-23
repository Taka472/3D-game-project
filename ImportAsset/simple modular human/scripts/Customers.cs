﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customers : MonoBehaviour
{
    public List<GameObject> way_points = new();
    public List<GameObject> way_points_2 = new();
    public List<GameObject> Stealing_points = new();
    public List<GameObject> pick_up_points = new();

    NavMeshAgent agent;
    public Animator ani;
    public GameObject aim_point;

    public bool execute_walking;
    public bool execute_sitting;
    public bool execute_stealing;
    public bool execute_picking_up;

    public bool execute_running;

    public float walk_speed;
    public float run_speed;

    Vector3 sitting_position;
    Vector3 sitting_Rotation;

    Vector3 stealing_position;
    Vector3 stealing_Rotation;

    public int locate;

    public bool walk;
    public bool run;
    public bool sit;
    public bool steal;
    public bool pick_up;

    public bool destermine_new_aim;
    public Sprite[] orderImage;
    public int orderDish = -1;

    public float waitingTime = 100;

    private int start;
    private int end;

    private GameObject door;

    private SpawnPeople spawnCustomer;

    void Awake()
    {
        spawnCustomer = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SpawnPeople>();

        agent = GetComponent<NavMeshAgent>();

        sitting_position = new Vector3(0, 0.5f, 0);
        sitting_Rotation = new Vector3(0, 90, 0);

        stealing_position = new Vector3(0, 0.04185915f, -0.07200003f);
        stealing_Rotation = new Vector3(0, 180, 0);

        GameObject[] waypointsFind = GameObject.FindGameObjectsWithTag("waypoint");
        GameObject[] waypoints2Find = GameObject.FindGameObjectsWithTag("waypoint2");
        GameObject[] stealingpointsFind = GameObject.FindGameObjectsWithTag("stealingpoint");
        GameObject[] pick_up_pointsFind = GameObject.FindGameObjectsWithTag("pickuppoint");

        way_points.Clear();
        Stealing_points.Clear();
        pick_up_points.Clear();

        foreach (GameObject g in waypointsFind)
        {
            g.GetComponent<Renderer>().enabled = false;
            way_points.Add(g);
        }
        foreach (GameObject g in waypoints2Find)
        {
            g.GetComponent<Renderer>().enabled = false;
            way_points_2.Add(g);
        }
        foreach (GameObject g in stealingpointsFind)
        {
            Stealing_points.Add(g);
        }
        foreach (GameObject g in pick_up_pointsFind)
        {
            pick_up_points.Add(g);
        }

        door = GameObject.FindGameObjectWithTag("AutoDoor");
    }

    bool in_sitting;
    bool in_stealing;
    bool in_pickup;

    Coroutine sitting_start;
    Coroutine stealing_start;
    Coroutine pickup_start;

    public GameObject crowbar;

    private void Start()
    {
        start = 1;
        end = 2;
    }

    IEnumerator sitting_down()
    {
        yield return new WaitForSeconds(0);

        transform.parent = aim_point.transform;


        Destroy(agent);
        GetComponent<BoxCollider>().enabled = true;

        ani.SetInteger("legs", 3);
        ani.SetInteger("arms", 3);

        transform.localPosition = sitting_position;
        switch(aim_point.layer)
        {
            case 13:
                {
                    transform.eulerAngles = Vector3.zero;
                    break;
                }
            case 14:
                {
                    transform.eulerAngles = -1 * sitting_Rotation;
                    break;
                }
            default:
                {
                    transform.eulerAngles = sitting_Rotation;
                    break;
                }
        }
        SeatControl.Unavailable.Add(aim_point);
        SeatControl.Available.Remove(aim_point);
        spawnCustomer.SpawnCustomer();

        Order();
        yield return new WaitForSeconds(5);

        //agent = gameObject.AddComponent<NavMeshAgent>();
        //
        //in_sitting = false;
        //destermine_new_aim = false;
        //transform.parent = null;
        //
        //StopCoroutine(sitting_start);
    }

    private void Order()
    {
        transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        Image dialog = transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>();
        orderDish = Random.Range(0, 8);
        dialog.sprite = orderImage[orderDish];
        spawnCustomer.SpawnCustomer();
        StarControl.gameStart = true;
    }

    public IEnumerator Eat()
    {
        StarControl.currentMeter += 20 * StarControl.difficult;
        yield return new WaitForSeconds(5);
        StandUp();
    }

    public void StandUp()
    {
        GameObject dish = null;
        if (GetComponentInChildren<OrderingNumber>() != null)
            dish = GetComponentInChildren<OrderingNumber>().gameObject;
        if (dish != null) Destroy(dish);
        agent = gameObject.AddComponent<NavMeshAgent>();
        in_sitting = false;
        start = 0;
        end = 0;
        GetComponent<BoxCollider>().enabled = false;
        destermine_new_aim = false;
        transform.SetParent(spawnCustomer.transform);
        transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        SeatControl.Available.Add(aim_point);
        SeatControl.Unavailable.Remove(aim_point);
        if (dish != null)
        {
            spawnCustomer.CustExist--;
            spawnCustomer.SpawnCustomer();
        }
        StopCoroutine(sitting_start);
        StartCoroutine(DestroyCustomer());
    }

    IEnumerator DestroyCustomer()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);    
    }

    IEnumerator stealing_execute()
    {
        yield return new WaitForSeconds(0);
        crowbar.SetActive(true);
        transform.parent = aim_point.transform;
        transform.localPosition = stealing_position;
        transform.localEulerAngles = stealing_Rotation;

        ani.SetInteger("legs", 5);
        ani.SetInteger("arms", 22);

        yield return new WaitForSeconds(5);
        crowbar.SetActive(false);
        in_stealing = false;
        destermine_new_aim = false;
        transform.parent = null;

        StopCoroutine(stealing_start);
    }


    IEnumerator pickup_execute()
    {
        yield return new WaitForSeconds(0);



        ani.SetInteger("legs", 32);
        ani.SetInteger("arms", 32);


        yield return new WaitForSeconds(2);

        in_pickup = false;
        destermine_new_aim = false;


        StopCoroutine(pickup_start);
    }

    public bool ready;

    void Update()
    {

        if (!ready)
        {
            return;
        }

        if (Mathf.Abs(door.transform.position.x - transform.position.x) < 2 && Mathf.Abs(door.transform.position.z - transform.position.z) < 2)
        {
            door.GetComponent<AutomationDoor>().customerTrigger = true;
        }
        else if (!in_sitting)
        {
            door.GetComponent<AutomationDoor>().customerTrigger = false;
        }

        if (!destermine_new_aim)
        {
            int what_to_choose = Random.Range(start, end);



            walk = false;
            run = false;
            sit = false;
            steal = false;
            pick_up = false;



            if (what_to_choose == 0)
            {
                walk = true;
                if (locate < 3)
                {
                    int Which_point = Random.Range(0, way_points.Count);
                    aim_point = way_points[Which_point];
                    destermine_new_aim = true;
                }
                else
                {
                    int Which_point = Random.Range(0, way_points_2.Count);
                    aim_point = way_points_2[Which_point];
                    destermine_new_aim = true;
                }
            }
            if (what_to_choose == 2)
            {
                run = true;

                int Which_point = UnityEngine.Random.Range(0, way_points.Count);
                aim_point = way_points[Which_point].gameObject;
                destermine_new_aim = true;
            }
            if (what_to_choose == 1)
            {
                sit = true;
                int Which_point = UnityEngine.Random.Range(0, SeatControl.Available.Count);
                aim_point = SeatControl.Available[Which_point];
                destermine_new_aim = true;
            }
            if (what_to_choose == 3)
            {
                steal = true;

                int Which_point = UnityEngine.Random.Range(0, Stealing_points.Count);
                aim_point = Stealing_points[Which_point].gameObject;
                destermine_new_aim = true;
            }
            if (what_to_choose == 4)
            {
                pick_up = true;

                int Which_point = UnityEngine.Random.Range(0, pick_up_points.Count);
                aim_point = pick_up_points[Which_point].gameObject;
                destermine_new_aim = true;
            }

        }
        if (destermine_new_aim)
        {
            if (walk)
            {

                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {

                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;

                    ani.SetInteger("arms", 5);
                    ani.SetInteger("legs", 5);

                    destermine_new_aim = false;
                }

            }
            if (run)
            {

                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {
                    agent.speed = run_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 2);
                    ani.SetInteger("legs", 2);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;

                    ani.SetInteger("arms", 5);
                    ani.SetInteger("legs", 5);

                    destermine_new_aim = false;
                }

            }
            if (sit && !in_sitting)
            {

                if (Vector3.Distance(transform.position, aim_point.transform.position) > 1.5f)
                {
                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 1.5f)
                {
                    agent.speed = 0;


                    if (!in_sitting)
                    {
                        in_sitting = true;

                        sitting_start = StartCoroutine(sitting_down());
                    }

                }

            }
            if (steal && !in_stealing)
            {

                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {

                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;



                    if (!in_stealing)
                    {
                        in_stealing = true;

                        stealing_start = StartCoroutine(stealing_execute());
                    }

                }


            }
            if (pick_up && !in_pickup)
            {
                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {

                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;



                    if (!in_pickup)
                    {
                        in_pickup = true;

                        pickup_start = StartCoroutine(pickup_execute());
                    }

                }
            }
        }
    }
}
