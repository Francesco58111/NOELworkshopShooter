﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI distanceText;

    private int currentScore;
    private float currentDistance;
    public float speedModifier;

    public static GameManager Instance;




    private void Awake()
    {
        Instance = this;
    }

    void AddToScore(int add)
    {
        currentScore += add;

        scoreText.text = "S : " + currentScore.ToString();
    }

    private void Update()
    {
        currentDistance += Time.deltaTime * (speedModifier * LevelManager.Instance.levelSpeedModifier);

        int distance = (int)currentDistance;

        distanceText.text = ("D : " + distance.ToString());
    }

}
