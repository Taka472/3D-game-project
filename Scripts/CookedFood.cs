using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedFood : MonoBehaviour
{
    public GameObject cookedFood;
    public float cookedTime;
    public bool haveOnion = false;
    public bool haveCarrot = false;
    public GameObject collideWith;

    private bool isCooking = false;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("pan object"))
        {
            gameObject.tag = "cooking food";
            collideWith = collision.gameObject;
            if (collision.gameObject.GetComponent<Pan>() != null)
            {
                Pan pan = collision.gameObject.GetComponent<Pan>();
                if (pan.isCookable)
                {
                    if (!isCooking)
                    {
                        StartCoroutine(Cooking());
                        isCooking = false;
                    }
                }
            }
            else if (collision.gameObject.GetComponent<PanWithOnion>() != null)
            {
                PanWithOnion pan = collision.gameObject.GetComponent<PanWithOnion>();
                if (pan.isCookable)
                {
                    if (!isCooking)
                    {
                        haveOnion = true;
                        StartCoroutine(Cooking());
                        isCooking = true;
                    }
                }
            }
            else if (collision.gameObject.GetComponent<PanWithCarrot>() != null)
            {
                PanWithCarrot pan = collision.gameObject.GetComponent<PanWithCarrot>();
                if (pan.isCookable)
                {
                    if (!isCooking)
                    {
                        haveOnion = true;
                        StartCoroutine(Cooking());
                        isCooking = true;
                    }
                }
            }
        }
    }

    private IEnumerator Cooking()
    {
        yield return new WaitForSeconds(cookedTime);
        Cooked();
    }
    
    private void Cooked()
    {
        Vector3 position = transform.position;
        Vector3 angles = transform.eulerAngles;
        Vector3 scale = transform.localScale;
        gameObject.SetActive(false);
        Destroy(GetComponent<Collider>());
        GameObject CookFood = Instantiate(cookedFood);
        CookFood.transform.position = position;
        CookFood.transform.eulerAngles = angles;
        CookFood.transform.localScale = scale;
        CookFood.AddComponent<Rigidbody>();
        Destroy(gameObject);
    }
}
