using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SkipCutScene : MonoBehaviour
{
    public bool isIntro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AudioManager.Instance.musicSource.Play();
            if (isIntro)
            {
                SceneManager.LoadScene("Tutorial");
            }
            else
            {
                UIManager.currentMenuScene = UIManager.MenuPanels.MAINMENU;
                SceneManager.LoadScene("MainMenu");
            }   
        }
    }
}
