using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class DetectVR : MonoBehaviour
{
    public GameObject desktopPlayer;
    public GameObject vrPlayer;

    // Start is called before the first frame update
    void Start()
    {
        var xrSetting = XRGeneralSettings.Instance;
        if (xrSetting == null)
        {
            Debug.Log("XR setting is null");
            return;
        }

        var xrManager = xrSetting.Manager;
        if (xrManager == null)
        {
            Debug.Log("XR manager is null");
            return;
        }

        var xrLoader = xrManager.activeLoader;
        if (xrLoader == null)
        {
            Debug.Log("XR loader is null");
            vrPlayer.SetActive(false);
            desktopPlayer.SetActive(true);
            return;
        }

        vrPlayer.SetActive(true);
        desktopPlayer.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
