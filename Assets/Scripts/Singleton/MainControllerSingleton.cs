using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControllerSingleton : Singleton<MainControllerSingleton>
{
    // Private classes
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AILauncher aILauncherController;
    [SerializeField] private Ball ballController;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private ScoreManager scoreManager;

    // Encapsulated classes into public variables
    public PlayerController PlayerController => playerController;
    public AILauncher AILauncher => aILauncherController;
    public Ball Ball => ballController;
    public TurnManager TurnManager => turnManager;
    public ScoreManager ScoreManager => scoreManager;


    
}
