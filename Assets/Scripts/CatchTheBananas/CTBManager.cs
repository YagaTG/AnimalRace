using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CTBManager : MonoBehaviour
{
    // Sounds
    public AudioSource CTBMusic;

    // Manages keeping track of the players' count and the winner of the minigame

    [SerializeField] GameObject player1, player2;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] BananaSpawner bananaSpawner;
    [SerializeField] TMP_Text player1ScoreText, player2ScoreText;

    const int WIN_SCORE = 20;

    public int Player1Score { get; set; }
    public int Player2Score { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
        UpdatePlayerScoreTexts();
        StartSpawningBananas();
        CTBMusic.mute = false;
    }

    void StartSpawningBananas()
    {
        StartCoroutine(bananaSpawner.SpawnBananas());
    }

    public void IncrementScore(string playerName)
    {
        if (playerName == "Player1")
        {
            Player1Score++;
        }
        else if (playerName == "Player2")
        {
            Player2Score++;
        }

        UpdatePlayerScoreTexts();
        CheckWin();
    }

    void UpdatePlayerScoreTexts()
    {
        player1ScoreText.text = "<size=80%>(����� 1)</size>\n����: " + Player1Score;
        player2ScoreText.text = "<size=80%>(����� 2)</size>\n����: " + Player2Score;
    }

    public string CheckWin()
    {
        string winner = "��� ����������";
        if (Player1Score == WIN_SCORE)
        {
            winner = player1.name;
            StartCoroutine(ShowGameOver(winner));
        }
        else if (Player2Score == WIN_SCORE)
        {
            winner = player2.name;
            StartCoroutine(ShowGameOver(winner));
        }

        return winner;
    }

    IEnumerator ShowGameOver(string winner)
    {
        CTBMusic.mute = true;
        GetComponent<MinigameEarnCoins>().GameOver(winner);
        gameOverCanvas.SetActive(true);
        bananaSpawner.IsSpawning = false;

        // Pause game for 2 seconds
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;

        GoBackToGameBoard();
    }

    void GoBackToGameBoard()
    {
        GameControl con = FindObjectOfType<GameControl>();
        con.reloadObjs();
        SceneLoader.unloadScene("CatchTheBananas");
    }
}
