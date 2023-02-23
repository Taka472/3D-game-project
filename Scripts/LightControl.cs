using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public static LightControl instance;
    public Light[] lights;

    private void Start()
    {
        instance = this;
    }

    public void TurnOnLight()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].gameObject.SetActive(true);
        }
    }

    public void TurnOffLight()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].gameObject.SetActive(false);
        }
    }
}
