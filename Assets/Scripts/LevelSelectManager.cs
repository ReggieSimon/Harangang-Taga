using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LevelSelectManager : MonoBehaviour
{
    public Button[] buttons;
    public static int currLevel;
    private void Awake()
    {
 
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }

    }
    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        currLevel = levelId;
        SceneManager.LoadScene(levelName);

    }
}
