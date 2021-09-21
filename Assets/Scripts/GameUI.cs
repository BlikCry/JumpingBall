using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yodo1.MAS;
using Random = UnityEngine.Random;

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
    private GameObject continueButton;
    [SerializeField]
    private GameObject highScoreText;
    [SerializeField]
    private GameObject recordsButton;
    [SerializeField]
    private GameObject playGamesButton;
    [SerializeField]
    private GameObject vibrationOffButton;
    [SerializeField]
    private List<GameObject> disableOnStart;
    [SerializeField]
    private List<GameObject> enableOnStart;

    private int highScore;
    private bool isHighScoreShown;

    private static bool isVibrationEnabled = true;
    private static bool beginGame;

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
        {
            recordsButton.SetActive(LeaderboardScript.IsAuthenticated);
            playGamesButton.SetActive(!LeaderboardScript.IsAuthenticated);
        }

        vibrationOffButton.SetActive(!isVibrationEnabled && !GroundScript.Instance.PrepareStarted);
        levelText.text = Mathf.Max(GroundScript.Instance.Level, 1).ToString();
        skipLevelText.text = (GroundScript.Instance.SkipLevels + 1).ToString();
        if (GroundScript.Instance.Level > highScore)
        {
            if (!isHighScoreShown)
                Vibrate();
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

    public void SetVibrationState(bool state)
    {
        isVibrationEnabled = state;
    }

    public void BeginGame()
    {
        beginGame = true;
    }

    public void ReloadLevel()
    {
        var id = SceneManager.GetActiveScene().buildIndex;
        Yodo1U3dMas.SetInterstitialAdDelegate((adEvent, error) =>
        {
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    SceneManager.LoadScene(id);
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    break;
                case Yodo1U3dAdEvent.AdError:
                    break;
            }
        });
        if (Yodo1U3dMas.IsInterstitialAdLoaded() && Random.value < 0.2)
            Yodo1U3dMas.ShowInterstitialAd();
        else
            SceneManager.LoadScene(id);
    }

    public void Continue()
    {
        Yodo1U3dMas.SetRewardedAdDelegate((adEvent, error) =>
        {
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    break;
                case Yodo1U3dAdEvent.AdError:
                    break;
                case Yodo1U3dAdEvent.AdReward:
                    PlayerPrefs.SetInt(GroundScript.CheckpointKey, GroundScript.Instance.Level - 1);
                    beginGame = true;
                    var id = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(id);
                    break;
            }
        });

        Yodo1U3dMas.ShowRewardedAd();
    }

    public void ShowLeaderBoard()
    {
        LeaderboardScript.ShowLeaderboard();
    }

    public void SignIn()
    {
        LeaderboardScript.SignIn();
    }

    private void OnBallDeath()
    {
        Vibrate();
        restartUI.SetActive(true);
        continueButton.SetActive(GroundScript.Instance.Level % 10 != 0 && Yodo1U3dMas.IsRewardedAdLoaded());
        LeaderboardScript.SetLeaderboardResult(GPGSIds.leaderboard_completed_levels, GroundScript.Instance.Level);
    }

    private void Vibrate()
    {
        if (isVibrationEnabled)
            Handheld.Vibrate();
    }
}
