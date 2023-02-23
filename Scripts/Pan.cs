using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    private GameObject player;

    public GameObject panWithOnion;
    public GameObject panWithCarrot;
    public bool isCookable = false;
    public bool isOnCooker = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cooker"))
            isOnCooker = true;
        if (collision.gameObject.CompareTag(player.tag))
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
        }
        else if (collision.gameObject.CompareTag("food") && collision.gameObject.GetComponent<CutOnion>() == null && isOnCooker)
        {
            isCookable = true;
        }
        else if (collision.gameObject.GetComponent<CutOnion>() != null && collision.gameObject.GetComponent<CutOnion>().isCut)
        {
            Replace(panWithOnion);
        }
        else if (collision.gameObject.GetComponent<CutCarrot>() != null && collision.gameObject.GetComponent<CutCarrot>().isCut)
        {
            Replace(panWithCarrot);
        }
        else isCookable = false;
    }

    private void Replace(GameObject ingredient)
    {
        Vector3 position = transform.position;
        Vector3 angles = transform.eulerAngles;
        Vector3 scale = transform.localScale;
        GameObject withOnion = Instantiate(ingredient);
        withOnion.transform.position = position;
        withOnion.transform.eulerAngles = angles;
        withOnion.transform.localScale = scale;
        Destroy(gameObject);
    }
}
