using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ETLController : MonoBehaviour
{
    // Sounds
    public AudioSource ETLMusic;

    public PlayerController p1;
    public PlayerController p2;

    [SerializeField]
    private GameObject GameOverCanvas;
    [SerializeField]
    private TextMeshProUGUI gameOverText;
    public List<GameObject> boardObjects = new List<GameObject>();

    // Constants for the amount of coins earned
    private const int WINNING_COINS = 6;
    private const int LOSING_COINS = 2;

    private void Start()
    {
        GameOverCanvas.SetActive(false);
        //goBackButton.onClick.AddListener(delegate { goBack(); });
        ETLMusic.mute = false;
    }

    //Defunct
    public void getBoardObjects(List<GameObject> objects)
    {
        this.boardObjects = objects;
        foreach(GameObject o in boardObjects)
        {
            o.SetActive(false);
        }
    }

    //This function is called by a player controller when that player collides with the lion
    public void PlayerDies(string name)
    {
        p1.playerJumpingAudio.mute = true;
        p2.playerJumpingAudio.mute = true;
        ObstacleController.gameOver = true;
        ETLMusic.mute = true;

        int winIndex = -1;
        int loseIndex = -1;

        GameOverCanvas.SetActive(true);
        if (name == "Player 1")
        {
            gameOverText.SetText(MinigameLoadPlayers.GetListOfPlayersPlaying()[0].getName() + " ����, " + MinigameLoadPlayers.GetListOfPlayersPlaying()[1].getName() + " �������!");

            winIndex = 1;
            loseIndex = 0;
        }
        else if(name == "Player 2")
        {
            gameOverText.SetText(MinigameLoadPlayers.GetListOfPlayersPlaying()[1].getName() + " ����, " + MinigameLoadPlayers.GetListOfPlayersPlaying()[0].getName() + " �������!");
            winIndex = 0;
            loseIndex = 1;
        }

        EarnCoins(winIndex, loseIndex);
        StartCoroutine(GameOverPause());
    }

    private void EarnCoins(int winIndex, int loseIndex)
    {
        MinigameLoadPlayers.GetListOfPlayersPlaying()[winIndex].changeGameBalanceByAmount(WINNING_COINS);
        MinigameLoadPlayers.GetListOfPlayersPlaying()[loseIndex].changeGameBalanceByAmount(LOSING_COINS);
    }

    IEnumerator GameOverPause()
    {
        // Pause game for 2 seconds
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;

        GoBackToGameBoard();
    }

    private void GoBackToGameBoard()
    {
        GameControl con = FindObjectOfType<GameControl>();
        con.reloadObjs();
        SceneLoader.unloadScene("EscapeTheLions");
    }
}
