using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerController controller;
    [SerializeField] private AILauncher aiLauncher;

    [SerializeField] private TurnManager turnManager;



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

    }
}
