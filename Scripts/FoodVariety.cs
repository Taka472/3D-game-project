using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodVariety : MonoBehaviour
{
    public bool haveOnion = false;
    public bool haveCarrot = false;
    public GameObject collideWith;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("pan object"))
        {
            collideWith = collision.gameObject;
            if (collision.gameObject.layer == 9)
                haveOnion = true;
            else if (collision.gameObject.layer == 10)
                haveCarrot = true;
            else if (collision.gameObject.layer == 12)
            {
                haveCarrot = true;
                haveOnion = true;
            }
        }
    }
}
