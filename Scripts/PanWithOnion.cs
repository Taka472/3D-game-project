using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanWithOnion : MonoBehaviour
{
    private GameObject player;
    public bool isCookable = false;
    public bool isOnCooker = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(player.tag))
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
        }
        else if (collision.gameObject.CompareTag("food"))
        {
            isCookable = true;
        }
        else isCookable = false;
    }
}
