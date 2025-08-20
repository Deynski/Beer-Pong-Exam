using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public delegate void OnChangeTurn();
    public static event OnChangeTurn onChangeTurn;
    [SerializeField] private Rigidbody ballRigidbody;
    public Rigidbody BallRigidbody => ballRigidbody;

    [SerializeField] private bool isBouncedOnce = false;
    public bool IsBouncedOnce => isBouncedOnce;
    private MainControllerSingleton mainControllerSingleton => MainControllerSingleton.Instance;


    private void FixedUpdate()
    {
        if (ballRigidbody.velocity == Vector3.zero && isBouncedOnce) // Change the turn when the ball stops in the middle of the board
        {
            BouncedOnce(false);
            onChangeTurn?.Invoke();
        }
    }
    private void OnCollisionEnter(Collision collision) // Ball collision checker
    {
        if (collision.gameObject.tag == "table" && (mainControllerSingleton.PlayerController.IsLaunched || mainControllerSingleton.AILauncher.IsLaunched))
        {
            if (mainControllerSingleton.TurnManager.CurrentTurn == TurnManager.Turn.Player)
            {
                mainControllerSingleton.PlayerController.PingPongBounce();
            }
            else
            {
                mainControllerSingleton.AILauncher.PingPongBounce();
            }
        }

        if (collision.gameObject.tag == "wall2" && mainControllerSingleton.TurnManager.CurrentTurn == TurnManager.Turn.Player) // Change turn when the ball hits the wall of the other side
        {
            BouncedOnce(false);
            onChangeTurn?.Invoke();
        }

        if (collision.gameObject.tag == "wall1" && mainControllerSingleton.TurnManager.CurrentTurn == TurnManager.Turn.AI) // Change turn when the ball hits the wall of the other side
        {
            BouncedOnce(false);
            onChangeTurn?.Invoke();
        }

    }

    private void OnTriggerEnter(Collider other) // Ball trigger collision Checker
    {
        if (other.tag == "AICups" && mainControllerSingleton.TurnManager.CurrentTurn == TurnManager.Turn.Player)
        {
            ScoreCollision(other.gameObject);

        }

        if (other.tag == "PlayerCups" && mainControllerSingleton.TurnManager.CurrentTurn == TurnManager.Turn.AI)
        {
            ScoreCollision(other.gameObject);
        }

    }

    private void ScoreCollision(GameObject other) // Change turn when the player or AI scores. Also removes the cups 
    {
        mainControllerSingleton.ScoreManager.RemoveCupAndScore(other.gameObject, mainControllerSingleton.TurnManager.CurrentTurn);
        BouncedOnce(false);
        onChangeTurn?.Invoke();
    }

    public void BouncedOnce(bool bounced)
    {
        isBouncedOnce = bounced;
    }

    public void StopBall()
    {
        ballRigidbody.velocity = Vector3.zero;
    }
}
