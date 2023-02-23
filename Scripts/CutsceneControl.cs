using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneControl : MonoBehaviour
{
    public GameObject player;
    public SpawnPeople spawner;

    public static bool intro = true;
    public static bool tutorial = true;

    public GameObject tutorialCutscene;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (intro && !tutorialCutscene.activeSelf)
            {
                intro = false;
                EndIntroCutscene();
            }
            else if (tutorial && tutorialCutscene.activeSelf)
            {
                tutorial = false;
                EndTutorialCutscene();
            }
        }
    }

    public void StartTutorial()
    {
        tutorial = true;
    }

    public void PlayCutscene()
    {
        player.SetActive(false);
    }

    public void EndTutorialCutscene()
    {
        spawner.SpawnCustomer();
        player.SetActive(true);
        gameObject.SetActive(false);
    }

    public void EndIntroCutscene()
    {
        intro = false;
        tutorialCutscene.SetActive(true);
        gameObject.SetActive(false);
    }

    public void EndLoseCutscene()
    {
        Cursor.lockState = CursorLockMode.None;
        DataManager.instance.CreateNew();
        StartCoroutine(BackToMenu());
    }

    public IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
