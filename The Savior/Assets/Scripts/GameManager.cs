using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Opening,
        Started,
        Over,
    }

    [SerializeField]
    GameState gameState = GameState.Opening;

    int score;
    int followerCount;
    int idleScoreMultiplierPerFollower = 1;
    int directScorePerFollowerGain = 10;
    int directScorePerFollowerLost = 10;

    int lives = 5;

    [SerializeField]
    GameObject gameStart;

    [SerializeField]
    GameObject gameHUD;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text finalScoreText;

    [SerializeField]
    Text livesText;
    
    [SerializeField]
    GameObject gameOver;

    private void OnEnable()
    {
        Actions.OnGameOpening += OnGameOpening;
        Actions.OnGameStarted += OnGameStarted;
        Actions.OnIncreaseFollowerCount += OnIncreaseFollowerCount;
        Actions.OnDecreaseFollowerCount += OnDecreaseFollowerCount;
        Actions.OnTouch += OnTouch;
        Actions.OnGameOver += OnGameOver;
        /*
        Actions.OnSpawn += OnSpawn;
        */
    }

    private void OnDisable()
    {
        Actions.OnGameOpening -= OnGameOpening;
        Actions.OnGameStarted -= OnGameStarted;
        Actions.OnIncreaseFollowerCount -= OnIncreaseFollowerCount;
        Actions.OnDecreaseFollowerCount -= OnDecreaseFollowerCount;
        Actions.OnTouch -= OnTouch;
        Actions.OnGameOver -= OnGameOver;
        /*
        Actions.OnSpawn -= OnSpawn;
        */
    }

    private void Start()
    {
        OnGameOpening();
    }

    void OnGameOpening()
    {
        gameState = GameState.Opening;
        gameStart.SetActive(true);
        gameHUD.SetActive(false);
        gameOver.SetActive(false);
    }

    void OnGameStarted()
    {
        gameState = GameState.Started;
        gameStart.SetActive(false);
        gameHUD.SetActive(true);
        gameOver.SetActive(false);

        followerCount = 0;
        score = 0;
        UpdateScore();
        lives = 5;
        UpdateLives();
        StartCoroutine(IdleFollowerCountScore());
    }

    void OnGameOver()
    {
        finalScoreText.text = score.ToString();
        gameState = GameState.Over;
        gameStart.SetActive(false);
        gameHUD.SetActive(false);
        gameOver.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(gameState == GameState.Opening)
            {
                Actions.OnGameStarted.Invoke();
            }
            if (gameState == GameState.Over)
            {
                Actions.OnGameOpening.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnTouch()
    {
        lives -= 1;
        UpdateLives();
        if (lives == 0)
        {
            Actions.OnGameOver.Invoke();
        }
    }

    void OnIncreaseFollowerCount()
    {
        IncreaseScore(directScorePerFollowerGain);
        followerCount += 1;
    }

    void OnDecreaseFollowerCount()
    {
        DecreaseScore(directScorePerFollowerLost);
        followerCount -= 1;
    }

    void IncreaseScore(int _score)
    {
        score += _score;
        UpdateScore();
    }

    void DecreaseScore(int _score)
    {
        score -= _score;
        UpdateScore();
    }

    IEnumerator IdleFollowerCountScore()
    {
        score += followerCount * idleScoreMultiplierPerFollower;
        UpdateScore();
        yield return new WaitForSeconds(1f);
        StartCoroutine(IdleFollowerCountScore());
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateLives()
    {
        livesText.text = "";

        for(int i = 0; i < lives; i++)
        {
            livesText.text += "I";
        }
    }
}
