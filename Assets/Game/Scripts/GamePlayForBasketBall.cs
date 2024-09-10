using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayForBasketBall : MonoBehaviour
{
    [SerializeField] private GameObject pota;



    private GameObject currentPota;

    [SerializeField] private GameObject Ball;
    [SerializeField] private Transform BallStarPoint;

    [HideInInspector] public bool updateScore;
    [HideInInspector] public bool holePlaced;
    [HideInInspector] public int holeWall;

    [HideInInspector] public bool BasketModeGoal;
    private float scaleDif;


    private float PotaAnchorX = 7.3f;
    private float minPotaAnchorY = 2;
    [HideInInspector] public float PotaAnchorY;
    private float specialNum = 0.43f;
    private float heightDiff;
    private float minHeight = 9.23f;
    private float anchorDiff;

    private Vector3 potaPosition;
    private Vector3 PotaRotation;

    private float RandomPotaPosX;
    private float RandomPotaPosY;

    private int potaReflection;
    private int potaAngleProbabilityRandom;
    [SerializeField] private int potaAngleProbability;
    
    private float potaAngle;
    private void Start()
    {

        Ball.transform.position = BallStarPoint.position;

        HeightDifferenceFormula();

    }


    private void Update()
    {

        if (FindObjectOfType<BasketballMode>().ended)
        {
            Ball.transform.position = BallStarPoint.position;
        }

        BasketChecker();

    }

    private void isPotaAngled()
    {
        potaAngleProbabilityRandom = Random.Range(1, 101);

        if (potaAngleProbabilityRandom <= potaAngleProbability)
        {
            potaAngle = Random.Range(-15, 15);
        }
        else
        {
            potaAngle = 0;
        }
    }




    private void BasketChecker()
    {

        if (FindObjectOfType<BasketballMode>().ended == true)
        {
            if (currentPota != null)
            {
                currentPota.GetComponent<Pota>().Basket = false;
                FindObjectOfType<BasketballMode>().timeOut = false;
                currentPota.GetComponent<Animator>().SetTrigger("BasketOldu");
            }

        }
        else
        {



            if (FindObjectOfType<BasketballMode>().ilkPota == true)
            {

                GeneratePota();
                FindObjectOfType<BasketballMode>().ilkPota = false;

            }

            if (currentPota != null)
            {
                if (currentPota.GetComponent<Pota>().Basket == true)
                {

                    BasketModeGoal = true;
                    currentPota.GetComponent<Animator>().SetTrigger("BasketOldu");

                    GeneratePota();

                    pota.GetComponent<Pota>().Basket = false;
                    updateScore = true;

                }

            }


            if (FindObjectOfType<BasketballMode>().timeOut == true)
            {

                currentPota.GetComponent<Animator>().SetTrigger("BasketOldu");

                GeneratePota();

                FindObjectOfType<BasketballMode>().holeCount = FindObjectOfType<BasketballMode>().holeCount - 1;

                FindObjectOfType<BasketballMode>().TimeBar.fillAmount = 1;

                FindObjectOfType<BasketballMode>().timeOut = false;

                updateScore = true;

            }


        }





    }

    private void DoGoalEffect()
    {

        //Instantiate(BasketParticleEffect, currentPota.transform.position, currentPota.transform.rotation * Quaternion.Euler(PotaRotation + new Vector3(-90,0,0)));

    }

    private void HeightDifferenceFormula()
    {

        heightDiff = FindObjectOfType<ViewportHandler>()._height - minHeight;
        anchorDiff = heightDiff * specialNum;
        PotaAnchorY = minPotaAnchorY + anchorDiff;
        //print("HoleAnchorY: " +HoleAnchorY);


    }

    private void GeneratePota()
    {

        RandomPotaPosX = Random.Range(-PotaAnchorX + 2.3f, +PotaAnchorX - 2.3f);
        RandomPotaPosY = Random.Range(-PotaAnchorY + 2f, +PotaAnchorY - 1f);
        potaPosition = new Vector3(RandomPotaPosX, RandomPotaPosY, 0);


        //rotation Y 180 reflects
        //rotation z 45 eğer

        isPotaAngled();

        potaReflection = Random.Range(0, 2);

        if (potaReflection == 0)
        {
            PotaRotation = new Vector3(0, 0, potaAngle);
        }

        if (potaReflection == 1)
        {
            PotaRotation = new Vector3(0, 180, potaAngle);
        }

        currentPota = Instantiate(pota, potaPosition, pota.transform.rotation * Quaternion.Euler(PotaRotation));


    }
}
