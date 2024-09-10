using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TouchMode : MonoBehaviour
{

    Touch touch;
    [SerializeField] private Image TimeBar;

    [HideInInspector] public int holeCount;
    private int BestHoleCount;
    
    private float CountDowntimer;
    [SerializeField] private TextMeshProUGUI HoleCountText;
    [SerializeField] private TextMeshProUGUI BestHoleCountText;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private TextMeshProUGUI CountDownText;

    [SerializeField] private GameObject TouchBlocker;
    [SerializeField] private GameObject TouchModeBlocker;

    private bool started;
    private bool ended;

    private bool countDownStart;
    private bool countdownEnded;

    [SerializeField] private float timeFactor;

    private bool holeUp;

    private bool countStarted;

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
        TouchModeBestHoleCountChecker();
        if(PlayerPrefs.HasKey("TouchModeBestHoleCount"))
        {
            BestHoleCountText.text = "Best: " + PlayerPrefs.GetFloat("TouchModeBestHoleCount").ToString();
        }
        else
        {
            BestHoleCountText.text = "";
        }
        countStarted = false;
        TouchModeBlocker.SetActive(false);
        FindObjectOfType<GamePlay>().TouchModeGoal = false;
        CountDownText.text = "";
        holeCount = 0;
        countDownStart = false;
        countdownEnded = false;
        StartButton.SetActive(true);
        started = false;
        ended = false;

    }

    void Update()
    {


        OneTouchMode();
        HoleCounter();
        CountDown();

        if (started && !ended)
        {
            TimeBarStatus();
            countDownStart = false;
            countdownEnded = false;
            BestHoleCountText.text = "";

        }

        if (ended)
        {
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
            TouchModeBestHoleCountChecker();
            Leaderboard.SetActive(true);
            FindObjectOfType<GamePlay>().Ball.transform.position = FindObjectOfType<GamePlay>().BallStarPoint.position;
            FindObjectOfType<GamePlay>().holePlaced = false;
            FindObjectOfType<GamePlay>().holeWall = 5;
            //BestTimeChecker();
            TouchBlocker.SetActive(true);
            TouchModeBlocker.SetActive(false);
            StartButton.SetActive(true);
            started = false;

        }
        if (!started)
        {
            TouchBlocker.SetActive(true);
        }







    }


    private void TimeBarStatus()
    {
        

        if (FindObjectOfType<GamePlay>().TouchModeGoal)
        {

            TimeBar.fillAmount = 1;
            
                holeUp = true;
            
            TouchModeBlocker.SetActive(false);
            countStarted = true;
            FindObjectOfType<GamePlay>().TouchModeGoal = false;


        }

        TimeBar.fillAmount = Mathf.MoveTowards(TimeBar.fillAmount, 0, Time.deltaTime / timeFactor);

        if (TimeBar.fillAmount == 0)
        {
            ended = true;

        }

    }

    public void StartGame()
    {
        FindObjectOfType<GamePlay>().TouchModeGoal = false;
        TimeBar.fillAmount = 1;
        Leaderboard.SetActive(false);
        holeCount = 0;
        ended = false;
        TouchBlocker.SetActive(true);
        StartButton.SetActive(false);
        CountDowntimer = 3;
        countDownStart = true;
        countDownStart = true;
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
                FindObjectOfType<GamePlay>().holeWall = Random.Range(1, 5);
                FindObjectOfType<GamePlay>().holePlaced = false;
                TouchModeBlocker.SetActive(false);

                countdownEnded = true;


            }

            CountDowntimer -= Time.deltaTime;
        }


    }

    private void HoleCounter()
    {
        if (holeUp)
        {
            holeCount = holeCount + 1;
            holeUp = false;
        }

        HoleCountText.text = "Hole Count: " + holeCount.ToString();

    }

    private void OneTouchMode()
    {
        if(!Leaderboard.activeSelf)
        {
            if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Ended)
            {
                
                TouchModeBlocker.SetActive(true);

            }

        }

        }

        
    }

    private void TouchModeBestHoleCountChecker()
    {
        if (PlayerPrefs.HasKey("TouchModeBestHoleCount"))
        {
            BestHoleCount = PlayerPrefs.GetInt("TouchModeBestHoleCount");

            if (holeCount > BestHoleCount)
            {
                BestHoleCount = holeCount;
                PlayerPrefs.SetInt("TouchModeBestHoleCount", BestHoleCount);
                FindObjectOfType<PlayfabManager>().SendLeaderboard(BestHoleCount, "TouchMode");
                BestHoleCountText.text = "Best: " + BestHoleCount.ToString();
            }
            else
            {
                
                BestHoleCountText.text = "Best: " + BestHoleCount.ToString();
            }

        }
        else
        {
            PlayerPrefs.SetInt("TouchModeBestHoleCount",holeCount);
            BestHoleCount = PlayerPrefs.GetInt("TouchModeBestHoleCount");
            FindObjectOfType<PlayfabManager>().SendLeaderboard(BestHoleCount, "TouchMode");
            BestHoleCountText.text = "Best: " + BestHoleCount.ToString();

        }

    }




}
