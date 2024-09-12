using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CutSceneManager : MonoBehaviour
{
    public float sec;
    public int menuNum;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.musicSource.Pause();
        UIManager.introWatched = true;
        StartCoroutine(GoBack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator GoBack()
    {
        yield return new WaitForSeconds(sec);
        AudioManager.Instance.musicSource.Play();
        switch (menuNum)
        {
            case 0:
                UIManager.currentMenuScene = UIManager.MenuPanels.MAINMENU;
                SceneManager.LoadScene("MainMenu");
                break;
            case 1:
                SceneManager.LoadScene("Tutorial");
                break;
            case 2:
                UIManager.currentMenuScene = UIManager.MenuPanels.SETTINGS;
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
