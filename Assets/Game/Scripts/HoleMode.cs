using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HoleMode : MonoBehaviour
{

    [HideInInspector] public int holeCount;
    private float timer;
    private float CountDowntimer;

    private float BestTimeForHoleMode;
    [SerializeField] private TextMeshProUGUI HoleLeftText;
    [SerializeField] private TextMeshProUGUI TimeText;
    [SerializeField] private TextMeshProUGUI BestTimeText;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private TextMeshProUGUI CountDownText;

    [SerializeField] private GameObject TouchBlocker;

    [HideInInspector] public bool started;
    [HideInInspector] public bool ended;

    [HideInInspector] public bool countDownStart;
    [HideInInspector] public bool countdownEnded;

    private float tmpFloat;
    private int sendtoleaderboard;

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
        if (PlayerPrefs.HasKey("BestTimeForHoleMode"))
        {
            BestTimeText.text = "Best: " + PlayerPrefs.GetFloat("BestTimeForHoleMode").ToString();
        }
        else
        {
            BestTimeText.text = "";
        }

        TimeText.text = "Time: 0";
        CountDownText.text = "";
        holeCount = 10;
        countDownStart = false;
        countdownEnded = false;
        StartButton.SetActive(true);
        started = false;
        ended = false;

    }

    void FixedUpdate()
    {


        HoleCounter();
        CountDown();

        if (started && !ended)
        {
            
            timer += Time.deltaTime;
            timer = Mathf.Round(timer * 100.0f) * 0.01f;
            TimeText.text = "Time: " + timer.ToString("F2");
            BestTimeText.text = "";
            countDownStart = false;
            countdownEnded = false;

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
            Leaderboard.SetActive(true);
            FindObjectOfType<GamePlay>().holePlaced = false;
            FindObjectOfType<GamePlay>().holeWall = 5;
            BestTimeChecker();
            TouchBlocker.SetActive(true);
            FindObjectOfType<TrajectoryManager>().touchOn = false;
            StartButton.SetActive(true);
            started = false;

        }
        if (!started)
        {
            FindObjectOfType<TrajectoryManager>().touchOn = false;
        }


    }


    private void BestTimeChecker()
    {
        if (PlayerPrefs.HasKey("BestTimeForHoleMode"))
        {
            BestTimeForHoleMode = PlayerPrefs.GetFloat("BestTimeForHoleMode");

            if (timer < BestTimeForHoleMode)
            {
                BestTimeForHoleMode = timer;
                PlayerPrefs.SetFloat("BestTimeForHoleMode", BestTimeForHoleMode);
                tmpFloat = BestTimeForHoleMode * 100;
                sendtoleaderboard = (int)tmpFloat;
                FindObjectOfType<PlayfabManager>().SendLeaderboard(-sendtoleaderboard,"HoleMode");
                BestTimeForHoleMode = Mathf.Round(BestTimeForHoleMode * 100.0f) * 0.01f;
                BestTimeText.text = "Best: " + BestTimeForHoleMode.ToString("F2");
            }
            else
            {

                BestTimeText.text = "Best: " + BestTimeForHoleMode.ToString("F2");
            }

        }
        else
        {
            PlayerPrefs.SetFloat("BestTimeForHoleMode", timer);
            BestTimeForHoleMode = PlayerPrefs.GetFloat("BestTimeForHoleMode");
            tmpFloat = BestTimeForHoleMode * 100;
            sendtoleaderboard = (int)tmpFloat;
            FindObjectOfType<PlayfabManager>().SendLeaderboard(-sendtoleaderboard,"HoleMode");
            BestTimeForHoleMode = Mathf.Round(BestTimeForHoleMode * 100.0f) * 0.01f;
            BestTimeText.text = "Best: " + BestTimeForHoleMode.ToString("F2");

        }

    }

    public void StartGame()
    {
        Leaderboard.SetActive(false);
        holeCount = 10;
        ended = false;
        TouchBlocker.SetActive(true);
        StartButton.SetActive(false);
        CountDowntimer = 3;
        countDownStart = true;
        timer = 0;
        TimeText.text = "Time: " + timer.ToString("F2");
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

            if (CountDowntimer <= 0)
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

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HoleCounter()
    {
        if (FindObjectOfType<GamePlay>().holeModeGoal == true)
        {
            holeCount = holeCount - 1;
            FindObjectOfType<GamePlay>().holeModeGoal = false;
        }

        HoleLeftText.text = "Hole Left: " + holeCount.ToString();

        if (holeCount == 0)
        {
            ended = true;

        }



    }


}
