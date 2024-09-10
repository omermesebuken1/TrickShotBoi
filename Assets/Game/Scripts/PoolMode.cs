using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PoolMode : MonoBehaviour
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



    private void Start()
    {
        if(PlayerPrefs.HasKey("BestTimeForHoleMode"))
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
            FindObjectOfType<GamePlayForPoolMode>().holePlaced = false;
            FindObjectOfType<GamePlayForPoolMode>().holeWall = 5;
            BestTimeChecker();
            TouchBlocker.SetActive(true);
            FindObjectOfType<TrajectoryManagerForPool>().touchOn = false;
            StartButton.SetActive(true);
            started = false;

        }
        if(!started)
        {
            FindObjectOfType<TrajectoryManagerForPool>().touchOn = false;
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
            PlayerPrefs.SetFloat("BestTimeForHoleMode",timer);
            BestTimeForHoleMode = PlayerPrefs.GetFloat("BestTimeForHoleMode");
            BestTimeForHoleMode = Mathf.Round(BestTimeForHoleMode * 100.0f) * 0.01f;
            BestTimeText.text = "Best: " + BestTimeForHoleMode.ToString("F2");

        }

    }

    public void StartGame()
    {
        holeCount = 10;
        ended = false;
        TouchBlocker.SetActive(true);
        StartButton.SetActive(false);
        CountDowntimer = 3;
        countDownStart = true;
        timer = 0;
        TimeText.text = "Time: " + timer.ToString("F2");

    }

    private void CountDown()
    {
        if (countDownStart && !countdownEnded)
        {

            if (CountDowntimer > 2 && CountDowntimer <= 3)
            {
                CountDownText.text = "3";
            }

            if (CountDowntimer > 1 && CountDowntimer <= 2)
            {
                CountDownText.text = "2";
            }

            if (CountDowntimer > 0 && CountDowntimer <= 1)
            {
                CountDownText.text = "1";
            }

            if (CountDowntimer <= 0)
            {
                CountDownText.text = "";
                started = true;
                TouchBlocker.SetActive(false);
                FindObjectOfType<TrajectoryManagerForPool>().touchOn = true;
                FindObjectOfType<GamePlayForPoolMode>().holeWall = Random.Range(1, 5);
                FindObjectOfType<GamePlayForPoolMode>().holePlaced = false;

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
        if (FindObjectOfType<GamePlayForPoolMode>().poolModeGoal == true)
        {
            holeCount = holeCount - 1;
            FindObjectOfType<GamePlayForPoolMode>().poolModeGoal = false;
        }

        HoleLeftText.text = "Hole Left: " + holeCount.ToString();

        if (holeCount == 0)
        {
            ended = true;

        }



    }

}
