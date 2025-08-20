using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManagerView : MonoBehaviour
{
    [SerializeField] private Slider aiGauge;
    [SerializeField] private Slider playerGauge;

    [SerializeField] private TextMeshProUGUI playerStatusText;
    [SerializeField] private TextMeshProUGUI aiStatus;

    [SerializeField] private Transform gameOverScreen;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private MainControllerSingleton mainControllerSingleton => MainControllerSingleton.Instance;

    private void Start()
    {
        aiGauge.value = 1;
        playerGauge.value = 1;
    }

    private void OnEnable()
    {
        ScoreManager.onGameOver += GameOverScreenPopup;
    }
    private void OnDisable()
    {
        ScoreManager.onGameOver -= GameOverScreenPopup;
    }

    private void GameOverScreenPopup()
    {
        gameOverScreen.DOScale(Vector3.one, 1f);

        if (mainControllerSingleton.ScoreManager.playerWon)
        {
            gameOverText.text = "GAME OVER! CONGRATULATIONS ON WINNING!";
        }

        if (mainControllerSingleton.ScoreManager.aiWon)
        {
            gameOverText.text = "GAME OVER! YOU WE'RE DEFEATED!";
        }
    }

    public void SetPlayerStatus(string status, Color color)
    {
        playerStatusText.text = status;
        playerStatusText.color = color;
    }

    public void SetAIStatus(string status, Color color)
    {
        aiStatus.text = status;
        aiStatus.color = color;
    }

    public void ModifyPlayerGauge()
    {
        float valueToSubtract = 1f / 6f;
        Debug.Log(valueToSubtract);
        playerGauge.value -= valueToSubtract;
    }

    public void ModifyAIGauge()
    {
        float valueToSubtract = 1f / 6f;
        Debug.Log(valueToSubtract);
        aiGauge.value -= valueToSubtract;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
