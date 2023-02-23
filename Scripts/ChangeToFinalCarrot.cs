using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToFinalCarrot : MonoBehaviour
{
    public GameObject spacialPan;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CutOnion>() == null)
            return;
        if (collision.gameObject.GetComponent<CutOnion>().isCut)
            Replace();
    }

    private void Replace()
    {
        Vector3 position = transform.position;
        Vector3 angle = transform.eulerAngles;
        Vector3 scale = transform.localScale;
        GameObject pan = Instantiate(spacialPan);
        pan.transform.position = position;
        pan.transform.eulerAngles = angle;
        pan.transform.localScale = scale;
        Destroy(gameObject);
    }
}
