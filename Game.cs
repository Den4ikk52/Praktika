using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int whoTurn;                 //0 = x and 1 = o
    public int turncount;               //counts the number of turn played
    public GameObject[] turnIcons;      //displays whos turn it is
    public Sprite[] playIcons;          //0 = x icon and 1 = y icon
    public Button[] tictactoeSpaces;    // playable space for our game
    public int[] markedSpaces;          //keeps track of which spaces have been marked 
    public Text winnerText;          //text to display the winner
    public GameObject[] winningLine;       //panel to display the winner

    // Use this for initialization
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whoTurn = 0;
        turncount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }

        // Initialize markedSpaces to -1 (indicating unmarked)
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
        for (int i = 0; i < winningLine.Length; i++)
        {
            winningLine[i].SetActive(false);
        }

        winnerText.gameObject.SetActive(false);
    }

    void Update()
    {

    }

    public void TicTacToeButton(int whichNumber)
    {
        tictactoeSpaces[whichNumber].image.sprite = playIcons[whoTurn];
        tictactoeSpaces[whichNumber].interactable = false;

        markedSpaces[whichNumber] = whoTurn + 1; // Mark the space with the current player's number
        turncount++;

        if (turncount > 4) // Check for a winner only after 5 turns
        {
            WinnerCheck();
        }
        if (whoTurn == 0)
        {
            whoTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    void WinnerCheck()
    {
        int[,] winCombos = new int[,]
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // рядки
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // стовпчики
            {0, 4, 8}, {2, 4, 6}             // діагоналі
        };

        for (int i = 0; i < 8; i++)
        {
            int a = winCombos[i, 0];
            int b = winCombos[i, 1];
            int c = winCombos[i, 2];

            if (markedSpaces[a] != -100 &&
                markedSpaces[a] == markedSpaces[b] &&
                markedSpaces[b] == markedSpaces[c])
            {
                WinnerDisplay(i);
                return;
            }
        }
    }

    void WinnerDisplay(int indexIn)
    {
        winnerText.gameObject.SetActive(true);

        int winner = markedSpaces[indexIn]; // тут уже точно переможець
        if (winner == 1)
        {
            winnerText.text = "Player O Wins!";
        }
        else if (winner == 2)
        {
            winnerText.text = "Player X Wins!";
        }

        winningLine[indexIn].SetActive(true);

        foreach (var space in tictactoeSpaces)
            space.interactable = false;
    }
}