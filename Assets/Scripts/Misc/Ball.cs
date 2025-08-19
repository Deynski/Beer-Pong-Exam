using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public delegate void OnChangeTurn();
    public static event OnChangeTurn onChangeTurn;

    [SerializeField] private PlayerController controller;
    [SerializeField] private AILauncher aiLauncher;

    [SerializeField] private TurnManager turnManager;

    [SerializeField] private Rigidbody ballRigidbody;
    public Rigidbody BallRigidbody => ballRigidbody;

    [SerializeField] private bool isBouncedOnce = false;
    public bool IsBouncedOnce => isBouncedOnce;


    private MainControllerSingleton mainControllerSingleton => MainControllerSingleton.Instance;


    private void FixedUpdate()
    {
        if (ballRigidbody.velocity == Vector3.zero && isBouncedOnce)
        {
            BouncedOnce(false);
            onChangeTurn?.Invoke();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "table" && (controller.IsLaunched || aiLauncher.IsLaunched))
        {
            if (turnManager.CurrentTurn == TurnManager.Turn.Player)
            {
                controller.PingPongBounce();
            }
            else
            {
                aiLauncher.PingPongBounce();
            }
        }

        if (collision.gameObject.tag == "wall")
        {
            BouncedOnce(false);
            onChangeTurn?.Invoke();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AICups" && mainControllerSingleton.TurnManager.CurrentTurn == TurnManager.Turn.Player) // Optimize
        {
            mainControllerSingleton.ScoreManager.RemoveCupAndScore(other.gameObject, mainControllerSingleton.TurnManager.CurrentTurn);
            BouncedOnce(false);
            onChangeTurn?.Invoke();

        }

        if (other.tag == "PlayerCups" && mainControllerSingleton.TurnManager.CurrentTurn == TurnManager.Turn.AI) // Optimize
        {
            mainControllerSingleton.ScoreManager.RemoveCupAndScore(other.gameObject, mainControllerSingleton.TurnManager.CurrentTurn);
            BouncedOnce(false);
            onChangeTurn?.Invoke();
        }

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
