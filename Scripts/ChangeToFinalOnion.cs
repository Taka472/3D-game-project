using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToFinalOnion : MonoBehaviour
{
    public GameObject SpecialPan;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("carrotSlice"))
            Replace();
    }

    private void Replace()
    {
        Vector3 position = transform.position;
        Vector3 angle = transform.eulerAngles;
        Vector3 scale = transform.localScale;
        GameObject pan = Instantiate(SpecialPan);
        pan.transform.position = position;
        pan.transform.eulerAngles = angle;
        pan.transform.localScale = scale;
        Destroy(gameObject);
    }
}
