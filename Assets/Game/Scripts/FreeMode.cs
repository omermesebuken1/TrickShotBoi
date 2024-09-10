using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreeMode : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI scoreBoard;
    public int score;

    [SerializeField] private GameObject showleaederboardButton;
    [SerializeField] private GameObject hideleaederboardButton;
    [SerializeField] private GameObject Leaderboard;

    [SerializeField] private AudioClip ButtonClick;

    private void Start() 
    {
      
        FindObjectOfType<GamePlay>().holeWall = Random.Range(1, 5);
        FindObjectOfType<GamePlay>().holePlaced = false;
        scoreBoardStart();
        HideLeaderBoard();

    }

    private void Update() {

        scoreBoardUpdater();
    }

    private void scoreBoardUpdater()
    {
        if (PlayerPrefs.HasKey("Score"))
        {

            if (FindObjectOfType<GamePlay>().updateScore)
            {
                score = PlayerPrefs.GetInt("Score") + 1;
                PlayerPrefs.SetInt("Score", score);
                FindObjectOfType<PlayfabManager>().SendLeaderboard(score, "FreeMode");
                scoreBoard.text = score.ToString();
                FindObjectOfType<GamePlay>().updateScore = false;
            }

        }
        else
        {

            if (FindObjectOfType<GamePlay>().updateScore)
            {
                PlayerPrefs.SetInt("Score", 0);
                score = PlayerPrefs.GetInt("Score");
                FindObjectOfType<PlayfabManager>().SendLeaderboard(score, "FreeMode");
                scoreBoard.text = score.ToString();
                FindObjectOfType<GamePlay>().updateScore = false;


            }

        }

    }

    private void scoreBoardStart()
    {

        if (PlayerPrefs.HasKey("Score"))
        {

            score = PlayerPrefs.GetInt("Score");
            scoreBoard.text = score.ToString();

        }
        else
        {

            PlayerPrefs.SetInt("Score", 0);
            score = PlayerPrefs.GetInt("Score");
            scoreBoard.text = score.ToString();


        }

    }

    public void ShowLeaderBoard()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(ButtonClick);
            }
        }
        showleaederboardButton.SetActive(false);
        hideleaederboardButton.SetActive(true);
        Leaderboard.SetActive(true);
    }

    public void HideLeaderBoard()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(ButtonClick);
            }
        }
        showleaederboardButton.SetActive(true);
        hideleaederboardButton.SetActive(false);
        Leaderboard.SetActive(false);
    }
    
}
