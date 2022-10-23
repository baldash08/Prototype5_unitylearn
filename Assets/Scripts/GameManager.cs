using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject volume;
    public GameObject pausePanel;
    private bool paused;
    private int score;
    private int lives;
    private float spawnRate = 1.0f;
    public bool isGameActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        // activate game
        isGameActive = true;
        score = 0;
        // Set the lives text
        lives = 3;
        livesText.text = "Lives: " + lives;
        // the difficult speed
        spawnRate /= difficulty;
        // starting coroutine
        StartCoroutine(SpawnTarget());
        UpdateScore(score);
        // deactive title screen
        titleScreen.gameObject.SetActive(false);
        volume.gameObject.SetActive(false);
    }

    public int GetLives()
    {
        return this.lives;
    }

    public void SetLives(int lives)
    {
        this.lives -= lives;
        livesText.text = "Lives: " + this.lives;
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        } else
        {
            paused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
