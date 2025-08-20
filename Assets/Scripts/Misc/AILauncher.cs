using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILauncher : MonoBehaviour
{
    public float baseLaunchForce = 10f; // Multiplier for drag strength
    public float verticalForce = 2f;    // Extra upward boost
    public float maxForce = 30f;        // Clamp max power

    [SerializeField] private bool isLaunched = false;
    public bool IsLaunched => isLaunched; // Check if ball is launched to record a single bounce

    private Vector3 direction;
    [SerializeField] private float dragDistance;

    [Header("AI Settings")]
    public float minDragDistance = 1f; // Min Drag value of AI 
    public float maxDragDistanceAI = 5f; // Max Drag value of AI
    public float launchDelay = 2f; // Timer before the AI launches

    private MainControllerSingleton mainControllerSingleton => MainControllerSingleton.Instance;

    private void OnEnable()
    {
        // AI waits and then launches automatically on enable
        Invoke(nameof(DecideLaunch), launchDelay);
    }

    void DecideLaunch()
    {
        if (isLaunched) return;

        // AI pretends to drag 
        Vector3 randomDir = new Vector3(
            Random.Range(-0.6f, -0.5f), // throws to the left side of the board
            Random.Range(0.2f, 0.7f), // mostly upward
            0f
        );

        direction = randomDir.normalized;

        dragDistance = Random.Range(minDragDistance, maxDragDistanceAI);

        LaunchBall();
        isLaunched = true;
    }

    void LaunchBall()
    {
        Rigidbody ballRB = mainControllerSingleton.Ball.BallRigidbody;
        // Reset ball velocity
        ballRB.velocity = Vector3.zero;

        // Scale launch force with "AI drag distance"
        float scaledForce = Mathf.Min(dragDistance * baseLaunchForce, maxForce);

        // Calculate velocity
        Vector3 launchVelocity = direction * scaledForce;
        launchVelocity += Vector3.up * verticalForce; // arc boost

        // Apply velocity
        ballRB.velocity = launchVelocity;
    }

    public void PingPongBounce()
    {
        Rigidbody ballRB = mainControllerSingleton.Ball.BallRigidbody;

        float scaledForce = Mathf.Min(dragDistance * baseLaunchForce, maxForce);

        Vector3 launchVelocity = direction * scaledForce;

        launchVelocity += Vector3.up * verticalForce;

        ballRB.velocity = launchVelocity;

        isLaunched = false;

        mainControllerSingleton.Ball.BouncedOnce(true);
    }
}


