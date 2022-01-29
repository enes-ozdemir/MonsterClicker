using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayGames : MonoBehaviour
{
    private const string LeaderboardID = "CggIoP-ltSsQAhAA";
    private const string AchievementID = "CggIoP-ltSsQAhAB";
    private static PlayGamesPlatform _platform;
    [SerializeField] private Text checkScoreText;

    private void Start()
    {
        AuthUser();
    }

    private static void AuthUser()
    {
        Debug.Log("AuthUser");
        if (_platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            _platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Logged in successfully");
            }
            else
            {
                Debug.Log("Login Failed");
            }
        });
    }

    public void AddScoreToLeaderboard()
    {
        var sentence = checkScoreText.text;
        var words = sentence.Split(',');
        ArrayList arlist = new ArrayList();
        foreach (string word in words)
        {
            arlist.Add(int.Parse(word));
        }

        var second = int.Parse(arlist[0].ToString());
        var millisecond = int.Parse(arlist[1].ToString());
        double vOut = (second + (millisecond * 0.01)) * 1000;
        long leaderTime = Convert.ToInt64(vOut);
        if (Social.Active.localUser.authenticated)
        {
            Debug.Log("AddScoreToLeaderboard" + leaderTime);
            Social.ReportScore(leaderTime, LeaderboardID,
                success => { Debug.Log("Score Succesfully added " + leaderTime); });
        }
    }

    public void ShowLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            Debug.Log("ShowLeaderboard");
            _platform.ShowLeaderboardUI();
        }
    }

    public void ShowAchievements()
    {
        if (Social.Active.localUser.authenticated)
        {
            _platform.ShowAchievementsUI();
        }
    }

    public void UnlockAchievement()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportProgress(AchievementID, 100f, success => { });
        }
    }
}