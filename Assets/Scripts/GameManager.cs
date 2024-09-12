using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject[] players;

    public bool levelCleared;
    public bool gameOver;

    public GameObject winPanel;
    public GameObject gameOverPanel;

    public GameObject instructionsTxt;

    public List<string> instructions = new List<string> { };

    public LayerMask layerMask;
    // Start is called before the first frame update
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        instructionsTxt.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-238,190), 0.20f);
        StartCoroutine(StopIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (hit.transform.GetComponent<PlayerMovement>())
                {
                    UnselectPlayers();
                    hit.transform.GetComponent<PlayerMovement>().selected = true;
                }
            }
        }
    }

    public void UnselectPlayers()
    {
        foreach (GameObject p in players)
        {
            p.GetComponent<PlayerMovement>().selected = false;
        }  
    }

    public void LevelCleared()
    {
        winPanel.SetActive(true);
        UnlockNewLevel();
        Time.timeScale = 1f;       
    }

    public void GameOver()
    {
        AudioManager.Instance.PlaySFX("TAYA");
        Time.timeScale = 0f;
        StartCoroutine(GameOverDelay());
    }

    public IEnumerator GameOverDelay()
    {
        yield return new WaitForSecondsRealtime(2f);
        gameOverPanel.SetActive(true);
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    private IEnumerator StopIntro()
    {
        yield return new WaitForSeconds(10);
        instructionsTxt.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-575, 190), 0.05f);
    }
}



