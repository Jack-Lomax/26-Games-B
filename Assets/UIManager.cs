using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text scoreText2;
    [SerializeField] private BananaController controller;
    [SerializeField] private GameObject endScreen;

    void Update()
    {
        scoreText.text = "Score: " + Mathf.RoundToInt(controller.GetScore());
        scoreText2.text = "Score: " + Mathf.RoundToInt(controller.GetScore());
    }

    void OnEnable() => controller.OnDeath += EndGame;
    void OnDisable() => controller.OnDeath -= EndGame;

    public void EndGame()
    {
        endScreen.SetActive(true);
    }



}
