using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    public GameObject car;
    public float time_loop = 10;
    private float currentTime = 0;

    private void Update()
    {
        if (Time.time - currentTime > time_loop)
        {
            DestroyCar();
            Instantiate(car);
            currentTime = Time.time;
        }
    }

    private void DestroyCar()
    {
        GameObject car = GameObject.FindGameObjectWithTag("car");
        if (car != null)
        {
            Destroy(car);
        }
    }
}
