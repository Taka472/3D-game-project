using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutOnion : MonoBehaviour
{
    public Material cutOnion;
    public bool isCut = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("pan object") && isCut)
        {
            Destroy(gameObject);
        }
    }
}
