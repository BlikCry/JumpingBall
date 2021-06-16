using GooglePlayGames;
using UnityEngine;

public class LeaderboardScript : MonoBehaviour
{
    public static bool IsAuthenticated => Social.localUser.authenticated;

    private static bool instanceCreated;

    private void Awake()
    {
        if (instanceCreated)
        {
            Destroy(gameObject);
            return;
        }
        
        instanceCreated = true;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //var config = new PlayGamesClientConfiguration();
        //PlayGamesPlatform.InitializeInstance(config);
        //PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        SignIn();
    }

    public static void SignIn()
    {
        if (!Social.localUser.authenticated)
            Social.localUser.Authenticate(success => {});
    }

    public static void SetLeaderboardResult(string leaderboard, int result)
    {
        if (Social.localUser.authenticated)
            Social.ReportScore(result, leaderboard, success => {});
    }

    public static void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
            Social.ShowLeaderboardUI();
    }
}
