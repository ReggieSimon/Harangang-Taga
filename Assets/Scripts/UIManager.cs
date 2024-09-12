
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject settingsPanel;
    public GameObject menuPanel;
    public GameObject levelSelectPanel;

    public static bool introWatched;

    public enum MenuPanels
    {
        MAINMENU,
        LEVELSELECT,
        SETTINGS
    }

    public static MenuPanels currentMenuScene = MenuPanels.MAINMENU;
    private void Awake()
    {
        switch (currentMenuScene)
        {
            case MenuPanels.MAINMENU: ShowMainMenu();
                break;
            case MenuPanels.LEVELSELECT:
                LevelSelectMenu();
                break;
            case MenuPanels.SETTINGS:
                ChangeScene_Settings();
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Settings()
    {
        settingsPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
    }
    public void LevelSelectMenu()
    {
        if (!introWatched)
        {
            SceneManager.LoadScene("IntroCutscene");
        }
        else
        {
            levelSelectPanel.SetActive(true);
        }   
    }
    public void ShowMainMenu()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
    }

    public void ChangeScene_MainMenu()
    {
        currentMenuScene = MenuPanels.MAINMENU;
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangeScene_Settings()
    {
        settingsPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
        menuPanel.SetActive(false);
    }


    public void Quitgame()
    {
        Application.Quit();
    }

    public void SettingsBack()
    {
        if (SceneManager.loadedSceneCount > 1)
        {
            SceneManager.UnloadSceneAsync("MainMenu");
        }
        else
        {
            ShowMainMenu();
        }

    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void UnlockAllLevels()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 5);
        PlayerPrefs.SetInt("ReachedIndex", 5);
        PlayerPrefs.Save();
    }
}
