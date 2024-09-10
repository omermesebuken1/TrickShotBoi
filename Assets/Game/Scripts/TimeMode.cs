using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class TimeMode : MonoBehaviour
{
    [HideInInspector] public int holeCount;
    private float timer;
    private float CountDowntimer;

    private int BestHoleCountForTimeMode;
    [SerializeField] private GameObject[] holeChecker;
    [SerializeField] private TextMeshProUGUI HoleCountText;
    [SerializeField] private TextMeshProUGUI TimeLeftText;
    [SerializeField] private TextMeshProUGUI BestHoleCountText;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private TextMeshProUGUI CountDownText;

    [SerializeField] private GameObject TouchBlocker;

    [HideInInspector] public bool started;
    [HideInInspector] public bool ended;

    [HideInInspector] public bool countDownStart;
    [HideInInspector] public bool countdownEnded;

    [SerializeField] private GameObject Leaderboard;

    [SerializeField] private GameObject SoundCaster;
    private GameObject currentSoundCaster;
    private bool count1cast;
    private bool count2cast;
    private bool count3cast;
    private bool countendcast;
    private bool GameEndcast;
    

    

    private void Start()
    {

        if (PlayerPrefs.HasKey("BestHoleCountForTimeMode"))
        {
            BestHoleCountText.text = "Best: " + PlayerPrefs.GetInt("BestHoleCountForTimeMode").ToString();
        }
        else
        {
            BestHoleCountText.text = "";
        }

        TimeLeftText.text = "Time: 30";
        CountDownText.text = "";
        holeCount = 0;
        countDownStart = false;
        countdownEnded = false;
        StartButton.SetActive(true);
        started = false;
        ended = false;
        FindObjectOfType<GamePlay>().Ball.transform.position = FindObjectOfType<GamePlay>().BallStarPoint.position;

    }

    void FixedUpdate()
    {
        //        print(FindObjectOfType<GamePlay>().holeWall);
        HoleCounter();
        CountDown();
        

        if (started && !ended)
        {

            timer -= Time.deltaTime;
            timer = Mathf.Round(timer * 100.0f) * 0.01f;
            TimeLeftText.text = "Time: " + timer.ToString("F2");
            BestHoleCountText.text = "";
            countDownStart = false;
            countdownEnded = false;

        }

        if (ended)
        {
            FindObjectOfType<GamePlay>().Ball.transform.position = FindObjectOfType<GamePlay>().BallStarPoint.position;
            if (PlayerPrefs.HasKey("Sound"))
            {
                if (PlayerPrefs.GetInt("Sound") == 1)
                {
                    if (!GameEndcast)
                    {
                        currentSoundCaster = Instantiate(SoundCaster);
                        currentSoundCaster.GetComponent<SoundCaster>().CastSoundEndDie("GameEnd");
                        GameEndcast = true;
                    }

                }
            }
            FindObjectOfType<GamePlay>().holePlaced = false;
            FindObjectOfType<GamePlay>().holeWall = 5;
            BestHoleCountChecker();
            TouchBlocker.SetActive(true);
            FindObjectOfType<TrajectoryManager>().touchOn = false;
            StartButton.SetActive(true);
            started = false;
            Leaderboard.SetActive(true);

        }
        if (!started)
        {
            FindObjectOfType<TrajectoryManager>().touchOn = false;
        }


    }


    private void BestHoleCountChecker()
    {
        if (PlayerPrefs.HasKey("BestHoleCountForTimeMode"))
        {
            BestHoleCountForTimeMode = PlayerPrefs.GetInt("BestHoleCountForTimeMode");

            if (holeCount > BestHoleCountForTimeMode)
            {
                BestHoleCountForTimeMode = holeCount;
                PlayerPrefs.SetInt("BestHoleCountForTimeMode", BestHoleCountForTimeMode);
                FindObjectOfType<PlayfabManager>().SendLeaderboard(BestHoleCountForTimeMode, "TimeMode");
                BestHoleCountText.text = "Best: " + BestHoleCountForTimeMode.ToString();
            }
            else
            {

                BestHoleCountText.text = "Best: " + BestHoleCountForTimeMode.ToString();
            }

        }
        else
        {
            PlayerPrefs.SetInt("BestHoleCountForTimeMode", holeCount);
            FindObjectOfType<PlayfabManager>().SendLeaderboard(BestHoleCountForTimeMode, "TimeMode");
            BestHoleCountForTimeMode = PlayerPrefs.GetInt("BestHoleCountForTimeMode");
            BestHoleCountText.text = "Best: " + BestHoleCountForTimeMode.ToString();

        }

    }

    public void StartGame()
    {
        holeCount = 0;
        ended = false;
        TouchBlocker.SetActive(true);
        StartButton.SetActive(false);
        CountDowntimer = 3;
        countDownStart = true;
        timer = 30;
        TimeLeftText.text = "Time: " + timer.ToString("F2");
        Leaderboard.SetActive(false);
        count1cast = false;
        count2cast = false;
        count3cast = false;
        countendcast = false;
        GameEndcast = false;

    }


    private void CountDown()
    {
        if (countDownStart && !countdownEnded)
        {

            if (CountDowntimer > 2 && CountDowntimer <= 3)
            {
                CountDownText.text = "3";
                if (PlayerPrefs.HasKey("Sound"))
                {
                    if (PlayerPrefs.GetInt("Sound") == 1)
                    {
                        if (!count1cast)
                        {
                            currentSoundCaster = Instantiate(SoundCaster);
                            currentSoundCaster.GetComponent<SoundCaster>().CastSoundEndDie("Count123");
                            count1cast = true;
                        }

                    }
                }
            }

            if (CountDowntimer > 1 && CountDowntimer <= 2)
            {
                CountDownText.text = "2";
                if (PlayerPrefs.HasKey("Sound"))
                {
                    if (PlayerPrefs.GetInt("Sound") == 1)
                    {
                        if (!count2cast)
                        {
                            currentSoundCaster = Instantiate(SoundCaster);
                            currentSoundCaster.GetComponent<SoundCaster>().CastSoundEndDie("Count123");
                            count2cast = true;
                        }

                    }
                }
            }

            if (CountDowntimer > 0 && CountDowntimer <= 1)
            {
                CountDownText.text = "1";
                if (PlayerPrefs.HasKey("Sound"))
                {
                    if (PlayerPrefs.GetInt("Sound") == 1)
                    {
                        if (!count3cast)
                        {
                            currentSoundCaster = Instantiate(SoundCaster);
                            currentSoundCaster.GetComponent<SoundCaster>().CastSoundEndDie("Count123");
                            count3cast = true;
                        }

                    }
                }
            }

            if (CountDowntimer <= 0)
            {
                if (PlayerPrefs.HasKey("Sound"))
                {
                    if (PlayerPrefs.GetInt("Sound") == 1)
                    {
                        if (!countendcast)
                        {
                            currentSoundCaster = Instantiate(SoundCaster);
                            currentSoundCaster.GetComponent<SoundCaster>().CastSoundEndDie("CountEnd");
                            countendcast = true;
                        }

                    }
                }
                CountDownText.text = "";
                started = true;
                TouchBlocker.SetActive(false);
                FindObjectOfType<TrajectoryManager>().touchOn = true;
                FindObjectOfType<GamePlay>().holeWall = Random.Range(1, 5);
                FindObjectOfType<GamePlay>().holePlaced = false;

                countdownEnded = true;


            }

            CountDowntimer -= Time.deltaTime;
        }


    }


    private void HoleCounter()
    {
        if (FindObjectOfType<GamePlay>().holeModeGoal == true)
        {
            holeCount = holeCount + 1;
            FindObjectOfType<GamePlay>().holeModeGoal = false;
        }

        HoleCountText.text = "Hole Count: " + holeCount.ToString();

        if (timer <= 0 && started)
        {
            ended = true;

        }



    }



   


}
