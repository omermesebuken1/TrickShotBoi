using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasketBallBall : MonoBehaviour
{

    Touch touch;
    [HideInInspector] public float totalDistance = 0;
    private bool record;
    private Vector3 previousLoc;

    [SerializeField] private Image ScoreBar1;
    [SerializeField] private Image ScoreBar2;

    private Color barColor;

    [SerializeField] private TextMeshProUGUI CurrentScoreText;

    private bool startScoreBoard;

    [HideInInspector] public float lastScore;
    [HideInInspector] public bool lastScoreSend;

    private void Start()
    {

        totalDistance = 0;
        record = false;
        ScoreBar1.fillAmount = 0;
        ScoreBar2.fillAmount = 0;
        CurrentScoreText.text = "";
        startScoreBoard = false;
        previousLoc = new Vector3(0, 0, 0);
        totalDistance = 0;
        
    }

    private void Update()
    {
        TouchMechanic();
        

    }
    void FixedUpdate()
    {

        if (record)
        {
            RecordDistance();
            ScoreBarsStatus();
        }

    }
    void RecordDistance()
    {
        if(record && startScoreBoard)
        {
        
        totalDistance += Vector3.Distance(transform.position, previousLoc);
        previousLoc = transform.position;

        }
        
        //print(totalDistance);
    }
    // void ToggleRecord() => record = !record;


    private void TouchMechanic()
    {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                record = false;
                previousLoc = transform.position;
                totalDistance = 0;
                
            }

            if (touch.phase == TouchPhase.Moved)
            {

                record = true;
                previousLoc = transform.position;
                totalDistance = 0;
                startScoreBoard = true;
                
            }


            if (touch.phase == TouchPhase.Ended)
            {
                record = true;
                startScoreBoard = true;
                previousLoc = transform.position;
                totalDistance = 0;

            }

        }
    }

    public void WriteDistance()
    {
        //print("Total: " + totalDistance);
        record = false;
        if(!lastScoreSend)
        {
            lastScore = totalDistance;
            lastScoreSend = true;
        }
        
        
        previousLoc = transform.position;
        totalDistance = 0;
    }


    private void ScoreBarsStatus()
    {

        if (FindObjectOfType<BasketballMode>().timeOut == true)
            {
                
                totalDistance = 0;
                
            }

        if (startScoreBoard)
        {
            totalDistance = Mathf.Round(totalDistance * 100.0f) * 0.01f;
            CurrentScoreText.text = totalDistance.ToString("F2");
            ScoreBar1.fillAmount = totalDistance/30;
            ScoreBar2.fillAmount = totalDistance/30;
            barColor = new Color(1,1,1,totalDistance/10);
            ScoreBar1.color = barColor;
            ScoreBar2.color = barColor;

           
        }

    }





}
