using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;


public class PlayfabManager : MonoBehaviour
{
    // Start is called before the first frame update

    [HideInInspector] public int[] PlayerPlace;
    [HideInInspector] public string[] PlayerName;
    [HideInInspector] public string[] PlayerID;
    [HideInInspector] public int[] PlayerScore;

    [HideInInspector] public string loggedInPlayfabId;

    [HideInInspector] public bool nameUpdated;

    private string randomString;

    const string glyphs = "abcdefghijklmnopqrstuvwxyz"; //add the characters you want

    [HideInInspector] public bool LeaderboardReady;

    void Start()
    {
        PlayerPlace = new int[20];
        PlayerName = new string[20];
        PlayerID = new string[20];
        PlayerScore = new int[20];
        Login();
        LeaderboardReady = false;
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnSuccessLogin, OnError);
    }

    void OnSuccessLogin(LoginResult result)
    {
        loggedInPlayfabId = result.PlayFabId;
        Debug.Log("Successful login/account create!");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        //Debug.Log(error,GenerateErrorReport());
    }


    public void SendLeaderboard(int score, string GameMode)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = GameMode,
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }


    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent!");

    }

    public void GetLeaderboard(string GameMode)
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = GameMode,
            StartPosition = 0,
            MaxResultsCount = 9
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        int i = 0;

        foreach (var item in result.Leaderboard)
        {
            //Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);

            PlayerPlace[i] = item.Position;
            PlayerID[i] = item.PlayFabId;
            PlayerName[i] = item.DisplayName;
            PlayerScore[i] = item.StatValue;


            //print(PlayerPlace[i] + " " + PlayerID[i] + " " + PlayerScore[i]);

            i++;
        }

        LeaderboardReady = true;

    }


    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = PlayerPrefs.GetString("PlayerName"),
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
        nameUpdated = true;


    }

    ////// RANDOM USER GENERATOR

    [ContextMenu("RandomSendDatasToLeaderboard")]
    public void RandomSendDatasToLeaderboard(int people)
    {
        StartCoroutine(RandomSends(people));
    }

    IEnumerator RandomSends(int people)
    {
        for (int i = 0; i < people; i++)
        {
            GenerateRandomString();
            name = randomString;
            yield return RandomSend();
        }
    }

    [ContextMenu("RandomSendLeaderboard")]
    public void RandomSendLeaderboard()
    {

        StartCoroutine(RandomSend());
    }

    IEnumerator RandomSend()
    {
        // Login
        string id = Random.Range(0, 100000) + "";
        var request = new LoginWithCustomIDRequest
        {
            CustomId = id,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccessLogin, OnError);

        yield return new WaitForSeconds(0.3f);
        // SetName
        SubmitRandomNameButton(name);

        yield return new WaitForSeconds(0.3f);
        // SendData
        RandomScore();
        yield return new WaitForSeconds(0.3f);
        Debug.Log("Random Vatandaş Created.");
    }

    public void SubmitRandomNameButton(string name)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    [ContextMenu("SendScore")]
    public void RandomScore()
    {
        int score = Random.Range(1, 15);
        SendLeaderboard(score, SceneManager.GetActiveScene().name);
    }

    private void GenerateRandomString()
    {
        randomString = null;
        int charAmount = Random.Range(3, 12); //set those to the minimum and maximum length of your string

        for (int i = 0; i < charAmount; i++)
        {
            randomString += glyphs[Random.Range(0, glyphs.Length)];
        }



    }

////// RANDOM USER GENERATOR


    [ContextMenu("GetLeaderBoardAroundPlayer")]
    public void GetLeaderBoardAroundPlayer(string GameMode)
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = GameMode,
            MaxResultsCount = 9
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAroundPlayerGet, OnError);
    }

    void OnLeaderboardAroundPlayerGet(GetLeaderboardAroundPlayerResult result)
    {
        int i = 0;

        foreach (var item in result.Leaderboard)
        {
            
            PlayerPlace[i] = 0;
            PlayerID[i] = null;
            PlayerName[i] = null;
            PlayerScore[i] = 0;

            i++;
        }

        i = 0;

        foreach (var item in result.Leaderboard)
        {
            
            PlayerPlace[i] = item.Position;
            PlayerID[i] = item.PlayFabId;
            PlayerName[i] = item.DisplayName;
            PlayerScore[i] = item.StatValue;

            i++;
        }

        LeaderboardReady = true;
    }

}
