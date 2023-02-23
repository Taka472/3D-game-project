using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    public GameObject fire;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("pan object"))
        {
            fire.SetActive(true);
            StartCoroutine(PutOut());
        }
    }

    IEnumerator PutOut()
    {
        yield return new WaitForSeconds(10);
        fire.SetActive(false);
    }
}   
