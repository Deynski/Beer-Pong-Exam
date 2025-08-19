using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float baseLaunchForce = 10f; // Multiplier for drag strength
    public float verticalForce = 2f;    // Extra upward boost
    public float maxForce = 30f;        // Clamp max power

    [SerializeField] private bool isLaunched = false;
    public bool IsLaunched => isLaunched;

    private Vector3 dragStartPos;
    private Vector3 direction;
    [SerializeField] private float dragDistance;

    private MainControllerSingleton mainControllerSingleton => MainControllerSingleton.Instance;

    void Update()
    {
        if (isLaunched) return;

        // Record drag start
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)
            );
        }

        // Release drag → launch
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 dragEndPos = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)
            );

            direction = dragStartPos - dragEndPos; // opposite of drag
            dragDistance = direction.magnitude;    // distance controls power

            LaunchBall();
            isLaunched = true;
        }
    }

    void LaunchBall() // Optimize
    {
        Rigidbody ballRB = mainControllerSingleton.Ball.BallRigidbody;
        // Reset ball before launch
        ballRB.velocity = Vector3.zero;

        // Scale launch force with drag distance
        float scaledForce = Mathf.Min(dragDistance * baseLaunchForce, maxForce);

        // Calculate velocity
        Vector3 launchVelocity = direction.normalized * scaledForce;
        launchVelocity += Vector3.up * verticalForce; // arc boost

        // Apply velocity
        ballRB.velocity = launchVelocity;
    }

    public void PingPongBounce()
    {
        Rigidbody ballRB = mainControllerSingleton.Ball.BallRigidbody;

        float scaledForce = Mathf.Min(dragDistance * baseLaunchForce, maxForce);

        Vector3 launchVelocity = direction.normalized * scaledForce;

        launchVelocity += Vector3.up * verticalForce;
        ballRB.velocity = launchVelocity;
        isLaunched = false;
        mainControllerSingleton.Ball.BouncedOnce(true);
    }
}
