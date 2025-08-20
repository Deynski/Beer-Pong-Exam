using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Turn currentTurn = Turn.Player;
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private Transform aiSpawnPosition;

    public Turn CurrentTurn => currentTurn;

    private MainControllerSingleton mainControllerSingleton => MainControllerSingleton.Instance;

    private void Start() // AI goes first
    {
        mainControllerSingleton.Ball.transform.position = aiSpawnPosition.position;
    }

    private void OnEnable()
    {
        Ball.onChangeTurn += ChangeTurn;
        ScoreManager.onGameOver += DisablePlayers;
    }
    private void OnDisable()
    {
        Ball.onChangeTurn -= ChangeTurn;
        ScoreManager.onGameOver -= DisablePlayers;
    }

    private void DisablePlayers()
    {
        Ball.onChangeTurn -= ChangeTurn; // unsubscribe to change turn
        
        mainControllerSingleton.AILauncher.enabled = false;
        mainControllerSingleton.PlayerController.enabled = false;
    }

    private void ChangeTurn() // Change the turn of the game
    {
        mainControllerSingleton.Ball.StopBall(true);
        if (currentTurn == Turn.Player)
        {
            TurnChangeValues(Turn.AI, aiSpawnPosition, false);
        }
        else
        {
            TurnChangeValues(Turn.Player, playerSpawnPosition, true);
        }

        Debug.Log(currentTurn.ToString());

    }

    private void TurnChangeValues(Turn turn, Transform spawnPosition, bool playerEnabled) //  Turn values will be set here
    {
        currentTurn = turn;
        mainControllerSingleton.Ball.transform.position = spawnPosition.position;
        mainControllerSingleton.PlayerController.enabled = playerEnabled;
        mainControllerSingleton.AILauncher.enabled = !playerEnabled;
        mainControllerSingleton.Ball.StopBall(false);
    }


    public enum Turn
    {
        Player,
        AI
    }
}
