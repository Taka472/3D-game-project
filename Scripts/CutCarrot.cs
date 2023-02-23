using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutCarrot : MonoBehaviour
{
    public GameObject cutCarrot;
    public bool isCut = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("pan object") && isCut)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
