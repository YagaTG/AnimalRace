using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameLoadPlayers : MonoBehaviour
{
    // This script can be attached to a game object to load players in from the lobby

    [SerializeField] private GameObject player1, player2;
    [SerializeField] private GameObject player1Accessory, player2Accessory;

    // Load player variables
    private static List<Player> listOfPlayersPlaying; // This will load in the selected players from the lobby
    private List<GameObject> playerObjects;
    private List<GameObject> playerAccessoryObjects;

    // Start is called before the first frame update
    void Start()
    {
        SetPlayerObjectLists();
        SetSelectedPlayers();
        LoadPlayerDataToPlayerObjects();
    }

    void SetPlayerObjectLists()
    {
        playerObjects = new List<GameObject>() { player1, player2 };
        playerAccessoryObjects = new List<GameObject>() { player1Accessory, player2Accessory };
    }

    void SetSelectedPlayers()
    {
        listOfPlayersPlaying = LobbyManager.getListOfPlayersPlaying();
    }

    void LoadPlayerDataToPlayerObjects()
    {
        for (int i = 0; i < listOfPlayersPlaying.Count; i++)
        {
            playerObjects[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Character Face Sprites/" + listOfPlayersPlaying[i].getAnimal());
            playerObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = "<size=60%>(����� " + (i + 1) + ")</size>\n" + listOfPlayersPlaying[i].getName();
            playerAccessoryObjects[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cosmetic Sprites/" + listOfPlayersPlaying[i].getAccessory());
        }
    }

    public static List<Player> GetListOfPlayersPlaying()
    {
        return listOfPlayersPlaying;
    }
}
