using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Turn currentTurn = Turn.Player;
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private Transform aiSpawnPosition;

    public Turn CurrentTurn => currentTurn;

    private MainControllerSingleton mainControllerSingleton => MainControllerSingleton.Instance;

    private void Start() // Temporary
    {
        //ball.transform.position = aiSpawnPosition.position;
        mainControllerSingleton.Ball.transform.position = aiSpawnPosition.position;
    }

    private void OnEnable()
    {
        Ball.onChangeTurn += ChangeTurn;
    }

    private void OnDisable()
    {
        Ball.onChangeTurn -= ChangeTurn;
    }

    private void ChangeTurn()
    {
        mainControllerSingleton.Ball.StopBall();
        if (currentTurn == Turn.Player) // Optimize
        {
            currentTurn = Turn.AI;
            mainControllerSingleton.Ball.transform.position = aiSpawnPosition.position;
            mainControllerSingleton.PlayerController.enabled = false;
            mainControllerSingleton.AILauncher.enabled = true;
        }
        else // Optimize
        {
            currentTurn = Turn.Player;
            mainControllerSingleton.Ball.transform.position = playerSpawnPosition.position;
            mainControllerSingleton.AILauncher.enabled = false;
            mainControllerSingleton.PlayerController.enabled = true;
        }

        Debug.Log(currentTurn.ToString());

    }

    private IEnumerator DelayTurnChange()
    {
        yield return new WaitForSeconds(2);

    }

    public enum Turn
    {
        Player,
        AI
    }
}
