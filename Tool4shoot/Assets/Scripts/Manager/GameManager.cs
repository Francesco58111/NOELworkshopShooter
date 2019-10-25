using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int currentScore;
    [Range(0,2)]
    public float gameSpeedModifier;
    

    public static GameManager Instance;




    private void Awake()
    {
        Instance = this;
    }



    public void AddToScore(float add)
    {
        currentScore += (int)add;

        scoreText.text = currentScore.ToString();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
