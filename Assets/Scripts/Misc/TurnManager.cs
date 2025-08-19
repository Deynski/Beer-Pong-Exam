using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public delegate void OnChangeTurn();
    public static event OnChangeTurn onChangeTurn;
    // Start is called before the first frame update
    [SerializeField] private Turn currentTurn = Turn.Player;
    public Turn CurrentTurn => currentTurn;

    private void OnEnable()
    {
        onChangeTurn += ChangeTurn;
    }

    private void OnDisable()
    {
        onChangeTurn -= ChangeTurn;
    }

    private void ChangeTurn()
    {
        if (currentTurn == Turn.Player)
        {
            currentTurn = Turn.AI;
        }
        else
        {
            currentTurn = Turn.Player;
        }
    }

    public enum Turn
    {
        Player,
        AI
    }
}
