using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    private const string HighScoreKey = "HIGHSCORE";

    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private GameObject restartUI;
    [SerializeField]
    private GameObject highScoreText;
    [SerializeField]
    private GameObject recordsButton;
    [SerializeField]
    private List<GameObject> disableOnStart;

    private int highScore;
    private bool isHighScoreShown;
    private bool beginGame;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 1);
        GroundScript.Instance.OnDeath += OnBallDeath;
    }

    private void Update()
    {
        recordsButton.SetActive(LeaderboardScript.IsAuthenticated);
        levelText.text = GroundScript.Instance.Level.ToString();
        if (GroundScript.Instance.Level > highScore)
        {
            if (!isHighScoreShown)
                Handheld.Vibrate();
            isHighScoreShown = true;
            highScore = GroundScript.Instance.Level;
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
            highScoreText.SetActive(true);
        }

    }

    private void LateUpdate()
    {
        if (beginGame)
        {
            GroundScript.Instance.Started = true;
            disableOnStart.ForEach(o => o.SetActive(false));
            Time.timeScale = 1;
        }
    }

    public void BeginGame()
    {
        beginGame = true;
    }

    public void ReloadLevel()
    {
        var id = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(id);
    }

    public void ShowLeaderBoard()
    {
        LeaderboardScript.ShowLeaderboard();
    }
    
    private void OnBallDeath()
    {
        Handheld.Vibrate();
        restartUI.SetActive(true);
        LeaderboardScript.SetLeaderboardResult(GPGSIds.leaderboard_completed_levels, GroundScript.Instance.Level);
    }
}
