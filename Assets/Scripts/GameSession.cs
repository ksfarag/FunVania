using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int PlayerLives = 3;
    int Score = 0;
    [SerializeField] TextMeshProUGUI lives;
    [SerializeField] TextMeshProUGUI score; 
    void Awake()
    {
        int sessions = FindObjectsOfType<GameSession>().Length;
        if (sessions > 1) 
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        lives.text=PlayerLives.ToString();
        score.text=Score.ToString();
    }
    public void ManageDeath()
    {
        if (PlayerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }
    
    public void ManageScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        score.text = Score.ToString();
    }

    private void TakeLife()
    {
        PlayerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //loads current scene
        lives.text = PlayerLives.ToString();
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePresist>().Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
    }
}
