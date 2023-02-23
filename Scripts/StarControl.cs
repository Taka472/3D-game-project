using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarControl : MonoBehaviour
{
    private readonly int MaxMeter = 5;
    public static float currentMeter = 3f;
    public static float difficult = 0.025f;
    public static bool gameStart = false;
    public Image[] starMeter;
    public DesktopMovement player;

    public CutsceneControl controller;
    private bool lose = false;

    private void Start()
    {
        starMeter[0].fillAmount = currentMeter / MaxMeter;
        starMeter[1].fillAmount = currentMeter / MaxMeter;
        starMeter[2].fillAmount = currentMeter / MaxMeter;
        starMeter[3].fillAmount = currentMeter / MaxMeter;
        starMeter[4].fillAmount = currentMeter / MaxMeter;
    }

    private void Update()
    {
        if (!gameStart || player.pauseIsOn || player.upgradeIsOn || player.guideIsOn)
            return;
        currentMeter -= Time.deltaTime * difficult;
        starMeter[0].fillAmount = currentMeter / MaxMeter;
        starMeter[1].fillAmount = currentMeter / MaxMeter;
        starMeter[2].fillAmount = currentMeter / MaxMeter;
        starMeter[3].fillAmount = currentMeter / MaxMeter;
        starMeter[4].fillAmount = currentMeter / MaxMeter;
        if (currentMeter <= 0 && !lose)
        {
            controller.gameObject.SetActive(true);
            controller.PlayCutscene();
            lose = true;
        }
    }
}