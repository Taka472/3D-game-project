using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public float loadingTime = 5;
    public Button startGame;
    public Button resumeGame;
    public Button setting;
    public Button quitButton;
    public Image background;
    public Image loadingBackground;
    public GameObject titleScreen;
    public GameObject loadingIcon;
    public Image settingMenu;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider sensitiveSlider;
    public AudioControl audioControl;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("music");
        sfxSlider.value = PlayerPrefs.GetFloat("sfx");
        sensitiveSlider.value = PlayerPrefs.GetFloat("sensitive");
    }

    public void NewGame(int sceneIndex)
    {
        DataManager.startNew = true;
        CutsceneControl.intro = true;
        CutsceneControl.tutorial = true;
        StartCoroutine(Loading(sceneIndex));
    }

    public void ResumeGame(int sceneIndex)
    {
        DataManager.startNew = false;
        CutsceneControl.intro = true;
        CutsceneControl.tutorial = true;
        StartCoroutine(Loading(sceneIndex));
    }
    
    IEnumerator Loading(int sceneIndex)
    {
        startGame.gameObject.SetActive(false);
        resumeGame.gameObject.SetActive(false);
        setting.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        titleScreen.SetActive(false);
        loadingBackground.gameObject.SetActive(true);
        loadingIcon.SetActive(true);
        float timer = Time.time;
        while (Time.time - timer < loadingTime)
        {
            yield return null;
        }
        yield return LoadSceneAsync(sceneIndex);
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone) {
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSetting()
    {
        titleScreen.gameObject.SetActive(false);
        settingMenu.gameObject.SetActive(true);
        musicSlider.value = PlayerPrefs.GetFloat("music");
        sfxSlider.value = PlayerPrefs.GetFloat("sfx");
        sensitiveSlider.value = PlayerPrefs.GetFloat("sensitive");
    }

    public void CloseSetting()
    {
        PlayerPrefs.SetFloat("music", musicSlider.value);
        PlayerPrefs.SetFloat("sfx", sfxSlider.value);
        PlayerPrefs.SetFloat("sensitive", sensitiveSlider.value);
        audioControl.VolumeChange();
        settingMenu.gameObject.SetActive(false);
        titleScreen.SetActive(true);
    }
}
