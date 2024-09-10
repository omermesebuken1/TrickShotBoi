using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ObstacleMode : MonoBehaviour
{
    [SerializeField] private GameObject[] Obstacles;

    private Transform[] ObstaclesCreated;

    [SerializeField] private GameObject ObstaclesParent;

    private int obstacleCount;

    private bool refreshObstacles;

    [SerializeField] private Image TimeBar;

    [HideInInspector] public int holeCount;
    private int BestHoleCount;
    private float CountDowntimer;
    [SerializeField] private TextMeshProUGUI HoleCountText;
    [SerializeField] private TextMeshProUGUI BestHoleCountText;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private TextMeshProUGUI CountDownText;
    [SerializeField] private GameObject TouchBlocker;

    [SerializeField] private GameObject ObstaclePopEffect;
    [SerializeField] private GameObject ObstacleCreateEffect;
    [SerializeField] private GameObject Leaderboard;

    private bool started;
    private bool ended;

    private bool countDownStart;
    private bool countdownEnded;

    [SerializeField] private float timeFactor;

    private bool holeUp;
    private bool countStarted;

    private int obstacleNum;

    private float RandomObsPosX;
    private float RandomObsPosY;

    [SerializeField] private int MaxObstacles;

    private float obstacleTimer;
    private bool StartingObstaclesEnd;

    private float HoleAnchorYforObstacles;
    private float HoleAnchorXforObstacles = 7.3f;

    private int NewObstacleNum;

    [SerializeField] private AudioClip ObstacleCreatedSound;

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
        if (PlayerPrefs.HasKey("ObstacleModeBestHoleCount"))
        {
            BestHoleCountText.text = "Best: " + PlayerPrefs.GetInt("ObstacleModeBestHoleCount").ToString();
        }
        else
        {
            BestHoleCountText.text = "";
        }
        countStarted = false;

        FindObjectOfType<GamePlay>().TouchModeGoal = false;
        CountDownText.text = "";
        holeCount = 0;
        countDownStart = false;
        countdownEnded = false;
        StartButton.SetActive(true);
        started = false;
        ended = false;
        obstacleCount = 0;
        ObstaclesCreated = new Transform[MaxObstacles];
        StartingObstaclesEnd = false;
        refreshObstacles = false;


    }

    void Update()
    {
        GenerateObstacleDeneme();
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
            countStarted = false;
            StartButton.SetActive(true);

            refreshObstacles = true;
            ObstacleDestroyerOnEnd();

            started = false;
            StartingObstaclesEnd = false;

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
            if (countStarted)
            {
                holeUp = true;
            }

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
        Leaderboard.SetActive(false);
        holeCount = 0;
        ended = false;
        TouchBlocker.SetActive(true);
        StartButton.SetActive(false);
        CountDowntimer = 3;
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
            StartingFirstObstacles();

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
                FindObjectOfType<GamePlay>().TouchModeGoal = true;

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
            GenerateObstacle();
            holeUp = false;
        }

        HoleCountText.text = "Hole Count: " + holeCount.ToString();

    }

    private void TouchModeBestHoleCountChecker()
    {
        if (PlayerPrefs.HasKey("ObstacleModeBestHoleCount"))
        {
            BestHoleCount = PlayerPrefs.GetInt("ObstacleModeBestHoleCount");

            if (holeCount > BestHoleCount)
            {
                BestHoleCount = holeCount;
                PlayerPrefs.SetInt("ObstacleModeBestHoleCount", BestHoleCount);
                FindObjectOfType<PlayfabManager>().SendLeaderboard(BestHoleCount, "ObstacleMode");
                BestHoleCountText.text = "Best: " + BestHoleCount.ToString();
            }
            else
            {

                BestHoleCountText.text = "Best: " + BestHoleCount.ToString();
            }

        }
        else
        {
            PlayerPrefs.SetInt("ObstacleModeBestHoleCount", holeCount);
            BestHoleCount = PlayerPrefs.GetInt("ObstacleModeBestHoleCount");
            FindObjectOfType<PlayfabManager>().SendLeaderboard(BestHoleCount, "ObstacleMode");
            BestHoleCountText.text = "Best: " + BestHoleCount.ToString();

        }

    }

    private void GenerateObstacle()
    {

        if (obstacleCount < MaxObstacles)
        {
            if (PlayerPrefs.HasKey("Sound"))
            {
                if (PlayerPrefs.GetInt("Sound") == 1)
                {
                    GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
                    GetComponent<AudioSource>().PlayOneShot(ObstacleCreatedSound);
                }
            }
            HoleAnchorYforObstacles = FindObjectOfType<GamePlay>().HoleAnchorY;
            RandomObsPosX = Random.Range(-HoleAnchorXforObstacles + 0.8f, +HoleAnchorXforObstacles - 0.8f);
            RandomObsPosY = Random.Range(-HoleAnchorYforObstacles + 0.5f, +HoleAnchorYforObstacles - 0.5f);
            obstacleNum = Random.Range(0, 7);
            Instantiate(ObstacleCreateEffect, new Vector3(RandomObsPosX, RandomObsPosY, 0), Obstacles[obstacleNum].transform.rotation);
            NewObstacleNo();
            ObstaclesCreated[NewObstacleNum] = Instantiate(Obstacles[obstacleNum], new Vector3(RandomObsPosX, RandomObsPosY, 0), Obstacles[obstacleNum].transform.rotation).transform;
            ObstaclesCreated[NewObstacleNum].parent = ObstaclesParent.transform;
            ObstacleCounter();
        }

    }

    private void ObstacleCounter()
    {
        obstacleCount = 0;

        for (int i = 0; i < ObstaclesCreated.Length; i++)
        {
            if (ObstaclesCreated[i] != null)
            {
                obstacleCount = obstacleCount + 1;
            }
        }
        //print("Obstacle Count: " + obstacleNum);

    }

    private void NewObstacleNo()
    {
        NewObstacleNum = 0;

        for (int i = 0; i < ObstaclesCreated.Length; i++)
        {
            if (ObstaclesCreated[i] == null)
            {
                NewObstacleNum = i;
                break;
            }
        }
        //print("Obstacle No: " + NewObstacleNum);

    }

    private void ObstacleDestroyerOnEnd()
    {
        if (refreshObstacles)
        {


            for (int i = 0; i < ObstaclesCreated.Length; i++)
            {
                if (ObstaclesCreated[i] != null)
                {
                    Instantiate(ObstaclePopEffect, ObstaclesCreated[i].transform.position, ObstaclesCreated[i].transform.rotation);
                    Destroy(ObstaclesCreated[i].gameObject);
                }
            }

            ObstacleCounter();

            refreshObstacles = false;
        }

    }

    private void StartingFirstObstacles()
    {

        obstacleTimer += Time.deltaTime;

        if (!StartingObstaclesEnd)
        {

            if (obstacleTimer > 0.5f)
            {
                GenerateObstacle();
                obstacleTimer = 0;
            }

            ObstacleCounter();

            if (obstacleCount >= 5)
            {
                obstacleTimer = 0;
                StartingObstaclesEnd = true;
            }



        }

    }

    private void GenerateObstacleDeneme()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            RandomObsPosX = -2.35f;
            RandomObsPosY = -4.5f;
            obstacleNum = Random.Range(0, 7);
            Instantiate(ObstacleCreateEffect, new Vector3(RandomObsPosX, RandomObsPosY, 0), Obstacles[obstacleNum].transform.rotation);
            ObstaclesCreated[obstacleCount] = Instantiate(Obstacles[obstacleNum], new Vector3(RandomObsPosX, RandomObsPosY, 0), Obstacles[obstacleNum].transform.rotation).transform;
            ObstaclesCreated[obstacleCount].parent = ObstaclesParent.transform;
            ObstacleCounter();
        }

    }






}
