using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaderboardGenel : MonoBehaviour
{
    private bool leaderboardCreated;

    [SerializeField] private GameObject[] player;

    public GameObject showLeaderboardButton;
    public GameObject showLeaderboardAroundPlayerButton;
    public GameObject hideLeaderboardButton;
    public GameObject Leaderboard;
    private float floatScore;
    private Color leaderboardcolor1 = new Color(0.6745283f,0.8426703f,1); //mavi
    private Color leaderboardcolor2 = new Color(1,1,1); //beyaz

    [SerializeField] private AudioClip ButtonClick;

    private void Start()
    {

        Leaderboard.SetActive(false);
        showLeaderboardButton.SetActive(true);
        showLeaderboardButton.SetActive(true);
        hideLeaderboardButton.SetActive(false);

    }


    private void Update()
    {
        if (leaderboardCreated)
        {
            StopCoroutine("GetLeaderboardData");
            leaderboardCreated = false;
        }

    }

    public void GetLeaderBoardButtonTop10()
    {
        FindObjectOfType<PlayfabManager>().GetLeaderboard(SceneManager.GetActiveScene().name);
        StartCoroutine("GetLeaderboardData");
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(ButtonClick);
            }
        }
        
    }

    public void GetLeaderBoardButtonAroundPlayer()
    {
        FindObjectOfType<PlayfabManager>().GetLeaderBoardAroundPlayer(SceneManager.GetActiveScene().name);
        StartCoroutine("GetLeaderboardData");
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(ButtonClick);
            }
        }
        
    }


    private void CreateLeaderboardOnPanelGenel()
    {

        if (FindObjectOfType<PlayfabManager>().LeaderboardReady)
        {
            Leaderboard.SetActive(false);

            for (int i = 0; i < player.Length; i++)
            {
                if (!leaderboardCreated)
                {

                    TextMeshProUGUI PlayerPlaceText = player[i].transform.Find("PlayerPlace").gameObject.GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI PlayerNameText = player[i].transform.Find("PlayerName").gameObject.GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI PlayerScoreText = player[i].transform.Find("PlayerScore").gameObject.GetComponent<TextMeshProUGUI>();

                    if (FindObjectOfType<PlayfabManager>().PlayerID[i] != null)
                    {
                        if(FindObjectOfType<PlayfabManager>().loggedInPlayfabId == FindObjectOfType<PlayfabManager>().PlayerID[i])
                        {
                            PlayerPlaceText.color = Color.yellow;
                            PlayerNameText.color = Color.yellow;
                            PlayerScoreText.color = Color.yellow;
                        }
                        else
                        {
                            if(i % 2 == 0)
                            {
                            PlayerPlaceText.color = leaderboardcolor1;
                            PlayerNameText.color = leaderboardcolor1;
                            PlayerScoreText.color = leaderboardcolor1;
                            }
                            else
                            {
                            PlayerPlaceText.color = leaderboardcolor2;
                            PlayerNameText.color = leaderboardcolor2;
                            PlayerScoreText.color = leaderboardcolor2;
                            }
                        }
                        
                        PlayerPlaceText.text = (FindObjectOfType<PlayfabManager>().PlayerPlace[i] + 1).ToString();
                        PlayerNameText.text = FindObjectOfType<PlayfabManager>().PlayerName[i];
                        PlayerScoreText.text = FindObjectOfType<PlayfabManager>().PlayerScore[i].ToString();

                    }
                    else
                    {
                        PlayerPlaceText.text = null;
                        PlayerNameText.text = null;
                        PlayerScoreText.text = null;
                    }
                    Leaderboard.SetActive(true);
                }
            }
            leaderboardCreated = true;
            FindObjectOfType<PlayfabManager>().LeaderboardReady = false;
        }
    }

     private void CreateLeaderboardOnPanelHoleMode()
    {

        if (FindObjectOfType<PlayfabManager>().LeaderboardReady)
        {
            Leaderboard.SetActive(false);

            for (int i = 0; i < player.Length; i++)
            {
                if (!leaderboardCreated)
                {

                    TextMeshProUGUI PlayerPlaceText = player[i].transform.Find("PlayerPlace").gameObject.GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI PlayerNameText = player[i].transform.Find("PlayerName").gameObject.GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI PlayerScoreText = player[i].transform.Find("PlayerScore").gameObject.GetComponent<TextMeshProUGUI>();

                    if (FindObjectOfType<PlayfabManager>().PlayerID[i] != null)
                    {
                        if(FindObjectOfType<PlayfabManager>().loggedInPlayfabId == FindObjectOfType<PlayfabManager>().PlayerID[i])
                        {
                            PlayerPlaceText.color = Color.yellow;
                            PlayerNameText.color = Color.yellow;
                            PlayerScoreText.color = Color.yellow;
                        }
                        else
                        {
                            if(i % 2 == 0)
                            {
                            PlayerPlaceText.color = leaderboardcolor1;
                            PlayerNameText.color = leaderboardcolor1;
                            PlayerScoreText.color = leaderboardcolor1;
                            }
                            else
                            {
                            PlayerPlaceText.color = leaderboardcolor2;
                            PlayerNameText.color = leaderboardcolor2;
                            PlayerScoreText.color = leaderboardcolor2;
                            }
                        }
                        
                        PlayerPlaceText.text = (FindObjectOfType<PlayfabManager>().PlayerPlace[i] + 1).ToString();
                        PlayerNameText.text = FindObjectOfType<PlayfabManager>().PlayerName[i];
                        floatScore = -1f * FindObjectOfType<PlayfabManager>().PlayerScore[i];
                        floatScore = floatScore / 100;
                        PlayerScoreText.text = floatScore.ToString("F2");

                    }
                    else
                    {
                        PlayerPlaceText.text = null;
                        PlayerNameText.text = null;
                        PlayerScoreText.text = null;
                    }
                    Leaderboard.SetActive(true);
                }
            }
            leaderboardCreated = true;
            FindObjectOfType<PlayfabManager>().LeaderboardReady = false;
        }
    }

     private void CreateLeaderboardOnPanelBasketMode()
    {

        if (FindObjectOfType<PlayfabManager>().LeaderboardReady)
        {
            Leaderboard.SetActive(false);

            for (int i = 0; i < player.Length; i++)
            {
                if (!leaderboardCreated)
                {

                    TextMeshProUGUI PlayerPlaceText = player[i].transform.Find("PlayerPlace").gameObject.GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI PlayerNameText = player[i].transform.Find("PlayerName").gameObject.GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI PlayerScoreText = player[i].transform.Find("PlayerScore").gameObject.GetComponent<TextMeshProUGUI>();

                    if (FindObjectOfType<PlayfabManager>().PlayerID[i] != null)
                    {
                        if(FindObjectOfType<PlayfabManager>().loggedInPlayfabId == FindObjectOfType<PlayfabManager>().PlayerID[i])
                        {
                            PlayerPlaceText.color = Color.yellow;
                            PlayerNameText.color = Color.yellow;
                            PlayerScoreText.color = Color.yellow;
                        }
                        else
                        {
                            if(i % 2 == 0)
                            {
                            PlayerPlaceText.color = leaderboardcolor1;
                            PlayerNameText.color = leaderboardcolor1;
                            PlayerScoreText.color = leaderboardcolor1;
                            }
                            else
                            {
                            PlayerPlaceText.color = leaderboardcolor2;
                            PlayerNameText.color = leaderboardcolor2;
                            PlayerScoreText.color = leaderboardcolor2;
                            }
                        }
                        
                        PlayerPlaceText.text = (FindObjectOfType<PlayfabManager>().PlayerPlace[i] + 1).ToString();
                        PlayerNameText.text = FindObjectOfType<PlayfabManager>().PlayerName[i];
                        floatScore = FindObjectOfType<PlayfabManager>().PlayerScore[i];
                        floatScore = floatScore / 100;
                        PlayerScoreText.text = floatScore.ToString("F2");

                    }
                    else
                    {
                        PlayerPlaceText.text = null;
                        PlayerNameText.text = null;
                        PlayerScoreText.text = null;
                    }
                    Leaderboard.SetActive(true);
                }
            }
            leaderboardCreated = true;
            FindObjectOfType<PlayfabManager>().LeaderboardReady = false;
        }
    }

    public void HideLeaderboard()
    {   
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(ButtonClick);
            }
        }
        StopCoroutine("GetLeaderboardData");
        Leaderboard.SetActive(false);
        leaderboardCreated = false;
        hideLeaderboardButton.SetActive(false);
    }

    IEnumerator GetLeaderboardData()
    {
        yield return new WaitUntil(() => FindObjectOfType<PlayfabManager>().LeaderboardReady == true);
        if(SceneManager.GetActiveScene().name == "HoleMode")
        {
            CreateLeaderboardOnPanelHoleMode();
        }
        else if(SceneManager.GetActiveScene().name == "BasketMode")
        {
            CreateLeaderboardOnPanelBasketMode();
        }
        else
        {
            CreateLeaderboardOnPanelGenel();
        }

        
        hideLeaderboardButton.SetActive(true);

    }

}
