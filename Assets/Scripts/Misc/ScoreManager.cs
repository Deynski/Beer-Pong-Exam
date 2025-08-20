using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerCups;
    [SerializeField] private List<GameObject> aiCups;

    [SerializeField] private int playerScore = 0;
    [SerializeField] private int aiScore = 0;

    public void RemoveCupAndScore(GameObject cup, TurnManager.Turn currentTurn)
    {
        cup.transform.parent.gameObject.SetActive(false);
        if (currentTurn == TurnManager.Turn.Player)
        {
            aiCups.Remove(cup);
            playerScore++;
        }
        else
        {
            playerCups.Remove(cup);
            aiScore++;
        }

        if (playerCups.Count == 0 || aiCups.Count == 0)
        {
            
        }
    }

}
