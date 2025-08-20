using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public delegate void OnGameOver();
    public static event OnGameOver onGameOver; // Game over event
    [SerializeField] private List<GameObject> playerCups;
    [SerializeField] private List<GameObject> aiCups;

    [SerializeField] private ScoreManagerView view; // UI container
    public bool playerWon = false;
    public bool aiWon = false;

    public void RemoveCupAndScore(GameObject cup, TurnManager.Turn currentTurn) // Scoring visualization, the cup will be removed and disabled
    {
        cup.transform.parent.gameObject.SetActive(false);
        if (currentTurn == TurnManager.Turn.Player)
        {
            aiCups.Remove(cup.transform.parent.gameObject);
            view.ModifyAIGauge();
            CheckAIStatus();
        }
        else
        {
            playerCups.Remove(cup.transform.parent.gameObject);
            view.ModifyPlayerGauge();
            CheckPlayerStatus();
        }

        if (playerCups.Count == 0 || aiCups.Count == 0)
        {
            onGameOver?.Invoke();
        }
    }

    private void CheckPlayerStatus() //  Change status of the player depending on remaining cups
    {
        if (playerCups.Count <= 4)
        {
            view.SetPlayerStatus("Player is nearly drunk", Color.yellow);
        }

        if (playerCups.Count == 1)
        {
            view.SetPlayerStatus("Player is really drunk", Color.red);
        }

        if (playerCups.Count == 0)
        {
            view.SetPlayerStatus("Player blacked out", Color.red);
            aiWon = true;
        }
    }

    private void CheckAIStatus() //  Change status of the AI depending on remaining cups
    {
        if (aiCups.Count <= 4)
        {
            view.SetAIStatus("Computer is nearly drunk", Color.yellow);
        }

        if (aiCups.Count == 1)
        {
            view.SetAIStatus("Computer is really drunk", Color.red);
        }

        if (aiCups.Count == 0)
        {
            view.SetAIStatus("Computer blacked out", Color.red);
            playerWon = true;
        }
    }

}
