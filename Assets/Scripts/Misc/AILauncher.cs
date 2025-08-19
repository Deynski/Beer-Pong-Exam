using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILauncher : MonoBehaviour
{
    public Rigidbody ballPrefab;
    public float baseLaunchForce = 10f; // Multiplier for drag strength
    public float verticalForce = 2f;    // Extra upward boost
    public float maxForce = 30f;        // Clamp max power

    [SerializeField] private bool isLaunched = false;
    public bool IsLaunched => isLaunched;

    private Vector3 direction;
    [SerializeField] private float dragDistance;

    [Header("AI Settings")]
    public float minDragDistance = 1f;
    public float maxDragDistanceAI = 5f;
    public float launchDelay = 2f; // seconds before AI launches

    private void Start()
    {
        // AI waits and then launches automatically
        Invoke(nameof(DecideLaunch), launchDelay);
    }

    private void Update()
    {
        // Record drag start
        
    }

    void DecideLaunch()
    {
        if (isLaunched) return;

        // AI "pretends" to drag: pick random direction & power
        Vector3 randomDir = new Vector3(
            Random.Range(-1f, 1f), // random X
            Random.Range(0.2f, 1f), // mostly upward
            0f
        );

        direction = randomDir.normalized;

        dragDistance = Random.Range(minDragDistance, maxDragDistanceAI);

        LaunchBall();
        isLaunched = true;
    }

    void LaunchBall()
    {
        // Reset ball velocity
        ballPrefab.velocity = Vector3.zero;

        // Scale launch force with "AI drag distance"
        float scaledForce = Mathf.Min(dragDistance * baseLaunchForce, maxForce);

        // Calculate velocity
        Vector3 launchVelocity = direction * scaledForce;
        launchVelocity += Vector3.up * verticalForce; // arc boost

        // Apply velocity
        ballPrefab.velocity = launchVelocity;
    }

    public void PingPongBounce()
    {
        float scaledForce = Mathf.Min(dragDistance * baseLaunchForce, maxForce);
        Vector3 launchVelocity = direction * scaledForce;
        launchVelocity += Vector3.up * verticalForce;
        ballPrefab.velocity = launchVelocity;
        isLaunched = false;
        Debug.Log("Is Launched should be disabled");

        // Schedule AI to launch again
        Invoke(nameof(DecideLaunch), launchDelay);
    }
}


