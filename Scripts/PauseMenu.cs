using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject achievementPage;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider sensitiveSlider;

    public DesktopMovement player;
    private void Awake()
    {
        gameObject.SetActive(false);
        musicSlider.value = PlayerPrefs.GetFloat("music");
        sfxSlider.value = PlayerPrefs.GetFloat("sfx");
        sensitiveSlider.value = PlayerPrefs.GetFloat("sensitive");
    }

    public void BackToGame()
    {
        PlayerPrefs.SetFloat("music", musicSlider.value);
        PlayerPrefs.SetFloat("sfx", sfxSlider.value);
        PlayerPrefs.SetFloat("sensitive", sensitiveSlider.value);
        player.pauseIsOn = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.mouseSen = 90 + 180 * sensitiveSlider.value;
        AudioControl.instance.VolumeChange();
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        StarControl.gameStart = false;
        DataManager.instance.SaveData();
        SceneManager.LoadScene(0);
    }

    public void OpenAchievement()
    {
        achievementPage.SetActive(true);
    }

    public void CloseAchievement()
    {
        achievementPage.SetActive(false);
    }

    public void QuitGame()
    {
        StarControl.gameStart = false;
        DataManager.instance.SaveData();
        Application.Quit();
    }
}
