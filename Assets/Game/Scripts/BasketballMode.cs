using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BasketballMode : MonoBehaviour
{
    [HideInInspector] public int holeCount;


    private float totalScore;
    private float CountDowntimer;
     [SerializeField] private float timeFactor;
    private float BestTotalForBasketMode;

    [HideInInspector] public Image TimeBar;
    [SerializeField] private TextMeshProUGUI HoleLeftText;
    [SerializeField] private TextMeshProUGUI TotalText;
    [SerializeField] private TextMeshProUGUI BestTotalText;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private TextMeshProUGUI CountDownText;

    [SerializeField] private GameObject TouchBlocker;

    

    [HideInInspector] public bool started;
    [HideInInspector] public bool ended;

    [HideInInspector] public bool countDownStart;
    [HideInInspector] public bool countdownEnded;

    [HideInInspector] public bool timeOut;

    [HideInInspector] public bool ilkPota;
    
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
        if(PlayerPrefs.HasKey("BestTotalForBasketMode"))
        {
            BestTotalText.text = "Best: " + PlayerPrefs.GetFloat("BestTotalForBasketMode").ToString();
        }
        else
        {
            BestTotalText.text = "";
        }

        TotalText.text = "Total: 0";
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

            TimeBarStatus();
            BestTotalText.text = "";
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
            BestMeasureChecker();
            TouchBlocker.SetActive(true);
            FindObjectOfType<TrajectoryManager>().touchOn = false;
            StartButton.SetActive(true);
            started = false;
            timeOut = true;

        }
        if(!started)
        {
            FindObjectOfType<TrajectoryManager>().touchOn = false;
        }


    }


    private void BestMeasureChecker()
    {
        if (PlayerPrefs.HasKey("BestTotalForBasketMode"))
        {
            BestTotalForBasketMode = PlayerPrefs.GetFloat("BestTotalForBasketMode");

            if (totalScore > BestTotalForBasketMode)
            {
                BestTotalForBasketMode = totalScore;
                PlayerPrefs.SetFloat("BestTotalForBasketMode", BestTotalForBasketMode);
                tmpFloat = BestTotalForBasketMode * 100;
                sendtoleaderboard = (int)tmpFloat;
                FindObjectOfType<PlayfabManager>().SendLeaderboard(sendtoleaderboard,"BasketMode");
                BestTotalForBasketMode = Mathf.Round(BestTotalForBasketMode * 100.0f) * 0.01f;
                BestTotalText.text = "Best: " + BestTotalForBasketMode.ToString("F2");
            }
            else
            {
                
                BestTotalText.text = "Best: " + BestTotalForBasketMode.ToString("F2");
            }

        }
        else
        {
            PlayerPrefs.SetFloat("BestTotalForBasketMode",totalScore);
            BestTotalForBasketMode = PlayerPrefs.GetFloat("BestTotalForBasketMode");
            tmpFloat = BestTotalForBasketMode * 100;
            sendtoleaderboard = (int)tmpFloat;
            FindObjectOfType<PlayfabManager>().SendLeaderboard(sendtoleaderboard,"BasketMode");
            BestTotalForBasketMode = Mathf.Round(BestTotalForBasketMode * 100.0f) * 0.01f;
            BestTotalText.text = "Best: " + BestTotalForBasketMode.ToString("F2");

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
        totalScore = 0;
        TotalText.text = "Time: " + totalScore.ToString("F2");
        BestTotalText.text = "";
        ilkPota = true;
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
                FindObjectOfType<TrajectoryManager>().touchOn = true;
                

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
        if (FindObjectOfType<GamePlayForBasketBall>().BasketModeGoal == true)
        {   
            
            totalScore = totalScore + FindObjectOfType<BasketBallBall>().lastScore;
            FindObjectOfType<BasketBallBall>().lastScoreSend = false;
            TotalText.text = "Total: " + totalScore.ToString("F2");
            holeCount = holeCount - 1;
            FindObjectOfType<GamePlayForBasketBall>().BasketModeGoal = false;
            TimeBar.fillAmount = 1;
        }

        

        HoleLeftText.text = "Hole Left: " + holeCount.ToString();

        if (holeCount == 0)
        {
            ended = true;

        }



    }

    private void TimeBarStatus()
    {

        TimeBar.fillAmount = Mathf.MoveTowards(TimeBar.fillAmount, 0, Time.deltaTime / timeFactor);

        if (TimeBar.fillAmount == 0)
        {
            timeOut = true;

        }

    }


}
