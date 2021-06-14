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
    private TMP_Text skipLevelText;
    [SerializeField]
    private GameObject restartUI;
    [SerializeField]
    private GameObject highScoreText;
    [SerializeField]
    private GameObject recordsButton;
    [SerializeField]
    private List<GameObject> disableOnStart;
    [SerializeField]
    private List<GameObject> enableOnStart;

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
        if (!GroundScript.Instance.PrepareStarted)
            recordsButton.SetActive(LeaderboardScript.IsAuthenticated);
        levelText.text = Mathf.Max(GroundScript.Instance.Level, 1).ToString();
        skipLevelText.text = (GroundScript.Instance.SkipLevels + 1).ToString();
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
            beginGame = false;
            GroundScript.Instance.PrepareStarted = true;
            disableOnStart.ForEach(o => o.SetActive(false));
            enableOnStart.ForEach(o => o.SetActive(true));
            Time.timeScale = 1;
        }
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
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
