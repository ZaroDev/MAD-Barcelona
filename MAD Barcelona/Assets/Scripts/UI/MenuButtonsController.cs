using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtonsController : MonoBehaviour
{
    public GameObject sureToExit;
    bool showExit;
    public void Awake()
    {
        showExit = false;
        sureToExit.SetActive(showExit);
    }
    public void SwichExit()
    {
        showExit = !showExit;
        sureToExit.SetActive(showExit);
    }
    public void ExitButton()
    {
        showExit = true;
        sureToExit.SetActive(showExit);
    }
    public void NoExit()
    {
        showExit = false;
        sureToExit.SetActive(showExit);
    }
    public void YesExit()
    {
        Application.Quit();
    }
    public void NewGame()
    {

    }
    public void Continue()
    {

    }
    public void Options()
    {

    }
    public void Credits()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }
}
